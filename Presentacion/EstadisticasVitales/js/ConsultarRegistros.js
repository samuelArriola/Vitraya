
// #columConRegFecha = columna consulta registro Fecha.
// #ParBusReg = parametro busqeuda registro. 
// #TipNacViv = tipo nacido vivo. (check para solicitar info de nacidos vivos)
// #TipDef = tipo defuncion. (check para solicitar info defunciones)
// #tipConRegPar = tipo consulta restro parametro. (option de filtro para busqueda)
// #btnBusReg = boton busqeuda registro. (buscar registro una vez digitados los parametros de filtro)
// #tbdRegNacViv = body de la tabka nacidos vivos.
// #tbdRegDef = body tabla defuncion.
// #fechaConReg1 = input datetime para fecha intervalo de fecha
// #fechaConReg2 = input datetime para fecha intervalo de fecha
// #columConRegFecha = columna que contiene a los input datetime.   

window.onload = function(){
    VPermisoAct(); //consultar permisos del usuario y almacenar datos en la variable "Permisos"
}

var Permisos = false;  // variable para almacenar los permisos que tiene el usuario, para validar opciones como generar excel y modificar registros.
var ConRegistros;
var position;  //posicion del registro en el vector conRegistros.

var parametroBus = $("#ParBusReg");


$(".modal").on("hidden.bs.modal", function () {
    let elemento = document.getElementById("ModalVerRegTex")
    elemento.innerHTML = '';
});


//moment(new Date("2020-11-30T00:00:00")).format("MM/DD/YYYY HH:MM")
var opcNacViv = document.getElementById("TipNacViv");
var opcDef = document.getElementById("TipDef");
var opcFec = document.getElementById("tipConRegPar");
opcNacViv.addEventListener('click', cargarOpcBu, false);
opcDef.addEventListener('click', cargarOpcBu, false);
opcFec.addEventListener('change', activarBus, false);
document.getElementById("btnBusReg").addEventListener('click', cargarTabla, false);

