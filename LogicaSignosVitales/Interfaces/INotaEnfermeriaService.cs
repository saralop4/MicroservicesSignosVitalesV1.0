using DataSignosVitales.DTOs;
using DataSignosVitales.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSignosVitales.Entities.NotaEnfermeriaModels;
using DataCenso.DTOs;

namespace LogicaSignosVitales.Interfaces
{
    public  interface INotaEnfermeriaService
    {
        Task<NotaEnfermeriaDTOs> ObtenerSignoVital(string? numero);

        Task<NotaEnfermeriaDTOs> CrearSignoVital(NotaEnfermeriaDTOs notaEnfermeriaDtos);
        Task<NotaEnfermeriaDTOs> ObtenerSignoVitalxEvolucion(string? nroevolucionrelacionado);

        Task ActualizarPorNumeroSignovital(string? numero, NotaEnfermeriaDTOs notaEnfermeriaDtos);
        Task ActualizarPorNroEvolucionRelacionado(string? nroEvolucionRelacionado, NotaEnfermeriaDTOs notaEnfermeriaDtos);

        public Task<List<HistoricoSignoVitalDTOs>> MostarHistorico(int estudio);

    }
}

