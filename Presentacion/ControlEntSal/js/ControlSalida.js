console.log('conetado a controlsalida.js');

let ListaSalida = document.getElementById('CStable');
let templateListaSalida = document.getElementById('CStableTemplate').content;
const fragment = document.createDocumentFragment();

const ejecutarAjax = (url, datos, success) => {
        $.ajax({
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

//ESCANER BOLETA SALIDA
$('#CScodigoR').on("keypress", function () {
    let CScodigoR = $('#CScodigoR').val();
        if (event.which === 13) {
            const data = {
                "Codigo": CScodigoR
            } 

            if (this.value.length >= 10) {
                return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 10");
            } else {
                if (isEmpy(data.Codigo)) {
                    console.log('Campo vacio');
                    $('#CSiden').val('');
                    $('#CSnombreR').val('');
                    $('#CSapell').val('');
                } else {
                   ejecutarajax("CSPaciente.aspx/GetPaciente", data , PacienteGet)
           
                }
            }
        }
    })

    function PacienteGet( res ) {
        var res = res.d;
        if (res.length < 1 ) {
            console.log('No se encontraron resultados' + res.length);
            $('#CSiden').val('');
            $('#CSnombreR').val('');
            $('#CSapell').val('');
            error("Notificacion", "Paciente no encontrado");
        } else {
            res.forEach((item) => {
                $('#CSiden').val(item.PACNUMDOC);
                $('#CSnombreR').val(item.PACPRINOM+""+item.PACSEGNOM);
                $('#CSapell').val(item.PACPRIAPE+""+item.PACSEGAPE);
            });
            $("#CSmanilla").focus();
        }

   
    }

    function isEmpy(string) {
        if (string == "" || string == null) {
            return true
        }
}

//ESCANER MANILLA
$('#CSmanilla').on("keypress", function () {
    let CSiden = $('#CSiden').val();
    let CSmanilla = $('#CSmanilla').val();

    if (event.which === 13) {
        const data = {
            "CScodigoR": $('#CScodigoR').val(),
            "CSiden": $('#CSiden').val(),
            "CSmanilla": CSmanilla
        }
 
        if (isEmpy(CSiden) || isEmpy(CSmanilla)) {
            error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
        }
        else
        {
            if (CSiden == CSmanilla) {
                ejecutarajax("CSPaciente.aspx/SalidaPaciente", data, SalidaPaciente)
            } else {
                ejecutarajax("CSPaciente.aspx/SPnoCoincide", data, modalRes('error'))
               
            }

        }
    }
})

function SalidaPaciente(res) {
    var res2 = res.d;
    if (res2 < 1) {
        modalRes('exito');
        $('#CSiden').val('');
        $('#CSnombreR').val('');
        $('#CSapell').val('');
        $('#CSmanilla').val('');
        $('#CScodigoR').val('');
    } else {
        error("Notificacion", "Ya el paciente ha salido");
    }
}


//ESCANER CEDULA DE ACUDIENTE
$('#MCScedulaBB ').on("keypress", function () {
    let MCScedulaBB = $('#MCScedulaBB').val();

    if (event.which === 13) {
        const data = {
            MCScedulaBB
        }

        if (this.value.length >= 15) {
            return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 15");
        } else {
            if (isEmpy(MCScedulaBB)) {
                console.log('Campo vacio');
                $('#MCSnombreBB').val('');
                $('#MCSacudienteBB').val('');
                $('#MCSnummBB').val('');
            } else {
                ejecutarajax("CSPaciente.aspx/MostrarAcoBB", data, MostrarAcuBB)

            }
        }
    }
})

function MostrarAcuBB(res) {
    res = res.d;
    console.log(res)
}


//Modal respuesta final 
function modalRes( tipo ) {
    if (tipo == "exito") {
        $('#exampleModalCenterAceptar').modal('show')
    } else if (tipo == "error") {
        $('#exampleModalCenterError').modal('show')
    } else {
        console.log("Tipo de modal no encontrado")
    }
}

$('#ModalSalBB').on('click', function () {
    $('#exampleModalBB').modal('show');
})

const modalSalBebe = document.getElementById('exampleModalBB');
modalSalBebe.addEventListener('shown.bs.modal', function () {
    ListaSalida.innerHTML = "";
    $('#CSAidenCCBB').val('');
    $('#CSACCNombreBB').val('');
    $('#CSAidenBB').focus();
})

//ESCANER CEDULA DE ACUDIENTE PARA BEBESS
$('#CSAidenBB').on("keypress", function () {
  
    if (event.which === 13) {
        let CSAidenBB = $('#CSAidenBB').val();
        const credenciales = CSAidenBB.split(',');
        const cc = credenciales[0].replace(/^(0+)/g, '');
        $('#CSAidenCCBB').val(cc);
        $('#CSACCNombreBB').val(credenciales[3] + " " + credenciales[1]);
        $('#CSAidenBB').val('');
        const CSAidenCCBB = $('#CSAidenCCBB').val();

        if (isEmpy(CSAidenCCBB)) {
            console.log('Campo vacio');
            ListaSalida.innerHTML = "";
        } else {
            tablaMostrarSalidas(CSAidenCCBB);
        }
        
    }
})
function tablaMostrarSalidas(ingreso) {
    ejecutarajax("CSPacienteBB.aspx/GetsalidaPorDocBB", { "doc": ingreso }, tablaPintarSalida)
}
function tablaPintarSalida(res) {
    res = res.d;

    if (res.length < 1) {
        $('#CSAidenCCBB').val('');
        $('#CSACCNombreBB').val('');
        ListaSalida.innerHTML = "";
        return error("Notificacion", "No se encontraron Resultados");
    }

    ListaSalida.innerHTML = "";

    res.forEach((item) => {
        templateListaSalida.querySelectorAll('td')[0].textContent = item.OID;
        templateListaSalida.querySelectorAll('td')[1].textContent = item.NOMPACIENTE;
        templateListaSalida.querySelectorAll('td')[2].textContent = item.DOCRESPONSABLE;
        templateListaSalida.querySelectorAll('td')[3].textContent = item.TPRESPONSABLE;
        templateListaSalida.querySelectorAll('td')[4].textContent = item.NOMRESPONSABLE;
        templateListaSalida.querySelectorAll('td')[5].textContent = item.NOMBB;
        templateListaSalida.querySelectorAll('td')[6].textContent = moment(item.FECHASS).format("DD/MM/YYYY HH:mm");


        templateListaSalida.querySelector('.btn-outline-success').dataset.id = item.OID;
        const clone = templateListaSalida.cloneNode(true);
        fragment.appendChild(clone);

    });

    ListaSalida.appendChild(fragment);
}

//BTN DAR SALIDA A ACUDIENTE BEBE
ListaSalida.addEventListener("click", (e) => {
    if (e.target.classList.contains("btn-outline-success")) {
        data = { oid: e.target.dataset.id }
        ejecutarAjax("CSPaciente.aspx/SetDarSalidaAcuBB", data, ResSetDarSalidaAcuBB);
    }
})

function ResSetDarSalidaAcuBB() {
   exito("Notificacion", "Salida exitosa")
    tablaMostrarSalidas($('#CSAidenCCBB').val());
}




