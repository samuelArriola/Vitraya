let params = new URLSearchParams(window.location.search);
let idAgenda = parseInt(params.get("idAgenda"));

function CargarParticipantes(msg) {
    let participantes = msg.d.Matriculas;
    let capacitacion = msg.d.Capacitacion;

    let idCapacitacion = capacitacion.IntOidCPCAPACITACION;

    $("#tbnEdtarMatricula").click(function (e) {
        window.location.href = `Matricula.aspx?idAgenda=${idAgenda}&idCapacitacion=${idCapacitacion}`;
    });


    $("#txtNomTema").text(capacitacion.StrTEMA)

    $("#tbParticipantes tbody").html("");
    participantes.forEach((participante, index) => {
        $("#tbParticipantes tbody").append(`
            <tr>
                <td width="75">
                    <div class="photo" style="background: url(${participante.Foto != "" ? `data:image/jpg;base64,${participante.Foto}` : "../Images/user.png"}); "></div> 
                </td>
                <td>
                    ${participante.Nombre}
                </td>
                <td class="text-left">
                    <div class="form-check form-switch">
                      <input class="form-check-input input-asistencia" data-asistencia="${participante.IdMatricula}" type="checkbox" ${participante.Asistido ? "checked":""}>
                    </div>
                </td>
            </tr>
        `);
    })

    DataTable("#tbParticipantes");
}

function GetMatriculas() {
    datos = {
        "idAgenda": idAgenda,
        "nombre": $("#txtSearch").val()
    }

    ejecutarajax("EjecutarCapa.aspx/GetMatriculas", datos, CargarParticipantes)
}

$("form").keypress(function (e) { if (e.keypress == 13) e.preventDefault() });
$("form").submit(function (e) { e.preventDefault() });


function CargarSubtemas(msg) {
    let subtemas = msg.d;
    console.log(subtemas);

    $("#tbSubtemas tbody").html("");

    subtemas.forEach(subtema => {
        $("#tbSubtemas tbody").append(`
            <tr>
                <td>${subtema.StrSUBTEMA}</td>
            </tr>
        `);
    });
}

function CargarArchivos(msg) {
    let archivos = msg.d;
    console.log(archivos)
    $("#tbArchivos tbody").html("");
    archivos.forEach(archivo => {
        $("#tbArchivos tbody").append(`
            <tr>
                <td>
                    <a href="${window.location.origin}/proceedings/Archivos.aspx?id=${archivo.IntOidGNArchivo}">${archivo.StrNombre}.${archivo.StrExt}</a>
                </td>
            </tr>
        `);
    });
}


$(document).on("change", ".input-asistencia", function (e) {
    let idMatricula = parseInt($(e.target).attr("data-asistencia"));
    ejecutarajax("EjecutarCapa.aspx/SetAsistencia", { "idMatricula": idMatricula, "asistido": this.checked }, e => { })
})


function GetArchivos() {
    ejecutarajax("EjecutarCapa.aspx/GetArchivos", { "idAgenda": idAgenda }, CargarArchivos);
} 

function GetSubtemas() {
    ejecutarajax("EjecutarCapa.aspx/GetSubtemas", { "idAgenda": idAgenda }, CargarSubtemas);
}

$("#btnFinalizar").click(function (e) {
    $("#modal-confirm").modal();
});

$("#bntFinalizarCapa").click(function (e) {
    ejecutarajax(
        "EjecutarCapa.aspx/FinalizarCapacitacion",
        { "idAgenda": idAgenda },
        function () {
            window.location.href = "AdministrarCapacitaciones.aspx";
        }
    )
})

$(document).ready(function () {
    GetMatriculas();

    $("#txtSearch").keypress(function (e) {
        if (e.keyCode == 13) {
            GetMatriculas();
        }
    });

    GetSubtemas();

    GetArchivos();
})

