IF OBJECT_ID('guardarTrabajador', 'p') Is Not Null
	Drop Procedure guardarTrabajador
GO
CREATE PROCEDURE guardarTrabajador
	@Xml NVARCHAR(max)
AS 
BEGIN
DECLARE @nIdXml INT

DECLARE @iID Int, 
		@nvNombre nvarchar(150), 
		@iRol Int, 
		@iTipo Int, 
		@iEstatus Int

	IF ISNULL(@Xml,'') <> ''
	BEGIN 
		exec dbo.sp_xml_preparedocument @nIdXml OUTPUT, @Xml

		SELECT  @iID = iID,@nvNombre = nvNombre, @iRol = iRol,@iTipo = iTipo,@iEstatus = iEstatus
		FROM OPENXML (@nIdXml, '/Datos/Trabajador', 3)
		WITH (iID int,nvNombre nvarchar(150),iRol int,iTipo int,iEstatus int)

		EXEC dbo.sp_xml_removedocument @nIdXml
	END
	BEGIN
		IF Exists (Select * From CatTrabajador Where iID = @iID)
		BEGIN
			UPDATE CatTrabajador
			SET nvNombre = @nvNombre, 
				iRolTrabajador = @iRol, 
				iTipoTrabajador = @iTipo,
				btEstatus = @iEstatus
			WHERE iID = @iID
		END
		ELSE
		BEGIN
			INSERT INTO CatTrabajador(nvNombre,iRolTrabajador,iTipoTrabajador,btEstatus)
			VALUES(@nvNombre,@iRol,@iTipo,@iEstatus)
		END
	END
END