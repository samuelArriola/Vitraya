

let accion;
let Imagenfirma;
let nombreG;
let cargoG

$(document).on("click", ".btnGenerarCertificado", function (e) {

    var boton = $(e.target).attr("id");
    
    if (boton == "D") {
        accion = "D";
    } else if (boton == "V") {
        accion = "V";
    }

    ejecutarajax("CertificadoLaboral.aspx/ObtenerInformacion", {}, PDFCertificadoLaboral);

})

function FuncionLog() {

    console.log("Certificados Laborales");

};

function formatNumber(num) {
    if (!num || num == 'NaN') return '0,00';
    if (num == 'Infinity') return '&#x221e;';
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + '.' + num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + ',' + cents);
}

//PDF EMPLEADO
function PDFCertificadoLaboral(msg) {

    datos = msg.d;

    var nombre;
    var identificacion;
    var cargo;
    var salario;
    var fechaVinculacion;
    var tipoContrato;

    var fechaActual = new Date();

    var logo = '../../Images/logocrecer.png';

    datos.forEach((item) => {

        moment.locale('es');

        nombre = item.StrNombre;
        identificacion = item.StrIdentificacion;
        cargo = item.StrCargo;
        tipoContrato = item.StrTipoContrato;
        salario = item.FloatSalario;
        fechaVinculacion = moment(item.DtFechaVinculacion).format("LL");

    })

    window.jsPDF = window.jspdf.jsPDF;

    var pdf = new jsPDF('p', 'in', 'letter')

    , sizes = 11
    , fonts = 'normal'
    , font, size, lines
    , margin = 3 
    , verticalOffset = margin
    , loremipsum = 'Que: ' + nombre + ' identificado con la cedula de ciudadanía número ' + identificacion + ' Labora actualmente en Centro Médico Crecer LTDA.'

    font = fonts
    size = sizes

    lines = pdf.setFont(font).setFontSize(size).splitTextToSize(loremipsum, 7.5)

    pdf.addImage(logo, 'PNG', 1, 0.5, 2.2, 1);

    pdf.setFontSize(sizes);

    pdf.text(1, 1.7, 'CENTRO MEDICO CRECER LTDA');
    pdf.text(1.6, 1.9, 'NIT. 806.004.548-6');

    pdf.text(2.1, 2.1, 'La Coordinación de Talento Humano del Centro Médico Crecer LTDA');
    pdf.text(4, 2.5, 'CERTIFICA');

    pdf.text(1, verticalOffset + size / 72, loremipsum, { align: 'justify', maxWidth: 6.5 })

    pdf.text(1, 3.8, 'Cargo: ' + cargo);
    pdf.text(1, 4, 'Salario básico: $ ' + formatNumber(salario));
    pdf.text(1, 4.2, 'Fecha de vinculación: ' + fechaVinculacion);
    pdf.text(1, 4.4, 'Tipo de Contrato: ' + tipoContrato);

    pdf.text(1, 5, 'Para constancia se firma este certificado el día ' + moment(fechaActual).format("LL"));

    pdf.text(1, 5.8, 'Atentamente');

    pdf.addImage(Imagenfirma, 'JPEG', 1, 5.9, 2, 0.5);
    pdf.setLineWidth(0.01);
    pdf.line(1, 6.6, 3.3, 6.6);
    pdf.text(1, 6.8, nombreG);
    pdf.text(1, 7, cargoG);

    pdf.setFontSize(9);
    pdf.text(1.2, 10, 'CENTRO MEDICO CRECER LTDA.AVENIDA PEDRO HEREDIA, SECTOR PRADO Nº 34 - 22 TEL 6421080 ext.907');
    pdf.text(3.2, 10.3, 'CARTAGENA DE INDIAS - COLOMBIA');

    if (accion == "V") {
        window.open(pdf.output('bloburl', 'CERTIFICADOLABORAL'), '', `width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no, directories=no, status=no, menubar=no`);

        var tipoAccion = "VISUALIZACION";

        datos = {
            "accion": tipoAccion,
        }

        ejecutarajax("CertificadoLaboral.aspx/setHistorico", datos, FuncionLog);

    } else if ( accion == "D") {
        pdf.save('CertificadoLaboralCRECER.pdf');
        exito("Notificacion", "Descargando Certificado Laboral");

        var tipoAccion = "DESCARGA";

        datos = {
            "accion": tipoAccion,
        }

        ejecutarajax("CertificadoLaboral.aspx/setHistorico", datos, FuncionLog);
    }
    
}

