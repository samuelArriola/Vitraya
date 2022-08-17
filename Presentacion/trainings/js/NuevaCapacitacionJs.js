
//Se obtiene todos los elementos del DOM Necesarios 
$txtResponsable = $("#txtResponsable");
$txtEjeTematico = $("#txtEjeTematico");
$txtTema = $("#txtTema");
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
$txtCapacitador = $("#txtCapacitador");
$txtTempFirma = $("#txtTempFirma");
$slcLugar = $("#slcLugar");
$txtLinkExterno = $("#txtLinkExterno");
$btnAddSubtema = $("#btnAddSubtema");
$btnCreateEjeTematico = $("#btnCreateEjeTematico");
$txtCreateEjeTematico = $("#txtCreateEjeTematico");

//metodo que valida que la informacion ingresada por el usuario en el formulario se correcta
function ValidarFormulario() {
    if ($txtResponsable.val().trim() == "" || $txtResponsable.attr("data-value") == -1) {
        error("Datos incompletos", "Por favor llene el campo responsable")
        return false;
    }

    if ($txtEjeTematico.val().trim() == "" || $txtEjeTematico.attr("data-value") == -1) {
        error("Datos incompletos", "Por favor seleccione un eje temático")
        return false;
    }

    if ($txtTema.val().trim().length < 8) {
        error("Datos incompletos", "Por favor llene el campo FE")
        return false;
    }

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
    if ($txtCapacitador.val().trim().length < 8) {
        error("Datos incompletos", "Por favor llene el campo Facilitador")
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

$btnCreateEjeTematico.click(function (e) {
    e.preventDefault();
    if ($txtCreateEjeTematico.val().trim().length < 8) {
        error("Información Insuficiente", "Por favor complete bien el campo Eje temático")
        return;
    }

    let EjeTematico = {
        "StrEJETEMATICO": $txtCreateEjeTematico.val(),
        "IntOidCPEJETEMATICO": 0
    };

    ejecutarajax("NuevaCapacitacion.aspx/CreateEjeTematico", {"EjeTematico":EjeTematico}, GetEjesTematicos);
});

$("#btnCrearCapacitacion").click(function (e) {
    e.preventDefault();
    SetCapacitacion();
});

$("#btnEjeTematico").click(function(e)  {
    e.preventDefault();
    $("#modal-ejetematico").modal()
})




//metodo que al hacer click sobre la x de la etiqueta del un subtema lo elimina
$(document).on("click", ".btn-close-lst", function () {
    this.parentElement.parentElement.removeChild(this.parentElement);
});



//se genera el autocompletado del capo responsable de la capacitacion
let AutoCompletaResponsable = new slcAutoComplete(
    $txtResponsable,
    $(".box-autocomplete")[0],
    "NuevaCapacitacion.aspx/GetUsers",
    function(data){
        $txtResponsable.val(data.text);
        $txtResponsable.attr("data-value",data.value)
    }
);

////se genera el autocompletado del capo de eje temático
let AutoCompletaEjeTematico = new slcAutoComplete(
    $txtEjeTematico,
    $(".box-autocomplete")[1],
    "NuevaCapacitacion.aspx/GetEjesTematicos",
    function (data) {
        $txtEjeTematico.val(data.text);
        $txtEjeTematico.attr("data-value", data.value)
    }
);

AutoCompletaResponsable.setAutocomplete();
AutoCompletaEjeTematico.setAutocomplete();

//se evita que se recargue la pagia por 
$("form").submit(e => e.preventDefault());
$("form").keypress(e => {if(e.keyCode == 13) e.preventDefault() });

function GetSubtemas() {
    let subTemas = [];
    document.querySelectorAll("#lstSubtemas .item-list").forEach(divSubtema => subTemas.push(divSubtema.innerText));
    return subTemas;
}


async function SetCapacitacion() {
    if (!ValidarFormulario())
        return;

    let Capacitacion = {
        "StrCODIGO": "",
        "StrLUGAR": "",
        "StrUNIDADFUNCIONAL": "",
        "StrTEMA": $txtTema.val(),
        "StrMODALIDAD": "",
        "StrRESPONSABLE": $txtResponsable.val(),
        "StrLINK": "",
        "IntOidCPEJETEMA":parseInt($txtEjeTematico.attr("data-value")),
        "IntGNCodUsu":parseInt($txtResponsable.attr("data-value")),
        "IntOidListArch": 0,
        "IntOidCPCAPACITACION": 0,
        "DtmFECHA": new Date(),
        "DtmHORAINICIAL": new Date(),
        "DtmHORAFINAL": new Date(),
        "StrESTADO": "1",
        "IntOidGNArchivo": 0,
        "StrInfoMatricula": "",
        "IntOidGDDocument": -1,
        "IntTempFirma": 0,
        "DtmFechaFirma": new Date()
    }

    let fechaFirma = new Date($FechaFinal.val());

    fechaFirma.setDate(fechaFirma.getDate() + parseInt($txtTempFirma.val()));


    let Agenda = {
        "IntOidCPAgenda": 0,
        "IntOidGNListaArchivos":0 , 
        "IntOidCPCapacitacion": 0,
        "IntTiempoFirma": parseInt($txtTempFirma.val()) || 5,
        "IntEstado": 1,
        "IntOidUsuarioResponsable": parseInt($txtResponsable.attr("data-value")),
        "DtmFecha": ($FechaInicio.val()),
        "DtmFechaFirma": fechaFirma,
        "DtmHoraInicial":('01/01/1800 ' + $HoraInicio.val()),
        "DtmHoraFinal": ('01/01/1800 ' + $HoraFinal.val()),
        "DtmFechaFinal": ($FechaFinal.val()),
        "StrModalidad": $slcModalidad.val() ,
        "StrLugar": $slcLugar.val(),
        "StrResponsable": $txtResponsable.val() ,
        "StrLinkReunion":$txtLinkReunion.val() ,
        "StrLinkAnexo": $txtLinkExterno.val(),
        "StrFacilitador": $txtCapacitador.val()
    }

    ejecutarajax(
        "NuevaCapacitacion.aspx/CreateCapacitacion",
        {
            "agenda": Agenda,
            "capacitacion": Capacitacion,
            "Subtemas": GetSubtemas()
        },
        function (msg) {
            let datos = msg.d;
            window.location.href = `CreacionExamenCapacitacion.aspx?idCapacitacion=${datos.idCapacitacion}&idAgenda=${datos.idAgenda}&Contexto=1`;
        }
    )

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
        "NuevaCapacitacion.aspx/cargarArchivo",
        { 'archivo': archivo },
        buscarArchivosSubidos
    )
});

