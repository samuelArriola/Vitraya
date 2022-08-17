const txtVersion = $("#txtVersion");
const txtIntroducion = $("#txtIntroducion");
const txtObjetivos = $("#txtObjetivos");
const txtAlcance = $("#txtAlcance");
const txtMarcoLegal = $("#ContentPlaceHolder_ddlNormas");
const txtDesarrollo = $("#txtDesarrollo");
const txtTermino = $("#txtTermino");
const txtGlosario = $("#txtGlosario");
const btnGuardar = $("#btnGuardar");
const btnRevision = $("#btnRevision");
const btnGuardarDef = $("#btnGuardarDef");
const lstDefiniciones = $("#lstDefiniciones");
const btnGuardarInd = $("#ContentPlaceHolder_btnGuardarInd");
const ddlProcesos = $("#ContentPlaceHolder_ddlProcesos");
const txtNombre = $("#txtNombre");
const txtRecFin = $("#txtRecFin");
const txtTalHum = $("#txtTalHum");
const txtEquipos = $("#txtEquipos");
const txtMedicamentos = $("#txtMedicamentos");
const txtRecInfo = $("#txtRecInfo");
const ddlProcs = $("#ContentPlaceHolder_ddlProcs");

let manual;

$("#ContentPlaceHolder_ddlProcs option[value='Protocolos']").attr("disabled", true);
$("#ContentPlaceHolder_ddlProcs option[value='Procedimientos']").attr("disabled", true);



function crearEditorTextos(editor) {
    tinymce.init({
        selector: editor,
        height: 400,
        plugins: 'print preview paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap quickbars emoticons',
        imagetools_cors_hosts: ['picsum.photos'],
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | fullscreen  preview save print | insertfile image media template link anchor codesample | ltr rtl',
        toolbar_sticky: false,
        language:"es",
        autosave_ask_before_unload: false,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        image_advtab: true, importcss_append: true,
        file_picker_callback: function (cb, value, meta) {
            var input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');
            input.onchange = function () {
                var file = this.files[0];

                var reader = new FileReader();
                reader.onload = function () {

                    var id = 'blobid' + (new Date()).getTime();
                    var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                    var base64 = reader.result.split(',')[1];
                    var blobInfo = blobCache.create(id, file, base64);
                    blobCache.add(blobInfo);
                    cb(blobInfo.blobUri(), { title: file.name });
                };
                reader.readAsDataURL(file);
            };

            input.click();
        },
        templates: [
            { title: 'New Table', description: 'creates a new table', content: '<div class="mceTmpl"><table width="98%%"  border="0" cellspacing="0" cellpadding="0"><tr><th scope="col"> </th><th scope="col"> </th></tr><tr><td> </td><td> </td></tr></table></div>' },
            { title: 'Starting my story', description: 'A cure for writers block', content: 'Once upon a time...' },
            { title: 'New list with dates', description: 'New List with dates', content: '<div class="mceTmpl"><span class="cdate">cdate</span><br /><span class="mdate">mdate</span><h2>My List</h2><ul><li></li><li></li></ul></div>' }
        ],
        template_cdate_format: '[Date Created (CDATE): %m/%d/%Y : %H:%M:%S]',
        template_mdate_format: '[Date Modified (MDATE): %m/%d/%Y : %H:%M:%S]',
        height: 600,
        image_caption: false,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h6 blockquote quickimage quicktable',
        noneditable_noneditable_class: "mceNonEditable",
        toolbar_mode: 'sliding',
        contextmenu: "link image imagetools table",
        paste_data_images: true,
        browser_spellcheck: true,
    });
}




