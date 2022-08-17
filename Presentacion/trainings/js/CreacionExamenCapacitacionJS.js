 

var NumPregunta = 0;


var textarea = document.querySelector('#NomExa'); 

textarea.addEventListener('keydown', autosize); 

//evento que redimenciona el textarea segun se precione una tecla;
function autosize() {
    var el = this;
    setTimeout(function () {
        el.style.cssText = 'height:auto; padding:0'; //se ajusta al tamaño del contenido
        el.style.cssText = 'height:' + (el.scrollHeight) + 'px'; //hace crecer el textarea dependiendo del contenido
    }, 0);
}


function CargarExamen(msg) {
    if (!msg.d) {
        return;
    }
    let examen = JSON.parse(msg.d);
    $("#NomExa").val(examen.StrNombre);
    examen.Preguntas.forEach(pregunta => {
        var divPregunta = document.createElement("div"); //se crea un nuevo div
        divPregunta.innerHTML = '' +
            '<div class="text-right">' +
            '   <i class="fa fa-close eliminarPregunta"></i>' +
            '</div>' +
            '<textarea class="NomPre" placeholder="Edite su Pregunta">' + pregunta.StrPregunta + '</textarea>' +
            '<ol>' +
            '</ol>';
        if(pregunta.Opciones.length > 2)
            divPregunta.innerHTML += '<i class="fa fa-plus agregarOpcion"></i><i class="fa fa-close eliminarOpcion mr-2" style="display:none">';
        divPregunta.className = "pregunta";
        opciones = divPregunta.querySelector("ol");
        if(pregunta.Opciones.length > 2 )
            pregunta.Opciones.forEach(opcion => {
                opciones.innerHTML += `
                    <li><input type="radio" name="Pregunta${NumPregunta}" ${opcion.IsCorrecta ? "checked":""} /><textarea class="NomOpc" placeholder="Edite su respuesta">${opcion.StrOpcion}</textarea></li>
                `;
            })
        else
            pregunta.Opciones.forEach(opcion => {
                opciones.innerHTML += `
                    <li><input type="radio" name="Pregunta${NumPregunta}" ${opcion.IsCorrecta ? "checked" : ""} /><textarea class="NomOpc" placeholder="Edite su respuesta" disabled>${opcion.StrOpcion}</textarea></li>
                `;
            })

        lienzo = document.getElementById("Lienzo");
        lienzo.appendChild(divPregunta);
        //agregarOpcion(divPregunta.querySelectorAll("i")[1]);
        //eliminarOpcion(divPregunta.querySelectorAll("i")[2]);
        NumPregunta++;
    });
}

function GetExamen() {
    let params = new URLSearchParams(window.location.search);
    let idCapacitacion = parseInt(params.get("idCapacitacion"));
    let contexto = parseInt(params.get("Contexto"));
    let idAgenda = parseInt(params.get("idAgenda"));
    ejecutarajax(
        "CreacionExamenCapacitacion.aspx/getExamenCapacitacion",
        { 'idCapacitacion': idCapacitacion, 'contexto': contexto, "idAgenda": idAgenda || 0 },
        CargarExamen
    )
}

//evento que agrega un etiqueta para la pregunta nueva
$("#ControlPreguntaOM").on("click", () => {
    var divPregunta = document.createElement("div"); //se crea un nuevo div

    //se le agrega el contenido de la pregunta al div
    divPregunta.innerHTML = '' +
        '<div class="text-right">' +
        '   <i class="fa fa-close eliminarPregunta"></i>' +
        '</div>' +
        '<textarea class="NomPre" placeholder="Edite su Pregunta"></textarea>' +
        '<ol>' +
        '   <li><input type="radio" name="Pregunta'+NumPregunta+'" /><textarea class="NomOpc" placeholder="Edite su respuesta"></textarea></li>' +
        '   <li><input type="radio" name="Pregunta' + NumPregunta +'" /><textarea class="NomOpc" placeholder="Edite su respuesta"></textarea></li>' +
        '   <li><input type="radio" name="Pregunta' + NumPregunta +'" /><textarea class="NomOpc" placeholder="Edite su respuesta"></textarea></li>' +
        '</ol>' +
        '<i class="fa fa-plus agregarOpcion"></i><i class="fa fa-close eliminarOpcion mr-2" style="display:none">';

    //le se le asigna la clase al div de la pregunta
    divPregunta.className = "pregunta";

    //se agrega el div al linzo del examen
    lienzo = document.getElementById("Lienzo");
    lienzo.appendChild(divPregunta);
    
   
    NumPregunta++;
});


//se llama la funcion que agrega los eventos para eliminar el div de la pregunta
$(document).on("click", ".eliminarPregunta",(e) => {
    document.getElementById("Lienzo").removeChild(e.target.parentElement.parentElement);
});

