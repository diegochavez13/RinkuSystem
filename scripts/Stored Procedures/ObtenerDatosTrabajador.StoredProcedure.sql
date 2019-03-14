IF OBJECT_ID('ObtenerDatosTrabajador', 'p')Is Not Null
	Drop Procedure ObtenerDatosTrabajador
Go 
Create Procedure ObtenerDatosTrabajador
	@idTrabajador Int
AS
Begin
	Select	CT.iID,
			CT.nvNombre,
			CRT.iID AS iRol,
			CRT.nvDescripcion AS nvRol,
			CTT.iID AS iTipo,
			CTT.nvDescripcion AS nvTipo,
			CT.btEstatus As iEstatus,
			nvEstatus = CASE WHEN CT.btEstatus = 1 THEN 'Activo' ELSE 'Inactivo' END
	From CatTrabajador CT
	Inner Join CatRolesTrabajador CRT
	On CRT.iID = CT.iRolTrabajador
	Inner Join CatTiposTrabajador CTT
	On CTT.iID = CT.iTipoTrabajador
	Where CT.iID = @idTrabajador
	And Ct.btEstatus = 1
End