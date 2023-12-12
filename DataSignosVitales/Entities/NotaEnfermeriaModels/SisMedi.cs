using System;
using System.Collections.Generic;

namespace DataSignosVitales.Entities.NotaEnfermeriaModels;

public partial class SisMedi
{
    public int Codigo { get; set; }

    public string? Cedula { get; set; }

    public string? Nombre { get; set; }

    public short? Especialidad { get; set; }

    public string? Registro { get; set; }

    public int? Tipo { get; set; }

    public int? Tiempo { get; set; }

    public short? EsUsuario { get; set; }

    public short? EsMedico { get; set; }

    public short? EsAnes { get; set; }

    public short? EsAyu { get; set; }

    public short? PagoProd { get; set; }

    public long? Valoresperado { get; set; }

    public short? EsPediatra { get; set; }

    public int? Pacientes { get; set; }

    public string? Servicio { get; set; }

    public int? CierraHistoria { get; set; }

    public short? EsAuditor { get; set; }

    public bool? AbreHistoria { get; set; }

    public int? Estado { get; set; }

    public short? EsEspecialista { get; set; }

    public string? Tercero { get; set; }

    public bool ApartaCita { get; set; }

    public string? Direccion { get; set; }

    public bool CitaExterna { get; set; }

    public string? Telefono { get; set; }

    public bool LeyendaConfirmarMedico { get; set; }

    public int? NivelMctos { get; set; }

    public bool? RequiereAuditoria { get; set; }

    public string? CodHistoriaPredeterminada { get; set; }

    public bool? EsMedicoFamiliar { get; set; }

    public bool? EsOdontologo { get; set; }

    public bool? EsPyp { get; set; }

    public int? MaxNoQx { get; set; }

    public int? MaxQx { get; set; }

    public int? MaxDiagnostico { get; set; }

    public int? MaxFormulas { get; set; }

    public int? MaxLaboratorios { get; set; }

    public int? MaxImagenologia { get; set; }

    public bool? EsPrioritario { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public short MaxFormulasPosfechadas { get; set; }

    public string? Firma { get; set; }

    public bool ModificaTriage { get; set; }

    public bool? EditarAuditor { get; set; }

    public bool ReclasificaRiesgo { get; set; }

    public string? TipoPago { get; set; }

    public string? CodManualPago { get; set; }

    public decimal? MontoPago { get; set; }

    public bool ExigeProcEvo { get; set; }

    public int? PagoParcial { get; set; }

    public int? EsEmpresa { get; set; }

    public string? Celular { get; set; }

    public string? Email { get; set; }

    public bool? Paliativo { get; set; }

    public bool? EditarBorrarEvos { get; set; }

    public int? EsVacunador { get; set; }

    public string? TipoIngreso { get; set; }
}
