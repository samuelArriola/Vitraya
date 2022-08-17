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

function traerListaReportes() {

    $("#loading-modal").modal();
    ejecutarAjax("ListaReportes.aspx/getListaReportes", {}, mostrarListaReportes);
}

function mostrarListaReportes(msg) {

    datos = msg.d;

    dtListaR = "";

    datos.forEach((item) => {

        dtListaR += `

            <tr>
                
                <td>${item.OidReportePB1}</td>
                <td>${item.Nombre1}</td>
                <td>${item.Descripcion1}</td>
                <td>  <button  type="button" class="btnVerReporte btn btn-primary" data-enlace="${item.Enlace1}" >ABRIR REPORTE <i class="fa fa-sign-out btnVerReporte" data-enlace="${item.Enlace1}"></i></button> </td>
               
            </tr>

        `;

    })

    $("#tbInfoR").html(dtListaR);
    DataTable("#tableReportes", 10);

    $("#loading-modal").modal("hide");

}

$(document).on("click", ".btnVerReporte", function (e) {

    let Enlace = $(e.target).attr("data-enlace");

    //window.location.href = Enlace;
    window.open(Enlace, '_blank');
})

$(document).on("click", ".btnFiltroNombreR", function (e) {

    let nombreReporte = $("#filtroNomReporte").val();

    datos = {
        "nombreReporte": nombreReporte
    }

    if (nombreReporte == "") {

        traerListaReportes();

    } else {

        ejecutarAjax("ListaReportes.aspx/filtroNombreR", datos, mostrarListaReportes);

    }

})

$(document).ready(function () {

    traerListaReportes();

});