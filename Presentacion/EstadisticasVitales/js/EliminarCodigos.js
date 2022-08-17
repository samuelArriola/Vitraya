var hasFile = false;  //control para verificar lectura archivo .txt
var numCodigos = []; // array para almacenar códigos validos
var numCodRep = []; // array para almacenar códigos duplicados dentro del mismo archivo .txt
var numCodRepBD = []; // array para almacenar los codigos que se encuantran en la BD

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


//agregando al input, el evento 'change' para leer el archivo cada que suban uno diferente. 
document.getElementById('fuArchivoACodigoRUAF')
    .addEventListener('change', leerArchivo, false);
function leerArchivo(e) { // funcion para leer el archivo txt. 
    var archivo = e.target.files[0];
    document.getElementById("ContentPlaceHolder_txtNomArchivo").innerHTML = e.target.files[0].name;
    if (!archivo) {
        hasFile = false;
        return;
    }
    if (archivo.type !== 'text/plain') {
        error("error", "Archivo no permitido");
        return;
    }

    hasFile = true;
    var lector = new FileReader();
    lector.onload = function (e) {
        var contenido = e.target.result;
        listarCodigos(contenido);
    };
    lector.readAsText(archivo);
}

// funcion para hacer una lista con el contenido del archivo. (dividir los códigos, y almacenarlos en una lista.)
function listarCodigos(contenido) {
    numCodigos = [];
    numCodRep = [];
    var lines = contenido.split(/\n/);

    lines.forEach((line) => {

        var codigos = line.split(" ");
        if (codigos.length == 1)
            codigos = line.split("\t")
        if (codigos.length == 1)
            codigos = line.split(";")
        if (codigos.length == 1)
            codigos = line.split(",")

        for (var i = 0; i < codigos.length; i++) {


            if (isNum(codigos[i]) && parseInt(codigos[i])) {
                if (!numCodigos.includes(parseInt(codigos[i])))
                    numCodigos.push(parseInt(codigos[i]));
                else
                    numCodRep.push(parseInt(codigos[i]));
            }
            else if (codigos[i] != "" && codigos[i] != " " && codigos[i].charCodeAt(0) != 13) {
                console.log("El codigo tiene caracter invalido " + codigos[i]);
                alert("El codigo tiene caracter invalido " + codigos[i]);
            }
        }
    });
}

//agregar al boton EliinarCodigos, el evento para mostrar los codigos; validos, duplicados y los que ya estan almacenados en BD
$("#BtnEliminarCodigos").on("click", (e) => {
    e.preventDefault();

    var btnNacViv = document.getElementById('btnNacidosvivos');
    var btnDef = document.getElementById('btnDefuncion');

    if (!hasFile)
        error("Error", "No se ha subido ningun Archivo")
    else if (!btnNacViv.checked && !btnDef.checked)
        error("Error", "Seleccione tipos de còdigo")
    else {
        valCodRuafDup();
        $("#exampleModal").modal();
    }

})

$("#btnEliminarCodigosModal").on("click", (e) => {

    if (numCodRepBD.length > 0) {
        //var CodRuaf = crearListCodRuaf();
        $.ajax({
            url: "EliminarCodigos.aspx/EliminarCodigoRuafs",
            data: JSON.stringify({ "numCodRepBD": numCodRepBD }),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                window.location.href = "CargarCodigosRUAF.aspx"
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });

    }
    else
        alert("0 codigos validos a eliminar, por favor cargue codigos.");

})

//funcion par crear uan lista de variables tipo CodRuaf
function crearListCodRuaf() {
    var CodRuaf = [];
    for (var wi = 0, m = numCodigos.length; wi < m; wi++) {
        var tipoCod;
        if (document.getElementById('btnNacidosvivos').checked)
            tipoCod = 'NacViv'
        if (document.getElementById('btnDefuncion').checked)
            tipoCod = 'Defunción'

        var Codigo = {
            "IntOIdCRCodRuaf": 0,
            "DateFecCod": new Date(),
            "StrIncidencia": '',
            "IsEstado": true,
            "StrTipCodigo": tipoCod,
            "DoubleGNCodUsu": 0,
            "doubleCRcodRuaf": parseInt(numCodigos[wi]),
        }

        CodRuaf.push(Codigo);
    }
    return CodRuaf;
}

