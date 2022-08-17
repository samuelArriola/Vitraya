txtOperMej = $("#txtOperMej");
txtNoConf = $("#txtNoConf");
txtActividad = $("#txtActividad");
txtComo = $("#txtComo");
txtPorQue = $("#txtPorQue");
txtCuando = $("#txtCuando");
txtDonde = $("#txtDonde");
txtCuanto = $("#txtCuanto");
txtSoporte = $("#txtSoporte");
txtQuienSeguimiento = $("#txtQuienSeguimiento");
txtProceso = $("#txtProceso");


function crearEditorTextos() {
    tinymce.init({
        selector: '#ContentPlaceHolder_textEditor',
        height: 400,
        width: "100%",
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

crearEditorTextos();



function CargarInfoPlanAccion(msg) {
    let plan = msg.d[0];
    let avances = msg.d[1];


    txtActividad.text(plan.StrActividad);
    txtComo.text(plan.StrComo);
    txtCuando.text(moment(plan.DtmFecFinalActa).format("DD/MM/YYYY"));
    txtCuanto.text(plan.StrCuanto);
    txtDonde.text(plan.StrDonde);
    txtNoConf.text(plan.StrNoConf);
    txtOperMej.text(plan.StrOporMej);
    txtPorQue.text(plan.StrPorQue);
    txtProceso.text(plan.StrProceso)
    txtQuienSeguimiento.text(plan.StrNombreUsuarioSeguimiento);
    txtSoporte.text(plan.StrSoporte);
    let domAvance = ""
    avances.forEach(avance => {
        moment.locale('es');
        domAvance += `
            <li>
                <div class="block">
                    <div class="tags">
                        <a href="" class="tag">
                            <span></span>
                        </a>
                    </div>
                    <div class="block_content">
                        <h2 class="title">
                            ${avance[0].StrTitulo}
                        </h2>
                        <div class="byline">
                            ${moment(avance[0].DtmFecha).format('MMMM Do YYYY, h:mm:ss a')} 
                        </div>
                        <p class="excerpt">
                            ${avance[0].StrAvance}
                        </p>
                        ${avance[1].length > 0 ? "<h6>Archivos Adjuntos</h6><br>": ""}
                        <ol>
                    
               
        `;

        avance[1].forEach((archivo, i) => {
            domAvance += `
                <li style="border: 0">
                    <a href="../proceedings/Archivos.aspx?id=${archivo.IntOidGNArchivo}">${archivo.StrNombre}</a>
                </li>
        
            `
        });

        domAvance += `
                        </ol>
                    </div>
                </div>
            </li>            
        `
    });

    console.log(domAvance);

    $("#lsAvances").html(domAvance);
}


function GetInfoPlanAccion() {
    Params = new URLSearchParams(window.location.search);
    idPlanAccion = parseInt(Params.get('idPlanAccion'));

    ejecutarajax("AvancePlanAccion.aspx/GetInfoPlanAccion", {"idPlanAccion":idPlanAccion},CargarInfoPlanAccion)
}

$(document).ready(GetInfoPlanAccion);