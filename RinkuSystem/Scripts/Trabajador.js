$(document).ready(function () {
    iniPantalla();
});

function iniPantalla() {
    Numerics("num", false, false);
    obtenerRoles();
    obtenerTiposTrabajador();
    $("#txtNombre").focus();

    $("#btnGuardar").click(function () {
        if(validarCampos())
        {
            guardarTrabajador();
        }
    });

    $("#btnBuscarEmpleado").click(function () {
        obtenerTrabajadores();
    });
    
    $("#btnLimpiar").click(function () {
        limpiarCampos();
    });

    initGridTrabajadores();
}

function obtenerRoles()
{
    connection.invoke('Trabajador', 'obtenerRoles', {}, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                var sHtml = "<option value = '0' selected>SELECCIONE UN ROL</option>";
                for (var i = 0 ; i < Respuesta.data.length ; i++) {
                    sHtml += "<option value='" + Respuesta.data[i].iID + "'>" + Respuesta.data[i].nvRol.toUpperCase() + "</option>"
                }
                $("#slcRoles").html(sHtml);
                break;
            case NO_DATOS:
                toast("Notificación", "No se encontraron datos de Roles", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function obtenerTiposTrabajador()
{
    connection.invoke('Trabajador', 'obtenerTiposTrabajador', {}, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                var sHtml = "<option value = '0' selected>SELECCIONE UN TIPO</option>";
                for (var i = 0 ; i < Respuesta.data.length ; i++) {
                    sHtml += "<option value='" + Respuesta.data[i].iID + "'>" + Respuesta.data[i].nvTipo.toUpperCase() + "</option>"
                }
                $("#slcTipo").html(sHtml);
                break;
            case NO_DATOS:
                toast("Notificación", "No se encontraron datos de Tipo de trabajador", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function validarCampos()
{
    var bBandera = false;

    if($("#txtNombre").val() == "")
    {
        toast("Notificación", "Favor de capturar el campo <b>NOMBRE</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#txtNombre").focus();
        return;
    }

    if ($("#slcRoles").val() == "0") {
        toast("Notificación", "Favor de seleccionar un <b>ROL</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#slcRoles").focus();
        return;
    }

    if ($("#slcTipo").val() == "0") {
        toast("Notificación", "Favor de seleccionar un <b>TIPO</b> ", "orange", 3000, LIGHT, RIGHT);
        $("#slcTipo").focus();
        return;
    }

   return bBandera = true;
}

function guardarTrabajador()
{
    var arrDatos = [];

    arrDatos.push({
        iID     : $("#txtNumeroEmpleado").val() == "" ? 0 : $("#txtNumeroEmpleado").val(),
        nvNombre: $("#txtNombre").val(),
        iRol    : $("#slcRoles").val(),
        iTipo   : $("#slcTipo").val(),
        iEstatus: $("#chkEstatus").is(":checked") == true ? 1 : 0
    });

    connection.invoke('Trabajador', 'guardarTrabajador', {args:JSON.stringify(arrDatos)}, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                toast("Notificación", "Se guardo correctamente al trabajador", "green", 3000, LIGHT, RIGHT);
                limpiarCampos();
                break;
            case NO_DATOS:
                toast("Notificación", "No se encontraron datos de Roles", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function initGridTrabajadores() {
    columns = ["Num. Empleado", "Nombre", "","Rol", "","Tipo", "","Estatus"];
    colModel = [{ name: "iID", index: "iID", width: 150},
                { name: "nvNombre", index: "nvNombre", width: 250 },
                { name: "iRol", index: "iRol", hidden: true },
                { name: "nvRol", index: "nvRol", width: 150 },
                { name: "iTipo", index: "iTipo", hidden: true },
                { name: "nvTipo", index: "nvTipo", width: 150 },
                { name: "iEstatus", index: "iEstatus", hidden:true },
                { name: "nvEstatus", index: "nvEstatus", width: 100}
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
            $("#slcRoles").val(row.iRol);
            $("#slcTipo").val(row.iTipo);

            validarEstatus = row.iEstatus == "true" ? true : false;

            $("#chkEstatus").prop('checked', validarEstatus);
  
            $(this).closest('.ui-dialog-content').dialog('close');
        },
    });

    updatePagerIcons();
}

function obtenerTrabajadores() {
    connection.invoke("Trabajador", "obtenerTrabajadores", {}, function (Respuesta) {
        switch (Respuesta.shStatus) {
            case OK_:
                mostrarGridTrabajadores(Respuesta.data);
                break;
            case NO_FOUND_RECORDS_:
                toast("Notificación", "No se encontraron trabajadores", "orange", 3000, LIGHT, RIGHT);
                break;
        }
    });
}

function mostrarGridTrabajadores(data) {
    $("#grid-Trabajadores").jqGrid('clearGridData');
    $("#grid-Trabajadores").jqGrid('addRowData', 1, data);
    dialogRegular2("div-grid-Trabajadores", "Trabajadores", 'auto', 'auto');
}

function limpiarCampos()
{
    $("#txtNumeroEmpleado").val("");
    $("#txtNombre").val("");
    $("#slcRoles").val(0);
    $("#slcTipo").val(0);
    $("#chkEstatus").prop('checked', true);

    $("#txtNombre").focus();
}