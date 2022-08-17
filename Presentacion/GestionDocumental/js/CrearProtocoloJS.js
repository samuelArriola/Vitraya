const txtNombre = $("#txtNombre");
const txtAlcance = $("#txtAlcance");
const txtObjetivo = $("#txtObjetivo");
const ddlResponsable = $("#ContentPlaceHolder_ddlResponsable");
const ddlTalentoHumano = $("#ContentPlaceHolder_ddlTalentoHumano");
const txtDefiniciones = $("#txtDefiniciones");
const txtRecomendaciones = $("#txtRecomendaciones");
const txtAnexos = $("#txtAnexos");
const btnGuardar = $("#btnGuardar");
const btnRevision = $("#btnRevision");
const txtAtividades = $("#txtAtividades");
const txtNomDef = $("#txtNomDef");
const tbDefiniciones = $("#tbDefiniciones");
const ddlProcesos = $("#ContentPlaceHolder_ddlProcesos");
const txtEquiposBiomedicos = $("#txtEquiposBiomedicos");
const txtMedicamentos = $("#txtMedicamentos");
const txtRecInformaticos = $("#txtRecInformaticos");
const txtRefNorm = $("#txtRefNorm");
const txtIndicadores = $("#txtIndicadores");



let protocolo;

function crearEditorTextos(editor) {
    tinymce.init({
        selector: '#'+editor,
        height: 400,
        plugins: 'paste searchreplace  code visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern noneditable help charmap quickbars emoticons',
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
        browser_spellcheck: true,
       
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
        contextmenu:false,
        paste_data_images: true,
        
    });
}

function ValidarFormuloario() {
    return txtNombre.val() == "" ||
        txtAlcance.val() == "" ||
        txtObjetivo.val() == "" ||
        tinyMCE.get("txtAnexos").getContent() == "" ||
        ddlProcesos.val() == "-1" ||
        txtEquiposBiomedicos.val() == "" ||
        txtMedicamentos.val() == "" ||
        txtRecInformaticos.val() == "" ||
        LstToString("lstResponsable") == "" ||
        LstToString("lstTalHumano") == "" 
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

function addObjectDdl(e) {
    let resp = "<div data-idResp=" + e.data.ddl.value + " class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + e.data.ddl.options[e.data.ddl.selectedIndex].innerText + "</div></div>";
    e.data.nodeLst.innerHTML += resp;
    e.target.value = -1
}

function GuardarProtocolo() {
  
    let protocolo = {
        IntOidGDProtocolo: 0,
        IntOidGDDocumento: 0,
        StrNombre: txtNombre.val(),
        StrAlcance: txtAlcance.val(),
        StrObjetivo: txtObjetivo.val(),
        StrRecursos: "",
        StrDefiniciones: tinyMCE.get("txtDefiniciones").getContent(),
        StrRecomendaciones: tinyMCE.get("txtRecomendaciones").getContent(),
        StrRefNorm: tinymce.get("txtRefNorm").getContent(),
        StrResponsable: LstToString("lstResponsable"),
        StrAnexos: tinyMCE.get("txtAnexos").getContent(),
        IntOidGDProceso: parseInt(ddlProcesos.val()),
        StrRecHumanos: LstToString("lstTalHumano"),
        StrRecEquiposBiomedicos : txtEquiposBiomedicos.val(),
        StrRecInformaticos : txtRecInformaticos.val(),
        StrRecMedicamentos: txtMedicamentos.val(),
        StrActividad: tinymce.get("txtAtividades").getContent(),
        StrIndicadores: tinymce.get("txtIndicadores").getContent(),
    }

    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));

    ejecutarajax(
        "CrearProtocolo.aspx/UpdateProtocolo",
        {
            'protocolo': protocolo,
            'idSolicitud': OIdSolicitud,
            'version': parseInt($("#txtVersion").val()) || 0
        },
        function () { exito("Hecho", "Los datos del Protocolo han sido guardados") }
    );
}



function CargarProtocolo(msg) {

    //se obtine la informacion del protocolo que ya ha sido guardada
    protocolo = JSON.parse(msg.d)[0];

    //se obtiene la informacion de las actividades guradadas
    actividades = JSON.parse(msg.d)[1];

    //se obtiene la informacion de los indidcadores
    indicadores = JSON.parse(msg.d)[2];

    //se carga el contenido de los datos guardados previamente
    txtNombre.val(protocolo.StrNombre)
    txtAlcance.val(protocolo.StrAlcance)
    txtObjetivo.val(protocolo.StrObjetivo)
    ddlProcesos.val(protocolo.IntOidGDProceso)
    txtRecInformaticos.val(protocolo.StrRecInformaticos)
    txtEquiposBiomedicos.val(protocolo.StrRecEquiposBiomedicos)
    txtMedicamentos.val(protocolo.StrRecMedicamentos)
    txtAnexos.val(protocolo.StrAnexos)
    txtAtividades.val(protocolo.StrActividad)
    txtRefNorm.val(protocolo.StrRefNorm);
    txtIndicadores.val(protocolo.StrIndicadores);
    txtDefiniciones.val(protocolo.StrDefiniciones);
   

    txtRecomendaciones.val(protocolo.StrRecomendaciones)
   

    //se carga el contnido de los recursos humanos
    protocolo.StrRecHumanos.split(",").forEach(element => {
        if (element != "") {
            let lstTalHumano = $("#lstTalHumano");
            let resp = "<div class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + element + "</div></div>";
            lstTalHumano[0].innerHTML += resp;
        }
    });

    protocolo.StrResponsable.split(",").forEach(element => {
        if (element != "") {
            let lstResponsable = $("#lstResponsable");
            let resp = "<div class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + element + "</div></div>";
            lstResponsable[0].innerHTML += resp;
        }
    })
}