//evita que la pagina se actulice al darle enter el input
$("form").on("submit", function (e) { e.preventDefault() })
// funcion para cargar tabla de datos, segun la consulta al servidor.
function cargarTabla() {

    // condicional para verificar si el usuario solicita informacion de nacidos vivo.
    if (parametroBus.val() != "") {

        if (opcNacViv.checked && opcFec.value != "Fecha") {
            let RegNac = {
                'dato': parametroBus.val(),
                'opc': document.getElementById("tipConRegPar").value,
                'fechaMin': "",
                'fechaMax': ""
            }

            $.ajax({ //  Metodo para enviar peticion al servidor y solicitar datos de nacidos vivos. 
                url: "ConsultarRegistros.aspx/cargarRegNacViv", //metodo del lado del servidor envia los datos de PQRS
                data: JSON.stringify(RegNac),
                dataType: "json",
                type: "post",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    let datos = JSON.parse(msg.d);

                    let tbNacViv = "";
                    if (datos[0].length == 0)
                        alert("Resultados Busqeuda: 0 coicidencias");
                    
                    for (let i = 0; i < datos[0].length; i++) { // ciclo para cargar los campos de la tabla NacViv
                        
                        tbNacViv += "<tr>" +
                            "<td>" + i + "</td>" +
                            "<td>" + datos[0][i].DoubleIdMadre + "</td>" +
                            "<td>" + datos[0][i].StrNomMadre + "</td>" +
                            "<td>" + datos[0][i].StrTipNac + "</td>" +
                            "<td>" + moment(datos[0][i].DateFecNac).format("YYYY-MM-DD HH:mm") + "</td>" +
                            "<td>" + datos[1][i] + "</td>" +
                            "<td>" + datos[0][i].DoubleGNCodUsu + "</td>";
                        
                        if (Permisos[0])
                            tbNacViv += "<td><a href=\"ActualizarNacViv.aspx?OIdCRCodRuaf=" + datos[0][i].IntCRCodRuaf + "\">Actualizar</a></td>";

                            tbNacViv += "<td><a href=\"#\" onClick = \"cargarInfoModalVerRegNV("+i+"); return false\" >Ver</a></td>" +
                            "</tr>";
                    }
                    ConRegistros = datos;

                    $("#tbdRegNacViv").html(tbNacViv);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });

        }

        //condicional para cargar informacion sobre Defunciones. 
        if (opcDef.checked && opcFec.value != "Fecha") {
            let RegDef = {
                'dato': parametroBus.val(),
                'opc': document.getElementById("tipConRegPar").value,
                'fechaMin': "",
                'fechaMax': ""
            }

            $.ajax({
                url: "ConsultarRegistros.aspx/cargarRegDef", //metodo del lado del servidor que envia los datos de Defuncion solicitados.
                data: JSON.stringify(RegDef),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    datos = JSON.parse(msg.d);
                    let tbDef = "";
                    if (datos[0].length == 0)
                        alert("Resultados Busqeuda: 0 coicidencias");
                    for (let i = 0; i < datos[0].length; i++) { // cargar la tabla de defunción con los datos enviados por el servidor. 
                        tbDef += "<tr>" +
                            "<td>" + i + "</td>" +
                            "<td>" + datos[0][i].StrTipDef + "</td>" +
                            "<td>" + moment(datos[0][i].DateFecDef).format("YYYY-MM-DD HH:mm") + "</td>" +
                            "<td>" + datos[0][i].StrNomPac + "</td>" +
                            "<td>" + datos[0][i].DoubleIdPaciente + "</td>" +
                            "<td>" + datos[1][i] + "</td>" +
                            "<td>" + datos[0][i].DoubleGNCodUsu + "</td>";
                            if (Permisos[0])
                                tbDef += "<td><a href=\"ActualizarDef.aspx?OIdCRCodRuaf=" + datos[0][i].IntOIdCRCodRuaf + "\">Actualizar</a></td>";

                            tbDef += "<td><a href=\"#\" onClick = \"cargarInfoModalVerRegDEF(" + i + "); return false\" >Ver</a></td>" +
                            "</tr>";

                            
                    }
                    ConRegistros = datos;
                    $("#tbdRegDef").html(tbDef);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });

        }

        

    } else if (opcFec.value != "Fecha"){
        alert("Por favor seleccione tipo de registro y digite parametros");
    }

    //condicional para consultas por fecha.
    if (opcFec.value == "Fecha") {

        if (opcNacViv.checked) {
            //consultar registro nacidos vivos por fecha
            if (document.getElementById("fechaConReg1").value != "" && document.getElementById("fechaConReg2").value != "") {
                let RegNacF = {
                    dato: parametroBus.val(),
                    opc: document.getElementById("tipConRegPar").value,
                    fechaMin: new Date(document.getElementById("fechaConReg1").value),
                    fechaMax: new Date(document.getElementById("fechaConReg2").value)
                }

                $.ajax({
                    url: "ConsultarRegistros.aspx/cargarRegNacVivFec", //metodo del lado del servidor para solicitar datos.
                    data: JSON.stringify(RegNacF),
                    dataType: "json",
                    type: "post",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        datos = JSON.parse(msg.d);
                        let tbNacViv = "";
                        if (datos[0].length == 0)
                            alert("Resultados Busqeuda: 0 coicidencias");
                        for (let i = 0; i < datos[0].length; i++) { // cargar la tabla de nacidos vivos, con los datos solicitados en caso de que halla! 
                            tbNacViv += "<tr>" +
                                "<td>" + i + "</td>" +
                                "<td>" + datos[0][i].DoubleIdMadre + "</td>" +
                                "<td>" + datos[0][i].StrNomMadre + "</td>" +
                                "<td>" + datos[0][i].StrTipNac + "</td>" +
                                "<td>" + moment(datos[0][i].DateFecNac).format("YYYY-MM-DD HH:mm") + "</td>" +
                                "<td>" + datos[1][i] + "</td>" +
                                "<td>" + datos[0][i].DoubleGNCodUsu + "</td>";
                                if (Permisos[0])
                                tbNacViv += "<td><a href=\"ActualizarNacViv.aspx?OIdCRCodRuaf=" + datos[0][i].IntCRCodRuaf + "\">Actualizar</a></td>";

                                tbNacViv += "<td><a href=\"#\" onClick = \"cargarInfoModalVerRegNV(" + i + "); return false\" >Ver</a></td>" +
                                "</tr>";
                        }
                        ConRegistros = datos;
                        $("#tbdRegNacViv").html(tbNacViv);
                    },
                    error: function (result) {
                        alert("ERROR " + result.status + ' ' + result.statusText);
                    }
                });

            }
            else {
                error("error","por favor escoger fechas")
            }
        }

        // condicional para capturar solicitud de fecha para Defunciones 
        if (opcDef.checked) {
            if (document.getElementById("fechaConReg1").value != "" && document.getElementById("fechaConReg2").value != "") {
                let RegDefF = {
                    'dato': parametroBus.val(),
                    'opc': document.getElementById("tipConRegPar").value,
                    'fechaMin': document.getElementById("fechaConReg1").value,
                    'fechaMax': document.getElementById("fechaConReg2").value
                }

                $.ajax({
                    url: "ConsultarRegistros.aspx/cargarRegDefFec", //metodo del lado del servidor para solicitar los datos.
                    data: JSON.stringify(RegDefF),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (msg) {
                        datos = JSON.parse(msg.d);
                        let tbDef = "";
                        if (datos[0].length == 0)
                            alert("Resultados Busqeuda: 0 coicidencias");
                        for (let i = 0; i < datos[0].length; i++) {
                            tbDef += "<tr>" +
                                "<td>" + i + "</td>" +
                                "<td>" + datos[0][i].StrTipDef + "</td>" +
                                "<td>" + moment(datos[0][i].DateFecDef).format("YYYY-MM-DD HH:mm") + "</td>" +
                                "<td>" + datos[0][i].StrNomPac + "</td>" +
                                "<td>" + datos[0][i].DoubleIdPaciente + "</td>" +
                                "<td>" + datos[1][i] + "</td>" +
                                "<td>" + datos[0][i].DoubleGNCodUsu + "</td>";
                                if (Permisos[0])
                                    tbDef += "<td><a href=\"ActualizarDef.aspx?OIdCRCodRuaf=" + datos[0][i].IntOIdCRCodRuaf + "\">Actualizar</a></td>";

                                tbDef += "<td><a href=\"#\" onClick = \"cargarInfoModalVerRegDEF(" + i + "); return false\" >Ver</a></td>" +
                                "</tr>";
                        }
                        ConRegistros = datos;
                        $("#tbdRegDef").html(tbDef);
                    },
                    error: function (result) {
                        alert("ERROR " + result.status + ' ' + result.statusText);
                    }
                });
            } else {
                error("error", "por favor escoger fechas")
            }

        }
    }
    //else
    //    alert("Por favor seleccione tipo de registro y digite parametros");
}


