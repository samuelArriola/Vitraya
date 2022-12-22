


$(document).on("click", ".btnBuscarDesprendible", function (e) {

   
    let fechaInicial1 = $("#txtFecha1").val();
    let fechaFinal = $("#txtFecha2").val();
   
    if (isEmpy(fechaInicial1) || isEmpy(fechaFinal) ) {
        error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
    } else {
        console.log('este metodo exitoso');
        let fechaInicial = fechaInicial1 + "-01";
        let fechaFinalF;
        let mes = parseInt(moment(fechaFinal).format("M")) + 1;
        if (mes == 13) {
            mes = "01";
            fechaFinalF = (parseInt(moment(fechaFinal).format("YYYY")) + 1) + "-" + mes + "-01";
        } else {
            fechaFinalF = moment(fechaFinal).format("YYYY") + "-" + mes + "-01";
        }
    
    
        datos = {
            "fechaI": fechaInicial,
            "fechaF": fechaFinalF
        }

        ejecutarajax("Desprendible.aspx/ObtenerListaDesprendibles", datos, CargarListaDesprendibles);
   
    }
})

function CargarListaDesprendibles(msg) {

    datos = msg.d;

    if (datos.length > 0) {

        dtDesprendibles = "";
        var mes;
        var anio;

        datos.forEach((item, i) => {

            moment.locale('es');
            dtDesprendibles += `

                <li class="list-group-item d-flex justify-content-between align-items-center" >
                    Desprendible ${moment(item.DtFechaNomina).format("MMMM YYYY")}
                    <span class="text-right">
                    <button type="button" data-accion="V" name="BtnVisualizar${i}" class="btnGPDF btn btn-primary btn-lg" data-id="${moment(item.DtFechaNomina).format('YYYY-MM-DD HH:mm:ss')}" ><i data-accion="V" class="fa fa-eye d-sm-none" data-id="${moment(item.DtFechaNomina).format('YYYY-MM-DD HH:mm:ss')}"></i><p data-accion="V" data-id="${moment(item.DtFechaNomina).format('YYYY-MM-DD HH:mm:ss')}" class="m-0 d-none d-sm-inline-block">Visualizar</p></button>
                    <button type="button" data-accion="D" name="BtnDescargar${i}" class="btnGPDF btn btn-secondary btn-lg" data-id="${moment(item.DtFechaNomina).format('YYYY-MM-DD HH:mm:ss')}" ><i data-accion="D" class="fa fa-download d-sm-none" data-id="${moment(item.DtFechaNomina).format('YYYY-MM-DD HH:mm:ss')}"></i><p data-accion="D" data-id="${moment(item.DtFechaNomina).format('YYYY-MM-DD HH:mm:ss')}" class="m-0 d-none d-sm-inline-block">Descargar</p></button>
                    </span>
                </li>
            
            `;

        })
        $("#ListaDesprendibles").html(dtDesprendibles);
        exito("Notificacion", "Busqueda completada");

    } else {
        error("Error","No se encontraron desprendibles disponibles en este rango de fechas")
    }
}

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

let accion;

$(document).on("click", ".btnGPDF", function (e) {

    var fechaDesprendible = $(e.target).attr("data-id");
    console.log(e.target);
    accion = $(e.target).attr("data-accion");

    datos = {
        "fecha": fechaDesprendible,
    }

    ejecutarajax("Desprendible.aspx/ObtenerInfoDesprendible", datos, descargarPDF);

})

