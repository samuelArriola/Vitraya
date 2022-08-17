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

var datos;

ddlTipDoc = $("#ContentPlaceHolder_ddlTipDoc")[0];
rdCrear = $("#ContentPlaceHolder_rdCrear")[0];
reEliminar = $("#ContentPlaceHolder_reEliminar")[0];
rdEditar = $("#ContentPlaceHolder_rdEditar")[0];
ddlProcesos = $("#ContentPlaceHolder_ddlProcesos")[0];
txtNomProc = $("#ContentPlaceHolder_txtNomProc")[0];
nomDoc = $("#ContentPlaceHolder_nomDoc")[0];
txtJus = $("#ContentPlaceHolder_txtJus")[0];
btnSolicitarPro = $("#ContentPlaceHolder_btnSolicitarPro")[0];


$(document).on('change', '#ContentPlaceHolder_ddlTipDoc', DatosDocumento);
async function DatosDocumento(evt) { //activar campos adicionales para documentos que lo requieren.
    if (this.value == "-1") {
        $(".DivProcedimiento").css("display", "none");
        $(".pnProceso").css("display","none");
        $(".pnNombre").css("display","none");
        $(".pnEditar").css("display", "none");
        $(".pnResponsable").css("display", "none");
    }
    else {

        if ($("#ContentPlaceHolder_rdCrear")[0].checked) {

            $(".DivProcedimiento").css("display", "block");
            $(".pnProceso").css("display", "block");
            $(".pnNombre").css("display", "block");
            $(".pnEditar").css("display", "none");
            $(".pnResponsable").css("display", "block");

        } else {

            $(".DivProcedimiento").css("display", "block");
            $(".pnProceso").css("display", "none");
            $(".pnNombre").css("display", "none");
            $(".pnEditar").css("display", "block");
            $("#ContentPlaceHolder_nomDoc").html("");
            $(".pnResponsable").css("display", "block");

            let tipo = this.value;

            ((await GetDataAjax("Solicitudes.aspx/GetDocumentosByTipo", { tipo: tipo })).d).forEach(documento => {
                $("#ContentPlaceHolder_nomDoc").prepend(`<option value="${documento.IntOidGDDocumento}">${documento.StrNomDoc}</option>`)
            });
        }
    }
}


const cargarDatos = () => {
    ejecutarajax(
        "Solicitudes.aspx/GetSolicitudes",
        {
            "nombre": $("#txtNomDoc").val(),
            "tipoSol": $("#ddlTipoSol").val(),
            "fecha": ($("#dtFecha").val() == "") ? new Date("01/01/3000") : new Date($("#dtFecha").val()),
            "tipoDoc": $("#txtTipoDoc").val(),
            "estado": $("#ddlEstado").val(),
        },
        cargarTablaSolicitudes
    )
}

function GetDataAjax(url, datos) {
    return $.ajax({
        url: url,
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
    });
}


$("#ContentPlaceHolder_rdEditar, #ContentPlaceHolder_reEliminar").on("change", async function (evt) {
    evt.preventDefault();
    $(".pnProceso").css("display", "none");
    $(".pnNombre").css("display", "none");
    $(".pnEditar").css("display", "block");
    let tipo = $("#ContentPlaceHolder_ddlTipDoc").val(); 
    $("#ContentPlaceHolder_nomDoc").html("");
    if (this.checked && tipo != "-1") {
        ((await GetDataAjax("Solicitudes.aspx/GetDocumentosByTipo", { tipo: tipo })).d).forEach(documento => {
            $("#ContentPlaceHolder_nomDoc").prepend(`<option value="${documento.IntOidGDDocumento}">${documento.StrNomDoc}</option>`)
        });
    }
});

$("#ContentPlaceHolder_rdCrear").on("change", function () {
    if (this.checked) {
        $(".pnProceso").css("display", "block");
        $(".pnNombre").css("display", "block");
        $(".pnEditar").css("display", "none");
    }
})

$(btnSolicitarPro).on("click", SetSolicitud);

$("form").submit(function (e) { e.preventDefault();}).keypress(function (e) {if(e.keyCode == 13) e.preventDefault() });

