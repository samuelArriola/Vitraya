
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
$("form").on("submit", function (e) { e.preventDefault() }) // evitar evento submit 

$(document).keyup(function (e) { // actualizar form, luego de cerrar modal. 
    if (e.keyCode == 27) {
        if ($('#ModalRegNacViv').is(':visible'))
            window.location.href = "ConsultarRegistros.aspx"
    }
});

//$(".modal").on("click", function (e) {
//    window.location.href = "ConsultarRegistros.aspx"
//})

$(".modal").on("hidden.bs.modal", function () {
    window.location.href = "ConsultarRegistros.aspx"
});

// agregar evento keypress a los inputs que solo admiten caracteres numéricos.
document.getElementById("ContentPlaceHolder_idMadreNV").addEventListener("keypress", soloNumeros, false);
document.getElementById("ContentPlaceHolder_edadGesNacimiento").addEventListener("keypress", soloNumeros, false);
document.getElementById("ContentPlaceHolder_pesoRN").addEventListener("keypress", soloNumeros, false);
function soloNumeros(e) { //función para limitar a caracteres numéricos, a los inputs correspondientes.
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}

//agregar evento click al boton guardar para cargar la info al modal.
$("#btnGuaRegNacVivFil").on("click", (e) => {
    e.preventDefault();

    var inpsNacViv = [...document.getElementsByClassName("inpNacViv")];
    var control = false; 
    for (var wi = 0, n = inpsNacViv.length; wi < n; wi++) { // validar inputs vacio.
        if (inpsNacViv[wi].value == "")
            control = true
    }

    if (control)
        error("error", "por favor complete los campos.")
    else {
        //cargarInfModal();
        //$("#ModalRegNacViv").modal(); //activar modal.
        var RegNacViv = { // crear el registro.
            "DoubleIdMadre": parseInt(document.getElementById("ContentPlaceHolder_idMadreNV").value),
            "StrNomMadre": document.getElementById("ContentPlaceHolder_NomMadNV").value,
            "StrTipNac": document.getElementById("ContentPlaceHolder_tipoNacimiento").value,
            "DateFecNac": document.getElementById("ContentPlaceHolder_fechaNacimiento").value,
            "IntCRCodRuaf": 0,
            "IntEdGesNac": parseInt(document.getElementById("ContentPlaceHolder_edadGesNacimiento").value),
            "DoubleGNCodUsu": parseInt(document.getElementById("ContentPlaceHolder_IdDocNacViv").value),
            "StrNomDoc": document.getElementById("ContentPlaceHolder_NomDocNacViv").value,
            "IntPesoRn": parseInt(document.getElementById("ContentPlaceHolder_pesoRN").value),
            "FloatTallaRN": parseFloat(document.getElementById("ContentPlaceHolder_tallaRN").value),
            "StrSexo": document.getElementById("ContentPlaceHolder_Sexo").value,
        }

        $.ajax({ // metodo para enviar los datos al servidor.
            url: "ActualizarNacViv.aspx/updateRegNacViv",
            data: JSON.stringify({ "RegNacViv": RegNacViv }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                cargarInfModal(JSON.parse(msg.d));
                exito("success", "Registro actualizado");

                $("#ModalRegNacViv").modal(); // activar modal

                //if (!$('#ModalRegNacViv').is(':visible'))
                //    window.location.href = "ConsultarRegistros.aspx"
                
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });

    }
})

// boton guardar(modal) para enviar datos al servidor. 
$("#closeModal").on("click", (e) => {
    window.location.href = "ConsultarRegistros.aspx"
})

// función paracargar datos digitados al modal.
function cargarInfModal(RegDef) {
    let elemento = document.getElementById("modalRegNacVivTex")

    elemento.innerHTML = '' +
        '<strong>Documento de la Madre:&nbsp</strong> ' + RegDef[0].DoubleIdMadre + '<br>' +
        '<strong>Nombre Madre:&nbsp</strong> ' + RegDef[0].StrNomMadre + '<br>' +
        '<strong>Tipo nacimiento:&nbsp</strong> ' + RegDef[0].StrTipNac + '<br>' +
        '<strong>Fecha nacimiento:&nbsp</strong> ' + moment(RegDef[0].DateFecNac).format("YYYY-MM-DD HH:mm") + '<br>' +
        '<strong>Código Ruaf:&nbsp</strong> ' + RegDef[1] + '<br>' +
        '<strong>Edad gestacional:&nbsp</strong> ' + RegDef[0].IntEdGesNac + '<br>' +
        '<strong>Documento Doctor:&nbsp</strong> ' + RegDef[0].DoubleGNCodUsu + '<br>' +
        '<strong>Nombre Doctor:&nbsp</strong>' + RegDef[0].StrNomDoc + '<br>' +
        '<strong>Peso Nacido Vivo:&nbsp</strong> ' + RegDef[0].IntPesoRn + '<br>' +
        '<strong>Talla Nacido Vivo:&nbsp</strong> ' + RegDef[0].FloatTallaRN + '<br>' +
        '<strong>Sexo:&nbsp</strong> ' + RegDef[0].StrSexo + '<br>';
}