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

function traerDatosHistoricoC() {

    $("#loading-modal").modal();
    ejecutarAjax("HistoricoCierres.aspx/GetInfoHistorico", {}, llenarTablaHistorico);

}

function llenarTablaHistorico(msg) {

    datos = msg.d;

    dtHistorico = "";

    datos.forEach((item) => {

        dtHistorico += `

            <tr>

                <td>${item.NumIngreso}</td>
                <td class="table-danger">${item.EstadoCierre}</td>
                <td>${item.UsuarioCierre}</td>
                <td>${moment(item.FechaCierre).format("DD/MM/YYYY HH:MM")}</td>
                <td>${item.MotivoCierre}</td>
                
            </tr>

        `;

    })

    $("#tbHistoricoCierre").html(dtHistorico);
    DataTable("#tableHistoricoCierre", 10);

    $("#loading-modal").modal("hide");

}

//FILTROS

let campoIngreso = document.getElementById("filtroIngreso");

campoIngreso.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {

        filtroH1();
    }

});

let campoNombreC = document.getElementById("filtroUsuario");

campoNombreC.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {

        filtroH1();
    }

});

$(document).on("click", ".btnBuscarH", function (e) {

    filtroH1();

})

function filtroH1() {

    let numeroIngreso = $("#filtroIngreso").val();
    let nombreCierre = $("#filtroUsuario").val();

    datos = {
        "numeroIngreso": numeroIngreso,
        "nombreCierre": nombreCierre
    }

    $("#loading-modal").modal();
    ejecutarAjax("HistoricoCierres.aspx/Getfiltro1", datos, llenarTablaHistorico);

}

$(document).on("click", ".btnBuscarFec", function (e) {

    let filtroFechaI = $("#filtroFechaC").val();
    let filtroFechaF = $("#filtroFechaC2").val();

    datos = {
        "fechaI": filtroFechaI,
        "fechaF": filtroFechaF
    }

    console.log(datos);

    $("#loading-modal").modal();
    ejecutarAjax("HistoricoCierres.aspx/Getfiltro2", datos, llenarTablaHistorico);

});

$(document).ready(function () {

    traerDatosHistoricoC();

});