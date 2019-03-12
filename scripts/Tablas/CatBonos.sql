CREATE TABLE CatBonos(
				iID Int Primary Key Identity(1,1),
				deBonoPorHora Decimal(8,4),
				iRolTrabajador Int,
				btEstatus bit,
				CONSTRAINT FK_CatBonos_CatRolesTrabajador FOREIGN KEY (iRolTrabajador)
				REFERENCES CatRolesTrabajador(iID)
			)