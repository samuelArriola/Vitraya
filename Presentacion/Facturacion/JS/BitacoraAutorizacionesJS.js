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

function traerDatosGeneralBitacora() {

    $("#loading-modal").modal();
    ejecutarAjax("BitacoraAutorizaciones.aspx/getBitacora", {}, llenarTablaBitacora);

}

let permiso;

function llenarTablaBitacora(msg) {

    datos = msg.d;

    dtBitacora = "";
    dtBitacora2 = "";

    datos.forEach((item) => {

        dtBitacora2 += `

            <tr>
                
                <td>${item.NumIngreso1}</td>
                <td>${item.TipoIdentificacion1}</td>
                <td>${item.Identificacion1}</td>
                <td>${item.Nombres1}</td>
                <td>${item.NumSolicitud1}</td>
                <td>${moment(item.FechaSolicitud1).format("DD/MM/YYYY HH:MM")}</td>
                <td>${item.OrigenAtencion1}</td>
                <td>${item.TipoServicio1}</td>
                <td>${item.PrioridadAtencion1}</td>
                <td>${item.UbicacionPaciente1}</td>
                <td>${moment(item.FechaIngreso1).format("DD/MM/YYYY HH:MM")}</td>
                <td>${item.ContratoPrestacion1}</td>
                <td>${item.Servicio1}</td>
                <td>${item.NumCama1}</td>
                <td>${item.DiagPrincipal1}</td>
                <td>${item.NombreIPS1}</td>
                <td>${item.DireccionIPS1}</td>
                <td>${item.ProfesionalSolicita1}</td>
                <td>${item.CargoProfesional1}</td>
                <td>${item.Estado1}</td>
                <td>${item.ClasificacionTecnologia}</td>
                <td>${item.NombreTecnologia}</td>
                <td>${item.CantidadTecnologia}</td>
                <td>${moment(item.FechaAprobacion1).format("DD/MM/YYYY HH:MM")}</td>
                <td>${item.NumAutorizacion1}</td>
                <td>${item.MotivoAnulacion1}</td>
                <td>${moment(item.FechaAnulacion1).format("DD/MM/YYYY HH:MM")}</td>
               
            </tr>

        `;

        if (item.Estado1 == "Pendiente") {

            if (permiso == 0) {

                dtBitacora += `

                    <tr>
                
                        <td>${item.NumIngreso1}</td>
                        <td>${item.Identificacion1}</td>
                        <td>${item.Nombres1}</td>
                        <td>${item.Servicio1}</td>
                        <td>${item.ContratoPrestacion1}</td>
                        <td>${item.NumSolicitud1}</td>
                        <td>${moment(item.FechaSolicitud1).format("DD/MM/YYYY")}</td>
                        <td>${item.ProfesionalSolicita1}</td>
                        <td class="table-warning">${item.Estado1}</td>
                        <td>Sin aprobación</td>
                        <td>Sin aprobación</td>
                        <td>  <button  type="button" class="btnDetalles btn btn-primary" data-id="${item.OidRegAutorizacion1}" >VER</button> </td>
                        <td>PENDIENTE</td>

                    </tr>

                `;

            } else if (permiso == 1) {

                dtBitacora += `

                    <tr>
                
                        <td>${item.NumIngreso1}</td>
                        <td>${item.Identificacion1}</td>
                        <td>${item.Nombres1}</td>
                        <td>${item.Servicio1}</td>
                        <td>${item.ContratoPrestacion1}</td>
                        <td>${item.NumSolicitud1}</td>
                        <td>${moment(item.FechaSolicitud1).format("DD/MM/YYYY")}</td>
                        <td>${item.ProfesionalSolicita1}</td>
                        <td class="table-warning">${item.Estado1}</td>
                        <td>Sin aprobación</td>
                        <td>Sin aprobación</td>
                        <td>  <button  type="button" class="btnDetalles btn btn-primary" data-id="${item.OidRegAutorizacion1}" >VER</button> </td>
                        <td class="ColumnaOpciones">
                              <button  type="button" id="btnAprobacion" class="btnAprobacion btn btn-success" data-ingreso="${item.NumIngreso1}" data-autorizacion="${item.NumAutorizacion1}" data-id="${item.OidRegAutorizacion1}" data-toggle="tooltip" data-placement="top" title="APROBAR"><i class="fa fa-check btnAprobacion" data-id="${item.OidRegAutorizacion1}" ></i></button>
                              <button  type="button" class="btnAnular btn btn-danger" data-id="${item.OidRegAutorizacion1}" data-toggle="tooltip" data-placement="top" title="ANULAR"><i class="fa fa-ban btnAnular" data-id="${item.OidRegAutorizacion1}"></i></button>
                              <button  type="button" class="btnEditar btn btn-warning" data-id="${item.OidRegAutorizacion1}" data-toggle="tooltip" data-placement="top" title="EDITAR"><i class="fa fa-pencil btnEditar" data-id="${item.OidRegAutorizacion1}"></i></button>
                        </td>

                    </tr>

                `;

            }

        } else if (item.Estado1 == "Aprobado") {

            dtBitacora += `

                <tr>
                
                    <td>${item.NumIngreso1}</td>
                    <td>${item.Identificacion1}</td>
                    <td>${item.Nombres1}</td>
                    <td>${item.Servicio1}</td>
                    <td>${item.ContratoPrestacion1}</td>
                    <td>${item.NumSolicitud1}</td>
                    <td>${moment(item.FechaSolicitud1).format("DD/MM/YYYY")}</td>
                    <td>${item.ProfesionalSolicita1}</td>
                    <td class="table-success" >${item.Estado1}</td>
                    <td>${moment(item.FechaAprobacion1).format("DD/MM/YYYY")}</td>
                    <td>${item.NumAutorizacion1}</td>
                    <td>  <button  type="button" class="btnDetalles btn btn-primary" data-id="${item.OidRegAutorizacion1}" >VER</button> </td>
                    <td>APROBADO</td>

                </tr>

            `;

        }else if (item.Estado1 == "Anulado") {

            dtBitacora += `

                <tr>
                
                    <td>${item.NumIngreso1}</td>
                    <td>${item.Identificacion1}</td>
                    <td>${item.Nombres1}</td>
                    <td>${item.Servicio1}</td>
                    <td>${item.ContratoPrestacion1}</td>
                    <td>${item.NumSolicitud1}</td>
                    <td>${moment(item.FechaSolicitud1).format("DD/MM/YYYY")}</td>
                    <td>${item.ProfesionalSolicita1}</td>
                    <td class="table-danger" >${item.Estado1}</td>
                    <td>Anulado</td>
                    <td>Anulado</td>
                    <td>  <button  type="button" class="btnDetalles btn btn-primary" data-id="${item.OidRegAutorizacion1}" >VER</button> </td>
                    <td>ANULADO</td>

                </tr>

            `;

        }

    })

    $("#tbInfo").html(dtBitacora);
    DataTable("#tableBitacora", 10);

    $("#tbInfo2").html(dtBitacora2);

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

    $("#loading-modal").modal("hide");

}

