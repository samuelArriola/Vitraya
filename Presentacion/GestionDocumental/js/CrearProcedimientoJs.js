
let Procedimiento;
async function crearEditorTextos(editor) {
    tinymce.init({
        selector: '#' + editor,
        plugins: 'print preview powerpaste  importcss searchreplace autolink   directionality code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap quickbars emoticons',
        imagetools_cors_hosts: ['picsum.photos'],
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | fullscreen  preview save print | insertfile image media template link anchor codesample | ltr rtl',
        image_advtab: true,
       
        importcss_append: true,
        file_picker_callback: function (callback, value, meta) {
            /* Provide file and text for the link dialog */
            if (meta.filetype === 'file') {
                callback('https://www.google.com/logos/google.jpg', { text: 'My text' });
            }

            /* Provide image and alt text for the image dialog */
            if (meta.filetype === 'image') {
                callback('https://www.google.com/logos/google.jpg', { alt: 'My alt text' });
            }

            /* Provide alternative source and posted for the media dialog */
            if (meta.filetype === 'media') {
                callback('movie.mp4', { source2: 'alt.ogg', poster: 'https://www.google.com/logos/google.jpg' });
            }
        },
        templates: [
            { title: 'New Table', description: 'creates a new table', content: '<div class="mceTmpl"><table width="98%%"  border="0" cellspacing="0" cellpadding="0"><tr><th scope="col"> </th><th scope="col"> </th></tr><tr><td> </td><td> </td></tr></table></div>' },
            { title: 'Starting my story', description: 'A cure for writers block', content: 'Once upon a time...' },
            { title: 'New list with dates', description: 'New List with dates', content: '<div class="mceTmpl"><span class="cdate">cdate</span><br /><span class="mdate">mdate</span><h2>My List</h2><ul><li></li><li></li></ul></div>' }
        ],
        template_cdate_format: '[Date Created (CDATE): %m/%d/%Y : %H:%M:%S]',
        template_mdate_format: '[Date Modified (MDATE): %m/%d/%Y : %H:%M:%S]',
        height: 400,
        // image_caption: false,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h6 blockquote quickimage quicktable',
        noneditable_noneditable_class: "mceNonEditable",
        toolbar_mode: 'sliding',
        contextmenu: true,
        browser_spellcheck: true,
        powerpaste_allow_local_images: true,
        powerpaste_word_import: 'prompt',
        powerpaste_html_import: 'prompt',
    });
}



$("select").val("-1");

$("form").keypress(function (e) { if (e.keyCode == 13) e.preventDefault() });
$("form").submit(function (e) { e.preventDefault() });


const $seleccionArchivos = document.querySelector("#ContentPlaceHolder_fuImageFlujo"),
    $imagenPrevisualizacion = document.querySelector("#imageFlujo");

// Escuchar cuando cambie
$seleccionArchivos.addEventListener("change", async function() {
    const archivos = $seleccionArchivos.files;
    if (!archivos || !archivos.length) {
        $imagenPrevisualizacion.src = "";
        return;
    }
    const file = archivos[0];
    imagen = document.querySelector("#imageFlujo img");
    imagen.src = await getBase64(file);
    $("#txtImagen").val(await getBase64(file));
})


//multiSelect Responsables
$(document).on("change", "#ContentPlaceHolder_ddlResponsable", (e) => {
    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_ddlResponsable").val() + " class=\"box-resp\"><div class=\"btnCloseResp\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_ddlResponsable option:selected").text() + "</div></div>";

    $("#ContentPlaceHolder_lstResponsables").html($("#ContentPlaceHolder_lstResponsables").html() + resp);
    $(e.target).val("-1");
});
$(document).on("click", ".btnCloseResp i", (e) => {
    lstResp = document.getElementById("ContentPlaceHolder_lstResponsables");

    lstResp.removeChild(e.target.parentElement.parentElement);
});


