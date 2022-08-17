


const $seleccionArchivos = document.querySelector("#ContentPlaceHolder_fuImageFlujo"),
    $imagenPrevisualizacion = document.querySelector("#imageFlujo");

// Escuchar cuando cambie
$seleccionArchivos.addEventListener("change", async function() {
    // Los archivos seleccionados, pueden ser muchos o uno
    const archivos = $seleccionArchivos.files;
    // Si no hay archivos salimos de la función y quitamos la imagen
    if (!archivos || !archivos.length) {
        $imagenPrevisualizacion.src = "";
        return;
    }
    // Ahora tomamos el primer archivo, el cual vamos a previsualizar
    const primerArchivo = archivos[0];

    let imgBase64 = await getBase64(primerArchivo)
    document.querySelector("#imageFlujo img").src = imgBase64;
    $("#txtImg").val(imgBase64);

});


//multiSelect Responsables
$(document).on("change", "#ContentPlaceHolder_ddlResponsables", (e) => {
    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_ddlResponsables").val() + " class=\"box-resp\"><div class=\"btnCloseResp\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_ddlResponsables option:selected").text() + "</div></div>";

    $("#ContentPlaceHolder_lstddlResponsables").html($("#ContentPlaceHolder_lstddlResponsables").html() + resp);
});
$(document).on("click", ".btnCloseResp i", (e) => {
    lstResp = document.getElementById("ContentPlaceHolder_lstddlResponsables");
    lstResp.removeChild(e.target.parentElement.parentElement);
});

//multiSelect Responsables


//MultiSelect  Recursos humanos
$(document).on("change", "#ContentPlaceHolder_ddlRecHumanos", (e) => {
    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_ddlRecHumanos").val() + " class=\"box-resp\"><div class=\"btnCloseRecHumanos\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_ddlRecHumanos option:selected").text() + "</div></div>";

    $("#ContentPlaceHolder_lstddlRecHumanos").html($("#ContentPlaceHolder_lstddlRecHumanos").html() + resp);
});
$(document).on("click", ".btnCloseRecHumanos i", (e) => {
    lstResp = document.getElementById("ContentPlaceHolder_lstddlRecHumanos");
    lstResp.removeChild(e.target.parentElement.parentElement);
});


$(document).on("change", "#ContentPlaceHolder_txtNormas", (e) => {
    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_txtNormas").val() + " class=\"box-resp\"><div class=\"btnCloseNormas\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_txtNormas option:selected").text() + "</div></div>";

    $("#lstNormas").html($("#lstNormas").html() + resp);
});

$(document).on("click", ".btnCloseNormas i", (e) => {
    lstResp = document.getElementById("lstNormas");
    lstResp.removeChild(e.target.parentElement.parentElement);
});



const Obtenercargos = (nomNodo) => {
    var cargos = "";

    divCargos = [...document.querySelectorAll("#" + nomNodo + " .box-resp")];
    for (var i in divCargos) {
        if (i == divCargos.length - 1)
            cargos += divCargos[i].querySelector("div:nth-child(2)").innerText;

        else
            cargos += divCargos[i].querySelector("div:nth-child(2)").innerText + ", "
    }
    return cargos;
};

const ObtenerSIPOC = () => {
    let filas = [...document.querySelectorAll("#tbActividades tbody tr")]
    let Sipoc = [];

    for (var i in filas) {
        Sipoc.push({
            'IntOidSipoc': 0,
            'StrProveedores': filas[i].querySelector("td:nth-child(1)").innerText,
            'StrEntrada': filas[i].querySelector("td:nth-child(2)").innerText,
            'StrTipoAct': filas[i].querySelector("td:nth-child(3)").innerText,
            'StrActividad': filas[i].querySelector("td:nth-child(4)").innerText,
            'StrClientes': filas[i].querySelector("td:nth-child(5)").innerText,
            'StrSalidas': filas[i].querySelector("td:nth-child(6)").innerText,
            'StrResponsables': filas[i].querySelector("td:nth-child(7)").innerText,
        })
    }
    return Sipoc;
};



