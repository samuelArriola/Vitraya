

$(document).on("click", ".BuscarReporte", function (e) {

    let fechaInicial = $("#txtFecha1").val();
    let fechaFinal = $("#txtFecha2").val();

    datos = {
        "fechaI": fechaInicial,
        "fechaF": fechaFinal
    }

    ejecutarajax("ReportesCovid.aspx/getFiltro", datos, CargarTablaFiltro1);

})

function CargarTablaFiltro1(msg) {

    datos = msg.d;

    dtReporte = "";

    datos.forEach((item) => {

        if (item.StrAdinamia == "SI" || item.StrTemperatura == "SI" || item.StrTos == "SI" || item.StrDifiRespiratoria == "SI" ||
            item.StrOdinofagia == "SI" || item.StrDLumbar == "SI" || item.StrDtoracico == "SI" || item.StrMalestarG == "SI" ||
            item.StrPerdOlfato == "SI" || item.StrPerdGusto == "SI" || item.StrContactoCon == "SI") {

            dtReporte += `

                <tr class="table-danger">
                
                    <td>${item.StrIdentificacion}</td>
                    <td>${item.StrNombres}</td>
                    <td>${item.StrTelefonoPersonal}</td>
                    <td>${item.StrEps}</td>
                    <td>${item.StrCargo}</td>
                    <td>${item.StrUnidad}</td>
                    <td>${item.StrAdinamia}</td>
                    <td>${item.StrTemperatura}</td>
                    <td>${item.StrVtemperatura}</td>
                    <td>${item.StrTos}</td>
                    <td>${item.StrDifiRespiratoria}</td>
                    <td>${item.StrOdinofagia}</td>
                    <td>${item.StrDLumbar}</td>
                    <td>${item.StrDtoracico}</td>
                    <td>${item.StrMalestarG}</td>
                    <td>${item.StrPerdOlfato}</td>
                    <td>${item.StrPerdGusto}</td>
                    <td>${item.StrContactoCon}</td>
                    <td>${item.StrNomPersona}</td>
                    <td>${item.StrIdePersona}</td>
                    <td>${item.StrTipoCaso}</td>
                    <td>${item.StrElementoBio}</td>
                    <td>${item.StrFechaDiaria}</td>

                </tr>

            `;

        } else {

            dtReporte += `

                <tr>
                
                    <td>${item.StrIdentificacion}</td>
                    <td>${item.StrNombres}</td>
                    <td>${item.StrTelefonoPersonal}</td>
                    <td>${item.StrEps}</td>
                    <td>${item.StrCargo}</td>
                    <td>${item.StrUnidad}</td>
                    <td>${item.StrAdinamia}</td>
                    <td>${item.StrTemperatura}</td>
                    <td>${item.StrVtemperatura}</td>
                    <td>${item.StrTos}</td>
                    <td>${item.StrDifiRespiratoria}</td>
                    <td>${item.StrOdinofagia}</td>
                    <td>${item.StrDLumbar}</td>
                    <td>${item.StrDtoracico}</td>
                    <td>${item.StrMalestarG}</td>
                    <td>${item.StrPerdOlfato}</td>
                    <td>${item.StrPerdGusto}</td>
                    <td>${item.StrContactoCon}</td>
                    <td>${item.StrNomPersona}</td>
                    <td>${item.StrIdePersona}</td>
                    <td>${item.StrTipoCaso}</td>
                    <td>${item.StrElementoBio}</td>
                    <td>${item.StrFechaDiaria}</td>

                </tr>

            `;

        }
    })
    $("#tbReporte").html(dtReporte);

    DataTable("#tableDocs");

    exito("Notificacion","Busqueda realizada con exito");

}

function traerDatosReporte() {

    ejecutarajax("ReportesCovid.aspx/getReporte", {}, llenarTablaReporte);

}

function llenarTablaReporte(msg) {

    datos = msg.d;

    dtReporte = "";

    datos.forEach((item) => {

        if (item.StrAdinamia == "SI" || item.StrTemperatura == "SI" || item.StrTos == "SI" || item.StrDifiRespiratoria == "SI" ||
            item.StrOdinofagia == "SI" || item.StrDLumbar == "SI" || item.StrDtoracico == "SI" || item.StrMalestarG == "SI" ||
            item.StrPerdOlfato == "SI" || item.StrPerdGusto == "SI" || item.StrContactoCon == "SI") {

            dtReporte += `

                <tr class="table-danger">
                
                    <td>${item.StrIdentificacion}</td>
                    <td>${item.StrNombres}</td>
                    <td>${item.StrTelefonoPersonal}</td>
                    <td>${item.StrEps}</td>
                    <td>${item.StrCargo}</td>
                    <td>${item.StrUnidad}</td>
                    <td>${item.StrAdinamia}</td>
                    <td>${item.StrTemperatura}</td>
                    <td>${item.StrVtemperatura}</td>
                    <td>${item.StrTos}</td>
                    <td>${item.StrDifiRespiratoria}</td>
                    <td>${item.StrOdinofagia}</td>
                    <td>${item.StrDLumbar}</td>
                    <td>${item.StrDtoracico}</td>
                    <td>${item.StrMalestarG}</td>
                    <td>${item.StrPerdOlfato}</td>
                    <td>${item.StrPerdGusto}</td>
                    <td>${item.StrContactoCon}</td>
                    <td>${item.StrNomPersona}</td>
                    <td>${item.StrIdePersona}</td>
                    <td>${item.StrTipoCaso}</td>
                    <td>${item.StrElementoBio}</td>
                    <td>${item.StrFechaDiaria}</td>

                </tr>

            `;

        } else {

                dtReporte += `

                <tr>
                
                    <td>${item.StrIdentificacion}</td>
                    <td>${item.StrNombres}</td>
                    <td>${item.StrTelefonoPersonal}</td>
                    <td>${item.StrEps}</td>
                    <td>${item.StrCargo}</td>
                    <td>${item.StrUnidad}</td>
                    <td>${item.StrAdinamia}</td>
                    <td>${item.StrTemperatura}</td>
                    <td>${item.StrVtemperatura}</td>
                    <td>${item.StrTos}</td>
                    <td>${item.StrDifiRespiratoria}</td>
                    <td>${item.StrOdinofagia}</td>
                    <td>${item.StrDLumbar}</td>
                    <td>${item.StrDtoracico}</td>
                    <td>${item.StrMalestarG}</td>
                    <td>${item.StrPerdOlfato}</td>
                    <td>${item.StrPerdGusto}</td>
                    <td>${item.StrContactoCon}</td>
                    <td>${item.StrNomPersona}</td>
                    <td>${item.StrIdePersona}</td>
                    <td>${item.StrTipoCaso}</td>
                    <td>${item.StrElementoBio}</td>
                    <td>${item.StrFechaDiaria}</td>

                </tr>

            `;

        }

    })

    $("#tbReporte").html(dtReporte);

    DataTable("#tableDocs");

}

$(document).on("click", ".ExportarReporte", function (e) {

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

    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HReporte Encuesta Covid Diaria", ".xlsx", [], false, "hoja 1")

    exito("Notificacion", "Exportando Informacion");

})

$(document).ready(function () {

    traerDatosReporte();
});