const cargarTablaSolicitudes = (msg) => {
    datos = JSON.parse(msg.d);
    tabla = "";
    for (var i = 0; i < datos.length; i++) {

        if (datos[i].StrEstado == "Finalizado")
            continue;

        tabla += `
            <tr>
                <td>${i + 1}</td>
                <td>${datos[i].StrTipoDoc}</td>
                <td>${datos[i].StrNomDoc}</td>
                <td>${datos[i].StrTipoSol}</td>
                <td>${datos[i].DtmFechaSol}</td>
                <td>${datos[i].StrEstado}</td>
        `;
        

        if (datos[i].StrEstado == "Aprobado") {
            if (datos[i].StrTipoSol.toLowerCase() == "eliminar") {
                tabla += `
                    <td>
                        <i class="fa fa-trash btn-delDoc" data-idDoc="${datos[i].IntOidGDDocE}" data-idSol="${datos[i].IntOidGDSolicitud}"></i>
                    </td>
                `;
            }
            else {
                switch (datos[i].StrTipoDoc) {
                    case "Procedimiento": {
                        tabla += `
                                <td><a href="CrearProcedimiento.aspx?OIdSolicitud=${datos[i].IntOidGDSolicitud}" ><i class="fa fa-edit"></i></a></td>
                                `;
                        break
                    }
                    case "Indicador": {
                        tabla += `
                                <td><a href="CrearIndicador.aspx?OIdSolicitud=${datos[i].IntOidGDSolicitud}" ><i class="fa fa-edit"></i></a></td>
                                `;
                        break;
                    }
                    case "Proceso": {
                        tabla += `
                                <td><a href="CrearProceso.aspx?OIdSolicitud=${datos[i].IntOidGDSolicitud}" ><i class="fa fa-edit"></i></a></td>
                                `;
                        break;
                    }
                    case "Protocolo": {
                        tabla += `
                                <td><a href="CrearProtocolo.aspx?OIdSolicitud=${datos[i].IntOidGDSolicitud}" ><i class="fa fa-edit"></i></a></td>
                                `;
                        break;
                    }
                    case "Manual": {
                        tabla += `
                                <td><a href="CrearManual.aspx?OIdSolicitud=${datos[i].IntOidGDSolicitud}" ><i class="fa fa-edit"></i></a></td>
                                `;
                        break;
                    }
                    case "Politica": {
                        tabla += `
                                <td><a href="CrearPolitica.aspx?OIdSolicitud=${datos[i].IntOidGDSolicitud}" ><i class="fa fa-edit"></i></a></td>
                                `;
                        break;
                    }
                }
            }
        }
        else {
            tabla += `<td></td>`;
        }

        tabla += `
                </tr>
            `;

        $("#tbSolicitud tbody").html(tabla);
    }
    DataTable("#tbSolicitud");
    cargarUsuarios();
}

function cargarUsuarios() {

    ejecutarAjax("Solicitudes.aspx/getUsuarios", {}, plasmarListaUsuarios);

}

function plasmarListaUsuarios(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        $("#usuarioResponsable").append(`<option value=${item.CodigoPermisoUsu}>${item.NombrePermisoUsu}</option>`);

    })
}

function validarFormularlio() {
    if (rdCrear.checked) {
        if (ddlProcesos.value == -1) {
            error("Campos incompretos", "Seleccione el Campo Proceso antes de realizar la solicitud");
            return false
        }
        if (!txtNomDoc.value) {
            error("Campos incompretos", "Complete el Campo Nombre del Documento antes de realizar la solicitud");
            return false;
        }
        else {
            if (nomDoc.value == "-1") {
                error("Campos incompretos", "Seleccione el Campo Nombre del documento antes de realizar la solicittud ");
                return false;
            }
        }
        if (!txtJus.value) {
            error("Campos incompretos", "Complete el Campo Justificación/Descripción antes de realizar la solicitud");
            return false;
        }
        if ($("#usuarioResponsable").val() == "") {
            error("Campos incompretos", "Debe seleccionar un usuario responsable antes de realizar la solicitud");
            return false;
        }
        return true;
    }
}


async function SetSolicitud() {

    if (!validarFormularlio)
        return;

    let solicitud = {
        "DblCodUsu": $("#usuarioResponsable").val(),
        "IntOidGNProceso": ddlProcesos.value,
        "IntOidGDSolicitud" : 0,
        "StrTipoSol": rdCrear.checked ? "crear" : rdEditar.checked ? "editar" : "eliminar",
        "StrNomDoc": rdCrear.checked ? txtNomProc.value : nomDoc.innerText,
        "StrNomUsu" : "",
        "StrCarUsu" : "",
        "StrJusSol": txtJus.value,
        "StrDesSol": "",
        "StrTipoDoc": ddlTipDoc.value,
        "DtmFechaSol" : new Date(),
        "StrEstado": "Pendiente...",
        "StrIncidencia" : "",
        "StrUnidadFuncional" : "",
        "IntGnDcDep" : 0,
        "IntOidGDDocE": rdCrear.checked ? 0 : nomDoc.value
    }

    console.log(solicitud);

    ejecutarajax(
        "Solicitudes.aspx/RealizarSolicitud",
        {
            "solicitud": solicitud
        },
        function () {
            exito("Hecho", "Solicitud realizada con exito");
            cargarDatos();
            txtNomDoc.value = "";
            txtJus.value = "";
            ddlProcesos.value = ddlTipDoc.value = -1
        }
    )

}

$(document).on("click", ".btn-delDoc", function (e) {
    let datos = {
        idDocumento : parseInt($(e.target).attr("data-idDoc")),
        idSolicitud : parseInt($(e.target).attr("data-idSol")),
    }
    ejecutarajax(
        "Solicitudes.aspx/DeleteDocument",
        datos,
        function (msg) {
            exito("Hecho", "El Documento ha sido marcado como eliminado");
            cargarDatos();
        }
    )
});


cargarDatos();



$(document).on('keypress', '#txtTipoDoc, #txtNomDoc', function (e) {  if(e.keyCode == 13)cargarDatos() });
$(document).on('change', '#ddlTipoSol', function () { cargarDatos() });
$(document).on('change', '#ddlEstado', function () { cargarDatos() });
$(document).on('change', '#dtFecha', function () { cargarDatos() });