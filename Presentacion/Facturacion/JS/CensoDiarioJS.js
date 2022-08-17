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

function traerDatosCensoDiario() {

    $("#loading-modal").modal();
    ejecutarAjax("CensoDiario.aspx/getInfoCenso", {}, llenarTablaCenso);

}

function llenarTablaCenso(msg) {

    datos = msg.d;

    let color;

    dtCenso = "";

    datos.forEach((item) => {

        if (item.UnidadFuncional == "URGENCIAS") {
            color = "table-danger";
        } else if (item.UnidadFuncional == "SALA DE PARTO") {
            color = "table-warning";
        } else if (item.UnidadFuncional == "UCI ADULTO") {
            color = "table-primary";
        } else if (item.UnidadFuncional == "UCI NEONATAL") {
            color = "table-info";
        } else if (item.UnidadFuncional == "HOSPITALIZACION") {
            color = "table-success";
        } else if (item.UnidadFuncional == "CIRUGIA") {
            color = "table-secondary";
        } else if (item.UnidadFuncional == "CAMAS DE EMERGENCIA") {
            color = "table-danger";
        }

        if (item.ResultadoCierre == 0) {

            dtCenso += `

                <tr>

                    <td class="${color}">${item.UnidadFuncional}</td>
                    <td>${item.UnidadFuncionalSubgrupo}</td>
                    <td>${item.NumeroIngreso}</td>
                    <td style="display:none;" >${item.TipoIdentificacion}</td>
                    <td>${item.NumeroIdentificacion}</td>
                    <td>${item.NombresApellidos}</td>
                    <td>${moment(item.FechaIngreso).format("DD/MM/YYYY HH:MM")}</td>
                    <td><button  type="button" class="btnVerDetalles btn btn-primary" data-id-type="${item.TipoIdentificacion}" data-id="${item.NumeroIdentificacion}">VER DETALLES</button></td>
                    <td>
                      <div class="form-check">
                       <input style="margin: auto; margin-left: auto; margin-right: auto;width:25px; height:25px;" class="form-check-input" type="checkbox" ${item.FechaEgreso == null ? "" : "Checked"} value="" id="flexCheckDefault">
                      </div>
                    </td>
               
                </tr>

            `;

        }

    })

    $("#tbInfoCenso").html(dtCenso);
    DataTable("#tableCenso", 10);

    $("#loading-modal").modal("hide");

}

let idPaciente;
let tipoId;

$(document).on("click", ".btnVerDetalles", function (e) {

    idPaciente = parseInt($(e.target).attr("data-id"));
    tipoId = $(e.target).attr("data-id-type");

    datos = {
        "id": idPaciente
    }

    ejecutarAjax("CensoDiario.aspx/GetCensoDetalles", datos, mostrarInfoDetalles);
})

let PNumeroIdPac;
let nombresPaciente;

let permiso;

function mostrarInfoDetalles(msg) {

    detalles = msg.d;
    dtDetalles = "";
    dtCerrar = "";
    let opcion = "";
    let Admision;

    //PermisosUsuario();
    autorizado = true;

    detalles.forEach((detalle) => {

        nombresPaciente = "Nombre de paciente: " + detalle.NombresApellidos;
        PNumeroIdPac = "Numero de Identificacion paciente: " + detalle.NumeroIdentificacion;
        Admision = detalle.NumeroIngreso;

        if (detalle.Resultado == 0) {

            if (permiso == 0) {
                opcion = `<p>PENDIENTE</p>`;
                $("#DivCerrar").hide();
            }
            if (permiso == 1) {
                opcion = `<button type="button" id="btnRedireccionAut" class="btnRedireccionAut btn btn-primary" data-id="${detalle.Cups}" data-unidad="${detalle.UnidadFuncional}">AUTORIZAR</button>`;
                //$("#DivCerrar").hide();
            }

        }else if (detalle.Resultado > 0) {

            if (permiso == 0) {
                opcion = `<p>AUTORIZADO</p>`;
                //$("#DivCerrar").hide();
            }
            if (permiso == 1) {
                opcion = `<p>AUTORIZADO</p>`;
                //$("#DivCerrar").show();
            }

        }

        if (detalle.Resultado == 0) {
            autorizado = false;
        }

        dtDetalles += `

            <tr>

                <td>${detalle.NumeroIngreso}</td>
                <td style="display:none;" >${detalle.NumeroIdentificacion}</td>
                <td>${detalle.TipoServicio}</td>
                <td>${detalle.UnidadFuncional}</td>
                <td>${detalle.Cups}</td>
                <td>${detalle.NombreCups}</td>
                <td>${detalle.Cama}</td>
                <td>${opcion}</td>
               
            </tr>       

        `;
    })

    //if (autorizado) $("#DivCerrar").show();
    //else $("#DivCerrar").hide()
    


    dtCerrar += `

            <table class="table-inf" id="tableFCierre">
            <tr>
                <th class="text-center" colspan="2">Cargue de información para cerrar paciente</th>
            </tr>
            <tr>
                <td>*Describa motivo</td>
                <td><textarea class="form-control" id="motivoCierre" ></textarea></td>
            <tr>
            </table>
            <div class="text-center">
                <div class="form-group">
                    <button style="margin-top: 20px;" type="button" data-id="${Admision}" id="btnCerrarPaciente" class="btnCerrarPaciente btn btn-danger">CERRAR PACIENTE</button>
                </div>
            </div>

    `;

    $("#PNombrePaciente").html(nombresPaciente);
    $("#PIdentificacionPaciente").html(PNumeroIdPac);
    $("#tbInfoDetalles").html(dtDetalles);
    $("#DivCerrar").html(dtCerrar);
    $("#modal2").modal();
}

