let ddlTipo = $("#ContentPlaceHolder_ddlTipo");
let txtNombre = $("#ContentPlaceHolder_txtNombre");
let txtJustificacion = $("#ContentPlaceHolder_txtJustificacion");
let ddlDominio = $("#ContentPlaceHolder_ddlDominio");
let txtCodSOGC = $("#ContentPlaceHolder_txtCodSOGC");
let txtDescNum = $("#ContentPlaceHolder_txtDescNum");
let txtOrInfoNum = $("#ContentPlaceHolder_txtOrInfoNum");
let txtFuenNum = $("#ContentPlaceHolder_txtFuenNum");
let txtDescDen = $("#ContentPlaceHolder_txtDescDen");
let txtOrInfoDen = $("#ContentPlaceHolder_txtOrInfoDen");
let txtFuenDen = $("#ContentPlaceHolder_txtFuenDen");
let txtUnidad = $("#ContentPlaceHolder_txtUnidad");
let txtFactor = $("#ContentPlaceHolder_txtFactor");
let ddlPeriodicidad = $("#ContentPlaceHolder_ddlPeriodicidad");
let ddlResponsable = $("#ContentPlaceHolder_ddlResponsable");
let txtFormula = $("#ContentPlaceHolder_txtFormula");
let txtEstandar = $("#ContentPlaceHolder_txtEstandar");
let ddlTendencia = $("#ContentPlaceHolder_ddlTendencia");
let txtTipGrafica = $("#ContentPlaceHolder_txtTipGrafica");
let txtInterpretacion = $("#ContentPlaceHolder_txtInterpretacion");
let ddlResponsableMed = $("#ContentPlaceHolder_ddlResponsableMed");
let ddlResponsableAna = $("#ContentPlaceHolder_ddlResponsableAna");
let ddlActores = $("#ContentPlaceHolder_ddlActores");
let txtVigencia = $("#ContentPlaceHolder_txtVigencia");
let ddlProcesos = $("#ContentPlaceHolder_ddlProcesos");
let txtVersion = $("#txtVersion");

let indicador;




$(document).on("change", "#ContentPlaceHolder_ddlActores", (e) => {
    let resp = "<div data-idResp=" + $("#ContentPlaceHolder_ddlActores").val() + " class=\"box-resp\"><div class=\"btnCloseRespActiv\"><i class=\"fa fa-close\"></i></div><div>" + $("#ContentPlaceHolder_ddlActores option:selected").text() + "</div></div>";
    $("#lstActores").html($("#lstActores").html() + resp);
});

$(document).on("click","#ContentPlaceHolder_ddlActores i", e => {
    let domPadre = e.target.parentElement.parentElement;
    domPadre.removeChild(e.target.parentElement);
});


const ActulizarIndicador = () => {
    let Indicador = {
        'IntOIdGDDocIndicador' : 0,
        'IntOidGDDocumento' : 0,
        'IntOidProceso': parseInt($("#ContentPlaceHolder_ddlProcesos").val()),
        'IntOidRevisor': 0,
        'IntOidAprovador' : 0,
        'StrNomDoc': "",
        'StrJustificacion' : txtJustificacion.val(),
        'StrCodSOGC' : txtCodSOGC.val(),
        'StrDescNum' : txtDescNum.val(),
        'StrOrInfoNum' : txtOrInfoNum.val(),
        'StrFuentPrimNum' : txtFuenNum.val(),
        'StrDescDen' : txtDescDen.val(),
        'StrOrInfoDen' : txtOrInfoDen.val(),
        'StrFuentPrimDen': txtFuenDen.val(),
        'StrUniMedicion': txtUnidad.val(),
        'StrFactor' : txtFactor.val(),
        'StrPeriodicidad' : ddlPeriodicidad.val(),
        'StrResponsable' : ddlResponsable.val(),
        'StrFormulaCalc': txtFormula.val(),
        'StrEstandar': txtEstandar.val(),
        'StrTendencia': ddlTendencia.val(),
        'StrTipGrafica': txtTipGrafica.val(),
        'StrInterpretacion': txtInterpretacion.val(),
        'StrResponsableMed': ddlResponsableMed.val(),
        'StrResponsableAna' : ddlResponsableAna.val(),
        'StrActores': Obtenercargos("lstActores"),
        'StrVigilancia': txtVigencia.val(),
        'StrTipo': ddlTipo.val(),
        'StrNomRevisor' : "",
        'StrTasa': "",
        'StrNomAprovador': "",
        'DtmFechaRevision': new Date("01/01/1800"),
        'DtmFechaAprovacion': new Date("01/01/1800"),
        'DtmFecha': new Date("01/01/1800"),
        'StrDominio': ddlDominio.val(),
        'IntOidGDProceso': parseInt(ddlProcesos.val()),
        'StrNomDoc': txtNombre.val()
    };

    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));

    ejecutarajax(
        "CrearIndicador.aspx/UpdateIndicador",
        { 'indicador': Indicador, 'idSolicitud': OIdSolicitud, 'version': parseInt(txtVersion.val()) || 0 },
        function () {
            exito("Indicador Guardado", "Los datos del Indicador se han guardado satisfactoriamente")
        }
    );
}

