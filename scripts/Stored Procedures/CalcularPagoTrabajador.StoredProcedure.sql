IF OBJECT_ID('CalcularPagoTrabajador', 'p')Is Not Null
	Drop Procedure CalcularPagoTrabajador
Go
Create Procedure CalcularPagoTrabajador
	@iIdTrabajador Int,
	@daFechaInicio DateTime,
	@daFechaFin DateTime
As
Begin
	Declare @ImpuestoISR Decimal(2,2),
			@ImpuestoAdicional Decimal(2,2),
			@deSueldoBaseMensual Decimal(18,4),
			@deValeDespensa Decimal(2,2);
			
	If Object_Id('tempdb..#tmpPagosTrabajador') Is Not Null
			Drop Table #tmpPagosTrabajador

	If Object_Id('tempdb..#tmpTotalPago') Is Not Null
			Drop Table #tmpTotalPago


	Select @ImpuestoISR = deImpuesto,
		   @ImpuestoAdicional = deImpuestoAdicional,
		   @deSueldoBaseMensual = deSueldoMensual,
		   @deValeDespensa = deValeDespensa
	From CatPorcentaje
	Where btEstatus = 1


	Select 
	IdTrabajador = CT.iID,
	idTipoTrabajador = CTT.iID,
	PagoDia = (COUNT(daFechaCaptura) * 8) * 30,
	PagaEntrega = Sum(CM.iEntregas) * 5,
	Bono = (Case When CR.iID = 1 Then 10 Else
				Case When CR.iID = 2 Then 5 Else 
						Case When CR.iID = 3 And CM.btCubrioTurno = 1 Then 
							Case When CM.iRolCubierto = 1 Then 10 Else 
								Case When CM.iRolCubierto = 2 Then 5 Else 0 End 
							End 
						Else 0 
					End
				End 
			End) * (COUNT(daFechaCaptura) * 8)
	Into #tmpPagosTrabajador
	From CapturaMovimientos CM
	Inner Join CatTrabajador CT
		On CT.iID = CM.iIdTrabajador
	Inner Join CatRolesTrabajador CR
		On CR.iID = CT.iRolTrabajador
	Inner Join CatTiposTrabajador CTT
		On CTT.iID = CT.iTipoTrabajador
	Where iIdTrabajador = @iIdTrabajador
	And Cast(CM.daFechaCaptura AS Date) >= CAST(@daFechaInicio As Date)
	And Cast(CM.daFechaCaptura AS Date) <= CAST(@daFechaFin As Date)
	And CM.btPagoMovimiento = 0
	Group By CT.iID, CTT.iID,CR.iID, CM.btCubrioTurno, CM.iRolCubierto

	Select 
		SueldoBruto = PagoDia + PagaEntrega + Bono,
		ISR = Case WHen (PagoDia + PagaEntrega + Bono) > @deSueldoBaseMensual 
					Then (PagoDia + PagaEntrega + Bono) * (@ImpuestoISR + @ImpuestoAdicional)
					Else (PagoDia + PagaEntrega + Bono) * @ImpuestoISR
			  End,
		Despensa = (PagoDia + PagaEntrega + Bono) * Case when idTipoTrabajador = 1 Then @deValeDespensa Else 0 End
	Into #tmpTotalPago
	From #tmpPagosTrabajador

	Select  SueldoBruto = Cast(SueldoBruto as Decimal(18,2)),
			ISR,
			Despensa,
			PagoNeto = (SueldoBruto - ISR) + Despensa
	From #tmpTotalPago
End