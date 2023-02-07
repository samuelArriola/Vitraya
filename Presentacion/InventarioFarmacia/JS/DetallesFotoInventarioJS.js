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

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function traerDatosRegistroF() {

    var FechaInventario = getParameterByName('Fec');

    datos = {
        "fechaFoto": FechaInventario
    }

    $("#loading-modal").modal();
    ejecutarAjax("DetallesFotoInventario.aspx/GetRegistrosFoto", datos, llenarTablaRegistros);

}

function llenarTablaRegistros(msg) {

    datos = msg.d;

    dtRegistros = "";

    datos.forEach((item) => {

        dtRegistros += `

            <tr>

                <td>${item.FT_Cod_almacen}</td>
                <td>${item.FT_Nom_almacen}</td>
                <td>${item.FT_Cod_producto}</td>
                <td>${item.FT_Nom_producto}</td>
                <td>${item.FT_Estado_producto}</td>
                <td>${item.FT_Cod_lote}</td>
                <td>${moment(item.FT_FecVen_lote).format("DD/MM/YYYY")}</td>
                <td>${item.FT_Cant_sistema}</td>

            </tr>

        `;

    })

    $("#tbDetallesF").html(dtRegistros);
    DataTable("#tableDetallesFoto", 15);

    $("#loading-modal").modal("hide");

}

$(document).ready(function () {

    traerDatosRegistroF();

});