$(document).on("click", ".btnDetalles", function (e) {

    let idAut = parseInt($(e.target).attr("data-id"));

    datos = {
        "id": idAut,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetDetalles", datos, mostrarInfoDetalles);
})

let AutId;
let tipoId;
let numId;
let nombres;
let numSolicitud;
let fechaSolicitud;
let origenAtencion;
let ubicacion;
let tipoServicio;
let fechaIngreso;
let prioridad;
let contrato;
let servicio;
let numCama;
let diagPrincipal;
let diag1;
let diag2;
let nombreIps;
let direccionIps;
let clasificacionT
let tecnologiaT
let cantidadT;
let profesionalSalud;
let cargoProfesional;
let estado;
let fechaAprobacion;
let numAutorizacion;
let archivoAutorizacion
let motivoAnulacion;
let fechaAnulacion;


//function mostrarInfoTecnologiasDetalles(AutIdTecn) {

//    datos = {
//        "id": AutIdTecn,
//    }

//    ejecutarAjax("BitacoraAutorizaciones.aspx/GetDetallesTecnologias", datos, mostrarInfoTecnologiasDetalles2);

//}

//function mostrarInfoTecnologiasDetalles2(msg) {

//    datos = msg.d;

//    dtTecnoDeta = "";

//    datos.forEach((item) => {

//        dtTecnoDeta += `

//            <tr>
//                <td>${item.ClasificacionTecnologia}</td>
//                <td>${item.NombreTecnologia}</td>
//                <td>${item.CantidadTecnologia}</td>
//            </tr>

//        `;
//    })

//    $("#tbInfoTDet").html(dtTecnoDeta);
//    $("#tbInfoTDetA").html(dtTecnoDeta);

//}

function mostrarObservacionesAut(AutId) {

    datos = {
        "AutId": AutId,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetObservaciones", datos, mostrarObservacionesAut2);
}

let fechaObservacion = [];
let usuarioObservacion = [];
let descripcionObservacion = [];
let ObservacionesInforme = 0;

function mostrarObservacionesAut2(msg) {

    fechaObservacion = [];
    usuarioObservacion = [];
    descripcionObservacion = [];

    datos = msg.d;

    dtObservaciones = "";

    datos.forEach((item) => {

        fechaObservacion.push(moment(item.FechaObservacion).format("DD/MM/YYYY HH:mm"));
        usuarioObservacion.push(item.ProfesionalSolicita1);
        descripcionObservacion.push(item.DescripcionObservacion1);

        dtObservaciones += `

            <tr>
                <td>${moment(item.FechaObservacion).format("DD/MM/YYYY HH:mm")}</td>
                <td>${item.ProfesionalSolicita1}</td>
                <td>${item.DescripcionObservacion1}</td>
            </tr>

        `;
    })

    $("#dtObservaciones").html(dtObservaciones);

    if (fechaObservacion.length != 0) {

        $("#tableObservaciones").show();
        ObservacionesInforme = 1;

    };
}

function mostrarArchivosAut(AutId) {

    datos = {
        "id": AutId,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetArchivosAut", datos, mostrarArchivosAut2);

}

function mostrarArchivosAut2(msg) {

    let nombreA;
    let contenidoA;
    let extensionA;
    let archivo;

    datos = msg.d;

    if (datos.length != 0) {

        datos.forEach((detalle) => {

            nombreA = detalle.ArchivoNombre1;
            contenidoA = detalle.ArchivoContenido1;
            extensionA = detalle.ArchivoExt1;
            archivo = detalle.Archivo1;

        })

        var filename = nombreA + "." + extensionA;

        let url = "data:" + contenidoA + ";base64," + archivo;
        var documento = null;
        fetch(url)
            .then(res => res.blob())
            .then(function (blob) {
                downloadBlob(blob, filename);
            });

    } else {

        var elem = $("#enlaceOrdenM");
        elem.html(`<span>ORDEN MEDICA NO CARGADA</span>`);
    }
}

function downloadBlob(blob, filename) {
    if (window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveBlob(blob, filename);
    } else {
        var elem = $("#enlaceOrdenM");
        elem.html(`<a href="${window.URL.createObjectURL(blob)}" target="_blank" >${filename}</a>`);
        elem.href = window.URL.createObjectURL(blob);
        elem.download = filename;
        elem.click();
    }
}

function mostrarArchivosAutAprob(AutId) {

    datos = {
        "id": AutId,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetArchivosAprobAut", datos, mostrarArchivosAprob2);

}

function mostrarArchivosAprob2(msg) {

    let nombreA;
    let contenidoA;
    let extensionA;
    let archivo;

    datos = msg.d;

    if (datos.length != 0) {

        datos.forEach((detalle) => {

            nombreA = detalle.ArchivoNombre1;
            contenidoA = detalle.ArchivoContenido1;
            extensionA = detalle.ArchivoExt1;
            archivo = detalle.Archivo1;

        })

        var filename = nombreA + "." + extensionA;

        let url = "data:" + contenidoA + ";base64," + archivo;
        var documento = null;
        fetch(url)
            .then(res => res.blob())
            .then(function (blob) {
                downloadBlob2(blob, filename);
            });

    } else {

        var elem = $("#enlaceArchivoAprobar");
        elem.html(`<span>ARCHIVO NO CARGADO</span>`);

    }

}

function downloadBlob2(blob, filename) {
    if (window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveBlob(blob, filename);
    } else {
        var elem = $("#enlaceArchivoAprobar");
        elem.html(`<a href="${window.URL.createObjectURL(blob)}" target="_blank" >${filename}</a>`);
        elem.href = window.URL.createObjectURL(blob);
        elem.download = filename;
        elem.click();
    }
}

function mostrarInfoDetalles(msg) {

    detalles = msg.d;

    let textArchivoAutorizacion;

    detalles.forEach((detalle) => {
        moment.locale('es');

        AutId = detalle.OidRegAutorizacion1;
        tipoId = detalle.TipoIdentificacion1;
        numId = detalle.Identificacion1;
        nombres = detalle.Nombres1;
        numSolicitud = detalle.NumSolicitud1;
        fechaSolicitud = moment(detalle.FechaSolicitud1).format('LLLL');
        origenAtencion = detalle.OrigenAtencion1;
        ubicacion = detalle.UbicacionPaciente1;
        tipoServicio = detalle.TipoServicio1;
        fechaIngreso = moment(detalle.FechaIngreso1).format('LLLL');
        NumIngreso = detalle.NumIngreso1;
        prioridad = detalle.PrioridadAtencion1;
        contrato = detalle.ContratoPrestacion1;
        servicio = detalle.Servicio1;
        numCama = detalle.NumCama1;
        diagPrincipal = detalle.DiagPrincipal1;
        diag1 = detalle.DiagRel11;
        diag2 = detalle.DiagRel21;
        nombreIps = detalle.NombreIPS1;
        direccionIps = detalle.DireccionIPS1;
        clasificacionT = detalle.ClasificacionTecnologia;
        tecnologiaT = detalle.NombreTecnologia;
        cantidadT = detalle.CantidadTecnologia;
        JustificacionClinica = detalle.JustificacionClinica1;
        profesionalSalud = detalle.ProfesionalSolicita1;
        cargoProfesional = detalle.CargoProfesional1;
        estado = detalle.Estado1;
        motivoAnulacion = detalle.MotivoAnulacion1;
        fechaAnulacion = moment(detalle.FechaAnulacion1).format('LLLL');

        if (estado == "Pendiente") {

            fechaAprobacion = "SIN RESPUESTA";
            numAutorizacion = "SIN RESPUESTA";
            textArchivoAutorizacion = "SIN RESPUESTA";
            motivoAnulacion = "SIN RESPUESTA";
            fechaAnulacion = "SIN RESPUESTA";

        } else if (estado == "Aprobado") {

            fechaAprobacion = moment(detalle.FechaAprobacion1).format('LLLL');
            numAutorizacion = detalle.NumAutorizacion1;
            mostrarArchivosAutAprob(AutId);
            motivoAnulacion = "NO ANULADA";
            fechaAnulacion = "NO ANULADA";

        } else if (estado == "Anulado") {

            fechaAprobacion = "SOLICITUD ANULADA";
            numAutorizacion = "SOLICITUD ANULADA";
            textArchivoAutorizacion = "SOLICITUD ANULADA";
        }

    })

    //mostrarInfoTecnologiasDetalles(AutId);
    mostrarObservacionesAut(AutId);
    mostrarArchivosAut(AutId);

    tbRegistro = `<table class="table-inf" id="ejemplotablabitacora">
        <tr>
            <th class="text-center" colspan="2">Información del Paciente</th>
        </tr>
        <tr>
            <td>Tipo de Identificación</td>
            <td>${tipoId}</td>
        <tr>
            <td>Numero de Identificación</td>
            <td>${numId}</td>
        </tr>
        <tr>
            <td>Nombre del paciente</td>
            <td>${nombres}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Solicitud de Autorización</th>
        </tr>
        <tr>
            <td>Numero de la solicitud</td>
            <td>${numSolicitud}</td>
        <tr>
            <td>Fecha y Hora de Radicación</td>
            <td>${fechaSolicitud}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Información de la atención</th>
        </tr>
        <tr>
            <td>Origen Atención</td>
            <td>${origenAtencion}</td>
        </tr>
        <tr>
            <td>Tipo de Servicios Solicitados</td>
            <td>${tipoServicio}</td>
        </tr>
        <tr>
            <td>Prioridad Atención</td>
            <td>${prioridad}</td>
        <tr>
            <td>Ubicacion Paciente</td>
            <td>${ubicacion}</td>
        </tr>
        <tr>
            <td>Numero de Ingreso</td>
            <td>${NumIngreso}</td>
        </tr>
        <tr>
            <td>Fecha y Hora de Ingreso</td>
            <td>${fechaIngreso}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Información de Servicios Solicitados</th>
        </tr>
        <tr>
            <td>Contrato Prestación</td>
            <td>${contrato}</td>
        <tr>
            <td>Servicio</td>
            <td>${servicio}</td>
        </tr>
        <tr>
            <td>Numero de cama</td>
            <td>${numCama}</td>
        </tr>
        <tr>
            <td>Diagnóstico Principal</td>
            <td>${diagPrincipal}</td>
        </tr>
        <tr>
            <td>Diagnóstico Rel 1</td>
            <td>${diag1}</td>
        </tr>
        <tr>
            <td>Diagnóstico Rel 2</td>
            <td>${diag2}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Nit de la IPS Solicitante</th>
        </tr>
        <tr>
            <td>Nombre de IPS</td>
            <td>${nombreIps}</td>
        <tr>
            <td>Dirección IPS</td>
            <td>${direccionIps}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Justificación Clínica</th>
        </tr>
        <tr>
            <td>Justificación Clínica</td>
            <td>${JustificacionClinica}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Tecnologías en Salud</th>
        </tr>
        <tr>
             <td>Clasificacion Tecnologia</td>
             <td>${clasificacionT}</td>
        </tr>
        <tr>
             <td>Codigo y Nombre Tecnologia</td>
             <td>${tecnologiaT}</td>
        </tr>
        <tr>
             <td>Cantidad</td>
             <td>${cantidadT}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Información del Usuario Solicitante</th>
        </tr>
        <tr>
            <td>Nombre Profesional de la Salud</td>
            <td>${profesionalSalud}</td>
        </tr>
        <tr>
            <td>Cargo</td>
            <td>${cargoProfesional}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Soportes de la Solicitud</th>
        </tr>
        <tr>
            <td>Orden Médica</td>
            <td><a id="enlaceOrdenM"></a></td>
        </tr>
        <tr>
            <td>Ocurrencia Posible Siniestro</td>
            <td></td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Información de Respuesta</th>
        </tr>
        <tr>
            <td>Estado</td>
            <td>${estado}</td>
        </tr>
        <tr>
            <td>Fecha de Aprobación</td>
            <td>${fechaAprobacion}</td>
        </tr>
        <tr>
            <td>Numero de Autorización</td>
            <td>${numAutorizacion}</td>
        </tr>
        <tr>
            <td>Archivo de Autorización</td>
            <td><a id="enlaceArchivoAprobar">${textArchivoAutorizacion}</a></td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Gestion de autorización</th>
        </tr>
        <tr>
            <td>Observación</td>
            <td><textarea class="form-control" id="inputObservacion" ></textarea></td>
        <tr>
        </table>
        <div class="text-center">
            <div class="form-group">
                <button style="margin-top: 24px;" type="button" data-id="${AutId}" id="btnGuardarObservacion" class="btnGuardarObservacion btn btn-primary">CARGAR OBSERVACION</button>
            </div>
        </div>

        <table style="display: none;" class="tableObservaciones table" id="tableObservaciones">
        <thead>
            <tr>
                <th>Fecha</th>
                <th>Usuario</th>
                <th>Observacion</th>
            </tr>
        </thead>
            <tbody id="dtObservaciones">

            </tbody>
        </table>

        <div class="text-center">
            <div class="form-group">
                <button style="margin-top: 24px;" type="button" data-id="${AutId}" id="btnImprimir" class="btnImprimir btn btn-primary">IMPRIMIR INFORMACION DE LA AUTORIZACION</button>
            </div>
        </div>
    `;

    if (estado == "Anulado") {

        tbAnulacion = `<table class="table-inf">
        <tr>
            <th class="text-center" colspan="2">Información de la anulación de la solicitud de autorizacion</th>
        </tr>
        <tr>
            <td>Fecha de Anulación</td>
            <td>${fechaAnulacion}</td>
        <tr>
            <td>Motivo</td>
            <td>${motivoAnulacion}</td>
        </tr>
        </table>   
        `;

        tbRegistro = tbRegistro + tbAnulacion;

    }

    $("#modalBody1").html(tbRegistro);
    $("#modal1").modal();
}

$(document).on("click", ".btnAprobacion", function (e) {

    let idAut = parseInt($(e.target).attr("data-id"));

    datos = {
        "id": idAut,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetDetalles", datos, mostrarInfoAprob);

})



function mostrarInfoAprob(msg) {

    detalles = msg.d;

    detalles.forEach((detalle) => {

        moment.locale('es');

        AutId = detalle.OidRegAutorizacion1;
        numSolicitud = detalle.NumSolicitud1;
        fechaSolicitud = moment(detalle.FechaSolicitud1).format('LLLL');
        NumIngreso = detalle.NumIngreso1;
        estado = detalle.Estado1;
        clasificacionT = detalle.ClasificacionTecnologia;
        tecnologiaT = detalle.NombreTecnologia;
        cantidadT = detalle.CantidadTecnologia;

    })

    //mostrarInfoTecnologiasDetalles(AutId);

    tbRegistro = `<table class="table-inf">
        <tr>
            <th class="text-center" colspan="2">Información de Solicitud Autorización</th>
        </tr>
        <tr>
            <td>Numero de Solicitud</td>
            <td>${numSolicitud}</td>
        <tr>
            <td>Fecha de la solicitud</td>
            <td>${fechaSolicitud}</td>
        </tr>
        <tr>
            <td>Estado de la Solicitud</td>
            <td>${estado}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Tecnologías en Salud</th>
        </tr>
        <tr>
             <td>Clasificacion Tecnologia</td>
             <td>${clasificacionT}</td>
        </tr>
        <tr>
             <td>Codigo y Nombre Tecnologia</td>
             <td>${tecnologiaT}</td>
        </tr>
        <tr>
             <td>Cantidad</td>
             <td>${cantidadT}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Cargar Información de Respuesta</th>
        </tr>
        <tr>
            <td>Fecha y Hora de Aprobación</td>
            <td><input type="datetime-local" class="form-control" id="fechaAprobacion" /></td>
        <tr>
            <td>Numero de Autorización</td>
            <td><input type="number" class="form-control" id="numAutorizacion" /></td>
        </tr>
        <tr>
            <td>Archivo de Autorización</td>
            <td><input type="file" class="form-control" id="ArchivoAutorizacion" /></td>
        </tr>
    </table>
    <div class="text-center">
        <div class="form-group">
            <button style="margin-top: 27px;" type="button" data-ingreso="${NumIngreso}" data-id="${AutId}" id="btnGuardarAprobacion" class="btnGuardarAprobacion btn btn-primary">CARGAR</button>
        </div>
    </div>
    `;

    $("#modalBody2").html(tbRegistro);
    $("#modal2").modal();

    const fechaActual = moment(Date.now()).format("YYYY-MM-DDTHH:MM");
    var inputFechaAprob = document.getElementById("fechaAprobacion");
    inputFechaAprob.max = fechaActual;
}

let idAutRepe;

$(document).on("click", ".btnGuardarAprobacion", function (e) {

    idAutorizacion = parseInt($("#btnGuardarAprobacion").attr("data-id"));
    NumIngreso = parseInt($(e.target).attr("data-ingreso"));
    fechaAprobacion = $("#fechaAprobacion").val();
    numAutorizacion = String($("#numAutorizacion").val());
    archivoAutorizacion = $("#ArchivoAutorizacion").val();

    if (fechaAprobacion == "" || numAutorizacion == "") {

        error("Error", "Debe diligenciar todos los campos para cargar aprobación de autorización")

    } else {

        datos = {
            "numIngreso": NumIngreso,
            "numAutorizacion": numAutorizacion
        }

        ejecutarAjax("BitacoraAutorizaciones.aspx/GetValidacionRep", datos, traerValidacionParaAprobar);

    }
})

function traerValidacionParaAprobar(msg) {

    detalles = msg.d;
    let resultadoValidacion;

    detalles.forEach((detalle) => {

        if (detalle.ResultadoAutRepetida > 0) {
            resultadoValidacion = "Repetida";
        } else if (detalle.ResultadoAutRepetida == 0){
            resultadoValidacion = "No repetida";
        }

    })

    if (resultadoValidacion == "No repetida") {

        datos = {
            "idAutorizacion": idAutorizacion,
            "fechaAprobacion": fechaAprobacion,
            "numAutorizacion": numAutorizacion
        }

        ejecutarAjax("BitacoraAutorizaciones.aspx/SetAprobAut", datos);

        if ($("#ArchivoAutorizacion").val() != "") {
            ObtenerArchivoAprob(idAutorizacion);
        }

        aprobacionExitosa();

    } else {

        error("Alerta", "El numero de autorizacion ya se encuentra en sistema y no se puede repetir")
        $("#numAutorizacion").val(""); 
    }

}

function aprobacionExitosa() {

    traerDatosGeneralBitacora();
    $('#modal2').modal('toggle')
    exito("Notificacion", "Cargue de respuesta a solicitud de autorización exitoso");
}

function ObtenerArchivoAprob(OidRegAutorizacion) {

    var Contenido;
    var Extension;

    var file = $('#ArchivoAutorizacion').prop('files');

    let nombre = file[0].name;

    let nombres = nombre.split(".");

    nombre = nombre.replace(`.${nombres[nombres.length - 1]}`, "");

    console.log(nombre);

    Contenido = file[0].type;

    console.log(Contenido);

    Extension = nombres[nombres.length - 1];

    console.log(Extension);

    var $i = $('#ArchivoAutorizacion'), // Put file input ID here
        input = $i[0]; // Getting the element from jQuery

    file = input.files[0]; // The file
    fr = new FileReader(); // FileReader instance
    fr.onload = function () {
        var codificado = fr.result;
        codificado2 = codificado.replace("data:" + Contenido + ";base64,", "")

        setArchivoAutorizacionA(nombre, codificado2, Contenido, Extension, OidRegAutorizacion)

    };
    fr.readAsDataURL(file);

}

function setArchivoAutorizacionA(Nombre, Archivo, Contenido, Extension, OidRegAutorizacion) {

    datos = {
        "Nombre": Nombre,
        "Archivo": Archivo,
        "Contenido": Contenido,
        "Extension": Extension,
        "OidRegAutorizacion": OidRegAutorizacion
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/InsertarDocumentoAprobacionAut", datos)

}

$(document).on("click", ".btnAnular", function (e) {

    let idAut = parseInt($(e.target).attr("data-id"));
    let NumIngreso = $(e.target).attr("data-ingreso");

    datos = {
        "id": idAut,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetDetalles", datos, mostrarAnularAut);
    
})

function mostrarAnularAut(msg) {

    detalles = msg.d;

    detalles.forEach((detalle) => {

        moment.locale('es');

        AutId = detalle.OidRegAutorizacion1;
        numSolicitud = detalle.NumSolicitud1;
        fechaSolicitud = moment(detalle.FechaSolicitud1).format('LLLL');
        estado = detalle.Estado1;
        clasificacionT = detalle.ClasificacionTecnologia;
        tecnologiaT = detalle.NombreTecnologia;
        cantidadT = detalle.CantidadTecnologia;

    })

    //mostrarInfoTecnologiasDetalles(AutId);

    tbRegistro = `<table class="table-inf">
        <tr>
            <th class="text-center" colspan="2">Información de Solicitud Autorización</th>
        </tr>
        <tr>
            <td>Numero de Solicitud</td>
            <td>${numSolicitud}</td>
        <tr>
            <td>Fecha de la solicitud</td>
            <td>${fechaSolicitud}</td>
        </tr>
        <tr>
            <td>Estado de la Solicitud</td>
            <td>${estado}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Tecnologías en Salud</th>
        </tr>
        <tr>
             <td>Clasificacion Tecnologia</td>
             <td>${clasificacionT}</td>
        </tr>
        <tr>
             <td>Codigo y Nombre Tecnologia</td>
             <td>${tecnologiaT}</td>
        </tr>
        <tr>
             <td>Cantidad</td>
             <td>${cantidadT}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Cargar Información o Motivo de Anulación</th>
        </tr>
        <tr>
            <td>Fecha y Hora de Anulación</td>
            <td><input type="datetime-local" class="form-control" id="fechaAnulacion" /></td>
        <tr>
        <tr>
            <td>Describa motivo</td>
            <td><textarea class="form-control" id="motivoAnulacion" ></textarea></td>
        <tr>
    </table>
    <div class="text-center">
        <div class="form-group">
            <button style="margin-top: 20px;" type="button" data-id="${AutId}" id="btnGuardarAnulacion" class="btnGuardarAnulacion btn btn-primary">ANULAR</button>
        </div>
    </div>
    `;

    $("#modalBody3").html(tbRegistro);
    $("#modal3").modal();

    const fechaActual = moment(Date.now()).format("YYYY-MM-DDTHH:MM");
    var inputFAnulacion = document.getElementById("fechaAnulacion");
    inputFAnulacion.max = fechaActual;
}

$(document).on("click", ".btnGuardarAnulacion", function (e) {

    idAutorizacion = parseInt($("#btnGuardarAnulacion").attr("data-id"));
    fechaAnulacion = $("#fechaAnulacion").val();
    motivoAnulacion = $("#motivoAnulacion").val();

    if (fechaAnulacion == "" || motivoAnulacion == "") {

        error("Error", "Debe diligenciar todos los campos para cargar anulacion de autorización")

    } else {

        datos = {
            "idAutorizacion": idAutorizacion,
            "fechaAnulacion": fechaAnulacion,
            "motivoAnulacion": motivoAnulacion
        }
        ejecutarAjax("BitacoraAutorizaciones.aspx/SetAnularAut", datos, anulacionExitosa);

    }
})

function anulacionExitosa() {

    traerDatosGeneralBitacora();
    $('#modal3').modal('toggle')
    exito("Notificacion", "Solicitud de autorizacion anulada exitosamente");
}

let idAutParaObservaciones;

//AGREGAR UNA OBSERVACION A UNA AUTORIZACION PENDIENTE
$(document).on("click", ".btnGuardarObservacion", function (e) {

    let idAut = parseInt($(e.target).attr("data-id"));
    let observacion = $("#inputObservacion").val();
    idAutParaObservaciones = idAut;

    datos = {
        "idAutorizacion": idAut,
        "observacion": observacion
    }

    if (observacion == "") {

        error("Error", "El campo 'Observaciones' debe estar diligenciado")

    } else {


        ejecutarAjax("BitacoraAutorizaciones.aspx/SetObservacion", datos, observacionGuardada);

    }

    
})

function observacionGuardada() {

    exito("Notificacion", "Observacion Añadida Satisfactoriamente");
    $("#inputObservacion").val("");
    mostrarObservacionesAut(idAutParaObservaciones);
    $("#tableObservaciones").show();
}

//EXPORTAR INFORMACION DE LA BITACORA
$(document).on("click", ".btnExportarInfo", function (e) {

    let tabla = [];

    let header = [];

    let idTabla = "#tableBitacora2";

    document.querySelectorAll(idTabla + " th").forEach(head => {
        header.push({ v: head.innerText, t: "s" })
    })

    tabla.push(header);

    document.querySelectorAll(idTabla + " tr").forEach(row => {
        let fila = [];
        row.querySelectorAll("td").forEach(celda => {
            fila.push({ v: celda.innerText, t: "s" })
        })
        tabla.push(fila);
    })

    tableExport = new TableExport(document.createElement("table"), {});

    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BITACORA", ".xlsx", [], false, "hoja 1")

    exito("Notificacion", "Exportando Informacion");

})

//EDITAR INFORMACION DE UNA AUTORIZACION
$(document).on("click", ".btnEditar", function (e) {

    let idAut = parseInt($(e.target).attr("data-id"));

    datos = {
        "id": idAut,
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetDetalles", datos, mostrarInfoDetallesEditar);
})

function mostrarInfoDetallesEditar(msg) {

    detalles = msg.d;

    let textArchivoAutorizacion;

    detalles.forEach((detalle) => {
        moment.locale('es');

        AutId = detalle.OidRegAutorizacion1;
        tipoId = detalle.TipoIdentificacion1;
        numId = detalle.Identificacion1;
        nombres = detalle.Nombres1;
        numSolicitud = detalle.NumSolicitud1;
        fechaSolicitud = moment(detalle.FechaSolicitud1).format('YYYY-MM-DDTHH:MM');
        origenAtencion = detalle.OrigenAtencion1;
        ubicacion = detalle.UbicacionPaciente1;
        tipoServicio = detalle.TipoServicio1;
        fechaIngreso = moment(detalle.FechaIngreso1).format('YYYY-MM-DDTHH:MM');
        NumIngreso = detalle.NumIngreso1;
        prioridad = detalle.PrioridadAtencion1;
        contrato = detalle.ContratoPrestacion1;
        servicio = detalle.Servicio1;
        numCama = detalle.NumCama1;
        diagPrincipal = detalle.DiagPrincipal1;
        diag1 = detalle.DiagRel11;
        diag2 = detalle.DiagRel21;
        nombreIps = detalle.NombreIPS1;
        direccionIps = detalle.DireccionIPS1;
        clasificacionT = detalle.ClasificacionTecnologia;
        tecnologiaT = detalle.NombreTecnologia;
        cantidadT = detalle.CantidadTecnologia;
        JustificacionClinica = detalle.JustificacionClinica1;
        profesionalSalud = detalle.ProfesionalSolicita1;
        cargoProfesional = detalle.CargoProfesional1;
        estado = detalle.Estado1;
        motivoAnulacion = detalle.MotivoAnulacion1;
        fechaAnulacion = moment(detalle.FechaAnulacion1).format('LLLL');

    })

    $("#EidAut").val(AutId);
    $("#EtipoId").val(tipoId);
    $("#EnumId").val(numId);
    $("#Enombres").val(nombres);
    $("#EnumSolicitud").val(numSolicitud);
    $("#EfechaSolicitud").val(fechaSolicitud);
    $("#EubicacionPaciente").val(ubicacion);
    $("#EnumeroIngreso").val(NumIngreso);
    $("#EfechaIngreso").val(fechaIngreso);
    $("#Eservicio").val(servicio);
    $("#EnumCama").val(numCama);
    $("#EdiagPrincipal").val(diagPrincipal);
    $("#EclasificacionT").val(clasificacionT);
    $("#EtecnologiaT").val(tecnologiaT);
    $("#EcantidadT").val(cantidadT);

    $("#modal4").modal();
    
}

$(document).on("click", ".btnActualizar", function (e) {

    datos = {

        "AutId": $("#EidAut").val(),
        "tipoId": $("#EtipoId").val(),
        "numId" : $("#EnumId").val(),
        "nombres" : $("#Enombres").val(),
        "numSolicitud" : $("#EnumSolicitud").val(),
        "fechaSolicitud" : $("#EfechaSolicitud").val(),
        "ubicacion" : $("#EubicacionPaciente").val(),
        "NumIngreso" : $("#EnumeroIngreso").val(),
        "fechaIngreso" : $("#EfechaIngreso").val(),
        "servicio" : $("#Eservicio").val(),
        "numCama" : $("#EnumCama").val(),
        "diagPrincipal" : $("#EdiagPrincipal").val(),
        "clasificacionT" : $("#EclasificacionT").val(),
        "tecnologiaT" : $("#EtecnologiaT").val(),
        "cantidadT" : $("#EcantidadT").val()
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/UpdateInformacion", datos, ActualizacionExitosa);

})

function ActualizacionExitosa() {

    traerDatosGeneralBitacora();
    $('#modal4').modal('toggle')
    exito("Notificacion", "Solicitud de autorizacion editada exitosamente");
}

//FILTROS

//CAMPOS DE TEXTO

let filtroNumId = document.getElementById("filtroNumId");
filtroNumId.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {
        ejecutarFiltro1();
    }

});

let filtroNomPacien = document.getElementById("filtroNomPacien");
filtroNomPacien.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {
        ejecutarFiltro1();
    }

});

let filtroNumSolic = document.getElementById("filtroNumSolic");
filtroNumSolic.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {
        ejecutarFiltro1();
    }

});

let filtroNumingreso = document.getElementById("filtroNumIngreso");
filtroNumingreso.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {
        ejecutarFiltro1();
    }

});

let filtroNumAut = document.getElementById("filtroNumAut");
filtroNumAut.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {
        ejecutarFiltro1();
    }

});