$(document).on("click", ".btnGenerarCertificadoA", function (e) {
    
    let identificacion = $("#id").val();

    if (identificacion == "") {

        error("Error","Debe ingresar un numero de identificacion");

    } else {

        datos = {
            "identificacion": identificacion,
        }

        ejecutarajax("CertificadoLaboral.aspx/ObtenerInformacionEmpleado", datos, PDFCertificadoLaboral2);
    }

})


//PDF ADMINISTRADOR
function PDFCertificadoLaboral2(msg) {

    datos = msg.d;

    if (datos.length > 0) {

        var estado;
        var nombre;
        var identificacion;
        var cargo;
        var salario;
        var fechaVinculacion;
        var tipoContrato;
        var fechaRetiro;
        var tiempo;

        var fechaActual = new Date();

        var logo = '../../Images/logocrecer.png';

        datos.forEach((item) => {

            moment.locale('es');

            estado = item.StrEstado;
            nombre = item.StrNombre;
            identificacion = item.StrIdentificacion;
            cargo = item.StrCargo;
            tipoContrato = item.StrTipoContrato;
            salario = item.FloatSalario;
            fechaVinculacion = moment(item.DtFechaVinculacion).format("LL");
            fechaRetiro = item.DtFechaRetiro;

        })

        if (estado == "ACTIVO") {
            tiempo = "Labora actualmente";
        } else if (estado != "ACTIVO") {
            tiempo = "Laboró";
        }

        window.jsPDF = window.jspdf.jsPDF;

        var pdf = new jsPDF('p', 'in', 'letter')

            , sizes = 11
            , fonts = 'normal'
            , font, size, lines
            , margin = 3
            , verticalOffset = margin
            , loremipsum = 'Que: ' + nombre + ' identificado con la cedula de ciudadanía número ' + identificacion + ' ' + tiempo + ' en Centro Médico Crecer LTDA.'

        font = fonts
        size = sizes

        lines = pdf.setFont(font).setFontSize(size).splitTextToSize(loremipsum, 7.5)

        pdf.addImage(logo, 'PNG', 1, 0.5, 2.2, 1);

        pdf.setFontSize(sizes);

        pdf.text(1, 1.7, 'CENTRO MEDICO CRECER LTDA');
        pdf.text(1.6, 1.9, 'NIT. 806.004.548-6');

        pdf.text(2.1, 2.1, 'La Coordinación de Talento Humano del Centro Médico Crecer LTDA');
        pdf.text(4, 2.5, 'CERTIFICA');

        pdf.text(1, verticalOffset + size / 72, loremipsum, { align: 'justify', maxWidth: 6.5 })

        pdf.text(1, 3.8, 'Cargo: ' + cargo);
        pdf.text(1, 4, 'Salario básico: $ ' + formatNumber(salario));

        if (estado == "ACTIVO") {
            pdf.text(1, 4.2, 'Fecha de vinculación: ' + fechaVinculacion);
        } else if (estado != "ACTIVO") {
            pdf.text(1, 4.2, 'Fecha de vinculación: ' + fechaVinculacion + ' hasta ' + moment(fechaRetiro).format("LL"));
        }

        pdf.text(1, 4.4, 'Tipo de Contrato: ' + tipoContrato);

        pdf.text(1, 5, 'Para constancia se firma este certificado el día ' + moment(fechaActual).format("LL"));

        pdf.text(1, 5.8, 'Atentamente');

        pdf.addImage(Imagenfirma, 'JPEG', 1, 5.9, 2, 0.5);
        pdf.setLineWidth(0.01);
        pdf.line(1, 6.5, 3.3, 6.5);
        pdf.text(1, 6.7, nombreG);
        pdf.text(1, 6.9, cargoG);

        pdf.setFontSize(9);
        pdf.text(1.2, 10, 'CENTRO MEDICO CRECER LTDA.AVENIDA PEDRO HEREDIA, SECTOR PRADO Nº 34 - 22 TEL 6421080 ext.907');
        pdf.text(3.2, 10.3, 'CARTAGENA DE INDIAS - COLOMBIA');

        window.open(pdf.output('bloburl', 'CERTIFICADOLABORAL'), '', `width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no, directories=no, status=no, menubar=no`);


    } else {

        error("Error", "No se encontro un usuario con este numero de identificacion")
    }
}