//cargar al modal la info modal registro nacido vivo.
function cargarInfoModalVerRegNV(wi) {

    if (Permisos[1]) {
        position = wi;
        let elemento = document.getElementById("ModalVerRegTex")
        elemento.innerHTML = '' +
            '<strong>Documento de la Madre:&nbsp</strong> ' + ConRegistros[0][wi].DoubleIdMadre + '<br>' +
            '<strong>Nombre Madre:&nbsp</strong> ' + ConRegistros[0][wi].StrNomMadre + '<br>' +
            '<strong>Tipo nacimiento:&nbsp</strong> ' + ConRegistros[0][wi].StrTipNac + '<br>' +
            '<strong>Fecaha nacimiento:&nbsp</strong> ' + moment(ConRegistros[0][wi].DateFecNac).format("YYYY-MM-DD HH:mm") + '<br>' +
            '<strong>Código Ruaf:&nbsp</strong> ' + ConRegistros[1][wi] + '<br>' +
            '<strong>Edad gestacional:&nbsp</strong> ' + ConRegistros[0][wi].IntEdGesNac + '<br>' +
            '<strong>Documento Médico:&nbsp</strong> ' + ConRegistros[0][wi].DoubleGNCodUsu + '<br>' +
            '<strong>Nombre Médico:&nbsp</strong>' + ConRegistros[0][wi].StrNomDoc + '<br>' +
            '<strong>Peso NV:&nbsp</strong> ' + ConRegistros[0][wi].IntPesoRn + '<br>' +
            '<strong>Talla NV:&nbsp</strong> ' + ConRegistros[0][wi].FloatTallaRN + '<br>' +
            '<strong>Sexo NV:&nbsp</strong> ' + ConRegistros[0][wi].StrSexo + '<br><br>';

            //'<div class="row justify-content-center">' +  // boton para canturar el evento de cargar historia de un registro (historia de modificaciones)
            //'  <button type="button" class="btn btn-info btn-sm" id = "btnHistoryNV">Historial</button>' +
            //' </div>';
        
        $("#ModalVerReg").modal();
    }
    else{

        let elemento = document.getElementById("ModalVerRegTex")
        elemento.innerHTML = '' +
            '<strong>Documento de la Madre:&nbsp</strong> ' + ConRegistros[0][wi].DoubleIdMadre + '<br>' +
            '<strong>Nombre Madre:&nbsp</strong> ' + ConRegistros[0][wi].StrNomMadre + '<br>' +
            '<strong>Tipo nacimiento:&nbsp</strong> ' + ConRegistros[0][wi].StrTipNac + '<br>' +
            '<strong>Fecaha nacimiento:&nbsp</strong> ' + moment(ConRegistros[0][wi].DateFecNac).format("YYYY-MM-DD HH:mm") + '<br>' +
            '<strong>Código Ruaf:&nbsp</strong> ' + ConRegistros[1][wi] + '<br>' +
            '<strong>Edad gestacional:&nbsp</strong> ' + ConRegistros[0][wi].IntEdGesNac + '<br>' +
            '<strong>Documento Médico:&nbsp</strong> ' + ConRegistros[0][wi].DoubleGNCodUsu + '<br>' +
            '<strong>Nombre Médico:&nbsp</strong>' + ConRegistros[0][wi].StrNomDoc + '<br>' +
            '<strong>Peso NV:&nbsp</strong> ' + ConRegistros[0][wi].IntPesoRn + '<br>' +
            '<strong>Talla NV:&nbsp</strong> ' + ConRegistros[0][wi].FloatTallaRN + '<br>' +
            '<strong>Sexo NV:&nbsp</strong> ' + ConRegistros[0][wi].StrSexo + '<br>';

        $("#ModalVerReg").modal();
    }  
}