let filtroEstAut = document.getElementById("filtroEstAut");
filtroEstAut.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {
        ejecutarFiltro1();
    }

});

function ejecutarFiltro1() {

    let numId = String($("#filtroNumId").val());
    let NomPacien = $("#filtroNomPacien").val();
    let NumSolic = String($("#filtroNumSolic").val());
    let NumIngreso = $("#filtroNumIngreso").val();
    let NumAut = String($("#filtroNumAut").val());
    let EstAut = $("#filtroEstAut").val();

    if (numId == "" && NomPacien == "" && NumSolic == "" && NumIngreso == "" && NumAut == "" && EstAut == "") {

        traerDatosGeneralBitacora();

    } else {

        datos = {
            "numId": numId,
            "NomPacien": NomPacien,
            "NumSolic": NumSolic,
            "NumIngreso": NumIngreso,
            "NumAut": NumAut,
            "EstAut": EstAut
        }

        ejecutarAjax("BitacoraAutorizaciones.aspx/obtenerinfoFiltro", datos , llenarTablaBitacora)

    }
}

//FECHAS
//FILTRO POR FECHAS DE SOLICITUDES
$(document).on("click", ".btnFiltroSolic", function (e) {

    ejecutarFiltro2();
})

function ejecutarFiltro2() {

    let fechIniSolic = $("#fechIniSolic").val();
    let fechFinSolic = $("#fechFinSolic").val();

    if (fechIniSolic == "" && fechFinSolic == "" ) {

        traerDatosGeneralBitacora();

    } else if (fechIniSolic == "" || fechFinSolic == "") {

        error("Error", "Debe diligenciar fecha inicial y fecha final de busqueda");

    } else {

        datos = {
            "fechaI": fechIniSolic,
            "fechaF": fechFinSolic
        }

        ejecutarAjax("BitacoraAutorizaciones.aspx/obtenerinfoFiltro2", datos, llenarTablaBitacora)

    }

}

