

params = new URLSearchParams(window.location.search);
idAgenda = parseInt(params.get("idAgenda"));
idExamenSol = parseInt(params.get("idExamenSol"));




function cargarExamneSol() {
    $.ajax({
        url: "InformeExamenes.aspx/getExamenSol",
        dataType: "json",
        data: JSON.stringify({ "idExamensol": idExamenSol, "idAgenda": idAgenda }),
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            examenSol = JSON.parse(msg.d);
            cargarInformeExamen(examenSol[1], examenSol[0]);
            setTimeout(print, 1000)
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
}

function cargarInformeExamen( examen, examenSol) {
  

    var tbPregunta = `<ul>`

    for (var i = 0; i < examen.Preguntas.length; i++) {
        tbPregunta += `<li style="font-style:italic; font-weight: 700">${examen.Preguntas[i].StrPregunta} <br/><ol style="font-style: normal; font-weight: 500">`;
        var Respuesta;
        for (var j = 0; j < examenSol.Respuestas.length; j++) {
            if (examen.Preguntas[i].IntOidCPPREGUNTA == examenSol.Respuestas[j].IntOidCPPREGUNTA) {
                Respuesta = examenSol.Respuestas[j];
                break;
            }
        }

        for (var j = 0; j < examen.Preguntas[i].Opciones.length; j++) {
            if (Respuesta.IntOidCPOPCION == examen.Preguntas[i].Opciones[j].IntOidOPCION)
                tbPregunta += `<li>${examen.Preguntas[i].Opciones[j].StrOpcion}<i class="ml-3 fa ${examen.Preguntas[i].Opciones[j].IsCorrecta ? "fa-check text-success" : "fa-close text-danger"}" style></i></li>`
            else
                tbPregunta += `<li>${examen.Preguntas[i].Opciones[j].StrOpcion}</li>`
        }
        tbPregunta += `</ol></li>`
    }
    tbPregunta += "</ul>";

    var resultados = document.getElementById("resultados");
    resultados.innerHTML = tbPregunta;
}





