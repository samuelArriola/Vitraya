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

let tipoConvocatoria = 1;

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE TODOS LOS USUARIOS
function traerListaUsuarios() {

    ejecutarAjax("Convocatorias2.aspx/getUsuarios", {}, plasmarListaUsuarios);

}

//FUNCION QUE RECIBE Y PLASMA EN EL SELECT LA INFORMACION DE LOS USUARIOS
function plasmarListaUsuarios(msg) {

    let usuarioLog;

    datos = msg.d;

    datos.forEach((item) => {

        $("#coordReunion").append(`<option value=${item.CodigoPermisoUsu}>${item.NombrePermisoUsu}</option>`);
        $("#invitadosI").append(`<option value=${item.CodigoPermisoUsu} data-correo=${item.CorreoPermisoUsu}>${item.NombrePermisoUsu}</option>`);
        usuarioLog = item.UsuarioLogueado;

    })

    $("#coordReunion option[value=" + usuarioLog + "]").attr("selected", true);
}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE TODOS LOS CARGOS
function traerListaCargos() {

    ejecutarAjax("Convocatorias2.aspx/getCargos", {}, plasmarListaCargos);

}

//FUNCION QUE RECIBE Y PLASMA EN EL SELECT LA INFORMACION DE LOS CARGOS
function plasmarListaCargos(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        $("#invitadosC").append(`<option value=${item.CodigoPermisoCargo} data-correo=${item.CorreonombrePermisoCargo}>${item.NombrePermisoCargo}</option>`);

    })
}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE TODAS LAS UNIDADES FUNCIONALES
function traerListaUnidadesFuncionales() {

    ejecutarAjax("Convocatorias2.aspx/getUnidadesFuncionales", {}, plasmarListaUnidadesFuncionales);

}

//FUNCION QUE RECIBE Y PLASMA EN EL SELECT LA INFORMACION DE LAS UNIDADES FUNCIONALES
function plasmarListaUnidadesFuncionales(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        $("#unidadFReunion").append(`<option value=${item.CodigoPermisoUniFun}>${item.NombrePermisoUniFun}</option>`);
        $("#invitadosUF").append(`<option value=${item.CodigoPermisoUniFun} data-correo=${item.CorreonombrePermisoUniFun}>${item.NombrePermisoUniFun}</option>`);

    })
}

//BOTON PARA AÑADIR UN USUARIO A LA VEZ POR NOMBRE
$(document).on("click", ".btninvitadosI", function (e) {

    let usuarioAgregarId = $("#invitadosI").val();
    let usuarioAgregarNom = $('select[id="invitadosI"] option:selected').text();
    let usuarioCorreo = $('select[id="invitadosI"] option:selected').attr("data-correo");


    if (usuarioAgregarId == 0) {

        error("Error", "Debe seleccionar un usuario.")

    } else {

        let fila = "<tr><td>" + usuarioAgregarId + "</td><td>" + usuarioAgregarNom + "</td><td>Invitado</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoInvitados").appendChild(btn);

        DataTable("#tableInvitados", 5);
        $("#invitadosI").val(0);
        $(".tableInvitados").show();

    }

})

//BOTON PARA AÑADIR USUARIOS POR SU CARGO
$(document).on("click", ".btninvitadosC", function (e) {

    let cargoAgregarId = $("#invitadosC").val();

    datos = {
        "idCargo": parseInt(cargoAgregarId)
    }

    if (cargoAgregarId == 0) {

        error("Error", "Debe seleccionar un cargo.")

    } else {

        ejecutarAjax("Convocatorias2.aspx/getUsuariosPorCargo", datos, añadirUsuariosPorCargosTabla);

    }

})

//FUNCION QUE RECIBE LOS USUARIOS POR CARGO Y LOS AGREGA A LA TABLA DE PERMISOS
function añadirUsuariosPorCargosTabla(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        let fila = "<tr><td>" + item.CodigoPermisoUsu + "</td><td>" + item.NombrePermisoUsu + "</td><td>Invitado</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoInvitados").appendChild(btn);

    })

    DataTable("#tableInvitados", 5);
    $("#invitadosC").val(0);
    $(".tableInvitados").show();

}

