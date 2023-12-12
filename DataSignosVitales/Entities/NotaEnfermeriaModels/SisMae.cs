using System;
using System.Collections.Generic;

namespace DataSignosVitales.Entities.NotaEnfermeriaModels;

public partial class SisMae
{
    public int Autoid { get; set; }

    public string? CodEntidad { get; set; }

    public string? CodClasi { get; set; }

    public long ConEstudio { get; set; }

    public string? TipoEstudio { get; set; }

    public string? CodEspeci { get; set; }

    public int? ViaIngreso { get; set; }

    public DateTime? FechaIng { get; set; }

    public DateTime? FechaEgr { get; set; }

    public string? HoraIng { get; set; }

    public string? HoraEgr { get; set; }

    public string? NroAutoriza { get; set; }

    public string? DiagnoIng { get; set; }

    public string? CausaExt { get; set; }

    public string? DiagnoEgr { get; set; }

    public string? DiagnoEgr1 { get; set; }

    public string? DiagnoEgr2 { get; set; }

    public string? DiagnoEgr3 { get; set; }

    public string? DiagnoCom { get; set; }

    public int? DestinoUsu { get; set; }

    public int? EstadoEgr { get; set; }

    public string? CausaMte { get; set; }

    public string? CodMedico { get; set; }

    public string? MedicoEgr { get; set; }

    public int? NroFactura { get; set; }

    public decimal? VlrFactura { get; set; }

    public string? ValorLetra { get; set; }

    public decimal? VlrDescto { get; set; }

    public decimal? VlrCoopago { get; set; }

    public decimal? VlrNeto { get; set; }

    public double? VlrExcedente { get; set; }

    public double? VlrAbono { get; set; }

    public double? VlrPagado { get; set; }

    public long NroCama { get; set; }

    public int? StatusRegis { get; set; }

    public string? CodUsuario { get; set; }

    public string? NomUsuario { get; set; }

    public DateTime? FechaUsuario { get; set; }

    public string? HoraCrea { get; set; }

    public string? Estado { get; set; }

    public int? MotivoEgr { get; set; }

    public string? Cama2 { get; set; }

    public string? CodEgreso { get; set; }

    public string? NomEgreso { get; set; }

    public DateTime? FechaEgreso { get; set; }

    public string? Cuenta { get; set; }

    public string? Obs { get; set; }

    public long? Contrato { get; set; }

    public string? Remision { get; set; }

    public short? EstadoRes { get; set; }

    public DateTime? FechaEstadoRes { get; set; }

    public string? UsuarioEstadoRes { get; set; }

    public short? Contabilizado { get; set; }

    public short Satisfaccion { get; set; }

    public short ComplicaQx { get; set; }

    public short TipoMuerte { get; set; }

    public DateTime? FechaContabilizado { get; set; }

    public int? EmpAsumeDesc { get; set; }

    public DateTime? FechaParto { get; set; }

    public string? CodigoServicio { get; set; }

    public string? LoteZeus { get; set; }

    public DateTime? FechaLoteZeus { get; set; }

    public int? Terapia { get; set; }

    public int? Medicamentos { get; set; }

    public string? NitAsegura { get; set; }

    public string? RsAsegura { get; set; }

    public DateTime? FechaAcc { get; set; }

    public DateTime? FechaAlta { get; set; }

    public string? ConsecSoat { get; set; }

    public string? NoPoliza { get; set; }

    public string? Numdoctra { get; set; }

    public string? Fuente { get; set; }

    public string? HoraAtencion { get; set; }

    public bool? Reingreso { get; set; }

    public long? Ufuncional { get; set; }

    public string? Embarazo { get; set; }

    public DateOnly? FechaMuerte { get; set; }

    public string? HoraMuerte { get; set; }

    public bool? PaciSatisfecho { get; set; }

    public string? ObservacionSatis { get; set; }

    public int? IdSede { get; set; }

    public long? Estadof { get; set; }

    public bool? Radicado { get; set; }

    public DateTime? FechaRadicado { get; set; }

    public DateTime? FechaRegistroRadicado { get; set; }

    public int? TotalReversiones { get; set; }

    public int? EstudioReferencia { get; set; }

