using System;
using System.Collections.Generic;

namespace DataSignosVitales.Entities.NotaEnfermeriaModels;

public partial class NotasEnfermerium
{
    public string Numero { get; set; } = null!;

    public int? Ingreso { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Hora { get; set; }

    public string Resumen { get; set; } = null!;

    public string? Enfermera { get; set; }

    public string? CodEnfer { get; set; }

    public string? Ta { get; set; }

    public string? Fc { get; set; }

    public string? Fr { get; set; }

    public string? Ps { get; set; }

    public decimal? Tp { get; set; }

    public string? O2 { get; set; }

    public int? SoloVitales { get; set; }

    public int? Calificacion { get; set; }

    public bool ExisteDatosQx { get; set; }

    public DateTime? FechaDatosQx { get; set; }

    public string? HoraInicioQx { get; set; }

    public string? HoraFinQx { get; set; }

    public int? InstrumentadorQx { get; set; }

    public int? RotadorQx { get; set; }

    public bool Cerrada { get; set; }

    public decimal? Glucometria { get; set; }

    public string? NroEvolucionRelacionado { get; set; }

    public bool Dinamica { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public int? Ufuncional { get; set; }

    public long? IdCita { get; set; }

    public string? Tam { get; set; }

    public bool? EstadoConciencia { get; set; }

    public string? Color { get; set; }

    public int? PuntuacionEscala { get; set; }
}
