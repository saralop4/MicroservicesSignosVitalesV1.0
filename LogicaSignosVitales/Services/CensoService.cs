using DataCenso.DTOs;
using DataSignosVitales.DTOs;
using DataSignosVitales.Entities.NotaEnfermeriaModels;
using DataSignosVitales.Interfaces;
using LogicaSignosVitales.Exceptions;
using LogicaSignosVitales.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;


namespace LogicaSignosVitales.Services
{
    public class CensoService : ICensoService
    {
        private readonly INotaEnfermeriaDbContext _notaenfermeriadbcontext;

        public CensoService(INotaEnfermeriaDbContext notaenfermeriadbcontext)
        {
            _notaenfermeriadbcontext = notaenfermeriadbcontext;
        }

        public async Task<List<SisCamaDTOs>> MostrarCamas(int? pabellon)
        {

            bool pabellonExistente = _notaenfermeriadbcontext.SisCamas.Any(u => u.Pabellon == pabellon);

            if (pabellonExistente)
            {

                var numeroMax = _notaenfermeriadbcontext.SisCamas
                    .Where(sc => sc.Pabellon == pabellon)
                    .SelectMany(
                        sc => _notaenfermeriadbcontext.NotasEnfermeria
                            .Where(ne => ne.Ingreso == sc.EstudioPaciente &&
                                         _notaenfermeriadbcontext.SisMaes.Any(m => m.ConEstudio == ne.Ingreso && m.Embarazo == "1"))
                            .DefaultIfEmpty(),
                        (sc, ne) => new { sc.EstudioPaciente, sc.Codigo, ne.Fecha, ne.Hora }
                    )
                    .GroupBy(x => new { x.EstudioPaciente, x.Codigo })
                    .Select(g => new
                    {
                        g.Key.EstudioPaciente,
                        g.Key.Codigo,
                        MaxFecha = g.Max(x => x.Fecha),
                        MaxHora = g.Max(x => x.Hora)
                    });


                var mainQuery = _notaenfermeriadbcontext.SisCamas
                    .Where(sc => sc.Pabellon == pabellon)
                    .GroupJoin(_notaenfermeriadbcontext.NotasEnfermeria,
                        sc => sc.EstudioPaciente,
                        ne => ne.Ingreso,
                        (sc, ne) => new { SisCama = sc, NotasEnfermeria = ne })
                    .SelectMany(
                        x => x.NotasEnfermeria.DefaultIfEmpty(),
                        (x, ne) => new { x.SisCama, NotasEnfermeria = ne })
                    .Join(numeroMax,
                        sc => new { sc.SisCama.EstudioPaciente, sc.SisCama.Codigo },
                        nm => new { nm.EstudioPaciente, nm.Codigo },
                        (sc, nm) => new
                        {
                            sc.SisCama.EstudioPaciente,
                            sc.SisCama.Codigo
                        });

                var unionQuery = _notaenfermeriadbcontext.SisCamas
                        .Where(sc => sc.Pabellon == pabellon && (sc.EstudioPaciente == null || sc.EstudioPaciente == -1))
                        .Select(sc => new
                        {
                            sc.EstudioPaciente,
                            sc.Codigo
                        });

                var finalQuery = mainQuery.Union(unionQuery);


                var listaSinValidar = await finalQuery.ToListAsync();
                var listaConValidacion = new List<SisCamaDTOs>();



                foreach (var item in listaSinValidar)
                {
                    bool isEmbarazo = _notaenfermeriadbcontext.SisMaes.Any(m => m.ConEstudio == item.EstudioPaciente && m.Embarazo == "1");

                    if (item.EstudioPaciente != null && item.EstudioPaciente.Value != -1 && isEmbarazo)
                    {
                        (string? color, string? observacion) = await ValidarEstadoConciencia(item.EstudioPaciente.Value);

                        listaConValidacion.Add(new SisCamaDTOs
                        {
                            Estudio = (int)item.EstudioPaciente,
                            Nro_Cama = item.Codigo,
                            Color = color,
                            Observacion = observacion
                        });
                    }
                    else
                    {
                        listaConValidacion.Add(new SisCamaDTOs
                        {
                            Estudio = (int)(item.EstudioPaciente ?? -1),
                            Nro_Cama = item.Codigo,
                            Color = "No aplicable",
                            Observacion = "No aplicable"
                        });
                    }
                }

                if (listaConValidacion != null)
                {
                    return listaConValidacion;
                }

                throw new ValidationListaConValidacionNullReferenceException();

            }

            throw new ValidationArgumentsEntityNullException();
        }