function EnviarRevision() {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax(
        "CrearProtocolo.aspx/EnviarRevision",
        {
            'idSolicitud': OIdSolicitud
        },
        () => {
            window.location.href = `ValidacionDibulgacion.aspx?IdSolicitud=${OIdSolicitud}&IdDocumento=${protocolo.IntOidGDDocumento}&NombreProc=${$("#txtNombre").val()}`
        }
    )
}

function GetProtocolo() {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax("CrearProtocolo.aspx/GetProtocolo", { 'idSolicitud': OIdSolicitud }, CargarProtocolo)
}



$(document).on("change", "#ContentPlaceHolder_ddlTalentoHumano", { 'ddl': ddlTalentoHumano[0], 'nodeLst': $("#lstTalHumano")[0] }, addObjectDdl)



$(document).on("change", "#ContentPlaceHolder_ddlResponsable", { 'ddl': ddlResponsable[0], 'nodeLst': $("#lstResponsable")[0] }, addObjectDdl)



$(document).on("click", ".btnCloseLst i", e => {
    let ParentNode = e.target.parentElement.parentElement.parentElement;
    ParentNode.removeChild(e.target.parentElement.parentElement);
});




$(document).on("click", ".edit-def", e => {
    row = e.target.parentElement.parentElement;
    txtNomDef.val(row.querySelector("td:nth-child(1)").innerHTML)
    txtDefiniciones.val(row.querySelector("td:nth-child(2)").innerHTML)
    row.parentElement.removeChild(row);
})

$(document).on("click", ".delete-act", e => {
    row = e.target.parentElement.parentElement;
    row.parentElement.removeChild(row);
})

$("#btnGuardar").click(function (e) { e.preventDefault(); GuardarProtocolo() });

$("#btnRevision").on("click", e => {
    e.preventDefault();
    if (ValidarFormuloario()) {
        error("Datos incompletos", "Llene todos todos los campos antes de enviar a revisión")
        return;
    }
    $("#event-modal").modal();
})

$("#ContentPlaceHolder_btnGuardarDoc").on("click",e => {
    e.preventDefault();
    GuardarProtocolo();
    EnviarRevision();
})

$("#crearDefinicion").on("click", e => {
    e.preventDefault();
    if (txtDefiniciones.val() == "" || txtNomDef.val() == "") {
        error("Fatan datos", "Llene todos los datos antes de cargar la definición")
        return;
    }
    dtDefiniciones = `
        <tr>
            <td>${txtNomDef.val()}</td>
            <td>${txtDefiniciones.val()}</td>
            <td><i class="fa fa-edit edit-def"></i></td>
            <td><i class="fa fa-trash delete-act"></i></td>
        </tr>
    `;
    tbDefiniciones[0].innerHTML += dtDefiniciones;
    txtDefiniciones.val("");
    txtNomDef.val("")
})

$(document).ready( function () {
    GetProtocolo();
    crearEditorTextos("txtAnexos");
    crearEditorTextos("txtAtividades");
    crearEditorTextos("txtRecomendaciones")
    crearEditorTextos("txtRefNorm")
    crearEditorTextos("txtIndicadores")
    crearEditorTextos("txtDefiniciones")
    $("#btnViewProtocolo").click(function () {
        let VHeight = window.innerHeight;
        let VWidth = window.innerWidth;
        window.open(`Protocolo/${protocolo.IntOidGDProtocolo}`, "", `width = 1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`)
    })
})

function GetDefiniciones() {
    tableAux = tbDefiniciones[0].cloneNode();
    tableAux.innerHTML = tbDefiniciones.html();
    rows = [...tableAux.querySelectorAll("tr")]
    for (i in rows) {
        rows[i].removeChild(rows[i].querySelector("td:nth-child(3)"))
        rows[i].removeChild(rows[i].querySelector("td:nth-child(3)"))
    }
    return tableAux.innerHTML;
}

$("#btnChangeTalHum").click(function () {
    if ($(this).find("i").hasClass("rotateAnimation")) {
        $(this).find("i").removeClass("rotateAnimation")
    }
    else {
        $(this).find("i").addClass("rotateAnimation")
    }
    if ($("#txtTalentoHumano").hasClass("d-none")) {
        $("#txtTalentoHumano").removeClass("d-none");
        ddlTalentoHumano.addClass("d-none");
    }
    else {
        $("#txtTalentoHumano").addClass("d-none");
        ddlTalentoHumano.removeClass("d-none");
    }
});

$("#txtResponsable").keypress(function (e) {
    if (e.keyCode == 13 && this.value.trim() != "") {
        let resp = "<div  class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + this.value + "</div></div>";
        $("#lstResponsable").append(resp);
        this.value = "";
    }
})

$("#txtTalentoHumano").keypress(function (e) {
    if (e.keyCode == 13) {
        let resp = "<div  class=\"box-resp\"><div class=\"btnCloseLst\"><i class=\"fa fa-close\"></i></div><div>" + this.value + "</div></div>";
        $("#lstTalHumano").append(resp);
        this.value = "";
    }
});

$("form").submit(function (e) { e.preventDefault() })
    .keypress(function (e) { if (e.keyCode == 13) e.preventDefault() });
$("#btnChangeResponsable").click(function () {
    if ($(this).find("i").hasClass("rotateAnimation")) {
        $(this).find("i").removeClass("rotateAnimation")
    }
    else {
        $(this).find("i").addClass("rotateAnimation")
    }
    if (ddlResponsable.hasClass("d-none")) {
        ddlResponsable.removeClass("d-none");
        $("#txtResponsable").addClass("d-none");
    }
    else {
        ddlResponsable.addClass("d-none");
        $("#txtResponsable").removeClass("d-none");
    }
})