
$("#ContentPlaceHolder_ddlProcesos option:first-child").attr("disabled", "disabled");




$("#ContentPlaceHolder_ddlProcesos").change(function (e) {
    e.preventDefault();
    let idProceso = $("#ContentPlaceHolder_ddlProcesos").val()
    let itemList = document.createElement("div");
    let nomProceso = $("#ContentPlaceHolder_ddlProcesos option:selected").text();

    $(itemList).attr("data-idProceso", idProceso)

    itemList.className = "item-list";
    itemList.innerHTML = `
         ${nomProceso}<i class="fa fa-close btn-close-lst"></i>
    `
    $("#lstProcesos").prepend(itemList);
    $("#ContentPlaceHolder_ddlProcesos").val("-1");
});



$("#btnHallazgo").click(function (e) {
    e.preventDefault();
    let hallazgo = $("#txtHallazgos").val()
    let responsable = $("#ContentPlaceHolder_ddlResponsableHall").val();
    let textResponsable = $("#ContentPlaceHolder_ddlResponsableHall option:selected").text();
    if (hallazgo.trim() == "" || hallazgo.trim().length < 8 || responsable == "-1") {
        error("Información Insuficiente", "La Información dada es insuficiente");
        return;
    }

    let itemList = $("#lsthallazgos");

    itemList.prepend(`
        <li class="mb-2" data-hallazgo="${hallazgo}" data-responsable="${responsable}">
            <span style="font-weight: 900">Responsable</span>
            <p>${textResponsable}</p>
        
            <span style="font-weight: 900">Hallazgo</span>
            <p>${hallazgo}</p>

            <i class="fa fa-close ml-1 btn btn-danger delete-def"></i>
           
        </li>
    `);

    $("#lsthallazgos").prepend(itemList);
    $("#txtHallazgos").val("");
});

$(document).on("click", ".delete-def", e => {
    e.preventDefault();
    e.target.parentElement.parentElement.removeChild(e.target.parentElement);
})


function ValidarFormulario() {
    if ($("#txtFecha").val().trim() == "") {
        error("No se ha especificado la fecha de la auditoría", "Por favor ingrese la fecha en que se reslizó la auditoria")
        return false;
    }
    if ($("#ContentPlaceHolder_ddlResponsable").val() == "-1") {
        error("No se seleccionado el ente auditor", "Por favor seleccione el ente auditor de la lista desplegable")
        return false;
    }
   
    if ($("#txtObjetivo").val().trim() == "") {
        error("No se ha ingresado el objetivo", "Por favor llene el campo objetivo")
        return false;
    }
    if ($("#txtAlcance").val().trim() == "") {
        error("No se ha ingresado el Alcance", "Por favor llene el campo Alcance")
        return false;
    }
    if (GetProcesos().length == 0) {
        error("No se ha seleccionado un proceso", "Por favor seleccione almenos un proceso")
        return false;
    }
    
    if (GetHallazgos().length == 0) {
        error("No se han ingresado hallasgos", "Por favor llene el campo hallasgos")
        return false;
    }

    return true;
}

$(document).on("click", ".btn-close-lst", function () {
    this.parentElement.parentElement.removeChild(this.parentElement);
});


function BorrarFormulario() {
    $("#txtFecha").val("");
    $("#ContentPlaceHolder_ddlResponsable").val("-1")
    $("#txtObjetivo").val("");
    $("#txtAlcance").val("");
    $("#ContentPlaceHolder_fuArchivo").val("");
    $("#txtHallazgos").val("");
    $("#ContentPlaceHolder_ddlResponsableHall").val("-1");


    [...document.getElementsByClassName("item-list")].forEach(item => {
        item.parentElement.removeChild(item);
    });

    [...document.querySelectorAll("#lsthallazgos li")].forEach(item => {
        item.parentElement.removeChild(item);
    });
}

$("#ContentPlaceHolder_btnGuardar").on("click", function (e) {
    if (!ValidarFormulario()) {
        e.preventDefault();
        return;
    }

    let auditoria = {
        'IntOIdAuditoriaExterna': 0,
        'IntOidListaArchivos': 0,
        'StrResponsable': $("#ContentPlaceHolder_ddlResponsable").val(),
        'StrObjetivo': $("#txtObjetivo").val(),
        'StrProcesos': JSON.stringify(GetProcesos()),
        'StrHallasgos': "",
        'DtmFecha': $("#txtFecha").val(),
        'StrAlcance': $("#txtAlcance").val()
    };

    ejecutarajax(
        "AuditoriaInterna.aspx/SetAuditoriaInterna",
        {
            "auditoria": auditoria,
            "hallazgos": GetHallazgos()
        },
        function (msg) {
            if (parseInt(msg.d)) {
                BorrarFormulario();
                exito("Hecho","La auditoria ha sido cargada correctamente");
            }
        }
    );

});

function GetProcesos() {
    let procesos = [];
    document.querySelectorAll("#lstProcesos .item-list").forEach(itemProceso => {
        procesos.push(parseInt($(itemProceso).attr("data-idProceso")));
    })
    return procesos;
}

function GetHallazgos() {
    let hallazgos = [];
    document.querySelectorAll("#lsthallazgos li").forEach(itemAuditor => {
        hallazgos.push({
            'IntResponsable': parseInt($(itemAuditor).attr("data-responsable")),
            'IntOidHallazgo': 0,
            'IntContexto': 0,
            'IntInstancia': 0,
            'StrDescripcion': $(itemAuditor).attr("data-hallazgo"),
        });
    })
    return hallazgos;
}

function getFile(file) {
    var reader = new FileReader();
    return new Promise((resolve, reject) => {
        reader.onerror = () => { reader.abort(); reject(new Error("Error parsing file")); }
        reader.onload = function () {

            let bytes = Array.from(new Uint8Array(this.result));
            let nombre = file.name;

            let nombres = nombre.split(".");

            nombre = nombre.replace(`.${nombres[nombres.length - 1]}`, "");

            resolve({
                'IntOidGNArchivo': 0,
                'AbteArchivo': bytes,
                'IntOidGNListaArchivos': 0,
                'StrContenido': file.type,
                'StrExt': nombres[nombres.length - 1],
                'StrNombre': nombre,
            });
        }
        reader.readAsArrayBuffer(file);
    });
}

const buscarArchivosSubidos = msg => {
    archivos = msg.d;
    DOMListaArchivo = "";
    archivos.forEach((archivo, i) => {
        DOMListaArchivo += `
            <div data-idArchivo="${i}" class="item-list">
                ${archivo.StrNombre}.${archivo.StrExt}<i class="fa fa-close btn-close-lst btn-delete-arch" data-id-archivo="${i}"></i>
            </div>
        `
    });
    $("#lsArchivos").html(DOMListaArchivo);
}

$("#ContentPlaceHolder_fuArchivo").on("change", async function (e) {
    let archivo = await getFile(this.files[0]);
    await ejecutarajax(
        "AuditoriaInterna.aspx/cargarArchivo",
        {'archivo': archivo},
        buscarArchivosSubidos
    )
});

$(document).on("click", ".btn-delete-arch", e => {
    let index = parseInt($(e.target).attr("data-id-archivo"));
    ejecutarajax(
        "AuditoriaInterna.aspx/deleteArchivoSubido",
        { 'index': index },
        buscarArchivosSubidos
    )
});