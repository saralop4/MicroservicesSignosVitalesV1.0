using System;
using System.Collections.Generic;

namespace DataSignosVitales.Entities.NotaEnfermeriaModels;

public partial class SisCama
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public int Pabellon { get; set; }

    public long? Ingreso { get; set; }

    public int? Paciente { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Servicio { get; set; }

    public long? EstadoPaciente { get; set; }

    public int? EstudioPaciente { get; set; }

    public int? TipoCama { get; set; }

    public string? Manual001 { get; set; }

    public string? Manual009 { get; set; }

    public bool FacturarEstancia { get; set; }

    public int? Estado { get; set; }

    public string? DetalleEstado { get; set; }

    public string? Manual0010 { get; set; }

    public string? Manual02 { get; set; }

    public string? Manual05 { get; set; }

    public string? Manual654 { get; set; }

    public string? Manual300 { get; set; }

    public string? Manual895 { get; set; }

    public string? Manual400 { get; set; }

    public string? Manual4569 { get; set; }

    public string? Manual100 { get; set; }

    public string? Manual594 { get; set; }

    public string? Manual0297 { get; set; }

    public string? Manual310 { get; set; }

    public string? Manual200 { get; set; }

    public int Id { get; set; }

    public string? Manual9001 { get; set; }

    public string? Manual9002 { get; set; }

    public string? Manual9004 { get; set; }

    public string? Manual9005 { get; set; }

    public int PuntoAtencion { get; set; }

    public int? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public long? EstudioReserva { get; set; }

    public long? EstadoPacienteReserva { get; set; }
}
