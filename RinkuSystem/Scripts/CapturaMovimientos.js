var objTrabajadores = {};
$(document).ready(function () {
    iniPantalla();
});

function iniPantalla()
{
    Numerics("num", false, false);
    $("#txtNumeroEmpleado").focus();
    $("#txtFechaCaptura").datepicker({
        format: 'dd-mm-yyyy',
        language: "es",
        endDate: '0d',
        autoclose: true
    });

    $("#btnBuscarEmpleado").click(function () {
        mostrarGridTrabajadores(objTrabajadores);
    });

    $("#btnCancelar").click(function () {
        limiarCampos();
    });

    $("#txtNumeroEmpleado").keyup(function (e) {
        if (e.keyCode == 13)
            obtenerTrabajador();
    });

    $("#btnCalendario").click(function () {
        $("#txtFechaCaptura").focus();
    });

    $("#btnGuardar").click(function () {
        if(validarCampos())
        {
            guardarMovimientoTrabajador();
        }
    });

    initGridTrabajadores();
    obtenerTrabajadores();
}

function obtenerTrabajador()
{
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

                    if (objTrabajador[0].iRol == 3)
                    {
                        $("#chkCubrioTurno").prop("disabled", false);
                        $("#txtRolhidden").val(objTrabajador[0].iRol);
                    }
                    else
                    {
                        $("#chkCubrioTurno").prop("disabled", true);
                        $("#txtRolhidden").val("");
                    }
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

function guardarMovimientoTrabajador()
{
    var objDatos = {
        iIdTrabajador: $("#txtNumeroEmpleado").val(),
        iEntregas: $("#txtCantidadEntregas").val(),
        daFecha:  $("#txtFechaCaptura").val(),
        bCubrioTurno: $("#chkCubrioTurno").is(':checked')
    }

    connection.invoke("CapturaMovimientos", "guardarMovimientoTrabajador", objDatos, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                toast("Notificación", "Se guardo correctamente la captura de movimiento", "green", 3000, LIGHT, RIGHT);
                limiarCampos();
                break;
            default:
                break;
        }
    });
}

function validarCampos() {
    var bBandera = false;

    if ($("#txtNumeroEmpleado").val() == "") {
        toast("Notificación", "Favor de capturar el campo <b>Num Empleado</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#txtNumeroEmpleado").focus();
        return;
    }

    if ($("#txtFechaCaptura").val() == "") {
        toast("Notificación", "Favor de seleccionar un <b>Fecha</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#txtFechaCaptura").focus();
        return;
    }

    if ($("#txtCantidadEntregas").val() == "") {
        toast("Notificación", "Cantidad de entregas no puede estar vacío, Favor de colocar una cantidad", "orange", 3000, LIGHT, RIGHT);
        $("#txtCantidadEntregas").focus();
        return;
    }

    if ($("#txtCantidadEntregas").val() == 0 ) {
        toast("Notificación", "Favor de capturar una cantidad de entregas correcta", "orange", 3000, LIGHT, RIGHT);
        $("#txtCantidadEntregas").focus();
        return;
    }

    var validarCheck = $("#chkCubrioTurno").is(':checked');

    if ($("#txtRolhidden").val() != 3 && validarCheck) {
        toast("Notificación", "No se puede cubrir turno por el Rol que tiene el empleado", "orange", 3000, LIGHT, RIGHT);
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

function limiarCampos() {
    $("#txtNumeroEmpleado").val("");
    $("#txtNombre").val("");
    $("#txtRol").val("");
    $("#txtTipo").val("");
    $("#chkCubrioTurno").prop('checked', false);
    $("#txtCantidadEntregas").val("");
    $("#txtFechaCaptura").val("");

    $("#txtNumeroEmpleado").focus();
}