function CargarMamual() {
    let manual = {
        IntOidGDDocumento: 0,
        IntOidGDManual: 0,
        //StrIntroduccion: txtIntroducion.val(),
        StrIntroduccion: tinyMCE.get("txtIntroducion").getContent(),
        //StrObjetivos: txtObjetivos.val(),
        StrObjetivos: tinyMCE.get("txtObjetivos").getContent(),
        StrMarcoLegal: tinyMCE.get("txtMarcoLegal").getContent(),
        StrAlcance: txtAlcance.val(),
        StrGlosario: tinyMCE.get("txtGlosario").getContent(),
        StrAnexos: tinyMCE.get("txtEditor").getContent(),
        StrFormatos: "",
        StrDesarrollo: tinyMCE.get("txtDesarrollo").getContent(),
        IntOidGDProceso: ddlProcesos.val(),
        StrNombre: txtNombre.val(),
        StrRecInfo: txtRecInfo.val(),
        StrMedicamentos: txtMedicamentos.val(),
        StrEquipos: txtEquipos.val(),
        StrTalentoHumano: txtTalHum.val(),
        StrRecFin: txtRecFin.val(),
        StrProcs: LstToString("lstProcs")
    };

    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));

    ejecutarajax(
        "CrearManual.aspx/UpdateManual",
        {
            "manual": manual,
            "idSolicitud": OIdSolicitud,
            "version": parseInt($("#txtVersion").val()) || 0  //en caso de que no se haya ingresado la versión se envía como versión 0
        },
        function(){
            exito("Datos guardados", "Los datos han sido guardados correctamente")
        }
    )
}