        private async Task<(string?, string?)> ValidarEstadoConciencia(int estudioPaciente)
        {
                     
            var result = await ConsultarLosSignosVitales(estudioPaciente);

            if (result.TensionArterial == "-1")
            {
                string vacio = "No aplicable";                
                return (vacio, vacio);
            };

            var notasConColor = result.Color;


            if (string.IsNullOrEmpty(notasConColor))
            {
                var datos = new DatosCalculoColorDTOs
                {

                    TensionArterial = result.TensionArterial,
                    FrecuenciaRespiratoria = result.FrecuenciaRespiratoria,
                    FrecuenciaCardiaca = result.FrecuenciaCardiaca,
                    Temperatura = result.Temperatura,
                    Oxigeno = result.Oxigeno,
                    EstadoConciencia = result.EstadoConciencia
                };

                int  sumatoria = NotaEnfemeriaService.Sumatoria(datos);
                notasConColor = NotaEnfemeriaService.CalcularColor(sumatoria);
            }
                string mensajeAlerta = "";

                if (notasConColor == "Rojo")
                {

                    mensajeAlerta = "Monitoreo continuo de signos vitales - Emergente al equipo con competencias en el diagnostico";
                }
                else if (notasConColor == "Naranja")
                {
                    mensajeAlerta = "Mínimo cada hora - Urgente al equipo médico a cargo de la paciente y al personal con competencias para manejo de la enfermedad aguda.";
                }
                else if (notasConColor == "Amarillo")
                {
                    mensajeAlerta = "Mínimo cada 4 horas - Llamado a enfermera a cargo";
                }
                else if (notasConColor == "Blanco")
                {
                    mensajeAlerta = "Observación de rutina";
                }

                return (notasConColor, mensajeAlerta);
        }


        private async Task<NotaEnfermeriaDTOs?> ConsultarLosSignosVitales(int estudioPaciente)
        { 

            var notasFiltradas = await _notaenfermeriadbcontext.NotasEnfermeria
                .Where(ne => ne.Ingreso == estudioPaciente)
                .Select(ne => new
                {
                    ne.Numero,
                    ne.Ingreso,
                    ne.Ta,
                    ne.Fc,
                    ne.Fr,
                    ne.Tp,
                    ne.O2,
                    ne.EstadoConciencia,
                    ne.Color,
                    ne.Fecha,
                    ne.Hora
                })
                .ToListAsync();

            var maxFechaHora = notasFiltradas
                .OrderByDescending(ne => ne.Fecha)
                .ThenByDescending(ne => ne.Hora)
                .FirstOrDefault();

            NotaEnfermeriaDTOs resultado = null;
            if (maxFechaHora != null)
            {
                resultado = new NotaEnfermeriaDTOs
                {
                    TensionArterial = maxFechaHora.Ta,
                    FrecuenciaCardiaca = maxFechaHora.Fc,
                    FrecuenciaRespiratoria = maxFechaHora.Fr,
                    Temperatura = maxFechaHora.Tp,
                    Oxigeno = maxFechaHora.O2,
                    EstadoConciencia = maxFechaHora.EstadoConciencia ?? false,
                    Color = maxFechaHora.Color
              
                };
            }


            bool existeEnSisMaes = resultado != null && _notaenfermeriadbcontext.SisMaes
                .Any(sm => sm.ConEstudio == estudioPaciente && sm.Embarazo == "1");


            if (existeEnSisMaes)
            {
                return resultado;
            }
            else
            {
                return resultado ?? new NotaEnfermeriaDTOs
                {
                    TensionArterial = "-1",
                    FrecuenciaCardiaca = "0",
                    FrecuenciaRespiratoria = "0",
                    Temperatura = 0M,
                    Oxigeno = "0",
                    EstadoConciencia = false,
                    Color = "no aplica"

                };
            }

        }

    }
}