$("#ContentPlaceHolder_btnCrearActividad").on("click", (e) => {
    e.preventDefault();
    $("#tbActividades thead").html(
        `<tr>   
             <th>Provedor(es)</th>
             <th>Entrada(s)</th>
             <th>Tipo Actividad</th>
             <th>Descripcion Actividad</th>
             <th>Cliente(s)</th>
             <th>Salida(s)</th>
             <th>Responsables</th>
             <th></th>
        </tr>`
    )

    $("#tbActividades tbody").html(
        $("#tbActividades tbody").html() +
        `<tr>
            <td>${ $("#ContentPlaceHolder_txtProvedores").val()}</td>
            <td>${ $("#ContentPlaceHolder_txtEntradas").val()}</td>
            <td >${$("#ContentPlaceHolder_ddlTipoActividad").val()}</td>
            <td >${$("#ContentPlaceHolder_txtActividad").val()}</td>
            <td >${$("#ContentPlaceHolder_txtClientes").val()}</td>
            <td >${$("#ContentPlaceHolder_txtSalidas").val()}</td>
            <td >${Obtenercargos("ContentPlaceHolder_lstddlResponsables")}</td>
            <td><i class="fa fa-trash" id="DeleteAct" ></i><i class="fa fa-edit ml-2" id="EditAct"></i></td>
        </tr>`
    );

    $("#ContentPlaceHolder_txtProvedores").val("");
    $("#ContentPlaceHolder_txtEntradas").val("");
    $("#ContentPlaceHolder_ddlTipoActividad").val("-1");
    $("#ContentPlaceHolder_txtActividad").val("");
    $("#ContentPlaceHolder_txtClientes").val("");
    $("#ContentPlaceHolder_txtSalidas").val("");
    $("#ContentPlaceHolder_lstddlResponsables").html("");
});

$(document).on("click", "#DeleteAct", (e) => {
    document.querySelector("#tbActividades tbody").removeChild(e.target.parentElement.parentElement);
});


