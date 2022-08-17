
function MensajeGuardado() {
    exito("Notificacion", "El bloqueo se ha cambiado satisfactoriamente");
}

function CargarTablaBloqueos(msg) {

    datos = msg.d;
    console.log(datos);
    dtBloqueos = ""

    datos.forEach((item, i) => {
        dtBloqueos += `

            <tr>
                <td>
                    <input type="hidden" id="inputCodigo" value="${item.intOidGnScriptsBloqueos}"> 
                    ${item.intOidGnScriptsBloqueos}
                </td>
                <td>${item.strNombre}</td>

                <td>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input checkEstado" type="radio" name="inlineRadioEstado${i}"  data-id="${item.intOidGnScriptsBloqueos}"  value="Pedagogico" ${item.strEstado == "Pedagogico"? "Checked":""}>
                        <label class="form-check-label" for="inlineRadio1">Pedagógico</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input checkEstado" type="radio" name="inlineRadioEstado${i}" data-id="${item.intOidGnScriptsBloqueos}"  value="Restrictivo" ${item.strEstado == "Restrictivo" ? "Checked" : ""}>
                        <label class="form-check-label" for="inlineRadio2">Restrictivo</label>
                    </div>
                </td>
            </tr>
        `;
    })
    $("#tbBloqueos").html(dtBloqueos);
}

$(document).on("change", ".checkEstado", function (e) {
    let id = parseInt($(e.target).attr("data-id"));
    let estado = $(e.target).val();

    datos = {
        "id": id,
        "estado": estado 
    }
    console.log(datos);
    ejecutarajax("PanelBloqueos.aspx/ActualizarBloqueos", datos, MensajeGuardado)

})

function GetPanelBloqueos() {
    ejecutarajax("PanelBloqueos.aspx/ObtenerBloqueos", {}, CargarTablaBloqueos);
}

GetPanelBloqueos();

//EXTRAER OPCIONES BLOQUEADAS

function MensajeGuardado2() {
    exito("Notificacion", "El cambio sobre la opcion se aplico correctamente");
}

$(document).on("click", ".btnMostrarOB", function (e) {

    AModalOpciones();

});

function CargarTablaOpcionesBloqueadas(msg) {

    datos = msg.d;
    dtOBloqueos = ""

    datos.forEach((item, i) => {
        dtOBloqueos += `

            <tr>
                <td>
                    <input type="hidden" id="inputCodigoOB" value="${item.IntOidGNOpcion}">
                    ${item.IntOidGNOpcion}
                </td>
                <td>${item.StrNombre}</td>

                <td>
                     <label class="switch">
                          <input type="checkbox" class="checkOpciones" name="checkOpciones${i}" data-id="${item.IntOidGNOpcion}" ${item.IntEstadoBloqueo == "1" ? "Checked" : ""}>
                          <span class="slider round"></span>
                     </label>
                </td>
            </tr>

        `;
    })
    $("#tbOBloqueos").html(dtOBloqueos);
}

function GetOpcionesBloqueadas() {

    ejecutarajax("PanelBloqueos.aspx/ObtenerOpcionesBloqueadas", {}, CargarTablaOpcionesBloqueadas);
}

function AModalOpciones() {

    GetOpcionesBloqueadas();

}

$(document).on("change", ".checkOpciones", function (e) {

    let id = parseInt($(e.target).attr("data-id"));
    let estado;

    if ($(this).is(':checked')) {
        estado = 1;
    } else {
        estado = 0;
    }
    
    datos = {
        "id": id,
        "estado": estado
    }

    console.log(ejecutarajax("PanelBloqueos.aspx/ActualizarOpcionesBloqueadas", datos, MensajeGuardado2));
})

