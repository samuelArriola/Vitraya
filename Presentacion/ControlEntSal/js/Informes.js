console.log('conectado a informes js');

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


//SALIDA DE PACIENTES
function PintarEgreso(fechaI, fechaF) {

    $('#ICStableEgreso').DataTable().destroy();
    var ICStableEgreso = $('#ICStableEgreso').DataTable({
        "ajax": {
            "url": "Informes.aspx/GetSPacientesReal",
            "method": "POST",
            "datatype": "json",
            "contentType" :"application/json; charset=utf-8",
           "data": function (d) {
               return JSON.stringify({
                   d, 
                   fechaI,
                   fechaF
               })
           },
            "dataSrc": 'd.data', //obtiene los datos de la propiedad res del objeto
            "error": function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        },

        "columns": [
            { "data": "OID" },
            { "data": "ORDENSALIDA" },
            { "data": "ADNINGRES1" },
            { "data": "DOCUMENTO" },
            { "data": "NOMBRE_COMPLETO" },
            { "data": "FECSALIDA" }
            //{ "defaultContent": "<div class='text-center'><div class='btn-group'><button class='btn btn-primary btn-sm btnEditar'><i class='material-icons'>edit</i></button><button class='btn btn-danger btn-sm btnBorrar'><i class='material-icons'>delete</i></button></div></div>" },
       ],

       "language": {
           url: "https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json"
       },

       dom: 'Bfrtip',
       buttons: [
           'copy', 'csv', 'excel', 'pdf', 'print'
       ],

       columnDefs: [
           {
               targets: 5,
               render: DataTable.render.datetime('DD-MM-YYYY HH:mm'),
           },
       ],
    
   });

}

//SALIDA DE MENORES DE EDAD
function PintarEgresoME(fechaI, fechaF) {

    $('#ICSMEtableEgreso').DataTable().destroy();
    var ICSMEtableEgreso = $('#ICSMEtableEgreso').DataTable({
        "ajax": {
            "url": "Informes.aspx/GetSalidaBB",
            "method": "POST",
            "datatype": "json",
            "contentType" :"application/json; charset=utf-8",
           "data": function (d) {
               return JSON.stringify({
                   d, 
                   fechaI,
                   fechaF
               })
           },
            "dataSrc": 'd.data', //obtiene los datos de la propiedad res del objeto
            "error": function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        },

        "columns": [
            { "data": "OID" },
            { "data": "ORDENSALIDA" },
            { "data": "ADNINGRES1" },
            { "data": "DOCPACIENTE" },
            { "data": "NOMPACIENTE" },
            { "data": "FECHASC" }
            //{ "defaultContent": "<div class='text-center'><div class='btn-group'><button class='btn btn-primary btn-sm btnEditar'><i class='material-icons'>edit</i></button><button class='btn btn-danger btn-sm btnBorrar'><i class='material-icons'>delete</i></button></div></div>" },
       ],

       "language": {
           url: "https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json"
       },

       dom: 'Bfrtip',
       buttons: [
           'copy', 'csv', 'excel', 'pdf', 'print'
       ],

       columnDefs: [
           {
               targets: 5,
               render: DataTable.render.datetime('DD-MM-YYYY HH:mm'),
           },
       ],
    
   });

}


//SALIDA DE FUGAS
function PintarFugas(fechaI, fechaF) {

    $('#ICStableFugas').DataTable().destroy();
    var ICSMEtableEgreso = $('#ICStableFugas').DataTable({
        "ajax": {
            "url": "Informes.aspx/PacientesFugaGet",
            "method": "POST",
            "datatype": "json",
            "contentType": "application/json; charset=utf-8",
            "data": function (d) {
                return JSON.stringify({
                    d,
                    fechaI,
                    fechaF
                })
            },
            "dataSrc": 'd.data', //obtiene los datos de la propiedad res del objeto
            "error": function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        },

        "columns": [
            { "data": "OrdenSalida" },
            { "data": "Ingreso" },
            { "data": "Cama" },
            { "data": "NombreCama" },
            { "data": "Doc" },
            { "data": "NombreCompleto" },
            { "data": "FechaIgre" },
            { "data": "FechaEgre" },
            { "data": "FechaOrden" },
            //{ "data": "OporMiIngreOrde" },
            //{ "data": "OporMiOrdEgre" },

            //{ "defaultContent": "<div class='text-center'><div class='btn-group'><button class='btn btn-primary btn-sm btnEditar'><i class='material-icons'>edit</i></button><button class='btn btn-danger btn-sm btnBorrar'><i class='material-icons'>delete</i></button></div></div>" },
        ],

        "language": {
            url: "https://cdn.datatables.net/plug-ins/1.12.1/i18n/es-ES.json"
        },

        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],

        columnDefs: [
            {
                targets: 6,
                render: DataTable.render.datetime('DD-MM-YYYY HH:mm'),   
            },
            {
                targets: 7,
                render: DataTable.render.datetime('DD-MM-YYYY HH:mm'),
            },
            {
                targets: 8,
                render: DataTable.render.datetime('DD-MM-YYYY HH:mm'),
            }
        ],

    });

}


$(document).on("click", ".btnBuscarDesprendible", function (e) {

    let fechaInicial = $("#txtFecha1").val();
    let fechaFinal = $("#txtFecha2").val();

    if (isEmpy(fechaInicial) || isEmpy(fechaFinal) ) {
        return error("Error", "Campo de fecha obligatorio")
    }

    let fechaFinalF;
    let mes = parseInt(moment(fechaFinal).format("M"));
    let dia = parseInt(moment(fechaFinal).format("D"));
    if (mes == 13) {
        mes = "01";
        fechaFinalF = (parseInt(moment(fechaFinal).format("YYYY")) + 1) + "-" + mes + "-" + dia;
    } else {
        fechaFinalF = moment(fechaFinal).format("YYYY") + "-" + mes + "-" + dia;
    }


    datos = {
        "fechaI": fechaInicial,
        "fechaF": fechaFinalF
    }

    PintarEgreso(fechaInicial, fechaFinalF);
    PintarEgresoME(fechaInicial, fechaFinalF);
    PintarFugas(fechaInicial, fechaFinalF)

    console.log(datos);
})

function isEmpy(string) {
    if (string == "" || string == null) {
        return true
    }
}



