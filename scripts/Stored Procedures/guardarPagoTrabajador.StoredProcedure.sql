IF OBJECT_ID('guardarPagoTrabajador', 'p') Is Not Null
	Drop Procedure guardarPagoTrabajador
Go
Create Procedure guardarPagoTrabajador
		@iIdTrabajador Int, 
		@daFechaInicio Date,
		@daFechaFin Date, 
		@deDespensa Decimal(18,2), 
		@deISR Decimal(18,2),
		@dePagoNeto Decimal(18,2),
		@deSueldoBruto Decimal(18,2)
AS
BEGIN
		INSERT INTO PagosTrabajador(idTrabajador,deSueldoBruto,deISR,deDespensa,dePagoNeto,daFechaInicioPeriodo,daFechaFinPeriodo)
		VALUES(@iIdTrabajador,@deSueldoBruto,@deISR,@deDespensa,@dePagoNeto,Cast(@daFechaInicio As Date), Cast(@daFechaFin As Date))
		
		Update CM
		SET CM.btPagoMovimiento = 1
		From CapturaMovimientos CM
		Where iIdTrabajador = @iIdTrabajador
		And Cast(daFechaCaptura As Date) >= Cast(@daFechaInicio As Date)
		And Cast(daFechaCaptura As Date) <= Cast(@daFechaFin As Date)
END