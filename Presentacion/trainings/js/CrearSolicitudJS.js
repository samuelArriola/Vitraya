

// Variables globales para los controles que se usaran en el proceso de la solicitud


TextFecha = $("#ContentPlaceHolder_TextFecha");
TextHoraIni = $("#ContentPlaceHolder_TextHoraIni");
TextHoraFinal = $("#ContentPlaceHolder_TextHoraFinal");
DropModalidad = $("#ContentPlaceHolder_DropModalidad");
DropLugar = $("#ContentPlaceHolder_DropLugar");
TextResponsable = $("#ContentPlaceHolder_TextResponsable");
ddlUsuarios = $("#ContentPlaceHolder_ddlUsuarios");
DropUnidad = $("#ContentPlaceHolder_DropUnidad");
DropEje = $("#ContentPlaceHolder_DropEje");
Texttema = $("#ContentPlaceHolder_TextFecha");
TextLInk = $("#ContentPlaceHolder_TextLInk");
FileUpload1 = $("#ContentPlaceHolder_FileUpload1");
fuExamen = $("#ContentPlaceHolder_fuExamen");
txtMatricula = $("#ContentPlaceHolder_txtMatricula");
btnguardar = $("#ContentPlaceHolder_btnguardar");
Button2 = $("#ContentPlaceHolder_Button2");


//listado de todos los archivos subidos a traves de FileUpload1
Archivos = []

function getFile(file) {
    var reader = new FileReader();
    return new Promise((resolve, reject) => {
        reader.onerror = () => { reader.abort(); reject(new Error("Error parsing file")); }
        reader.onload = function () {

            let bytes = Array.from(new Uint8Array(this.result));
            let nombre = file.name;
            let base64StringFile = btoa(bytes.map((item) => String.fromCharCode(item)).join(""));
            let nombres = nombre.split(".");

            nombre = nombre.replace(`.${nombres[nombres.length - 1]}`, "");

            resolve({
                'IntOidGNArchivo': 0,
                'base64StringFile': base64StringFile,
                'IntOidGNListaArchivos': 0,
                'StrContenido': file.type,
                'StrExt': nombres[nombres.length - 1],
                'StrNombre': nombre,
            });
        }
        reader.readAsArrayBuffer(file);
    });
}



//Metodo que se usa para generalizar la socitudes ajax al servidor
function ejecutarajax(url, data, success) {
    $.ajax({
        url: url,
        dataType: "json",
        data: JSON.stringify(data),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: success,
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}


function CargarArchivos(msg) {
    //se obtiene la lista de los archivos
    archivos = msg.d;

    
    //se crea la variable tabla cargar los datos en una tabla en formato html
    let tabla = "";

    //por cada item de la lista de los archivos se crea una nueva fila
    archivos.forEach(archivo => {
        tabla += `
            <tr>
                <td>${archivo.StrNombre}</td>
                <td>${archivo.StrExt}</td>
                <td><i class="glyphicon glyphicon-trash btn-delete" data-id="${archivo.IntOidGNArchivo}"></i></td>
            </tr>
        `
    });

    //se pasan los datos de la variable tabla a la tabla de los archivos
    $("#tbArchivos tbody").html(tabla);

}

//metodo que obtiene una lista de los archivos desde el servidor
function GetArchivosCapactacion() {
    //se consulta el id de la capacitacion que se encuentra en la url
    let params = new URLSearchParams(window.location.search);
    let idCapacitacion = parseInt(params.get("idCapacitacion"));

    //se hace una peticion ajax para consulta la lista de lis archivos atraves del id de la capacitacion 
    ejecutarajax("CrearSolicitud.aspx/GetArchivos", {}, CargarArchivos);
}

//metodo que muesta el nombre del archivo que se usara para crea el axamen
$("#ContentPlaceHolder_fuExamen").change(function (e) {
    e.preventDefault();
    let file = e.target.files[0];
    let domPadre = e.target.parentElement;
    let label = domPadre.querySelector("label");

    label.innerText = file.name;
});

$(document).on("click", ".btn-delete", function (e) {

    //se obtiene el id del archivo
    idArchivo = parseInt(e.target.getAttribute("data-id"));

    //se envia la solictud para eliminar el archivo y se muetra un mensaje de exito
    ejecutarajax("EditarCapa.aspx/DaleteArchivo", { 'idArchivo': idArchivo }, () => { exito("Hecho", "El archivo ha sido Eliminado exitosamente"), GetArchivosCapactacion() })
});



FileUpload1.change( async e => {
    e.preventDefault();
    file = e.target.files[0];
    archivo = await getFile(file)
    ejecutarajax(
        "CrearSolicitud.aspx/SetArchivo",
        archivo,
        function () {
            exito("Hecho", "El Archivo ha sido cargado");
            GetArchivosCapactacion();
        }
    )
});