//FILTRO POR FECHAS DE APROBACIONES DE AUTORIZACIONES
$(document).on("click", ".btnFiltroAprob", function (e) {

    ejecutarFiltro3();
})

function ejecutarFiltro3() {

    let fechIniAprob = $("#fechIniAprob").val();
    let fechFinAprob = $("#fechFinAprob").val();

    if (fechIniAprob == "" && fechFinAprob == "") {

        traerDatosGeneralBitacora();

    } else if (fechIniAprob == "" || fechFinAprob == "") {

        error("Error", "Debe diligenciar fecha inicial y fecha final de busqueda");

    } else {

        datos = {
            "fechaI": fechIniAprob,
            "fechaF": fechFinAprob
        }

        ejecutarAjax("BitacoraAutorizaciones.aspx/obtenerinfoFiltro3", datos, llenarTablaBitacora)

    }

}

function generarPDF() {

    var doc = new jsPDF();
    doc.fromHTML($('#ejemplotablabitacora').html(), 15, 15, {
        'width': 170, 'elementHandlers': specialElementHandlers
    });
    doc.save('sample-file.pdf');

}

$(document).on("click", ".btnImprimir", function (e) {

    var data2 = [];

    var pdf = new jsPDF('p', 'pt');
    pdf.text(20, 20, "Informacion General de Autorizaciones");

    var columns = ["Item", "Descripción"];
    var data = [
        ["Tipo de Identificación", tipoId],
        ["Numero de Identificación", numId],
        ["Nombre del paciente", nombres],

        ["Numero de la solicitud", numSolicitud],
        ["Fecha y Hora de Radicación", fechaSolicitud],

        ["Origen Atención", origenAtencion],
        ["Tipo de Servicios Solicitados", tipoServicio],
        ["Prioridad Atención", prioridad],
        ["Ubicacion Paciente", ubicacion],
        ["Numero de Ingreso", NumIngreso],
        ["Fecha y Hora de Ingreso", fechaIngreso],

        ["Contrato Prestación", contrato],
        ["Servicio", servicio],
        ["Numero de cama", numCama],
        ["Diagnóstico Principal", diagPrincipal],

        ["Nombre de IPS", nombreIps],
        ["Dirección IPS", direccionIps],

        ["Justificación Clínica", JustificacionClinica],

        ["Clasificacion Tecnologia", clasificacionT],
        ["Codigo y Nombre Tecnologia", tecnologiaT],
        ["Cantidad", cantidadT],

        ["Nombre Profesional de la Salud", profesionalSalud],
        ["Cargo", cargoProfesional],

        ["Estado", estado],

        ["Fecha de Aprobación", fechaAprobacion],
        ["Numero de Autorización", numAutorizacion],

        ["Fecha de Anulación", fechaAnulacion],
        ["Motivo de Anulación", motivoAnulacion],

    ];

    pdf.autoTable(columns, data,
        { margin: { top: 25 } }
    );

    /*if (ObservacionesInforme == "Aparece") {*/
    if (fechaObservacion.length != 0) {
        
        var columns2 = ["Fecha", "Usuario", "Observacion"];

        data2 = [];

        fechaObservacion.forEach((item, i) => {
            data2.push(
                [
                    item,
                    usuarioObservacion[i],
                    descripcionObservacion[i]
                ]
            )
        })

        pdf.autoTable(columns2, data2,
            { margin: { top: 25 } }
        );

    }

    //pdf.autoTable({ html: '#ejemplotablabitacora', headStyles: { valign: 'top', halign: 'center' }, styles: { lineColor: [44, 62, 80], lineWidth: 1 }, pageBreak: 'avoid' });

    fechaObservacion = [];
    usuarioObservacion = [];
    descripcionObservacion = [];

    //pdf.save('mipdf.pdf');
    window.open(pdf.output('bloburl', 'CERTIFICADOLABORAL'), '', `width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no, directories=no, status=no, menubar=no`);

})

//VALIDAR PERMISOS Y VISUALIZACIONES
function PermisosUsuario() {

    let linkOpcion = "../Facturacion/BitacoraAutorizaciones.aspx";

    datos = {
        "linkOpcion": linkOpcion
    }

    ejecutarAjax("BitacoraAutorizaciones.aspx/GetPermisos", datos, ValidarPermisos);

}

function ValidarPermisos(msg) {

    let datos = msg.d;

    if (datos.BlnConfirmar == false || datos.BlnCrear == false || datos.BlnEliminar == false || datos.BlnModificar == false) {

        permiso = 0;

    }
    if (datos.BlnConfirmar == true && datos.BlnCrear == true && datos.BlnEliminar == true && datos.BlnModificar == true) {

        permiso = 1;

    }


    //traerDatosGeneralBitacora();

}

function traerCUPS() {

    ejecutarAjax("BitacoraAutorizaciones.aspx/getCUPS", {}, llenarCUPS);
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

$(document).ready(function () {

    PermisosUsuario();
    traerDatosGeneralBitacora();
    traerCUPS();
    $("#tableBitacora2").hide();

});