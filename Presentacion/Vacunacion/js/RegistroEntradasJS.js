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

$("#slcTipo").change(async function (e) {
    let insumos = (await ejecutarAjax("RegistroEntradas.aspx/GetInsumos", { "tipo": this.value })).d
    $("#slcNombre").html("<option value=\"-1\" disabled selected>Seleccione</option>");
    let opcsInsumos = "";

    insumos.forEach(insumo => {
        opcsInsumos += `
            <option value="${insumo.IntOidVCInsumo}">${insumo.StrNombre}</option>
        `;
    })

    $("#slcNombre").append(opcsInsumos);
})


async function  validarDatos() {
    let inputs = document.querySelectorAll("#formCreateLote input, #formCreateLote select")
    for (let i = 1, length = inputs.length; i < length; i++) {

        if (inputs[i].id == "txtExistenciasCrear")
            continue;
        if ($(inputs[i]).attr("disabled")) {
            continue;
        }

        if (inputs[i].value == "" || inputs[i].value == "-1") {
            error("Datos incompletos", `Por favor complete todos los campos`);
            $(inputs[i]).addClass("is-invalid");
            return false;
        }
    }


    let LoteRegistrado = (await ejecutarAjax(
        "RegistroEntradas.aspx/GetLoteExt",
        { "idInsumo": parseInt($("#slcNombre").val()), "numLote": $("#txtNumLoteCrear").val() }
    )).d;

    if (LoteRegistrado != null) {
        error("El Lote ya se encuentra registrado", "Por favor registre otro número de lote")
        return false;
    }

    return true;
}

$(document).on("focus", "#formCreateLote input, #formCreateLote select, #txtFechaIngreso, #tbEntradas input",function () {$(this).removeClass("is-invalid") })

$("#btnCrear").click(async function () {

    flag = await validarDatos();
    if (flag) {
        $("#tbEntradas tbody").append(`
            <tr data-new >
                <td>${$("#slcTipo").val()}</td>
                <td data-idinsumo=${$("#slcNombre").val()}>${$("#slcNombre option:selected").text()}</td>
                <td>${$("#txtNumLoteCrear").val()}</td>
                <td>${$("#txtDiluyente").val()}</td>
                <td>${$("#txtCantidadCrear").val()}</td>
                <td>${$("#txtCantidadCrear").val()}</td>
                <td class="text-center"><i style="cursor:pointer" class="fa fa-trash btn btn-danger"></i></td>
            </tr>`  
        );
        $("#formCreateLote input").val("");
        $("#formCreateLote select").val("-1");
        $("#formCreateLote input:nth-child(n+2), #formCreateLote select, #formCreateLote button").attr("disabled", "");

    }
})

function GetLotesFromTable() {
    let Lotes = [];
    rows = document.querySelectorAll(`#tbEntradas tr[data-new]`)
    rows.forEach(row => {
        Lotes.push({
            'IntOidVCLote': 0,
            'IntOidVCInsumo': parseInt($(row).find("td[data-idinsumo]").attr("data-idinsumo")),
            'IntTotalIngresado': parseInt($(row).find("td")[4].innerText),
            'StrNumLote': $(row).find("td")[2].innerText,
            'StrDiluyente': $(row).find("td")[3].innerText
        })
    });
    return Lotes;
}


function getDetalle() {
    return {
        IntOidVCDetEntLot : 0,
        IntOidUsuario : 0,
        DtmFechaEntrada: $("#txtFechaIngreso").val(),
        DtmFechaRegistro: moment(new Date()).format("YYYY-MM-DD"),
    }
}

function getEntradaLote() {
    let entradasLotes = [];
    rows = document.querySelectorAll(`#tbEntradas tr[data-idlote]`)
    rows.forEach(row => {
        entradasLotes.push({
            "IntOidVCEntradaLote": 0,
            "IntOidVCDetEntLot": 0,
            "IntOidVCLote": parseInt($(row).attr("data-idlote")),
            "FltCantidad": parseFloat($(row).find("input").val()),
        })
    })
    return entradasLotes;
}

$(document).on("click", "#tbEntradas .btn-danger", function (e) {
    $(e.target.parentElement.parentElement).remove();
});

function validarRegistroEntrada() {
    if ($("#txtFechaIngreso").val() == "") {
        error("Registro de entrada sin fecha", "Por favor indique una fecha para el registro de la entrada de los lotes")
        $("#txtFechaIngreso").addClass("is-invalid")
        return 0;
    }
    let inputs = document.querySelectorAll("#tbEntradas input");
    for(let input of inputs) {
        if (input.value == "") {
            error("Datos Incompletos", "Por favor llene todos los datos antes de registrar la entrada");
            $(input).addClass("is-invalid");
            return 0;
        }
    };
    return 1
}

$("#btnAgregarEntrada").click(function () {
    if (validarRegistroEntrada()) {
        ejecutarAjax(
            "RegistroEntradas.aspx/SetEntrada",
            {
                lotes: GetLotesFromTable(),
                detalle: getDetalle(),
                entradaLotes: getEntradaLote(),
            }
        )
        $("#tbEntradas tbody tr").remove();
    }
});
    
let autocompleteLotes = new slcAutoComplete(
    $("#txtSerachLotes"),
    document.getElementById("lstLotes"),
    "RegistroEntradas.aspx/GetLotes",
    async function (datos) {
        if (datos.value == 0) {
            $("#slcTipo,#slcNombre,#txtNumLoteCrear,#txtCantidadCrear,#btnCrear").removeAttr("disabled");
        }
        else {
            let lote = (await ejecutarAjax(
                "RegistroEntradas.aspx/GetLote",
                { "idLote": parseInt(datos.value) }
            )).d;
            $("#tbEntradas tbody").append(`
            <tr data-idlote="${lote.OidVCLote}">
                <td>${lote.Tipo}</td>
                <td data-idinsumo=${lote.OidVCInsumo}>${lote.Nombre}</td>
                <td>${lote.NumLote}</td>
                <td>${lote.Diluyente}</td>
                <td>${lote.Existencias}</td>
                <td><input type="number" class="form-control" /></td>
                <td class="text-center"><i style="cursor:pointer" class="fa fa-trash btn btn-danger"></i></td>
            </tr>`
            );
            $("#formCreateLote input:nth-child(n+2), #formCreateLote select, #formCreateLote button").attr("disabled","");
        }
    }
);

$("#slcNombre").change(function () {
    if ($(this).find("option:selected").text() == "Pfizer") {
        $("#txtDiluyente").removeAttr("disabled");
    }
    else {
        $("#txtDiluyente").attr("disabled","disabled");
    }
});
autocompleteLotes.setAutocomplete();

$("form").keypress(function (e) { if (e.keyCode == 13) e.preventDefault() });

