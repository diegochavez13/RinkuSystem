IF OBJECT_ID('ObtenerRolTrabajador', 'p') Is Not Null
	Drop Procedure ObtenerRolTrabajador
GO

Create Procedure ObtenerRolTrabajador
As
Begin
	Select iID,
		   nvDescripcion As nvRol 
	From CatRolesTrabajador
	Where btEstatus = 1
End