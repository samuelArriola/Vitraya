


let EstadisticaPlanes;


function CargarEstadisticasPlanes() {
    ejecutarajax(
        "Index.aspx/GetEstadisticas",
        {},
        function (datos) {
            EstadisticaPlanes = datos.d;


            var myChart = document.getElementById('myChartPlanes');

            ctx = myChart.getContext("2d");
            ctx.clearRect(0, 0, myChart.width, myChart.height);

            stats = [EstadisticaPlanes.EstAsignado, EstadisticaPlanes.EstProceso, EstadisticaPlanes.EstEvaluacion, EstadisticaPlanes.EstTerminado];
            total = stats.reduce((a, b) => a + b, 0)

            var myChartSettings = {
                type: 'bar',
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
                    responsive: true,
                    animation: {
                        duration: 50,
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
                                    ctx.fillStyle = '#000';
                                    var y_pos = model.y - 5;
                                    if ((scale_max - model.y) / scale_max >= 0.93)
                                        y_pos = model.y + 20;
                                    ctx.lineWidth = 2;
                                    ctx.fillText(dataset.data[i], model.x, y_pos);
                                }
                            });
                        }
                    },
                    tooltips: {
                        enabled: false
                    }
                }
            };
            myChartGrafic = new Chart(myChart, myChartSettings);

            $("#totalPlanes").text(`Total ${total}`)
        }
    )
}

function CargarInfoCapacitaciones() {
    ejecutarajax(
        "Index.aspx/GetEstadisticasCapacitaciones",
        {},
        function (msg) {
            EstadisticaCaps = msg.d;
            var myChart = document.getElementById('myChartCapacitaciones');

            ctx = myChart.getContext("2d");
            ctx.clearRect(0, 0, myChart.width, myChart.height);

            stats = [EstadisticaCaps.Inasistido, EstadisticaCaps.NoFirmado, EstadisticaCaps.Firmado];
            total = stats.reduce((a, b) => a + b, 0)
           
            var myChart = document.getElementById('myChartCapacitaciones');

            var myChartSettings = {
                type: 'bar',
                tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                data: {
                    labels: ['Inasistidas', 'No firmadas', 'Firmada'],
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
                    responsive: true,
                    animation: {
                        duration: 50,
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
                                    ctx.fillStyle = '#000';
                                    var y_pos = model.y - 5;
                                    if ((scale_max - model.y) / scale_max >= 0.93)
                                        y_pos = model.y + 20;
                                    ctx.lineWidth = 2;
                                    ctx.fillText(dataset.data[i], model.x, y_pos);
                                }
                            });
                        }
                    },
                    tooltips: {
                        enabled: false
                    }
                }
            };
            myChartGrafic = new Chart(myChart, myChartSettings);
            $("#totalCapacitaciones").text(`Total ${total}`)
        }
    )
}

function CargarInfoActas() {
    ejecutarajax(
        "Index.aspx/GetEstadisticaActas",
        {},
        function (msg) {
            EstadisticaCaps = msg.d;
            var myChart = document.getElementById('myChartActas');

            ctx = myChart.getContext("2d");
            ctx.clearRect(0, 0, myChart.width, myChart.height);

            stats = [EstadisticaCaps.Inasistido, EstadisticaCaps.NoFirmado, EstadisticaCaps.Firmado];
            total = stats.reduce((a, b) => a + b, 0)


            var myChartSettings = {
                type: 'bar',
                tooltipFillColor: "rgba(51, 51, 51, 0.55)",
                data: {
                    labels: ['Inasistidas', 'No firmadas', 'Firmada'],
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
                    responsive: true,
                    animation: {
                        duration: 50,
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
                                    ctx.fillStyle = '#000';
                                    var y_pos = model.y - 5;
                                    if ((scale_max - model.y) / scale_max >= 0.93)
                                        y_pos = model.y + 20;
                                    ctx.lineWidth = 2;
                                    ctx.fillText(dataset.data[i], model.x, y_pos);
                                }
                            });
                        }
                    },
                    tooltips: {
                        enabled: false
                    }
                }
            };
            myChartGrafic = new Chart(myChart, myChartSettings);
            $("#totalActas").text(`Total ${total}`)
        }
    )
}

$(document).ready(function (e) {
    CargarEstadisticasPlanes();
    CargarInfoCapacitaciones();
    CargarInfoActas();
});



