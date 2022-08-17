
function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function SetListaPendiente(msg2) {

    lista = msg2.d;
    var contenido = "";

    lista.forEach((item2) => {

        if (item2.intOidGnScriptsBloqueos == 1 && item2.strResultConsulta == 0) {
            contenido += `<p>${item2.strNombre} : DILIGENCIADA </p>`;
        }
        else if (item2.intOidGnScriptsBloqueos == 1 && item2.strResultConsulta != 0) {
            contenido += `<p>${item2.strNombre} : NO DILIGENCIADA </p>`;
        }
        else {
            contenido += `<p>${item2.strNombre} : ${item2.strResultConsulta} </p>`;
        }
    })
    $("#ListPendientes").html(contenido);

}

function listarPendientes() {

    var nomPagina = getParameterByName('opcion');

    $("#nombreOpcion").html(nomPagina);

    ejecutarajax("PlantillaBloqueo.aspx/ObtenerPendientes", {}, SetListaPendiente);

}

$(document).ready(listarPendientes);