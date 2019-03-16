CREATE TABLE CatFuncionalidades(
	iID int Primary Key identity(1,1),
	iIdModulo Int,
	nvDescripcion Nvarchar(50),
	nvAccion Nvarchar(100),
	btEstatus bit,
	CONSTRAINT FK_CatFuncionalidades_CatModulos FOREIGN KEY (iIdModulo)
	REFERENCES CatModulos(iID)
)