let txtUsuResp = $("#txtUsuResp");
let txtUsuSeg = $("#txtUsuSeg");
let ddlProcesos = $("#ContentPlaceHolder_ddlProcesos");
let txtFecha1 = $("#txtFecha1");
let txtFecha2 = $("#txtFecha2");
let slcOrigen = $("#slcOrigen");
let slcEstado = $("#slcEstado");
let datos = [];

let indexPlanes;

function CargarPlanesAccion(msg) {
    datos = msg.d;

    let dataTable = "";


    for (let i = 0; i < datos.length; i++) {
        let estados = ["ASIGNADO", "PROCESO", "EVALUACION", "TERMINADO"];

        dataTable += `
            <tr>
                <td>${datos[i].StrOrigen}</td>
                <td>${datos[i].StrProceso}</td>
                <td>${datos[i].StrActividad}</td>
                <td>${datos[i].StrNombreUsuarioResponsable}</td>
                <td>${moment(datos[i].DtmFecFinalActa).format("DD/MM/YYYY")}</td>
                <td>${datos[i].StrNombreUsuarioSeguimiento}</td>
                <td>${estados[datos[i].IntEstAct - 1]}</td>
                <td><button type="button" class="btn btn-primary btnVerPlan" data-index=${i}><i class="fa fa-eye verPlan"></i></button></td>
            </tr>
        `;
    }
    $("#tbMatrisPlanesAccion").html(dataTable)
    DataTable("#tablePlanes");
}


function GetPlanesAccion() {

    datos = {
        nomUsuResp: txtUsuResp.val(),
        nomUsuSeg: txtUsuSeg.val(),
        contexto: slcOrigen.val(),
        fecha1: new Date(txtFecha1.val() == "" ? "01/01/1800" : txtFecha1.val()),
        fecha2: new Date(txtFecha2.val() == "" ? "01/01/3000" : txtFecha2.val()),
        proceso: ddlProcesos.val(),
        estado: slcEstado.val()
    }
    ejecutarajax("MatrisPlanesAccion.aspx/GetPlanesAccion", datos, CargarPlanesAccion)
}

$(document).on("click", ".btnVerPlan", function (e) {

    moment.locale("es");

    indexPlanes = $(this).attr("data-index");
    let plan = datos[indexPlanes];

    let estados = ["ASIGNADO", "PROCESO", "EVALUACION", "TERMINADO"];

    let dtPlan = `
        <thead>
            <tr>
                <th colspan="2">Información completa del plan de acción</th>
            <tr>
        <thead>
        <tbody>
            <tr>
                <th>Origen</th>
                <td>${plan.StrOrigen}</td>
            </tr>
            <tr>
                <th>Proceso</th>
                <td>${plan.StrProceso}</td>
            </tr>
            <tr>
                <th>¿Qué?</th>
                <td>${plan.StrActividad}</td>
            </tr>
            <tr>
                <th>¿Cómo?</th>
                <td>${plan.StrComo}</td>
            </tr>
            <tr>
                <th>¿Por qué?</th>
                <td>${plan.StrPorQue}</td>
            </tr>
            <tr>
                <th>¿Quién?</th>
                <td>${plan.StrNombreUsuarioResponsable}</td>
            </tr>
            <tr>
                <th>¿Cúando?</th>
                <td>${moment(plan.DtmFecFinalActa).format("LL")}</td>
            </tr>
            <tr>
                <th>¿Dónde?</th>
                <td>${plan.StrDonde}</td>
            </tr>
            <tr>
                <th>¿Cuánto Costará?</th>
                <td>${plan.StrCuanto}</td>
            </tr>
            <tr>
                <th>¿Cómo se soporta?</th>
                <td>${plan.StrSoporte}</td>
            </tr>
            <tr>
                <th>Responsable de seguimiento</th>
                <td>${plan.StrNombreUsuarioSeguimiento}</td>
            </tr>
            <tr>
                <th>Estado</th>
                <td>${estados[plan.IntEstAct -1]}</td>
            </tr>
        </tbody>
    `
    $("#tablePlansAcc").html(dtPlan);

    $("#modalVerPlan").modal();

});


$("#btnDownload").on("click",function(e){
    tabla = [];
    let estados = ["ASIGNADO", "PROCESO", "EVALUACION", "TERMINADO"];

    var header = [
        { v: "Origen", t: "s" },
        { v: "Proceso", t: "s" },
        { v: "¿Qué?", t: "s" },
        { v: "¿Cómo?", t: "s" },
        { v: "¿Por Qué?", t: "s" },
        { v: "¿Quién?", t: "s" },
        { v: "¿Cuándo?", t: "s" },
        { v: "¿Dónde?", t: "s" },
        { v: "¿Cuánto Costará?", t: "s" },
        { v: "¿Cómo se Soporta?", t: "s" },
        { v: "Responsable del Seguimiento", t: "s" },
        { v: "Estado", t: "s" }
    ];

    tabla.push(header);

    datos.forEach(item => {
        tabla.push([
            { v: item.StrOrigen, t: "s" },
            { v: item.StrProceso, t: "s" },
            { v: item.StrActividad, t: "s" },
            { v: item.StrComo, t: "s" },
            { v: item.StrPorQue, t: "s" },
            { v: item.StrNombreUsuarioResponsable, t: "s" },
            { v: moment(item.DtmFecFinalActa).format("DD/MM/YYYY"), t: "s" },
            { v: item.StrDonde, t: "s" },
            { v: item.StrCuanto, t: "s" },
            { v: item.StrSoporte, t: "s" },
            { v: item.StrNombreUsuarioSeguimiento, t: "s" },
            { v: estados[item.IntEstAct - 1], t: "s" }
        ]);
    });
    tableExport = new TableExport(document.createElement("table"), {});
    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Matriz de planes de acción", ".xlsx", [], false, "hoja 1")
});


$("#btnEditarPlan").click(function (e) {
    window.location.href = "EditarPlanAccion.aspx?idPlanAccion=" + datos[indexPlanes].IntOidPAPlanAccion
});

$("#btnEliminarPlan").click(function () {
    $("#modalVerPlan").modal("hide");
    $("#modalEliminar").modal();
});

$("#btnConfirmarEliminar").click(function (e) {
    ejecutarajax(
        "MatrisPlanesAccion.aspx/EliminarPlanAccion",
        { "idPlanAccion": datos[indexPlanes].IntOidPAPlanAccion },
        function (msg) {
            $("#modalEliminar").modal("hide");

            let result = msg.d;
            if (result)
                exito("Plan de acción eliminado", "El plan de acción ha sido eliminado correctamente");
            else
                error("Problemas para eliminar el plan de acción","ha ocurrido un error mientras se elminaba el registro")
        }
    );
    GetPlanesAccion();
});

ddlProcesos.on("change", function () { GetPlanesAccion()});
slcEstado.on("change", function () { GetPlanesAccion() });
txtFecha1.on("change", function () { GetPlanesAccion() });
txtFecha2.on("change", function () { GetPlanesAccion() });
txtUsuResp.on("keypress", function (e) {if (e.keyCode == 13) GetPlanesAccion() });
txtUsuSeg.on("keypress", function (e) {if (e.keyCode == 13) GetPlanesAccion() });
slcOrigen.on("keypress", function (e) {if (e.keyCode == 13) GetPlanesAccion() });
GetPlanesAccion();


