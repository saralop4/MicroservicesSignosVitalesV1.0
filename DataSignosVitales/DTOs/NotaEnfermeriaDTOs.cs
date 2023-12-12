using DataSignosVitales.Entities.NotaEnfermeriaModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataSignosVitales.DTOs
{
    public class NotaEnfermeriaDTOs
    {

        [RegularExpression(@"^[0-9]+$")]
        [MaxLength(10)]
        public string? Numero_SignoVital { get; set; }

        [Required]
        [RegularExpression(@"^\d+$")]
        public int? Estudio { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Fecha { get; set; }

        [Required]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$")]
        public string? Hora { get; set; }

        public string? Nota { get; set; } = " ";

        [RegularExpression(@"^[a-zA-Z ]+$")]
        [Required]
        public string? Enfermera { get; set; }


        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^\d+$")]
        public string? CodigoEnfermera { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression("^[0-9/]+$")]
        public string? TensionArterial { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[0-9]+(\.[0-9]+)?$")]
        public string? FrecuenciaCardiaca { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[0-9]+(\.[0-9]+)?$")]
        public string? FrecuenciaRespiratoria { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        public string Peso { get; set; }

        [Required]
        [RegularExpression(@"^\d+([.,]\d+)?$")]
        public decimal? Temperatura { get; set; }


        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$")]
        public string? Oxigeno { get; set; }

        public int? SoloVitales { get; set; } = 1; 

        public bool Cerrada { get; set; } = false;

        [Required]
        [RegularExpression(@"^[0-9]+(\.[0-9]+)?$")]
        public decimal? Glucometria { get; set; }

        
        [RegularExpression(@"^[0-9]+(\.[0-9]+)?$")]
        public int? UnidadFuncional { get; set; }

  
        [MaxLength(10)]
        [RegularExpression(@"^\d+(\.\d+)?$")]
        public string? Tamizaje { get; set; }


        [Required]
        public bool? EstadoConciencia { get; set; } = false;

        public string? NroEvolucionRelacionado { get; set; }

        [JsonIgnore]
        public string? Color { get; set; } = " ";

    }
}
