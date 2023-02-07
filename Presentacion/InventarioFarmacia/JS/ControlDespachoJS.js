console.log('Conectado ControlDespachoJS');

let ListaSuministro = document.getElementById('CDBodySuministro');
let templateListaSuministro = document.getElementById('CDSuministroTemplate').content;
const fragment = document.createDocumentFragment();

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

$(document).ready(function () {
    $(".spinner-border_list_suministro").hide();
    $("#CDBusSuministro").focus();
});

//MOSTRAR SUMINISTRO
$('#CDBusSuministro').on("focusout", function () {
    let CDdiSuministro = $('#CDBusSuministro').val();
    if (CDdiSuministro.length > 19) {
        return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 19");
    } else {
        if (isEmpy(CDdiSuministro)) {
            console.log('Campo vacio');
            LimpiarCabeceraSuministro();

        } else {

            $(".spinner-border_list_suministro").show();
            ejecutarajax("ControlDespacho.aspx/CountSuministros", { OIDSUMINISTRO: CDdiSuministro }, ResCountSuministros)
            
        }
    }
})

function ResCountSuministros(res) {
    res = res.d; 
    let CDdiSuministro = $('#CDBusSuministro').val();

    if (res < 1) {
        ejecutarajax("ControlDespacho.aspx/SuministrosGet", { oid_suministro: CDdiSuministro }, ResSuministrosGet)
    } else {
        $(".spinner-border_list_suministro").hide();
        LimpiarCabeceraSuministro();
        error("Notificacion", "Ya el suministro a sido entrgado");
    }

}



function ResSuministrosGet(res) {
    var res2 = res.d;
   
    if (res2.length < 1) {
        $(".spinner-border_list_suministro").hide();
        LimpiarCabeceraSuministro();
        ListaSalidaListaSuministroinnerHTML = "";
        return $("#CDBodySuministro").html(" <tr> <td colspan = '8'> <h5> UPss!! No hay resultados</h5> </td > </tr> ");

    }
    LimpiarCabeceraSuministro();
    $("#div_firmar").css('visibility', 'hidden');

    cabeceraSuministro(res2[0])
    if (res2[0].AREA_SERVICIO == "AREA DE URGENCIAS") {
        $("#cdOber_prolon").html(`
            <label class= "p-2 bd-highlight" >
                <span>* ¿Observación Prolongada?</span>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="RadiosObsPro" id="cdObservaciónSi" value="SI" />
                    <label class="form-check-label" for="exampleRadios1">Si</label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="RadiosObsPro" id="cdObservaciónNo" value="NO" />
                    <label class="form-check-label" for="exampleRadios2">No</label>
                </div>
            </label >
            `)
    } else {
        $("#cdOber_prolon").html("")
    }

    tablaPintarSuministro(res);

}

function tablaPintarSuministro(res) {

    res = res.d;
    ListaSuministro.innerHTML = "";


    res.forEach( (item) => {
        templateListaSuministro.querySelectorAll('td')[0].textContent = item.CODIGO_MED;
        templateListaSuministro.querySelectorAll('td')[1].textContent = item.DESCRIP_MED;
        templateListaSuministro.querySelectorAll('td')[2].textContent = item.POSOLOGIA;
        templateListaSuministro.querySelectorAll('td')[3].textContent = item.DOSIS_MED;
        templateListaSuministro.querySelectorAll('td')[4].textContent = item.unidad_medida;
        templateListaSuministro.querySelectorAll('td')[5].textContent = item.FECUENCIA_MED;
        templateListaSuministro.querySelectorAll('td')[6].textContent = item.VIA_ADMIN_MED;
        templateListaSuministro.querySelectorAll('td')[7].textContent = item.OID_LOTE;
        templateListaSuministro.querySelectorAll('td')[8].textContent = item.CANTIDAD;

        const clone = templateListaSuministro.cloneNode(true);
        fragment.appendChild(clone);

    });

    ListaSuministro.appendChild(fragment);
    $(".spinner-border_list_suministro").hide();
}

const FocusFirma = document.getElementById('MCDFirma');
    FocusFirma.addEventListener('shown.bs.modal', function () {
    $('#CD_user').focus();
    })

