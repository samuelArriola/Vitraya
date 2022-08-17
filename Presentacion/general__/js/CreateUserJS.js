let documento = $("#documento");
let nombre = $("#nombre");
let cargo = $("#cargo");
let email = $("#email");
let estado = $("#estado");


function error(titulo, texto) {
    new PNotify({
        title: titulo,
        text: texto,
        type: 'error',
        styling: 'bootstrap3',
        delay: 3000
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

const cargarTablaUsuarios = () => {
   
    let usuario = {
        'documento':documento.val(),
        'nombre': nombre.val(),
        'cargo': cargo.val(),
        'email': email.val(),
        'estado': estado.val()
    }


    $.ajax({
        url: "CreateUsuarios.aspx/cargarTablaUsuarios", //metodo del lado del servidor que gurada los datos del PQRS
        data: JSON.stringify(usuario),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            datos = JSON.parse(msg.d);
            let tbUsuarios = ""

            for (let i = 0; i < datos.length; i++) {
                tbUsuarios += "<tr>" +
                    "<td>" + i + "</td>"+
                    "<td>" + datos[i].GNCodUsu1 + "</td>"+
                    "<td>" + datos[i].GNNomUsu1 + "</td>"+
                    "<td>" + datos[i].GnCargo1 + "</td>"+
                    "<td>" + datos[i].GNCrusu1 + "</td>"+
                    "<td>" + datos[i].GnEtUsu1 + "</td>" +
                    "<td><a href=\"ActualizarUsuario.aspx?idUsuario=" + datos[i].GNCodUsu1+"\">Actualizar</a></td>" +
                "</tr>";
            }

            $("#tbdUsuarios").html(tbUsuarios);
        },
        error: function (result) {
             alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });

}

cargarTablaUsuarios();

inputs = [...document.querySelectorAll("#tbUsuarios input")];

for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener("keyup", cargarTablaUsuarios, false);
}

const $seleccionArchivos = document.querySelector("#fuImagePerfil"),
    $imagenPrevisualizacion = document.querySelector("#imagePerfil");

// Escuchar cuando cambie
$seleccionArchivos.addEventListener("change", () => {
    // Los archivos seleccionados, pueden ser muchos o uno
    const archivos = $seleccionArchivos.files;
    // Si no hay archivos salimos de la función y quitamos la imagen
    if (!archivos || !archivos.length) {
        $imagenPrevisualizacion.src = "";
        return;
    }
    // Ahora tomamos el primer archivo, el cual vamos a previsualizar
    const primerArchivo = archivos[0];
    // Lo convertimos a un objeto de tipo objectURL
    const objectURL = URL.createObjectURL(primerArchivo);
    // Y a la fuente de la imagen le ponemos el objectURL
    $imagenPrevisualizacion.style.backgroundImage = "url(" + objectURL +")";
});

$("#txtFoto").change(function (e) {
    let nomArchivo = e.target.value.split("\\");
    nomArchivo = nomArchivo[nomArchivo.length - 1]
    $("#lbFoto").text(nomArchivo);
});

$("#form1").submit(function (e) {
    if ($("#txtDocumento").val() == "") {
        error("Error","Falta Digilenciar el campo Documento")
        e.preventDefault();
        return;
    }
    if ($("#txtNombre").val() == "") {
        error("Error", "Falta Digilenciar el campo Nombre")
        e.preventDefault();
        return;
    }
    if ($("#txtUnidadFuncional").val() == "-1") {
        error("Error", "Falta Digilenciar el campo Unidad Funcional")
        e.preventDefault();
        return;
    }
    if ($("#txtCargo").val() == "-1") {
        error("Error", "Falta Digilenciar el campo Cargo")
        e.preventDefault();
        return;
    }
    if ($("#Email").val() == "") {
        error("Error", "Falta Digilenciar el campo Correo Electrónico")
        e.preventDefault();
        return;
    }
    if ($("#txtPasssword").val() == "") {
        error("Error", "Falta Digilenciar el campo Contraseña")
        e.preventDefault();
        return;
    }
    if ($("#ddlEps").val() == "-1") {
        error("Error", "Falta Digilenciar el campo EPS")
        e.preventDefault();
        return;
    }
    if ($("#txtTelefono").val() == "") {
        error("Error", "Falta Digilenciar el campo Teléfono")
        e.preventDefault();
        return;
    }
    if ($("#ddlRoles").val() == "-1") {
        error("Error", "Falta Digilenciar el campo Rol De usuario")
        e.preventDefault();
        return;
    } if ($("#txtFoto").get(0).files.length === 0) {
        error("Error", "Falta Digilenciar el campo Firma")
        e.preventDefault();
        return;
    }
    if ($("#fuImagePerfil").get(0).files.length === 0) {
        error("Error", "Falta Digilenciar el campo Firma")
        e.preventDefault();
        return;
    }
    exito("Hecho", "Datos Actulizados");
});