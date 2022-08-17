
var nombreMeses = ['IndiceCero', 'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre',];

function CargarGraficoMeses(msg) {

    var informacion = "";

    exito("Notificacion", "Busqueda completada");

    $('html, body').animate({
        scrollTop: $("#contenedorCanvas").offset().top
    }, 1000);

    var años = [];
    var meses = [];
    var CantidadesMeses = [];

    datos = msg.d;

    datos.forEach((item) => {

        for (var i = 0; i < nombreMeses.length; i++) {

            if (item.IntMes == i) {

                meses.push(nombreMeses[i]);
                años.push(item.IntAñoMes);
                CantidadesMeses.push(item.IntCantMes)

            }

        }
        
    })

    var ctx = document.getElementById('myChartMeses').getContext('2d');
    var myChartMeses = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: meses,
            datasets: [{
                label: 'NUMERO DE ENCUESTAS',
                data: CantidadesMeses,
                backgroundColor: 'rgb(40, 73, 219 )',
                borderColor: 'rgb(40, 73, 219 )',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            animation: {
                duration: 500,
                easing: "easeOutQuart",
                onComplete: function () {
                    var ctx = this.chart.ctx;
                    ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontFamily, 'normal', Chart.defaults.global.defaultFontFamily);
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'bottom';

                    this.data.datasets.forEach(function (dataset) {
                        for (var i = 0; i < dataset.data.length; i++) {
                            var model = dataset._meta[Object.keys(dataset._meta)[0]].data[i]._model,
                                scale_max = dataset._meta[Object.keys(dataset._meta)[0]].data[i]._yScale.maxHeight;
                            ctx.fillStyle = '#444';
                            var y_pos = model.y - 5;
                            if ((scale_max - model.y) / scale_max >= 0.93)
                                y_pos = model.y + 20;
                            ctx.fillText(dataset.data[i], model.x, y_pos);
                        }
                    });
                }
            }
        }
    });

    informacion = "Fecha inicial: " + meses[0] + " " + años[0] + " - Fecha final: " + meses.pop() + " " + años.pop();
    $("#infoMeses").text(informacion);
}

$(document).on("click", ".btnBuscarMes", function (e) {

    $("#myChartMeses").remove();

    var canvas = document.createElement("canvas");
    canvas.id = "myChartMeses";
    canvas.width = "400";
    canvas.height = "100";
    document.getElementById("contenedorCanvas").appendChild(canvas);

    let mesInicial = $("#txtFechaMes1").val();
    let mesFinal = $("#txtFechaMes2").val();

    datos = {
        "mesI": mesInicial,
        "mesF": mesFinal
    }

    ejecutarajax("EstadisticasEC.aspx/ObtenerMeses", datos, CargarGraficoMeses)

})

function CargarGraficoDias(msg) {

    var informacion = "";

    exito("Notificacion", "Busqueda completada");

    $('html, body').animate({
        scrollTop: $("#contenedorCanvas2").offset().top
    }, 1000);

    var años = [];
    var meses = [];
    var dias = [];
    var CantidadesDias = [];

    datos = msg.d;

    datos.forEach((item) => {
        
        for (var i = 0; i < nombreMeses.length; i++) {

            if (item.IntMesDia == i) {

                meses.push(nombreMeses[i]);
                dias.push(item.IntDia);
                CantidadesDias.push(item.IntCantDia);
                años.push(item.IntAnioMesDia);

            }

        }

    })

    var ctx = document.getElementById('myChartDias').getContext('2d');
    var myChartDias = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: dias,
            datasets: [{
                label: 'NUMERO DE ENCUESTAS',
                data: CantidadesDias,
                backgroundColor: 'rgba(40, 73, 219)',
                borderColor: 'rgb(40, 73, 219)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            animation: {
                duration: 500,
                easing: "easeOutQuart",
                onComplete: function () {
                    var ctx = this.chart.ctx;
                    ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontFamily, 'normal', Chart.defaults.global.defaultFontFamily);
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'bottom';

                    this.data.datasets.forEach(function (dataset) {
                        for (var i = 0; i < dataset.data.length; i++) {
                            var model = dataset._meta[Object.keys(dataset._meta)[0]].data[i]._model,
                                scale_max = dataset._meta[Object.keys(dataset._meta)[0]].data[i]._yScale.maxHeight;
                            ctx.fillStyle = '#444';
                            var y_pos = model.y - 5;
                            if ((scale_max - model.y) / scale_max >= 0.93)
                                y_pos = model.y + 20;
                            ctx.fillText(dataset.data[i], model.x, y_pos);
                        }
                    });
                }
            }
        }
    });

    informacion = "Fecha inicial: " + dias[0] + " de " + meses[0] + " del " + años[0] + " - Fecha final: " + dias.pop() + " de " + meses.pop() + " del " + años.pop();
    $("#infoDias").text(informacion);

    let diaInicial = $("#txtFechaDia1").val();
    let diaFinal = $("#txtFechaDia2").val();

    datosID = {
        "diaI": diaInicial,
        "diaF": diaFinal
    };

    ejecutarajax("EstadisticasEC.aspx/ObtenerInfoDetalle", datosID, CargarTablaInfoDetalle);

}

function CargarTablaInfoDetalle(msg) {

    datos = msg.d;

    dtInfoDetalle = "";

    datos.forEach((item) => {

        dtInfoDetalle += `

            <tr>
                
                <td>${item.StrCedula}</td>
                <td>${item.StrNombres}</td>
                <td>${item.IntCantEmpleado}</td>

            </tr>

        `;
    })
    $("#tbInfo").html(dtInfoDetalle);
    DataTable("#tableInfo");

    $("#tableInfo").show();
    $("#exportInfoDetalle").show();
}

$(document).on("click", ".btnBuscarDia", function (e) {

    $("#myChartDias").remove();

    var canvas = document.createElement("canvas");
    canvas.id = "myChartDias";
    canvas.width = "400";
    canvas.height = "100";
    document.getElementById("contenedorCanvas2").appendChild(canvas);

    let diaInicial = $("#txtFechaDia1").val();
    let diaFinal = $("#txtFechaDia2").val();

    datos = {
        "diaI": diaInicial,
        "diaF": diaFinal
    };

    ejecutarajax("EstadisticasEC.aspx/ObtenerDias", datos, CargarGraficoDias);

})

$(document).on("click", ".exportInfoDetalle", function (e) {

    let tabla = [];

    let header = [];

    let idTabla = "#tableInfo";

    document.querySelectorAll(idTabla + " th").forEach(head => {
        header.push({ v: head.innerText, t: "s" })
    })

    tabla.push(header);

    document.querySelectorAll(idTabla + " tr").forEach(row => {
        let fila = [];
        row.querySelectorAll("td").forEach(celda => {
            fila.push({ v: celda.innerText, t: "s" })
        })
        tabla.push(fila);
    })

    tableExport = new TableExport(document.createElement("table"), {});

    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Diligenciamiento por dias ENCUESTA COVID DIARIA", ".xlsx", [], false, "hoja 1")

    exito("Notificacion", "Exportando Informacion");

})

$(document).ready(function () {

    $("#tableInfo").hide();
    $("#exportInfoDetalle").hide();

});