//BOTON PARA AÑADIR USUARIOS POR UNIDAD FUNCIONAL
$(document).on("click", ".btninvitadosUF", function (e) {

    let UFAgregarId = $("#invitadosUF").val();

    datos = {
        "idUnidadF": parseInt(UFAgregarId)
    }

    if (UFAgregarId == 0) {

        error("Error", "Debe seleccionar una unidad funcional.")

    } else {

        ejecutarAjax("Convocatorias2.aspx/getUsuariosPorUF", datos, añadirUsuariosPorUFTabla);

    }

})

//FUNCION QUE RECIBE LOS USUARIOS POR UNIDAD FUNCIONAL Y LOS AGREGA A LA TABLA DE PERMISOS
function añadirUsuariosPorUFTabla(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        let fila = "<tr><td>" + item.CodigoPermisoUsu + "</td><td>" + item.NombrePermisoUsu + "</td><td>Invitado</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoInvitados").appendChild(btn);

    })

    DataTable("#tableInvitados", 5);
    $("#invitadosUF").val(0);
    $(".tableInvitados").show();

}

//ELIMINAR UN USUARIO DE LA TABLA DE INVITADOS
$("#tableInvitados").on('click', '.btnDelete', function () {
    $(this).closest('tr').remove();
    //DataTable("#tablePermisos", 10);
});

//BOTON PARA AÑADIR UN TEMA A LA VEZ POR NOMBRE
$(document).on("click", ".btnAñadirTema", function (e) {

    let nombreTema = $("#temasReunion").val();


    if (nombreTema == "") {

        error("Error", "Debe ingresar un nombre del tema.")

    } else {

        let fila = "<tr><td>" + nombreTema + "</td><td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td></tr>";

        let btn = document.createElement("TR");
        btn.innerHTML = fila;
        document.getElementById("tbInfoTemas").appendChild(btn);

        //DataTable("#tableTemas", 10);
        $("#temasReunion").val("");
        $(".tableTemas").show();

    }

})

//ELIMINAR UN TEMA DE LA TABLA DE AGENDA
$("#tableTemas").on('click', '.btnDelete', function () {
    $(this).closest('tr').remove();
});

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE LOS COMITES PROGRAMADOS
function traerComitesProgramados() {

    $("#loading-modal").modal();
    ejecutarAjax("Convocatorias2.aspx/getComitesProgramados", {}, plasmarComitesP);

}

//FUNCION QUE PLASMA EN UNA TABLA LOS COMITES PROGRAMADOS DEL USUARIO LOGUEADO
function plasmarComitesP(msg) {

    datos = msg.d;

    dtListaCP = "";

    datos.forEach((item) => {
        moment.locale('es');

        dtListaCP += `

            <tr>
                
                <td>${item.StrNombre}</td>
                <td>${moment(item.DtmFechEditable).format("LLLL")}</td>
                <td style="display:none;" >${item.DtmFecInicio}</td>
                <td style="display:none;" >${item.StrLugarReun}</td>
                <td>  <button  style="hight: 80px;" type="button" class="btnConvocarC btn btn-primary" data-id="${item.IntOidARActas}" data-idComite="${item.IntOidAReunionC}">CONVOCAR <i class="fa fa-bullhorn btnConvocarC" data-id="${item.IntOidARActas}" data-idComite="${item.IntOidAReunionC}"></i></button> </td>
               
            </tr>

        `;

    })

    $("#tbConvD").html(dtListaCP);
    DataTable("#tableConvocatoriasD", 10);
    traerComitesConvocados();
}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE LOS COMITES CONVOCADOS
function traerComitesConvocados() {

    ejecutarAjax("Convocatorias2.aspx/getComitesConvocados", {}, plasmarComitesC);

}

//FUNCION QUE PLASMA EN UNA TABLA LOS COMITES CONVOCADOS DEL USUARIO LOGUEADO
function plasmarComitesC(msg) {

    datos = msg.d;

    dtListaCC = "";

    datos.forEach((item) => {
        moment.locale('es');

        dtListaCC += `

            <tr>
                
                <td>${item.StrNombre}</td>
                <td>${item.StrLugarReun}</td>
                <td>${moment(item.DtmFechEditable).format("LLLL")}</td>
                <td>  <button  type="button" class="btnConvocarP btn btn-secondary" data-id="${item.IntOidARActas}" >DESARROLLAR <i class="fa fa-edit  btnConvocarP" data-id="${item.IntOidARActas}"></i></button> </td>
               
            </tr>

        `;

    })

    $("#tbConP").html(dtListaCC);
    DataTable("#tableConvocatoriasP", 10);
    $("#loading-modal").modal("hide");
}

