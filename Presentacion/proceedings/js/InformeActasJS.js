const txtCodigo = $("#txtCodigo");
const txtNombre = $("#txtNombre");
const fecha1 = $("#fecha1");
const fecha2 = $("#fecha2");
const txtCoordinador = $("#txtCoordinador");
const txtEstado = $("#txtEstado");


Actas = [];


function CargarActas(msg) {
    Actas = msg.d;

    dataTable = "";
    Actas.forEach((acta, index) => {
        if (txtEstado.val() == 3) {
            if (acta.IntEstado == 3) {
                dataTable += `
                    <tr>
                        <td>${acta[0].StrSigla}</td>
                        <td>${acta[0].StrNombre}</td>
                        <td>${moment(acta[0].DtmFechEditable).format("DD/MM/YYYY")}</td>
                        <td>${acta[0].StrCoordinador}</td>
                        <td>Firmada</td>
                        <td><a href="javascript:window.open('${window.origin}/ActasReunion/Acta/${acta[0].IntOidARActas}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')" target="frmActa"><i class="glyphicon glyphicon-eye-open"></i></a></td>
                        <td>
                    </tr>
                `;
            }
        }
        else {
            dataTable += `
                <tr>
                    <td>${acta[0].StrSigla}</td>
                    <td>${acta[0].StrNombre}</td>
                    <td>${moment(acta[0].DtmFechEditable).format("DD/MM/YYYY")}</td>
                    <td>${acta[0].StrCoordinador}</td>
                    <td>${acta[0].IntEstado == 1 ? "En proceso" : "Cerrada"}</td>
                    <td><a href="javascript:window.open('${window.origin}/ActasReunion/Acta/${acta[0].IntOidARActas}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')" target="frmActa"><i class="glyphicon glyphicon-eye-open"></i></a></td>
                    <td  class="btnVerNoFirmados" data-index=${index}>Asistentes sin firma</td>
                </tr>
            `;
        }
        
    })
    $("#tablaActas tbody").html(dataTable)
    DataTable("#tablaActas");
    $("#loading-modal").modal("hide");
}

$(document).on("click", ".btnVerNoFirmados", e => {
    let index = parseInt($(e.target).attr("data-index"));

    let dataTable = `
        <table class="table-inf">
            <thead>
                <tr>
                    <th>Documento</th>
                    <th>Nombre</th>
                    <th>Rol</th>
                </tr>
            <thead>
            <tbody>
    `

    Actas[index][1].forEach(userWithoutSigning => {
        dataTable += `
            <tr>
                <td>${userWithoutSigning.IntGNCodUsu}</td>
                <td>${userWithoutSigning.StrNombre}</td>
                <td>${userWithoutSigning.StrTipoUsuario}</td>
            </tr>
        `
    });

    dataTable += `</tbody></thea>`
    $("#modal-content").html(dataTable)

    $("#modal-view-no-signing").modal();
});
function GetActas() {
    $("#loading-modal").modal();

    info = {
        'nombre': txtNombre.val(),
        'codigo': txtCodigo.val(),
        'fecha1': new Date(fecha1.val() == ""? "01/01/1800" : fecha1.val()),
        'fecha2': new Date(fecha2.val() == "" ? "01/01/3000" : fecha2.val()),
        'coordinador': txtCoordinador.val(),
        'estado': txtEstado.val()
    };
    console.log(info);

    ejecutarajax("InformeActas.aspx/GetActas", info, CargarActas)
}



$(document).on("click", "#btnExpotExcel", function (e) {
    e.preventDefault();
    let tableExport = new TableExport(
        $("#tablaActas")[0],
        {
            exportButtons: false, // No queremos botones
            filename: "Informe de actas de reunion", //Nombre del archivo de Excel
            sheetname: "hoja 1", //Título de la hoja
        }
    );
    let datos = [[
        { v: "Código", t: "s" },
        { v: "Nombre", t: "s" },
        { v: "Fecha", t: "s" },
        { v: "Hora Inicial", t: "s" },
        { v: "Hora Final", t: "s" },
        { v: "Fecha del sistema", t: "s" },
        { v: "Coordinador", t: "s" },
        { v: "Estado", t: "s" },
    ]];

    Actas.forEach(acta => {
        estado = ["En Proceso", "Cerrada", "Firmada"]
        datos.push([
            { v: acta[0].StrSigla, t: "s" },
            { v: acta[0].StrNombre, t: "s" },
            { v: moment(acta[0].DtmFechEditable).format("DD/MM/YYYY"), t: "s" },
            { v: moment(acta[0].DtmFecInicio).format("HH:mm A"), t: "s" },
            { v: moment(acta[0].DtmFecFinal).format("HH:mm A"), t: "s" },
            { v: moment(acta[0].DtmFecSistema).format("DD/MM/YYYY"), t: "s" },
            { v: acta[0].StrCoordinador, t: "s" },
            { v: estado[acta[0].IntEstado - 1], t: "s" },
        ])
    })

    
    tableExport.export2file(datos, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Informe de Actas de reunión", ".xlsx", [], false, "hoja 1")
});


$(document).ready(function () {
    $(document.forms[0]).on("keypress", e => { if(e.keyCode == 13) e.preventDefault()});
    GetActas();
})

txtCodigo.keypress(function (e) {
    if (e.keyCode == 13)
        GetActas();
});

txtNombre.keypress(function (e) {
    if (e.keyCode == 13)
        GetActas();
});

txtCoordinador.keypress(function (e) {
    if (e.keyCode == 13)
        GetActas();
});




