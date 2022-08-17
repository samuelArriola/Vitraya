let Params = new URLSearchParams(window.location.search);
let idRegistro = parseInt(Params.get('idRegistro'));

let registro = {};


dtFechaVacunacion = $("#dtFechaVacunacion")
slcTipoDocumento = $("#slcTipoDocumento")
txtDocumento = $("#txtDocumento")
txtPrimerApellido = $("#txtPrimerApellido")
txtSegundoApellido = $("#txtSegundoApellido")
txtNombre = $("#txtNombre")
dtFechaNacimiento = $("#dtFechaNacimiento")
txtEdad = $("#txtEdad")
slcSexo = $("#slcSexo")
slcRegimen = $("#slcRegimen")
txtAseguradora = $("#ContentPlaceHolder_txtAseguradora")
txtDeptoResidencia = $("#txtDeptoResidencia")
txtMunicipioResidencia = $("#txtMunicipioResidencia")
txtAreaResidencia = $("#txtAreaResidencia")
txtBarrio = $("#txtBarrio")
txtDireccion = $("#txtDireccion")
txtTelefono = $("#txtTelefono")
slcDesplazamiento = $("#slcDesplazamiento")
slcDiscapacidad = $("#slcDiscapacidad")
slcGrupoEtnico = $("#slcGrupoEtnico")
txtEmail = $("#txtEmail")
slcGestacion = $("#slcGestacion")
slcEtapa = $("#slcEtapa")
slcDosis = $("#slcDosis")
slcBiologico = $("#slcBiologico")
txtLote = $("#txtLote")
txtJeringa = $("#txtJeringa")
txtNombreVacunador = $("#txtNombreVacunador")
txtNombreIps = $("#txtNombreIps")
slcTipoDocumentoAC = $("#slcTipoDocumentoAC")
txtDocumentoAC = $("#txtDocumentoAC")
txtParentescoAC = $("#txtParentescoAC")
txtNombreAC = $("#txtNombreAC")
txtAseguradoraAC = $("#ContentPlaceHolder_txtAseguradoraAC")
txtTelefonoAC = $("#txtTelefonoAC")
slcDesplazamientoAC = $("#slcDesplazamientoAC")
slcTipoPoblacion = $("#slcTipoPoblacion")
dtFechaParto = $("#dtFechaParto")
slcGrupoEtnicoAC = $("#slcGrupoEtnicoAC")
slcDiscapacidadAC = $("#slcDiscapacidadAC")
txtEmailAC = $("#txtEmailAC")
slcRegimenAC = $("#slcRegimenAC")
txtLoteJeringa = $("#txtLoteJeringa")
txtDeptoNacimiento = $("#txtDeptoNacimiento")
txtMunicipioNacimiento = $("#txtMunicipioNacimiento")
txtSemanasGestacionMenor = $("#txtSemanasGestacionMenor")
txtPesoNacimientoMenor = $("#txtPesoAlNacerMenor")
slcLugarAtencionPartoMenor = $("#slcLugarPartoMenor")
txtNombreLugarPartoMenor = $("#txtNombreLugarPartoMenor")


let validadorPoblacion = {
    "Adultos mayores de 80 años y más": n => n >= 80,
    "Adultos mayores de 75 a 79 años": n => n >= 75 && n <= 79,
    "Adultos mayores de 70 a 74 años": n => n >= 70 && n <= 74,
    "Adultos mayores de 65 a 69 años": n => n >= 65 && n <= 69,
    "Adultos mayores de 60 a 64 años": n => n >= 60 && n <= 64,
    "Población de 55 a 59 años": n => n >= 55 && n <= 59,
    "Población de 50 a 54 años": n => n >= 50 && n <= 54,
    "Población de 45 a 49 años": n => n >= 45 && n <= 49,
    "Población de 40 a 44 años": n => n >= 40 && n <= 44,
    "Población de 35 a 39 años": n => n >= 35 && n <= 39,
    "Población de 30 a 34 años": n => n >= 30 && n <= 34,
    "Población de 25 a 29 años": n => n >= 25 && n <= 29,
    "Población de 20 a 24 años": n => n >= 20 && n <= 24,
    "Población de 16 a 19 años": n => n >= 16 && n <= 19,
    "Población de 12 a 15 años": n => n >= 12 && n <= 15,
    "Gestantes": n => false,
    "Migrantes": n => false,
}


