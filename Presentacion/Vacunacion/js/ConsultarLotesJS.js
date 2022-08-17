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
function getLotes() {
    ejecutarAjax(
        "ConsultarLotes.aspx/GetLotes",
        {
            idInsumo: $("#ContentPlaceHolder_slcInsumo").val()
        },
        function (msg) {
            let Lotes = msg.d;

            let dtLotes = "";

            Lotes.forEach(lote => {
                dtLotes += `
                <tr>
                    <td>${lote.StrNombreInsumo}</td>
                    <td>${lote.StrNumLote}</td>
                    <td>${lote.IntTotalIngresado}</td>
                    <td>${lote.IntExistencias}</td>
                </tr>
            `;
            });
            $("#tbLotes tbody").html(dtLotes);
        }
    )
    
}

$("#ContentPlaceHolder_slcInsumo").change(getLotes)

$(document).ready(function () {
    getLotes();
})