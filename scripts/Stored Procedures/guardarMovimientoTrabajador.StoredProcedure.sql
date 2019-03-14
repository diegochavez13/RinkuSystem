IF OBJECT_ID('guardarMovimientoTrabajador', 'p') Is Not Null
	Drop Procedure guardarMovimientoTrabajador
Go
Create Procedure guardarMovimientoTrabajador
		@iEntregas Int,
		@bCubrioTurno bit,
		@iIdTrabajador Int,
		@daFecha Date
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
		Insert Into CapturaMovimientos(iIdTrabajador,daFechaCaptura,iEntregas,btCubrioTurno,btPagoMovimiento)
		Values(@iIdTrabajador,@daFecha,@iEntregas,@bCubrioTurno,0)
	End
	ELSE
	Begin
		Insert Into CapturaMovimientos(iIdTrabajador,daFechaCaptura,iEntregas,btCubrioTurno,btPagoMovimiento)
		Values(@iIdTrabajador,@daFecha,@iEntregas,@bCubrioTurno,0)	
	End
End