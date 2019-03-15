var objPagos = {};
$(document).ready(function () {
    iniPantalla();
});


function iniPantalla() {
    Numerics("num", false, false);

    $("#txtNumeroEmpleado").focus();

    $("#txtFechaInicio").datepicker({
        format: 'dd-mm-yyyy',
        language: "es",
        endDate: '0d',
        autoclose: true
    });

    $("#txtFechaFin").datepicker({
        format: 'dd-mm-yyyy',
        language: "es",
        endDate: '0d',
        autoclose: true
    });

    $("#btnFechaInicio").click(function () {
        $("#txtFechaInicio").focus();
    });

    $("#btnFechaFin").click(function () {
        $("#txtFechaFin").focus();
    });

    $("#btnBuscarEmpleado").click(function () {
        mostrarGridTrabajadores(objTrabajadores);
    });

    $("#btnLimpiar").click(function () {
        limpiarCampos();
    });

    $("#txtNumeroEmpleado").keyup(function (e) {
        if (e.keyCode == 13)
            obtenerTrabajador();
    });

    $("#btnVistaPrevia").click(function () {
        if (validarCampos()) {
            generarVistaPrevia();
        }
    });

    initGridTrabajadores();
    obtenerTrabajadores();
}

function obtenerTrabajador() {
    var objDatos = {
        idTrabajador: $("#txtNumeroEmpleado").val()
    }
    connection.invoke("Trabajador", "obtenerTrabajador", objDatos, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                var objTrabajador = Respuesta.data;
                $("#txtNumeroEmpleado").val(objTrabajador[0].iID);
                $("#txtNombre").val(objTrabajador[0].nvNombre);
                $("#txtRol").val(objTrabajador[0].nvRol);
                $("#txtTipo").val(objTrabajador[0].nvTipo);
                break;
            case NO_FOUND_RECORDS_:
                toast("Notificación", "No se encontraron datos con ese Número de empleado", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function obtenerTrabajadores() {
    connection.invoke("Trabajador", "ObtenerDatosTrabajadores", {}, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                objTrabajadores = Respuesta.data;
                break;
            case NO_FOUND_RECORDS_:
                toast("Notificación", "No se encontraron trabajadores", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function generarVistaPrevia()
{
    var objDatos = {
        iIdTrabajador: $("#txtNumeroEmpleado").val(),
        daFechaInicio:  $("#txtFechaInicio").val(),
        daFechaFin: $("#txtFechaFin").val()
    }

    connection.invoke("PagosNomina", "generarVistaPrevia", objDatos, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                objPagos = Respuesta.data;
                getVistaPrevia();
                break;
            case NO_FOUND_RECORDS_:
                toast("Notificación", "No se encontraron pagos pendientes", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function validarCampos() {
    var bBandera = false;
    var daInicio = new Date($("#txtFechaInicio").val());
    var daFin = new Date($("#txtFechaFin").val());

    if ($("#txtNumeroEmpleado").val() == "") {
        toast("Notificación", "Favor de consultar un trabajador para continuar con su pago de su nómina ", "orange", 3000, LIGHT, RIGHT);
        $("#txtNombre").focus();
        return;
    }

    if ($("#txtFechaInicio").val() == "") {
        toast("Notificación", "Favor de capturar el campo <b>Fecha Inicio</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#txtNombre").focus();
        return;
    }

    if ($("#txtFechaFin").val() == "") {
        toast("Notificación", "Favor de capturar el campo <b>Fecha Fin</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#txtNombre").focus();
        return;
    }

    if (daInicio > daFin) {
        toast("Notificación", "La fecha inicio es mayor a la fecha fin, favor de corregir", "orange", 3000, LIGHT, RIGHT);
        $("#txtNombre").focus();
        return;
    }

    return bBandera = true;
}

function initGridTrabajadores() {
    columns = ["Num. Empleado", "Nombre", "", "Rol", "", "Tipo", "", "Estatus"];
    colModel = [{ name: "iID", index: "iID", width: 150 },
                { name: "nvNombre", index: "nvNombre", width: 250 },
                { name: "iRol", index: "iRol", hidden: true },
                { name: "nvRol", index: "nvRol", width: 150 },
                { name: "iTipo", index: "iTipo", hidden: true },
                { name: "nvTipo", index: "nvTipo", width: 150 },
                { name: "iEstatus", index: "iEstatus", hidden: true },
                { name: "nvEstatus", index: "nvEstatus", width: 100 }
    ];

    jQuery("#grid-Trabajadores").jqGrid({
        datatype: "local",
        height: 'auto',
        shrinkToFit: true,
        colNames: columns,
        colModel: colModel,
        viewrecords: true,
        rowNum: 10,
        pager: "#pager-Trabajadores",
        altRows: true,
        multiselect: false,
        multiboxonly: true,
        onSelectRow: function (rowid) {

        },
        ondblClickRow: function (rowid) {
            row = $(this).getRowData(rowid);

            $("#txtNumeroEmpleado").val(row.iID);
            $("#txtNombre").val(row.nvNombre);
            $("#txtRol").val(row.nvRol);
            $("#txtTipo").val(row.nvTipo);

            if (row.iRol == 3)
                $("#chkCubrioTurno").prop("disabled", false);
            else
                $("#chkCubrioTurno").prop("disabled", true);

            $(this).closest('.ui-dialog-content').dialog('close');
        },
    });

    updatePagerIcons();
}

function mostrarGridTrabajadores(data) {
    $("#grid-Trabajadores").jqGrid('clearGridData');
    $("#grid-Trabajadores").jqGrid('addRowData', 1, data);
    dialogRegular2("div-grid-Trabajadores", "Trabajadores", 'auto', 'auto');
}

function getVistaPrevia() {
    
    $("#lblNombreTrabajador").html($("#txtNombre").val());
    $("#lblValSueldoBruto").html("$ " + objPagos[0].deSueldoBruto);
    $("#lblValISR").html("$ " + objPagos[0].deISR);
    $("#lblValDespensa").html("$ " + objPagos[0].deDespensa);
    $("#lblValPagoNeto").html("$ " + objPagos[0].dePagoNeto);

    dialogRegular2("modal-Preview", "Vista Previa", '600', 'auto');

    buttons = [{
        html: "<i class='icon-credit-card bigger-110'></i>&nbsp; " + "Generar Pago",
        class: "btn btn-xs btn-primary",
        click: function () {
            $.fn.modal.Constructor.prototype.enforceFocus = function () { };
            guardarPagoTrabajador();
            $("div[role='dialog']").css("z-index", 1200);
            $(this).dialog("destroy");
            $("#modal-Preview").hide();
        }
    }, {
        html: "<i class='icon-remove bigger-110'></i>&nbsp; " + "Cerrar",
        class: "btn btn-xs btn-grey",
        click: function () {
            $(this).dialog("destroy");
            $("#modal-Preview").hide();
        }
    }];
    $("#modal-Preview").dialog("option", "buttons", buttons);
}

function guardarPagoTrabajador() {

    arrGuardarPago = {
            iIdTrabajador: $("#txtNumeroEmpleado").val(),
            daFechaInicio: $("#txtFechaInicio").val(),
            daFechaFin: $("#txtFechaFin").val(),
            deDespensa: objPagos[0].deDespensa,
            deISR: objPagos[0].deISR,
            dePagoNeto: objPagos[0].dePagoNeto,
            deSueldoBruto: objPagos[0].deSueldoBruto
    };
    console.log(arrGuardarPago);
    connection.invoke("PagosNomina", "guardarPagoTrabajador", arrGuardarPago, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                toast("Notificación", "Se guardó correctamente el pago", "green", 3000, LIGHT, RIGHT);
                limpiarCampos();
                break;
            default:
                toast("Error", "Ocurrió un error inesperado, comunicarse con mesa de ayuda", "red", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function limpiarCampos() {
    $("#txtNumeroEmpleado").val("");
    $("#txtNombre").val("");
    $("#txtRol").val("");
    $("#txtTipo").val("");
    $("#txtFechaCaptura").val("");
    $("#txtFechaInicio").val("");
    $("#txtFechaFin").val("");

    $("#txtNumeroEmpleado").focus();
}