//multiSelect Talento Humano 
$(document).on("change", "#ContentPlaceHolder_ddlTalHumano", (e) => {
    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_ddlTalHumano").val() + " class=\"box-resp\"><div class=\"btnCloseRespTH\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_ddlTalHumano option:selected").text() + "</div></div>";

    $("#ContentPlaceHolder_lsTalHumano").html($("#ContentPlaceHolder_lsTalHumano").html() + resp);
    $(e.target).val("-1");

});
$(document).on("click", ".btnCloseRespTH i", (e) => {
    lstResp = document.getElementById("ContentPlaceHolder_lsTalHumano");
    lstResp.removeChild(e.target.parentElement.parentElement);
});


$("#btn-change-th").click(function (e) {
    if ($("#txtTalentoHumano").hasClass("d-none")) {
        $("#txtTalentoHumano").removeClass("d-none");
        $("#ContentPlaceHolder_ddlTalHumano").addClass("d-none");
    }
    else {
        $("#txtTalentoHumano").addClass("d-none");
        $("#ContentPlaceHolder_ddlTalHumano").removeClass("d-none");
    }
})
$("#txtTalentoHumano").keypress(function (e) {
    if (e.keyCode == 13 && this.value.trim() != "") {
        let resp = "<div  class=\"box-resp\"><div class=\"btnCloseRespTH\"><i class=\"fa fa-close\"></i></div><div>" + this.value + "</div></div>";
        $("#ContentPlaceHolder_lsTalHumano").append(resp)
        this.value = ""
    }
})



//MultiSelect Referencias Indicadores



const Obtenercargos = (nomNodo) =>{
    var cargos = "";
  
    divCargos = [...document.querySelectorAll("#" + nomNodo + " .box-resp")];
    for (var i in divCargos) {
        if (i == divCargos.length - 1)
            cargos += divCargos[i].querySelector("div:nth-child(2)").innerText;

        else
            cargos += divCargos[i].querySelector("div:nth-child(2)").innerText + ", "
    }
    return cargos;
}


function ValidarInformacion() {
    return $("#ContentPlaceHolder_txtAlcance").val() == "" ||
        Obtenercargos("ContentPlaceHolder_lstResponsables") == "" ||
        $("#ContentPlaceHolder_ddlResponsable").val() == "" ||
        $("#ContentPlaceHolder_txtEntradas").val() == "" ||
        $("#ContentPlaceHolder_txtSalidas").val() == "" ||
        $("#ContentPlaceHolder_txtProEsperado").val() == "" ||
        $("#ContentPlaceHolder_txtEstCalidad").val() == "" ||
        $("#ContentPlaceHolder_txtClientes").val() == "" ||
        $("#ContentPlaceHolder_txtEquiBiomedicos").val() == "" ||
        $("#ContentPlaceHolder_txtMEDiMEInsumos").val() == "" ||
        $("#ContentPlaceHolder_txtProvedores").val() == "" ||
        $("#ContentPlaceHolder_txtRFinancieros").val() == "" ||
        $("#ContentPlaceHolder_txtRInformticos").val() == "" ||
        Obtenercargos("ContentPlaceHolder_lsTalHumano") == "" ||
        $("#txtNombre").val() == "";
}


