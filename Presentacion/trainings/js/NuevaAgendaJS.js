let params = new URLSearchParams(window.location.search);
let idCapacitacion = parseInt(params.get("idCapacitacion"));


//Se obtiene todos los elementos del DOM Necesarios 
$FechaInicio = $("#FechaInicio");
$HoraInicio = $("#HoraInicio");
$FechaFinal = $("#FechaFinal");
$HoraFinal = $("#HoraFinal");
$txtSubtemas = $("#txtSubtemas");
$fuArchivo = $("#fuArchivo");
$slcModalidad = $("#slcModalidad");
$txtLinkReunion = $("#txtLinkReunion");
$linkExterno = $("#linkExterno");
$txtLinkReunion = $("#txtLinkReunion");
$Capacitador = $("#Capacitador");
$txtTempFirma = $("#txtTempFirma");
$slcLugar = $("#slcLugar");
$txtLinkExterno = $("#txtLinkExterno");
$btnAddSubtema = $("#btnAddSubtema");
$btnCreateEjeTematico = $("#btnCreateEjeTematico");
$txtCreateEjeTematico = $("#txtCreateEjeTematico");

//metodo que valida que la informacion ingresada por el usuario en el formulario se correcta
function ValidarFormulario() {
    

    if ($FechaInicio.val() == "") {
        error("Datos incompletos", "Por favor llene el campo fecha de incio")
        return false;
    }

    if ($HoraInicio.val() == "") {
        error("Datos incompletos", "Por favor llene el campo hora de inicio")
        return false;
    }

    if ($FechaFinal.val() == "") {
        error("Datos incompletos", "Por favor llene el campo fecha de finalización")
        return false;
    }
    if ($HoraFinal.val() == "") {
        error("Datos incompletos", "Por favor llene el campo hora de finalización")
        return false;
    }

    if (GetSubtemas().length == 0) {
        error("Datos incompletos", "Por favor agrege un subtema")
        return false;
    }

    if ($slcModalidad.val() == "-1") {
        error("Datos incompletos", "Por favor seleccione una modalidad")
        return false;
    }

    if ($slcLugar.val() == -1) {
        error("Datos incompletos", "Por favor llene el campo lugar de la capacitación")
        return false;
    }

    if (isNaN(parseInt($txtTempFirma.val()))) {
        error("Datos incompletos", "Por favor llene el campo número de días para la firma con un número correcto")
        return false;
    }

    return true;
}

//metodo que agrega los subtemas en forma de etiqueta
$btnAddSubtema.click(e => {
    e.preventDefault();
    let subTema = $txtSubtemas.val()
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
    $txtSubtemas.val("");
});


//metodo que al hacer click sobre la x de la etiqueta del un subtema lo elimina
$(document).on("click", ".btn-close-lst", function () {
    this.parentElement.parentElement.removeChild(this.parentElement);
});




//se evita que se recargue la pagia por 
$("form").submit(e => e.preventDefault());
$("form").keypress(e => { if (e.keyCode == 13) e.preventDefault() });

function GetSubtemas() {
    let subTemas = [];
    document.querySelectorAll("#lstSubtemas .item-list").forEach(divSubtema => subTemas.push(divSubtema.innerText));
    return subTemas;
}


async function SetAgenda() {
    if (!ValidarFormulario())
        return;
    let fechaFirma = new Date($FechaFinal.val());

    fechaFirma.setDate(fechaFirma.getDate() + parseInt($txtTempFirma.val()));


    let Agenda = {
        "IntOidCPAgenda": 0,
        "IntOidGNListaArchivos": 0,
        "IntOidCPCapacitacion": idCapacitacion,
        "IntTiempoFirma": parseInt($txtTempFirma.val()) || 5,
        "IntEstado": 1,
        "IntOidUsuarioResponsable":0,
        "DtmFecha": new Date($FechaInicio.val()),
        "DtmFechaFirma": fechaFirma,
        "DtmHoraInicial": new Date('01/01/1800 ' + $HoraInicio.val()),
        "DtmHoraFinal": new Date('01/01/1800 ' + $HoraFinal.val()),
        "DtmFechaFinal": new Date($FechaFinal.val()),
        "StrModalidad": $slcModalidad.val(),
        "StrLugar": $slcLugar.val(),
        "StrResponsable":"",
        "StrLinkReunion": $txtLinkReunion.val(),
        "StrLinkAnexo": $txtLinkExterno.val(),
        "StrFacilitador": ""
    }

    ejecutarajax(
        "NuevaAgenda.aspx/CreateAgenda",
        {
            "agenda": Agenda,
            "Subtemas": GetSubtemas()
        },
        function (msg) {
            let datos = msg.d;
            window.location.href = `CreacionExamenCapacitacion.aspx?idCapacitacion=${datos.idCapacitacion}&idAgenda=${datos.idAgenda}&Contexto=1`;
        }
    )

}

$("#btnCrearAgenda").click(function (e) {
    e.preventDefault();
    SetAgenda();
});

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
        "NuevaAgenda.aspx/cargarArchivo",
        { 'archivo': archivo },
        buscarArchivosSubidos
    )
});

$(document).on("click", ".btn-delete-arch", e => {
    let index = parseInt($(e.target).attr("data-id-archivo"));
    ejecutarajax(
        "NuevaAgenda.aspx/deleteArchivoSubido",
        { 'index': index },
        buscarArchivosSubidos
    )
});

function CargarAgenda(msg) {

    let Agenda = msg.d.Agenda;
    let Subtemas = msg.d.Subtemas;



    $txtTempFirma.val(Agenda.IntTiempoFirma);
    $FechaInicio.val(moment(Agenda.DtmFecha).format("YYYY-MM-DD"));
    $HoraInicio.val(moment(Agenda.DtmHoraInicial).format("HH:mm"));
    $HoraFinal.val(moment(Agenda.DtmHoraFinal).format("HH:mm"));
    $FechaFinal.val(moment(Agenda.DtmFechaFinal).format("YYYY-MM-DD"));
    $slcModalidad.val(Agenda.StrModalidad);
    $slcLugar.val(Agenda.StrLugar)
    $txtLinkReunion.val(Agenda.StrLinkReunion)
    $txtLinkExterno.val(Agenda.StrLinkAnexo)

    ejecutarajax("NuevaAgenda.aspx/GetArchivosSubidos", {}, buscarArchivosSubidos);

    Subtemas.forEach(subtema => {
        let subTema = subtema.StrSUBTEMA;
        let itemList = document.createElement("div");
        itemList.className = "item-list";
        itemList.innerHTML = `      
            ${subTema}<i class="fa fa-close btn-close-lst"></i>
        `
        $("#lstSubtemas").prepend(itemList);
    });

    $("#btnEditarMatricula").click(function (e) {
        window.location.href = `Matricula.aspx?idAgenda=${Agenda.IntOidCPAgenda}&idCapacitacion=${Agenda.IntOidCPCapacitacion}`
    })

    $("#btnEditarExamen").click(function (e) {
        window.location.href = `CreacionExamenCapacitacion.aspx?idCapacitacion=${Agenda.IntOidCPCapacitacion}&idAgenda=${Agenda.IntOidCPAgenda}&Contexto=1`;
    });
}


function GetAgenda() {
    ejecutarajax("NuevaAgenda.aspx/GetAgenda", { "idCapacitacion": idCapacitacion }, CargarAgenda)
}

$(document).ready(function (e) {
    GetAgenda();
});

