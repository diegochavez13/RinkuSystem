CREATE TABLE CapturaMovimientos(
				iID Int Primary Key Identity(1,1),
				iIdTrabajador Int Not Null,
				daFechaCaptura DateTime,
				iEntregas Int,
				btCubrioTurno bit,
				iRolCubierto Int,
				btPagoMovimiento bit,
				CONSTRAINT FK_CapturaMovimientos_CatTrabajador FOREIGN KEY (iIdTrabajador)
				REFERENCES CatTrabajador(iID)
			)