    public DateTime? FechaReversion { get; set; }

    public long? UsuarioReversion { get; set; }

    public string? MotivoAnulacion { get; set; }

    public int? EpicrisisMedEspecialista { get; set; }

    public string? CodUsuarioGuardaEpicrisis { get; set; }

    public string? NomUsuarioGuardaEpicrisis { get; set; }

    public string? Numdoctra2 { get; set; }

    public bool? MuerteMaterna { get; set; }

    public string? ResTipoIdentificacion { get; set; }

    public string? ResCedula { get; set; }

    public string? ResNombreTer { get; set; }

    public string? ResDireccion { get; set; }

    public string? ResCiudad { get; set; }

    public string? ResTelefono { get; set; }

    public string? ResNombre1 { get; set; }

    public string? ResNombre2 { get; set; }

    public string? ResApellido1 { get; set; }

    public string? ResApellido2 { get; set; }

    public string? Fntpagos { get; set; }

    public string? CertificadoDefuncion { get; set; }

    public string? ResCliente { get; set; }

    public string? FuenteRdo { get; set; }

    public string? NumdoctraRdo { get; set; }

    public bool? UsaConsecutivoFacturaZc { get; set; }

    public int? IdPyp { get; set; }

    public bool? MuerteNeumonia { get; set; }

    public string? Fntecausacion { get; set; }

    public string? Dctocausacion { get; set; }

    public string? Prefijo { get; set; }

    public string? NumeroFactura { get; set; }

    public bool Dvinterna { get; set; }

    public long? UsuarioDv { get; set; }

    public DateTime? FechaDv { get; set; }

    public bool EsEps { get; set; }

    public int? TratamientoOdonTerminado { get; set; }

    public DateTime? FechaTratamientoOdonTerminado { get; set; }

    public DateOnly? FechaReingreso { get; set; }

    public string? HoraReingreso { get; set; }

    public string? ObservacionEpicrisis { get; set; }

    public long? ContratoAdmision { get; set; }

    public string? FnteRv { get; set; }

    public string? DctoRv { get; set; }

    public bool OrdenAlta { get; set; }

    public string? DiagnoEpicrisis2 { get; set; }

    public string? DiagnoEpicrisis3 { get; set; }

    public string? DiagnoEpicrisis4 { get; set; }

    public string? DiagnoEpicrisis5 { get; set; }

    public string? DiagnoEpicrisis6 { get; set; }

    public int? PuntoAtencion { get; set; }

    public string? DiagnoEgr4 { get; set; }

    public string? DiagnoEgr5 { get; set; }

    public string? DiagnoEgr6 { get; set; }

    public bool PosibleCaida { get; set; }

    public string? ObsPosibleCaida { get; set; }

    public int? UsuarioGuardaPosibleCaida { get; set; }

    public int? ResolucionFactura { get; set; }

    public bool IsPyP { get; set; }

    public bool? GeneraFacturaPcte { get; set; }

    public string? PolizaSoat { get; set; }

    public string? ObservacionFactura { get; set; }

    public int? UsuarioAudita { get; set; }

    public bool RemisionConNotas { get; set; }

    public string? RemisionPyp { get; set; }

    public string? RemisionMorbilidad { get; set; }

    public DateOnly? FechaCorte { get; set; }

    public int BloqueoImpresion { get; set; }

    public string? CentroCosto { get; set; }

    public int? IdReferencia { get; set; }

    public string? DescripcionReferencia { get; set; }

    public int? IdReferenciaR1 { get; set; }

    public string? DescripcionReferenciaR1 { get; set; }

    public int? IdReferenciaR2 { get; set; }

    public string? DescripcionReferenciaR2 { get; set; }

    public int? IdReferenciaR3 { get; set; }

    public string? DescripcionReferenciaR3 { get; set; }

    public int? IdReferenciaR4 { get; set; }

    public string? DescripcionReferenciaR4 { get; set; }

    public int? IdReferenciaR5 { get; set; }

    public string? DescripcionReferenciaR5 { get; set; }

    public int? IdReferenciaR6 { get; set; }

    public string? DescripcionReferenciaR6 { get; set; }
}
