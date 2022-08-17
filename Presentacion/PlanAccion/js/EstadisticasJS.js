ddlUsuarios = $("#ContentPlaceHolder_ddlUsuarios");
ddlProcesos = $("#ContentPlaceHolder_ddlProcesos");
txtFecha1 = $("#txtFecha1");
txtFecha2 = $("#txtFecha2");

let myChartGrafic;

function CargarTabla(datos) {
    let dataTable = "";
    for (let i = 0; i < datos.length; i++) {
        dataTable += `
            <tr>
                <td>${datos[i].Proceso}</td>
                <td>${datos[i].EstAsignado}</td>
                <td>${datos[i].EstProceso}</td>
                <td>${datos[i].EstEvaluacion}</td>
                <td>${datos[i].EstTerminado}</td>
            </tr>
        `;
    }
    $("#tbEstadistica").html(dataTable);
    DataTable("#tableEstadisticas");
}

function CargarGrafico(datos) {
    let stats = [0,0,0,0];

    for (let i = 0; i < datos.length; i++) {
        stats[0] += datos[i].EstAsignado;
        stats[1] += datos[i].EstProceso;
        stats[2] += datos[i].EstEvaluacion;
        stats[3] += datos[i].EstTerminado;
    }

    
    let sumLista = 0;
    stats.forEach(num => {
        sumLista += num
    }); 
    


    let numAsignado = parseInt(stats[0] /sumLista * 100);
    let numProceso = parseInt(stats[1] / sumLista * 100);
    let numEvaluacion = parseInt(stats[2] / sumLista * 100);
    let numTerminado = parseInt(stats[3] / sumLista * 100);

    $("#p_asignado").text(numAsignado + "%");
    $("#p_proceso").text(numProceso + "%");
    $("#p_evaluacion").text(numEvaluacion + "%");
    $("#p_terminado").text(numTerminado + "%");

    var myChart = document.getElementById('myChart');

    ctx = myChart.getContext("2d");
    ctx.clearRect(0, 0, myChart.width, myChart.height);

    var myChartSettings = {
        type:'bar',
        tooltipFillColor: "rgba(51, 51, 51, 0.55)",
        data: {
            labels: ['Asignado', 'En proceso', 'En evaluación', 'Terminado'],
            datasets: [{
                data: stats,
                backgroundColor: [
                    "#9B59B6",
                    "#E74C3C",
                    "#26B99A",
                    "#3498DB"
                ],
                hoverBackgroundColor: [
                    "#B370CF",
                    "#E95E4F",
                    "#36CAAB",
                    "#49A9EA"
                ]
            }]
            
        },
        options: {
            legend: false,
            responsive: true
        }
    };
    myChartGrafic = new Chart(myChart, myChartSettings);
}


function CargarEstadisticas(msg) {
    datos = msg.d;
    fDatos = [];
    for (let e = 0; e < datos.length; e++) {
        aux = datos[e];
        object = {};
        for (let i = 0; i < aux.length; i++) {
            object[aux[i].Key] = aux[i].Value
        }
        fDatos.push(object);
    }
    DatosEstadistica = fDatos;

    CargarTabla(fDatos);
    CargarGrafico(fDatos);
}

function GetEstadisticasPlanesAccion() {

    $("#myChart").remove();
    var canvas = document.createElement("canvas");
    canvas.id = "myChart";
    canvas.width = "400";
    canvas.height = "100";
    document.getElementById("contenedorCanvas").appendChild(canvas);

    let datos = {
        'idUsuario': ddlUsuarios.val(),
        'proceso': ddlProcesos.val(),
        'fecha1': new Date(txtFecha1.val() || "01-01-1800"),
        'fecha2': new Date(txtFecha2.val() || "01-01-3000")
    }
    ejecutarajax("Estadisticas.aspx/GetEstadisticas", datos, CargarEstadisticas );
}

$(ddlUsuarios).on("change", GetEstadisticasPlanesAccion);
$(ddlProcesos).on("change", GetEstadisticasPlanesAccion);
$(txtFecha1).on("change", GetEstadisticasPlanesAccion);
$(txtFecha2).on("change", GetEstadisticasPlanesAccion);

$(document).ready(GetEstadisticasPlanesAccion);