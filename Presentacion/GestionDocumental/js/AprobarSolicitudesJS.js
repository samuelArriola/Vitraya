let txtNomDoc = $("#txtNomDoc");
let ddlTipoSol = $("#ddlTipoSol");
let dtFecha = $("#dtFecha");
let txtTipoDoc = $("#txtTipoDoc");
let ddlEstado = $("#ddlEstado");
let btnRechazar = $("#btnRechazar");
let btnAprobar = $("#btnAprobar");

var datos;
var index;

const cargarDatos = () => {
    ejecutarajax(
        "AprobarSolicitudes.aspx/GetSolicitudes",
        {
            "nombre":txtNomDoc.val(),
            "tipoSol":ddlTipoSol.val(),
            "fecha": (dtFecha.val() == "")? new Date("01/01/3000") : new Date(dtFecha.val()),
            "tipoDoc": txtTipoDoc.val(),
            "estado": ddlEstado.val(),
        },
        cargarTablaSolicitudes
    )
}

const cargarTablaSolicitudes = (msg) => {
    datos = JSON.parse(msg.d);
    console.log(datos);
    tabla = "";
    for (var i = 0; i < datos.length; i++) {
        tabla += `
            <tr>
                <td>${i + 1}</td>
                <td>${datos[i].StrTipoDoc}</td>
                <td>${datos[i].StrNomDoc}</td>
                <td>${datos[i].StrTipoSol}</td>
                <td>${datos[i].DtmFechaSol}</td>
                <td>${datos[i].StrEstado}</td>
                <td><a href="#" onClick = "cargarInfoModal(${i})"><i class="fa fa-eye"></i></a></td>
            </tr>
        `
        $("#tbSolicitud tbody").html(tabla);
    }
}

cargarDatos();

txtTipoDoc.on("keyup", cargarDatos);
txtNomDoc.on("keyup", cargarDatos);
ddlTipoSol.on("change", cargarDatos);
ddlEstado.on("change", cargarDatos);
dtFecha.on("change", cargarDatos);

btnRechazar.on("click", ValidarIncidencia);
btnAprobar.on("click", aprobarSolicitud);

function ValidarIncidencia() {
    if ($("#txtIncidencia").val() == "") {
        error("error", "Por favor describa las causas de rechazo.")
    } else {
        ejecutarajax(
            "AprobarSolicitudes.aspx/SetEvaluacion",
            {
                "oidSolicitud": "" + datos[index].IntOidGDSolicitud,
                "incidencia": $("#txtIncidencia").val(),
                "estado": "Rechazado",
            },
            Rechazada
        );
        $('#exampleModalCenter').modal('hide')
    }
}

function Rechazada(){
    exito("success", "Solicitud Rechazada");
    cargarDatos();
}

function aprobarSolicitud() {
    ejecutarajax(
        "AprobarSolicitudes.aspx/SetEvaluacion",
        {
            "oidSolicitud": "" + datos[index].IntOidGDSolicitud,
            "incidencia":"",
            "estado": "Aprobado",
        },
        Aprobada
    );
    $('#exampleModalCenter').modal('hide')
}

function Aprobada() {
    exito("success", "Solicitud Aprobada");
    cargarDatos();
}

function cargarInfoModal(i) {
    index = i;
    let modalBody = $("#imodal-body");
   
    modalBody.html("" +
        //'<strong>Proceso:&nbsp; </strong> ' + datos[i].IntOidGNProceso + '<br>' +
        '<strong>Tipo Documento:</strong>&nbsp' + datos[i].StrTipoDoc + '<br>' +
        '<strong>Nombre Procedimiento:&nbsp </strong>' + datos[i].StrNomDoc + '<br>' +
        '<strong>Tipo Solicitud: &nbsp</strong> ' + datos[i].StrTipoSol + '<br>' +
        '<strong>Solicitante: &nbsp</strong>' + datos[i].StrNomUsu + '<br>' +
        '<strong>Justificaciòn:&nbsp</strong>' + datos[i].StrJusSol + '<br>' +
        '<strong>Descripciòn:&nbsp </strong>' + datos[i].StrDesSol + '<br>'
    );

    $("#exampleModalCenter").modal();
}