function descargarPDF(msg) {

    informacion = msg.d;

    var nombreEmpleado;
    var identificacion;
    var fechaNomina;
    var cargo;
    var grado;
    var sueldo;
    var netoAPagar;
    var fechaActual = new Date();
    var codigoConcepto = [];
    var nombreConcepto = [];
    var cantidad = [];
    var devengado = [];
    var sumaDevengados = 0;
    var deduccion = [];
    var sumaDeducciones = 0;

    informacion.forEach((desprendible) => {

        fechaNomina = desprendible.DtFechaNomina;
        nombreEmpleado = desprendible.StrEmpleado;
        identificacion = desprendible.StrIdentificacion;
        cargo = desprendible.StrCargo;
        grado = desprendible.StrGrado;
        sueldo = formatNumber(desprendible.FloatSueldo);

        if (desprendible.StrCodigoConcepto != "C507" && desprendible.StrCodigoConcepto != "C508") {

            codigoConcepto.push(desprendible.StrCodigoConcepto);
            nombreConcepto.push(desprendible.StrNombreConcepto);
            cantidad.push(desprendible.FloatCantidad);
            devengado.push(desprendible.FloatDevengado);
            sumaDevengados = sumaDevengados + desprendible.FloatDevengado;
            deduccion.push(desprendible.FloatDeduccion);
            sumaDeducciones = sumaDeducciones + desprendible.FloatDeduccion;

        }
        
    })

    netoAPagar = sumaDevengados - sumaDeducciones;

    window.jsPDF = window.jspdf.jsPDF;

    var doc = new jsPDF();

    var logo = '../../Images/logocrecer.png';

    doc.addImage(logo, 'PNG', 20, 20, 60, 25);

    doc.setFontSize(8);

    doc.text(140, 15, 'Fecha Actual: ' + moment(fechaActual).format("LLLL"), 'left' );

    doc.setFontSize(10);

    doc.text(110, 30, 'DESPRENDIBLE DE NÓMINA');
    doc.text(100, 35, 'NÓMINA DEL 01/' + moment(fechaNomina).format("MM/YYYY") + ' AL ' + moment(fechaNomina).format("DD/MM/YYYY"));

    doc.line(20, 50, 193, 50);

    doc.setFontSize(8);

    doc.text(20, 59, 'EMPLEADO: ' + nombreEmpleado);
    doc.text(120, 59, 'CÉDULA: ' + identificacion);
    doc.text(20, 64, 'CARGO: ' + cargo);
    doc.text(120, 64, 'SUELDO BASICO: $ ' + sueldo);

    doc.line(20, 70, 193, 70);

    doc.text(20, 75, 'NÓMINA: ' + moment(fechaNomina).format("DD/MM/YYYY"));
    doc.text(20, 85, 'CONCEPTO');
    doc.text(110, 85, 'DEVENGADOS');
    doc.text(140, 85, 'DEDUCCIONES');
    doc.text(170, 85, 'NETO A PAGAR');

    doc.line(20, 90, 193, 90);

    var espacio = 90;

    for (var i = 0; i < codigoConcepto.length; i++) {

        espacio = espacio + 5;

        doc.text(20, espacio, codigoConcepto[i]);
        doc.text(33, espacio, nombreConcepto[i]);
        doc.text(103, espacio, cantidad[i].toString());
        doc.text(110, espacio, '$ ' + formatNumber(devengado[i]).toString());
        doc.text(140, espacio, '$ ' + formatNumber(deduccion[i]).toString());
    }

    espacio = espacio + 5;
    doc.line(20, espacio, 193, espacio);

    espacio = espacio + 5
    doc.text(25, espacio, 'TOTAL NÓMINA ' + moment(fechaNomina).format("DD/MM/YYYY") + ':');
    doc.text(110, espacio, '$ ' + formatNumber(sumaDevengados).toString());
    doc.text(140, espacio, '$ ' + formatNumber(sumaDeducciones).toString());
    doc.text(170, espacio, '$ ' + formatNumber(netoAPagar).toString());
    doc.line(105, espacio + 1.7, 193, espacio + 2);

    espacio = espacio + 5
    doc.text(25, espacio, 'TOTAL EMPLEADO ' + nombreEmpleado + ':');
    doc.text(110, espacio, '$ ' + formatNumber(sumaDevengados).toString());
    doc.text(140, espacio, '$ ' + formatNumber(sumaDeducciones).toString());
    doc.text(170, espacio, '$ ' + formatNumber(netoAPagar).toString());
    doc.line(105, espacio + 1.7, 193, espacio + 2);

    if (accion == "V") {

        exito("Notificacion", "Visualizando desprendible");
        window.open(doc.output('bloburl', 'Desprendible'), '', `width=1400, height=${window.innerHeight}, left=${(window.innerWidth / 2) - 700}, top=10, toolbar=no`);
        
    } else if (accion == "D") {

        doc.save('Desprendible/' + moment(fechaNomina).format("MMMM/YYYY") + '.pdf');
        exito("Notificacion", "Descargando desprendible");
    }

}

function validarEstadoOpc() {

    ejecutarajax("Desprendible.aspx/ObtenerValidacionOpcion", {}, mostrarEstado);

}

function mostrarEstado(msg) {

    EtiquetaTexto = $("#textoVisibilidad");

    validaciones = msg.d;

    validaciones.forEach((item) => {

        if (item.Oid_Config == 1 && item.EstadoValorConfig == "1") {

            EtiquetaTexto.html("OPCION: HABILITADA");

        } else if (item.Oid_Config == 1 && item.EstadoValorConfig == "0") {

            EtiquetaTexto.html("OPCION: INHABILITADA");
        }

    })

}

var mensaje = "";

$(document).on("click", ".btnActivar", function (e) {

    mensaje = "ACTIVADO";
    ejecutarajax("Desprendible.aspx/ActivarOpcion", {}, Mensaje);

})

$(document).on("click", ".btnInactivar", function (e) {

    mensaje = "INACTIVADO";
    ejecutarajax("Desprendible.aspx/DesactivarOpcion", {}, Mensaje);

})

function Mensaje() {
    validarEstadoOpc();
    exito("Notificacion", "Se ha " + mensaje + " la opcion DESPRENDIBLES satisfactoriamente");
}

function PermisosUsuario() {

    let linkOpcion = "../Servicios/Desprendible.aspx";

    datos = {
        "linkOpcion": linkOpcion
    }

    ejecutarajax("Desprendible.aspx/GetPermisos", datos, ValidarPermisos);
}

function ValidarPermisos(msg) {

    let datos = msg.d;

    if (datos.BlnConfirmar == false || datos.BlnCrear == false || datos.BlnEliminar == false || datos.BlnModificar == false) {

        $(".divcontrolOpcion").hide();

    }
    if (datos.BlnConfirmar == true && datos.BlnCrear == true && datos.BlnEliminar == true && datos.BlnModificar == true) {

        validarEstadoOpc();

    }

}

function isEmpy(string) {
    if (string == "" || string == null) {
        return true
    }
}

$(document).ready(function () {

    PermisosUsuario();

});



