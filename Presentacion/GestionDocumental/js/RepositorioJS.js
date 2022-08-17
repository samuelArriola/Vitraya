let datas = [];

txtProceso = $("#txtProceso")
txtCodigo = $("#txtCodigo")
txtEstandar = $("#txtEstandar")
txtNombre = $("#txtNombre")
txtTipo = $("#txtTipo")
txtDireccion = $("#txtDireccion")
txtVersion = $("#txtVersion")
txtClasificacion = $("#txtClasificacion")
txtEstado = $("#txtEstado")
txtCambio = $("#txtCambio")
txtElaborador = $("#txtElaborador")
txtRevisor = $("#txtRevisor")
txtfechaAprobacion = $("#txtfechaAprobacion")
txtAprobador = $("#txtAprobador")
txtfechaEmision = $("#txtfechaEmision")
txtEnlace = $("#txtEnlace")
txtfechaActualizacion = $("#txtfechaActualizacion")
txtfechaResponsableActualizacion = $("#txtfechaResponsableActualizacion")
txtObservaciones = $("#txtObservaciones")

textProceso = $("#textProceso");
textCodigo = $("#textCodigo");
textNombre = $("#textNombre");
textFecha = $("#textFecha");



function ejecutarajax(url, datos, success) {

    $.ajax({
        url: url,
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success:success,
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    })
}

function CargarListadoMaestro(m) {
    datas = m.d;
    let table = "";
    datas.forEach((dato, index )=> {
        table += `
                <tr>
                    <td>${dato.Proceso}</td>
                    <td>${dato.Codigo}</td>
                    <td>${dato.Nombre}</td>
                    <td>${dato.FechaEmision}</td>
                    <td><i class="fa fa-eye btn-view" data-index="${index}"></i></td>
                    <td><a href="?Nombre=${dato.Nombre}"><i class="fa fa-cloud-download"></i></td>
                </tr>    
            `
    });
    $("#table").html(table)
}

$(document).on("click", ".btn-view", function (e) {
    index = parseInt($(this).attr("data-index"));
    data = datas[index];

    console.log(index);

    txtProceso.text(data.Proceso);
    txtCodigo.text(data.Codigo);
    txtEstandar.text(data.Estandar);
    txtNombre.text(data.Nombre);
    txtTipo.text(data.Tipo);
    txtDireccion.text(data.Direccion);
    txtVersion.text(data.Version);
    txtClasificacion.text(data.Clasificacion);
    txtEstado.text(data.Estado);
    txtCambio.text(data.Cambio);
    txtElaborador.text(data.Elaborador);
    txtRevisor.text(data.Revisor);
    txtfechaAprobacion.text(data.FechaAprobacion);
    txtAprobador.text(data.Aprobador);
    txtfechaEmision.text(data.FechaEmision);
    txtEnlace.text(data.Enlace);
    txtfechaActualizacion.text(data.FechaActualizacion);
    txtfechaResponsableActualizacion.text(data.ResponsableActualizacion);
    txtObservaciones.text(data.Observaciones);
    $(".modal").modal();
});

function GetListadoMaestro() {
    let datos = {
        nombre: textNombre.val(),
        codigo: textCodigo.val(),
        proceso : textProceso.val(),
        fecha : textFecha.val()
    }

    ejecutarajax("Repositorio.aspx/loadArchs", datos, CargarListadoMaestro)
}

GetListadoMaestro()

textNombre.on("keyup", GetListadoMaestro);
textProceso.on("keyup", GetListadoMaestro);
textCodigo.on("keyup", GetListadoMaestro);
textFecha.on("keyup", GetListadoMaestro);