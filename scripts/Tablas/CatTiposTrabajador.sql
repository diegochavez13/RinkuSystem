CREATE TABLE CatTiposTrabajador(
				iID int Primary Key Identity (1,1),
				nvDescripcion Nvarchar(50) Not Null,
				btEstatus bit Default 1
			)