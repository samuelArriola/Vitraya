

//$(document).keyup(function (e) { // actualizar form, luego de cerrar modal. 
//    if (e.keyCode == 27) {
//        if ($('#ModalRegDef').is(':visible'))
//            window.location.reload();
//    }
//});

$(".modal").on("hidden.bs.modal", function () {
    window.location.reload();
});


// función para restringir a solo número
document.getElementById("ContentPlaceHolder_idPacDef").addEventListener("keypress", soloNumeros, false);
function soloNumeros(e) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}

//agregar evneto click al boton guardar principal, para cargar info al modal.
$("#ContentPlaceHolder_btnGuaRegDefFil").on("click", (e) => {
    e.preventDefault();

    var inpsNacViv = [...document.getElementsByClassName("inpDef")];
    var control = false;
    for (var wi = 0, n = inpsNacViv.length; wi < n; wi++) { // validar inputs vacio.
        if (inpsNacViv[wi].value == "")
            control = true
    }

    if (control || !($("#chkEstadoPacienteSi")[0].checked || $("#chkEstadoPacienteNo")[0].checked))
        error("error", "por favor complete los campos.")
    else {

        var RegDef = {
            "StrTipDef": document.getElementById("ContentPlaceHolder_tipDef").value,
            "DateFecDef": document.getElementById("ContentPlaceHolder_fechaDefuncion").value,
            "StrNomPac": document.getElementById("nomPacDef").value,
            "DoubleIdPaciente": parseInt(document.getElementById("ContentPlaceHolder_idPacDef").value),
            "IntOIdCRCodRuaf": 0,
            "DoubleGNCodUsu": parseInt(document.getElementById("ContentPlaceHolder_IdDocDef").value),
            "StrNomDoc": document.getElementById("ContentPlaceHolder_NomDocDef").value,
            "StrServicio": document.getElementById("ContentPlaceHolder_ServDef").value,
            "BlnEstadoPaciente": $("#chkEstadoPacienteSi")[0].checked
        }

        $.ajax({ // metodo que envia los datos al servidor.
            url: "RegistroDefuncion.aspx/guardarRegDef",
            data: JSON.stringify({ "RegDef": RegDef }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {

                datos = JSON.parse(msg.d);
                if (datos == null) {
                    error("error", "El paciente ya se encuentra registrado");
                } else {
                    cargarInfModal(JSON.parse(msg.d));
                    exito("success","Registro Guardado")
                    $("#ModalRegDef").modal();
                    //alert("guardado");
                    //window.location.href = "RegistroDefuncion.aspx"
                }

            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });

        //cargarInfModal();
        //$("#ModalRegDef").modal(); // activar modal.
    }
})

$("#cerrarModal").on("click", (e) => {
    window.location.href = "RegistroDefuncion.aspx";
})


//función para cargar datos al modal.
function cargarInfModal(regDef) {
    let elemento = document.getElementById("modalRegDefTex");

    elemento.innerHTML = '' +
        '<strong>Tipo Defunción:&nbsp </strong> ' + regDef[0].StrTipDef +'<br>' +
        '<strong>Fecha: </strong>&nbsp' + moment(regDef[0].DateFecDef).format("YYYY-MM-DD HH:mm") + '<br>' +
        '<strong>Nombre paciente:&nbsp </strong>' + regDef[0].StrNomPac+'<br>' +
        '<strong>Documento paciente: &nbsp</strong> ' + regDef[0].DoubleIdPaciente +'<br>' +
        '<strong>Código Ruaf: &nbsp</strong>' + regDef[1] +'<br>' +
        '<strong>Documento Médico:&nbsp</strong>' + regDef[0].DoubleGNCodUsu +'<br>' +
        '<strong>Nombre Médico:&nbsp </strong>' + regDef[0].StrNomDoc +'<br>' +
        '<strong>Servicio:&nbsp </strong> ' + regDef[0].StrServicio+ '<br>';

}

//// boton guardar(modal) para enviar los datos al servidor.
//$("#btnGuaRegDef").on("click", (e) => {

//    var RegDef = {
//        "StrTipDef": document.getElementById("tipDef").value,
//        "DateFecDef": document.getElementById("fechaDefuncion").value,
//        "StrNomPac": document.getElementById("nomPacDef").value,
//        "DoubleIdPaciente": parseInt(document.getElementById("idPacDef").value),
//        "IntOIdCRCodRuaf": 0,
//        "DoubleGNCodUsu": parseInt(document.getElementById("IdDocDef").value),
//        "StrNomDoc": document.getElementById("NomDocDef").value,
//        "StrServicio": document.getElementById("ServDef").value,
//    }

//    $.ajax({ // metodo que envia los datos al servidor.
//        url: "RegistroDefuncion.aspx/guardarRegDef",
//        data: JSON.stringify({ "RegDef": RegDef }),
//        dataType: "json",
//        type: "POST",
//        contentType: "application/json; charset=utf-8",
//        success: function (msg) {
            
//            datos = JSON.parse(msg.d);
//            if (datos == null) {
//                error("error", "El paciente ya se encuentra registrado");
//            } else {
//                alert("guardado");
//                window.location.href = "RegistroDefuncion.aspx"
//            }
            
//        },
//        error: function (result) {
//            alert("ERROR " + result.status + ' ' + result.statusText);
//        }
//    });


//})
