
let indexItemBox = -1;

function GetDataAjax(url, datos) {
    return $.ajax({
        url: url,
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
    });
}



$("#btnGuardarSubtema").click(function (e) {
    console.log(e);
    e.preventDefault();
    let subTema = $("#txtSubtema").val()
    if (subTema.trim() == "" || subTema.trim().length < 8) {
        error("Información Insuficiente", "La Información dada es insuficiente");
        return;
    }
    let itemList = document.createElement("div");
    itemList.className = "item-list";
    itemList.innerHTML = `
         ${subTema}<i class="fa fa-close btn-close-lst"></i>
    `
    $("#lstSubtemas").prepend(itemList);
    $("#txtSubtema").val("");
});

$(document).on("click", ".btn-close-lst", function () {
    this.parentElement.parentElement.removeChild(this.parentElement);
});


async function setBoxAutocomplete(element) {
    let cargos = (await GetDataAjax("ValidacionDibulgacion.aspx/GetCargos", { name: element.value })).d
    let boxAutocomplete = $("#box-autocomplete");
    lstAutocomplete = "";
    for (let i = 0; i < cargos.length; i++) {
        cargo = cargos[i];
        lstAutocomplete += `<div class="item-autocomplete" data-id="${cargo.IntGnDcCgo}" >${cargo.StrGnNomCgo}</div>`;
    }

    $("#box-autocomplete").html(lstAutocomplete);
}

$("#txtCargo").keyup(function (e) {
    e.preventDefault();
    ItemBox = $(".item-autocomplete");
    if (e.keyCode == 38) {
        
        if (indexItemBox <= 0) {
            indexItemBox = ItemBox.length - 1;
        }
        else {
            indexItemBox--; 
        }
        ItemBox.removeClass("item-selected")
        $(ItemBox.get(indexItemBox)).addClass("item-selected")
        $("#box-autocomplete").scrollTop(ItemBox.get(indexItemBox).offsetTop)
        this.value = $(ItemBox.get(indexItemBox)).text()
        return;
    }
    if (e.keyCode == 40) {
        if (indexItemBox >= ItemBox.length - 1) {
            indexItemBox = 0;
        }
        else{
            indexItemBox ++;
        }
        ItemBox.removeClass("item-selected")
        $(ItemBox.get(indexItemBox)).addClass("item-selected")
        $("#box-autocomplete").scrollTop(ItemBox.get(indexItemBox).offsetTop)
        this.value = $(ItemBox.get(indexItemBox)).text()
        return;
    }
    if (e.keyCode == 13) {
        if (indexItemBox != -1) {
            let itemList = document.createElement("div");
            itemList.className = "item-list";
            itemList.innerHTML = `
                ${$(ItemBox.get(indexItemBox)).text()}<i class="fa fa-close btn-close-lst"></i>
            `
            $(itemList).attr("data-id", $(ItemBox.get(indexItemBox)).attr("data-id"));
            $("#lstCargos").prepend(itemList);
            this.value = "";
            this.blur();
        }
        return;
    }
    setBoxAutocomplete(this);
    indexItemBox = -1;
});

$(document).on("focus", "#txtCargo", function (e) {
    indexItemBox = -1;
    $("#box-autocomplete").css("max-height", "300px");
    $("#box-autocomplete").css("border", "1px solid #000");
    setBoxAutocomplete(this);
    
})
$(document).on("blur", "#txtCargo", function (e) {
    $("#box-autocomplete").css("max-height", "0");
    $("#box-autocomplete").css("border", "0");
})

$("form").submit(function (e) { e.preventDefault() });

$(document).on("mousedown", ".item-autocomplete", function (e) {
    console.log(this);
    let itemList = document.createElement("div");
    itemList.className = "item-list";
    itemList.innerHTML = `
                ${$(this).text()}<i class="fa fa-close btn-close-lst"></i>
            `
    $(itemList).attr("data-id", $(this).attr("data-id"));
    $("#lstCargos").prepend(itemList);
    $("#txtCargo").val("");
});
$(document).on("mouseover", ".item-autocomplete", function (e) { $("#txtCargo").val($(this).text()) });

