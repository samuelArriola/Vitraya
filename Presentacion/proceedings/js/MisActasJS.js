$txtCodigo = $("#txtCodigo");
$txtNombre = $("#txtNombre");
$txtFecha = $("#txtFecha");
$txtLugar = $("#txtLugar");
$slcEstado = $("#slcEstado");

Actas = [];


const CargarActas = (msg) => {
    let Datos = msg.d;
    dataTableActas = "";
    Datos.forEach(item => {
        dataTableActas += `
            <tr>
                <td>${item.acta.StrSigla}</td>
                <td>${item.acta.StrNombre}</td>
                <td>${moment(item.acta.DtmFechEditable).format("DD/MM/YYYY")}</td>
                <td>${item.acta.StrLugarReun}</td>
                <td>${item.miembro.BlnFirmado ? "Firmado" : "No firmado"}</td>
                <td class="text-center">
                    <a href="javascript:window.open('${window.origin}/ActasReunion/Acta/${item.acta.IntOidARActas}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')" target="arch"><i class="btn btn-info fa fa-2x fa-eye"></i></a>
                    ${!item.miembro.BlnFirmado ? `<div class="btn btn-info p-1 pl-3 pr-3 ml-3 btn-firmar" data-idmiembro="${item.miembro.IntOidARActasDM}">
                        <img src="../Images/firma-con-boligrafo.svg" width="20" data-idmiembro="${item.miembro.IntOidARActasDM}" />
                    </div>` : ""}   
                </td>
            </tr>
        `
    });

    $("#tbActas tbody").html(dataTableActas);
    DataTable("#tbActas");
    $("#loading-modal").modal("hide");
}


$(document).on("click", ".btn-firmar", function (e) {
    let idMiembro = parseInt($(e.target).attr("data-idmiembro"))
    console.log(idMiembro)
    ejecutarajax(
        "MisActas.aspx/FirmarActa",
        { "idMiembro": idMiembro },
        function (msg) {
            if (msg.d) {
                GetActas();
                exito("Hecho", "Acta firmada correctamente")
            }
            else {
                error("Error","Ha ocurrido un error al intentar firnar el acta")
            }
        }
    )
});

function GetActas() {
    $("#loading-modal").modal();

    datos = {
        "codigo": $txtCodigo.val(),
        "nombre": $txtNombre.val(),
        "fecha": new Date($txtFecha.val() || "01-01-3000"),
        "lugar": $txtLugar.val(),
        "estado": $slcEstado.val(),
    }
    ejecutarajax("MisActas.aspx/GetActasByUser", datos, CargarActas);
    
}

$(document).ready(() => {
    GetActas();
});

$txtCodigo.keypress(function (e) { if (e.keyCode == 13) GetActas() })
$txtNombre.keypress(function (e) { if (e.keyCode == 13) GetActas() })
$txtLugar.keypress(function (e) { if (e.keyCode == 13) GetActas() })

$txtFecha.change(function (e) { GetActas()})
$slcEstado.change(function (e) { GetActas() })

function GenerateToolTip(text) {
    toolTip = document.createElement("div");
    $(toolTip).attr("class", "tooltip");
    console.log(toolTip);
    $(toolTip).text(text);

    $(document.body).prepend(toolTip);
}

