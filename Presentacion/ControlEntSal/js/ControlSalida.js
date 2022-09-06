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

            if (this.value.length >= 12) {
                return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 12");
            } else {
                if (isEmpy(data.Codigo)) {
                    console.log('Campo vacio');
                    CSPlimiar();
                } else {
                   ejecutarajax("CSPaciente.aspx/GetPaciente", data , PacienteGet)
           
                }
            }
        }
    })

    function PacienteGet( res ) {
        var res = res.d;
        if (res.length < 1 ) {
             CSPlimiar();
            error("Notificacion", "Boleta de salida no encontrado");

        } else {
            $('#CSmanilla').val('');
            res.forEach((item) => {
                $('#CSiden').val(item.PACNUMDOC);
                $('#CSnombreR').val(item.PACPRINOM+" "+item.PACSEGNOM);
                $('#CSapell').val(item.PACPRIAPE+" "+item.PACSEGAPE);
                $('#CSedad').val(item.PACEDAD);
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
    let CSedad = $('#CSedad').val();

    if (event.which === 13) {
        const data = {
            "CScodigoR": $('#CScodigoR').val(),
            "CSiden": $('#CSiden').val(),
            "CSmanilla": CSmanilla
        }
 
        if ( isEmpy(CSiden) || isEmpy(CSmanilla) || isEmpy(CSedad) ) {
            error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
        } else
        {
            if (CSiden == CSmanilla) {

                if (CSedad < 18) {
                    $('#exampleModalBB').modal('show');
                } else {
                    console.log(CSedad)
                    ejecutarajax("CSPaciente.aspx/SalidaPaciente", data, SalidaPaciente)
                }

            } else {
                ejecutarajax("CSPaciente.aspx/SPnoCoincide", data, modalRes('error','La identificacion de la BOLETA no es igual al de la MANILLA' ))
               
            }

        }
    }
})

function SalidaPaciente(res) {
    var res2 = res.d;
    if (res2 < 1) {
        modalRes('exito', 'La salida ha sido exitosa.');
        CSPlimiar();
      
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
        $('#MCSExitoH4').html(msm);
        $('#exampleModalCenterAceptar').modal('show')
    } else if (tipo == "error") {
        $('#MCSErrorH4').html(msm);
        $('#exampleModalCenterError').modal('show')
    } else {
        console.log("Tipo de modal no encontrado")
    }
}

function CSPlimiar() {
    $('#CSiden').val('');
    $('#CSnombreR').val('');
    $('#CSapell').val('');
    $('#CSedad').val('');
    $('#CSmanilla').val('');
    $('#CScodigoR').val('');
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
    CSPlimiar();
    $("#CScodigoR").focus();
})

const modalSalBebeclose = document.getElementById('exampleModalCenterAceptar');
 modalSalBebeclose.addEventListener('hidden.bs.modal', function () {
    $("#CScodigoR").focus();
 })

const modalSalBebeErroclose = document.getElementById('exampleModalCenterError');
modalSalBebeErroclose.addEventListener('hidden.bs.modal', function () {
    ListaSalida.innerHTML = "";
    CSPlimiar();
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

let CSVtableCensoBody = document.getElementById('CSVtableCensoBody');
let CSVtableCensoBodyTemplate = document.getElementById('CSVtableCensoBodyTemplate').content;
const fragment2 = document.createDocumentFragment();
var fila;
CSVmostrarCenso($('#CSVFbuscar').val(), $('#CSVFGrupo').val(), $('#CSVFSubGrupo').val() );


//MODAL DE RESPUESTAS
function modalResVisita(tipo, msm) {
    if (tipo == "exito") {
        $('#MCSExitoH42').html(msm);
        $('#MCVExito').modal('show')
    } else if (tipo == "error") {
        $('#MCSErrorH42').html(msm);
        $('#MCVError').modal('show')
    } else {
        console.log("Tipo de modal no encontrado")
    }
}



$('#CSVFbuscar').on("keyup", function () {
    let CSVFbuscar = $('#CSVFbuscar').val();
    let CSVFGrupo = $('#CSVFGrupo').val();
    let CSVFSubGrupo = $('#CSVFSubGrupo').val();
    CSVmostrarCenso(CSVFbuscar, CSVFGrupo, CSVFSubGrupo)
})


$("#CSVFGrupo").change( function () {
    let CSVFbuscar = $('#CSVFbuscar').val();
    let CSVFGrupo = $('#CSVFGrupo').val();
    let CSVFSubGrupo = $('#CSVFSubGrupo').val();
    CSVmostrarCenso(CSVFbuscar, CSVFGrupo, CSVFSubGrupo)
    
});

$('#CSVFSubGrupo').on("click", function () {
    let CSVFGrupo = $('#CSVFGrupo').val();
    ejecutarajax("CSPaciente.aspx/GetCensoSubGrupos", { Cod_grupo: CSVFGrupo }, res => {
        res = res.d;
        $('#CSVFSubGrupo').empty();
        $('#CSVFSubGrupo').append('<option value="" selected="selected">Seleccione</option>');
        res.forEach((item) => {
            $('#CSVFSubGrupo').append($("<option>", {
                value: item.Cod_Subgrupo,
                text: item.Nom_Subgrupo
            }));
        });
    });
  
})

$("#CSVFSubGrupo").change(function () {
    let CSVFbuscar = $('#CSVFbuscar').val();
    let CSVFGrupo = $('#CSVFGrupo').val();
    let CSVFSubGrupo = $('#CSVFSubGrupo').val();
    CSVmostrarCenso(CSVFbuscar, CSVFGrupo, CSVFSubGrupo)

});

$('#CSVFlimpiar').on("click", function () {
    $('#CSVFbuscar').val('');
    $('#CSVFGrupo').val('');
    $('#CSVFSubGrupo').val('');
    CSVmostrarCenso($('#CSVFbuscar').val(), $('#CSVFGrupo').val(), $('#CSVFSubGrupo').val());
})


    function CSVmostrarCenso(buscar, grupo, subgrupo) {
        const data = {
            buscar,
            grupo,
            subgrupo
        } 
        
        ejecutarajax("CSPaciente.aspx/GetCenso", data, ResCSVmostrarCenso)
    }

    function ResCSVmostrarCenso(res) {
        res = res.d;
        if (res.length < 1) {
            return $("#CSVtableCensoBody").html(" <tr> <td colspan = '8'> <h5> UPss!! No hay resultados</h5> </td > </tr> ");

        }

        CSVtableCensoBody.innerHTML = "";
        $('.spinner-border').hide();
        res.forEach((item) => {

            CSVtableCensoBodyTemplate.querySelectorAll('td')[4].classList.add('tdFocus');
            CSVtableCensoBodyTemplate.querySelector('tr').classList.add('CStableItem');
            CSVtableCensoBodyTemplate.querySelectorAll('td')[0].textContent = item.Nom_Grupo;
            CSVtableCensoBodyTemplate.querySelectorAll('td')[1].textContent = item.Nom_Subgrupo;
            CSVtableCensoBodyTemplate.querySelectorAll('td')[2].textContent = item.Documento;
            CSVtableCensoBodyTemplate.querySelectorAll('td')[3].textContent = item.NOM_PAC;
            CSVtableCensoBodyTemplate.querySelectorAll('td')[4].textContent = item.VISITA;
            CSVtableCensoBodyTemplate.querySelectorAll('td')[5].textContent = item.Ingreso;
           
            CSVtableCensoBodyTemplate.querySelectorAll('td')[6].textContent = item.Cod_Cama;
       
            const clone = CSVtableCensoBodyTemplate.cloneNode(true);
            fragment2.appendChild(clone);
            
        });

        CSVtableCensoBody.appendChild(fragment2);
        DataTable("#CSVtableCenso", 10);

    }


//REGISTRAR DATOS DEL PACIENTE         
$(document).on("click", ".CStableItem", function () {
    fila = $(this).closest("tr");
   
    let iden = fila.find('td:eq(2)').text();
    let nombre = fila.find('td:eq(3)').text();
    let visita = fila.find('td:eq(4)').text();
    let ingreso = fila.find('td:eq(5)').text();
    let cod_cama = fila.find('td:eq(6)').text();

    if (visita == 'NO') {
        $('#CSVPingreso').val(ingreso);
        $('#CSVPiden').val(iden);
        $('#CSVPnom').val(nombre);
        $('#CSVPcodCama').val(cod_cama);
        $("#CSVViden").focus();
    } else { 
        $('#MCVCambioVisita').modal('show');
    }
 
});

//ESCANER CEDULA DE ACUDIENTE PARA BEBESS
$('#CSVViden').on("keypress", function () {

    if (event.which === 13) {
        let CSVViden = $('#CSVViden').val();
        const credenciales = CSVViden.split(',');
        const cc = credenciales[0].replace(/^(0+)/g, '');

        $('#CSVViden').val(cc);
        $('#CSVVnom').val(credenciales[3] + " " + credenciales[1] + " " + credenciales[2]);



        var ADNINGRES1 =  $('#CSVPingreso').val();
        var DocPaciente = $('#CSVPiden').val();
        var NomPaciente =  $('#CSVPnom').val();
        var Cod_cama = $('#CSVPcodCama').val();
        var DocResponsable = $('#CSVViden').val();
        var NombreRes = $('#CSVVnom').val();
     
        if (isEmpy(ADNINGRES1) || isEmpy(DocPaciente) || isEmpy(Cod_cama) || isEmpy(DocResponsable) || isEmpy(NombreRes)  ) {
            error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
        } else {
            const data = {
                ADNINGRES1,
                DocPaciente,
                NomPaciente,
                Cod_cama,
                DocResponsable,
                NombreRes
            }
            ejecutarajax("CSPaciente.aspx/SetInserVisita", data, ResSetInserVisita)
           

        }

        

    }
})

function ResSetInserVisita(res) {
    res = res.d;
    console.log(res);
    if (res < 1) {
        //paciente registrado
        modalResVisita('exito', 'Visita Registrada');
        CSVmostrarCenso($('#CSVFbuscar').val(), $('#CSVFGrupo').val(), $('#CSVFSubGrupo').val());
    } else {
        modalResVisita('error', 'Paciente Con Visita');
    }

}

const ModalError = document.getElementById('MCVError');
ModalError.addEventListener('hidden.bs.modal', function () {
    SCVlimpiar();
})

const ModalExito = document.getElementById('MCVExito');
ModalExito.addEventListener('hidden.bs.modal', function () {
    SCVlimpiar();
})

//BTN LIPIAR CAMPOS 
$('#CSVbtnLimpiar').on("click", function () {
    SCVlimpiar();
})

function SCVlimpiar() {
    $('#CSVPingreso').val('');
    $('#CSVPiden').val('');
    $('#CSVPnom').val('');
    $('#CSVPcodCama').val('');
    $('#CSVViden').val('');
    $('#CSVVnom').val('');
} 

function visto(ADNINGRES1) {
    ejecutarajax("CSPaciente.aspx/getVerVisita", { ADNINGRES1 } , res => {
        console.log(res.d)
    })

}