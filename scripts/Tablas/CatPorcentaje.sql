CREATE TABLE CatPorcentaje(
				iID Int Primary Key Identity(1,1),
				deImpuesto Decimal(2,2),
				deImpuestoAdicional Decimal(2,2),
				deSueldoMensual Decimal(18,4),
				deValeDespensa Decimal(2,2),
				btEstatus bit
			)