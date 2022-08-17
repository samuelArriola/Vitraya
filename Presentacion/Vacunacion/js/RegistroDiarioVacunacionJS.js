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
    "Población de 3 a 11 años": n => n >= 3 && n <= 11,
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
    if (edad < 18) {
        $("#panel-ac").show()
        $(".groupNaciMenor").show()
    }
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

async function ValidarFormulario() {
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
                error("Formato de correo incorrecto", `Por favor llene el campo "${$(label).text()}" con formato correcto`);
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
        if (edad >= 18 && (input.id == "txtSemanasGestacionMenor" || input.id == "txtPesoAlNacerMenor" || input.id == "txtNombreLugarPartoMenor" || input.id == "slcLugarPartoMenor")) {
            continue;
        }


        if (input.value.trim() == "" || input.value == "-1") {
            error("Campos incompletos", `Por favor llene el campo "${$(label).text()}"`);
            $(input).addClass("is-invalid");
            return false;
        }
    }

    let flagDosis = (
        await ejecutarAjax(
            "RegistroDiarioVacunacion.aspx/ValidarDosis",
            {
                documento: txtDocumento.val(),
                dosis: slcDosis.val()
            }
        )
    ).d

    if (!flagDosis) {
        error("Paciente con dosis ya aplicada", "Al paciente ya se le aplicó la dosis seleccionada, por varifique los datos");
        slcDosis.addClass("is-invalid");
        return false;
    }

    if ((slcDosis.val() == "3 = Única" && slcBiologico.val() != "Janssen") || ((slcBiologico.val() == "Janssen") && (slcDosis.val() != "3 = Única" && slcDosis.val() != "4 = Refuerzo"))) {
        error("Dosis o Biológico incorrectos", "La dosis aplicada no corresponde al Biológico seleccionado")
        return false;
    }

    return true;
}

$("#btnRegistrar").click(async function (e) {
    if (await ValidarFormulario()) {
        registro = {
            IntOidRegistroDiarioVac: 0,
            StrTipoDocumento: slcTipoDocumento.val(),
            StrDocumento: txtDocumento.val(),
            StrSexo: slcSexo.val(),
            StrPrimerApellido: txtPrimerApellido.val(),
            StrSegundoApellido: txtSegundoApellido.val(),
            StrNombres: txtNombre.val(),
            StrRegimen: slcRegimen.val(),
            StrEps: txtAseguradora.val(),
            StrDeptoResidencia: txtDeptoResidencia.val(),
            StrMunicipioResidencia: txtMunicipioResidencia.val(),
            StrAreaResidencia: txtAreaResidencia.val(),
            StrBarrio: txtBarrio.val(),
            StrDireccion: txtDireccion.val(),
            StrTelefono: txtTelefono.val(),
            StrGrupoEtnico: slcGrupoEtnico.val(),
            StrDesplazamiento: slcDesplazamiento.val(),
            StrDiscapacidad: slcDiscapacidad.val(),
            StrEmail: txtEmail.val(),
            StrGestacion: slcGestacion.val(),
            StrEtapaVacunacion: "",
            StrTipoPoblacion: slcTipoPoblacion.val(),
            StrDosis: slcDosis.val(),
            StrBiologico: slcBiologico.find("option:selected").text(),
            StrLote: txtLote.val(),
            StrJeringa: txtJeringa.find("option:selected").text(),
            StrNombreVacunador: txtNombreVacunador.val(),
            StrNombreIps: txtNombreIps.val(),
            StrTipoDocumentoAC: slcTipoDocumentoAC.val().replace("-1", ""),
            StrDocumentoAC: txtDocumentoAC.val(),
            StrParentescoAC: txtParentescoAC.val(),
            StrNombresAC: txtNombreAC.val(),
            StrRegimenAC: slcRegimenAC.val().replace("-1", ""),
            StrEpsAC: txtAseguradoraAC.val().replace("-1", ""),
            StrGrupoEtnicoAC: slcGrupoEtnicoAC.val().replace("-1", ""),
            StrDesplazamientoAC: slcDesplazamientoAC.val().replace("-1", ""),
            StrDiscapacidadAC: slcDiscapacidadAC.val().replace("-1", ""),
            StrEmailAC: txtEmailAC.val(),
            StrTelefonoAC: txtTelefonoAC.val(),
            DtmFechaVacunacion: dtFechaVacunacion.val(),
            DtmFechaNacimiento: dtFechaNacimiento.val(),
            StrFechaProbableParto: dtFechaParto.val() == "" ? null : dtFechaParto.val(),
            StrLoteJeringa: txtLoteJeringa.val(),
            IntEdad: parseInt(txtEdad.val()),
            DtmFechaRetgistro: new Date(),
            IntOidRegistrador: 0,
            StrLugarRegistro: $("input[name=rdsAreas]:checked").val(),
            IntOidVCLoteJeringa: 0,
            IntOidVCLoteBiologico: 0,
            StrDeptoNacimiento: txtDeptoNacimiento.val(),
            StrMunicipioNacimiento: txtMunicipioNacimiento.val(),
            StrSemanasMenor: txtSemanasGestacionMenor.val(),
            StrPesoMenor: txtPesoNacimientoMenor.val(),
            StrLugarParto: slcLugarAtencionPartoMenor.val(),
            StrNombreLugarParto: txtNombreLugarPartoMenor.val()


        }
        ejecutarAjax(
            "RegistroDiarioVacunacion.aspx/SetRegistro",
            { "registro": registro },
            function (estado) {
                if (estado.d) {
                    exito("Vacunación registrada", "Se registro la vacunación satisfactoriamente")
                    LimpiarFormulario();
                }
                else {
                    error("Error", "No se puede resgistrar la vacunación del paciente dos veces el mismo día.")
                }
            }
        )
    }

});

$(".x_content input, .x_content select").on("focus", function (e) {
    $(this).removeClass("is-invalid");
})


let autoCompleteMunicipio;
let autoCompleteMunicipio2;
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

    $(".groupNaciMenor").hide();
    $("#txtDeptoNacimiento").val("");
    $("#txtMunicipioNacimiento").val("");
    $("#txtSemanasGestacionMenor").val("");
    $("#txtPesoAlNacerMenor").val("");
    $("#slcLugarPartoMenor").val("");
    $("#txtNombreLugarPartoMenor").val("");
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

//////////////////////////////////////////////////////////////////////////////////

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

//////////////////////////////////////////////////////////////////////////////////

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

//////////////////////////////////////////////////////////////////////////////////

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

$(document).ready(function () {
    CargarSlcs();
});

$("#btnInputFechaParto").click(function () {
    let types = {
        "text": "date",
        "date": "text"
    }
    dtFechaParto.prop("type", types[dtFechaParto.attr("type")]);
});