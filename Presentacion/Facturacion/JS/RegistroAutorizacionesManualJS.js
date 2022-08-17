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
            console.log(result);
        }
    });
}

let OidRegAut;
let tipoId;
let numId;
let nombres;
let numSolicitud;
let fechaSolicitud;
let origenAtencion;
let ubicacion;
let tipoServicio;
let fechaIngreso;
let numIngreso;
let prioridad;
let contrato;
let servicio;
let numCama;
let diagPrincipal;
let diag1;
let diag2;
let nombreIps;
let direccionIps;
let justificacionClinica;
let clasificacionT;
let tecnologiaT;
let cantidadT;
let profesionalSalud;
let cargoProfesional;
let ordenMedica;

let campoIngreso = document.getElementById("numeroIngreso");

campoIngreso.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {

        traerInfoIngreso();
    }

});

$(document).on("click", ".btnBuscarInfoIngreso", function (e) {

    traerInfoIngreso();

})

function traerInfoIngreso() {

    numIngreso = String($("#numeroIngreso").val());

    datos = {
        "numIngreso": numIngreso
    }

    if (numIngreso != "") {

        $("#loading-modal").modal();
        ejecutarAjax("SolicitarAutorizacionesManual.aspx/GetDatosDinamica", datos, plasmarInfoIngreso)

    } else {

        error("Error", "Debe ingresar el numero de ingreso del paciente.");
    }
}

function plasmarInfoIngreso(msg) {

    let numServicio;
    datos = msg.d;

    if (datos.length != 0) {

        datos.forEach((item) => {
            
            tipoId = item.TipoId;
            numId = item.NumId;
            nombres = item.Nombres;
            origenAtencion = item.OrigenAtencion;
            ubicacion = item.UbicacionPaciente;
            tipoServicio = item.TipoServiciosSolicitados;
            fechaIngreso = moment(item.FechaHoraIngreso).format('YYYY-MM-DD') + 'T' + moment(item.FechaHoraIngreso).format('HH:mm');
            numIngreso = item.NumeroIngreso;
            prioridad = item.PrioridadAtencion;
            tipoServicio = item.TipoServiciosSolicitados;
            contrato = item.NumContratoPrestacion + ' - ' + item.ContratoPrestacion;
            servicio = item.NumServicio + ' - ' + item.Servicio;
            numServicio = item.NumServicio;
            numCama = item.NumeroCama;
            diagPrincipal = item.NumDiagnosticoPrincipal + ' - ' + item.DiagnosticoPrincipal;
            tecnologiaT = item.NombreCUPS1 + ' - ' + item.DescripCUPS1;
            profesionalSalud = item.NombreProfesional;

        })

        $("#tipoId").val(tipoId);
        $("#numId").val(numId);
        $("#nombres").val(nombres);
        $("#origenAtencion").val(origenAtencion);
        $("#ubicacion").val(ubicacion);
        $("#fechaIngreso").val(fechaIngreso);
        $("#prioridad").val(prioridad);
        $("#tipoServicio").val(tipoServicio);
        $("#contrato").val(contrato);
        $("#servicio").val(servicio);
        $("#numCama").val(numCama);
        $("#diagPrincipal").val(diagPrincipal);
        $("#justificacionClinica").val("N");
        $("#profesionalSalud").val(profesionalSalud);
        $("#cargoProfesional").val("Autorizador");
        $("#clasificacionT").val(numServicio);
        $("#tecnologiaT").val(tecnologiaT);
        $("#cantidadT").val("1");

        $("#loading-modal").modal("hide");

    } else if (datos.length == 0) {

        $("#loading-modal").modal("hide");
        error("Error", "El numero de ingreso no cuenta con registro en el sistema");
    }

}

$(document).on("click", ".btnGuardar", function (e) {

    tipoId = $("#tipoId").val();
    numId = $("#numId").val();
    nombres = $("#nombres").val();
    numSolicitud = $("#numSolicitud").val();
    fechaSolicitud = $("#fechaSolicitud").val();
    origenAtencion = $("#origenAtencion").val();
    ubicacion = $("#ubicacion").val();
    tipoServicio = $("#tipoServicio").val();
    fechaIngreso = $("#fechaIngreso").val();
    numIngreso = $("#numeroIngreso").val();
    prioridad = $("#prioridad").val();
    contrato = $("#contrato").val();
    servicio = $("#servicio").val();
    numCama = $("#numCama").val();
    diagPrincipal = $("#diagPrincipal").val();
    diag1 = $("#diag1").val();
    diag2 = $("#diag2").val();
    nombreIps = $("#nombreIps").val();
    direccionIps = $("#direccionIps").val();
    justificacionClinica = $("#justificacionClinica").val();
    clasificacionT = $("#clasificacionT").val();
    tecnologiaT = $("#tecnologiaT").val();
    cantidadT = $("#cantidadT").val();
    profesionalSalud = $("#profesionalSalud").val();
    cargoProfesional = $("#cargoProfesional").val();
    ordenMedica = $('#ordenMedica').val();

    if (tipoId == 0 || numId == "" || nombres == "" || numSolicitud == "" || fechaSolicitud == "" || origenAtencion == "" || ubicacion == "" || tipoServicio == "" || fechaIngreso == "" || numIngreso == "" ||
        prioridad == "" || contrato == "" || servicio == "" || servicio == " - " || numCama == "" || diagPrincipal == "" || nombreIps == "" || direccionIps == "" || justificacionClinica == "" ||
        profesionalSalud == "" || clasificacionT == "" || tecnologiaT == "" || tecnologiaT == " - "/*|| ordenMedica == ""*/) {

        error("Error", "Debe diligenciar todos los campos marcados como obligatorios");

    } else {

        datos = {
            "tipoId": tipoId,
            "numId": String(numId),
            "nombres": nombres,
            "numSolicitud": String(numSolicitud),
            "fechaSolicitud": fechaSolicitud,
            "origenAtencion": origenAtencion,
            "ubicacion": ubicacion,
            "tipoServicio": tipoServicio,
            "fechaIngreso": fechaIngreso,
            "numIngreso": numIngreso,
            "prioridad": prioridad,
            "contrato": contrato,
            "servicio": servicio,
            "numCama": numCama,
            "diagPrincipal": diagPrincipal,
            "diag1": diag1,
            "diag2": diag2,
            "nombreIps": nombreIps,
            "direccionIps": direccionIps,
            "justificacionClinica": justificacionClinica,
            "clasificacionT": clasificacionT,
            "tecnologiaT": tecnologiaT,
            "cantidadT": cantidadT,
            "profesionalSalud": profesionalSalud,
            "cargoProfesional": cargoProfesional
        }

        /*ejecutarAjax("SolicitarAutorizaciones.aspx/InsertarAutorizacion", datos, insertarTecnologias)*/
        ejecutarAjax("SolicitarAutorizacionesManual.aspx/InsertarAutorizacion", datos, ObtenerArchivo)
    }

})

