
CREATE TABLE [dbo].[notas_enfermeria]([estado_conciencia] [bit] NULL);

Update [notas_enfermeria] set [estado_conciencia] = 0;

alter table notas_enfermeria add puntuacionEscala int null, color varchar(20) null;