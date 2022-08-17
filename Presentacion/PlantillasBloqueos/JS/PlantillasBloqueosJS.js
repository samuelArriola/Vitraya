

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

var link = "";

function SetLinkPagina(msg) {

    info = msg.d;

    info.forEach((item) => {

        link = item.stringPrefijo;

    })

}

$(document).on("click", ".btnContinuar", function (e) {

    location.href = link;

})

function SetListaPendiente(msg2) {

    lista = msg2.d;
    var contenido = "";

    lista.forEach((item2) => {

        if (item2.intOidGnScriptsBloqueos == 1 && item2.strResultConsulta != 0) {
           contenido += `<p>${item2.strNombre} : DILIGENCIADA </p>`;
        }
        else if (item2.intOidGnScriptsBloqueos == 1 && item2.strResultConsulta == 0) {
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
    var idOpcion = getParameterByName('idOpcion');

    ejecutarajax("PlantillaBloqueoPedagogico.aspx/ObtenerPendientes", {}, SetListaPendiente);

    $("#nombreOpcion").html(nomPagina);

    datos = {
        "id": idOpcion
    }

    ejecutarajax("PlantillaBloqueoPedagogico.aspx/ObtenerOpcion", datos, SetLinkPagina);
}

$(document).ready(listarPendientes);