const $txtDocumento = $("#txtDocumento");
const $txtNombreUsuario = $("#txtNombreUsuario");
const $txtSigla = $("#txtSigla");
const $txtNombreActa = $("#txtNombreActa");

function CargarListaNoFirmados(smg) {
    datos = smg.d;
    console.log(datos);

    dataTable = "";

    datos.forEach(item => {
        dataTable += `
            <tr>
                <td>${item.Documento}</td>
                <td>${item.NombreUsuario}</td>
                <td>${item.Sigla}</td>
                <td>${item.NombreActa}</td>
            </tr>
        `;
    });

    $("#tbNoFirmados").html(dataTable);
    DataTable("#tbAsistentesSinFirma");
    $("#loading-modal").modal("hide");

}


$(document).on("click", "#btnExportar", function (e) {
    e.preventDefault();
    let tableExport = new TableExport(
        $("#tbAsistentesSinFirma")[0],
        {
            exportButtons: false, // No queremos botones
            filename: "Informe Asistentes sin firma en actas de Reunión", //Nombre del archivo de Excel
            sheetname: "hoja 1", //Título de la hoja
        }
    );
    let datos = tableExport.getExportData();
    let preferenciasDocumento = datos.tbAsistentesSinFirma.xlsx;

    preferenciasDocumento.data.shift();

    for (var i = 0; i < preferenciasDocumento.data.length; i++) {
        for (var j = 0; j < preferenciasDocumento.data[i].length; j++) {
            preferenciasDocumento.data[i][j].t = "s"
        }
    }
    tableExport.export2file(preferenciasDocumento.data, preferenciasDocumento.mimeType, preferenciasDocumento.filename, preferenciasDocumento.fileExtension, preferenciasDocumento.merges, preferenciasDocumento.RTL, preferenciasDocumento.sheetname);
});

function GetListaNoFirmados() {
    $("#loading-modal").modal();
    datos = {
        "nombreUsuario": $txtNombreUsuario.val() ?? "",
        "documento" : $txtDocumento.val() ?? "",
        "sigla": $txtSigla.val() ?? "",
        "nombreActa": $txtNombreActa.val() ?? "",
    }

    ejecutarajax("InformeAsistentesSinFirma.aspx/GetAsistentesSinFirma", datos, CargarListaNoFirmados)
}

$(document.forms[0]).on("keypress", e => { if (e.keyCode == 13) e.preventDefault() });

$(document).ready(GetListaNoFirmados);

$txtDocumento.keypress(e => {
    if (e.keyCode == 13) {
        GetListaNoFirmados();
        $txtDocumento.focus();
    }
});

$txtNombreActa.keypress(e => {
    if (e.keyCode == 13)
        GetListaNoFirmados();
});

$txtNombreUsuario.keypress(e => {
    if (e.keyCode == 13)
        GetListaNoFirmados();
});

$txtSigla.keypress(e => {
    if (e.keyCode == 13)
        GetListaNoFirmados();
});