function ObtenerArchivo(msg) {

    detalles = msg.d;

    if (ordenMedica != "") {

        detalles.forEach((detalle) => {

            OidRegAut = detalle.OidRegAutorizacion1;

        });

        var Contenido;
        var Extension;

        var file = $('#ordenMedica').prop('files');

        let nombre = file[0].name;

        let nombres = nombre.split(".");

        nombre = nombre.replace(`.${nombres[nombres.length - 1]}`, "");

        console.log(nombre);

        Contenido = file[0].type;

        console.log(Contenido);

        Extension = nombres[nombres.length - 1];

        console.log(Extension);

        var $i = $('#ordenMedica'), // Put file input ID here
            input = $i[0]; // Getting the element from jQuery

        file = input.files[0]; // The file
        fr = new FileReader(); // FileReader instance
        fr.onload = function () {
            // Do stuff on onload, use fr.result for contents of file
            //$('#file-content').append($('<div/>').html(fr.result))
            var codificado = fr.result;
            codificado2 = codificado.replace("data:" + Contenido + ";base64,", "")

            setArchivoOrdenMedica(nombre, codificado2, Contenido, Extension, OidRegAut)

            //console.log(codificado2);
            //console.log(_base64ToArrayBuffer(codificado2));

        };
        //fr.readAsText( file );
        fr.readAsDataURL(file);

    } else {

        mensajeAutGuardada();

    }

}

function setArchivoOrdenMedica(Nombre, Archivo, Contenido, Extension, OidRegAutorizacion) {

    datos = {
        "Nombre": Nombre,
        "Archivo": Archivo,
        "Contenido": Contenido,
        "Extension": Extension,
        "OidRegAutorizacion": OidRegAutorizacion
    }

    ejecutarAjax("SolicitarAutorizacionesManual.aspx/InsertarDocumentoOrdenM", datos, mensajeAutGuardada)

}

function traerCUPS() {

    ejecutarAjax("SolicitarAutorizacionesManual.aspx/getCUPS", {}, llenarCUPS);
}

function llenarCUPS(msg) {

    datos = msg.d;

    let descripcionCUP;

    opcCups = "";

    datos.forEach((item) => {

        descripcionCUP = item.NombreCUPS1 + ' ' + item.DescripCUPS1;

        opcCups += `

            <option>${descripcionCUP}</option>

        `;
    })
    $("#listaCups").html(opcCups);

}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function mensajeAutGuardada() {


    exito("Notificacion", "Se ha registrado la solicitud de autorización satisfactoriamente");

    $("#tipoId").val(0);
    $("#numId").val("");
    $("#nombres").val("");
    $("#numSolicitud").val("");
    $("#fechaSolicitud").val("");
    $("#origenAtencion").val("");
    $("#ubicacion").val("");
    $("#tipoServicio").val("");
    $("#fechaIngreso").val("");
    $("#numeroIngreso").val("");
    $("#prioridad").val("");
    $("#contrato").val("");
    $("#servicio").val("");
    $("#numCama").val("");
    $("#diagPrincipal").val("");
    $("#diag1").val("");
    $("#diag2").val("");
    $("#nombreIps").val("");
    $("#direccionIps").val("");
    $("#justificacionClinica").val("");
    $("#clasificacionT").val("");
    $("#tecnologiaT").val("");
    $("#cantidadT").val("");
    $("#profesionalSalud").val("");
    $("#cargoProfesional").val("");
    $("#ordenMedica").val("");
    $("#posibleSiniestro").val("");

}

$(document).ready(function () {

    const fechaActual = moment(Date.now()).format("YYYY-MM-DDTHH:MM");
    var input = document.getElementById("fechaSolicitud");
    input.max = fechaActual;

    traerCUPS();

});