using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSignosVitales.DTOs
{
    public class DatosCalculoColorDTOs
    {

        public string? TensionArterial { get; set; }

        public string? FrecuenciaCardiaca { get; set; }

        public string? FrecuenciaRespiratoria { get; set; }

        public decimal? Temperatura { get; set; }

        public string? Oxigeno { get; set; }

        public bool? EstadoConciencia { get; set; } = false;

    }
}
