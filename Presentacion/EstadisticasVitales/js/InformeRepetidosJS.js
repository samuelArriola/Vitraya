

function cargardatos(msg) {
    let registros = msg.d;

    console.log(registros);

    dtRegistros = "";

    registros.forEach(registro => {
        dtRegistros += `
            <tr>
                <td>${registro.Codigo}</td>
                <td>${registro.DocumentoMadre}</td>
                <td>${registro.NombreMadre}</td>
                <td>${registro.NombreDoctor}</td>
                <td>${registro.Fecha}</td>
            </tr>
        `;
    });
    $("#tbRegistrosNV tbody").html(dtRegistros);
    DataTable("#tbRegistrosNV");
}


function getRegistrosRepetidos() {

    ejecutarajax(
        "InformeRepetidos.aspx/GetDocsRepetidosNV",
        {
            codigo: $("#txtCodigo").val(),
            docMadre: $("#txtDocumentoMadre").val(),
            nomMadre: $("#txtNomMadre").val(),
            nomDoctor: $("#txtNomDoctor").val()
        },
        cargardatos
    )
}

$(document).ready(function () {
    getRegistrosRepetidos();
})

$("#txtCodigo, #txtDocumentoMadre, #txtNomMadre, #txtNomDoctor").keypress(function (e) {
    if (e.keyCode == 13) {
        getRegistrosRepetidos();
    }
});