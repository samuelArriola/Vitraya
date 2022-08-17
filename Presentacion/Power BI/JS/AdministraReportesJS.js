//METODO FIJO DE AJAX
const ejecutarAjax = (url, datos, success) => {
    $.ajax({
        url: url,
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: success,
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE LOS INFORMES
function traerListaReportes() {

    $("#loading-modal").modal();
    ejecutarAjax("AdministrarReportes.aspx/getListaReportes", {}, mostrarListaReportes);
}

//FUNCION QUE RECIBE Y PLASMA EN LA TABLA LA INFORMACION DE LOS REPORTES
function mostrarListaReportes(msg) {

    datos = msg.d;

    dtListaR = "";

    datos.forEach((item) => {

        dtListaR += `

            <tr>
                
                <td>${item.OidReportePB1}</td>
                <td>${item.Nombre1}</td>
                <td>${item.Estado1 == 0 ? "INACTIVO" : "ACTIVO"}</td>
                <td>  <button  type="button" class="btnEditarReporte btn btn-primary" data-id="${item.OidReportePB1}" >EDITAR REPORTE  <i class="fa fa-pencil btnEditarReporte" data-id="${item.OidReportePB1}"></i></button> </td>
               
            </tr>

        `;

    })

    $("#tbInfoR").html(dtListaR);
    DataTable("#tableReportes", 10);

    $("#loading-modal").modal("hide");

}

//BOTON QUE TRASLADA LA INFORMACION DE UN REPORTE SELECCIONADO AL FORMULARIO
$(document).on("click", ".btnEditarReporte", function (e) {

    let idReporte = parseInt($(e.target).attr("data-id"));

    datos = {
        "idReporte": idReporte
    }

    $("#loading-modal").modal();
    ejecutarajax("AdministrarReportes.aspx/getDetalleReportes", datos, plasmarDetalleReporte)
})

//LISTA DE VARIABLES PARA GUARDAR LA INFORMACION TRAIDA DE BASE DE DATOS
let idReporte;
let nombreR;
let estadoR;
let descripcionR;
let tipoR;
let enlaceR;

//FUNCION QUE PLASMA EN EL FORMULARIO LA INFORMACION A EDITAR DE LOS REPORTES
function plasmarDetalleReporte(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        idReporte = item.OidReportePB1;
        nombreR = item.Nombre1;
        estadoR = item.Estado1;
        descripcionR = item.Descripcion1;
        tipoR = item.Tipo1;
        enlaceR = item.Enlace1;

    })

    $("#codigoR").val(idReporte);
    $("#nombreR").val(nombreR);
    $(`input[name=inlineRadioOptions][value="${estadoR}"]`).prop("checked", true);
    $("#tipoR").val(tipoR);
    $("#enlaceR").val(enlaceR);
    $("#descripcionR").val(descripcionR);
    $("#permisosU").val(0);
    $("#permisosC").val(0);
    $("#permisosUF").val(0);

    usuariosPermisosBD(idReporte);

    $("#btnGuardar").hide();
    $("#codigoRLabel").show();
    $("#codigoR").show();
    $("#btnActualizar").show();
    $("#loading-modal").modal("hide");
}

//FUNCION QUE REALIZA LA PETICION A BASE DE DATOS PARA TRAER LOS USUARIOS DE UN REPORTE SELECCIONADO
function usuariosPermisosBD(id) {

    datos = {
        "idReporte": id
    }

    ejecutarajax("AdministrarReportes.aspx/getPermisosBD", datos, plasmarPermisosReporte)
}

//FUNCION QUE RECIBE Y PLASMA EN LA TABLA DE EDITAR REPORTES TODOS LOS USUARIOS CON PERMISOS A ESE RESPECTIVO REPORTE
function plasmarPermisosReporte(msg) {

    datos = msg.d;

    dtListaPermisos = "";

    datos.forEach((item) => {

        dtListaPermisos += `

            <tr>
                
                <td>${item.CodigoPermisoUsu}</td>
                <td>${item.NombrePermisoUsu}</td>
                <td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td>
               
            </tr>

        `;

    })

    $("#tbInfoP").html(dtListaPermisos);
    DataTable("#tablePermisos", 10);
    DataTable("#tableReportes", 10);

}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE TODOS LOS USUARIOS
function traerListaUsuarios() {

    ejecutarAjax("AdministrarReportes.aspx/getUsuarios", {}, plasmarListaUsuarios);

}

//FUNCION QUE RECIBE Y PLASMA EN EL SELECT LA INFORMACION DE LOS USUARIOS
function plasmarListaUsuarios(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        $("#permisosU").append(`<option value=${item.CodigoPermisoUsu}>${item.NombrePermisoUsu}</option>`);

    })
}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE TODOS LOS CARGOS
function traerListaCargos() {

    ejecutarAjax("AdministrarReportes.aspx/getCargos", {}, plasmarListaCargos);

}

