$("#lnkEditarFoto").click(function (e) {
    e.preventDefault();
    $("#ContentPlaceHolder_fuFotoPerfil").click();
});

$("#ContentPlaceHolder_fuFotoPerfil").change(function (e) {
    const archivos = this.files;
    if (!archivos || !archivos.length) {
        $("#ContentPlaceHolder_imagePerfil2").css({
            "background-image" : `url("../Images/user.png")`
        })
        return;
    }
    const primerArchivo = archivos[0];
    const objectURL = URL.createObjectURL(primerArchivo);
    $("#ContentPlaceHolder_imagePerfil2").css({
        "background-image": `url("${objectURL}")`
    })
});

$("#ContentPlaceHolder_btnActualizar").click(function (e) {
    if ($("#ContentPlaceHolder_txtNombreE").val().trim() == "") {
        error("Campo Nombre vacio", "Por favor complete el campo Nombre antes de actulizar")
        e.preventDefault()
        return;
    }
    if ($("#ContentPlaceHolder_txtEmailE").val().trim() == "") {
        error("Campo Correo electrónico vacio", "Por favor complete el campo Correo electrónico antes de actulizar")
        e.preventDefault()
        return;
    }
    if ($("#ContentPlaceHolder_txtTelefonoE").val().trim() == "") {
        error("Campo Teléfono vacio", "Por favor complete el campo Teléfono antes de actulizar")
        e.preventDefault()
        return;
    }
})