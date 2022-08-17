﻿console.log('conetado a CSPacienteBB.js');

let ListaSalida = document.getElementById('CStableBB');
let templateListaSalida = document.getElementById('CStableBBTemplate').content;
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


//MOSTRAR INGRESO
$('#CSingresoBB').on("focusout", function () {
    let CSingresoBB = $('#CSingresoBB').val();
    if (CSingresoBB.length > 19) {
        return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 19");
    } else {
        if (isEmpy(CSingresoBB)) {
            console.log('Campo vacio');
            $('#CSInomsBB').val('');
            $('#CSIidenBB').val('');
        } else {
            ejecutarajax("CSPaciente.aspx/GetPaciente", { "Codigo": CSingresoBB }, getIngreBB)

        }
    }
    
})

function getIngreBB(res) {
    res = res.d;
    if (res.length < 1) {
        $('#CSInomsBB').val('');
        $('#CSIidenBB').val('');
        ListaSalida.innerHTML = "";
        return error("Notificacion", "Paciente no encontrado");
    }

    res.forEach((item) => {
        $('#CSInomsBB').val(item.PACPRINOM + " " + item.PACSEGNOM + " " + item.PACPRIAPE + " " + item.PACSEGAPE);
        $('#CSIidenBB').val(item.PACNUMDOC);
    });
    tablaMostrarSalidas($('#CSingresoBB').val())
   
}

//MOSTRAR ACUDIENTES EXISTENTES EN DINAMICA
$('#CSAidenBB').on("focusout", function () {
    let CSingresoBB = $('#CSAidenBB').val();
    if (CSingresoBB.length > 10) {
        return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 10");
    } else {
        if (isEmpy(CSingresoBB)) {
            console.log('Campo vacio' + CSingresoBB);
            $('#CSAtipoBB').val('');
            $('#CSAidenBB').val('');
        } else {
            ejecutarajax("CSPacienteBB.aspx/GetAcudienteBB", { "CSingresoBB": CSingresoBB }, getAcudienteBB)

        }
    }
})

function getAcudienteBB(res) {
    res = res.d;
    res.forEach((item) => {
        $('#CSAnomsBB').val(item.PACPRINOM + "" + item.PACSEGNOM + "" + item.PACPRIAPE + "" + item.PACSEGAPE);
    });
}


//REGISTRAR ACUDIENTE
$('#btnCSregistroBB').on("click", function () {
    let CSingresoBB = $('#CSingresoBB').val();
    let CSInomsBB = $('#CSInomsBB').val();
    let CSIidenBB = $('#CSIidenBB').val();
    let CSAidenBB = $('#CSAidenBB').val()
    let CSAtipoBB = $('#CSAtipoBB').val();
    let CSAnomsBB = $('#CSAnomsBB').val();
    let CSBnumeroRBB = $('#CSBnumeroRBB').val();
   
    const data = {
        CSingresoBB,
        CSInomsBB,
        CSIidenBB,
        CSAidenBB,
        CSAtipoBB,
        CSAnomsBB,
        CSBnumeroRBB
    }

    if (isEmpy(CSingresoBB) || isEmpy(CSInomsBB) || isEmpy(CSIidenBB) || isEmpy(CSAidenBB) || isEmpy(CSAtipoBB) || isEmpy(CSAnomsBB) ) {
        error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
    } else {
        ejecutarajax("CSPacienteBB.aspx/SetAcudienteBB", data, resRegistroAcu )
    } 
    
})

function resRegistroAcu() {
    exito("Notificacion", "Acudiente registrado")
    tablaMostrarSalidas($('#CSingresoBB').val())
}

function isEmpy(string) {
    if (string == "" || string == null) {
        return true
    }
}


function tablaMostrarSalidas(ingreso) {
    ejecutarajax("CSPacienteBB.aspx/GetsalidaBB", { ingreso }, tablaPintarSalida)
}

function tablaPintarSalida(res) {
    res = res.d;
    ListaSalida.innerHTML = "";

    res.forEach((item) => {
        templateListaSalida.querySelectorAll('td')[0].textContent = item.OID;
        templateListaSalida.querySelectorAll('td')[1].textContent = item.NOMPACIENTE;
        templateListaSalida.querySelectorAll('td')[2].textContent = item.DOCRESPONSABLE;
        templateListaSalida.querySelectorAll('td')[3].textContent = item.TPRESPONSABLE;
        templateListaSalida.querySelectorAll('td')[4].textContent = item.NOMRESPONSABLE;
        templateListaSalida.querySelectorAll('td')[5].textContent = item.NOMBB;
        templateListaSalida.querySelectorAll('td')[6].textContent = moment(item.FECHASS).format("DD/MM/YYYY HH:mm");
       
     
        templateListaSalida.querySelector('.btn-danger').dataset.id = item.OID;
      //  templateListaSalida.querySelector('.btn-success').dataset.id = item.OID;
        const clone = templateListaSalida.cloneNode(true);
        fragment.appendChild(clone);

    });

    ListaSalida.appendChild(fragment);
}

//BTN ELIMINAR Y ACTUALIZAR ACUDIENTE
ListaSalida.addEventListener("click", (e) => {
     
    if (e.target.classList.contains("btn-danger")) {
        data = { oid: e.target.dataset.id }
        ejecutarAjax("CSPacienteBB.aspx/DeleteAcuBB", data, DeleteAcuBB);
    }
    if (e.target.classList.contains("btn-success")) {
        data = { oid: e.target.dataset.id }
    }


})

function DeleteAcuBB() {
    tablaMostrarSalidas($('#CSingresoBB').val());
    exito("Notificacion", "Eliminado exitoso")
}