
var params = new URLSearchParams(window.location.search);
var idCapacitacion = parseInt(params.get("idAgenda"));

$.ajax({
    url: "ReponderExamenCapacitacion.aspx/getExamenCapacitacion", 
    dataType: "json",
    data: JSON.stringify({"idCapacitacion":idCapacitacion}),
    type: "POST",
    contentType: "application/json; charset=utf-8",
    success: function (msg) {
        
        examen = JSON.parse(msg.d);
        cargarExamen(examen)
    },
    error: function (result) {
        alert("ERROR " + result.status + ' ' + result.statusText);
    }
});

const validarExamen = () => {
    divPreguntas = [...document.getElementsByClassName("pregunta")];
    if (!divPreguntas.length) {
        error("No hay Preguntas", "No se encontrón ninguna pregunta, intente primero agregar una pregunta");
        return false;
    }
    if (document.getElementById("NomExa").value == "") {
        error("Error", "Por favor digite el título del examen");
        return false;
    }

    for (var i = 0, n = divPreguntas.length; i < n; i++) {
        var inputIsChecked = false;
        var inputs = [...divPreguntas[i].querySelectorAll("input")];
        for (var j = 0, m = inputs.length; j < m; j++) {
            if (inputs[j].checked) {
                inputIsChecked = true;
                break;
            }
        }
        var txtvacio = false;
        var texts = [...divPreguntas[i].querySelectorAll("textarea")];
        for (var j = 0, m = texts.length; j < m; j++) {
            if (texts[j].value == "") {
                txtvacio = true;
                break;
            }
        }

        if (!inputIsChecked || txtvacio) {
            error("Error", "Hay preguntas sin completar, por favor llene los datos y seleccione la opción correcta");
            divPreguntas[i].style.borderLeftColor = "#f35555";
            divPreguntas[i].addEventListener("mouseover", (e) => {
                divPreguntas[i].style.borderLeftColor = "#0368ac";
            })
            return false;
        }
    }

    return true;
}

function cargarExamen(examen) {
    $("#NomExa").text(examen.StrNombre);
    for (var i in examen.Preguntas) {
        let divPregunta = document.createElement("div");
        divPregunta.setAttribute("data-id", examen.Preguntas[i].IntOidCPPREGUNTA);
        divPregunta.className = "pregunta";
        let olOpciones = document.createElement("ol");
        for (var j in examen.Preguntas[i].Opciones) {
            let liOpcion = document.createElement("li");
            liOpcion.innerHTML = `<input type="radio" name="pregunta${examen.Preguntas[i].IntOidCPPREGUNTA}" data-idPregunta="${examen.Preguntas[i].IntOidCPPREGUNTA}" data-idOpcion="${examen.Preguntas[i].Opciones[j].IntOidOPCION}" />
                                  <textarea class="NomOpc" disabled>${examen.Preguntas[i].Opciones[j].StrOpcion}</textarea>`;
            olOpciones.appendChild(liOpcion);
        }
        let taNombrePregunta = document.createElement("textarea");
        taNombrePregunta.className = "NomPre";
        taNombrePregunta.setAttribute("disabled", "");
        taNombrePregunta.innerText = examen.Preguntas[i].StrPregunta;
        divPregunta.appendChild(taNombrePregunta);
        divPregunta.appendChild(olOpciones);
        document.getElementById("Lienzo").appendChild(divPregunta);
    }

    

    texts = document.getElementsByTagName("textarea");

    for (i = 0; i < texts.length; i++) {
        text = texts[i];
        text.style.height = 'auto';
        text.style.padding = "0";
        text.style.height = (text.scrollHeight) + 'px';
    }
    
}

$("#BtnReponderExa").on("click", (e) => {
    e.preventDefault();
    if (validarExamen())
        reponderExamen();
})

const reponderExamen = () => {
    iptOpciones = [...document.querySelectorAll(".pregunta ol input")]

    let respuestas = [];


    for (var i = 0, n = iptOpciones.length; i < n; i++) {
        if (iptOpciones[i].checked) {
            respuesta = {
                intOidCPPREGUNTA: parseInt(iptOpciones[i].getAttribute("data-idPregunta"), 10),
                intOidCPOPCION: parseInt(iptOpciones[i].getAttribute("data-idOpcion"),10),
                intOidCPEXAMENSOL: 0,
                intOidCPRESPUESTA: 0
            }
            respuestas.push(respuesta);
        }
    }
    let examenSol = {
        "intOidCPEXAMENSOL": 0,
        "intIDMATRICULA": 0,
        "intResultado": 0,
        "intOidPCEXAMEN": examen.IntOidCPEXAMEN,
        "Respuestas": respuestas
    }
   
    console.log(cargarExamenSol(examenSol));
}

const cargarExamenSol = (examen) => {
    return $.ajax({
        url: "ReponderExamenCapacitacion.aspx/responderExamenCapacitacion",
        dataType: "json",
        data: JSON.stringify({ "examenSol": examen, "idAgenda": idCapacitacion}),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            console.log(msg.d)
            var resultado = JSON.parse(msg.d);
            if (resultado.isAprobado) {
                $("#notificacion").html(`<h4>Examen Aprobado, Su nota es : ${resultado.nota} </h4>`);
                $("#btnReiniciar").hide();
                $("#mdlResultado").modal();
                setInterval(() => { window.location.href = "Userprincipal.aspx"}, 5000);
            }
            else {
                $("#notificacion").html(`<h4>Examen Reprobado, Su nota es : ${resultado.nota} </h4>`);
                $("#mdlResultado").modal()
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText + ' ' );
        }
    });
}
$("#btnCerrar").on("click", e => {
    e.preventDefault();
    window.location.href = "Userprincipal.aspx";
})