let idActaComite;

//BOTON QUE TRASLADA LA INFORMACION DE UN COMITE SELECCIONADO AL FORMULARIO
$(document).on("click", ".btnConvocarC", function (e) {

    $("#loading-modal").modal();
    let idActa = parseInt($(e.target).attr("data-id"));
    idActaComite = idActa;
    let idComite = parseInt($(e.target).attr("data-idComite"));
    tipoConvocatoria = 2;

    let nombreComite = $(this).parents("tr").find("td").eq(0).html();
    let fechaComite = $(this).parents("tr").find("td").eq(2).html();
    let lugarComite = $(this).parents("tr").find("td").eq(3).html();

    $("#nomReunion").val(nombreComite);
    $("#fechaReunion").val(moment(fechaComite).format("YYYY-MM-DD") + 'T' + moment(fechaComite).format("HH:mm"));
    $("#lugarReunion").val(lugarComite);
    $(".DivunidadFReunion").hide();
    $(".DivcoordReunion").hide();

    traerTemasC(idComite);

    $(".tableTemas").show();
    $(".tableInvitados").show();
    $(".btnCancelar").show();

})

let idComiteTemporal;

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE LOS MIEMBROS DE COMITES PROGRAMADOS
function traerMiembrosC() {

    datos = {
        "idComite": idComiteTemporal
    }

    ejecutarAjax("Convocatorias2.aspx/getMiembrosC", datos, plasmarMiembrosC);

}

//FUNCION QUE PLASMA EN UNA TABLA LOS MIEMBROS DEL COMITE A CONVOCAR
function plasmarMiembrosC(msg) {

    datos = msg.d;

    dtListaMiembros = "";

    datos.forEach((item) => {

        dtListaMiembros += `

            <tr>
                
                <td>${item.GNCodUsu1}</td>
                <td>${item.NombreUsuario}</td>
                <td>${item.TpNomUsu1}</td>
                <td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td>
               
            </tr>

        `;

    })

    $("#tbInfoInvitados").html(dtListaMiembros);
    DataTable("#tableInvitados", 5);


    $("#loading-modal").modal("hide");

}

//FUNCION ENCARGADA DE REALIZAR LA PETICION AL SERVIDOR PARA TRAER LA INFORMACION DE LOS TEMAS DE COMITES PROGRAMADOS
function traerTemasC(idComite) {

    idComiteTemporal = idComite;

    datos = {
        "idComite": idComite
    }

    ejecutarAjax("Convocatorias2.aspx/getTemasC", datos, plasmarTemasC);

}

//FUNCION QUE PLASMA EN UNA TABLA LOS TEMAS DEL COMITE A CONVOCAR
function plasmarTemasC(msg) {

    datos = msg.d;

    dtListaTemas = "";

    datos.forEach((item) => {

        dtListaTemas += `

            <tr>
                
                <td>${item.Nombre}</td>
                <td><button type='button' class='btnDelete btn btn-danger'><i class='fa  fa-trash-o btnDelete'></i></button></td>
               
            </tr>

        `;

    })

    $("#tbInfoTemas").html(dtListaTemas);
    traerMiembrosC();
}

//FUNCION QUE AL DAR CLICK EN EL BOTON CANCELAR LIMPIA EL FORMULARIO
$(document).on("click", ".btnCancelar", function (e) {

    $("#nomReunion").val("");
    $("#fechaReunion").val("");
    $("#lugarReunion").val("");

    $(".DivunidadFReunion").show();
    $(".DivcoordReunion").show();

    $("#tbInfoTemas tr").remove();
    $("#tbInfoInvitados tr").remove();

    $(".tableTemas").hide();
    $(".tableInvitados").hide();
    $(".btnCancelar").hide();

    tipoConvocatoria = 1;

})

