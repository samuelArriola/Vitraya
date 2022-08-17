let tbDocsRev = $("#tbDocsRev");
let txtEstado = $("#txtEstado");
let txtTipDoc = $("#txtTipDoc");
let txtNomDoc = $("#txtNomDoc");
let datos;

let documento;




function openDocument(link) {
    let VHeight = window.innerHeight;
    let VWidth = window.innerWidth;
    window.open(`${link}`, "", `width = 1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`)
}

const cargarTablaDocumentos = (msg) => {
    datos = JSON.parse(msg.d);
    let tbody = "";

    for (var i in datos) {
        let estados = ["Preliminar", "En Construcción","En Revisión", "En Aprobación", "Publicación"]
        switch (datos[i][0].StrTipDoc) {
            case "Indicador": {
                tbody += `
                            <tr>
                                 <td>${datos[i][0].StrNomDoc}</td>
                                 <td>${datos[i][0].StrTipDoc}</td>
                                 <td>${estados[datos[i][0].IntEstado]}</td>
                                 <td>
                                    <a
                                    href="javascript:openDocument('Indicador/${datos[i][1]}')">
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 1 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >`: ""}</td>   
                            </tr>
                            `
                break;
            }
            case "Proceso": {
                tbody += `
                            <tr>
                                 <td>${datos[i][0].StrNomDoc}</td>
                                 <td>${datos[i][0].StrTipDoc}</td>
                                 <td>${estados[datos[i][0].IntEstado]}</td>
                                 <td>
                                    <a
                                    href="javascript:openDocument('Proceso/${datos[i][1]}')" >
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 1 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
                            </tr>
                            `
                break;
            }
            case "Procedimiento": {
                tbody += `
                            <tr>
                                 <td>${datos[i][0].StrNomDoc}</td>
                                 <td>${datos[i][0].StrTipDoc}</td>
                                 <td>${estados[datos[i][0].IntEstado]}</td>
                                 <td>
                                    <a
                                    href="javascript:openDocument('Procedimiento/${datos[i][1]}')" >
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 1 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
                            </tr>
                            `
                break;

            }
            case "Protocolo": {
                tbody += `
                            <tr>
                                 <td>${datos[i][0].StrNomDoc}</td>
                                 <td>${datos[i][0].StrTipDoc}</td>
                                 <td>${estados[datos[i][0].IntEstado]}</td>
                                 <td>
                                    <a
                                    href="javascript:openDocument('Protocolo/${datos[i][1]}')" >
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 1 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
                            </tr>
                            `
                break;

            }
            case "Manual": {
                tbody += `
                            <tr>
                                 <td>${datos[i][0].StrNomDoc}</td>
                                 <td>${datos[i][0].StrTipDoc}</td>
                                 <td>${estados[datos[i][0].IntEstado]}</td>
                                 <td>
                                    <a
                                    href="javascript:openDocument('Manual/${datos[i][1]}')" >
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 1 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
                            </tr>
                            `
                break;

            }
        }
        
    }
    tbDocsRev.html(tbody);
    DataTable("#tableDocs");
}

$(document).on("click", ".edit-doc",(e) => {
    let element = e.target;
    i = parseInt(element.getAttribute("data-index"))
    documento = datos[i]

    $("#lbNomDoc").text(documento[0].StrNomDoc)
    $("#lbElaborador").text(documento[0].StrNomSolicitante)
    $("#lbTipo").text(documento[0].StrTipDoc)

    $("#event-modal").modal();
})


const obtenerRev = (nomNodo) => {
    var cargos = [];

    divCargos = [...document.querySelectorAll("#" + nomNodo + " .box-resp")];
    for (var i in divCargos) {
        cargos.push(parseInt(divCargos[i].getAttribute("data-idResp")));
    }
    return cargos;
}


ejecutarajax("AsignarRevisionAprobacion.aspx/GetGocumentos", {
    'NomDoc': txtNomDoc.val(),
    'Tipo':txtTipDoc.val(),
    'Estado':txtEstado.val()
}, cargarTablaDocumentos)


txtEstado.on('change', (e) => {
    e.preventDefault();
    ejecutarajax("AsignarRevisionAprobacion.aspx/GetGocumentos", {
        'NomDoc': txtNomDoc.val(),
        'Tipo': txtTipDoc.val(),
        'Estado': txtEstado.val()
    }, cargarTablaDocumentos)
})

txtTipDoc.on('keyup', (e) => {
    e.preventDefault();
    ejecutarajax("AsignarRevisionAprobacion.aspx/GetGocumentos", {
        'NomDoc': txtNomDoc.val(),
        'Tipo': txtTipDoc.val(),
        'Estado': txtEstado.val()
    }, cargarTablaDocumentos)
})

txtNomDoc.on('keyup', (e) => {
    e.preventDefault();
        ejecutarajax("AsignarRevisionAprobacion.aspx/GetGocumentos", {
        'NomDoc': txtNomDoc.val(),
        'Tipo': txtTipDoc.val(),
        'Estado': txtEstado.val()
    }, cargarTablaDocumentos)
})

$("form").submit((e) => { e.preventDefault(); })


$(document).on("change", "#ContentPlaceHolder_ddlRevisores", (e) => {

    if ($(e.target).val() == $("#ContentPlaceHolder_ddlAprobador").val()) {
        error("Error", "El usuario Revisor no puede ser el mismo usuarion Aprobador");
        $(e.target).val("-1");
        return;
    }

    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_ddlRevisores").val() + " class=\"box-resp\"><div class=\"btnCloseResp\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_ddlRevisores option:selected").text() + "</div></div>";

    $("#lstRevisores").html($("#lstRevisores").html() + resp);
});

$(document).on("change", "#ContentPlaceHolder_ddlAprobador", (e) => {
    let idRevisores = obtenerRev("lstRevisores");
    for (let i = 0; i < idRevisores.length; i++) {
        if (idRevisores[i] == $(e.target).val()) {
            error("Error", "El usuario Revisor no puede ser el mismo usuarion Aprobador");
            $(e.target).val("-1");
            return;
        }
    }
});


$(document).on("click", ".btnCloseResp i", (e) => {
    lstResp = document.getElementById("lstRevisores");
    lstResp.removeChild(e.target.parentElement.parentElement);
});

$("#ContentPlaceHolder_btnAsignar").on("click", (e) => {
    let aprobador = {
        'IntOidGDAprobacion':0,
        'IntOidGDDocumento': documento[0].IntOidGDDocumento,
        'IntOidRevisor': parseInt($("#ContentPlaceHolder_ddlAprobador").val()),
        'IntEstado':0,
        'StrDetalles': "", 
        'DtmFecha': new Date(),
        'StrCargo': ""
    }

    let revisores = [];
    let idRevisores = obtenerRev("lstRevisores") 
    idRevisores.forEach((idrevisor) => {
        revisores.push({
            'IntOidGDRevision': 0,
            'IntOidGDDocumento': documento[0].IntOidGDDocumento,
            'IntOidRevisor': idrevisor,
            'IntEstado': 0,
            'StrDetalles': "",
            'DtmFecha': new Date(),
            'StrCargo': ""
        })
    })
    ejecutarajax("AsignarRevisionAprobacion.aspx/AsignarRevisores", {
        'revisiones':revisores,
        'aprobacion': aprobador,
        'idDocumento': documento[0].IntOidGDDocumento
    }, (e) => {
            window.location.reload()
    })

    
})