$("#box-autocomplete").on("mouseout", function (e) { $("#txtCargo").val("") });

function GetDataFromLst(selector) {
    let lstObjects = $(selector).find(".item-list");
    let data = [];
    for (let i = 0; i < lstObjects.length; i++) {
        data.push(parseInt($(lstObjects[i]).attr("data-id")) || $(lstObjects[i]).text());
    }
    return data;
}

let idCapacitacion;
let idAgenda;

function SetDivulgacion(msg) {

    datos = msg.d;

    datos.forEach((item) => {

        idAgenda = item.OidCPAgenda1;

    })

    //Params = new URLSearchParams(window.location.search);
    //IdDocumento = parseInt(Params.get('IdDocumento'));

    //let divulgacion = {
    //    IntOidGDDivulgacion: 0,
    //    IntOidCPEjeTematico: parseInt($("#ContentPlaceHolder_slctEjeTematico").val()),
    //    StrCargos: JSON.stringify(GetDataFromLst("#lstCargos")),
    //    StrSubtemas: JSON.stringify(GetDataFromLst("#lstSubtemas")),
    //    IntOidGDDocumento: IdDocumento,
    //    IntTempFirma: parseInt($("#txtTempFirma").val())
    //}
    //ejecutarajax(
    //    "ValidacionDibulgacion.aspx/SetDivulgacion",
    //    { divulgacion: divulgacion },
    //    function (msg) {
    //        Params = new URLSearchParams(window.location.search);
    //        IdDocumento = parseInt(Params.get('IdDocumento'));
    //        window.location.href = `../trainings/CreacionExamenCapacitacion.aspx?idCapacitacion=${IdDocumento}&Contexto=2`
    //    });

    Params = new URLSearchParams(window.location.search);
    IdDocumento = parseInt(Params.get('IdDocumento'));

    let divulgacion = {
        IntOidGDDivulgacion: 0,
        IntOidCPEjeTematico: parseInt($("#ContentPlaceHolder_slctEjeTematico").val()),
        StrCargos: JSON.stringify(GetDataFromLst("#lstCargos")),
        StrSubtemas: JSON.stringify(GetDataFromLst("#lstSubtemas")),
        IntOidGDDocumento: IdDocumento,
        IntTempFirma: parseInt($("#txtTempFirma").val())
    }
    ejecutarajax(
        "ValidacionDibulgacion.aspx/SetDivulgacion",
        { divulgacion: divulgacion },
        function (msg) {
            Params = new URLSearchParams(window.location.search);
            IdDocumento = parseInt(Params.get('IdDocumento'));
            window.location.href = `../trainings/CreacionExamenCapacitacion.aspx?idCapacitacion=${idCapacitacion}&idAgenda=${idAgenda}&Contexto=2`
        });

}

function CreateAgenda(msg) {

    Params = new URLSearchParams(window.location.search);
    let IdSolicitud = parseInt(Params.get('IdSolicitud'));

    datos = msg.d;

    datos.forEach((item) => {

        idCapacitacion = item.IntOidCPCAPACITACION;

    })

    datos = {
        "idCapacitacion": idCapacitacion,
        "idSolicitud": IdSolicitud
    }

    ejecutarajax("ValidacionDibulgacion.aspx/SetAgenda", datos, SetDivulgacion);
}

function CreateCapacitacion() {

    Params = new URLSearchParams(window.location.search);
    let nombreCapacitacion = Params.get('NombreProc');
    let IdSolicitud = parseInt(Params.get('IdSolicitud'));

    datos = {
        "nombreC": "Socialización del documento " + nombreCapacitacion,
        "idSolicitud": IdSolicitud
    }

    $("#loading-modal").modal();
    ejecutarajax("ValidacionDibulgacion.aspx/SetCapacitacion", datos, CreateAgenda);
}


$("#txtTempFirma").on("keypress", function (e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /^([0-9])*$/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
});

$("#btnGuardar").click(function (e) { e.preventDefault; CreateCapacitacion() });