using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataSignosVitales.DTOs
{
    public class HistoricoSignoVitalDTOs
    {
        public string? Numero_SignoVital { get; set; }

        public int? Estudio { get; set; }


        public DateTime? Fecha { get; set; }


        public string? Hora { get; set; }

        public string? Nota { get; set; } = " ";


        public string? Enfermera { get; set; }



        public string? CodigoEnfermera { get; set; }


        public string? TensionArterial { get; set; }


        public string? FrecuenciaCardiaca { get; set; }


        public string? FrecuenciaRespiratoria { get; set; }

        public string Peso { get; set; }


        public decimal? Temperatura { get; set; }


        public string? Oxigeno { get; set; }


        public decimal? Glucometria { get; set; }


        public string? Tamizaje { get; set; }


        public string? Color { get; set; } = " ";

        public int? PuntuacionEscala { get; set; }

    }
}
