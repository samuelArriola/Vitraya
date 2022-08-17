const ejecutarAjax = (url, datos, success) => {
    return $.ajax({
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

let numeroAutorizacion;
let estadoAutorizacion;

$(document).on("click", ".btnConsultarAut", function (e) {

    numeroAutorizacion = $("#inputAutorizacion").val();

    if (numeroAutorizacion == "") {

        error("Error", "Debe ingresar un numero de autorización")

    } else {

        datos = {
            "numeroAutorizacion": numeroAutorizacion,
        }

        $("#loading-modal").modal();
        ejecutarAjax("ConsultaAutorizaciones.aspx/ConsultaAut", datos, registrarConsulta);

    }

})

function registrarConsulta(msg) {

    datos = msg.d;

    if (datos.length != 0) {

        $("#loading-modal").modal("hide");
        error("ADVERTENCIA", "El codigo " + numeroAutorizacion + " para autorizacion ya se encuentra en sistema, no debe usarlo nuevamente.");
        estadoAutorizacion = "Repetido";

    } else if (datos.length == 0) {

        $("#loading-modal").modal("hide");
        exito("NOTIFICACIÓN", "El codigo " + numeroAutorizacion + " para autorizacion no se encuentra en sistema, puede utilizarlo.");
        estadoAutorizacion = "No repetido";
    }

    datos = {
        "numeroAutorizacion": numeroAutorizacion,
        "estadoAutorizacion": estadoAutorizacion,
    }

    ejecutarAjax("ConsultaAutorizaciones.aspx/SetHistorico", datos, finProceso);

}

function finProceso() {
    $("#inputAutorizacion").val("");
}

function traerDatosHistorico() {

    ejecutarAjax("ConsultaAutorizaciones.aspx/GetHistorico", {}, llenarTablaHistorico);
}

function llenarTablaHistorico(msg) {

    datos = msg.d;

    dtHistorico = "";

    datos.forEach((item) => {
        moment.locale('es');
        dtHistorico += `

            <tr>
                
                <td>${item.IntIdUsuario}</td>
                <td>${item.StrNombreUsuario}</td>
                <td>${item.StrNumeroAut}</td>
                <td>${item.StrEstadoAut}</td>
                <td>${moment(item.DtfechaConsulta).format("LLL")}</td>

            </tr>

        `;
    })
    $("#tbHistorico").html(dtHistorico);
    DataTable("#tableDocs");
}

$(document).on("click", ".BuscarHistorico", function (e) {

    let fechaFinalF;
    let fechaInicial = $("#txtFecha1").val() + "-01";
    let fechaFinal = $("#txtFecha2").val();
    let mes = parseInt(moment(fechaFinal).format("M")) + 1;
    if (mes == 13) {
        mes = "01";
        fechaFinalF = (parseInt(moment(fechaFinal).format("YYYY")) + 1) + "-01-01";
    } else {
        fechaFinalF = moment(fechaFinal).format("YYYY") + "-" + mes + "-01";
    }

    TipoInformacion = $("#TipoI").val();

    datos = {
        "fecha1": fechaInicial,
        "fecha2": fechaFinalF,
    }

    if ($("#txtFecha1").val() == "" || fechaFinal == "") {

        error("Error", "Debe seleccionar fecha inicial y fecha final")

    }
    else {

        if (TipoInformacion == "0") {

            ejecutarAjax("ConsultaAutorizaciones.aspx/GetFiltro1", datos, actualizarTabla);

        } else if (TipoInformacion == "1") {

            ejecutarAjax("ConsultaAutorizaciones.aspx/GetFiltro2", datos, actualizarTabla);

        } else if (TipoInformacion == "2") {

            ejecutarAjax("ConsultaAutorizaciones.aspx/GetFiltro3", datos, actualizarTabla);

        }

    } 

})

$(document).on("click", ".ExportarHistorico", function (e) {

    let tabla = [];

    let header = [];

    let idTabla = "#tableDocs";

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

    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Historico Consulta Autorizaciones", ".xlsx", [], false, "hoja 1")

    exito("Notificacion", "Exportando Informacion");

})

function actualizarTabla(msg) {

    datos = msg.d;

    dtHistorico = "";

    datos.forEach((item) => {
        moment.locale('es');
        dtHistorico += `

            <tr>
                
                <td>${item.IntIdUsuario}</td>
                <td>${item.StrNombreUsuario}</td>
                <td>${item.StrNumeroAut}</td>
                <td>${item.StrEstadoAut}</td>
                <td>${moment(item.DtfechaConsulta).format("LLL")}</td>

            </tr>

        `;
    })
    $("#tbHistorico").html(dtHistorico);
    DataTable("#tableDocs");

}

$(document).ready(function () {

    traerDatosHistorico();

});