

function error(titulo, texto) { // mensaje de error
    new PNotify({
        title: titulo,
        text: texto,
        type: 'error',
        styling: 'bootstrap3',
        delay: 1000
    });
}

function exito(titulo, texto) { // mensaje de exito
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
//        if ($('#ModalRegDef').is(':visible'))
//            window.location.href = "ConsultarRegistros.aspx"
//    }
//});

$(".ModalRegDef").on("hidden.bs.modal", function () {
    window.location.href = "ConsultarRegistros.aspx"
});

document.getElementById("ContentPlaceHolder_idPacDef").addEventListener("keypress", soloNumeros, false); // evitar caracteres no numéricos en el campo id paciente. 
function soloNumeros(e) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
} 
$("form").on("submit", function (e) { e.preventDefault() })  //quitar el evento submit al form.

$("#btnGuaRegDefFil").on("click", (e) => { // agregar evento clic al boton guardar.
    e.preventDefault();

    var inpsNacViv = [...document.getElementsByClassName("inpDef")]; 
    var control = false;
    for (var wi = 0, n = inpsNacViv.length; wi < n; wi++) { // validacion de inputs vacio.
        if (inpsNacViv[wi].value == "")
            control = true
    }

    if (control)
        error("error", "por favor complete los campos.")
    else {
        //cargarInfModal(); 
        //$("#ModalRegDef").modal(); // activar el modal 
        var RegDef = {
            "StrTipDef": document.getElementById("ContentPlaceHolder_tipDef").value,
            "DateFecDef": document.getElementById("ContentPlaceHolder_fechaDefuncion").value,
            "StrNomPac": document.getElementById("ContentPlaceHolder_nomPacDef").value,
            "DoubleIdPaciente": parseInt(document.getElementById("ContentPlaceHolder_idPacDef").value),
            "IntOIdCRCodRuaf": 0,
            "DoubleGNCodUsu": parseInt(document.getElementById("ContentPlaceHolder_IdDocDef").value),
            "StrNomDoc": document.getElementById("ContentPlaceHolder_NomDocDef").value,
            "StrServicio": document.getElementById("ContentPlaceHolder_ServDef").value,
        }

        $.ajax({ // metodo para enviar datos al servidor.
            url: "ActualizarDef.aspx/ActualizarRegDef",
            data: JSON.stringify({ "RegDef": RegDef }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                cargarInfModal(JSON.parse(msg.d));
                exito("success", "Registro actualizado");
                $("#ModalRegDef").modal(); // activar modal
                
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    }
})


function cargarInfModal(regDef) {
    let elemento = document.getElementById("modalRegDefTex");

    elemento.innerHTML = '' +
        '<strong>Tipo Defunción:&nbsp </strong> ' + regDef[0].StrTipDef + '<br>' +
        '<strong>Fecha: </strong>&nbsp' + moment(regDef[0].DateFecDef).format("YYYY-MM-DD HH:mm") + '<br>' +
        '<strong>Nombre paciente:&nbsp </strong>' + regDef[0].StrNomPac + '<br>' +
        '<strong>Documento paciente: &nbsp</strong> ' + regDef[0].DoubleIdPaciente + '<br>' +
        '<strong>Código Ruaf: &nbsp</strong>' + regDef[1] + '<br>' +
        '<strong>Documento Médico:&nbsp</strong>' + regDef[0].DoubleGNCodUsu + '<br>' +
        '<strong>Nombre Médico:&nbsp </strong>' + regDef[0].StrNomDoc + '<br>' +
        '<strong>Servicio:&nbsp </strong> ' + regDef[0].StrServicio + '<br>';

}


$("#cerrarModal").on("click", (e) => {
    window.location.href = "ConsultarRegistros.aspx";
})