function traerCoincidencias() {

    ejecutarajax("CertificadoLaboral.aspx/getCoincidencias", {}, llenarCoincidencias);
}

function llenarCoincidencias(msg) {

    datos = msg.d;

    opcCoincidencias = "";

    datos.forEach((item) => {
        opcCoincidencias += `

            <option value="${item.StrIdentificacion}">${item.StrNombre}</option>

        `;
    })
    $("#listaCoincidencias").html(opcCoincidencias);
    $("#listaCoincidenciasF").html(opcCoincidencias);

}

function traerDatosHistorico() {

    ejecutarajax("CertificadoLaboral.aspx/getHistorico", {}, llenarTablaH);
}

function llenarTablaH(msg) {

    datos = msg.d;

    dtHistorico = "";

    datos.forEach((item) => {
        moment.locale('es');
        dtHistorico += `

            <tr>
                
                <td>${item.StrIdentificacion}</td>
                <td>${item.StrNombre}</td>
                <td>${item.StrAccionHistorico}</td>
                <td>${moment(item.DtFechaHistorico).format("LLL")}</td>

            </tr>

        `;
    })
    $("#tbHistorico").html(dtHistorico);
    DataTable("#tableDocs");

}

let TipoInformacion;

$(document).on("click", ".BuscarHistorico", function (e) {

    let fechaFinalF;
    let fechaInicial = $("#txtFecha1").val() + "-01";
    let fechaFinal = $("#txtFecha2").val();
    let mes = parseInt(moment(fechaFinal).format("M")) + 1;
    if (mes == 13) {
        mes = "01";
        fechaFinalF = (parseInt(moment(fechaFinal).format("YYYY")) + 1) + "-01-01";
    } else {
        fechaFinalF = moment(fechaFinal).format("YYYY") + "-" + mes + "-01";
    }
    
    TipoInformacion = $("#TipoI").val();

    datos = {
        "fecha1": fechaInicial,
        "fecha2": fechaFinalF,
    }

    if (TipoInformacion == "0" || $("#txtFecha1").val() == "" || fechaFinal == "") {

        error("Error", "Debe seleccionar fecha inicial, fecha final y tipo de informacion.")

    }
    else if (TipoInformacion == "1" && $("#txtFecha1").val() != "" && fechaFinal != "") {

        ejecutarajax("CertificadoLaboral.aspx/filtro1", datos, buscarFiltro1);

    } else if (TipoInformacion == "2" && $("#txtFecha1").val() != "" && fechaFinal != "") {

        ejecutarajax("CertificadoLaboral.aspx/filtro2", datos, buscarFiltro2);
    } 
})

function buscarFiltro1(msg) {

    datos = msg.d;

    dtfiltro1 = "";

    datos.forEach((item) => {
        moment.locale('es');
        dtfiltro1 += `

            <tr>
                
                <td>${item.StrIdentificacion}</td>
                <td>${item.StrNombre}</td>
                <td>${item.StrAccionHistorico}</td>
                <td>${moment(item.DtFechaHistorico).format("LLL")}</td>

            </tr>

        `;
    })
    $("#tbHistorico").html(dtfiltro1);

    $("#table2").hide();
    $("#table2  + .navTable").hide();
    $("#tableDocs").show();
    DataTable("#tableDocs");
}

