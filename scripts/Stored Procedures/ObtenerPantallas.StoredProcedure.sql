If OBJECT_ID('ObtenerPantallas', 'p') IS NOT NULL
	Drop Procedure ObtenerPantallas
GO
Create Procedure ObtenerPantallas
AS
Begin

	Select 
		CM.iID AS iIdModulo,
		CM.nvDescripcion AS nvNombreModulo,
		CF.iID AS iIdFuncionalidad,
		CF.nvDescripcion AS nvNombreFuncionalidad,
		CF.nvAccion AS nvAccion
	From CatFuncionalidades CF
	Inner Join CatModulos CM 
		On CM.iID = CF.iIdModulo And CM.btEstatus = 1
	Where CF.btEstatus = 1
	
End