function GuardarDatos() {
    var lsTalHumano = "";

    var lstResponsables = "";

    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    var DocProcedimiento = {
        'IntOIdGdDocprocedimiento': 0,
        'IntOidGDDocumento': 0,
        'IntOidGNListaArchivo': 0,
        'IntOidRevisor': 0,
        'IntOidAprobador': 0,
        'StrNomProceso': $("#ContentPlaceHolder_ddlProcesos option:selected").text() ,
        'StrNomProcedimiento': $("#txtNombre").val(),
        'StrAlcance': $("#ContentPlaceHolder_txtAlcance").val(),
        'StrObjetivo': $("#ContentPlaceHolder_txtObjetivo").val(),
        'StrResponsable': Obtenercargos("ContentPlaceHolder_lstResponsables"),
        'StrRecursosNecesarios': "",
        'StrEntradas': $("#ContentPlaceHolder_txtEntradas").val(),
        'StrSalidas': $("#ContentPlaceHolder_txtSalidas").val(),
        'StrProEsperado': $("#ContentPlaceHolder_txtProEsperado").val(),
        'StrEstCalidad': $("#ContentPlaceHolder_txtEstCalidad").val(),
        'StrRefNormativas': tinymce.get("txtNormas").getContent(),
        'StrDefiniciones': tinymce.get("txtDescripcion").getContent(),
        'StrAnexos': tinymce.get("ContentPlaceHolder_txtAnexos").getContent(),
        'StrNomAprobador': "",
        'StrNomRevisor': "",
        'StrDocRelacionados': "",
        'DtFechaC': new Date(),
        'DtFechaRevision': new Date("1800-01-01"),
        'DtFechaAprobacion': new Date("1800-01-01"),
        'StrClientes': $("#ContentPlaceHolder_txtClientes").val(),
        'StrEquipos': $("#ContentPlaceHolder_txtEquiBiomedicos").val(),
        'StrMedicamentos': $("#ContentPlaceHolder_txtMEDiMEInsumos").val(),
        'StrProveedores': $("#ContentPlaceHolder_txtProvedores").val(),
        'StrRecFin': $("#ContentPlaceHolder_txtRFinancieros").val(),
        'StrRecInfo': $("#ContentPlaceHolder_txtRInformticos").val(),
        'StrTalentoHumano': Obtenercargos("ContentPlaceHolder_lsTalHumano"),
        'IntOidGDProceso': parseInt($("#ContentPlaceHolder_ddlProcesos").val()) || 0,
        'StrFlujoGrama': $("#txtImagen").val(),
        'StrActividad': tinymce.get("ContentPlaceHolder_txtEditor").getContent(),
        'StrIndicadores': tinymce.get("txtIndicadores").getContent(),
        'StrDocumentosRelacionados': tinymce.get("txtDocumentosRelacionados").getContent(),
    }

    indicadores = [];


    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));

    console.log( $.ajax({ // metodo para enviar los datos al servidor.
        url: "CrearProcedimiento.aspx/setUpdateDocPro",
        data: JSON.stringify({ "Procedimiento": DocProcedimiento, 'idSolicitud': OIdSolicitud,   "version": parseInt($("#txtVersion").val()) || 0 }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            exito("success", "Procedimiento Actualizado");
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    }));
}

$(document).on("click", "#ContentPlaceHolder_btnGuardar", (e) => {
    e.preventDefault();

    GuardarDatos();
})