$(document).on("click", ".btn-delete-arch", e => {
    let index = parseInt($(e.target).attr("data-id-archivo"));
    ejecutarajax(
        "NuevaCapacitacion.aspx/deleteArchivoSubido",
        { 'index': index },
        buscarArchivosSubidos
    )
});


function GetEjesTematicos() {
    ejecutarajax(
        "NuevaCapacitacion.aspx/GetEjesTematicos",
        { "name": "" },
        function (msg) {
            let EjesTematicos = msg.d;
            dataTableEjes = "";

            EjesTematicos.forEach(eje => {
                dataTableEjes += `
                    <tr>
                        <td>${eje.text}</td>
                        <td>
                            <div class="btn" data-ideje="${eje.value}" id="btn-delete-eje">
                                <img data-ideje="${eje.value}" src="../Images/Delete.png" width="20"></img>
                            </div>
                        </td>
                    </tr>
                `;
            });

            $("#tbEjesTematicos tbody").html(dataTableEjes);
            DataTable("#tbEjesTematicos");
        }
    )
}

$(document).ready(function (e) {
    GetEjesTematicos();
});

$(document).on("click", "#btn-delete-eje", function (e) {
    let idEjeTematico = parseInt($(e.target).attr("data-ideje"))
    ejecutarajax("NuevaCapacitacion.aspx/DeleteEjeTematico", { "idEjeTematico": idEjeTematico }, GetEjesTematicos);
});