const ValidarFormularioVacio = () => {
    return txtJustificacion.val() == "" ||
        txtCodSOGC.val() == "" ||
        txtDescNum.val() == "" ||
        txtOrInfoNum.val() == "" ||
        txtFuenNum.val() == "" ||
        txtDescDen.val() == "" ||
        txtOrInfoDen.val() == "" ||
        txtFuenDen.val() == "" ||
        txtUnidad.val() == "" ||
        txtFactor.val() == "" ||
        ddlPeriodicidad.val() == "-1" ||
        ddlResponsable.val() == "-1" ||
        txtFormula.val() == "" ||
        txtEstandar.val() == "" ||
        ddlTendencia.val() == "-1" ||
        txtTipGrafica.val() == "" ||
        txtInterpretacion.val() == "" ||
        ddlResponsableMed.val() == "-1" ||
        ddlResponsableAna.val() == "-1" ||
        Obtenercargos("lstActores") == "" ||
        txtVigencia.val() == "" ||
        ddlTipo.val() == "-1" ||
        txtNombre.val() == "";
}



const EnviarRevision = (e) => {
    e.preventDefault();
    ActulizarIndicador();
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax("CrearIndicador.aspx/EnviarRevision", { 'idSolicitud': OIdSolicitud }, function () { window.location.href = `ValidacionDibulgacion.aspx?IdDocumento=${indicador.IntOidGDDocumento}`})
}

$("#ContentPlaceHolder_btnGuardarInd").on("click", EnviarRevision);

$("#ContentPlaceHolder_btnGuardar").on("click", e => { e.preventDefault(); ActulizarIndicador() })

$("#ContentPlaceHolder_btnEnviarRevision").on("click", function (e) {
    e.preventDefault();
    if (ValidarFormularioVacio()) {
        error("Datos Incompletos", "Diligencie todos los campos antes de solicitar la revisión")
        return;
    }
    $("#event-modal").modal()
});
const Obtenercargos = (nomNodo) => {
    var cargos = "";

    divCargos = [...document.querySelectorAll("#" + nomNodo + " .box-resp")];
    for (var i in divCargos) {
        if (i == divCargos.length - 1)
            cargos += divCargos[i].querySelector("div:nth-child(2)").innerText;

        else
            cargos += divCargos[i].querySelector("div:nth-child(2)").innerText + ", "
    }
    return cargos;
}

const CargarIndicador = (smg) => {
    indicador = JSON.parse(smg.d)[0];
    console.log(JSON.parse(smg.d));
    let proceso = JSON.parse(smg.d)[1];

    console.log(proceso)

    txtJustificacion.val(indicador.StrJustificacion);
    txtCodSOGC.val(indicador.StrCodSOGC);
    txtDescNum.val(indicador.StrDescNum);
    txtOrInfoNum.val(indicador.StrOrInfoNum);
    txtFuenNum.val(indicador.StrFuentPrimNum);
    txtDescDen.val(indicador.StrDescDen);
    txtOrInfoDen.val(indicador.StrOrInfoDen);
    txtFuenDen.val(indicador.StrFuentPrimDen);
    txtUnidad.val(indicador.StrUniMedicion);
    txtFactor.val(indicador.StrFactor);
    ddlPeriodicidad.val(indicador.StrPeriodicidad);
    ddlResponsable.val(indicador.StrResponsable);
    txtFormula.val(indicador.StrFormulaCalc);
    txtEstandar.val(indicador.StrEstandar);
    ddlTendencia.val(indicador.StrTendencia);
    txtTipGrafica.val(indicador.StrTipGrafica);
    txtInterpretacion.val(indicador.StrInterpretacion);
    ddlResponsableMed.val(indicador.StrResponsableMed);
    ddlResponsableAna.val(indicador.StrResponsableAna);
    ddlProcesos.val(indicador.IntOidGNProceso);
    txtVigencia.val(indicador.StrVigilancia);
    ddlTipo.val(indicador.StrTipo);
    txtNombre.val(indicador.StrNomDoc);
    ddlProcesos.val(proceso.IntOIdProceso);
    

    let actores = indicador.StrActores.split(",");
    console.log(indicador.StrActores);
    console.log(actores);

    actores.forEach((actor) => {
        if (actor) {
            let resp = "<div  class=\"box-resp\"><div class=\"btnCloseRespActiv\"><i class=\"fa fa-close\"></i></div><div>" + actor + "</div></div>";
            $("#lstActores").html($("#lstActores").html() + resp);
        }
    });
}

const GetIndicador = () => {
    Params = new URLSearchParams(window.location.search);
    OIdSolicitud = parseInt(Params.get('OIdSolicitud'));
    ejecutarajax("CrearIndicador.aspx/CargarIndicador", {'idSolicitud':OIdSolicitud}, CargarIndicador)
}

$(document).on("click", ".btnCloseRespActiv", (e) => {

    let domPadre = e.target.parentElement.parentElement;
    let contenedor = domPadre.parentElement;

    contenedor.removeChild(domPadre);
})

GetIndicador();

$(txtEstandar).on("keypress", function(e){
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /^([0-9])*$/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
});

$(txtVersion).on("keypress", function (e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /^([0-9])*$/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
});

$("#btnViewIndicador").click(function () {
    let VHeight = window.innerHeight;
    let VWidth = window.innerWidth;
    window.open(`Indicador/${manual.IntOidGDManual}`, "", `width = 1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`)
})
