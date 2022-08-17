
var parametroBus = $("#ParBusReg");

function error(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'error',
        styling: 'bootstrap3',
        delay: 1000
    });
}

function exito(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'success',
        styling: 'bootstrap3',
        delay: 3000
    });
}


var opcValNacViv = document.getElementById("ContentPlaceHolder_TipValNacViv");
var opcValDef = document.getElementById("ContentPlaceHolder_TipValDef");
opcValNacViv.addEventListener('click', cargarTb, false);
opcValDef.addEventListener('click', cargarTb, false);
document.getElementById("btnValReg").addEventListener('click', cargarTabla, false); //boton buscar registro.


document.getElementById("ParValReg").addEventListener("keypress", soloNumeros, false); // limitar a solo numérico input de busqueda.
function soloNumeros(e) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}





// funcion para guardar la incidencia. 
function guardarInc(){

        // validar que sea opc nacido vivo y ademas ya halla seleccionado registro a modificar. 
    if (opcValNacViv.checked && [...document.querySelectorAll("#tbValNacViv td")].length > 0 && document.getElementById("texIncidencia").value != "") {

        let rowReg = [...document.querySelectorAll("#tbValNacViv td")]        
        let dato = {
            'incidencia': document.getElementById("texIncidencia").value,
            'OIdCRCodRuaf': rowReg[5].innerText,
            'tipoReg': "NacViv"
        }

        $.ajax({ // metodo para enviar al servidor la incidencia, y actualizar el código.
            url: "ModificarValidarCodigo.aspx/actRegIncidenciaNacViv",
            data: JSON.stringify(dato),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
               exito("success", "Guardado");
                window.location.href = "ModificarValidarCodigo.aspx" 
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });

        //validar que sea Defunción y que ya tenga seleccionado el registro a modificar.
    } else if (opcValDef.checked && [...document.querySelectorAll("#tbValDef td")].length > 0 && document.getElementById("texIncidencia").value != "") {
        let rowReg = [...document.querySelectorAll("#tbValDef td")]  
        let dato = {
            'incidencia': document.getElementById("texIncidencia").value,
            'OIdCRCodRuaf': rowReg[5].innerText,
            'tipoReg': "Defunción"
        }

        $.ajax({ // metodo para enviar al servidor la incidencia, y actualizar el codigo del registro.
            url: "ModificarValidarCodigo.aspx/actRegIncidenciaDef",
            data: JSON.stringify(dato),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                exito("success", "Guardado");
                window.location.href = "ModificarValidarCodigo.aspx"
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    } 

    if ([...document.querySelectorAll("#tbValDef td")].length = 0 || document.getElementById("texIncidencia").value == "") {
        error("error", "Buscar registro y describir incidencia.");
    }

}

// funcion para cargar la tabla y el registro que se desea modificar.
function cargarTabla() { 

    if (opcValNacViv.checked) { // condicional para iniciar proceso de busqueda y carga de datos nacidos vivo
       
        if (document.getElementById("ParValReg").value != "") {
            let dato = {
                'CodRuaf': document.getElementById("ParValReg").value
            }
             
            $.ajax({
                url: "ModificarValidarCodigo.aspx/cargarValRegNacViv", // metodo para hacer peticion de registro nacido vivo al servidor
                data: JSON.stringify(dato),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {
                    datos = JSON.parse(msg.d);
                    let tbNacViv = "";
                    if (datos[0].length == 0)   
                        alert("Resultados Busqeuda: 0 coicidencias");
                    for (let i = 0; i < datos[0].length; i++) {
                        tbNacViv += "<tr>" +
                            "<td>" + i + "</td>" +
                            "<td>" + datos[0][i].DoubleIdMadre + "</td>" +
                            "<td>" + datos[0][i].StrNomMadre + "</td>" +
                            "<td>" + datos[0][i].StrTipNac + "</td>" +
                            "<td>" + moment(datos[0][i].DateFecNac).format("YYYY-MM-DD HH:mm") + "</td>" +
                            "<td>" + datos[1][i] + "</td>" +
                            "<td>" + datos[0][i].DoubleGNCodUsu + "</td>" +
                            "</tr>";
                    }

                    $("#tbdValNacViv").html(tbNacViv);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });

        } else {
            error("error", "Por favor digite Código");
        }

    } else if (opcValDef.checked) { // condicional para iniciar proceso de busqueda y carga de datos Defunción

        if (document.getElementById("ParValReg").value != "") {
            let dato = {
                'CodRuaf': document.getElementById("ParValReg").value
            }

            $.ajax({
                url: "ModificarValidarCodigo.aspx/cargarValRegDef", //metodo para hacer peticion de registro Defunción al servidor
                data: JSON.stringify(dato),
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
                            "<td>" + datos[0][i].DoubleGNCodUsu + "</td>" +
                            "</tr>";
                    }

                    $("#tbdValDef").html(tbDef);
                },
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        } else {
            error("error","Por favor digite Código");
        }

    } else {
        error("error","Por favor seleccione tipo de registro y digite Código");
    }
}


// funcion para cargar las tablas, segun la consulta.
function cargarTb() {
    if (opcValNacViv.checked) { // validar opc nacido vivo.
        var tablaVal = document.getElementById("TablaValReg");
        tablaVal.innerHTML = '';
        tablaVal.innerHTML = '' +   //cargar tabla nacido vivo.
            
            '<div class="x_title row justify-content-center">' + 
            '<div class="clearfix">' +
            '<h6><strong>Registro Nacidos Vivos</strong></h6>' +
            '</div>' +
            '</div>' +
            '<div class="x_content" style="overflow:auto; max-height:400px;">' +
            '<table class="table table-hover" id="tbValNacViv" style="width:100%">' +
            '<thead>' +
            '<tr>' +
            '<th style="background:#2a3f54; color:white; border-top-left-radius:5px">#</th>' +
            '<th style="background:#2a3f54; color:white">Documento madre </th>' +
            '<th style="background:#2a3f54; color:white">Nombre Madre</th>' +
            '<th style="background:#2a3f54; color:white">Tipo Nacimiento</th>' +
            '<th style="background:#2a3f54; color:white">Fecha</th>' +
            '<th style="background:#2a3f54; color:white">Código RUAF</th>' +
            '<th style="background:#2a3f54; color:white; border-top-right-radius:5px">Documento Doctor</th>' +
            '</tr>' +
            '</thead>' +
            '<tbody id="tbdValNacViv">' +
            '</tbody>' +
            '</table>' +
            ' </div>' +
            '<section id="TipVal">' +
            '<div class="row mt-3 justify-content-center">' +
            '<div class="col-xl-3 col-lg-4 col-md-5 col-sm-7 col-10 mb-2 ">' +
            '<label for="texIncidencia">Describa Incidencia.</label>' +
            '<textarea class="form-control inputCon" id="texIncidencia" rows="3" style="border-radius:5px"></textarea>' +
            '</div>' +
            '</div>' +
            '<div class="row mt-3 justify-content-center">' +
            '<button type="button" class="col-xl-1 col-lg-2 col-md-2 col-sm-2 col-3 btn btn-primary" id="btnGuardarVal">Guardar</button>' +
            '</div>' +
            '</section>';

        
    }

    if (opcValDef.checked) { // validar opcion Defunción
        var TablaVal = document.getElementById("TablaValReg");
        TablaVal.innerHTML = '';
        TablaVal.innerHTML = '' + //cargar tabla defunción
            
            ' <div class="x_title row justify-content-center">' +
            '<div class="clearfix">' +
            '<h6><strong>Registro Defunción</strong></h6>' +
            '</div>' +
            '</div>' +
            '<div class="x_content" style="overflow:auto; max-height:400px;">' +
            '<table class="table table-hover" id="tbValDef" style="width:100%">' +
            '<thead>' +
            '<tr>' +
            '<th style="background:#2a3f54; color:white; border-top-left-radius:5px">#</th>' +
            '<th style="background:#2a3f54; color:white">Tipo </th>' +
            '<th style="background:#2a3f54; color:white">Fecha</th>' +
            '<th style="background:#2a3f54; color:white">Nombre</th>' +
            '<th style="background:#2a3f54; color:white">Documento Paciente </th>' +
            '<th style="background:#2a3f54; color:white">Código RUAF</th>' +
            '<th style="background:#2a3f54; color:white; border-top-right-radius:5px">Documento Doctor</th>' +
            '</tr>' +
            '</thead>' + 
            '<tbody id="tbdValDef">' +
            '</tbody>' +
            '</table>' +
            '</div>' +
            
            '<section id="TipVal">' +
            '<div class="row mt-3 justify-content-center">' +
            '<div class="col-xl-3 col-lg-4 col-md-5 col-sm-7 col-10 mb-2 ">' +
            '<label for="texIncidencia">Describa Incidencia.</label>' +
            '<textarea class="form-control inputCon" id="texIncidencia" rows="3" style="border-radius:5px"></textarea>' +
            '</div>' +
            '</div>' +
            '<div class="row mt-3 justify-content-center">' +
            '<button type="button" class="col-xl-1 col-lg-2 col-md-2 col-sm-2 col-3 btn btn-primary" id="btnGuardarVal">Guardar</button>' +
            '</div>' +
            '</section>';
        
    }
    document.getElementById("btnGuardarVal").addEventListener('click', guardarInc, false); // btn guardar incidencia. 
}