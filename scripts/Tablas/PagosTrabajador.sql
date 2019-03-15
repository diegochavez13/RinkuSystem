Create Table PagosTrabajador(
		iID Int Primary Key Identity(1,1),
		idTrabajador Int,
		deSueldoBruto Decimal(18,4),
		deISR Decimal(18,4),
		deDespensa Decimal(18,4),
		dePagoNeto Decimal(18,4),
		daFechaInicioPeriodo DateTime,
		daFechaFinPeriodo DateTime,
		daFechaRegistro DateTime Default GetDate(),
		CONSTRAINT FK_PagosTrabajador_CatTrabajador FOREIGN KEY (idTrabajador)
		REFERENCES CatTrabajador(iID)
	)