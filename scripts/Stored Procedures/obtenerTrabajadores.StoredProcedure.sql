IF OBJECT_ID('obtenerTrabajadores', 'p')Is Not Null
	Drop Procedure obtenerTrabajadores
GO
Create Procedure obtenerTrabajadores
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
End