

function error(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'error',
        styling: 'bootstrap3',
        delay: 3000
    });
}

function Advertencia(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'advertencia',
        styling: 'bootstrap3',
        delay: 4000
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

//$(document).keyup(function (e) { // actualizar form, luego de cerrar modal. 
//    if (e.keyCode == 27) {
//        if ($('#ModalRegNacViv').is(':visible'))
//            window.location.reload();
//    }
//});

$(".modal").on("hidden.bs.modal", function () {
    window.location.reload();
});

//enviar mensaje en caso de proporcianar datos poco usuales en el formulario
document.getElementById("edadGesNacimiento").addEventListener("blur", intervaloEdadGes, false);
function intervaloEdadGes() {
    if (parseInt(document.getElementById("edadGesNacimiento").value) > 42 || parseInt(document.getElementById("edadGesNacimiento").value) < 20) {
        error("error","Valor poco usual para Edad Gestacional, por favor verifique")
    }
}
document.getElementById("pesoRN").addEventListener("blur", intervaloPesoRN, false);
function intervaloPesoRN() {
    if (parseInt(document.getElementById("pesoRN").value) > 5000 || parseInt(document.getElementById("pesoRN").value) < 1500) {
        error("error", "Valor poco usual para Peso RN, por favor verifique")
    }
}

document.getElementById("idMadreNV").addEventListener("blur", VerificarReg, false);
function VerificarReg() { // verificar si una madre ya fue registrada para enviar mensaje de advertencia en casi de ser duplicado.

    let datos = {
        'IdMadre': document.getElementById("idMadreNV").value
    }


    $.ajax({ // metodo para enviar los datos al servidor.
        url: "RegistroNacidoVivo.aspx/VerificarIdMadre",
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {

            if (msg.d)
                Advertencia("advertencia","Esta Madre se encuentra con registro(s), por favor verifique que no se trate de informacion duplicada. Puede ir a CONSULTAS y verificar.")
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

// agregar evento a los inputs que solo es permitido caracter numérico
document.getElementById("idMadreNV").addEventListener("keypress", soloNumeros, false);
document.getElementById("edadGesNacimiento").addEventListener("keypress", soloNumeros, false);
document.getElementById("pesoRN").addEventListener("keypress", soloNumeros, false);
function soloNumeros(e) { // evitar caracteres diferentes de numéricos
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}

//boton guardar. par cargar info al modal-
$("#ContentPlaceHolder_btnGuaRegNacVivFil").on("click", (e) => {
    e.preventDefault();

    var inpsNacViv = [...document.getElementsByClassName("inpNacViv")];
    var control = false;
    for (var wi = 0, n = inpsNacViv.length; wi < n; wi++) { // validar inputs vacio.
        if (inpsNacViv[wi].value == "")
            control = true
    }

    if (control)
        error("error","por favor complete los campos.")
    else {

        var RegNacViv = {
            "DoubleIdMadre": parseInt(document.getElementById("idMadreNV").value),
            "StrNomMadre": document.getElementById("NomMadNV").value,
            "StrTipNac": document.getElementById("tipoNacimiento").value,
            "DateFecNac": document.getElementById("ContentPlaceHolder_fechaNacimiento").value,
            "IntCRCodRuaf": 0,
            "IntEdGesNac": parseInt(document.getElementById("edadGesNacimiento").value),
            "DoubleGNCodUsu": parseInt(document.getElementById("ContentPlaceHolder_IdDocNacViv").value),
            "StrNomDoc": document.getElementById("ContentPlaceHolder_NomDocNacViv").value,
            "IntPesoRn": parseInt(document.getElementById("pesoRN").value),
            "FloatTallaRN": parseFloat(document.getElementById("tallaRN").value),
            "StrSexo": document.getElementById("Sexo").value,
        }

        $.ajax({ // metodo para enviar los datos al servidor.
            url: "RegistroNacidoVivo.aspx/guardarRegNacViv",
            data: JSON.stringify({ "RegNacViv": RegNacViv }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {

                exito("success", "Registro Guardado")
                cargarInfModal(JSON.parse(msg.d));
                $("#ModalRegNacViv").modal(); // activar modal

                //alert("guardado");
                //window.location.href = "RegistroNacidoVivo.aspx"
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });

        //cargarInfModal();
        //$("#ModalRegNacViv").modal(); // activar modal
    }
})

$("#CerrarModal").on("click", (e) => {  
    window.location.href = "RegistroNacidoVivo.aspx"
})

   //cargar información al modal.
function cargarInfModal(RegDef) {
    let elemento = document.getElementById("modalRegNacVivTex")

    elemento.innerHTML = '' +
        '<strong>Documento de la Madre:&nbsp</strong> ' + RegDef[0].DoubleIdMadre + '<br>' +
        '<strong>Nombre Madre:&nbsp</strong> ' + RegDef[0].StrNomMadre + '<br>' +
        '<strong>Tipo nacimiento:&nbsp</strong> ' + RegDef[0].StrTipNac+'<br>' +
        '<strong>Fecha nacimiento:&nbsp</strong> ' + moment(RegDef[0].DateFecNac).format("YYYY-MM-DD HH:mm") +'<br>' +
        '<strong>Código Ruaf:&nbsp</strong> ' + RegDef[1] + '<br>' +
        '<strong>Edad gestacional:&nbsp</strong> ' + RegDef[0].IntEdGesNac + '<br>' +
        '<strong>Documento Doctor:&nbsp</strong> ' + RegDef[0].DoubleGNCodUsu + '<br>' +
        '<strong>Nombre Doctor:&nbsp</strong>' + RegDef[0].StrNomDoc + '<br>' +
        '<strong>Peso Nacido Vivo:&nbsp</strong> ' + RegDef[0].StrTipNac+'<br>' +
        '<strong>Talla Nacido Vivo:&nbsp</strong> ' + RegDef[0].FloatTallaRN + '<br>' +
        '<strong>Sexo:&nbsp</strong> ' + RegDef[0].StrSexo + '<br>';
}


//// boton guardar(modal) para enviar datos al servidor.
//$("#btnGuaRegNacViv").on("click", (e) => {

//    var RegNacViv = {
//        "DoubleIdMadre": parseInt(document.getElementById("idMadreNV").value),
//        "StrNomMadre": document.getElementById("NomMadNV").value,
//        "StrTipNac": document.getElementById("tipoNacimiento").value,
//        "DateFecNac": document.getElementById("fechaNacimiento").value,
//        "IntCRCodRuaf": 0,
//        "IntEdGesNac": parseInt(document.getElementById("edadGesNacimiento").value),
//        "DoubleGNCodUsu": parseInt(document.getElementById("IdDocNacViv").value),
//        "StrNomDoc": document.getElementById("NomDocNacViv").value,
//        "IntPesoRn": parseInt(document.getElementById("pesoRN").value),
//        "FloatTallaRN": parseFloat(document.getElementById("tallaRN").value),
//    }

//    $.ajax({ // metodo para enviar los datos al servidor.
//        url: "RegistroNacidoVivo.aspx/guardarRegNacViv",
//        data: JSON.stringify({ "RegNacViv": RegNacViv }),
//        dataType: "json",
//        type: "POST",
//        contentType: "application/json; charset=utf-8",
//        success: function (msg) {
//            alert("guardado");
//            window.location.href = "RegistroNacidoVivo.aspx"
//        },
//        error: function (result) {
//            alert("ERROR " + result.status + ' ' + result.statusText);
//        }
//    });

//})