//FUNCION QUE RECIBE Y PLASMA EN EL SELECT LA INFORMACION DE LOS CARGOS
function plasmarListaCargos(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        $("#permisosC").append(`<option value=${item.CodigoPermisoCargo}>${item.NombrePermisoCargo}</option>`);

    })
}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE TODAS LAS UNIDADES FUNCIONALES
function traerListaUnidadesFuncionales() {

    ejecutarAjax("AdministrarReportes.aspx/getUnidadesFuncionales", {}, plasmarListaUnidadesFuncionales);

}

//FUNCION QUE RECIBE Y PLASMA EN EL SELECT LA INFORMACION DE LAS UNIDADES FUNCIONALES
function plasmarListaUnidadesFuncionales(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        $("#permisosUF").append(`<option value=${item.CodigoPermisoUniFun}>${item.NombrePermisoUniFun}</option>`);

    })
}

//BOTON PARA AÑADIR UN USUARIO A LA VEZ POR NOMBRE
$(document).on("click", ".btnpermisosU", function (e) {

    let usuarioAgregarId = $("#permisosU").val();
    let usuarioAgregarNom = $('select[id="permisosU"] option:selected').text();


    if (usuarioAgregarId == 0 ) {

        error("Error", "Debe seleccionar un usuario.")

    } else {

        let fila = "<tr><td>" + usuarioAgregarId + "</td><td>" + usuarioAgregarNom + "</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoP").appendChild(btn);

        DataTable("#tablePermisos", 10);
        $("#permisosU").val(0);

    }

})

//BOTON PARA AÑADIR USUARIOS POR SU CARGO
$(document).on("click", ".btnpermisosC", function (e) {

    let cargoAgregarId = $("#permisosC").val();

    datos = {
        "idCargo": parseInt(cargoAgregarId)
    }

    if (cargoAgregarId == 0) {

        error("Error", "Debe seleccionar un cargo.")

    } else {

        ejecutarAjax("AdministrarReportes.aspx/getUsuariosPorCargo", datos, añadirUsuariosPorCargosTabla);

    }

})

//FUNCION QUE RECIBE LOS USUARIOS POR CARGO Y LOS AGREGA A LA TABLA DE PERMISOS
function añadirUsuariosPorCargosTabla(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        let fila = "<tr><td>" + item.CodigoPermisoUsu + "</td><td>" + item.NombrePermisoUsu + "</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoP").appendChild(btn);

    })

    DataTable("#tablePermisos", 10);
    $("#permisosC").val(0);

}

//BOTON PARA AÑADIR USUARIOS POR UNIDAD FUNCIONAL
$(document).on("click", ".btnpermisosUF", function (e) {

    let UFAgregarId = $("#permisosUF").val();

    datos = {
        "idUnidadF": parseInt(UFAgregarId)
    }

    if (UFAgregarId == 0) {

        error("Error", "Debe seleccionar una unidad funcional.")

    } else {

        ejecutarAjax("AdministrarReportes.aspx/getUsuariosPorUF", datos, añadirUsuariosPorUFTabla);

    }

})

//FUNCION QUE RECIBE LOS USUARIOS POR UNIDAD FUNCIONAL Y LOS AGREGA A LA TABLA DE PERMISOS
function añadirUsuariosPorUFTabla(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        let fila = "<tr><td>" + item.CodigoPermisoUsu + "</td><td>" + item.NombrePermisoUsu + "</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoP").appendChild(btn);

    })

    DataTable("#tablePermisos", 10);
    $("#permisosUF").val(0);

}

$("#tablePermisos").on('click', '.btnDelete', function () {
    $(this).closest('tr').remove();
    //DataTable("#tablePermisos", 10);
});

//BOTON QUE REGISTRA UN NUEVO REPORTE EN EL SISTEMA
$(document).on("click", ".btnGuardar", function (e) {

    nombreR = $("#nombreR").val();
    if ($('#inlineRadio1').prop('checked') ) {
        estadoR = 1;
    } else if ($('#inlineRadio2').prop('checked') ) {
        estadoR = 0;
    }
    descripcionR = $("#descripcionR").val();
    tipoR = $("#tipoR").val();
    enlaceR = $("#enlaceR").val();

    datos = {
        "nombre": nombreR,
        "estado": estadoR,
        "descripcion": descripcionR,
        "tipo": tipoR,
        "enlace": enlaceR
    }

    if (nombreR == "" || !(estadoR == 0) && !(estadoR == 1) || descripcionR == "" || tipoR == 0 || enlaceR == "") {

        error("Error", "Debe diligenciar todos los campos")

    } else {

        ejecutarAjax("AdministrarReportes.aspx/setReporte", datos, agregarPermisosUsuarios);

    }

})

