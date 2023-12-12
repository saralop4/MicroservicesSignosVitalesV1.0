using DataCenso.DTOs;
using DataSignosVitales.DTOs;
using DataSignosVitales.Entities.NotaEnfermeriaModels;
using DataSignosVitales.Interfaces;
using LogicaSignosVitales.Exceptions;
using LogicaSignosVitales.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LogicaSignosVitales.Services

{
    public class NotaEnfemeriaService : INotaEnfermeriaService
    {
        private readonly INotaEnfermeriaDbContext _notaenfermeriadbcontext;

        public NotaEnfemeriaService(INotaEnfermeriaDbContext notaenfermeriadbcontext)
        {
            _notaenfermeriadbcontext = notaenfermeriadbcontext;
        }
        public async Task ActualizarPorNumeroSignovital(string? numero_SignoVital, NotaEnfermeriaDTOs? notaEnfermeriaDtos)
        {

            if (numero_SignoVital != null && notaEnfermeriaDtos != null)
            {
                var entidadExistente = await ValidarUpdatePorNumero(numero_SignoVital);


                var datosCalculoColor = new DatosCalculoColorDTOs
                {

                    TensionArterial = notaEnfermeriaDtos.TensionArterial,
                    FrecuenciaRespiratoria = notaEnfermeriaDtos.FrecuenciaRespiratoria,
                    FrecuenciaCardiaca = notaEnfermeriaDtos.FrecuenciaCardiaca,
                    Temperatura = notaEnfermeriaDtos.Temperatura,
                    Oxigeno = notaEnfermeriaDtos.Oxigeno,
                    EstadoConciencia = notaEnfermeriaDtos.EstadoConciencia
                };

                var sumatoria = Sumatoria(datosCalculoColor);

                var color = CalcularColor(sumatoria);


                if (entidadExistente != null)
                {

                    entidadExistente.Ingreso = notaEnfermeriaDtos.Estudio;
                    entidadExistente.Fecha = notaEnfermeriaDtos.Fecha;
                    entidadExistente.Hora = notaEnfermeriaDtos.Hora;
                    entidadExistente.Resumen = notaEnfermeriaDtos.Nota ?? " ";
                    entidadExistente.Enfermera = notaEnfermeriaDtos.Enfermera;
                    entidadExistente.CodEnfer = notaEnfermeriaDtos.CodigoEnfermera;
                    entidadExistente.Ta = notaEnfermeriaDtos.TensionArterial;
                    entidadExistente.Fc = notaEnfermeriaDtos.FrecuenciaCardiaca;
                    entidadExistente.Fr = notaEnfermeriaDtos.FrecuenciaRespiratoria;
                    entidadExistente.Ps = notaEnfermeriaDtos.Peso;
                    entidadExistente.Tp = notaEnfermeriaDtos.Temperatura;
                    entidadExistente.O2 = notaEnfermeriaDtos.Oxigeno;
                    entidadExistente.SoloVitales = notaEnfermeriaDtos.SoloVitales;
                    entidadExistente.Cerrada = notaEnfermeriaDtos.Cerrada;
                    entidadExistente.Glucometria = notaEnfermeriaDtos.Glucometria;
                    entidadExistente.Ufuncional = notaEnfermeriaDtos.UnidadFuncional;
                    entidadExistente.Tam = notaEnfermeriaDtos.Tamizaje;
                    entidadExistente.EstadoConciencia = notaEnfermeriaDtos.EstadoConciencia;
                    entidadExistente.NroEvolucionRelacionado = notaEnfermeriaDtos.NroEvolucionRelacionado ?? null;
                    entidadExistente.Color = color;
                    entidadExistente.PuntuacionEscala = sumatoria;

                    await _notaenfermeriadbcontext.SaveChangesAsync();

                }
                else
                {
                    throw new ValidationEntityExisting();
                }
            }
            else
            {
                throw new ValidationArgumentsEntityNullException();
            }
        }

        public async Task<NotaEnfermeriaDTOs> CrearSignoVital(NotaEnfermeriaDTOs notaEnfermeriaDtos)
        {

            await ValidarNumero(notaEnfermeriaDtos.Numero_SignoVital);

            var estudioValido = await ValidarEstudioSisMae(notaEnfermeriaDtos.Estudio);

            await ValidarCodigoEnfermera(notaEnfermeriaDtos.CodigoEnfermera);


            var datosCalculoColor = new DatosCalculoColorDTOs
            {

                TensionArterial = notaEnfermeriaDtos.TensionArterial,
                FrecuenciaRespiratoria = notaEnfermeriaDtos.FrecuenciaRespiratoria,
                FrecuenciaCardiaca = notaEnfermeriaDtos.FrecuenciaCardiaca,
                Temperatura = notaEnfermeriaDtos.Temperatura,
                Oxigeno = notaEnfermeriaDtos.Oxigeno,
                EstadoConciencia = notaEnfermeriaDtos.EstadoConciencia
            };

            var sumatoria = Sumatoria(datosCalculoColor);

            var color = CalcularColor(sumatoria);


            var create = new NotasEnfermerium
            {
                Numero = notaEnfermeriaDtos.Numero_SignoVital,
                Ingreso = estudioValido,
                Fecha = notaEnfermeriaDtos.Fecha,
                Hora = notaEnfermeriaDtos.Hora,
                Resumen = notaEnfermeriaDtos.Nota ?? " ",
                Enfermera = notaEnfermeriaDtos.Enfermera,
                CodEnfer = notaEnfermeriaDtos.CodigoEnfermera,
                Ta = notaEnfermeriaDtos.TensionArterial,
                Fc = notaEnfermeriaDtos.FrecuenciaCardiaca,
                Fr = notaEnfermeriaDtos.FrecuenciaRespiratoria,
                Ps = notaEnfermeriaDtos.Peso,
                Tp = notaEnfermeriaDtos.Temperatura,
                O2 = notaEnfermeriaDtos.Oxigeno,
                SoloVitales = notaEnfermeriaDtos.SoloVitales,
                Cerrada = notaEnfermeriaDtos.Cerrada,
                Glucometria = notaEnfermeriaDtos.Glucometria,
                Ufuncional = notaEnfermeriaDtos.UnidadFuncional,
                Tam = notaEnfermeriaDtos.Tamizaje,
                EstadoConciencia = notaEnfermeriaDtos.EstadoConciencia,
                NroEvolucionRelacionado = notaEnfermeriaDtos.NroEvolucionRelacionado,
                Color = color,
                PuntuacionEscala = sumatoria


            };


            _notaenfermeriadbcontext.NotasEnfermeria.Add(create);
            await _notaenfermeriadbcontext.SaveChangesAsync();

            return notaEnfermeriaDtos;

        }

        public async Task<NotaEnfermeriaDTOs> ObtenerSignoVital(string? numero_SignoVital)
        {
            if (string.IsNullOrEmpty(numero_SignoVital))
            {
                throw new ValidationNumeroSignoVitalRequeridoException();
            }

            var registroNotaEnfermeria = await _notaenfermeriadbcontext.NotasEnfermeria
                .FirstOrDefaultAsync(u => u.Numero == numero_SignoVital);

            if (registroNotaEnfermeria != null)
            {
                var numeroDtos = new NotaEnfermeriaDTOs
                {
                    Numero_SignoVital = registroNotaEnfermeria.Numero,
                    Estudio = registroNotaEnfermeria.Ingreso.HasValue ? (int)registroNotaEnfermeria.Ingreso : 0,
                    Fecha = registroNotaEnfermeria.Fecha ?? DateTime.MinValue,
                    Hora = registroNotaEnfermeria.Hora,
                    Nota = registroNotaEnfermeria.Resumen ?? string.Empty,
                    Enfermera = registroNotaEnfermeria.Enfermera ?? string.Empty,
                    CodigoEnfermera = registroNotaEnfermeria.CodEnfer ?? "0",
                    TensionArterial = registroNotaEnfermeria.Ta ?? "0",
                    FrecuenciaCardiaca = registroNotaEnfermeria.Fc ?? "0",
                    FrecuenciaRespiratoria = registroNotaEnfermeria.Fr ?? "0",
                    Peso = registroNotaEnfermeria.Ps ?? "0",
                    Temperatura = registroNotaEnfermeria.Tp ?? 0.0m,
                    Oxigeno = registroNotaEnfermeria.O2 ?? "0",
                    SoloVitales = registroNotaEnfermeria.SoloVitales.HasValue ? (int)registroNotaEnfermeria.SoloVitales : 0,
                    Cerrada = registroNotaEnfermeria.Cerrada,
                    Glucometria = registroNotaEnfermeria.Glucometria ?? 0.0m,
                    UnidadFuncional = registroNotaEnfermeria.Ufuncional.HasValue ? (int)registroNotaEnfermeria.Ufuncional : 0,
                    Tamizaje = registroNotaEnfermeria.Tam ?? string.Empty,
                    EstadoConciencia = registroNotaEnfermeria.EstadoConciencia ?? false,
                    NroEvolucionRelacionado = registroNotaEnfermeria.NroEvolucionRelacionado ?? "0"
                };

                return numeroDtos;
            }

            throw new ValidationEntityExisting();
        }
        public async Task<NotaEnfermeriaDTOs> ObtenerSignoVitalxEvolucion(string? nroevolucionrelacionado)
        {
            if (string.IsNullOrEmpty(nroevolucionrelacionado))
            {
                throw new ValidationNroEvolucionSignoVitalRequeridoException();
            }

            var registroNotaEnfermeria = await _notaenfermeriadbcontext.NotasEnfermeria
                .FirstOrDefaultAsync(u => u.NroEvolucionRelacionado == nroevolucionrelacionado);

            if (registroNotaEnfermeria != null)
            {
                var numeroDtos = new NotaEnfermeriaDTOs
                {
                    Numero_SignoVital = registroNotaEnfermeria.Numero,
                    Estudio = registroNotaEnfermeria.Ingreso.HasValue ? (int)registroNotaEnfermeria.Ingreso : 0,
                    Fecha = registroNotaEnfermeria.Fecha ?? DateTime.MinValue,
                    Hora = registroNotaEnfermeria.Hora,
                    Nota = registroNotaEnfermeria.Resumen ?? string.Empty,
                    Enfermera = registroNotaEnfermeria.Enfermera ?? string.Empty,
                    CodigoEnfermera = registroNotaEnfermeria.CodEnfer ?? "0",
                    TensionArterial = registroNotaEnfermeria.Ta ?? "0",
                    FrecuenciaCardiaca = registroNotaEnfermeria.Fc ?? "0",
                    FrecuenciaRespiratoria = registroNotaEnfermeria.Fr ?? "0",
                    Peso = registroNotaEnfermeria.Ps ?? "0",
                    Temperatura = registroNotaEnfermeria.Tp ?? 0.0m,
                    Oxigeno = registroNotaEnfermeria.O2 ?? "0",
                    SoloVitales = registroNotaEnfermeria.SoloVitales.HasValue ? (int)registroNotaEnfermeria.SoloVitales : 0,
                    Cerrada = registroNotaEnfermeria.Cerrada,
                    Glucometria = registroNotaEnfermeria.Glucometria ?? 0.0m,
                    UnidadFuncional = registroNotaEnfermeria.Ufuncional.HasValue ? (int)registroNotaEnfermeria.Ufuncional : 0,
                    Tamizaje = registroNotaEnfermeria.Tam ?? string.Empty,
                    EstadoConciencia = registroNotaEnfermeria.EstadoConciencia ?? false,
                    NroEvolucionRelacionado = registroNotaEnfermeria.NroEvolucionRelacionado ?? "0"
                };

                return numeroDtos;
            }

            throw new ValidationEntityExisting();
        }

        private async Task<int> ValidarEstudioSisMae(int? estudio)
        {
            if (estudio.HasValue)
            {
                var ingreso = await _notaenfermeriadbcontext.SisMaes
                    .Where(md => md.ConEstudio == estudio.Value)
                    .Select(md => md.ConEstudio)
                    .FirstOrDefaultAsync();

                if (ingreso != 0)
                {
                    return (int)ingreso;
                }
            }

            throw new ValidationEstudioException();
        }

        private async Task ValidarNumero(string numero_SignoVital)
        {

            var numeroSignoVital = await _notaenfermeriadbcontext.NotasEnfermeria
                .Where(nm => nm.Numero == numero_SignoVital)
                .Select(nm => nm.Numero)
                .FirstOrDefaultAsync();

            if (numeroSignoVital != null)
            {
                throw new ValidationNumeroSignoVitalDbUpdateException();
            }

        }

        private async Task ValidarCodigoEnfermera(string codigoEnfermera)
        {
            var enfermera = await _notaenfermeriadbcontext.SisMedis
                .Where(sm => sm.Codigo.ToString() == codigoEnfermera)
                .Select(sm => sm.Codigo)
                .FirstOrDefaultAsync();

            if (enfermera.ToString() == null)
            {
                throw new ValidationCodigoEnfermeraExcepcion();
            }


        }

        private async Task<NotasEnfermerium?> ValidarUpdatePorNumero(string numero_SignoVital)
        {

            var objetoExistente = await _notaenfermeriadbcontext.NotasEnfermeria
           .Where(nm => nm.Numero == numero_SignoVital)
           .FirstOrDefaultAsync();

            if (objetoExistente == null)
            {
                throw new ValidationEntityExisting();
            }
            return objetoExistente;
        }

        private async Task<NotasEnfermerium?> ValidarUpdatePorNroEvolucionRelacionado(string nro_EvolucionRelacionado)
        {
            var objetoExistente = await _notaenfermeriadbcontext.NotasEnfermeria
                .Where(nm => nm.NroEvolucionRelacionado == nro_EvolucionRelacionado)
                .FirstOrDefaultAsync();

            if (objetoExistente == null)
            {
                throw new ValidationEntityExisting();
            }
            return objetoExistente;
        }

        public async Task ActualizarPorNroEvolucionRelacionado(string? nroEvolucionRelacionado, NotaEnfermeriaDTOs notaEnfermeriaDtos)
        {


            if (nroEvolucionRelacionado != null && notaEnfermeriaDtos != null)
            {

                var entityExisting = await ValidarUpdatePorNroEvolucionRelacionado(nroEvolucionRelacionado);


                if (entityExisting != null)
                {

                    entityExisting.Ingreso = notaEnfermeriaDtos.Estudio;
                    entityExisting.Fecha = notaEnfermeriaDtos.Fecha;
                    entityExisting.Hora = notaEnfermeriaDtos.Hora;
                    entityExisting.Resumen = notaEnfermeriaDtos.Nota;
                    entityExisting.Enfermera = notaEnfermeriaDtos.Enfermera;
                    entityExisting.CodEnfer = notaEnfermeriaDtos.CodigoEnfermera;
                    entityExisting.Ta = notaEnfermeriaDtos.TensionArterial;
                    entityExisting.Fc = notaEnfermeriaDtos.FrecuenciaCardiaca;
                    entityExisting.Fr = notaEnfermeriaDtos.FrecuenciaRespiratoria;
                    entityExisting.Ps = notaEnfermeriaDtos.Peso;
                    entityExisting.Tp = notaEnfermeriaDtos.Temperatura;
                    entityExisting.O2 = notaEnfermeriaDtos.Oxigeno;
                    entityExisting.SoloVitales = notaEnfermeriaDtos.SoloVitales;
                    entityExisting.Cerrada = notaEnfermeriaDtos.Cerrada;
                    entityExisting.Glucometria = notaEnfermeriaDtos.Glucometria;
                    entityExisting.Ufuncional = notaEnfermeriaDtos.UnidadFuncional;
                    entityExisting.Tam = notaEnfermeriaDtos.Tamizaje;
                    entityExisting.EstadoConciencia = notaEnfermeriaDtos.EstadoConciencia;
                    entityExisting.NroEvolucionRelacionado = notaEnfermeriaDtos.NroEvolucionRelacionado ?? null;

                    await _notaenfermeriadbcontext.SaveChangesAsync();
                }
                else
                {
                    throw new ValidationEntityExisting();
                }
            }
            else
            {
                throw new ValidationArgumentsEntityNullException();
            }
        }

        public static int Sumatoria(DatosCalculoColorDTOs dato)
        {

            int sumatoria = 0;
            string? cadenaOriginal = dato.TensionArterial;
            char separador = '/';

            string[] subcadena = cadenaOriginal.Split(separador);

            for (int i = 0; i < subcadena.Length; i++)
            {
                if (int.TryParse(subcadena[i], out int taValue))
                {
                    if (i == 0) // PAS
                    {
                        sumatoria +=
                            (taValue < 80) ? 3 :
                            (taValue >= 80 && taValue <= 89) ? 2 :
                            (taValue >= 90 && taValue <= 139) ? 0 :
                            (taValue >= 140 && taValue <= 149) ? 1 :
                            (taValue >= 150 && taValue <= 159) ? 2 :
                            (taValue >= 160) ? 3 : 0;

                    }
                    else if (i == 1) // PAD
                    {
                        sumatoria +=
                            (taValue < 90) ? 0 :
                            (taValue >= 90 && taValue <= 99) ? 1 :
                            (taValue >= 100 && taValue <= 109) ? 2 :
                            (taValue >= 110) ? 3 : 0;

                    }
                }
            }

            if (int.TryParse(dato.FrecuenciaRespiratoria, out int frValue))
            {
                sumatoria +=
                    (frValue < 10) ? 3 :
                    (frValue >= 10 && frValue <= 17) ? 0 :
                    (frValue >= 18 && frValue <= 24) ? 1 :
                    (frValue >= 25 && frValue <= 29) ? 2 :
                    (frValue >= 30) ? 3 : 0;

            }

            if (int.TryParse(dato.FrecuenciaCardiaca, out int fcValue))
            {
                sumatoria +=
                    (fcValue < 60) ? 3 :
                    (fcValue >= 60 && fcValue <= 110) ? 0 :
                    (fcValue >= 111 && fcValue <= 149) ? 2 :
                    (fcValue >= 150) ? 3 : 0;


            }


            if (dato.Oxigeno.Equals("aire ambiente", StringComparison.OrdinalIgnoreCase))
            {
                sumatoria += 0;
            }
            else if (int.TryParse(dato.Oxigeno, out int o2Value))
            {
                sumatoria +=
                    (o2Value >= 24 && o2Value <= 39) ? 1 :
                    (o2Value >= 40) ? 3 : 0;
            }

            double? tpValue = (double?)dato.Temperatura;
            if (tpValue.HasValue)
            {
                sumatoria +=
                    (tpValue < 34.0) ? 3 :
                    (tpValue >= 34.0 && tpValue <= 35.0) ? 1 :
                    (tpValue >= 35.1 && tpValue <= 37.9) ? 0 :
                    (tpValue >= 38.0 && tpValue <= 38.9) ? 1 :
                    (tpValue >= 39) ? 3 : 0;

            }

            if (dato.EstadoConciencia == true)
            {
                sumatoria += 0;
            }
            else
            {
                sumatoria += 3;

            }


            //Console.WriteLine("SUMATORIA TOTAL: ");
            //Console.WriteLine(JsonConvert.SerializeObject(sumatoria));

            return sumatoria;


        }

        public static string CalcularColor(int sumatoria)
        {
            string color;

            if (sumatoria >= 6)
            {
                color = "Rojo";

            }
            else if (sumatoria >= 4 && sumatoria < 6)
            {
                color = "Naranja";

            }
            else if (sumatoria >= 1 && sumatoria <= 3)
            {
                color = "Amarillo";

            }
            else
            {
                color = "Blanco";

            }

            return color;


        }

        public async Task<List<HistoricoSignoVitalDTOs>> MostarHistorico(int estudio)
        {
            if (string.IsNullOrEmpty(estudio.ToString()))
            {
                throw new ValidationNumeroSignoVitalRequeridoException();
            }

            bool registroNotaEnfermeria = _notaenfermeriadbcontext.NotasEnfermeria.Any(u => u.Ingreso == estudio);

            var listaConValidacion = new List<HistoricoSignoVitalDTOs>();

            if (registroNotaEnfermeria)
            {
                var resultado = await _notaenfermeriadbcontext.NotasEnfermeria
                    .Where(nota => nota.Ingreso == estudio)
                    .OrderByDescending(nota => nota.Fecha)
                    .ThenByDescending(nota => nota.Hora)
                    .ToListAsync();



                foreach (var item in resultado)
                {
                    if (item.Ingreso != null && item.Ingreso != -1)
                    {
                        listaConValidacion.Add(new HistoricoSignoVitalDTOs
                        {
                            Numero_SignoVital = item.Numero,
                            Estudio = item.Ingreso,
                            Fecha = item.Fecha,
                            Hora = item.Hora,
                            Nota = item.Resumen,
                            Enfermera = item.Enfermera,
                            CodigoEnfermera = item.CodEnfer,
                            TensionArterial = item.Ta,
                            FrecuenciaCardiaca = item.Fc,
                            FrecuenciaRespiratoria = item.Fr,
                            Peso = item.Ps,
                            Temperatura = item.Tp,
                            Oxigeno = item.O2,
                            Glucometria = item.Glucometria,
                            Tamizaje = item.Tam,
                            PuntuacionEscala = item.PuntuacionEscala,
                            Color = item.Color

                        });
                    }
                    else
                    {
                        listaConValidacion.Add(new HistoricoSignoVitalDTOs
                        {
                            Numero_SignoVital = item.Numero,
                            Estudio = 0,
                            Fecha = DateTime.MinValue,
                            Hora = item.Hora,
                            Nota = string.Empty,
                            Enfermera = string.Empty,
                            CodigoEnfermera = "0",
                            TensionArterial = "0",
                            FrecuenciaCardiaca = "0",
                            FrecuenciaRespiratoria = "0",
                            Peso = "0",
                            Temperatura = 0.0m,
                            Oxigeno = "0",
                            Glucometria = 0.0m,
                            Tamizaje = string.Empty,
                            PuntuacionEscala = 0,
                            Color = " "
                        });
                    }
                }
            }

            return listaConValidacion;
        }

    }
}
