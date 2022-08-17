

let contador = 0;
//metodo que crea el editor de textos para desarrollar un tema
function crearEditorTextos() {
    tinymce.init({
        selector: '#ContentPlaceHolder_txtEditor',
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
            { title: 'Nueva Tabla', description: 'creates a new table', content: '<div class="mceTmpl"><table width="98%%"  border="0" cellspacing="0" cellpadding="0"><tr><th scope="col"> </th><th scope="col"> </th></tr><tr><td> </td><td> </td></tr></table></div>' },
            { title: 'Starting my story', description: 'A cure for writers block', content: 'Once upon a time...' },
            { title: 'New list with dates', description: 'New List with dates', content: '<div class="mceTmpl"><span class="cdate">cdate</span><br /><span class="mdate">mdate</span><h2>My List</h2><ul><li></li><li></li></ul></div>' }
        ],
        template_cdate_format: '[Date Created (CDATE): %m/%d/%Y : %H:%M:%S]',
        template_mdate_format: '[Date Modified (MDATE): %m/%d/%Y : %H:%M:%S]',
        height: 600,
        image_caption: true,
        quickbars_selection_toolbar: 'bold italic | quicklink h2 h6 blockquote quickimage quicktable',
        noneditable_noneditable_class: "mceNonEditable",
        toolbar_mode: 'sliding',
        contextmenu: "link image imagetools table",
    });
}

crearEditorTextos();


//evento que se le da al boton para mostrar o ocultar el menu de los temas
$("#circle_left").on("click", function () {
    let content = document.getElementById("ContentPlaceHolder_content_menu_left");
    let btnMenuRight = document.getElementById("circle_left");
    let arrowLeft = btnMenuRight.querySelector("i");
    if (content.style.transform == "translate(0px, -50%)") {
        content.style.transform = "translate(-500px, -50%)";
        btnMenuRight.style.transform = "translate(-525px, -50%)";
        arrowLeft.style.transform = "rotate(180deg)";
       
    }
    else {
        content.style.transform = "translate(0px, -50%)";
        btnMenuRight.style.transform = "translate(-50%, -50%)";
        arrowLeft.style.transform = "translateY(-50%)";
    }    
});

const menu = (idTema, idActa) => {

    Params = new URLSearchParams(window.location.search);
    idtemaD = parseInt(Params.get('idTema'));

    $.ajax({
        url: "DesarrollarTemas.aspx/navMenu",
        headers: {
            "Transfer-Encoding": "gzip",
            "Content-Type": "application/json; charset=utf-8"
        },
        data: JSON.stringify({ 'idTemaD': idtemaD, 'idTema': idTema, 'idActa': idActa, 'desarrollo': tinyMCE.get("ContentPlaceHolder_txtEditor").getContent() }),
        dataType: "json",
        type: "POST",
        success: function (msg) {
            window.location.href = msg.d;
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

const GuardarUltimoTema = () => {
    Params = new URLSearchParams(window.location.search);
    idtema = parseInt(Params.get('idTema'));
   
    $.ajax({
        url: "DesarrollarTemas.aspx/btnGuardarActa_Click",
        data: JSON.stringify({'idTema': idtema,'desarrollo': tinyMCE.get("ContentPlaceHolder_txtEditor").getContent() }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            if (msg.d) {
                $("#event-modal").modal();
            }
            else {
                error("Temas Incompletos", "Desarrolle todos los temas antes de guardar y cerar el acta");
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

$(document).on("click", "#ContentPlaceHolder_btnGuardarActa", GuardarUltimoTema);

$("#btnPreView").on("click", (e) => {
    window.open(`${window.origin}/ActasReunion/Acta/${idActa}`, '', `width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no`)
});

function GuardarDesarrolloTema() {
    Params = new URLSearchParams(window.location.search);
    idtemaD = parseInt(Params.get('idTema'));
    idActa = parseInt(Params.get('idActa'));

    $.ajax({
        url: "DesarrollarTemas.aspx/navMenu",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'idTemaD': idtemaD, 'idTema': idtemaD, 'idActa': idActa, 'desarrollo': tinyMCE.get("ContentPlaceHolder_txtEditor").getContent() }),
        dataType: "json",
        type: "POST",
        success: function (msg) {
            
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

function GuardarTemaAutomaticamente() {
    if (contador != 0) {
        GuardarDesarrolloTema();
    }
    contador++;

    setTimeout(GuardarTemaAutomaticamente, 30000);
}

GuardarTemaAutomaticamente();

$("#btnGuardarAvance").on("click", function (e) { e.preventDefault(), GuardarDesarrolloTema(); exito("Hecho", "El desarrollo del tema ha sido actualizado"); });