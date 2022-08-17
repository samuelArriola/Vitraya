
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
    $imagenPrevisualizacion.style.backgroundImage = "url(" + objectURL + ")";
});

$("#txtFoto").change(function (e) {
    let nomArchivo = e.target.value.split("\\");
    nomArchivo = nomArchivo[nomArchivo.length - 1]
    $("#lbFoto").text(nomArchivo);
});

$("#btnActualizarUsuario").click(function (e) {
    exito("Hecho","Datos Actulizados")
})