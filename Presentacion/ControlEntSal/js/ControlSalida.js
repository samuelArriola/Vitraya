console.log('conetado a controlsalida.js');

let Sub_Grupos = [];
let camas = [];
let ListaSalida = document.getElementById('CStable');
let templateListaSalida = document.getElementById('CStableTemplate').content;
const fragment = document.createDocumentFragment();
const fragment2 = document.createDocumentFragment();

let CsvCenso = document.getElementById('CspResOption');
let CSVCensoTemplate = document.getElementById('CSVCensoTemplate').content;

let CSVcontent;
let CSVcontentTemplate = document.getElementById('CSVcontentTemplate').content;



//let CSVcontent = document.getElementById('CSVcontent');
//let CSVcontentTemplate = document.getElementById('CSVcontentTemplate').content;



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

            if (this.value.length >= 12) {
                return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 12");
            } else {
                if (isEmpy(data.Codigo)) {
                    console.log('Campo vacio');
                    $('#CSiden').val('');
                    $('#CSnombreR').val('');
                    $('#CSapell').val('');
                    $('#CSmanilla').val('');
                } else {
                   ejecutarajax("CSPaciente.aspx/GetPaciente", data , PacienteGet)
           
                }
            }
        }
    })

    function PacienteGet( res ) {
        var res = res.d;
        if (res.length < 1 ) {
            $('#CSiden').val('');
            $('#CSnombreR').val('');
            $('#CSapell').val('');
            $('#CSmanilla').val('');
            error("Notificacion", "Boleta de salida no encontrado");

        } else {
            $('#CSmanilla').val('');
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
                ejecutarajax("CSPaciente.aspx/SPnoCoincide", data, modalRes('error','La identificacion de la BOLETA no es igual al de la MANILLA' ))
               
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
        $('#MCSErrorH4').html("Ya el paciente ha salido");
        $('#exampleModalCenterError').modal('show');
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


//MODAL SALIDA CLINICA DEL ACUDIENTE 
function modalRes( tipo, msm ) {
    if (tipo == "exito") {
        $('#exampleModalCenterAceptar').modal('show')
    } else if (tipo == "error") {
        $('#MCSErrorH4').html(msm);
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
    $('#CSAidenBB').focus();
})

modalSalBebe.addEventListener('hidden.bs.modal', function () {
    ListaSalida.innerHTML = "";
    $('#CSAidenCCBB').val('');
    $('#CSACCNombreBB').val('');
    $("#CScodigoR").focus();
})

const modalSalBebeclose = document.getElementById('exampleModalCenterAceptar');
 modalSalBebeclose.addEventListener('hidden.bs.modal', function () {
    $("#CScodigoR").focus();
 })

const modalSalBebeErroclose = document.getElementById('exampleModalCenterError');
modalSalBebeErroclose.addEventListener('hidden.bs.modal', function () {
    ListaSalida.innerHTML = "";
    $('#CSiden').val('');
    $('#CSnombreR').val('');
    $('#CSapell').val('');
    $('#CSmanilla').val('');
    $('#CScodigoR').val('');
    $('#CScodigoR').focus();
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
        return $("#CStable").html(" <tr> <td colspan = '8'> <h5> UPss!! No hay resultados</h5> </td > </tr> ");
         
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
    $('#CSAidenBB').focus();
}

//--------------------------------------------  CONTROL  ENTRADA-SALIDA DE VISITANTES  -------------------------------------------------//

$(document).on("click", "div .option", function (e) {
    Sub_Grupos = [];
    CsvCenso.innerHTML = '';
    let index = $(this).attr("id");
    data = { Cod_grupo: index }
    ejecutarAjax("CSPaciente.aspx/GetCensoSubGrupos", data, ResGetCensoSubGrupos);
});


function ResGetCensoSubGrupos (res) {
    res = res.d;  
    res.forEach((item) => {

        Sub_Grupos[item.Cod_Subgrupo] = '1';
        CSVCensoTemplate.querySelector('h5').textContent = item.Nom_Subgrupo;
        CSVCensoTemplate.querySelectorAll('div')[2].classList.add('CSVcontent');
        const clone = CSVCensoTemplate.cloneNode(true);
        fragment.appendChild(clone);

      
        CSVcontent = document.querySelector('.CSVcontent');
        console.log(CSVcontent);

        ejecutarAjax("CSPaciente.aspx/GetCenso", { Cod_Subgrupo: item.Cod_Subgrupo }, (res2) => {

            res2 = res2.d;
            camas = [];

            res2.forEach((items) => {

                camas.push(items);
                CSVcontentTemplate.querySelector('span').textContent = items.Cod_Cama;
                const clone2 = CSVcontentTemplate.cloneNode(true);
                fragment2.appendChild(clone2);
            });
            Sub_Grupos[item.Cod_Subgrupo] = camas
            CSVcontent.appendChild(fragment2);
        });
        CsvCenso.appendChild(fragment);
    });

    console.log(Sub_Grupos);
}
