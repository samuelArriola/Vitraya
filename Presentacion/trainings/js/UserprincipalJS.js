let agendas = []
let agenda;

function CargarAgenda(msg) {
    agendas = msg.d
    console.log(agendas);
    $("#tbAgenda tbody").html("")
    agendas.forEach((agenda, i) => {
        $("#tbAgenda tbody").append(`
            <tr>
                <td>
                    ${
                        agenda.Asistido && agenda.Firmado == false ? agenda.Examen > 0 ?
                        `<span class="btn btn-info btn-firmar" data-idagenda="${agenda.IdAgenda}" ><img data-idagenda="${agenda.IdAgenda}" src="../Images/firma-con-boligrafo.svg" width="25" /></span>` :
                        `<a href="ReponderExamenCapacitacion.aspx?idAgenda=${agenda.IdAgenda}"><span class="btn btn-info"><img src="../Images/examen.svg" width="25" /></span></a>` : ""
                    }
                </td>
                <td data-index="${i}" class="btn-get-agenda">${agenda.StrTEMA}</td>
            </tr>
        `);
    });
    if (agendas) {
        GetInfoAgenda(0);
        agenda = agendas[0];
    }
    DataTable("#tbAgenda");
}


$(document).on("click", ".btn-firmar", function (e) {
    let idAgenda = parseInt($(e.target).attr("data-idagenda"));
    ejecutarajax("Userprincipal.aspx/FirmarAsistencia", { "idAgenda": idAgenda }, function () {
        GetAgendas()
        exito("¡Hecho!", "Se firmo la asistencia a la capacitación")
    });
});

function CargarInfoAgenda(msg) {
    let Subtemas = msg.d.Subtemas;
    let Archivos = msg.d.Archivos;
    let Examenes = msg.d.Examenes;

    $("#info-agenda").html("");

    if (agenda) {
        $("#info-agenda").append(`
            <div class="x_panel">
                <div class="x_content">
                    <h2 class="text-center">${agenda.StrTEMA}</h2>
                    ${
                        agenda.LinkAnexo.trim() != "" ?
                        `
                            <a href="${agenda.LinkAnexo}"><button type="button"><i class="fa  fa-youtube-play fa-2x"></i></button></a>
                        `:""
                      }
                </div>
            </div>
        `);
    }
    

    if (Examenes.length > 0) {
        let divExamenes = document.createElement("div");
        divExamenes.className = "x_panel";
        divExamenes.innerHTML = `
            <div class="x_title">
                <div class="clearfix">
                    <h6>Mis Examenes</h6>
                </div>
            </div>
            <div class="x_content">
                <table class="tbMenu">
                    <thead>
                        <tr>
                            <th colspan="2">Examenes realizados</th>
                        </tr>
                    </thead>
                    <tbody>
        
                    </tbody>
                <table>
            </div>
        `
        let tbodyExa = divExamenes.querySelector("tbody");

        Examenes.forEach(examen => {
            $(tbodyExa).append(`
                <tr>
                    <td>${examen.StrTitulo}</td>
                    <td>
                        <a href="InformeExamenes.aspx?idAgenda=${agenda.IdAgenda}&idExamenSol=${examen.IntOidCPEXAMENSOL}" target="arch"><i class="fa fa-eye"></i></a>
                    </td>
                </tr>
            `)
        });
        $("#info-agenda").append(divExamenes);
    }

    if (Subtemas.length > 0) {

        let divSubtemas = document.createElement("div");
        divSubtemas.className = "x_panel";
        divSubtemas.innerHTML = `
            <div class="x_title">
                <div class="clearfix">
                    <h6>Subtemas</h6>
                </div>
            </div>
            <div class="x_content">
                <table class="tbMenu">
                    <thead>
                        <tr>
                            <th>Listado de subtemas</th>
                        </tr>
                    </thead>
                    <tbody>
                        
                    </tbody>
                <table>
            </div>
        `
        let tbodySubtemas = divSubtemas.querySelector("tbody")
        Subtemas.forEach(subtema => $(tbodySubtemas).append(
            `
                <tr>
                    <td>${subtema.StrSUBTEMA}</td>
                </tr>
            `
        ));

        $("#info-agenda").append(divSubtemas);
    }

    if (Archivos.length > 0) {
        let divArchivos = document.createElement("div");
        divArchivos.className = "x_panel";
        divArchivos.innerHTML = `
            <div class="x_title">
                <div class="clearfix">
                    <h6>Archivos</h6>
                </div>
            </div>
            <div class="x_content">
                <table class="tbMenu">
                    <thead>
                        <tr>
                            <th>Listado de Archivos</th>
                        </tr>
                    </thead>
                    <tbody>

                    </tbody>
                <table>
            </div>
        `
        tbodyAchivos = divArchivos.querySelector("tbody");

        Archivos.forEach(archivo => {
            $(tbodyAchivos).append(`
                <tr> 
                    <td><a href="${window.location.origin}/proceedings/Archivos.aspx?id=${archivo.IntOidGNArchivo}">${archivo.StrNombre}.${archivo.StrExt}</a></td>
                </tr>
            `);
        });

        $("#info-agenda").append(divArchivos);
    }
}

function GetInfoAgenda(index) {
    if (agendas[index]) {
        ejecutarajax("Userprincipal.aspx/GetInfoAgenda", { 'idAgenda': agendas[index].IdAgenda, "idListaArchivos": agendas[index].IdListaArchivos }, CargarInfoAgenda)
    }
     else
        $("#info-agenda").html("");
}


function GetAgendas() {
    let datos = {
        "estado": $("input[name=estado]:checked").val(),
        "tema": $("#txtSearch").val()
    };
    
    ejecutarajax("Userprincipal.aspx/getCapacitaionesUsuario", datos, CargarAgenda)
}

$(document).ready(function () {
    GetAgendas();
});

$("input[name=estado]").change(function (e) {
    e.preventDefault();
    if (e.target.checked) {
        GetAgendas();
    }
});

$(document).on("click", ".btn-get-agenda", function (e) {
    let index = parseInt($(e.target).attr("data-index"));
    GetInfoAgenda(index);
    agenda = agendas[index];
});