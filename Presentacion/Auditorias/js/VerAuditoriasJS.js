

let datos = [];
indexDatos = 0;

function cargarAuditoriasExternas(msg) {
    datos = msg.d;
    let contentAuditorias = ""; //variable que va contener el codigo HTML para la tabla de la auditorias
    console.log(msg.d);
    datos.forEach((dato,index) => {
        let auditoria = dato.Auditoria;
        moment.locale('es');
        contentAuditorias += `
             <div class="col-12 col-sm-6 col-md-4 col-lg-3  card-container p-1">
                <div class="card-flip">
                    <div class="card-flip-front">
                        <div class="card-head">
                            <div class="card-icono">
                                <img src="../Images/auditor.svg"/>
                            </div>
                        </div>
                        <div>
                            <h2 class="card-title">Auditoria realizada por: ${auditoria.StrEnteAuditor} el dia ${moment(auditoria.DtmFecha).format("dddd,  D [de] MMMM [de] YYYY")}</h2>
                        </div>
                       <div class="btn-view">
                           <button class="btn btn-secondary form-control btn-ver" data-index="${index}">
                               <i class="fa fa-eye mr-2" style="font-size:16px"></i>Ver información
                           </button>
                       </div>
                    </div>
                    <div class="card-flip-back">
                      
                    </div>
                </div>
            </div>
        `;
        $("#panel-auditorias").html(contentAuditorias);
    });
}

$("#btn-asignar").click(e => {
    e.preventDefault();
    window.location.href = `CargarPlanAccion.aspx?index=0&idAuditoria=${datos[indexDatos].Auditoria.IntOIdAuditoriaExterna}&ContextoAuditoria=2`;
});


$(document).on("click", ".btn-ver", function (e) {
    e.preventDefault();

    let index = parseInt($(this).attr("data-index"));

    indexDatos = index;

    console.log(index);

    auditoria = datos[index].Auditoria;
    auditores = datos[index].Auditores;
    procesos = datos[index].Procesos;
    archivos = datos[index].Archivos;

    $("#txtFecha").text(moment(auditoria.DtmFecha).format("dddd,  D [de] MMMM [de] YYYY"));
    $("#txtEnteAuditor").text(auditoria.StrEnteAuditor);


    $("#txtAuditores").html(
        (function(list){
            auditoresText = "";
            list.forEach(item => {
                auditoresText += `${item}<br/>`
            });
            return auditoresText;
        })(auditores)
    );

    $("#txtObjetivo").text(auditoria.StrObjetivo);
    $("#txtProcesos").html(
        (function (list) {
            procesosText = "";
            list.forEach(item => {
                procesosText += `${item.StrNomPro}<br/>`
            });
            return procesosText;
        })(procesos)
    );
    $("#txtAlcance").text(auditoria.StrAlcance);
    $("#txtAnexo").html(
        (function (archivos) {
            txtArchivos = "";
            archivos.forEach(archivo => {
                txtArchivos += `<a href="${window.location.origin}/proceedings/Archivos.aspx?id=${archivo.IntOidGNArchivo}">${archivo.StrNombre}.${archivo.StrExt}</a><br/>`;
            });
            return txtArchivos;
        })(archivos)
    );
    $(".modal").modal();

});

function GetAuditorias() {
    ejecutarajax("VerAuditorias.aspx/GetAuditoriaExternas", {},cargarAuditoriasExternas)
}

$(document).ready(GetAuditorias);