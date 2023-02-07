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

$(document).on("click", ".btnTomarCaptura", function (e) {

    $("#loading-modal").modal();
    ejecutarAjax("FotosInventario.aspx/GetSetFoto", {}, successful);

})

function successful() {

    loadCardsInventarios();
    $("#loading-modal").modal("hide");
    exito("Notificacion", "La captura de foto de cantidades ha sido exitosa");

}

function loadCardsInventarios() {

    $("#loading-modal").modal();
    ejecutarAjax("FotosInventario.aspx/GetFotosInvVitraya", {}, successfulCardsInventarios);

}

function successfulCardsInventarios(msg) {

    moment.locale('es');
    fechas = msg.d;

    dtCards = "";

    fechas.forEach((item) => {

        dtCards += `

            <div class="col-sm-3">
                <div class="card card border-dark mb-3">
                    <div class="card-body">
                        <h5 class="card-title">INVENTARIO ${moment(item.FT_Fecha_foto).format("DD MMMM YYYY") }</h5>
                        <p class="card-text">Se consolida la informacion de las cantidades en inventario hasta la fecha..</p>
                        <a href="DetallesFotoInventario.aspx?Fec=${moment(item.FT_Fecha_foto).format("YYYY/MM/DD") }" class="btn btn-primary">Abrir</a>
                    </div>
                </div>
            </div>

        `;

    })

    $("#divCardsInventario").html(dtCards);

    $("#loading-modal").modal("hide");

}

$(document).ready(function () {

    loadCardsInventarios();

});