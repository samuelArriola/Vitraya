tablePlanesAccion = $("#tablePlanesAccion");
txtResponsable = $("#txtResponsable");
txtFecha = $("#txtFecha");
txtLugar = $("#txtLugar");
columnPlan = $(".columna .p-3");

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
txtUsuCrea = $("#txtUsuCrea");

let planes;


function BuscarPlan(estado, cadena) {
    cadena = cadena.toLowerCase();
    planesB = []
    planes.forEach(plan => {
        if ((plan.IntEstAct == estado) &&
            (
                plan.StrActividad.toLowerCase().includes(cadena) ||
                plan.StrComo.toLowerCase().includes(cadena) ||
                plan.StrCuanto.toLowerCase().includes(cadena) ||
                plan.StrDonde.toLowerCase().includes(cadena) ||
                plan.StrProceso.toLowerCase().includes(cadena) ||
                plan.StrSoporte.toLowerCase().includes(cadena) ||
                plan.StrNombreUsuarioResponsable.toLowerCase().includes(cadena) ||
                moment(plan.DtmFecFinalActa).format("DD/MM/YYYY").includes(cadena)
            )) {
            planesB.push(plan);
        }
    })

    $(columnPlan[estado - 1]).find(".card_k").remove();

    planesB.forEach((plan, index) => {
        //se crea el contenido html para las cartas de los planes de accion
        switch (plan.IntEstAct) {
            case 1: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[0].innerHTML += cardHtml;
                break;
            }
            case 2: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                           
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[1].innerHTML += cardHtml;
                break;
            }
            case 3: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                           
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                            <i class="fa fa-check icon-blue btn-resp-plan" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Iniciar revisión"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[2].innerHTML += cardHtml;
                break;
            }
            case 4: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[3].innerHTML += cardHtml;
                break;
            }
        }
        
    });
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
}

function CargarPlanesAccion(msg){
    planes = msg.d;
    console.log(planes)

    Params = new URLSearchParams(window.location.search);
    let indexPag = parseInt(Params.get('index'));


    //Se cargan todas las cards con la informacion de los planes de accion
    planes.forEach((plan, index) => {
        //se crea el contenido html para las cartas de los planes de accion


        switch (plan.IntEstAct) {
            case 1: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[0].innerHTML += cardHtml;
                break;
            }
            case 2: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                           
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[1].innerHTML += cardHtml;
                break;
            }
            case 3: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                           
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                            <i class="fa fa-check icon-blue btn-resp-plan" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Iniciar revisión"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[2].innerHTML += cardHtml;
                break;
            }
            case 4: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioResponsable}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                        </h6>
                    </div>
                `;

                // se agregan la cartas recien creadas al panel de los plnes de accion
                columnPlan[3].innerHTML += cardHtml;
                break;
            }
        }
    });
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
}


$(document).on("click", ".btn-resp-plan", function () {

    let idPlanAccion = parseInt($(this).attr("data-id"))
    window.location.href = "RevisarPlanAccion.aspx?IdPlanAccion=" + idPlanAccion;
});

function GetPlanesAccion() {
    let datos = {
        nombre:"",
        lugar: "",
        fecha: new Date("01/01/3000")
    };

    ejecutarajax("AdminPlanAccion.aspx/GetPlanesAccionByIdSeg", datos, CargarPlanesAccion )
}

$(document).ready(function () {
    GetPlanesAccion();
});

$(document).on("click", ".btn-view-plan", function (e) {
    e.preventDefault();
    let index = parseInt($(this).attr("data-index"))
    let plan = planes[index];

    txtActividad.text(plan.StrActividad);
    txtComo.text(plan.StrComo);
    txtCuando.text(moment(plan.DtmFecFinalActa).format("DD/MM/YYYY"));
    txtCuanto.text(plan.StrCuanto);
    txtDonde.text(plan.StrDonde);
    txtNoConf.text(plan.StrFuente);
    txtOperMej.text(plan.StrOrigen);
    txtPorQue.text(plan.StrPorQue);
    txtProceso.text(plan.StrProceso)
    txtQuienSeguimiento.text(plan.StrNombreUsuarioSeguimiento);
    txtSoporte.text(plan.StrSoporte);
    txtUsuCrea.text(plan.StrNombreUsuarioAprueba);

    GetAvances(plan.IntOidPAPlanAccion);

    $("#modal-view-plan").modal();
});


$(document).on("keypress", "#txtSearchAsignado", e => {
    if (e.keyCode == 13) {
        BuscarPlan(1, $(e.target).val())
    }
})

$(document).on("keypress", "#txtSearchProceso", e => {
    if (e.keyCode == 13) {
        BuscarPlan(2, $(e.target).val())
    }
})

$(document).on("keypress", "#txtSearchEvaluacion", e => {
    if (e.keyCode == 13) {
        BuscarPlan(3, $(e.target).val())
    }
})

$(document).on("keypress", "#txtSearchTerminado", e => {
    if (e.keyCode == 13) {
        BuscarPlan(4, $(e.target).val())
    }
})

function CargarAvances(msg) {

    console.log(msg.d)
    let avances = msg.d;
    let articulo = ""

    $("#pnInfoAvances").html("");
    avances.forEach((dato, index) => {
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
    $("#pnInfoAvances").html(articulo);
}

function GetAvances(idPlanAccion) {
    console.log(idPlanAccion);
    ejecutarajax(window.location.origin + "/PlanAccion/RevisarPlanAccion.aspx/GetAvancesByIdPlan", { 'idPlanAccion': idPlanAccion}, CargarAvances)
}

$(document).on('dblclick', 'input[list]', function (event) {
    event.preventDefault();
    var str = $(this).val();
    $('div[list=' + $(this).attr('list') + '] span').each(function (k, obj) {
        if ($(this).html().toLowerCase().indexOf(str.toLowerCase()) < 0) {
            $(this).hide();
        }
    })
    $('div[list=' + $(this).attr('list') + ']').toggle(100);
    $(this).focus();
})

$(document).on('blur', 'input[list]', function (event) {
    event.preventDefault();
    var list = $(this).attr('list');
    setTimeout(function () {
        $('div[list=' + list + ']').hide(100);
    }, 100);
})

$(document).on('click', 'div[list] span', function (event) {
    event.preventDefault();
    var list = $(this).parent().attr('list');
    var item = $(this).html();
    $('input[list=' + list + ']').val(item);
    $('div[list=' + list + ']').hide(100);
})

$(document).on('keyup', 'input[list]', function (event) {
    event.preventDefault();
    var list = $(this).attr('list');
    var divList = $('div[list=' + $(this).attr('list') + ']');
    if (event.which == 27) { 
        $(divList).hide(200);
        $(this).focus();
    }
    else if (event.which == 13) { 
        if ($('div[list=' + list + '] span:visible').length == 1) {
            var str = $('div[list=' + list + '] span:visible').html();
            $('input[list=' + list + ']').val(str);
            $('div[list=' + list + ']').hide(100);
        }
    }
    else if (event.which == 9) {
        $('div[list]').hide();
    }
    else {
        $('div[list=' + list + ']').show(100);
        var str = $(this).val();
        $('div[list=' + $(this).attr('list') + '] span').each(function () {
            if ($(this).html().toLowerCase().indexOf(str.toLowerCase()) < 0) {
                $(this).hide(200);
            }
            else {
                $(this).show(200);
            }
        });
    }
})

