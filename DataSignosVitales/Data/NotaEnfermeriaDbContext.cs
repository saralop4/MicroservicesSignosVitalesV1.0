using System;
using System.Collections.Generic;
using DataSignosVitales.Entities.NotaEnfermeriaModels;
using DataSignosVitales.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataSignosVitales.Data;

public partial class NotaEnfermeriaDbContext : DbContext, INotaEnfermeriaDbContext
{
    public NotaEnfermeriaDbContext(DbContextOptions<NotaEnfermeriaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NotasEnfermerium> NotasEnfermeria { get; set; }

    public virtual DbSet<SisCama> SisCamas { get; set; }

    public virtual DbSet<SisMae> SisMaes { get; set; }

    public virtual DbSet<SisMedi> SisMedis { get; set; }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await base.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            var mensaje = $"existe un campo que infringe las restriciones de la base de datos: {ex.Message}";
            throw new DbUpdateException(mensaje, ex);
        }
        catch (Exception ex)
        {
            var message = $"Ocurrió un error al guardar los cambios: {ex.Message}";
            throw new Exception(message, ex);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<NotasEnfermerium>(entity =>
        {
            entity.HasKey(e => e.Numero).HasName("notas_enfermeria_pk");

            entity.ToTable("notas_enfermeria");

            entity.HasIndex(e => new { e.Ingreso, e.Calificacion }, "IDX_ingreso_calificacion");

            entity.HasIndex(e => e.SoloVitales, "NotaEnfermeria");

            entity.HasIndex(e => e.NroEvolucionRelacionado, "indice1k_notas");

            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Activo)
                .IsRequired()
                .HasDefaultValueSql("('1')")
                .HasColumnName("activo");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.CodEnfer)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cod_enfer");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Dinamica).HasColumnName("dinamica");
            entity.Property(e => e.Enfermera)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("enfermera");
            entity.Property(e => e.EstadoConciencia).HasColumnName("estado_conciencia");
            entity.Property(e => e.Fc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("fc");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaDatosQx).HasColumnType("smalldatetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Fr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("fr");
            entity.Property(e => e.Glucometria).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.Hora)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora");
            entity.Property(e => e.HoraFinQx)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.HoraInicioQx)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.IdCita).HasColumnName("id_cita");
            entity.Property(e => e.Ingreso).HasColumnName("ingreso");
            entity.Property(e => e.NroEvolucionRelacionado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.O2)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("o2");
            entity.Property(e => e.Ps)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ps");
            entity.Property(e => e.PuntuacionEscala).HasColumnName("puntuacionEscala");
            entity.Property(e => e.Resumen)
                .IsUnicode(false)
                .HasColumnName("resumen");
            entity.Property(e => e.SoloVitales)
                .HasDefaultValue(0)
                .HasColumnName("solo_vitales");
            entity.Property(e => e.Ta)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ta");
            entity.Property(e => e.Tam)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tam");
            entity.Property(e => e.Tp)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("tp");
            entity.Property(e => e.Ufuncional).HasColumnName("ufuncional");
        });

        modelBuilder.Entity<SisCama>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sis_cama");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.DetalleEstado)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.EstadoPaciente).HasColumnName("estado_paciente");
            entity.Property(e => e.EstadoPacienteReserva).HasColumnName("estado_paciente_reserva");
            entity.Property(e => e.EstudioPaciente).HasColumnName("estudio_paciente");
            entity.Property(e => e.EstudioReserva).HasColumnName("estudio_reserva");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Ingreso).HasColumnName("ingreso");
            entity.Property(e => e.Manual001)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_001");
            entity.Property(e => e.Manual0010)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_0010");
            entity.Property(e => e.Manual009)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_009");
            entity.Property(e => e.Manual02)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_02");
            entity.Property(e => e.Manual0297)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_0297");
            entity.Property(e => e.Manual05)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_05");
            entity.Property(e => e.Manual100)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_100");
            entity.Property(e => e.Manual200)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_200");
            entity.Property(e => e.Manual300)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_300");
            entity.Property(e => e.Manual310)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_310");
            entity.Property(e => e.Manual400)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_400");
            entity.Property(e => e.Manual4569)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_4569");
            entity.Property(e => e.Manual594)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_594");
            entity.Property(e => e.Manual654)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_654");
            entity.Property(e => e.Manual895)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_895");
            entity.Property(e => e.Manual9001)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_9001");
            entity.Property(e => e.Manual9002)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_9002");
            entity.Property(e => e.Manual9004)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_9004");
            entity.Property(e => e.Manual9005)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("manual_9005");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pabellon).HasColumnName("pabellon");
            entity.Property(e => e.Paciente).HasColumnName("paciente");
            entity.Property(e => e.Servicio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("servicio");
            entity.Property(e => e.TipoCama).HasColumnName("tipo_cama");
        });

        modelBuilder.Entity<SisMae>(entity =>
        {
            entity.HasKey(e => e.ConEstudio);

            entity.ToTable("sis_maes", tb =>
                {
                    tb.HasTrigger("tr_sis_maesColaAseguramiento");
                    tb.HasTrigger("tr_sis_maesRoundValores");
                });

            entity.HasIndex(e => e.Remision, "<Name of Missing Index, sysname,>");

            entity.HasIndex(e => e.Estado, "EstadosEgresos");

            entity.HasIndex(e => e.FechaContabilizado, "FECHA_CONT");

            entity.HasIndex(e => e.Estado, "IDX_COSTOLABSXPCTE_SISMAES_ESTADO");

            entity.HasIndex(e => e.Estado, "IDX_CltasRealizadas_CargosFact_4");

            entity.HasIndex(e => new { e.Estado, e.NroFactura }, "IDX_ContabilizarFacts_BuscarFacts");

            entity.HasIndex(e => new { e.Estado, e.IdSede, e.Radicado, e.NroFactura }, "IDX_FactsRadicadasMovGerencial");

            entity.HasIndex(e => new { e.Estado, e.NroFactura }, "IDX_FacturasRadicadas");

            entity.HasIndex(e => new { e.NroFactura, e.Contrato, e.Prefijo }, "IDX_MovContableZC_Factura");

            entity.HasIndex(e => new { e.Prefijo, e.NumeroFactura }, "IDX_NumeroFacturaReal");

            entity.HasIndex(e => new { e.IdSede, e.FechaIng, e.Estado }, "IDX_PlanesFacturacion_AC_1");

            entity.HasIndex(e => new { e.Estado, e.IdSede, e.FechaEgr, e.NroFactura }, "IDX_ProductividadFacturadores_1");

            entity.HasIndex(e => new { e.IdSede, e.FechaEgr, e.Estado }, "IDX_ProductividadFacturadores_2");

            entity.HasIndex(e => new { e.IdSede, e.FechaEgr, e.Estado }, "IDX_ProductividadFacturadores_3");

            entity.HasIndex(e => new { e.IdSede, e.FechaEgr, e.Estado }, "IDX_ProductividadFacturadores_4");

            entity.HasIndex(e => e.StatusRegis, "IDX_RIPSNOMINAL_ANT");

            entity.HasIndex(e => new { e.StatusRegis, e.Estado, e.Contrato, e.FechaEgr, e.NroFactura }, "IDX_RipsUS");

            entity.HasIndex(e => new { e.Contrato, e.IdSede }, "IDX_VendidoxUF_1");

            entity.HasIndex(e => new { e.IdSede, e.Estado }, "IDX_sis_maesFact");

            entity.HasIndex(e => new { e.IdSede, e.Estado }, "IDX_sis_maes_autoid");

            entity.HasIndex(e => e.Contrato, "IDX_sis_maes_contrato");

            entity.HasIndex(e => new { e.Contrato, e.IdSede, e.Estado }, "IDX_sis_maes_contrato_autoid");

            entity.HasIndex(e => new { e.Contrato, e.Estado }, "IDX_sis_maes_contrato_estado");

            entity.HasIndex(e => new { e.Contrato, e.FechaEgreso }, "IDX_sis_maes_contrato_fecha");

            entity.HasIndex(e => new { e.IdSede, e.Estado }, "IDX_sis_maes_id_sede");

            entity.HasIndex(e => e.RemisionMorbilidad, "IDX_sis_maes_remision_morbilidad");

            entity.HasIndex(e => e.RemisionPyp, "IDX_sis_maes_remision_pyp");

            entity.HasIndex(e => new { e.Ufuncional, e.IdSede, e.Estado }, "IDX_sis_maes_ufuncional");

            entity.HasIndex(e => e.EstadoRes, "IDX_sismaes_estadores");

            entity.HasIndex(e => e.TipoEstudio, "INDEX_TIPO_ESTUDIO");

            entity.HasIndex(e => new { e.Estado, e.Contabilizado, e.FechaIng, e.NroFactura }, "ZEUS®FACTUID");

            entity.HasIndex(e => new { e.Estado, e.FechaIng, e.NroFactura }, "ZEUS®IDFACT");

            entity.HasIndex(e => e.Contrato, "Zeus®indice030_5");

            entity.HasIndex(e => new { e.TipoEstudio, e.EstadoEgr }, "dbo.Zeus®indice030");

            entity.HasIndex(e => e.Estado, "estado_index");

            entity.HasIndex(e => e.FechaIng, "idx_FechaIngreso");

            entity.HasIndex(e => new { e.StatusRegis, e.NroFactura, e.Remision }, "idx_View_Rmaes");

            entity.HasIndex(e => new { e.DiagnoIng, e.DiagnoEgr, e.DiagnoEgr1, e.DiagnoEgr2, e.DiagnoEgr3 }, "idx_diags");

            entity.HasIndex(e => e.Estado, "idx_vista_zeusnotas");

            entity.HasIndex(e => e.CodEntidad, "sis_maes_cod_entidad");

            entity.HasIndex(e => e.Contrato, "sis_maes_contrato");

            entity.HasIndex(e => e.Estadof, "sis_maes_estadof");

            entity.HasIndex(e => new { e.IdSede, e.Estado }, "sis_maes_facturacion");

            entity.HasIndex(e => e.FechaEgr, "sis_maes_fechaegr");

            entity.HasIndex(e => e.FechaIng, "sis_maes_fechaing");

            entity.HasIndex(e => e.Autoid, "sis_maes_idx");

            entity.HasIndex(e => e.NroFactura, "sis_maes_nro_factura");

            entity.Property(e => e.ConEstudio).HasColumnName("con_estudio");
            entity.Property(e => e.Autoid).HasColumnName("autoid");
            entity.Property(e => e.BloqueoImpresion).HasColumnName("bloqueo_impresion");
            entity.Property(e => e.Cama2)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cama2");
            entity.Property(e => e.CausaExt)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("causa_ext");
            entity.Property(e => e.CausaMte)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("causa_mte");
            entity.Property(e => e.CentroCosto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("centro_costo");
            entity.Property(e => e.CertificadoDefuncion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("certificado_defuncion");
            entity.Property(e => e.CodClasi)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("cod_clasi");
            entity.Property(e => e.CodEgreso)
                .IsUnicode(false)
                .HasColumnName("cod_egreso");
            entity.Property(e => e.CodEntidad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cod_entidad");
            entity.Property(e => e.CodEspeci)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cod_especi");
            entity.Property(e => e.CodMedico)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cod_medico");
            entity.Property(e => e.CodUsuario)
                .IsUnicode(false)
                .HasColumnName("cod_usuario");
            entity.Property(e => e.CodUsuarioGuardaEpicrisis)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cod_usuario_guarda_epicrisis");
            entity.Property(e => e.CodigoServicio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo_servicio");
            entity.Property(e => e.ComplicaQx).HasColumnName("complica_qx");
            entity.Property(e => e.ConsecSoat)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("consec_soat");
            entity.Property(e => e.Contabilizado)
                .HasDefaultValue((short)0)
                .HasColumnName("contabilizado");
            entity.Property(e => e.Contrato).HasColumnName("contrato");
            entity.Property(e => e.Cuenta)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cuenta");
            entity.Property(e => e.DctoRv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DCTO_RV");
            entity.Property(e => e.Dctocausacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DCTOCAUSACION");
            entity.Property(e => e.DescripcionReferencia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferencia");
            entity.Property(e => e.DescripcionReferenciaR1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferenciaR1");
            entity.Property(e => e.DescripcionReferenciaR2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferenciaR2");
            entity.Property(e => e.DescripcionReferenciaR3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferenciaR3");
            entity.Property(e => e.DescripcionReferenciaR4)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferenciaR4");
            entity.Property(e => e.DescripcionReferenciaR5)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferenciaR5");
            entity.Property(e => e.DescripcionReferenciaR6)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcionReferenciaR6");
            entity.Property(e => e.DestinoUsu).HasColumnName("destino_usu");
            entity.Property(e => e.DiagnoCom)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_com");
            entity.Property(e => e.DiagnoEgr)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr");
            entity.Property(e => e.DiagnoEgr1)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr1");
            entity.Property(e => e.DiagnoEgr2)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr2");
            entity.Property(e => e.DiagnoEgr3)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr3");
            entity.Property(e => e.DiagnoEgr4)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr4");
            entity.Property(e => e.DiagnoEgr5)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr5");
            entity.Property(e => e.DiagnoEgr6)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_egr6");
            entity.Property(e => e.DiagnoEpicrisis2)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_epicrisis2");
            entity.Property(e => e.DiagnoEpicrisis3)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_epicrisis3");
            entity.Property(e => e.DiagnoEpicrisis4)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_epicrisis4");
            entity.Property(e => e.DiagnoEpicrisis5)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_epicrisis5");
            entity.Property(e => e.DiagnoEpicrisis6)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_epicrisis6");
            entity.Property(e => e.DiagnoIng)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("diagno_ing");
            entity.Property(e => e.Dvinterna).HasColumnName("DVInterna");
            entity.Property(e => e.Embarazo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("embarazo");
            entity.Property(e => e.EmpAsumeDesc)
                .HasDefaultValue(0)
                .HasColumnName("emp_asume_desc");
            entity.Property(e => e.EpicrisisMedEspecialista).HasColumnName("epicrisis_med_especialista");
            entity.Property(e => e.EsEps).HasColumnName("es_eps");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.EstadoEgr).HasColumnName("estado_egr");
            entity.Property(e => e.EstadoRes).HasColumnName("estado_res");
            entity.Property(e => e.Estadof)
                .HasDefaultValue(1L)
                .HasColumnName("estadof");
            entity.Property(e => e.FechaAcc)
                .HasColumnType("datetime")
                .HasColumnName("fecha_acc");
            entity.Property(e => e.FechaAlta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_alta");
            entity.Property(e => e.FechaContabilizado)
                .HasColumnType("datetime")
                .HasColumnName("fecha_contabilizado");
            entity.Property(e => e.FechaCorte).HasColumnName("fecha_corte");
            entity.Property(e => e.FechaDv)
                .HasColumnType("datetime")
                .HasColumnName("FechaDV");
            entity.Property(e => e.FechaEgr)
                .HasColumnType("datetime")
                .HasColumnName("fecha_egr");
            entity.Property(e => e.FechaEgreso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_egreso");
            entity.Property(e => e.FechaEstadoRes)
                .HasColumnType("datetime")
                .HasColumnName("fecha_estado_res");
            entity.Property(e => e.FechaIng)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ing");
            entity.Property(e => e.FechaLoteZeus)
                .HasColumnType("datetime")
                .HasColumnName("fecha_lote_zeus");
            entity.Property(e => e.FechaMuerte).HasColumnName("fecha_muerte");
            entity.Property(e => e.FechaParto)
                .HasColumnType("datetime")
                .HasColumnName("fecha_parto");
            entity.Property(e => e.FechaRadicado)
                .HasColumnType("datetime")
                .HasColumnName("fecha_radicado");
            entity.Property(e => e.FechaRegistroRadicado)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro_radicado");
            entity.Property(e => e.FechaReingreso).HasColumnName("fechaReingreso");
            entity.Property(e => e.FechaReversion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_reversion");
            entity.Property(e => e.FechaTratamientoOdonTerminado)
                .HasColumnType("datetime")
                .HasColumnName("fechaTratamientoOdonTerminado");
            entity.Property(e => e.FechaUsuario)
                .HasColumnType("datetime")
                .HasColumnName("fecha_usuario");
            entity.Property(e => e.FnteRv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FNTE_RV");
            entity.Property(e => e.Fntecausacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FNTECAUSACION");
            entity.Property(e => e.Fntpagos)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FNTPAGOS");
            entity.Property(e => e.Fuente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FUENTE");
            entity.Property(e => e.FuenteRdo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FUENTE_RDO");
            entity.Property(e => e.GeneraFacturaPcte).HasDefaultValue(false);
            entity.Property(e => e.HoraAtencion)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("hora_atencion");
            entity.Property(e => e.HoraCrea)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora_crea");
            entity.Property(e => e.HoraEgr)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("hora_egr");
            entity.Property(e => e.HoraIng)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("hora_ing");
            entity.Property(e => e.HoraMuerte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hora_muerte");
            entity.Property(e => e.HoraReingreso)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("horaReingreso");
            entity.Property(e => e.IdPyp).HasColumnName("id_pyp");
            entity.Property(e => e.IdReferencia).HasColumnName("idReferencia");
            entity.Property(e => e.IdReferenciaR1).HasColumnName("idReferenciaR1");
            entity.Property(e => e.IdReferenciaR2).HasColumnName("idReferenciaR2");
            entity.Property(e => e.IdReferenciaR3).HasColumnName("idReferenciaR3");
            entity.Property(e => e.IdReferenciaR4).HasColumnName("idReferenciaR4");
            entity.Property(e => e.IdReferenciaR5).HasColumnName("idReferenciaR5");
            entity.Property(e => e.IdReferenciaR6).HasColumnName("idReferenciaR6");
            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.LoteZeus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lote_zeus");
            entity.Property(e => e.Medicamentos)
                .HasDefaultValue(0)
                .HasColumnName("medicamentos");
            entity.Property(e => e.MedicoEgr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("medico_egr");
            entity.Property(e => e.MotivoAnulacion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.MotivoEgr).HasColumnName("motivo_egr");
            entity.Property(e => e.MuerteMaterna).HasColumnName("muerte_materna");
            entity.Property(e => e.MuerteNeumonia).HasColumnName("muerte_neumonia");
            entity.Property(e => e.NitAsegura)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("nit_asegura");
            entity.Property(e => e.NoPoliza)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_poliza");
            entity.Property(e => e.NomEgreso)
                .IsUnicode(false)
                .HasColumnName("nom_egreso");
            entity.Property(e => e.NomUsuario)
                .IsUnicode(false)
                .HasColumnName("nom_usuario");
            entity.Property(e => e.NomUsuarioGuardaEpicrisis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nom_usuario_guarda_epicrisis");
            entity.Property(e => e.NroAutoriza)
                .IsUnicode(false)
                .HasColumnName("nro_autoriza");
            entity.Property(e => e.NroCama)
                .HasDefaultValue(-1L)
                .HasColumnName("nro_cama");
            entity.Property(e => e.NroFactura)
                .HasDefaultValue(0)
                .HasColumnName("nro_factura");
            entity.Property(e => e.Numdoctra)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUMDOCTRA");
            entity.Property(e => e.Numdoctra2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUMDOCTRA_2");
            entity.Property(e => e.NumdoctraRdo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NUMDOCTRA_RDO");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Obs)
                .IsUnicode(false)
                .HasColumnName("obs");
            entity.Property(e => e.ObsPosibleCaida).IsUnicode(false);
            entity.Property(e => e.ObservacionEpicrisis)
                .IsUnicode(false)
                .HasColumnName("observacionEpicrisis");
            entity.Property(e => e.ObservacionFactura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("observacion_Factura");
            entity.Property(e => e.ObservacionSatis)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("observacion_satis");
            entity.Property(e => e.OrdenAlta).HasColumnName("orden_alta");
            entity.Property(e => e.PaciSatisfecho).HasColumnName("paci_satisfecho");
            entity.Property(e => e.PolizaSoat)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PolizaSOAT");
            entity.Property(e => e.Prefijo)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Radicado)
                .HasDefaultValue(false)
                .HasColumnName("radicado");
            entity.Property(e => e.Reingreso).HasColumnName("reingreso");
            entity.Property(e => e.Remision)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("remision");
            entity.Property(e => e.RemisionConNotas).HasColumnName("remisionConNotas");
            entity.Property(e => e.RemisionMorbilidad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("remision_morbilidad");
            entity.Property(e => e.RemisionPyp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("remision_pyp");
            entity.Property(e => e.ResApellido1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_apellido1");
            entity.Property(e => e.ResApellido2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_apellido2");
            entity.Property(e => e.ResCedula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_cedula");
            entity.Property(e => e.ResCiudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_ciudad");
            entity.Property(e => e.ResCliente)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("res_cliente");
            entity.Property(e => e.ResDireccion)
                .IsUnicode(false)
                .HasColumnName("res_direccion");
            entity.Property(e => e.ResNombre1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_nombre1");
            entity.Property(e => e.ResNombre2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_nombre2");
            entity.Property(e => e.ResNombreTer)
                .IsUnicode(false)
                .HasColumnName("res_nombre_ter");
            entity.Property(e => e.ResTelefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("res_telefono");
            entity.Property(e => e.ResTipoIdentificacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("res_tipo_identificacion");
            entity.Property(e => e.RsAsegura)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("rs_asegura");
            entity.Property(e => e.Satisfaccion).HasColumnName("satisfaccion");
            entity.Property(e => e.StatusRegis).HasColumnName("status_regis");
            entity.Property(e => e.Terapia).HasColumnName("terapia");
            entity.Property(e => e.TipoEstudio)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_estudio");
            entity.Property(e => e.TipoMuerte).HasColumnName("tipo_muerte");
            entity.Property(e => e.TotalReversiones).HasDefaultValue(0);
            entity.Property(e => e.TratamientoOdonTerminado)
                .HasDefaultValue(0)
                .HasColumnName("tratamientoOdonTerminado");
            entity.Property(e => e.Ufuncional).HasColumnName("ufuncional");
            entity.Property(e => e.UsaConsecutivoFacturaZc)
                .HasDefaultValue(false)
                .HasColumnName("UsaConsecutivoFactura_Zc");
            entity.Property(e => e.UsuarioAudita).HasColumnName("usuario_audita");
            entity.Property(e => e.UsuarioDv).HasColumnName("UsuarioDV");
            entity.Property(e => e.UsuarioEstadoRes)
                .IsUnicode(false)
                .HasColumnName("usuario_estado_res");
            entity.Property(e => e.UsuarioReversion).HasColumnName("usuario_reversion");
            entity.Property(e => e.ValorLetra)
                .HasColumnType("text")
                .HasColumnName("valor_letra");
            entity.Property(e => e.ViaIngreso).HasColumnName("via_ingreso");
            entity.Property(e => e.VlrAbono).HasColumnName("vlr_abono");
            entity.Property(e => e.VlrCoopago)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("vlr_coopago");
            entity.Property(e => e.VlrDescto)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("vlr_descto");
            entity.Property(e => e.VlrExcedente).HasColumnName("vlr_excedente");
            entity.Property(e => e.VlrFactura)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("vlr_factura");
            entity.Property(e => e.VlrNeto)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("vlr_neto");
            entity.Property(e => e.VlrPagado).HasColumnName("vlr_pagado");
        });

        modelBuilder.Entity<SisMedi>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("sis_medi");

            entity.HasIndex(e => e.Cedula, "IDX_cedula");

            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.AbreHistoria).HasColumnName("abre_historia");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.CierraHistoria)
                .HasDefaultValue(0)
                .HasColumnName("cierra_historia");
            entity.Property(e => e.CitaExterna).HasColumnName("citaExterna");
            entity.Property(e => e.CodHistoriaPredeterminada)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodManualPago)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("cod_manual_pago");
            entity.Property(e => e.Direccion)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.EditarAuditor)
                .HasDefaultValue(false)
                .HasColumnName("editar_auditor");
            entity.Property(e => e.EditarBorrarEvos).HasColumnName("editar_borrar_evos");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EsAnes).HasColumnName("es_anes");
            entity.Property(e => e.EsAuditor).HasColumnName("es_auditor");
            entity.Property(e => e.EsAyu).HasColumnName("es_ayu");
            entity.Property(e => e.EsEmpresa).HasColumnName("es_empresa");
            entity.Property(e => e.EsEspecialista).HasColumnName("es_especialista");
            entity.Property(e => e.EsMedico).HasColumnName("es_medico");
            entity.Property(e => e.EsMedicoFamiliar).HasDefaultValue(false);
            entity.Property(e => e.EsOdontologo).HasDefaultValue(false);
            entity.Property(e => e.EsPediatra).HasColumnName("es_pediatra");
            entity.Property(e => e.EsPrioritario).HasDefaultValue(false);
            entity.Property(e => e.EsPyp).HasDefaultValue(false);
            entity.Property(e => e.EsUsuario).HasColumnName("es_usuario");
            entity.Property(e => e.EsVacunador).HasColumnName("es_vacunador");
            entity.Property(e => e.Especialidad).HasColumnName("especialidad");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.ExigeProcEvo)
                .HasDefaultValue(true)
                .HasColumnName("exige_proc_evo");
            entity.Property(e => e.Firma)
                .HasMaxLength(300)
                .HasColumnName("firma");
            entity.Property(e => e.LeyendaConfirmarMedico).HasColumnName("leyendaConfirmarMedico");
            entity.Property(e => e.MaxDiagnostico)
                .HasDefaultValue(0)
                .HasColumnName("max_diagnostico");
            entity.Property(e => e.MaxFormulas)
                .HasDefaultValue(0)
                .HasColumnName("max_formulas");
            entity.Property(e => e.MaxFormulasPosfechadas).HasDefaultValue((short)1);
            entity.Property(e => e.MaxImagenologia)
                .HasDefaultValue(0)
                .HasColumnName("max_imagenologia");
            entity.Property(e => e.MaxLaboratorios)
                .HasDefaultValue(0)
                .HasColumnName("max_laboratorios");
            entity.Property(e => e.MaxNoQx)
                .HasDefaultValue(0)
                .HasColumnName("max_no_qx");
            entity.Property(e => e.MaxQx)
                .HasDefaultValue(0)
                .HasColumnName("max_qx");
            entity.Property(e => e.ModificaTriage).HasColumnName("modifica_triage");
            entity.Property(e => e.MontoPago)
                .HasColumnType("money")
                .HasColumnName("monto_pago");
            entity.Property(e => e.NivelMctos).HasDefaultValue(0);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pacientes).HasColumnName("pacientes");
            entity.Property(e => e.PagoParcial).HasColumnName("pago_parcial");
            entity.Property(e => e.PagoProd)
                .HasDefaultValue((short)0)
                .HasColumnName("pago_prod");
            entity.Property(e => e.Paliativo)
                .HasDefaultValue(false)
                .HasColumnName("paliativo");
            entity.Property(e => e.PrimerApellido).IsUnicode(false);
            entity.Property(e => e.PrimerNombre).IsUnicode(false);
            entity.Property(e => e.ReclasificaRiesgo).HasColumnName("reclasifica_riesgo");
            entity.Property(e => e.Registro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("registro");
            entity.Property(e => e.RequiereAuditoria).HasDefaultValue(false);
            entity.Property(e => e.SegundoApellido).IsUnicode(false);
            entity.Property(e => e.SegundoNombre).IsUnicode(false);
            entity.Property(e => e.Servicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("servicio");
            entity.Property(e => e.Telefono)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.Tercero)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Tiempo).HasColumnName("tiempo");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.TipoIngreso)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_ingreso");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo_pago");
            entity.Property(e => e.Valoresperado).HasColumnName("valoresperado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
