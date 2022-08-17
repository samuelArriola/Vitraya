let planes = []
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
txtUsuCreador = $("#txtUsuCreador");

txtSearchAsignado = $("#txtSearchAsignado");
txtSearchProceso = $("#txtSearchProceso");
txtSearchEvaluacion = $("#txtSearchEvaluacion");
txtSearchTerminado = $("#txtSearchTerminado");

columnPlan = $(".columna .p-3")

panelPlanes = $("#panelPlanes");



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
                plan.StrNombreUsuarioSeguimiento.toLowerCase().includes(cadena) ||
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
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                            <i class="glyphicon glyphicon-new-window icon-blue btn-avance" data-toggle="tooltip" data-id="${plan.IntOidPAPlanAccion}" data-placement="top" title="Cargar avance"></i>
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
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                            <i class="glyphicon glyphicon-new-window icon-blue btn-avance" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Cargar avance"></i>
                            <i class="fa fa-check icon-green btn-cerrar" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Enviar a revisión"></i>
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
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
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
                columnPlan[2].innerHTML += cardHtml;
                break;
            }
            case 4: {
                let cardHtml = `
                    <div class="card_k">
                         <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            <i class="fa fa-calendar mr-3"></i>${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}
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

function CargarPlanes(msg) {
    planes = msg.d;

    Params = new URLSearchParams(window.location.search);
    let indexPag = parseInt(Params.get('index'));
    console.log(planes)

    //Se cargan todas las cards con la informacion de los planes de accion
    planes.forEach((plan, index) => {
        //se crea el contenido html para las cartas de los planes de accion

        switch (plan.IntEstAct) {
            case 1: {
                let cardHtml = `
                    <div class="card_k">
                        <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
                        </div>
                        
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                            
                        </h6>
                        <h6 class="text-right">
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                            <i class="glyphicon glyphicon-new-window icon-blue btn-avance" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Cargar avance"></i>
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
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            
                            <i class="fa fa-eye icon-blue btn-view-plan" data-index="${index}" data-toggle="tooltip" data-placement="top" title="Ver plan de acción"></i>
                            <i class="glyphicon glyphicon-new-window icon-blue btn-avance" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Cargar avance"></i>
                            <i class="fa fa-check icon-green btn-cerrar" data-id="${plan.IntOidPAPlanAccion}" data-toggle="tooltip" data-placement="top" title="Enviar a revisión"></i>
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
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
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
                columnPlan[2].innerHTML += cardHtml;
                break;
            }
            case 4: {
                let cardHtml = `
                    <div class="card_k">
                         <div class="text-center">
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Responsable de seguimiento:&nbsp;</span>
                            <h6>${plan.StrNombreUsuarioSeguimiento}</h6>
                        </div>
                        <p>${plan.StrActividad}</p>
                        <h6>
                            <span style="font-size:13px; color:#21618C;font-weight: 600">Codigo de plan de accion: <b>${plan.IntOidPAPlanAccion}</b></span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha De Asignacion:&nbsp;</span><span style="color: rgb(0, 180,0)">${moment(plan.DtmFecIniActa).format("DD/MM/YYYY")}</span></br>
                            <span style="color: #ccc; font-size: 13px">Fecha Limite:&nbsp;</span><span style="color: ${new Date(plan.DtmFecFinalActa) < new Date() ? "rgb(0, 180,0)" : "rgb(180,0,0)"}">${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}</span>
                        </h6>
                        <h6 class="text-right">
                            <i class="fa fa-calendar mr-3"></i>${moment(plan.DtmFecFinalActa).format("DD/MM/YYYY")}
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

$(document).on("click", ".btn-cerrar", function () {
    let idPlanAccion = parseInt($(this).attr("data-id"));
    $("#btn-cerrar-plan").attr("data-id", idPlanAccion);
    $("#modal-alert").modal();
    
});

$("#btn-cerrar-plan").click(function (e) {
    let idPlanAccion = parseInt($(this).attr("data-id"));
    e.preventDefault();
    ejecutarajax(
        "MisPlanes.aspx/CerrarPlan",
        { idPlanAccion, idPlanAccion },
        function () {
            for (var i = 0; i < columnPlan.length; i++) {
                columnPlan[i].innerHTML = ""
            }
            GetPlanesAccion();
        }
    )
    $("#modal-alert").modal("hide");
    exito("Enviado a revisión", "El plan de Acción seleccionado ha sido enviadi a revision");
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
    txtUsuCreador.text(plan.StrNombreUsuarioCreador);
    $("#modal-view-plan").modal();
});
function GetPlanesAccion() {
    ejecutarajax("MisPlanes.aspx/GetPlanesAccionByUsu",{},CargarPlanes)
}

$(document).on("click", ".btn-avance", function (e) {
    e.preventDefault();
    let idPlanAccion = parseInt($(this).attr("data-id"))
    window.location.href = "AvancePlanAccion.aspx?idPlanAccion="+idPlanAccion
});

$(document).ready(function () {
    GetPlanesAccion();
});

$(document).on("keypress","#txtSearchAsignado",e => {
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