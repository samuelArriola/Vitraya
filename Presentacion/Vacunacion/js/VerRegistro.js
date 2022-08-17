
let index = 0;
const ejecutarAjax = (url, datos, success) => {
    return $.ajax({
        url: url,
        data: JSON.stringify(datos),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: success,
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}
let registros = [];
function cargarRegistros(msg) {
    registros = msg.d;
    dtRegistros = "";

    registros.forEach((registro, i) => {
        dtRegistros += `
            <tr>
                <td>${moment(registro.DtmFechaVacunacion).format("DD/MM/YYYY")}</td>
                <td>${registro.StrDocumento}</td>
                <td>${registro.StrNombres + " " + registro.StrPrimerApellido + " " + registro.StrSegundoApellido}</td>
                <td>${registro.StrBiologico}</td>
                <td>${registro.StrEtapaVacunacion}</td>
                <td>${registro.StrLugarRegistro}</td>
                <td><i class="fa fa-eye ver-registro" data-index=${i} data-toggle="tooltip" data-placement="top" title="Ver todo el registro"></i></td>
            </tr>
        `;
    });
    $("#tbRegistrosVacunacion tbody").html(dtRegistros);
    DataTable("#tbRegistrosVacunacion", 30);
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
    $("#loading-modal").modal("hide")
}

async function deleteRegistro() {
    if ($("#txtMotivo").val().trim().length > 8) {
        let idRegistro = registros[index].IntOidRegistroDiarioVac;
        let result = (await ejecutarAjax("VerRegistro.aspx/DeleteRegistro", { idRegistro: idRegistro, motivo: $("#txtMotivo").val() })).d
        $("#ConfirmDeleteModal").modal("hide");
        if (result)
            exito("Registro Eliminado", "El registro ha sido eliminado exitosamente");
        else
            error("Error al eliminar el regostro", "ha ocurrido un error mientras se eliminaba el registro");


        $("#txtMotivo").val("");
    }
    else {
        error("Campo motivo incompleto", "Por favor complete bien el campo motivo antes de eliminar el registro")
    }
}

function getRegistros() {
    $("#loading-modal").modal();
    datos = {
        fecha1: $("#dtFechaVacunacion1").val() || '1800-01-01',
        fecha2: $("#dtFechaVacunacion2").val() || '3000-01-01',
        documento: $("#txtDocumento").val(),
        nombre: $("#txtNombre").val(),
        biologico: $("#slcBiologico").val(),
        etapa: $("#slcEtapa").val(),
        cantidad: $("#slcCantidad").val(),
        lugar: $("#slcLugar").val()
    }

    ejecutarAjax("VerRegistro.aspx/GetRegistros", datos, cargarRegistros);
}

$(document).ready(function (e) {
    getRegistros();
})

$("#txtDocumento, #txtNombre").keypress(function (e) {
    if (e.keyCode == 13) getRegistros();
})

$("#dtFechaVacunacion1, #dtFechaVacunacion2, #slcBiologico, #slcEtapa, #slcLugar, #slcCantidad").change(function (e) {
    getRegistros();
})

$("#btnDescargar").click(function (e) {
    tabla = []
    let header = [
        { v: "consecutivo", t: "s" },
        { v: "Fecha de vacunación (dd/mm/aaaa)", t: "s" },
        { v: "T. Id", t: "s" },
        { v: "(4) N° IDENTIFICACIÓN", t: "s" },
        { v: "FECHA DE NACIMIENTO (dd/mm/aaaa)", t: "s" },
        { v: "DEPARTAMENTO DE NACIMIENTO", t: "s" },
        { v: "MUNICIPIO DE NACIMIENTO", t: "s" },
        { v: "EDAD", t: "s" },
        { v: "SEXO", t: "s" },
        { v: "PRIMER APELLIDO", t: "s" },
        { v: "SEGUNDO APELLIDO", t: "s" },
        { v: "NOMBRES", t: "s" },
        { v: "RÉGIMEN", t: "s" },
        { v: "ASEGURADORA", t: "s" },
        { v: "DEPTO RESIDENCIA", t: "s" },
        { v: "MUNICIPIO RESIDENCIA", t: "s" },
        { v: "ÁREA DE RESIDENCIA", t: "s" },
        { v: "BARRIO / CENTRO POBLADO O VEREDA DE RESIDENCIA", t: "s" },
        { v: "DIRECCIÓN DE RESIDENCIA", t: "s" },
        { v: "TELÉFONO", t: "s" },
        { v: "GRUPO ÉTNICO", t: "s" },
        { v: "CONDICIÓN DE DESPLAZAMIENTO", t: "s" },
        { v: "CONDICIÓN DE DISCAPACIDAD", t: "s" },
        { v: "CORREO ELECTRÓNICO", t: "s" },
        { v: "CONDICION DE USUARIA", t: "s" },
        { v: "FECHA PROBABLE DE PARTO (dd/mm/aaaa)", t: "s" },
        { v: "Tipo población", t: "s" },
        { v: "Dosis aplicada", t: "s" },
        { v: "Biologico", t: "s" },
        { v: "Lote Biologico", t: "s" },
        { v: "Jeringa", t: "s" },
        { v: "Lote Jeringa", t: "s" },
        { v: "NOMBRES Y APELLIDOS CUIDADOR MENOR", t: "s" },
        { v: "T. Id CUIDADOR MENOR", t: "s" },
        { v: "Asist(4) N° IDENTIFICACIÓN CUIDADOR MENOR", t: "s" },
        { v: "semanas de gestacion del menor", t: "s" },
        { v: "Peso del menor al nacer", t: "s" },
        { v: "Lugar atencion del parto del menor", t: "s" },
        { v: "Nombre lugar atencion del parto del menor", t: "s" },
        { v: "NOMBRE DEL VACUNADOR", t: "s" },
        { v: "Nombre IPS", t: "s" },
        { v: "ETAPA DE VACUNACIÓN", t: "s" },
        { v: "PARENTESCO CUIDADOR MENOR", t: "s" },
        { v: "RÉGIMEN CUIDADOR MENOR", t: "s" },
        { v: "ASEGURADORA CUIDADOR MENOR", t: "s" },
        { v: "TELÉFONO CUIDADOR MENOR", t: "s" },
        { v: "GRUPO ÉTNICO CUIDADOR MENOR", t: "s" },
        { v: "CONDICIÓN DE DESPLAZAMIENTO CUIDADOR MENOR", t: "s" },
        { v: "CONDICIÓN DE DISCAPACIDAD CUIDADOR MENOR", t: "s" },
        { v: "CORREO ELECTRÓNICO CUIDADOR MENOR", t: "s" }
    ];
    tabla.push(header);
    registros.forEach((registro, i) => {
        fila = [
            { v: i + 1, t: "s" },
            { v: moment(registro.DtmFechaVacunacion).format("DD/MM/YYYY"), t: "s" },
            { v: registro.StrTipoDocumento, t: "s" },
            { v: registro.StrDocumento, t: "s" },
            { v: moment(registro.DtmFechaNacimiento).format("DD/MM/YYYY"), t: "s" },
            { v: registro.StrDeptoNacimiento, t: "s" },
            { v: registro.StrMunicipioNacimiento, t: "s" },
            { v: registro.IntEdad, t: "s" },
            { v: registro.StrSexo, t: "s" },
            { v: registro.StrPrimerApellido, t: "s" },
            { v: registro.StrSegundoApellido, t: "s" },
            { v: registro.StrNombres, t: "s" },
            { v: registro.StrRegimen, t: "s" },
            { v: registro.StrEps, t: "s" },
            { v: registro.StrDeptoResidencia, t: "s" },
            { v: registro.StrMunicipioResidencia, t: "s" },
            { v: registro.StrAreaResidencia, t: "s" },
            { v: registro.StrBarrio, t: "s" },
            { v: registro.StrDireccion, t: "s" },
            { v: registro.StrTelefono, t: "s" },
            { v: registro.StrGrupoEtnico, t: "s" },
            { v: registro.StrDesplazamiento, t: "s" },
            { v: registro.StrDiscapacidad, t: "s" },
            { v: registro.StrEmail, t: "s" },
            { v: registro.StrGestacion, t: "s" },
            { v: registro.StrFechaProbableParto == null ? "" : moment(registro.StrFechaProbableParto).format("DD/MM/YYYY"), t: "s" },
            { v: registro.StrTipoPoblacion, t: "s" },
            { v: registro.StrDosis, t: "s" },
            { v: registro.StrBiologico, t: "s" },
            { v: registro.StrLote, t: "s" },
            { v: registro.StrJeringa, t: "s" },
            { v: registro.StrLoteJeringa, t: "s" },
            { v: registro.StrNombresAC, t: "s" },
            { v: registro.StrTipoDocumentoAC, t: "s" },
            { v: registro.StrDocumentoAC, t: "s" },
            { v: registro.StrSemanasMenor, t: "s" },
            { v: registro.StrPesoMenor, t: "s" },
            { v: registro.StrLugarParto, t: "s" },
            { v: registro.StrNombreLugarParto, t: "s" },
            { v: registro.StrNombreVacunador, t: "s" },
            { v: registro.StrNombreIps, t: "s" },
            { v: registro.StrEtapaVacunacion, t: "s" },
            { v: registro.StrParentescoAC, t: "s" },
            { v: registro.StrRegimenAC, t: "s" },
            { v: registro.StrEpsAC, t: "s" },
            { v: registro.StrTelefonoAC, t: "s" },
            { v: registro.StrGrupoEtnicoAC, t: "s" },
            { v: registro.StrDesplazamientoAC, t: "s" },
            { v: registro.StrDiscapacidadAC, t: "s" },
            { v: registro.StrEmailAC, t: "s" }
        ];
        tabla.push(fila);
    });
    tableExport = new TableExport(document.createElement("table"), {});
    tableExport.export2file(tabla, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Registro diario de vacunación", ".xlsx", [], false, "hoja 1")
})

$(document).on("click", ".ver-registro", function (e) {
    index = parseInt($(e.target).attr("data-index"));
    tbRegistro = `<table class="table-inf">
        <tr>
            <th class="text-center" colspan="2">Datos del paciente</th>
        </tr>
        <tr>
            <td>consecutivo</td>
            <td>${index + 1}</td>
        <tr>
            <td>Fecha de vacunación (dd/mm/aaaa)</td>
            <td>${moment(registros[index].DtmFechaVacunacion).format("DD/MM/YYYY")}</td>
        </tr>
        <tr>
            <td>T. Id</td>
            <td>${registros[index].StrTipoDocumento}</td>
        </tr>
        <tr>
            <td>(4) N° IDENTIFICACIÓN</td>
            <td>${registros[index].StrDocumento}</td>
        </tr>
        <tr>
            <td>FECHA DE NACIMIENTO (dd/mm/aaaa)</td>
            <td>${moment(registros[index].DtmFechaNacimiento).format("DD/MM/YYYY")}</td>
        </tr>
        <tr>
            <td>DEPARTAMENTO DE NACIMIENTO</td>
            <td>${registros[index].StrDeptoNacimiento}</td>
        </tr>
        <tr>
            <td>MUNICIPIO DE NACIMIENTO</td>
            <td>${registros[index].StrMunicipioNacimiento}</td>
        </tr>
        <tr>
            <td>EDAD</td>
            <td>${registros[index].IntEdad}</td>
        </tr>
        <tr>
            <td>SEXO</td>
            <td>${registros[index].StrSexo}</td>
        </tr>
        <tr>
            <td>PRIMER APELLIDO</td>
            <td>${registros[index].StrPrimerApellido}</td>
        </tr>
        <tr>
            <td>SEGUNDO APELLIDO</td>
            <td>${registros[index].StrSegundoApellido}</td>
        </tr>
        <tr>
            <td>NOMBRES</td>
            <td>${registros[index].StrNombres}</td>
        </tr>
        <tr>
            <td>RÉGIMEN</td>
            <td>${registros[index].StrRegimen}</td>
        </tr>
        <tr>
            <td>ASEGURADORA</td>
            <td>${registros[index].StrEps}</td>
        </tr>
        <tr>
            <td>DEPTO RESIDENCIA</td>
            <td>${registros[index].StrDeptoResidencia}</td>
        </tr>
        <tr>
            <td>MUNICIPIO RESIDENCIA</td>
            <td>${registros[index].StrMunicipioResidencia}</td>
        </tr>
        <tr>
            <td>ÁREA DE RESIDENCIA</td>
            <td>${registros[index].StrAreaResidencia}</td>
        </tr>
        <tr>
            <td>BARRIO / CENTRO POBLADO O VEREDA DE RESIDENCIA</td>
            <td>${registros[index].StrBarrio}</td>
        </tr>
        <tr>
            <td>DIRECCIÓN DE RESIDENCIA</td>
            <td>${registros[index].StrDireccion}</td>
        </tr>
        <tr>
            <td>TELÉFONO</td>
            <td>${registros[index].StrTelefono}</td>
        </tr>
        <tr>
            <td>GRUPO ÉTNICO</td>
            <td>${registros[index].StrGrupoEtnico}</td>
        </tr>
        <tr>
            <td>CONDICIÓN DE DESPLAZAMIENTO</td>
            <td>${registros[index].StrDesplazamiento}</td>
        </tr>
        <tr>
            <td>CONDICIÓN DE DISCAPACIDAD</td>
            <td>${registros[index].StrDiscapacidad}</td>
        </tr>
        <tr>
            <td>CORREO ELECTRÓNICO</td>
            <td>${registros[index].StrEmail}</td>
        </tr>
        <tr>
            <td>CONDICION DE USUARIA</td>
            <td>${registros[index].StrGestacion}</td>
        </tr>
        <tr>
            <td>FECHA PROBABLE DE PARTO (dd/mm/aaaa)</td>
            <td>${moment(registros[index].StrFechaProbableParto).format("DD/MM/YYYY")}</td>
        </tr>
        <tr>
            <td>ETAPA DE VACUNACIÓN</td>
            <td>${registros[index].StrEtapaVacunacion}</td>
        </tr>
        <tr>
            <td>Tipo población</td>
            <td>${registros[index].StrTipoPoblacion}</td>
        </tr>
        <tr>
            <td>Dosis aplicada</td>
            <td>${registros[index].StrDosis}</td>
        </tr>
        <tr>
            <td>Biologico</td>
            <td>${registros[index].StrBiologico}</td>
        </tr>
        <tr>
            <td>Lote Biologico</td>
            <td>${registros[index].StrLote}</td>
        </tr>
        <tr>
            <td>Jeringa</td>
            <td>${registros[index].StrJeringa}</td>
        </tr>
        <tr>
            <td>Lote Jeringa</td>
            <td>${registros[index].StrLoteJeringa}</td>
        </tr>
        <tr>
            <td>NOMBRE DEL VACUNADOR</td>
            <td>${registros[index].StrNombreVacunador}</td>
        </tr>
        <tr>
            <td>Nombre IPS</td>
            <td>${registros[index].StrNombreIps}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Datos del paciente en caso de ser menor de edad</th>
        </tr>
        <tr>
            <td>SEMANAS DE GESTACIÓN DEL MENOR</td>
            <td>${registros[index].StrSemanasMenor}</td>
        </tr>
        <tr>
            <td>PESO DEL MENOR AL NACER</td>
            <td>${registros[index].StrPesoMenor}</td>
        </tr>
        <tr>
            <td>LUGAR ATENCIÓN DEL PARTO DEL MENOR</td>
            <td>${registros[index].StrLugarParto}</td>
        </tr>
        <tr>
            <td>NOMBRE LUGAR ATENCIÓN DEL PARTO DEL MENOR</td>
            <td>${registros[index].StrNombreLugarParto}</td>
        </tr>
        <tr>
            <th class="text-center" colspan="2">Datos del asistente del menor de edad</th>
        </tr>
        <tr>
            <td>T. Id</td>
            <td>${registros[index].StrTipoDocumentoAC}</td>
        </tr>
        <tr>
            <td>Asist(4) N° IDENTIFICACIÓN</td>
            <td>${registros[index].StrDocumentoAC}</td>
        </tr>
            <td>PARENTESCO</td>
            <td>${registros[index].StrParentescoAC}</td>
        </tr>
        <tr>
            <td>NOMBRES Y APELLIDOS</td>
            <td>${registros[index].StrNombresAC}</td>
        </tr>
        <tr>
            <td>RÉGIMEN</td>
            <td>${registros[index].StrRegimenAC}</td>
        </tr>
        <tr>
            <td>ASEGURADORA</td>
            <td>${registros[index].StrEpsAC}</td>
        </tr>
        <tr>
            <td>TELÉFONO</td>
            <td>${registros[index].StrTelefonoAC}</td>
        </tr>
        <tr>
            <td>GRUPO ÉTNICO</td>
            <td>${registros[index].StrGrupoEtnicoAC}</td>
        </tr>
        <tr>
            <td>CONDICIÓN DE DESPLAZAMIENTO</td>
            <td>${registros[index].StrDesplazamientoAC}</td>
        </tr>
        <tr>
            <td>CONDICIÓN DE DISCAPACIDAD</td>
            <td>${registros[index].StrDiscapacidadAC}</td>
        </tr>
        <tr>
            <td>CORREO ELECTRÓNICO</td>
            <td>${registros[index].StrEmailAC}</td>
        </tr>
    </table>   
    `;
    $("#modalBody1").html(tbRegistro);
    $("#modal1").modal();
})

$("#bntEditar").click(function () { window.location.href = "EditarRegistro.aspx?idRegistro=" + registros[index].IntOidRegistroDiarioVac });

$("#btnEliminar").click(function (e) {
    $("#modal1").modal("hide");
    $("#ConfirmDeleteModal").modal();
});

$("#btnDeleteModal").click(async function (e) {
    await deleteRegistro();
})
