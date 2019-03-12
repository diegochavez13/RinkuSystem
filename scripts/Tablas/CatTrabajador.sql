CREATE TABLE CatTrabajador(
				iID int Primary Key Identity (1,1),
				nvNombre Nvarchar(50) Not Null,
				nvApellidoPaterno Nvarchar(50) Not Null,
				nvApellidoMaterno Nvarchar(50),
				iRolTrabajador Int Not Null,
				iTipoTrabajador Int Not Null,
				btEstatus bit Default 1,
				CONSTRAINT FK_CatTrabajador_CatRolesTrabajador FOREIGN KEY (iRolTrabajador)
				REFERENCES CatRolesTrabajador(iID),
				CONSTRAINT FK_CatTrabajador_CatTiposTrabajador FOREIGN KEY (iTipoTrabajador)
				REFERENCES CatTiposTrabajador(iID)
			)