$(document).on("click", ".btnCerrarPaciente", function (e) {

    let Admision = $(e.target).attr("data-id");
    let motivoCierre = $("#motivoCierre").val();

    if (motivoCierre == "") {

        error("Error", "Debe diligenciar el motivo del cierre del paciente");

    } else {

        datos = {
            "motivoCierre": motivoCierre,
            "Admision": Admision
        }

        $('#modal2').modal('hide');
        ejecutarAjax("CensoDiario.aspx/SetCierrePaciente", datos, traerDatosCensoDiario);
    }
})

$(document).on("click", ".btnRedireccionAut", function (e) {

    let cups = $(e.target).attr("data-id");
    let unidadF = $(e.target).attr("data-unidad");

    datos = {
        "idPaciente": idPaciente,
        "idCups": cups,
        "descrUnidad": unidadF,
    }

    let link = "../Facturacion/SolicitarAutorizaciones.aspx?idPaciente=" + idPaciente + "&idCups=" + cups + "&tipoId=" + tipoId + "&unidadF=" + unidadF;

    //window.location.href = link;

    window.open(link, '_blank');
    $('#modal2').modal('hide');

})

$(document).on("click", ".btnVerHistorico", function (e) {

    let linkHistorico = "../Facturacion/HistoricoCierres.aspx";

    window.location.href = linkHistorico;
})

//FILTRO DE GRUPOS
$(document).on("click", ".btnBuscar", function (e) {

    let grupo = $("#filtroGrupo").val();
    let subgrupo = $("#filtroSubGrupo").val();

    datos = {
        "grupo": grupo,
        "subgrupo": subgrupo,
    }

    $("#loading-modal").modal();
    ejecutarAjax("CensoDiario.aspx/GetfiltroGrupo", datos, llenarTablaCenso);
})

//FILTRO DE FECHA DE INGRESO
$(document).on("click", ".btnBuscarFec", function (e) {

    let filtroFecha = $("#filtroFecha").val();

    datos = {
        "filtroFecha": filtroFecha
    }

    $("#loading-modal").modal();
    ejecutarAjax("CensoDiario.aspx/GetfiltroFecha", datos, llenarTablaCenso);
})

let campoNumeroID = document.getElementById("filtroNumId");

campoNumeroID.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {

        filtroNumeroIdentificacion();
    }

});

function filtroNumeroIdentificacion() {

    let numID = $("#filtroNumId").val();

    datos = {
        "numID": numID
    }

    $("#loading-modal").modal();
    ejecutarAjax("CensoDiario.aspx/GetfiltroNumId", datos, llenarTablaCenso);

}

let campoNombre = document.getElementById("filtroNombres");

campoNombre.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {

        filtroNombre();
    }

});

function filtroNombre() {

    let nombrePaciente = $("#filtroNombres").val();

    datos = {
        "nombrePaciente": nombrePaciente
    }

    $("#loading-modal").modal();
    ejecutarAjax("CensoDiario.aspx/GetfiltroNombre", datos, llenarTablaCenso);

}

let campoIngreso = document.getElementById("filtroNumIngreso");

campoIngreso.addEventListener("keydown", function (e) {

    if (e.keyCode === 13) {

        filtroIngreso();
    }

});

function filtroIngreso() {

    let numeroIngreso = $("#filtroNumIngreso").val();

    datos = {
        "numeroIngreso": numeroIngreso
    }

    $("#loading-modal").modal();
    ejecutarAjax("CensoDiario.aspx/GetfiltroIngreso", datos, llenarTablaCenso);

}

//VALIDAR PERMISOS Y VISUALIZACIONES
function PermisosUsuario() {

    let linkOpcion = "../Facturacion/CensoDiario.aspx";

    datos = {
        "linkOpcion": linkOpcion
    }

    ejecutarAjax("CensoDiario.aspx/GetPermisos", datos, ValidarPermisos);

}

function ValidarPermisos(msg) {

    let datos = msg.d;

    if (datos.BlnConfirmar == false || datos.BlnCrear == false || datos.BlnEliminar == false || datos.BlnModificar == false) {

        permiso = 0;

    }
    if (datos.BlnConfirmar == true && datos.BlnCrear == true && datos.BlnEliminar == true && datos.BlnModificar == true) {

        permiso = 1;

    }

}

$(document).ready(function () {

    PermisosUsuario();
    traerDatosCensoDiario();

});