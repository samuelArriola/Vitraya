const txtIntroducion = $("#txtIntroducion");
const txtObjetivos = $("#txtObjetivos");
const txtAlcance = $("#txtAlcance");
const txtMarcoLegal = $("#txtMarcoLegal");
const txtDesarrollo = $("#txtDesarrollo");
const txtTermino = $("#txtTermino");
const txtGlosario = $("#txtGlosario");
const btnGuardar = $("#btnGuardar");
const btnRevision = $("#btnRevision");
const btnGuardarDef = $("#btnGuardarDef");
const lstDefiniciones = $("#lstDefiniciones");
const btnGuardarInd = $("#ContentPlaceHolder_btnGuardarInd");
const ddlProcesos = $("#ContentPlaceHolder_ddlProcesos");
const txtObjetivosEsp = $("#txtObjetivosEsp");
const txtNombre = $("#txtNombre");
let Politica;
function crearEditorTextos(editor) {
    tinymce.init({
        selector: editor,
        height: 400,
        plugins: 'print preview paste importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap quickbars emoticons',
        imagetools_cors_hosts: ['picsum.photos'],
        menubar: 'file edit view insert format tools table help',
        toolbar: 'undo redo | bold italic underline strikethrough | fontselect fontsizeselect formatselect | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | fullscreen  preview save print | insertfile image media template link anchor codesample | ltr rtl',
        toolbar_sticky: false,
        autosave_ask_before_unload: false,
        autosave_interval: "30s",
        autosave_prefix: "{path}{query}-{id}-",
        autosave_restore_when_empty: false,
        autosave_retention: "2m",
        image_advtab: true,
        content_css: '//www.tiny.cloud/css/codepen.min.css',
        link_list: [
            { title: 'My page 1', value: 'http://www.tinymce.com' },
            { title: 'My page 2', value: 'http://www.moxiecode.com' }
        ],
        image_list: [
            { title: 'My page 1', value: 'http://www.tinymce.com' },
            { title: 'My page 2', value: 'http://www.moxiecode.com' }
        ],
        image_class_list: [
            { title: 'None', value: '' },
            { title: 'Some class', value: 'class-name' }
        ],
        importcss_append: true,
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
    });
}

crearEditorTextos("#txtEditor");


function CargarPolitica() {
    Politica = {
        IntOidGDDocumento: 0,
        IntOidGDPolitica: 0,
        StrIntroduccion: txtIntroducion.val(),
        StrObjetivos: txtObjetivos.val(),
        StrMarcoLegal: txtMarcoLegal.val(),
        StrAlcance: txtAlcance.val(),
        StrGlosario: getGlosario(),
        StrAnexos: tinyMCE.get("txtEditor").getContent(),
        StrFormatos: "",
        StrDesarrollo: tinyMCE.get("txtDesarrollo").getContent(),
        IntOidGDProceso: ddlProcesos.val(),
        StrObjetivosEsp: txtObjetivosEsp.val(),
        StrNombre: txtNombre.val()
    };

    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));

    ejecutarajax(
        "CrearPolitica.aspx/UpdatePolitica",
        {
            "Politica": Politica,
            "idSolicitud": OIdSolicitud,
            "version": parseInt($("#txtVersion").val()) || 0
        },
        function () {
            success("Datos guardados", "Los datos han sido guardados correctamente")
        })
}

function CargarDatos(msg) {
    Politica = JSON.parse(msg.d);
    txtIntroducion.val(Politica.StrIntroduccion);
    txtObjetivos.val(Politica.StrObjetivos);
    txtMarcoLegal.val(Politica.StrMarcoLegal);
    txtAlcance.val(Politica.StrAlcance);
    tinyMCE.get("txtEditor").setContent(Politica.StrAnexos);
    tinyMCE.get("txtDesarrollo").setContent(Politica.StrDesarrollo);
    lstDefiniciones.html(Politica.StrGlosario);
    ddlProcesos.val(politica.IntOidGDProceso);
    txtNombre.val(politica.StrNombre)

    lis = [...lstDefiniciones[0].querySelectorAll("li")]
    for (i in lis) {
        lis[i].innerHTML += `
            <i class="fa fa-close ml-1 btn btn-danger delete-def"></i>
            <i class="fa fa-edit ml-1 btn btn-success edit-def"></i>
        `
    }
}

function GetPolitica() {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax("CrearPolitica.aspx/GetPolitica", { "idSolicitud": OIdSolicitud }, CargarDatos)
}

btnGuardar.on("click", e => {
    e.preventDefault();
    CargarPolitica();
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
    $(".modal").modal();
});

btnGuardarInd.on("click", e => {
    e.preventDefault();
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax(
        "CrearPolitica.aspx/EnviarRevision",
        {
            "idSolicitud": OIdSolicitud
        },
        () => { window.location.href = `ValidacionDibulgacion.aspx?IdDocumento=${Politica.IntOidGDDocumento}` }
    );
});

function ValidarFormulario() {
    return txtAlcance.val() == "" ||
        txtIntroducion.val() == "" ||
        txtMarcoLegal.val() == "" ||
        txtObjetivos.val() == "" ||
        tinyMCE.get("txtDesarrollo").getContent() == "" ||
        tinyMCE.get("txtEditor").getContent() == "" ||
        getGlosario() == "";
}

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



window.onload = GetPolitica;