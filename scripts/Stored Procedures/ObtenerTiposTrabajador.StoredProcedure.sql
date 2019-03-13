IF OBJECT_ID('ObtenerTiposTrabajador', 'p') Is Not Null
	Drop Procedure ObtenerTiposTrabajador
GO

Create Procedure ObtenerTiposTrabajador
As
Begin
	Select iID,
		   nvDescripcion As nvTipo
	From CatTiposTrabajador
	Where btEstatus = 1
End