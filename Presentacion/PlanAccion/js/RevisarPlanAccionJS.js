
function CargarPlanesAccion(msg) {
    let Datos = msg.d;

    let articulo = "";

    Datos.forEach((dato, index) => {
        moment.locale('es');
        articulo += `
            <li>
                <div class="block">
                    <div class="block_content">
                        <h2 class="title" style="font-weight:600">
                           ${dato[0].StrTitulo}
                        </h2>
                        <div class="byline">
                             ${moment(dato[0].DtmFecha).format('MMMM Do YYYY, h:mm:ss a')}  
                        </div>
                        <p class="excerpt">
                           ${dato[0].StrAvance}
                        </p>
                        <h2 class="title" style="font-size: 13px">
                          Archivos Adjuntos
                        </h2>
                        <ul>
        `;

        dato[1].forEach((archivo, i) => {
            articulo += `
                <li style="border: 0">
                    <a href="../proceedings/Archivos.aspx?id=${archivo.IntOidGNArchivo}">${archivo.StrNombre}</a>
                </li>
        
            `
        });

        articulo += `
                </ul>
                    </div>
                </div>
            </li>
        `;
       
    });
    $("#listaAvances").html(articulo);
}

function EnviarRevision(revision, notas = "") {
    Params = new URLSearchParams(window.location.search);
    let idPlanAccion = parseInt(Params.get('IdPlanAccion'));

    let datos = {
        idPlanAccion: idPlanAccion,
        revision: revision,
        notas: notas,
    };

    ejecutarajax(
        "RevisarPlanAccion.aspx/SetRevisarPlanAccion",
        datos,
        function () {
            exito("Plan de Acción revisado", revision ? "El plan de acción ha sido marcado como cumplido." : "El plan de acción ha sido rechazado y se ha enviado  una  notificación con motivo de rechazo")
            setInterval(() => {window.location.href = "AdminPlanAccion.aspx" }, 5000)
        }
    )
}

function GetAvances() {

    Params = new URLSearchParams(window.location.search);
    let idPlanAccion = parseInt(Params.get('IdPlanAccion'));

    ejecutarajax("RevisarPlanAccion.aspx/GetAvancesByIdPlan", {idPlanAccion: idPlanAccion}, CargarPlanesAccion)
}

$("#btnAceptar").on("click", function (e) {
    e.preventDefault();
    EnviarRevision(true);
});

$("#btnRechazar").on("click", function (e) {
    e.preventDefault();
    $("#modalRechazo").modal();
});

$("#btnEnviarRechazo").click(function (e) {
    let motivo = $("#taMotivo").val()

    if (motivo.trim() == "") {
        error("Error", "No se puede rechazar un plan de acción sin ningún motivo")
        return;
    }

    EnviarRevision(false,motivo)
})

$(document).ready(function () {
    GetAvances();
});