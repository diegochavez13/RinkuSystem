IF OBJECT_ID('guardarMovimientoTrabajador', 'p') Is Not Null
	Drop Procedure guardarMovimientoTrabajador
Go
Create Procedure guardarMovimientoTrabajador
		@iEntregas Int,
		@bCubrioTurno bit,
		@iIdTrabajador Int,
		@daFecha Date,
		@iRolCubierto Int
As
Begin
	IF Exists (Select * From CapturaMovimientos Where iIdTrabajador = @iIdTrabajador 
				And Cast(daFechaCaptura as Date) = Cast(@daFecha as Date)
				And btPagoMovimiento = 0)
	Begin
		Update CapturaMovimientos
		SET iEntregas = iEntregas + @iEntregas, btCubrioTurno = @bCubrioTurno
		Where iIdTrabajador = 1 
		And Cast(daFechaCaptura as Date) = Cast(@daFecha as Date)
		And btPagoMovimiento = 0
	End
	ELSE IF (Select Count(1) From CapturaMovimientos Where iIdTrabajador = @iIdTrabajador 
				And Cast(daFechaCaptura as Date) = Cast(@daFecha as Date)
				And btPagoMovimiento = 1) > 0
	Begin
		IF Cast(GETDATE() as Date) = CAST(@daFecha As Date)
			Insert Into CapturaMovimientos(iIdTrabajador,daFechaCaptura,iEntregas,btCubrioTurno,iRolCubierto,btPagoMovimiento)
			Values(@iIdTrabajador,@daFecha,@iEntregas,@bCubrioTurno,@iRolCubierto,0)
		ELSE
		Begin
			RAISERROR ('No se puede capturar un movimiento de un día ya pagado.', -- Message text.
					   16, -- Severity.
						1 -- State.
						);
        End
	End
	ELSE
	Begin
		Insert Into CapturaMovimientos(iIdTrabajador,daFechaCaptura,iEntregas,btCubrioTurno,iRolCubierto,btPagoMovimiento)
		Values(@iIdTrabajador,@daFecha,@iEntregas,@bCubrioTurno,@iRolCubierto,0)	
	End
End