// funcion para hacer una lista con el contenido del archivo. (dividir los códigos, y almacenarlos en una lista.)
function listarCodigos(contenido) {
    numCodigos = [];
    numCodRep = [];
    var lines = contenido.split(/\n/);

    lines.forEach((line) => {

        var codigos = line.split(" ");
        if (codigos.length == 1)
            codigos = line.split("\t")
        if (codigos.length == 1)
            codigos = line.split(";")
        if (codigos.length == 1)
            codigos = line.split(",")

        for (var i = 0; i < codigos.length; i++) {


            if (isNum(codigos[i]) && parseInt(codigos[i])) {
                if (!numCodigos.includes(parseInt(codigos[i])))
                    numCodigos.push(parseInt(codigos[i]));
                else
                    numCodRep.push(parseInt(codigos[i]));
            }
            else if (codigos[i] != "" && codigos[i] != " " && codigos[i].charCodeAt(0) != 13) {
                console.log("El codigo tiene caracter invalido " + codigos[i]);
                alert("El codigo tiene caracter invalido " + codigos[i]);
            }
        }
    });
}

//validar si un caracter es número.
const isNum = (num) => {
    for (var i = 0; i < num.length; i++) {
        if (isNaN(num.charAt(i)))
            return false
    }
    return true
}


//funcion para insertar en el modal la info de codigos validos y repetidos. 
function mostrarContenido() {
    var elemento = document.getElementById('contenido-archivo');
    elemento.innerHTML = '';

    elemento.innerHTML += "<hr/>";
    elemento.innerHTML += "<br/> N° de códigos que no se encuentran registrados: " + numCodigos.length;
    elemento.innerHTML += "<br/>";

    for (var i = 1; i < numCodigos.length + 1; i++) {

        if (i % 5 === 0) {
            elemento.innerHTML += numCodigos[i - 1] + "<br/>";
        }
        else {
            elemento.innerHTML += numCodigos[i - 1] + '&nbsp;&nbsp;&nbsp;';
        }
    }

    elemento.innerHTML += "<hr/>";
    elemento.innerHTML += "<br/> N° de Códigos duplicados en archivo .txt: " + numCodRep.length;
    elemento.innerHTML += "<br/>";

    for (var i = 1; i < numCodRep.length + 1; i++) {

        if (i % 5 === 0) {
            elemento.innerHTML += numCodRep[i - 1] + "<br/>";
        }
        else {
            elemento.innerHTML += numCodRep[i - 1] + '&nbsp;&nbsp;&nbsp;';
        }
    }

}


function valCodRuafDup() { // obtener una lista del servidor con los codigos duplicados, en caso de que halla.
    var CodRuaf = crearListCodRuaf();
    $.ajax({
        url: "CargarCodigosRUAF.aspx/valCodRuafDup",
        data: JSON.stringify({ "CodRuaf": CodRuaf }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            var numCodRepBd = JSON.parse(msg.d); //respuesta del servidor, lista de codigos que ya se encuentran en base de datos.

            for (var i = 0, m = numCodRepBd.length; i < m; i++) numCodRepBD[i] = numCodRepBd[i].DoubleCRcodRuaf;

            if (numCodRepBD.length > 0) {

                for (var wi = 0, m = numCodRepBd.length; wi < m; wi++) { // ciclo para filtrar del array los codigos repetidos.
                    var u = numCodRepBd[wi].DoubleCRcodRuaf;
                    var i = numCodigos.indexOf(u);
                    numCodigos.splice(i, 1);
                }

            }
            mostrarContenido(); // funcion para imprimir en un modal las listas de códigos correctos y duplicados.  

            //agregar el modal, la info de los codigos repetidos, para que usuario pueda ver cuales son los que ya esta en BD
            var elemento = document.getElementById('contenido-archivo');
            elemento.innerHTML += "<hr/>";
            elemento.innerHTML += "<br/> N° de Códigos en base de datos: " + numCodRepBD.length;
            elemento.innerHTML += "<br/>";

            for (var i = 1; i < numCodRepBD.length + 1; i++) { // ciclo para imprimir el vector de codigos repetidos. 

                if (i % 5 === 0) {
                    elemento.innerHTML += numCodRepBD[i - 1] + "<br/>";
                }
                else {
                    elemento.innerHTML += numCodRepBD[i - 1]+ '&nbsp;&nbsp;&nbsp;';
                }
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);

        }

    });

}