//FUNCION QUE RECIBE EL ID DEL REPORTE RECIENTEMENTE REGISTRADO PARA AGREGAR LOS USUARIOS CON SUS PERMISOS DE ESE RESPECTIVO REPORTE
function agregarPermisosUsuarios(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        idReporte = item.OidReportePB1;

    });

    $('#tablePermisos tbody tr').each(function () {

        var identificacionU = $(this).find('td').eq(0).text();
        var nombreU = $(this).find('td').eq(1).text();

        datos = {
            "idR": idReporte,
            "identificacionU": identificacionU,
            "nombreU": nombreU
        }

        ejecutarAjax("AdministrarReportes.aspx/InsertarPermisos", datos)

    });

    $("#nombreR").val("");
    $('#inlineRadio1').prop('checked', false)
    $('#inlineRadio2').prop('checked', false)
    $("#tipoR").val(0);
    $("#enlaceR").val("");
    $("#descripcionR").val("");
    $("#tbInfoP tr").remove();
    traerListaReportes();
    exito("Notificación", "Reporte guardado satisfactoriamente");
   
}

//BOTON QUE ACTUALIZA UN REPORTE EN EL SISTEMA
$(document).on("click", ".btnActualizar", function (e) {

    idReporte = $("#codigoR").val();
    nombreR = $("#nombreR").val();
    if ($('#inlineRadio1').prop('checked')) {
        estadoR = 1;
    } else if ($('#inlineRadio2').prop('checked')) {
        estadoR = 0;
    }
    descripcionR = $("#descripcionR").val();
    tipoR = $("#tipoR").val();
    enlaceR = $("#enlaceR").val();

    datos = {
        "codigo": parseInt(idReporte),
        "nombre": nombreR,
        "estado": estadoR,
        "descripcion": descripcionR,
        "tipo": tipoR,
        "enlace": enlaceR
    }

    if (nombreR == "" || !(estadoR == 0) && !(estadoR == 1) || descripcionR == "" || tipoR == 0 || enlaceR == "") {

        error("Error", "Debe diligenciar todos los campos")

    } else {

        ejecutarAjax("AdministrarReportes.aspx/updateReporte", datos);
        formatearPermisosUsuarios(idReporte);

    }

})

let idTemporal;

function formatearPermisosUsuarios(id) {

    idTemporal = id;

    datos = {
        "codigo": parseInt(id),
    }

    ejecutarAjax("AdministrarReportes.aspx/deletePermisos", datos, actualizarPermisosUsuarios);

}

function actualizarPermisosUsuarios() {

    let id = idTemporal;

    $('#tablePermisos tbody tr').each(function () {

        var identificacionU = $(this).find('td').eq(0).text();
        var nombreU = $(this).find('td').eq(1).text();

        datos = {
            "idR": id,
            "identificacionU": identificacionU,
            "nombreU": nombreU
        }

        ejecutarAjax("AdministrarReportes.aspx/InsertarPermisos", datos)

    });

    $("#nombreR").val("");
    $('#inlineRadio1').prop('checked', false)
    $('#inlineRadio2').prop('checked', false)
    $("#tipoR").val(0);
    $("#enlaceR").val("");
    $("#descripcionR").val("");
    $("#tbInfoP tr").remove();
    $("#btnActualizar").hide();
    $("#codigoRLabel").hide();
    $("#codigoR").hide();
    $("#btnGuardar").show();
    traerListaReportes();
    exito("Notificación", "Reporte actualizado satisfactoriamente");

}

$(document).on("click", ".btnFiltroNombreR", function (e) {

    let nombreReporte = $("#filtroNomReporte").val();

    datos = {
        "nombreReporte": nombreReporte
    }

    if (nombreReporte == "") {

        traerListaReportes();

    } else {

        ejecutarAjax("AdministrarReportes.aspx/filtroNombreR", datos, mostrarListaReportes);

    }

})

$(document).ready(function () {

    $("#btnActualizar").hide();
    $("#codigoRLabel").hide();
    $("#codigoR").hide();
    traerListaReportes();
    traerListaUsuarios();
    traerListaCargos();
    traerListaUnidadesFuncionales();

});