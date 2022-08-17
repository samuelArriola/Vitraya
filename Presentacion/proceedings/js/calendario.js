var date;
var hora;
var evento
var event;
var idEvento = -1;




function editEvent(event) {
    console.log(event)
    if ($("#ddlNombreC").val() == -1) return;
   
    $('#event-modal #lugar').val(event ? event.location : '');
    if (event.id) {
        $.ajax({
            url: "CreationOfCommittees.aspx/GetEvento",
            data: JSON.stringify({ id: event.id }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                evento = JSON.parse(msg.d);
                $('#txtFecha').val(moment(evento.DtmFechaInicio).format("HH:MM"));
                idEvento = evento.IntOidGNEvento;
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    }
    $('#event-modal').modal();
    date = event.startDate;
}

function deleteEvent(event) {
    var dataSource = $('#calendar').data('calendar').getDataSource();

    for (var i in dataSource) {
        if (dataSource[i].id == event.id) {
            dataSource.splice(i, 1);
            $.ajax({
                url: "CreationOfCommittees.aspx/DeleteEvento",
                data: JSON.stringify({ id: event.id }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                success: function (msg) {
                    evento = JSON.parse(msg.d);
                    $('#txtFecha').val(moment(evento.DtmFechaInicio).format("HH:MM"));
                    idEvento = evento.IntOidGNEvento;
                    console.log(msg.d);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });

            break;
        }
        
    }
    cargarEventos();
}

const  saveEvent = () =>
{

    if ($("#ContentPlaceHolder_txtFecha").val() == '') {
        return;
    }

    event = {
        id: idEvento,
        name: $('#ContentPlaceHolder_ddlNombreC option:selected').text(),
        location: $('#event-modal #lugar').val(),
        startDate: date,
        endDate: date,
    }

    hora = new Date("1970-01-01T" + $('#ContentPlaceHolder_txtFecha').val());

    date2 = date;

    date2.setHours(hora.getHours());
    date2.setMinutes(hora.getMinutes());

    var event2 = {
        id: idEvento,
        name: $('#ContentPlaceHolder_ddlNombreC option:selected').text(),
        location: $('#event-modal #lugar').val(),
        startDate: date2,
        endDate: date2,
        idGNModulo: 3,
        idCronograma: $("#ContentPlaceHolder_ddlNombreC").val()
    }


    datos = JSON.stringify(event2);
    
    
   
    var dataSource = $('#calendar').data('calendar').getDataSource();

    if (event.id > -1) {
        for (var i in dataSource) {
            if (dataSource[i].id == event.id) {
                dataSource[i].name = event.name;
                dataSource[i].location = event.location;
                dataSource[i].startDate = Date.parse(moment(event.startDate).format("YYYY-MM-DD"));
                dataSource[i].endDate = Date.parse(moment(event.startDate).format("YYYY-MM-DD"));

                

                $.ajax({
                    url: "CreationOfCommittees.aspx/updateEvento",
                    data: datos,
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    type: "POST",
                    async: false,
                    success: function (msg) {
                        event.id = JSON.parse(msg.d).OidGnEvento;
                    },
                    error: function (result) {
                        alert("ERROR " + result.status + ' ' + result.statusText);
                    }
                });
            }
        }
        cargarEventos();
    }
    else {

        $.ajax({
            url: "CreationOfCommittees.aspx/setCronogramaComite",
            data: datos,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            success: function (msg) {
                console.log(msg.d)
                event.id = (JSON.parse(msg.d)).IntOidGNEvento;
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
        event.startDate = Date.parse(moment(event.startDate).format("YYYY-MM-DD"));
        event.endDate = Date.parse(moment(event.endDate).format("YYYY-MM-DD"));
        dataSource.push(event);
    }

    $('#calendar').data('calendar').setDataSource(dataSource);
    $('#event-modal').modal('hide');
    idEvento = -1;
    
}

$(function () {



    var currentYear = new Date().getFullYear();

    $('#calendar').calendar({
        enableContextMenu: true,
        enableRangeSelection: true,
        contextMenuItems: [
            {
                text: 'Cambiar',
                click: editEvent
            },
            {
                text: 'Eliminar',
                click: deleteEvent
            }
        ],
        selectRange: function (e) {
            editEvent({ startDate: e.startDate, endDate: e.endDate });
        },
        
        dayContextMenu: function (e) {
            $(e.element).popover('hide');
        },
        dataSource: [ 
        ]
    });

    cargarEventos();

    $('#save-event').click(function () {
        saveEvent();
    });
});

function cargarEventos(){
    $.ajax({
        url: "CreationOfCommittees.aspx/GetEventos",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (msg) {
            var dataSource = [];
            eventos = JSON.parse(msg.d);
            
            for (i = 0; i < eventos.length; i++){
                var evento = {
                    id: eventos[i].IntOidGNEvento,
                    name: eventos[i].StrContenido + "\n" + eventos[i].StrLugar + "\n" + moment(eventos[i].DtmFechaInicio).format('DD/MM/YY HH:mm'),
                    location: eventos[i].StrLugar,
                    startDate: Date.parse(moment(eventos[i].DtmFechaInicio).format('YYYY-MM-DD')),
                    endDate: Date.parse(moment(eventos[i].DtmFechaFinal).format('YYYY-MM-DD')),
                }
                dataSource.push(evento);
            }
            $('#calendar').data('calendar').setDataSource(dataSource);
            
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}