//cargar historia de un registro seleccionado
$(document).on("click", "#btnHistoryNV", () => {  //agregar evento cargar historia al regitro nacido vivo seleccionado.

    $.ajax({
        url: "ConsultarRegistros.aspx/getHistoryNV", //metodo del lado del servidor para solicitar los datos.
        data: JSON.stringify({ 'IdMadre': ConRegistros[0][position].DoubleIdMadre + "" }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            datos = JSON.parse(msg.d);
            if (!datos.length > 0)
                alert("cero resultados")

            let elemento = document.getElementById("ModalVerRegTex");

            for (let i = 0; i < datos.length; i++) {
                elemento.innerHTML += '<br/>';

                if (i == 0) {
                    elemento.innerHTML += '&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Primer registro <br>' +
                        '&nbsp&nbsp&nbsp&nbsp Fecha de Registro: ' + moment(datos[i].DateFecTimeStart).format("YYYY-MM-DD HH:mm") + '<br>';
                }
                else {
                    elemento.innerHTML += '&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Actualización. N°:' + i + '<br>'+
                    '&nbsp&nbsp Fecha de Actualización: ' + moment(datos[i].DateFecTimeStart).format("YYYY-MM-DD HH:mm") + '<br>';
                }
                    
                elemento.innerHTML += '' +
                    '<strong>Documento de la Madre:&nbsp</strong> ' + datos[i].DoubleIdMadre + '<br/>' +
                    '<strong>Nombre Madre:&nbsp</strong> ' + datos[i].StrNomMadre + '<br/>' +
                    '<strong>Tipo nacimiento:&nbsp</strong> ' + datos[i].StrTipNac + '<br/>' +
                    '<strong>Fecaha nacimiento:&nbsp</strong> ' + moment(datos[i].DateFecNac).format("YYYY-MM-DD HH:mm") + '<br/>' +
                    '<strong>Edad gestacional:&nbsp</strong> ' + datos[i].IntEdGesNac + '<br/>' +
                    '<strong>Documento Médico:&nbsp</strong> ' + datos[i].DoubleGNCodUsu + '<br/>' +
                    '<strong>Nombre Médico:&nbsp</strong>' + datos[i].StrNomDoc + '<br/>' +
                    '<strong>Peso NV:&nbsp</strong> ' + datos[i].IntPesoRn + '<br/>' +
                    '<strong>Talla NV:&nbsp</strong> ' + datos[i].FloatTallaRN + '<br/>' +
                    '<strong>Sexo NV:&nbsp</strong> ' + datos[i].StrSexo + '<br><br/>';
            }
            document.getElementById("btnHistoryNV").disabled = true;
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
})

//cargar informacion de lregistro seleccionado al modal.
function cargarInfoModalVerRegDEF(wi) {  

    if (Permisos[1]) {
        position = wi;
        let elemento = document.getElementById("ModalVerRegTex")
        elemento.innerHTML = '' +
            '<strong>Tipo Defunción:&nbsp </strong> ' + ConRegistros[0][wi].StrTipDef + '<br>' +
            '<strong>Fecha: </strong>&nbsp' + moment(ConRegistros[0][wi].DateFecDef).format("YYYY-MM-DD HH:mm") + '<br>' +
            '<strong>Nombre paciente:&nbsp </strong>' + ConRegistros[0][wi].StrNomPac + '<br>' +
            '<strong>Documento paciente: &nbsp</strong> ' + ConRegistros[0][wi].DoubleIdPaciente + '<br>' +
            '<strong>Código Ruaf: &nbsp</strong>' + ConRegistros[1][wi] + '<br>' +
            '<strong>Documento Médico:&nbsp</strong>' + ConRegistros[0][wi].DoubleGNCodUsu + '<br>' +
            '<strong>Nombre Médico:&nbsp </strong>' + ConRegistros[0][wi].StrNomDoc + '<br>' +
            '<strong>Servicio:&nbsp </strong> ' + ConRegistros[0][wi].StrServicio + '<br><br>';
            '<strong>Fallecimiento 48 después del ingreso:&nbsp </strong> ' + ConRegistros[0][wi].BlnEstadoPaciente ? "Si" : "No" + '<br>';

            //'<div class="row justify-content-center">' +  // boton para canturar el evento de cargar historia de un registro (historia de modificaciones)
            //'  <button type="button" class="btn btn-info btn-sm" id = "btnHistoryDef">Historial</button>' +
            //' </div>';

        $("#ModalVerReg").modal();
    }
    else {
        let elemento = document.getElementById("ModalVerRegTex")
        elemento.innerHTML = '' +
            '<strong>Tipo Defunción:&nbsp </strong> ' + ConRegistros[0][wi].StrTipDef + '<br>' +
            '<strong>Fecha: </strong>&nbsp' + moment(ConRegistros[0][wi].DateFecDef).format("YYYY-MM-DD HH:mm") + '<br>' +
            '<strong>Nombre paciente:&nbsp </strong>' + ConRegistros[0][wi].StrNomPac + '<br>' +
            '<strong>Documento paciente: &nbsp</strong> ' + ConRegistros[0][wi].DoubleIdPaciente + '<br>' +
            '<strong>Código Ruaf: &nbsp</strong>' + ConRegistros[1][wi] + '<br>' +
            '<strong>Documento Médico:&nbsp</strong>' + ConRegistros[0][wi].DoubleGNCodUsu + '<br>' +
            '<strong>Nombre Médico:&nbsp </strong>' + ConRegistros[0][wi].StrNomDoc + '<br>' +
            '<strong>Fallecimiento 48 después del ingreso:&nbsp </strong> ' + ConRegistros[0][wi].BlnEstadoPaciente ? "Si": "No" + '<br>';
        $("#ModalVerReg").modal();
    } 
}

$(document).on("click", "#btnHistoryDef", () => {  //agregar evento cargar historia al regitro Defunción seleccionado.

    $.ajax({
        url: "ConsultarRegistros.aspx/getHistoryDef", //metodo del lado del servidor para solicitar los datos.
        data: JSON.stringify({ 'IdPaciente': ConRegistros[0][position].DoubleIdPaciente + "" }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            datos = JSON.parse(msg.d);
            if (!datos.length > 0)
                alert("cero resultados")

            let elemento = document.getElementById("ModalVerRegTex");

            for (let i = 0; i < datos.length; i++) {
                elemento.innerHTML += '<br>';
                if (i == 0) {
                    elemento.innerHTML += '&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Primer registro <br>' +
                        '&nbsp&nbsp&nbsp&nbsp Fecha de Registro: ' + moment(datos[i].DateFecTimeStart).format("YYYY-MM-DD HH:mm") + '<br>';
                }
                else {
                    elemento.innerHTML += '&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Actualización. N°:' + i + '<br>' +
                        '&nbsp&nbsp Fecha de Actualización: ' + moment(datos[i].DateFecTimeStart).format("YYYY-MM-DD HH:mm") + '<br>';
                }

                elemento.innerHTML += ''+   
                    '<strong>Tipo Defunción:&nbsp </strong> ' + datos[i].StrTipDef + '<br>' +
                    '<strong>Fecha: </strong>&nbsp' + moment(datos[i].DateFecDef).format("YYYY-MM-DD HH:mm") + '<br>' +
                    '<strong>Nombre paciente:&nbsp </strong>' + datos[i].StrNomPac + '<br>' +
                    '<strong>Documento paciente: &nbsp</strong> ' + datos[i].DoubleIdPaciente + '<br>' +
                    '<strong>Documento Médico:&nbsp</strong>' + datos[i].DoubleGNCodUsu + '<br>' +
                    '<strong>Nombre Médico:&nbsp </strong>' + datos[i].StrNomDoc + '<br>' +
                    '<strong>Servicio:&nbsp </strong> ' + datos[i].StrServicio + '<br>';
                    '<strong>Fallecimiento 48 después del ingreso:&nbsp </strong> ' + ConRegistros[0][wi].BlnEstadoPaciente ? "Si" : "No" + '<br>';

            }
            document.getElementById("btnHistoryDef").disabled = true;

        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
})


// cargar registros de la tabla defuncion para exportar a excel.
function getDatosTablaDef() {

    var tabla = [];
    var header = [
        { v: "Tipo Defunción", t: "s" },
        { v: "Fecha Defunción", t: "s" },
        { v: "Nombre paciente", t: "s" },
        { v: "Documento paciente", t: "s" },
        { v: "Código Ruaf", t: "s" },
        { v: "Documento Médico", t: "s" },
        { v: "Nombre Médico", t: "s" },
        { v: "Servicio", t: "s" },
        { v: "Fallecimiento 48 después del ingreso", t: "s" }
    ];

    tabla.push(header);

    for (var wi = 0; wi < ConRegistros[0].length; wi++) {
        fila = [
            { v: ConRegistros[0][wi].StrTipDef, t: "s" },
            { v: moment(ConRegistros[0][wi].DateFecDef).format("YYYY-MM-DD HH:mm"), t: "s" },
            { v: ConRegistros[0][wi].StrNomPac, t: "s" },
            { v: ConRegistros[0][wi].DoubleIdPaciente, t: "s" },
            { v: ConRegistros[1][wi], t: "s" },
            { v: ConRegistros[0][wi].DoubleGNCodUsu, t: "s" },
            { v: ConRegistros[0][wi].StrNomDoc, t: "s" },
            { v: ConRegistros[0][wi].StrServicio, t: "s" },
            { v: ConRegistros[0][wi].BlnEstadoPaciente ? "Si" : "No", t: "s" }
        ]
        tabla.push(fila);
    }

    tableExport = new TableExport($("#tbRegDef"), {
        exportButtons: false, // No queremos botones
        filename: "Registros Defuncion", //Nombre del archivo de Excel
        sheetname: "Registros", //Título de la hoja
    });
    datos = tableExport.getExportData();
    preferenciasDocumento = datos.tbRegDef.xlsx;
    tableExport.export2file(tabla, preferenciasDocumento.mimeType, preferenciasDocumento.filename, preferenciasDocumento.fileExtension, preferenciasDocumento.merges, preferenciasDocumento.RTL, preferenciasDocumento.sheetname);

}

//cargar datos de la tabla a una variable para enviarla al archivo xlsx.
function getDatosTablaNV(){

    var tabla = [];
    var header = [
        { v: "Documento de la Madre", t: "s"},
        { v: "Nombre de la Madre", t: "s" },
        { v: "Tipo de Nacimiento", t: "s" },
        { v: "Fecha de Nacimiento", t: "s" },
        { v: "Código RUAF", t: "s" },
        { v: "Edad Gestacional", t: "s" },
        { v: "Documento del Médico", t: "s" },
        { v: "Nombre Médico", t: "s" },
        { v: "Peso Recien Nacido", t: "s" },
        { v: "talla del recien Nacido", t: "s" },
        { v: "Sexo", t: "s" }
                   ];

    tabla.push(header);

    for (var wi = 0; wi < ConRegistros[0].length; wi++) {
        fila = [
            { v: ConRegistros[0][wi].DoubleIdMadre, t: "s" },
            { v: ConRegistros[0][wi].StrNomMadre, t: "s" },
            { v: ConRegistros[0][wi].StrTipNac, t: "s" },
            { v: moment(ConRegistros[0][wi].DateFecNac).format("YYYY-MM-DD HH:mm"), t: "s" },
            { v: ConRegistros[1][wi], t: "s" },
            { v: ConRegistros[0][wi].IntEdGesNac, t: "s" },
            { v: ConRegistros[0][wi].DoubleGNCodUsu, t: "s" },
            { v: ConRegistros[0][wi].StrNomDoc, t: "s" },
            { v: ConRegistros[0][wi].IntPesoRn, t: "s" },
            { v: ConRegistros[0][wi].FloatTallaRN, t: "s" },
            { v: ConRegistros[0][wi].StrSexo, t: "s" }
        ]
        tabla.push(fila);
    }

    tableExport = new TableExport($("#tbRegNacViv"), {
        exportButtons: false, // No queremos botones
        filename: "Registros Nacidos Vivos", //Nombre del archivo de Excel
        sheetname: "Registros", //Título de la hoja
    });
    datos = tableExport.getExportData();
    preferenciasDocumento = datos.tbRegNacViv.xlsx;
    tableExport.export2file(tabla, preferenciasDocumento.mimeType, preferenciasDocumento.filename, preferenciasDocumento.fileExtension, preferenciasDocumento.merges, preferenciasDocumento.RTL, preferenciasDocumento.sheetname);

}




// funcion para activar el panel para busqueda por fechas o consultar todos los registros.
function activarBus() {

    if (opcFec.value == "Fecha") {
        let fecOpc = document.getElementById("columConRegFecha"); // seleccional el div donde se inserta los inputs
        fecOpc.innerHTML = '' + // insertamos los inpus correspondientes.
            '<div class="col-xl-4 col-lg-5 col-md-6 col-12">' +
            ' <label for="fechaConReg1" class="mb-0">Fecha Mínima</label>' +
            ' <input type="datetime-local" id="fechaConReg1" class="form-control inputCon" />' +
            ' </div>' +
            '<div class="col-xl-4 col-lg-5 col-md-6 col-12">' +
            '   <label for="fechaConReg2" class="mb-0">Fecha Máxima</label>' +
            '   <input type="datetime-local" id="fechaConReg2" class="form-control inputCon" visible="false" />' +
            '</div>';
        let parBusReg = document.getElementById("ParBusReg")
        parBusReg.setAttribute("readonly", "") // desactivamos en input para buscar por parametros. 
        parBusReg.value = "";

    }
    else if (opcFec.value == "Todos") {

        if (opcNacViv.checked) {
            let RegNac = {
                'dato': "",
                'opc': document.getElementById("tipConRegPar").value,
                'fechaMin': "",
                'fechaMax': ""
            }

            $.ajax({ //  Metodo para enviar peticion al servidor y solicitar datos de nacidos vivos. 
                url: "ConsultarRegistros.aspx/cargarRegNacViv", //metodo del lado del servidor envia los datos de PQRS
                data: JSON.stringify(RegNac),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    datos = JSON.parse(msg.d);

                    let tbNacViv = "";
                    if (datos[0].length == 0)
                        alert("Resultados Busqeuda: 0 coicidencias");

                    for (let i = 0; i < datos[0].length; i++) { // ciclo para cargar los campos de la tabla NacViv

                        tbNacViv += "<tr>" +
                            "<td>" + i + "</td>" +
                            "<td>" + datos[0][i].DoubleIdMadre + "</td>" +
                            "<td>" + datos[0][i].StrNomMadre + "</td>" +
                            "<td>" + datos[0][i].StrTipNac + "</td>" +
                            "<td>" + moment(datos[0][i].DateFecNac).format("YYYY-MM-DD HH:mm") + "</td>" +
                            "<td>" + datos[1][i] + "</td>" +
                            "<td>" + datos[0][i].DoubleGNCodUsu + "</td>";
                        if (Permisos[0])
                            tbNacViv += "<td><a href=\"ActualizarNacViv.aspx?OIdCRCodRuaf=" + datos[0][i].IntCRCodRuaf + "\">Actualizar</a></td>";

                        tbNacViv += "<td><a href=\"#\" onClick = \"cargarInfoModalVerRegNV(" + i + "); return false\" >Ver</a></td>" +
                            "</tr>";
                    }
                    ConRegistros = datos;

                    $("#tbdRegNacViv").html(tbNacViv);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }

        if (opcDef.checked) {
            let RegDef = {
                'dato': "",
                'opc': document.getElementById("tipConRegPar").value,
                'fechaMin': "",
                'fechaMax': ""
            }

            let metodoCon = "cargarRegDef";
            $.ajax({
                url: "ConsultarRegistros.aspx/"+metodoCon+"", //metodo del lado del servidor que envia los datos de Defuncion solicitados.
                data: JSON.stringify(RegDef),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    datos = JSON.parse(msg.d);
                    let tbDef = "";
                    if (datos[0].length == 0)
                        alert("Resultados Busqeuda: 0 coicidencias");
                    for (let i = 0; i < datos[0].length; i++) { // cargar la tabla de defunción con los datos enviados por el servidor. 
                        tbDef += "<tr>" +
                            "<td>" + i + "</td>" +
                            "<td>" + datos[0][i].StrTipDef + "</td>" +
                            "<td>" + moment(datos[0][i].DateFecDef).format("YYYY-MM-DD HH:mm") + "</td>" +
                            "<td>" + datos[0][i].StrNomPac + "</td>" +
                            "<td>" + datos[0][i].DoubleIdPaciente + "</td>" +
                            "<td>" + datos[1][i] + "</td>" +
                            "<td>" + datos[0][i].DoubleGNCodUsu + "</td>";
                        if (Permisos[0])
                            tbDef += "<td><a href=\"ActualizarDef.aspx?OIdCRCodRuaf=" + datos[0][i].IntOIdCRCodRuaf + "\">Actualizar</a></td>";

                        tbDef += "<td><a href=\"#\" onClick = \"cargarInfoModalVerRegDEF(" + i + "); return false\" >Ver</a></td>" +
                            "</tr>";
                    }
                    ConRegistros = datos;
                    $("#tbdRegDef").html(tbDef);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }


        let parBusReg = document.getElementById("ParBusReg")
        parBusReg.setAttribute("readonly", "") // desactivamos en input para buscar por parametros. 
        parBusReg.value = "";

    }
    else {
        let fecOpc = document.getElementById("columConRegFecha");
        fecOpc.innerHTML = '';
        let parBusReg = document.getElementById("ParBusReg");
        parBusReg.removeAttribute("readonly", "");
        parBusReg.value = "";
    }
}

// cargar las tablas segun el tipo de dato que el usuario desee consultar. 
function cargarOpcBu() {

    if (opcNacViv.checked) { // cargar tabla Nacidos vivos.
        //console.log("Nac");
        var divOpcNacViv = document.getElementById("tipConRegPar");
        var tablaReg = document.getElementById("TablaReg");
        var btnExcel = document.getElementById("btnExcel");

        if (Permisos[1]) {
            btnExcel.innerHTML = '';
            btnExcel.innerHTML = '' +
                '<button type="button" class="btn btn-primary  mt-3" id="excelNV"> Exportar Excel </button>';
        }
        

        divOpcNacViv.innerHTML = '';
        divOpcNacViv.innerHTML = '' +  //agregar al select los parametros de busqueda.
            '<option>Documento Madre</option>' +
            '<option>Nombre Madre</option>' +
            '<option>Tipo Parto</option>' +
            '<option>Código RUAF</option>' +
            '<option>Documento Médico</option>' +
            '<option>Fecha</option>' +
            '<option>Todos</option>';

        tablaReg.innerHTML = '';
        tablaReg.innerHTML = '' +  // agregar la tabla al body.
            '<div class="x_title row justify-content-center" style="position:sticky;top: 0;background: #fff;z-index: 500">' +
            '<div class="clearfix" >' +
            '<h6><strong>Consulta Nacidos Vivos</strong></h6>' +
            '</div>' +
            '</div>' +
            '<div class="x_content" style="overflow:auto; max-height:400px;">' +
            '<table class="table table-hover table-striped" id="tbRegNacViv" style="width:100%">' +
            '<thead>' +
            '<tr ">' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white; border-top-left-radius:5px">#</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Documento Madre </th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Nombre Madre</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Tipo Nacimiento</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Fecha</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Código RUAF</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Documento Médico</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white"></th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white; border-top-right-radius:5px"></th>' +
            '</tr>' +
            '</thead>' +
            '<tbody id="tbdRegNacViv">' +
            '</tbody>' +
            '</table>' +
            ' </div>';
        let fecOpc = document.getElementById("columConRegFecha");
        fecOpc.innerHTML = '';
        let parBusReg = document.getElementById("ParBusReg");
        parBusReg.removeAttribute("readonly", "");
        parBusReg.value = "";

        document.getElementById("excelNV").addEventListener('click', getDatosTablaNV, false); // agregar evento click al boton para generar excel de registro nacido vivo.
    }

    if (opcDef.checked) { // Cargar tabla Defunciones 
        //console.log("Def")
        var divOpcNacViv = document.getElementById("tipConRegPar");
        var TablaReg = document.getElementById("TablaReg");
        var btnExcel = document.getElementById("btnExcel");

        if (Permisos[1]) {
            btnExcel.innerHTML = '';
            btnExcel.innerHTML = '' +
                '<button type="button" class="btn btn-primary  mt-3" id="excelDEF"> Exportar Excel </button>';
        }
        


        divOpcNacViv.innerHTML = '';
        divOpcNacViv.innerHTML = '' + // agregamos las opciones a la etiqueta select
            '<option>Documento Paciente</option>' +
            '<option>Nombre Paciente</option>' +
            '<option>Tipo defunción</option>' +
            '<option>Código RUAF</option>' +
            '<option>Documento Médico</option>' +
            '<option>Fecha</option>' +
            '<option>Todos</option>';
        
        TablaReg.innerHTML = '';
        TablaReg.innerHTML = '' + // agregar la tabla defunciones al body.
            
            ' <div class="x_title row justify-content-center" style="position:sticky;top: 0;background: #fff;z-index: 500">' +
            '<div class="clearfix">' +
            '<h6><strong>Consulta Defunción</strong></h6>' +
            '</div>' +
            '</div>' +
            '<div class="x_content" style="overflow:auto; max-height:400px;">' +
            '<table class="table table-hover table-striped" id="tbRegDef" style="width:100%">' +
            '<thead>' +
            '<tr>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white; border-top-left-radius:5px">#</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Tipo </th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Fecha</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Nombre</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Documento Paciente</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Código RUAF</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white">Documento Médico</th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white"></th>' +
            '<th style="position:sticky;top: 0px; background:#2a3f54; color:white; border-top-right-radius:5px"></th>' +
            '</tr>' +
            '</thead>' +
            '<tbody id="tbdRegDef">' +
            '</tbody>' +
            '</table>' +
            '</div>';
        let fecOpc = document.getElementById("columConRegFecha");  
        fecOpc.innerHTML = '';

        let parBusReg = document.getElementById("ParBusReg"); //ParBusReg = parametros busqueda registro. 
        parBusReg.removeAttribute("readonly", "");
        parBusReg.value = "";

        document.getElementById("excelDEF").addEventListener('click', getDatosTablaDef, false); // agregar evento click al boton para generar excel de registro Defunción
    }
}

//validar si tiene permiso para modificar en el modulo estadicsticas vitales opcion consultar registros
function VPermisoAct() { 
    
    $.ajax({ 
        url: "ConsultarRegistros.aspx/ValidarPermisosMod", //metodo del lado del servidor envia los datos de PQRS
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            let dato = JSON.parse(msg.d);
            Permisos = dato;
            
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    }); 
} 