function EnviarDocRevision() {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));

    $.ajax({ // metodo para enviar los datos al servidor.
        url: "CrearProcedimiento.aspx/EnviarDocumentoRevision",
        data: JSON.stringify({'idSolicitud': OIdSolicitud}),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            window.location.href = `ValidacionDibulgacion.aspx?IdSolicitud=${OIdSolicitud}&IdDocumento=${Procedimiento.IntOidGDDocumento}&NombreProc=${$("#txtNombre").val()}`;
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}


$("ContentPlaceHolder_fuImageFlujo").change(function () { $("form").submit() });

$("#ContentPlaceHolder_btnEnviarRevision").click(e => {
    e.preventDefault();
    if (ValidarInformacion()) {
        error("Datos incompletos", "Por Favor llene todos los campos antes de enviar la solicitud revisión")
        return;
    }
    $("#event-modal").modal();
});


$("#ContentPlaceHolder_btnGuardarInd").click(e => {
    e.preventDefault()
    GuardarDatos();
    EnviarDocRevision();
});


function CargarDocumento(msg) {
    Procedimiento = JSON.parse(msg.d)[0];
     
    $("#ContentPlaceHolder_txtAlcance").val(Procedimiento.StrAlcance);
    $("#ContentPlaceHolder_txtObjetivo").val(Procedimiento.StrObjetivo);
    $("#ContentPlaceHolder_txtEntradas").val(Procedimiento.StrEntradas);
    $("#ContentPlaceHolder_txtSalidas").val(Procedimiento.StrSalidas);
    $("#ContentPlaceHolder_txtProEsperado").val(Procedimiento.StrProEsperado);
    $("#ContentPlaceHolder_txtEstCalidad").val(Procedimiento.StrEstCalidad);
    $("#ContentPlaceHolder_txtAnexos").val(Procedimiento.StrAnexos);
    $("#ContentPlaceHolder_txtClientes").val(Procedimiento.StrClientes);
    $("#ContentPlaceHolder_txtEquiBiomedicos").val(Procedimiento.StrEquipos);
    $("#ContentPlaceHolder_txtMEDiMEInsumos").val(Procedimiento.StrMedicamentos);
    $("#ContentPlaceHolder_txtProvedores").val(Procedimiento.StrProveedores);
    $("#ContentPlaceHolder_txtRFinancieros").val(Procedimiento.StrRecFin);
    $("#ContentPlaceHolder_txtRInformticos").val(Procedimiento.StrRecInfo);
    $("#ContentPlaceHolder_ddlProcesos").val(Procedimiento.IntOidGDProceso);
    $("#txtImagen").val(Procedimiento.StrFlujoGrama);
    $("#txtNombre").val(Procedimiento.StrNomProcedimiento);
    $("#ContentPlaceHolder_txtEditor").val(Procedimiento.StrActividad);
    $("#txtNormas").val(Procedimiento.StrRefNormativas);
    $("#txtIndicadores").val(Procedimiento.StrIndicadores);
    $("#txtDocumentosRelacionados").val(Procedimiento.StrDocumentosRelacionados)
    $("#txtDescripcion").val(Procedimiento.StrDefiniciones)
    
    imagen = document.querySelector("#imageFlujo img");
    if(Procedimiento.StrFlujoGrama != "")
        imagen.src = Procedimiento.StrFlujoGrama;
   

    Procedimiento.StrResponsable.split(",").forEach(responsable => {
        
        resp = "<div  class=\"box-resp\"><div class=\"btnCloseResp\"><i class=\"fa fa-close\"></i></div><div>" + responsable + "</div></div>";
        if (responsable != "")
            $("#ContentPlaceHolder_lstResponsables").append(resp);
    });


    Procedimiento.StrTalentoHumano.split(",").forEach(talHum => {
      
        if (talHum != "") {
            let resp = "<div  class=\"box-resp\"><div class=\"btnCloseRespTH\"><i class=\"fa fa-close\"></i></div><div>" + talHum + "</div></div>";
            $("#ContentPlaceHolder_lsTalHumano").append(resp)
        }
    });
}



function GetProcedimiento() {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    $.ajax({ // metodo para enviar los datos al servidor.
        url: "CrearProcedimiento.aspx/GetProcedimiento",
        data: JSON.stringify({ 'IdSolicitud': OIdSolicitud }),
        async: true,
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: CargarDocumento,
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}



$(document).ready(function () {
    GetProcedimiento();
    crearEditorTextos("ContentPlaceHolder_txtEditor");
    crearEditorTextos("txtDescripcion");
    crearEditorTextos("txtIndicadores");
    crearEditorTextos("txtNormas");
    crearEditorTextos("ContentPlaceHolder_txtAnexos")
    crearEditorTextos("txtDocumentosRelacionados")


    $("#btnViewProcedimiento").click(function () {
        let VHeight = window.innerHeight;
        let VWidth = window.innerWidth;
        window.open(`Procedimiento/${Procedimiento.IntOIdGdDocprocedimiento}`, "", `width = 1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`)
    })
});


async function GetImage(file) {
    return await getBase64(file)
}

function getBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

$("#btnChangeControlResponsable").click(function () {
    if ($("#txtResponsable").hasClass("d-none")) {
        $("#txtResponsable").removeClass("d-none")
        $("#ContentPlaceHolder_ddlResponsable").addClass("d-none");
    }
    else {
        $("#txtResponsable").addClass("d-none")
        $("#ContentPlaceHolder_ddlResponsable").removeClass("d-none");
    }
})


$("#txtResponsable").keypress(function (e) {
    if (e.keyCode == 13) {
        let resp = "<div  class=\"box-resp\"><div class=\"btnCloseResp\"><i class=\"fa fa-close\"></i></div><div>" + this.value + "</div></div>";

        $("#ContentPlaceHolder_lstResponsables").html($("#ContentPlaceHolder_lstResponsables").html() + resp);
        this.value = ""
    }
});