$("#ControlPreguntaVF").on("click", () => {
    var divPregunta = document.createElement("div"); //se crea un nuevo div

    //se le agrega el contenido de la pregunta al div
    divPregunta.innerHTML = '' +
        '<div class="text-right">' +
        '   <i class="fa fa-close eliminarPregunta"></i>' +
        '</div>' +
        '<textarea class="NomPre" placeholder="Edite su Pregunta"></textarea>' +
        '<ol>' +
        '<li><input type="radio" name="Pregunta' + NumPregunta + '" /><textarea class="NomOpc" disabled>Verdadero</textarea></li>' +
        '<li><input type="radio" name="Pregunta' + NumPregunta + '" /><textarea class="NomOpc" disabled>Falso</textarea></li>' +
        '</ol>';

    //le se le asigna la clase al div de la pregunta
    divPregunta.className = "pregunta";

    //se agrega el div al linzo del examen
    lienzo = document.getElementById("Lienzo");
    lienzo.appendChild(divPregunta);    
    NumPregunta++;
});



$(document).on("click", ".agregarOpcion", function (e) {
    divPregunta = e.target.parentElement;
    console.log(divPregunta);
    lista = divPregunta.childNodes[2];
    var li = document.createElement('li');
    li.innerHTML = '<input type="radio" name="' + lista.querySelector("input").name + '" /><textarea class="NomOpc" placeholder="Edite su respuesta"></textarea></li>'
    lista.appendChild(li);
    var numOpciones = lista.children.length;
    if (numOpciones == 4) {
        divPregunta.childNodes[4].style.display = "inline-block";
    }
    if (numOpciones == 5) {
        divPregunta.childNodes[3].style.display = "none";
    }
})

$(document).on("click", ".eliminarOpcion", function (e) {
    divPregunta = e.target.parentElement;

    lista = divPregunta.childNodes[2];

    lista.removeChild(lista.lastChild);

    var numOpciones = lista.children.length;
    if (numOpciones == 3) {
        divPregunta.childNodes[4].style.display = "none";
    }
    if (numOpciones == 4) {
        divPregunta.childNodes[3].style.display = "inline-block";
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

$("#BtnCrearExaCap").on("click", (e) => {
    e.preventDefault(); // se previene el evento 
    if (validarExamen())
        $("#pn-num-apr").modal();
})

$("#btnGuradarExamen").on("click", (e) => {

     numPorcentajeAprobacion = parseInt($("#txtPorcentajeAprobacion").val());
    if (numPorcentajeAprobacion == NaN) {
        error("", "Ingrese una nota minima de aprobacion antes de guardar");
        return;
    }
    if (numPorcentajeAprobacion < 60) {
        error("", "El valor ingresado no puede ser menor de 60");
        return;
    }
    if (numPorcentajeAprobacion > 100) {
        error("", "el valor ingresado no puede se mayor a 100")
        return;
    }
    crearExamen(numPorcentajeAprobacion)
})


function crearExamen(numAprovacion) {

    var divPreguntas = [...document.getElementsByClassName("pregunta")];

    var preguntas = [];

    for (var i = 0, m = divPreguntas.length; i < m; i++) {
        var nomPregunta = divPreguntas[i].querySelector(".NomPre").value;
        var ListOpciones = [...divPreguntas[i].querySelectorAll("li")]; 
        var opciones = [];
        for (var j = 0, n = ListOpciones.length; j < n; j++) {
            var opcion = ListOpciones[j].querySelector("textarea").value;
            var valor = ListOpciones[j].querySelector("input").checked;
            opciones.push({ "StrOpcion": opcion, "IsCorrecta": valor, "intOidOPCION": 0, "intOidCPPREGUNTA": 0 });
        }
        preguntas.push({ "StrPregunta": nomPregunta, "Opciones": opciones, "intOidCPPREGUNTA": 0, "intOidCPEXAMEN":0 });
    }

    var params = new URLSearchParams(window.location.search);
    var idCapacitacion = parseInt(params.get("idCapacitacion"));
    var contexto = parseInt(params.get("Contexto"));
    var idAgenda = parseInt(params.get("idAgenda"));

    var examen = {
        "StrNombre": document.getElementById("NomExa").value,
        "Preguntas": preguntas,
        "IntOidCPEXAMEN": 0,
        "IntOidInstancia": idCapacitacion,
        "IntNumApro": numAprovacion,
        "IntContexto":contexto,
    }

    $.ajax({
        url: "CreacionExamenCapacitacion.aspx/crearExamen", 
        data: JSON.stringify({ "examen": examen, "idAgenda": idAgenda || 0 }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            var params = new URLSearchParams(window.location.search);
            var idCapacitacion = parseInt(params.get("idCapacitacion"));
            var contexto = parseInt(params.get("Contexto"));
            if (contexto == 2) {
                window.location.href = "../GestionDocumental/Solicitudes.aspx";
            }
            else {
            	window.location.href = "AdministrarCapacitaciones.aspx"
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });

    
}

$(document).ready(function () {
    GetExamen();
});

$(document).on("keyup", ".NomPre", autosize);
$(document).on("keyup", ".NomOpc", autosize);