function cabeceraSuministro(data) {
    $("#CDarea_servicio").val(data.AREA_SERVICIO);
    $("#CDfec_doc").val( moment(data.FEC_DOCUMENTO).format("YYYY/MM/DD HH:mm"));
    $("#CDcama").val(data.CAMA);
    $("#CDiden").val(data.DOCUMENTO_PAC);
    $("#CDInoms").val(data.NOMBRE_PAC);
    $("#CDconsec").val(data.CONSECUTIVO);
}

function LimpiarCabeceraSuministro() {
    ListaSuministro.innerHTML = "";
    $("#CDarea_servicio").val("");
    $("#CDfec_doc").val("");
    $("#CDcama").val("");
    $("#CDiden").val("");
    $("#CDInoms").val("");
    $("#CDconsec").val("");


    $("input[name='checkTip']").each(function () {
        $(this).prop("checked", false);
    });
    $("input[name='RadiosObsPro']").prop("checked", false);
    $(".spinner-border_list_suministro").hide();
}

function isEmpy(string) {
    if (string == "" || string == null) {
        return true
    }
}

$(document).on("click", "input[name='checkTip']", function () {
    if ($("input[name='checkTip']").length == $("input[name='checkTip']:checked").length) {
        $("#div_firmar").css( 'visibility' ,'visible' );
    } else {
        $("#div_firmar").css( 'visibility','hidden' );
    }
});

function validarVacio() {

    if (isEmpy($('#CD_user').val()) || isEmpy($('#CD_pass').val()) || isEmpy($('#CDarea_servicio').val()) || isEmpy($('#CDfec_doc').val()) || isEmpy($('#CDBusSuministro').val()) || 
        $('input[type=radio]:checked').length < 1 || isEmpy($('#CDiden').val()) || isEmpy($('#CDInoms').val()) || $("input[name='checkTip']").length != $("input[name='checkTip']:checked").length) {
        return true;
    }
}
function validarVacio2() {
    if (isEmpy($('#CD_user').val()) || isEmpy($('#CD_pass').val()) || isEmpy($('#CDarea_servicio').val()) || isEmpy($('#CDfec_doc').val()) || isEmpy($('#CDconsec').val()) || isEmpy($('#CDBusSuministro').val()) ||
       isEmpy($('#CDiden').val()) || isEmpy($('#CDInoms').val()) || $("input[name='checkTip']").length != $("input[name='checkTip']:checked").length) {
        return true;
    }
}

//BOTÓN DE FIRMAR
$('#CD_btn_firmar').on('click', () => {

    var res = $("#CDarea_servicio").val() == "AREA DE URGENCIAS" ? validarVacio() : validarVacio2();
    if (res) {
        error("Notificacion", "Los campos con (*) no deben estar vaciós, por llavor rellene todo los campos.");
    } else {
        
        data = {
            user: $("#CD_user").val(),
            pass: $("#CD_pass").val()
        }
        console.log(data);

        ejecutarajax("ControlDespacho.aspx/Login", data, ResLoginGet)

    }

});

function ResLoginGet(res) {
    res = res.d;
    if (res == true) {

        data = {
            OIDSUMINISTRO: $("#CDBusSuministro").val(),
            CONSECUTIVO: $("#CDconsec").val(),
            DOCUMENTO_PAC: $("#CDiden").val(),
            USUARIOFIRMA: $("#CD_user").val(),
            CPAC: $("#cdCPAC").val(),
            CCANT: $("#cdCCANT").val(),
            CVIAADMIN: $("#cdCVIAADMIN").val(),
            CDOSIS: $("#cdCDOSIS").val(),
            OBSPRO: isEmpy($('input[name="RadiosObsPro"]:checked').val()) ? "NO APLICA" : $('input[name="RadiosObsPro"]:checked').val(),
            CDInoms:  $("#CDInoms").val(),
        }
        ejecutarajax("ControlDespacho.aspx/InserSuministro", data, ResInserSuministro)

    } else {
       error("Notificacion", "usuaruia o contraseña incorrecto");
    }
}

function ResInserSuministro() {
    $("#MCDFirma").modal('hide');
    LimpiarCabeceraSuministro();
    exito("Notificacion", "Firma Exitosa");
}