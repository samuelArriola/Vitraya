$txtCapacitacion = $("#txtCapacitacion");
$txtDocumento = $("#txtDocumento");
$txtNombre = $("#txtNombre");
$txtUnidad = $("#txtUnidad");

let datos = []

function CargarInformeAsistencia(msg) {
    datos = msg.d;

    let dataTable = ""
    datos.forEach(item => {
        dataTable += `
            <tr>
               <td>${item.Tema}</td>
               <td>${item.Documento}</td>
               <td>${item.Nombre}</td>
               <td>${item.Unidad}</td>
               <td>${item.Asistencia}</td>
               <td>${item.Firma}</td>
            </tr>
        `
    });

    $("#table-informe-asistencia tbody").html(dataTable);
    DataTable("#table-informe-asistencia");
    let tema = datos[0].Tema
    let CountFirmas = 0;
    let CountMatriculas = 0;
    let filas = [];

    for (let item in datos) {
        if (item.Tema != tema) {

        }
        else {
            CountMatriculas ++;
            CountFirmas += item.Firma == "Firmado" ? 1 : 0;
        }
    }
}


function GetInformeAsistencia() {
    datos = {
        "tema": $txtCapacitacion.val(),
        "documento": $txtDocumento.val(),
        "nomUsuario": $txtNombre.val(),
        "unidad": $txtUnidad.val()
    };

    ejecutarajax("InformesCapacitaciones.aspx/GetInformeAsistencia",datos, CargarInformeAsistencia)
}

$(document).ready(function (e) {
    GetInformeAsistencia();
});

$(document).on("click", "#btnDescargar", function (e) {
   
    tabla = [];
    var header = [
        { v: "Capacitación", t: "s" },
        { v: "Documento", t: "s" },
        { v: "Nombre", t: "s" },
        { v: "Unidad Funcional", t: "s" },
        { v: "Asistencia", t: "s" },
        { v: "Firma", t: "s" }
    ];

    tabla.push(header);

    datos.forEach(item => {
        fila = [
            { v: item.Tema, t: "s" },
            { v: item.Documento, t: "s" },
            { v: item.Nombre, t: "s" },
            { v: item.Unidad, t: "s" },
            { v: item.Asistencia, t: "s" },
            { v : item.Firma, t : "s"}
        ]
        tabla.push(fila);
    })

    tableExport = new TableExport(document.createElement("table"), {});
    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Informe de asistencia a capacitaciones", ".xlsx", [], false, "hoja 1")
})

$txtCapacitacion.keypress(function (e) {
    if (e.keyCode == 13) {
        GetInformeAsistencia();
    }
});

$txtDocumento.keypress(function (e) {
    if (e.keyCode == 13) {
        GetInformeAsistencia();
    }
});

$txtUnidad.keypress(function (e) {
    if (e.keyCode == 13) {
        GetInformeAsistencia();
    }
});
$txtNombre.keypress(function (e) {
    if (e.keyCode == 13) {
        GetInformeAsistencia();
    }
});