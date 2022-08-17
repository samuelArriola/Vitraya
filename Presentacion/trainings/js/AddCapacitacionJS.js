capacitaciones = [];
let idCapacitacion;



$(document).on("click", "#btnCrearAgenda", function (e) {
    e.preventDefault();
    window.location.href = "NuevaAgenda.aspx?idCapacitacion=" + idCapacitacion;
});

function CargarCapacitaciones(msg) {
    capacitaciones = msg.d;
    dtCapacitaciones = ""
    console.log(capacitaciones);
    capacitaciones.forEach((capacitacion, index) => {
        dtCapacitaciones += `
            <tr>
                <td data-index="${index}">${capacitacion.StrTEMA}</td>
            </tr>
        `;
    })
    $("#tbCapacitciones tbody").html(dtCapacitaciones);
    DataTable("#tbCapacitciones")

    $("#lbCodigo").text(capacitaciones[0].StrCODIGO)
    $("#lbResponsable").text(capacitaciones[0].StrRESPONSABLE)
    $("#lbTema").text(capacitaciones[0].StrTEMA)

    if (capacitaciones.length > 0)
        GetAgenda(0)
}

function GetCapacitaciones() {
    ejecutarajax("AddCapacitacion.aspx/GetCapacitaciones", { "info": $("#txtSearch").val() }, CargarCapacitaciones)
}

$(document).on("click", "#tbCapacitciones tbody td", function (e) {
    index = parseInt($(e.target).attr("data-index"))
    $("#lbCodigo").text(capacitaciones[index].StrCODIGO)
    $("#lbResponsable").text(capacitaciones[index].StrRESPONSABLE)
    $("#lbTema").text(capacitaciones[index].StrTEMA)

    GetAgenda(index);
});

$(document).ready(function () {
    GetCapacitaciones();
    $("#txtSearch").keypress(function (e) {
        if (e.keyCode == 13) {
            GetCapacitaciones();
        }
    });

    $("form").keypress(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
        }
    })
});


function CargarAgendas(msg) {

    let mensaje1 = "No iniciado";
    let mensaje2 = "Iniciado";
    let mensaje3 = "Terminado";

    let agendas = msg.d;
    $("#panelAgendas").html("");

    let HtmlPanelAgendas = ""

    console.log(agendas);

    agendas.forEach((agenda, index) => HtmlPanelAgendas += `
        <div class="col-12 col-sm-6 col-md-4 col-lg-3">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <span>Agenda # ${index + 1} </span>
                    </div>
                </div>
                <div class="x_content">
                    <div class="d-flex justify-content-between">
                        <div>
                            <img src="../Images/alfabeto.svg" width="50" />
                        </div>
                        <div>
                            <div>ESTADO:</div>
                            <div><b>${agenda.IntEstado == 1 ? mensaje1 : (agenda.IntEstado == 2 ? mensaje2 : mensaje3)}</b></div>
                        </div>
                        <div>
                            <div>${agenda.StrModalidad}</div>
                            <div>${moment(agenda.DtmFecha).format("DD/MM/YYYY")}</div>
                        </div>
                    </div>
                    <div class="justify-content-around d-flex mt-2">
                        <button class="btn btn-info btn-config" type="button" onclick="ConfiurarAgenda(${agenda.IntOidCPAgenda},${agenda.IntEstado})" data-toggle="tooltip" data-placement="bottom" title="CONFIGURACION DE AGENDA">
                            <i class="fa fa-cog"></i>
                        </button>
                        <button class="btn btn-info" type="button" onclick="GetAcataAsistencia(${agenda.IntOidCPCapacitacion},${agenda.IntEstado})" data-toggle="tooltip" data-placement="bottom" title="ACTA DE ASISTENCIA">
                            <i class="fa fa-file-text-o"></i>
                        </button >  
                        <button class="btn btn-info btn-ejecutar" type="button" data-idagenda=${agenda.IntOidCPAgenda} data-estado="${agenda.IntEstado}" data-toggle="tooltip" data-placement="bottom" title="INICIAR CAPACITACION">
                            <i class="fa fa-check" data-idagenda=${agenda.IntOidCPAgenda} data-estado="${agenda.IntEstado}"></i>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    `)

    $("#panelAgendas").html(HtmlPanelAgendas);

    $("#panelAgendas").append(`
        <div class="col-12 col-sm-6 col-md-4 col-lg-3">
            <div class="x_panel justify-content-center d-flex align-items-center" style="height:calc(100% - 10px);" id="btnCrearAgenda">
                <i class="fa fa-plus" style="font-size: 90px"></i>
            </div>
        </div>
    `);

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

}

$(document).on("click", ".btn-ejecutar", function (e) {
    e.preventDefault();
    let idAgenda = $(e.target).attr("data-idagenda")
    let estado = $(e.target).attr("data-estado")
    switch (estado) {
        case "1": {
            console.log(estado);
            $("#btnIniciar").attr("data-idagenda", idAgenda)
            $("#modal-iniciar").modal();
            break;
        }
        case "2": {
            window.location.href = "EjecutarCapa.aspx?idAgenda=" + idAgenda;
            break;
        }
        case "3": {
            error("Capacitación finalizada", "La capacitación esta terminada y no se puede realizar cambios")
            break;
        }
    }

})

$("#btnIniciar").click(function (e) {
    let idAgenda = $(e.target).attr("data-idagenda")
    ejecutarajax(
        "AddCapacitacion.aspx/IniciarCapacitacion",
        { "idAgenda": idAgenda },
        function (msg) {
            window.location.href = "EjecutarCapa.aspx?idAgenda=" + idAgenda;
        }
    )
});


function GetAcataAsistencia(idCapacitacion, Estado) {
    if (Estado != 3) {
        error("Capacitación no terminada", "Mientras la capacitación no este terminda no se puede ver el acta de asistencia");
    }
    else {
        $("#arch").attr("src", "ActaCapacitacion.aspx?idCapacitacion=" + idCapacitacion)
    }
}


function ConfiurarAgenda(idAgenda, estado) {

    if (estado == 1)
        window.location.href = "EditarCapa.aspx?idAgenda=" + idAgenda;
    else
        error("Capacitación iniciada", "Mientras la capacitación esté iniciada no se podra editar")
}

function GetAgenda(index) {
    idCapacitacion = capacitaciones[index].IntOidCPCAPACITACION;

    $("#btnCrearAgenda").attr("data-idcapacitacion", idCapacitacion);

    ejecutarajax("AddCapacitacion.aspx/GetAgendasByIdCapacitacion", { "idCapacitacion": idCapacitacion }, CargarAgendas)
}


$("#btnMenuTemas").click(function (e) {
    $("#menuContainerTemas").removeClass("d-none");
    setTimeout(function () {
        $("#menuTemas .x_panel").css({
            "transform": "translateX(-100%)"
        });
    }, 100, false);
});

$(".btnHideMenuTemas").click(function (e) {
    $("#menuTemas .x_panel").css({
        "transform": "translateX(0)"
    });
    setTimeout(function () { $("#menuContainerTemas").addClass("d-none"); }, 300, false)
});