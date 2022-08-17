
document.querySelector('.custom-file-input').addEventListener('change', function (e) {
    var fileName = document.querySelector('.custom-file-input').files[0].name;
    var nextSibling = e.target.nextElementSibling
    nextSibling.innerText = fileName
})

function error(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'error',
        styling: 'bootstrap3',
        delay: 1000
    });
}
function exito(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'success',
        styling: 'bootstrap3',
        delay: 3000
    });
}

const btnGuardarClick = (e) => {
    e.preventDefault();
    let dtmfechaReunion = new Date($("#ContentPlaceHolder_txtFechaReunion").val());
    let dtmHorInicio = new Date("1970-01-01T" + $("#ContentPlaceHolder_txtHorainicio").val());
    let dtmHoraFinal = new Date("1970-01-01T" + $("#ContentPlaceHolder_txtHorafinal").val());
    let strLugarReunion = $("#ContentPlaceHolder_txtLugarReunion").val();

    if (dtmfechaReunion == "") {
        error("Error", "El campo Fecha de reunión falta por llenar")
        return;
    }
    if (dtmHorInicio == "") {
        error("Error", "El campo Hora de inicio falta por llenar")
        return;
    }
    if (dtmHoraFinal == "") {
        error("Error", "El campo Hora de finalización falta por llenar")
        return;
    }
    if (strLugarReunion == "") {
        error("Error", "El campo Lugar de reunión falta por llenar")
        return;
    }
    $(".modal").modal();
}

const btnGuardarCabeceraClick = (e) => {
    let dtmfechaReunion = new Date($("#ContentPlaceHolder_txtFechaReunion").val());
    let dtmHorInicio = new Date("1970-01-01T" + $("#ContentPlaceHolder_txtHorainicio").val());
    let dtmHoraFinal = new Date("1970-01-01T" + $("#ContentPlaceHolder_txtHorafinal").val());
    let strLugarReunion = $("#ContentPlaceHolder_txtLugarReunion").val();

    if (dtmfechaReunion == "") {
        error("Error", "El campo Fecha de reunión falta por llenar")
        return;
    }
    if (dtmHorInicio == "") {
        error("Error", "El campo Hora de inicio falta por llenar")
        return;
    }
    if (dtmHoraFinal == "") {
        error("Error", "El campo Hora de finalización falta por llenar")
        return;
    }
    if (strLugarReunion == "") {
        error("Error", "El campo Lugar de reunión falta por llenar")
        return;
    }

    let CabeceraDelActa = {
        DtmfechaReunion: dtmfechaReunion,
        DtmHorInicio: dtmHorInicio,
        DtmHoraFinal: dtmHoraFinal,
        StrLugarReunion: strLugarReunion,
    }

    datos = JSON.stringify(CabeceraDelActa);

    $.ajax({
        url: "RecordMinutes.aspx/cerrarActa",
        data: datos,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (msg) {
            if (msg.d == 1) {
                window.location.href = "MisActas.aspx";
            }
            else {
                error("Error", msg.d);
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}




const btnGuardarTemaClick = (e) => {
    e.preventDefault();
    let strDesrrolloTema = tinymce.activeEditor.getContent();
    let strNomTema = $("#ContentPlaceHolder_lbNomTema").text();

    if (strDesrrolloTema == "") {
        error("Error", "El desarrollo del tema no tiene ningun contenido");
        return;
    }

    let tema = { StrDesarrollo: strDesrrolloTema, StrNomTema: strNomTema };

    let datos = JSON.stringify(tema);

    $.ajax({
        url: "RecordMinutes.aspx/guardarTema",
        data: datos,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        success: function (msg) {
            if (msg.d == 1) {
                exito("Exito", "El desarrollo del tema fue guardado satisfactoriamente");
            }
            else {
                error("error", msg.d)
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });

    $("#ContentPlaceHolder_taActividad").removeAttr("disabled");
    $("#ContentPlaceHolder_taSoporteActividad").removeAttr("disabled");
    $("#ContentPlaceHolder_ddlReposnsableSeguimiento").removeAttr("disabled");
    $("#ContentPlaceHolder_ddlResponsableActividad").removeAttr("disabled");
    $("#ContentPlaceHolder_ddlResposableAprueba").removeAttr("disabled");
    $("#ContentPlaceHolder_txtFechaLimiteCompromiso").removeAttr("disabled");
    $("#ContentPlaceHolder_ddlProceso").removeAttr("disabled");
    $("#ContentPlaceHolder_btnAgregarCompromiso").removeAttr("disabled");
}




$("#ContentPlaceHolder_btnGuardar").click(btnGuardarClick);
$("#CerrarActa").click(btnGuardarCabeceraClick);
$("#ContentPlaceHolder_btnGuardarTema").click(btnGuardarTemaClick);