//FUNCION SOBRE EL BOTON DE REALIZAR CONVOCATORIA PARA INSERTARLA EN BASE DE DATOS
$(document).on("click", ".btnGuardar", function (e) {

    if (tipoConvocatoria == 1) {

        if ($("#nomReunion").val() != "" && $("#unidadFReunion").val() != 0 && $("#coordReunion").val() != "" && $("#fechaReunion").val() != "" && $("#lugarReunion").val() != "") {

            datos = {
                "nombreReunion": $("#nomReunion").val(),
                "unidadFuncionalReunion": $("#unidadFReunion").val(),
                "coordinadorReunion": $("#coordReunion").val(),
                "fechaReunion": $("#fechaReunion").val(),
                "lugarReunion": $("#lugarReunion").val(),
                "linkReunion": $("#linkReunion").val()
            }

            $("#loading-modal").modal();
            ejecutarAjax("Convocatorias2.aspx/setConvocatoriaRG", datos, matriculaCorreoMiembros);

        } else {
            error("Alerta", "Debe diligenciar todos los campos marcados como obligatorios")
        }

    }
    if (tipoConvocatoria == 2) {

        if ($("#nomReunion").val() != "" && $("#fechaReunion").val() != "" && $("#lugarReunion").val() != "") {

            datos = {
                "idActaComite": idActaComite,
                "fechaReunion": $("#fechaReunion").val(),
                "lugarReunion": $("#lugarReunion").val()
            }

            ejecutarAjax("Convocatorias2.aspx/setConvocatoriaCP", datos, matriculaCorreoMiembros);

        } else {
            error("Alerta", "Debe diligenciar todos los campos marcados como obligatorios")
        }

    }

})

//FUNCION PARA MATRICULAR LOS MIEMBROS DE UNA REUNION O COMITE
function matriculaCorreoMiembros() {

    $('#tableInvitados tbody tr').each(function () {

        var identificacionU = $(this).find('td').eq(0).text();
        var nombreU = $(this).find('td').eq(1).text();
        var tipoU = $(this).find('td').eq(2).text();

        datos = {
            "identificacionU": identificacionU,
            "nombreU": nombreU,
            "tipoU": tipoU
        }

        ejecutarAjax("Convocatorias2.aspx/setMiembros", datos)

    });

    insertarTemas();
}

//FUNCION PARA REGISTRAR LOS TEMAS DE UNA REUNION O COMITE
function insertarTemas() {

    $('#tableTemas tbody tr').each(function () {

        var nombreTema = $(this).find('td').eq(0).text();

        datos = {
            "nombreTema": nombreTema
        }

        ejecutarAjax("Convocatorias2.aspx/setTemas", datos)

    });

    exitoConvocatoria();
}

//FUNCION PARA DAR POR TERMINADO EL REGISTRO
function exitoConvocatoria() {

    $("#nomReunion").val("");
    $("#fechaReunion").val("");
    $("#lugarReunion").val("");
    $("#unidadFReunion").val(0);
    $("#coordReunion").val(0);
    $("#linkReunion").val("");

    $(".DivunidadFReunion").show();
    $(".DivcoordReunion").show();

    $("#tbInfoTemas tr").remove();
    $("#tbInfoInvitados tr").remove();

    $(".tableTemas").hide();
    $(".tableInvitados").hide();
    $(".btnCancelar").hide();

    tipoConvocatoria = 1;

    traerComitesProgramados();

    exito("Notificación", "Convocatoria realizada satisfactoriamente");
}

//BOTON QUE REDIRECCIONA A LA PAGINA DE DESARROLLO DE LOS TEMAS
$(document).on("click", ".btnConvocarP", function (e) {

    let idActa = parseInt($(e.target).attr("data-id"));

    datos = {
        "idActa": idActa
    }

    ejecutarAjax("Convocatorias2.aspx/intoDesarrollo", datos, redirectDesarrollo)

})

function redirectDesarrollo() {

    window.location.href = "../proceedings/RecordMinutes.aspx";

}

$(document).ready(function () {

    traerComitesProgramados();
    traerListaUsuarios();
    traerListaCargos();
    traerListaUnidadesFuncionales();

});