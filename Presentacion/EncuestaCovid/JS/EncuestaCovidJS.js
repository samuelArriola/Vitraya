$('tr.SesionContacto').hide();

let contacto = $("input:radio[name='contactoEstrecho']");

$(document).on("change", contacto, function (e) {

    let seleccion = $("input:radio[name='contactoEstrecho']:checked").val();

    if ( seleccion == "SI" ) {
        $('tr.SesionContacto').show();
    } else if (seleccion == "NO") {
        $('tr.SesionContacto').hide();
    }

})


function MensajeGuardado() {
    exito("Notificacion", "Ha diligenciado su encuesta diaria COVID satisfactoriamente");
    //window.location.reload();
    CargarMenu();
    cargarMenuMovil();
}

$(document).on("click", ".btnGuardar", function (e) {

    if ($("input[name='valorTemperatura']").val().length == 0 ) {

        error("Notificacion", "Debe ingresar su temperatura para poder continuar");

    } else if ($("input[name='valorTemperatura']").val().length > 0) {

        $(".btnGuardar").prop('disabled', true);

        let adinamia = $("input:radio[name='adinamia']:checked").val();
        let temperatura = $("input:radio[name='temperatura']:checked").val();
        let valorTemperatura = $("input[name='valorTemperatura']").val();
        let tos = $("input:radio[name='tos']:checked").val();
        let dificultadRespiratoria = $("input:radio[name='dificultadRespiratoria']:checked").val();
        let odinofagia = $("input:radio[name='odinofagia']:checked").val();
        let dolorLumbar = $("input:radio[name='dolorLumbar']:checked").val();
        let dolorToracico = $("input:radio[name='dolorToracico']:checked").val();
        let malestarGeneral = $("input:radio[name='malestarGeneral']:checked").val();
        let perdidaOlfato = $("input:radio[name='perdidaOlfato']:checked").val();
        let perdidaGusto = $("input:radio[name='perdidaGusto']:checked").val();
        let elementosBioseguridad = $("input:radio[name='elemtnosBioseguridad']:checked").val();
        let contactoEstrecho = $("input:radio[name='contactoEstrecho']:checked").val();
        let nombreContactoEstrecho;
        let idContactoEstrecho;
        let tipoCaso;

        if (contactoEstrecho == "NO") {

            nombreContactoEstrecho = "NULL";
            idContactoEstrecho = "NULL";
            tipoCaso = "NULL";

        } else if (contactoEstrecho == "SI"){

            nombreContactoEstrecho = $("input[name='nombreContactoEstrecho']").val();
            idContactoEstrecho = $("input[name='idContactoEstrecho']").val();
            tipoCaso = $("input:radio[name='tipoCaso']:checked").val();

        }

        datos = {
            "adinamia": adinamia,
            "temperatura": temperatura,
            "valorTemperatura": valorTemperatura,
            "tos": tos,
            "dificultadRespiratoria": dificultadRespiratoria,
            "odinofagia": odinofagia,
            "dolorLumbar": dolorLumbar,
            "dolorToracico": dolorToracico,
            "malestarGeneral": malestarGeneral,
            "perdidaOlfato": perdidaOlfato,
            "perdidaGusto": perdidaGusto,
            "elementosBioseguridad": elementosBioseguridad,
            "contactoEstrecho": contactoEstrecho,
            "nombreContactoEstrecho": nombreContactoEstrecho,
            "idContactoEstrecho": idContactoEstrecho,
            "tipoCaso": tipoCaso
        }

        $("input[name = 'valorTemperatura']").val(" ");
        ejecutarajax("EncuestaCovid.aspx/InsertarEncuesta", datos, MensajeGuardado)
    }

})

