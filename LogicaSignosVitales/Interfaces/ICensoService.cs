using DataCenso.DTOs;
using DataSignosVitales.DTOs;
using DataSignosVitales.Entities.NotaEnfermeriaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Interfaces
{
    public interface ICensoService
    {
        public Task<List<SisCamaDTOs>> MostrarCamas(int? pabellon);


    }
}