function buscarFiltro2(msg) {

    var nombreMeses = ['PosicionCero', 'Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];
    var mesEX;

    datos = msg.d;

    dtfiltro2 = "";

    datos.forEach((item) => {


        moment.locale('es');

        for (var i = 0; i < nombreMeses.length; i++) {

            if (item.StrMes == i) {

                mesEX = nombreMeses[i];

            }

        }

        dtfiltro2 += `

            <tr>
                
                <td>${item.StrAnio}</td>
                <td>${mesEX}</td>
                <td>${item.StrTotal}</td>

            </tr>

        `;
    })
    $("#tb2").html(dtfiltro2);

    $("#tableDocs").hide();
    $("#tableDocs + .navTable").hide();
    $("#table2").show();
    DataTable("#table2");
   
}

$(document).on("click", ".exportInfoDetalle", function (e) {

    let tabla = [];

    let header = [];

    let idTabla = "#tableDocs";

    if (TipoInformacion == "1" || TipoInformacion == "0") {
        idTabla = "#tableDocs";
    } else if (TipoInformacion == "2") {
        idTabla = "#table2";
    }


    document.querySelectorAll(idTabla + " th").forEach(head => {
        header.push({ v: head.innerText, t: "s" })
    })

    tabla.push(header);

    document.querySelectorAll(idTabla + " tr").forEach(row => {
        let fila = [];
        row.querySelectorAll("td").forEach(celda => {
            fila.push({v: celda.innerText, t: "s" })
        })
        tabla.push(fila);
    })

    tableExport = new TableExport(document.createElement("table"), {});

    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Historico Certificados Laborales", ".xlsx", [], false, "hoja 1")

    exito("Notificacion", "Exportando Informacion");

    traerCoincidencias();

    $("#TipoI").val("0");
    TipoInformacion = "0";

    $("#table2").hide();
    $("#tableDocs").show();
    
})

function traerDatosFirma(){
    ejecutarajax("CertificadoLaboral.aspx/getDatosFirma", {}, llenarCampoFirma);
}

function llenarCampoFirma(msg){

    let nombreF;
    let identificacionF;
    let firma;
    let cargo;

    datos = msg.d;

    datos.forEach((item) => {

        nombreF = item.StrNombre;
        identificacionF = item.StrIdentificacion;
        cargo = item.StrCargo;
        firma = item.Firmabase64;
        
    })
    $("#inputFirma").val(nombreF);

    Imagenfirma = "data:image/jpeg;base64," + firma;
    nombreG = nombreF;
    cargoG = cargo;

}

$(document).on("click", ".btnCambioFirma", function (e) {

    let id = $("#idF").val();

    datos = {
        "identificacion": id,
    }

    ejecutarajax("CertificadoLaboral.aspx/getInfoUsuFirma", datos, actualizaUsuFirma);

})

function actualizaUsuFirma(msg) {

    datos1 = msg.d;

    if (datos1.length < 1) {

        error("Error","El usuario no se puede seleccionar como usuario firmante debido a que se encuentra retirado")

    } else {

        let nombre;
        let identififcacion;
        let cargo;
        let firmaAEnviar;
        let firmaAMostrar;

        datos1.forEach((item) => {

            nombre = item.StrNombre;
            identififcacion = item.StrIdentificacion;
            cargo = item.StrCargo;
            firmaAEnviar = item.BtFirma;
            firmaAMostrar = item.Firmabase64;

        })

        Imagenfirma = "data:image/jpeg;base64," + firmaAMostrar;
        nombreG = nombre;
        cargoG = cargo;
        $("#inputFirma").val(nombreG);
        $("#idF").val("");

        datos = {
            "identificacion": identififcacion,
            "nombre": nombre,
            "firma": firmaAEnviar,
            "cargo": cargo,
        }

        ejecutarajax("CertificadoLaboral.aspx/ActualizarFirma", datos, mensajeActualizarFirma);

    }
    
}

function mensajeActualizarFirma() {
    exito("Notificacion", "Usuario firmante actualizado con exito");
}

$(document).ready(function () {

    traerCoincidencias();
    traerDatosHistorico();
    traerDatosFirma();

    $("#table2").hide();

});


