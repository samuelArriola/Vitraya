let tbDocsRev = $("#tbDocsRev");
let txtEstado = $("#txtEstado");
let txtTipDoc = $("#txtTipDoc");
let txtNomDoc = $("#txtNomDoc");
let datos;

let documento;






function cargarTablaDocumentos(msg) {
    datos = JSON.parse(msg.d);
    let tbody = "";

    for (var i in datos) {
        let estados = ["Preliminar", "En Construcción", "En Revisión", "En Aprobación", "Publicación"]
        switch (datos[i][0].StrTipDoc) {
            case "Indicador": {
                tbody += `
                            <tr>
                                 <td>${datos[i][0].StrNomDoc}</td>
                                 <td>${datos[i][0].StrTipDoc}</td>
                                 <td>${estados[datos[i][0].IntEstado]}</td>
                                 <td>
                                    <a
                                    href="javascript:window.open('Indicador/${datos[i][1]}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')">
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 3 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
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
                                    href="javascript:window.open('Proceso/${datos[i][1]}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')">>
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 3 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
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
                                    href="javascript:window.open('Procedimiento/${datos[i][1]}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')">
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 3 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
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
                                    href="javascript:window.open('Protocolo/${datos[i][1]}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')">
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 3 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
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
                                    href="javascript:window.open('Manual/${datos[i][1]}', '', 'width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700 }, top=10, toolbar=no')">
                                        <i class="fa fa-eye"></i>
                                    </a>
                                 </td>
                                 <td>${datos[i][0].IntEstado == 3 ? `<i class="fa fa-edit edit-doc" data-index="${i}" >` : ""}</td>   
                            </tr>
                            `
                break;

            }
        }

    }
    tbDocsRev.html(tbody);
    DataTable("#tableDocs");
}

const CargarDocsApro = () => {
    ejecutarajax("AprobarDocumento.aspx/GetDocumentos", {
        'NomDoc': txtNomDoc.val(),
        'Tipo': txtTipDoc.val(),
        'Estado': txtEstado.val()
    }, cargarTablaDocumentos)
}

CargarDocsApro();


$(document).on("click", ".edit-doc", (e) => {
    let element = e.target;
    i = parseInt(element.getAttribute("data-index"))
    documento = datos[i]


    $("#lbNomDoc").text(documento[0].StrNomDoc)
    $("#lbElaborador").text(documento[0].StrNomSolicitante)
    $("#lbTipo").text(documento[0].StrTipDoc)

    $("#event-modal").modal();
})



function AprobarRechazarRev(estado, idDocumento, detalles) {

    ejecutarajax("AprobarDocumento.aspx/AprobarRechazarRev", {
        'estado': estado,
        'idDocumento': idDocumento,
        'detalles': detalles,
    }, () => { window.location.reload() })
}

$("#btnAprobar").on("click", function (e) {
    e.preventDefault();
    AprobarRechazarRev(1, documento[0].IntOidGDDocumento, $("#txtDetalles").val());
})

$("#btnRechazar").on("click", function (e) {
    e.preventDefault();
    if ($("#txtDetalles").val() == "") {
        error("Error", "No se puede rechazar un documento sin un motivo");
        return;
    }
    AprobarRechazarRev(2, documento[0].IntOidGDDocumento, $("#txtDetalles").val());
})


txtEstado.on('change', CargarDocsApro)

txtTipDoc.on('keyup', CargarDocsApro)

txtNomDoc.on('keyup', CargarDocsApro)