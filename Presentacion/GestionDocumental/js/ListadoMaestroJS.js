let tbDocsRev = $("#tbDocsRev");
let txtEstado = $("#txtEstado");
let txtTipDoc = $("#txtTipDoc");
let txtNomDoc = $("#txtNomDoc");
let datos;

let documento;
let listadoMaestro;

function  ejecutarajax(url, datos, success) {
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

function GetDataAjax(url, datos) {
    return $.ajax({
        url: url,
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
    });
}




async function  cargarTablaDocumentos(msg) {

    
    var ContentTable = "";

    listadoMaestro = msg.d;


    console.log(listadoMaestro)
    
    listadoMaestro.forEach((documento, i) => {

        ContentTable += `
            <tr>
                <td>${documento.Proceso}</td>
                <td>${documento.Codigo}</td>
                <td>${documento.Nombre}</td>
                <td>${documento.Tipo}</td>

                <td>${documento.Version}</td>
                <td>Publicado</td>
                <td class="static-cell">
                    <a href="javascript:openDocument('${documento.Tipo + "/" + documento.IdDocumento}')">
                        <i class="fa fa-file-pdf-o"></i>
                    </a>
                    <i class="fa fa-eye ml-2 view-documento"  data-index="${i}"></i>
                </td>
            </tr>
        `;
    })



    $("#tbDocsRev").html(ContentTable);
    DataTable("#tableDocs")
}
$(document).ready(function () {
    datos = {
        proceso: $("#txtProceso").val(),
        codigo: $("#txtCodigo").val(),
        nombre: $("#txtNombre").val(),
        tipo: $("#slcTipo").val(),
        version: $("#txtVersion").val()
    }

    ejecutarajax("ListadoMaestro.aspx/GetDocumentos", datos,
        function (msg) { cargarTablaDocumentos(msg) }
    );
})

$("#txtProceso, #txtCodigo, #txtNombre, #txtVersion").keypress(function (e) { if (e.keyCode == 13) cargarTablaDocumentos() })
$("#slcTipo").change(function () {
    cargarTablaDocumentos();
})


function openDocument(link) {
    let VHeight = window.innerHeight;
    let VWidth = window.innerWidth;
    window.open(`${link}`, "", `width = 1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`)
}


$(document).on("click", "#btnExpotExcel", function (e) {
    e.preventDefault();
    let tableExport = new TableExport(
        $("#tableDocs")[0],
        {
            exportButtons: false, // No queremos botones
            filename: "Listado Maestro", //Nombre del archivo de Excel
            sheetname: "hoja 1", //Título de la hoja
        }
    );
    let datos = tableExport.getExportData();
    let preferenciasDocumento = datos.tableDocs.xlsx;
    for (var i = 0; i < preferenciasDocumento.data.length; i++) {
        for (var j = 0; j < preferenciasDocumento.data[i].length; j++) {
            preferenciasDocumento.data[i][j].t = "s"
        }
    }

    tableExport.export2file(preferenciasDocumento.data, preferenciasDocumento.mimeType, preferenciasDocumento.filename, preferenciasDocumento.fileExtension, preferenciasDocumento.merges, preferenciasDocumento.RTL, preferenciasDocumento.sheetname);
})

$(document).on("click", ".view-documento", function (e) {
    console.log(e)
    documento = listadoMaestro[parseInt($(e.target).attr("data-index"))];
    $("#lbProcesos").text(documento.Proceso)
    $("#lbNombre").text(documento.Nombre)
    $("#lbEstado").text(documento.Estado)
    $("#lbCambio").text(documento.Cambio)
    $("#lbFecElab").text(documento.FechaElaboracion)
    $("#lbElaborador").text(documento.Elaborador)
    $("#lbFecRev").text(documento.FechaRevision)
    $("#lbRevisor").text(documento.Revisores)
    $("#lbFecApro").text(documento.FechaAprobacion)
    $("#lbAprobador").text(documento.Aprobador)
    $("#lbFecApro").text(documento.FechaAprobacion)
    $("#lbAprobador").text(documento.Aprobador)
    $("#modal-view-documento").modal()
})