const ejecutarAjax = (url, datos, success) => {
    return $.ajax({
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

function validarEmail(valor) {
    return /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i.test(valor) || valor == "";
}

function calcularEdad(d1, d2 = new Date()) {
    var diff = d2.getTime() - d1.getTime();
    return Math.floor(diff / (1000 * 60 * 60 * 24 * 365.25));
}

dtFechaNacimiento.change(function (e) {
    let edad = calcularEdad(new Date(dtFechaNacimiento.val()), dtFechaVacunacion.val() == "" ? new Date() : new Date(dtFechaVacunacion.val()))
    if (edad < 18)
        $("#panel-ac").show()
    else
        $("#panel-ac").hide()

    txtEdad.val(edad);

    if (edad < 12) {
        slcGestacion.val("2 = No Gestante").attr("disabled", "");
        dtFechaParto.attr("disabled", "");
    }
    else {

        dtFechaParto.removeAttr("disabled");
    }

    if (!(["CE", "PA", "PEP"].includes(slcTipoDocumento.val()) || slcGestacion.val() == "1 = Gestante")) {

        let validations = Object.values(validadorPoblacion);
        let poblaciones = Object.keys(validadorPoblacion);

        for (let i = 0, j = poblaciones.length - 2; i < j; i++) {
            if (validations[i](edad)) {
                slcTipoPoblacion.val(poblaciones[i]);
                break;
            }
        }
    }
})

function ValidarFormulario() {
    let edad = calcularEdad(new Date(dtFechaNacimiento.val()), dtFechaVacunacion.val() == "" ? new Date() : new Date(dtFechaVacunacion.val()))
    let inputs = []

    if (!(txtSemanasGestacionMenor.val() >= 0 && txtSemanasGestacionMenor.val() <= 42)) {
        error("Eror", "Semanas gestacional del menor no pueden ser menor a 0 ni mayor a 42");
        return false;
    }

    if (!(txtPesoNacimientoMenor.val() >= 0 && txtPesoNacimientoMenor.val() <= 15)) {
        error("Eror", "Peso al nacer del menor no puede ser menor a 0 ni mayor a 15");
        return false;
    }

    if (edad < 18) {
        inputs = document.querySelectorAll(".x_content input, .x_content select");
    }
    else {
        inputs = document.querySelectorAll("#panel1 input, #panel1 select")
    }

    for (i = 0, length = inputs.length; i < length; i++) {
        input = inputs[i];
        label = input.previousElementSibling

        if (input.id == "dtFechaParto") {
            if (slcGestacion.val() == "1 = GESTANTE" && input.value == "") {
                error("Campos incompletos", `Por favor llene el campo "${$(label).text()}"`);
                $(input).addClass("is-invalid");
                return false;
            }
            else {
                continue;
            }
        }

        if ((input.id == "txtEmailAC" || input.id == "txtEmail")) {
            if (!validarEmail(input.value)) {
                error("Alerta", `Por favor llene el campo "${$(label).text()}"con el formato correcto`);
                $(input).addClass("is-invalid");
                return false;
            }
            else {
                continue;
            }
        }
        if (input.id == "txtSegundoApellido") {
            continue;
        }
        if ($("#txtEdad").val() >= 18 && (input.id == "txtSemanasGestacionMenor" || input.id == "txtPesoAlNacerMenor" || input.id == "txtNombreLugarPartoMenor" || input.id == "slcLugarPartoMenor")) {
            continue;
        }
        if (input.id == "txtDeptoNacimiento" || input.id == "txtMunicipioNacimiento") {
            continue;
        }
        if ($("#txtEdad").val() <= 18 && (input.id == "slcTipoDocumentoAC" || input.id == "txtDocumentoAC" || input.id == "txtParentescoAC" || input.id == "txtNombreAC"
            || input.id == "slcRegimenAC" || input.class == "txtAseguradoraAC" || input.id == "txtTelefonoAC" || input.id == "slcDesplazamientoAC" || input.id == "slcDiscapacidadAC"
            || input.id == "slcGrupoEtnicoAC" || input.id == "txtEmailAC")) {
            continue;
        }

        if (input.value.trim() == "" || input.value == "-1") {
            error("Campos incompletos", `Por favor llene el campo "${$(label).text()}"`);
            $(input).addClass("is-invalid");
            return false;
        }
    }
    return true;
}


$(".x_content input, .x_content select").on("focus", function (e) {
    $(this).removeClass("is-invalid");
})


let autoCompleteMunicipio;
slcSexo.change(function () {
    let edad = calcularEdad(new Date(dtFechaNacimiento.val()), dtFechaVacunacion.val() == "" ? new Date() : new Date(dtFechaVacunacion.val()))
    if (this.value == "M") {
        slcGestacion.val("2 = No Gestante").attr("disabled", "");
        dtFechaParto.attr("disabled", "");
    }
    else if (isNaN(edad)) {
        slcGestacion.val("-1").removeAttr("disabled");
        dtFechaParto.removeAttr("disabled");
    }
    else if (edad >= 12) {
        slcGestacion.val("-1").removeAttr("disabled");
        dtFechaParto.removeAttr("disabled");
    }
    else {
        slcGestacion.val("2 = No Gestante").attr("disabled", "");
        dtFechaParto.attr("disabled", "");
    }
});

slcTipoDocumento.change(function () {
    console.log(this.value)
    if (["CE", "PA", "PEP"].includes(this.value)) {
        if ((slcGestacion.val() == "1 = Gestante")) {
            slcGestacion.change()
        }
        else {
            slcTipoPoblacion.val("Migrantes");
        }
    } else {
        if (slcGestacion.val() == "1 = Gestante") {
            slcGestacion.change()
        }
        else {
            dtFechaNacimiento.change();
        }
    }
});

slcGestacion.change(function (e) {
    if (this.value == "2 = No Gestante") {
        dtFechaParto.attr("disabled", "");
        slcTipoDocumento.change();
        dtFechaNacimiento.change();
    }
    else {
        dtFechaParto.removeAttr("disabled");
        slcTipoPoblacion.val("Gestantes")
    }
})



function LimpiarFormulario() {
    $(".x_content input").val("");
    $(".x_content select").val("-1");
    slcGestacion.removeAttr("disabled");
    dtFechaParto.removeAttr("disabled");
    txtNombreIps.val("Centro Médico Crecer Ltda.");
    $("#panel-ac").hide();
}

$("#btnCancelar").click(function () {
    LimpiarFormulario();
})



$(`select[id^="slcRegimen"]`).change(function (e) {
    let slcAs;
    if (this.id == "slcRegimen") {
        slcAs = txtAseguradora;
    }
    else {
        slcAs = txtAseguradoraAC;
    }

    if (this.value == '3=Pobre no asegurado') {
        slcAs.val("Pobre no asegurado");
        slcAs.attr("disabled", "");
    }
    else {
        slcAs.val("-1");
        slcAs.removeAttr("disabled");
    }
})


let autoCompleteDepartamento = new slcAutoComplete(
    txtDeptoResidencia,
    document.getElementById("lstDeptos"),
    "RegistroDiarioVacunacion.aspx/GetDepartamentos",
    function (datos) {
        txtDeptoResidencia.val(datos.text);
    }
);
autoCompleteDepartamento.setAutocomplete();

autoCompleteMunicipio = new slcAutoComplete(
    txtMunicipioResidencia,
    document.getElementById("lstMunicipios"),
    "RegistroDiarioVacunacion.aspx/GetMunicipio",
    function (datos) {
        txtMunicipioResidencia.val(datos.text);
    },
    txtDeptoResidencia
)
autoCompleteMunicipio.setAutocomplete();

let autoCompleteDepartamento2 = new slcAutoComplete(
    txtDeptoNacimiento,
    document.getElementById("lstDeptos2"),
    "RegistroDiarioVacunacion.aspx/GetDepartamentos",
    function (datos) {
        txtDeptoNacimiento.val(datos.text);
    }
);
autoCompleteDepartamento2.setAutocomplete();

autoCompleteMunicipio2 = new slcAutoComplete(
    txtMunicipioNacimiento,
    document.getElementById("lstMunicipios2"),
    "RegistroDiarioVacunacion.aspx/GetMunicipio",
    function (datos) {
        txtMunicipioNacimiento.val(datos.text);
    },
    txtDeptoNacimiento
)
autoCompleteMunicipio2.setAutocomplete();

$(".x_content input").attr("autocomplete", "off");

$("#btnAceptarModal").click(function () {
    $(".modal1").modal("hide");
})

$(document).ready(function () {
    $("#modal1").on('hide.bs.modal', function (e) {
        if ($("input[name=rdsAreas]:checked").val() == undefined) {
            error("", "Por favor seleccione una area de registro")
            e.preventDefault()
        }
    });
    $("#modal1").modal();
})


$("#btnInputFechaParto").click(function () {
    let types = {
        "text": "date",
        "date": "text"
    }
    dtFechaParto.prop("type", types[dtFechaParto.attr("type")]);
});



function cargarRegistro(msg) {
    registro = msg.d;

    slcTipoDocumento.val(registro.StrTipoDocumento);
    txtDocumento.val(registro.StrDocumento);
    slcSexo.val(registro.StrSexo).change();
    txtPrimerApellido.val(registro.StrPrimerApellido);
    txtSegundoApellido.val(registro.StrSegundoApellido);
    txtNombre.val(registro.StrNombres);
    slcRegimen.val(registro.StrRegimen).change();
    txtAseguradora.val(registro.StrEps);
    txtDeptoResidencia.val(registro.StrDeptoResidencia);
    txtMunicipioResidencia.val(registro.StrMunicipioResidencia);
    txtAreaResidencia.val(registro.StrAreaResidencia);
    txtBarrio.val(registro.StrBarrio);
    txtDireccion.val(registro.StrDireccion);
    txtTelefono.val(registro.StrTelefono);
    slcGrupoEtnico.val(registro.StrGrupoEtnico);
    slcDesplazamiento.val(registro.StrDesplazamiento);
    slcDiscapacidad.val(registro.StrDiscapacidad);
    txtEmail.val(registro.StrEmail);
    slcGestacion.val(registro.StrGestacion).change();
    slcEtapa.val(registro.StrEtapaVacunacion).change();
    slcTipoPoblacion.val(registro.StrTipoPoblacion);
    slcDosis.val(registro.StrDosis);
    slcBiologico.val(registro.StrBiologico);
    txtJeringa.val(registro.StrJeringa);
    txtNombreVacunador.val(registro.StrNombreVacunador);
    txtNombreIps.val(registro.StrNombreIps);
    slcTipoDocumentoAC.val(registro.StrTipoDocumentoAC || "-1");
    txtDocumentoAC.val(registro.StrDocumentoAC);
    txtParentescoAC.val(registro.StrParentescoAC);
    txtNombreAC.val(registro.StrNombresAC);
    slcRegimenAC.val(registro.StrRegimenAC || "-1").change();
    txtAseguradoraAC.val(registro.StrEpsAC || "-1");
    slcGrupoEtnicoAC.val(registro.StrGrupoEtnicoAC || "-1");
    slcDesplazamientoAC.val(registro.StrDesplazamientoAC || "-1");
    slcDiscapacidadAC.val(registro.StrDiscapacidadAC || "-1");
    txtEmailAC.val(registro.StrEmailAC);
    txtTelefonoAC.val(registro.StrTelefonoAC);
    dtFechaVacunacion.val(moment(registro.DtmFechaVacunacion).format("YYYY-MM-DD"));
    dtFechaNacimiento.val(moment(registro.DtmFechaNacimiento).format("YYYY-MM-DD"));
    dtFechaParto.val(new Date(registro.DtmFechaProbableParto) == "Invalid Date" ? "" : moment(registro.DtmFechaProbableParto).format("YYYY-MM-DD"));
    txtLoteJeringa.val(registro.StrLoteJeringa);
    txtLote.val(registro.StrLote);
    txtEdad.val(registro.IntEdad);

    txtDeptoNacimiento.val(registro.StrDeptoNacimiento);
    txtMunicipioNacimiento.val(registro.StrMunicipioNacimiento);
    txtSemanasGestacionMenor.val(registro.StrSemanasMenor);
    txtPesoNacimientoMenor.val(registro.StrPesoMenor);
    slcLugarAtencionPartoMenor.val(registro.StrLugarParto);
    txtNombreLugarPartoMenor.val(registro.StrNombreLugarParto);
}



function GetRegistros() {
    ejecutarAjax("EditarRegistro.aspx/GetRegistro", { "idRegistro": idRegistro }, cargarRegistro)
}

$(document).ready(function () {
    GetRegistros();
});



$("#btnRegistrar").click(function (e) {
    if (ValidarFormulario()) {


        registro.StrTipoDocumento = slcTipoDocumento.val();
        registro.StrDocumento = txtDocumento.val();
        registro.StrSexo = slcSexo.val();
        registro.StrPrimerApellido = txtPrimerApellido.val();
        registro.StrSegundoApellido = txtSegundoApellido.val();
        registro.StrNombres = txtNombre.val();
        registro.StrRegimen = slcRegimen.val();
        registro.StrEps = txtAseguradora.val();
        registro.StrDeptoResidencia = txtDeptoResidencia.val();
        registro.StrMunicipioResidencia = txtMunicipioResidencia.val();
        registro.StrAreaResidencia = txtAreaResidencia.val();
        registro.StrBarrio = txtBarrio.val();
        registro.StrDireccion = txtDireccion.val();
        registro.StrTelefono = txtTelefono.val();
        registro.StrGrupoEtnico = slcGrupoEtnico.val();
        registro.StrDesplazamiento = slcDesplazamiento.val();
        registro.StrDiscapacidad = slcDiscapacidad.val();
        registro.StrEmail = txtEmail.val();
        registro.StrGestacion = slcGestacion.val();
        registro.StrEtapaVacunacion = "";
        registro.StrTipoPoblacion = slcTipoPoblacion.val();
        registro.StrDosis = slcDosis.val();
        registro.StrBiologico = slcBiologico.find("option:selected").text();
        registro.StrLote = txtLote.val();
        registro.StrJeringa = txtJeringa.find("option:selected").text();
        registro.StrNombreVacunador = txtNombreVacunador.val();
        registro.StrNombreIps = txtNombreIps.val();
        registro.StrTipoDocumentoAC = slcTipoDocumentoAC.val().replace("-1", "");
        registro.StrDocumentoAC = txtDocumentoAC.val();
        registro.StrParentescoAC = txtParentescoAC.val();
        registro.StrNombresAC = txtNombreAC.val();
        registro.StrRegimenAC = slcRegimenAC.val().replace("-1", "");
        registro.StrEpsAC = txtAseguradoraAC.val().replace("-1", "");
        registro.StrGrupoEtnicoAC = slcGrupoEtnicoAC.val().replace("-1", "");
        registro.StrDesplazamientoAC = slcDesplazamientoAC.val().replace("-1", "");
        registro.StrDiscapacidadAC = slcDiscapacidadAC.val().replace("-1", "");
        registro.StrEmailAC = txtEmailAC.val();
        registro.StrTelefonoAC = txtTelefonoAC.val();
        registro.DtmFechaVacunacion = dtFechaVacunacion.val();
        registro.DtmFechaNacimiento = dtFechaNacimiento.val();
        registro.StrFechaProbableParto = dtFechaParto.val() == "" ? null : dtFechaParto.val();
        registro.StrLoteJeringa = txtLoteJeringa.val();
        registro.IntEdad = parseInt(txtEdad.val());
        registro.DtmFechaRetgistro = moment(registro.DtmFechaRetgistro).format("YYYY-MM-DD") == 'Invalid date' ? moment(new Date()).format("YYYY-MM-DD") : moment(registro.DtmFechaRetgistro).format("YYYY-MM-DD");
        registro.IntOidVCLoteJeringa = 0;
        registro.IntOidVCLoteBiologico = 0;
        registro.StrDeptoNacimiento = txtDeptoNacimiento.val();
        registro.StrMunicipioNacimiento = txtMunicipioNacimiento.val();
        registro.StrSemanasMenor = txtSemanasGestacionMenor.val();
        registro.StrPesoMenor = txtPesoNacimientoMenor.val();
        registro.StrLugarParto = slcLugarAtencionPartoMenor.val();
        registro.StrNombreLugarParto = txtNombreLugarPartoMenor.val();

        console.log(ejecutarAjax(
            "EditarRegistro.aspx/UpdateRegistro",
            { "registro": registro },
            function (estado) {
                if (estado.d) {
                    exito("Resgistro actualizado", "Se Actualizado el registro satisfactoriamente")
                    LimpiarFormulario();
                    setInterval(() => { window.location.href = "VerRegistro.aspx" }, 3000);
                }
                else {
                    error("Error", "No se ha podido Actualizar el registro")
                }
            }
        ))
    }
})