function GuardarProceso(){

    Params = new URLSearchParams(window.location.search);
    idProceso = parseInt(Params.get('idProceso'));


    var proceso = {
        "IntOIdProceso": idProceso,
        "StrNomPro": $("#ContentPlaceHolder_txtNombre").val(),
        "StrEstado": "Activo",
        "StrTipo": $("#ContentPlaceHolder_ddlTipPro").val(),
        "StrProcesoPadre": $("#ContentPlaceHolder_ddlProcesoPadre").val(),
        "StrPrefijo": $("#ContentPlaceHolder_txtPrefijo").val(),
        "StrObjetivo": $("#ContentPlaceHolder_txtObjetivo").val(),
        "StrAlcance": $("#ContentPlaceHolder_txtAlcance").val(),
        "StrLideresProceso": $("#ContentPlaceHolder_ddlLideres").val(),
        "SIPOCs": ObtenerSIPOC(),
        "StrRecFinancieros": $("#ContentPlaceHolder_txtRecFinacieros").val(),
        "StrRecHumanos": Obtenercargos("ContentPlaceHolder_lstddlRecHumanos"),
        "StrNormas": Obtenercargos("lstNormas"),
        "StrRiesgos": $("#ContentPlaceHolder_txtRiesgos").val(),
        "IntOidGNListaArchivo": 0,
        "StrDocRelacionados": $("#ContentPlaceHolder_txtDocRelacionados").val(),
        "IntVersion": 0,
        "intOidGDSolicitud": idProceso,
        "StrRecursosFis": $("#ContentPlaceHolder_txtRecFis").val(),
        "StrRecursosInfo": $("#ContentPlaceHolder_txtRecInf").val(),
        "StrRecursosTec": $("#ContentPlaceHolder_txtRecTeg").val(),
        "IntOidGDDocumento": 0,
        "StrDocRelacionados": "",
        "IntGnDcDep": parseInt($("#ContentPlaceHolder_ddlUnidades").val()),
        "StrFlujoGrama": $("#txtImg").val(),
        "StrRecursosMed": $("#ContentPlaceHolder_txtRecMed").val()
    };

    $.ajax({ // metodo para enviar los datos al servidor.
        url: "CrearProceso.aspx/SetProceso",
        data: JSON.stringify({ "proceso": proceso }),
        dataType: "json",
        async: false,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            exito("success", "Proceso Creado");
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

const RealizarSolicitud = () => {


    Params = new URLSearchParams(window.location.search);
    idProceso = parseInt(Params.get('idProceso'));

    $.ajax({ // metodo para enviar los datos al servidor.
        url: "CrearProceso.aspx/realizarSolicitud",
        data: JSON.stringify({ "idProceso": idProceso }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            window.location.href = "VistaProcesos.aspx"
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}


$(document).on("click", "#ContentPlaceHolder_btnCrearPro", (e) => {
    e.preventDefault();
    GuardarProceso();
    location.reload();
})

$("#btnEnviarSol").click(function (e) {
    e.preventDefault();
    if (ValidarInformacion())
        error("Datos incompletos", "Diligencie todos los datos antes de enviar a revisión")
    else {
        
        $(".modal").modal()
    }
});

const ValidarInformacion = () => {
    return $("#ContentPlaceHolder_txtNomPro").val() == "" ||
        $("#ContentPlaceHolder_ddlTipPro").val() == "-1" ||
        $("#ContentPlaceHolder_txtPrefijo").val() == "" ||
        $("#ContentPlaceHolder_txtObjetivo").val() == "" ||
        $("#ContentPlaceHolder_txtAlcance").val() == "" ||
        $("#ContentPlaceHolder_txtRecFinacieros").val() == "" ||
        Obtenercargos("lstNormas") == "" ||
        $("#ContentPlaceHolder_txtRiesgos").val() == "" ||
        Obtenercargos("ContentPlaceHolder_lstddlRecHumanos") == "" ||
        ObtenerSIPOC().length == 0 ||
        $("#ContentPlaceHolder_txtRecFis").val() == "" ||
        $("#ContentPlaceHolder_txtRecInf").val() == "" ||
        $("#ContentPlaceHolder_txtRecTeg").val() == "" ||
        $("#txtImg").val() == "" ||
        $("#ContentPlaceHolder_ddlProcesoPadre").val() == "-1"


}



$("#ContentPlaceHolder_btnGuardarPro").on("click", function (e){
    e.preventDefault();
    GuardarProceso();
    RealizarSolicitud();
})

function CargarDatosProc(msg) {



    datos = JSON.parse(msg.d);
    console.log(datos);
    $("#ContentPlaceHolder_ddlTipPro").val(datos.StrTipo == "" ? "-1" : datos.StrTipo);
    $("#ContentPlaceHolder_txtPrefijo").val(datos.StrPrefijo);
    $("#ContentPlaceHolder_txtObjetivo").val(datos.StrObjetivo);
    $("#ContentPlaceHolder_txtAlcance").val(datos.StrAlcance);
    $("#ContentPlaceHolder_txtRecFinacieros").val(datos.StrRecFinancieros);
    $("#ContentPlaceHolder_txtRecTeg").val(datos.StrRecursosTec);
    $("#ContentPlaceHolder_txtRecInf").val(datos.StrRecursosInfo);
    $("#ContentPlaceHolder_txtRecFis").val(datos.StrRecursosFis);
    $("#ContentPlaceHolder_txtRiesgos").val(datos.StrRiesgos);
    $("#ContentPlaceHolder_txtRiesgos").val(datos.StrRiesgos);
    $("#ContentPlaceHolder_ddlLideres").val(datos.StrLideresProceso == "" ? "-1" : datos.StrLideresProceso);
    $("#ContentPlaceHolder_ddlUnidades").val(datos.IntGnDcDep == 0 ? "-1" : datos.IntGnDcDep);
    $("#ContentPlaceHolder_txtRecMed").val(datos.StrRecursosMed)
    $("#ContentPlaceHolder_ddlProcesoPadre").val(datos.StrProcesoPadre);
    if(datos.StrFlujoGrama != "")
        $("#imageFlujo img")[0].src = datos.StrFlujoGrama;
    $("#txtImg").val(datos.StrFlujoGrama)
    $("#ContentPlaceHolder_txtNombre").val(datos.StrNomPro);


    $("#tbActividades thead").html(
        `<tr>   
             <th>Provedor(es)</th>
             <th>Entrada(s)</th>
             <th>Tipo Actividad</th>
             <th>Descripcion Actividad</th>
             <th>Cliente(s)</th>
             <th>Salida(s)</th>
             <th>Responsables</th>
             <th></th>
        </tr>`
    )

    for (var i in datos.SIPOCs) {
        $("#tbActividades tbody").html(
            $("#tbActividades tbody").html() +
            `<tr>
            <td>${datos.SIPOCs[i].StrProveedores}</td>
            <td>${datos.SIPOCs[i].StrEntrada}</td>
            <td>${datos.SIPOCs[i].StrTipoAct}</td>
            <td>${datos.SIPOCs[i].StrActividad}</td>
            <td>${datos.SIPOCs[i].StrClientes}</td>
            <td>${datos.SIPOCs[i].StrSalidas}</td>
            <td>${datos.SIPOCs[i].StrResponsables}</td>
            <td><i class="fa fa-trash" id="DeleteAct" ></i><i class="fa fa-edit ml-2" id="EditAct"></i></td>
        </tr>`
        );
    }

    

    let RecursosH = datos.StrRecHumanos.split(",");
    for (var i in RecursosH) {
        let resp = "<div  class=\"box-resp\"><div class=\"btnCloseRecHumanos\"><i class=\"fa fa-close\"></i></div><div>" + RecursosH[i] + "</div></div>";
        $("#ContentPlaceHolder_lstddlRecHumanos").html($("#ContentPlaceHolder_lstddlRecHumanos").html() + (RecursosH[i] != "" )?resp : "");
    }

    let Normas = datos.StrNormas.split(",");
    Normas.forEach((norma) => {
        let resp = "<div  class=\"box-resp\"><div class=\"btnCloseNormas\"><i class=\"fa fa-close\"></i></div><div>" + norma + "</div></div>";
        $("#lstNormas").html($("#lstNormas").html() + (RecursosH[i] != "") ? resp : "");
    });
}

function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

const GetDatosProc = () => {
    Params = new URLSearchParams(window.location.search);
    idProceso = parseInt(Params.get('idProceso'));
    ejecutarajax("CrearProceso.aspx/GetProceso", {"idSolicitud": idProceso}, CargarDatosProc)
}

GetDatosProc();