const LstToString = (nomNodo) => {
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

function CargarDatos(msg) {

    manual = JSON.parse(msg.d);

    txtVersion.val(manual.StrVersion);

    //txtIntroducion.val(manual.StrIntroduccion);
    tinyMCE.get("txtIntroducion").setContent(manual.StrIntroduccion);

    //txtObjetivos.val(manual.StrObjetivos)
    tinyMCE.get("txtObjetivos").setContent(manual.StrObjetivos);
    txtAlcance.val(manual.StrAlcance);
    tinyMCE.get("txtEditor").setContent(manual.StrAnexos);
    tinyMCE.get("txtDesarrollo").setContent(manual.StrDesarrollo);
    tinyMCE.get("txtMarcoLegal").setContent(manual.StrMarcoLegal);
    lstDefiniciones.html(manual.StrGlosario);
    ddlProcesos.val(manual.IntOidGDProceso);
    txtNombre.val(manual.StrNombre);
    txtRecFin.val(manual.StrRecFin);
    txtRecInfo.val(manual.StrRecInfo);
    txtTalHum.val(manual.StrTalentoHumano);
    txtEquipos.val(manual.StrEquipos);
    txtMedicamentos.val(manual.StrMedicamentos);
    tinyMCE.get("txtGlosario").setContent(manual.StrGlosario);

    //manual.StrMarcoLegal.split(",").forEach(norma => {
    //    if (!norma)
    //        return;
    //    let lstNormas = $("#lstNormas");
    //    let resp = "<div class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + norma + "</div></div>";
    //    lstNormas[0].innerHTML += resp;
    //});

    manual.StrProcs.split(",").forEach(proc => {
        if (proc == "")
            return;
        let lstProcs = $("#lstProcs");
        let resp = "<div class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + proc + "</div></div>";
        lstProcs[0].innerHTML += resp;
    });
}

function GetManual() {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax("CrearManual.aspx/GetManual", {"idSolicitud": OIdSolicitud}, CargarDatos)
} 

btnGuardar.on("click", e => {
    e.preventDefault();
    CargarMamual();
});

btnGuardarDef.on("click", e => {
    e.preventDefault();
    let strTermino = txtTermino.val();
    let strDefinicion = txtGlosario.val();

    let strGlosario = `
        <li class="mb-2">
            <span style="font-weight: 900">${strTermino}</span>
            <p>${strDefinicion}</p>
            <i class="fa fa-close ml-1 btn btn-danger delete-def"></i>
            <i class="fa fa-edit ml-1 btn btn-success edit-def"></i>
        </li>
    `
    lstDefiniciones.html(lstDefiniciones.html() + strGlosario);
    txtTermino.val("");
    txtGlosario.val("");
})

$(document).on("click", ".delete-def", e => {
    e.preventDefault();
    e.target.parentElement.parentElement.removeChild(e.target.parentElement);
})

$(document).on("click", ".edit-def", e => {
    let strTermino = e.target.parentElement.querySelector("span").innerText;
    let strDefinicion = e.target.parentElement.querySelector("p").innerText;

    txtTermino.val(strTermino);
    txtGlosario.val(strDefinicion);

    e.preventDefault();
    e.target.parentElement.parentElement.removeChild(e.target.parentElement);
});

btnRevision.on("click", e => {
    e.preventDefault();
    if (ValidarFormulario()) {
        error("Datos Incompletos", "Diligencie el acta antes de enviar a revisión")
        return;
    }
    $("#event-modal").modal();
});

btnGuardarInd.on("click", e => {
    e.preventDefault();
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax(
        "CrearManual.aspx/EnviarRevision",
        {
            "idSolicitud": OIdSolicitud
        },
        () => { window.location.href = `ValidacionDibulgacion.aspx?IdSolicitud=${OIdSolicitud}&IdDocumento=${manual.IntOidGDDocumento}&NombreProc=${$("#txtNombre").val()}` }
    );
});

$("#btnChangeControl").click(function (e) {
    if (ddlProcs.hasClass("d-none")) {
        ddlProcs.removeClass("d-none");
        $("#txtProcedimientos").addClass("d-none")
    }
    else {
        ddlProcs.addClass("d-none");
        $("#txtProcedimientos").removeClass("d-none")
    }
});

$(document).on("click", ".btnCloseLst i", e => {
    let ParentNode = e.target.parentElement.parentElement.parentElement;
    ParentNode.removeChild(e.target.parentElement.parentElement);
});

function addObjectDdl(e) {
    let resp = "<div data-idResp=" + e.data.ddl.value + " class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + e.data.ddl.options[e.data.ddl.selectedIndex].innerText + "</div></div>";
    e.data.nodeLst.innerHTML += resp;
}


$(document).on("change", "#ContentPlaceHolder_ddlNormas", { 'ddl': txtMarcoLegal[0], 'nodeLst': $("#lstNormas")[0] }, addObjectDdl)
$(document).on("change", "#ContentPlaceHolder_ddlProcs", { 'ddl': ddlProcs[0], 'nodeLst': $("#lstProcs")[0] }, addObjectDdl)

function ValidarFormulario() {
    return txtAlcance.val() == "" ||
        tinyMCE.get("txtIntroducion").getContent() == "" ||
        txtMarcoLegal.val() == "" ||
        tinyMCE.get("txtObjetivos").getContent() == "" ||
        txtMedicamentos.val() == "" ||
        txtRecFin.val() == "" ||
        txtRecInfo.val() == ""||
        txtTalHum.val() == "" ||
        txtEquipos.val() == "" ||
        tinyMCE.get("txtDesarrollo").getContent() == "" ||
        tinyMCE.get("txtEditor").getContent() == "" ||
        getGlosario() == "" 
        //LstToString("lstNormas") == "";
}

$(document).ready(function (e) {
    $("#btnViewManual").click(function () {
        let VHeight = window.innerHeight;
        let VWidth = window.innerWidth;
        window.open(`Manual/${manual.IntOidGDManual}`, "", `width = 1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`)
    })
    crearEditorTextos("#txtEditor");
    crearEditorTextos("#txtDesarrollo");
    crearEditorTextos("#txtGlosario");
    crearEditorTextos("#txtMarcoLegal");
    crearEditorTextos("#txtIntroducion");
    crearEditorTextos("#txtObjetivos");
});

function getGlosario() {
    let glosario = document.createElement("ul");
    glosario.innerHTML = lstDefiniciones.html()
    lis = [...glosario.querySelectorAll("li")];
    for (i in lis) {
        btns = [...lis[i].querySelectorAll("i")];
        for (j in btns) {
            lis[i].removeChild(btns[j])
        }
    }
    return glosario.innerHTML;
}

$("#txtProcedimientos").keypress(function(e){
    if (e.keyCode == 13) {
        let resp = "<div class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + this.value + "</div></div>";
        $("#lstProcs").append(resp);
    }
})

$("form").submit(e => { e.preventDefault() }).keypress(e => {if (e.keyCode == 13) e.preventDefault() });



window.onload = GetManual;