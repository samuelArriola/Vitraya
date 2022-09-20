console.log('conetado a CSPacienteBB.js');

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
            $('#CSIedadBB').val('');
            ListaSalida.innerHTML = "";
        } else {
            ejecutarajax("CSPacienteBB.aspx/GetCountPacienteSalida", { ingreso: CSingresoBB }, ResGetCountPacienteSalida)

        }
    }
    
})

function ResGetCountPacienteSalida(res) {
    let CSingresoBB = $('#CSingresoBB').val();
    var res2 = res.d;
    console.log(res2)

    if (res2 < 1) {
        ejecutarajax("CSPacienteBB.aspx/GetPacientesIngreso", { "Codigo": CSingresoBB }, getIngreBB)
    } else {
        $('#CSInomsBB').val('');
        $('#CSIidenBB').val('');
        $('#CSIedadBB').val('');
        ListaSalida.innerHTML = "";
        error('Notificacion','Ya el paciente ha salido')
    }

}




function getIngreBB(res) {
    res = res.d;
    if (res.length < 1) {
        $('#CSInomsBB').val('');
        $('#CSIidenBB').val('');
        $('#CSIedadBB').val('');
        ListaSalida.innerHTML = "";
        return error("Notificacion", "Paciente no encontrado");
    }

    res.forEach((item) => {
        $('#CSInomsBB').val(item.PACPRINOM);
        $('#CSIidenBB').val(item.PACNUMDOC);
        $('#CSIedadBB').val(item.EDAD);
    });
    tablaMostrarSalidas($('#CSingresoBB').val())
   
}

//MOSTRAR ACUDIENTES EXISTENTES EN DINAMICA
$('#CSAidenBB').on("focusout", function () {
    let CSingresoBB = $('#CSAidenBB').val();
    if (CSingresoBB.length > 12) {
        return error("Notificacion", "Ha sobrepasado el limite de  caracteres; max 12");
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
    if (res.length < 1) {
        $('#CSAnomsBB').val('');
        $('#CSAtipoBB').val('');
    } else {
        res.forEach((item) => {
            $('#CSAnomsBB').val(item.PACPRINOM + " " + item.PACSEGNOM + " " + item.PACPRIAPE + " " + item.PACSEGAPE);
        });
    }
}


//REGISTRAR ACUDIENTE
$('#btnCSregistroBB').on("click", function () {
    let CSingresoBB = $('#CSingresoBB').val();
    let CSInomsBB = $('#CSInomsBB').val();
    let CSIidenBB = $('#CSIidenBB').val();
    let CSAidenBB = $('#CSAidenBB').val()
    let CSAtipoBB = $('#CSAtipoBB').val();
    let CSAnomsBB = $('#CSAnomsBB').val();
    let CSedadBB = $('#CSIedadBB').val();
  
   
    const data = {
        CSingresoBB,
        CSInomsBB,
        CSIidenBB,
        CSAidenBB,
        CSAtipoBB,
        CSAnomsBB,
        CSedadBB
    }

    if (isEmpy(CSingresoBB) || isEmpy(CSInomsBB) || isEmpy(CSIidenBB) || isEmpy(CSAidenBB) || isEmpy(CSAtipoBB) || isEmpy(CSAnomsBB) || isEmpy(CSedadBB) ) {
        error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
    } else {

        if (CSedadBB > 18 ) {
            ejecutarajax("CSPacienteBB.aspx/SetAcudienteBB", data, resRegistroAcu )
        } else {
            ejecutarajax("CSPacienteBB.aspx/SetAcudienteBBMenor", data, ResSetAcudienteBBMenor )
        }
    } 
    
})

function resRegistroAcu() {

    $('#CSAidenBB').val('');
    $('#CSAtipoBB').val('');
    $('#CSAnomsBB').val('');
    console.log('limpiar campos')
    exito("Notificacion", "Acudiente registrado")
    tablaMostrarSalidas($('#CSingresoBB').val())
}

function ResSetAcudienteBBMenor( res ) {

    $('#CSAidenBB').val('');
    $('#CSAtipoBB').val('');
    $('#CSAnomsBB').val('');
    tablaMostrarSalidas($('#CSingresoBB').val())
    if (res.d == 0) {
        exito("Notificacion", 'Registro Exitoso')
    } else {
        error("Notificacion", 'Acudiente ya Registrado')
    }
}

function isEmpy(string) {
    if (string == "" || string == null) {
        return true
    }
}


function tablaMostrarSalidas(ingreso) {
    if (isEmpy(ingreso)) {
        error("Notificacion", "Campo de INGRESO vácio");
    } else {
        ejecutarajax("CSPacienteBB.aspx/GetsalidaBB", { ingreso }, tablaPintarSalida)
    }
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
        templateListaSalida.querySelector('.btn-success').dataset.id = item.OID;
        const clone = templateListaSalida.cloneNode(true);
        fragment.appendChild(clone);

    });

    ListaSalida.appendChild(fragment);
}


//BTN ELIMINAR Y ACTUALIZAR ACUDIENTE
ListaSalida.addEventListener("click", (e) => {
     
    if (e.target.classList.contains("btn-danger")) {
        $('#MCSErrorBB').modal('show');
        $('#MCSIdenBB').val(e.target.dataset.id);
        
    }
    if (e.target.classList.contains("btn-success")) {
        data = { oid: e.target.dataset.id }
        ejecutarAjax("CSPacienteBB.aspx/GetUpdateAcuBB", data, GetUpdateAcuBB);
      
    }
})
//ElIMINAR ACUDIENTE
$('#btnMCSEliminarEBB').on("click", function () {
    var oid = $('#MCSIdenBB').val();
    data = { oid: oid }
    ejecutarAjax("CSPacienteBB.aspx/DeleteAcuBB", data, DeleteAcuBB);

})

function DeleteAcuBB() {
    tablaMostrarSalidas($('#CSingresoBB').val());
    $('#MCSErrorBB').modal('hide');
    exito("Notificacion", "Eliminado exitoso")
}

function GetUpdateAcuBB(res) {
    res = res.d;
    res.forEach((item) => {
        $('#CSAoidEdiBB').val(item.OID)
        $('#CSAidenIdiBB').val(item.DOCRESPONSABLE)
        $('#CSATpResEdiCCBB').val(item.TPRESPONSABLE)
        $('#CSACCNombreEdiBB').val(item.NOMRESPONSABLE)
   
    })

    $('#ModalEditaSalidaBB').modal('show')
}

//EDITAR ACUDIENTE
$('#btnCSEditoBB').on("click", function () {
    let CSAoidEdiBB = $('#CSAoidEdiBB').val()
    let CSAidenIdiBB = $('#CSAidenIdiBB').val()
    let CSATpResEdiCCBB = $('#CSATpResEdiCCBB').val()
    let CSACCNombreEdiBB =  $('#CSACCNombreEdiBB').val()
 
    const data = {
        CSAoidEdiBB,
        CSAidenIdiBB,
        CSATpResEdiCCBB,
        CSACCNombreEdiBB
    }

    if (isEmpy(CSAidenIdiBB) || isEmpy(CSATpResEdiCCBB) || isEmpy(CSACCNombreEdiBB) || isEmpy(CSAoidEdiBB) ) {
        error("Notificacion", "Verifique que los campos con (*) estén diligenciados");
    } else {
        ejecutarajax("CSPacienteBB.aspx/UpdateAcuBB", data, resEditarAcu)
    }

})

function resEditarAcu() {
    $('#ModalEditaSalidaBB').modal('hide')
    $('#CSAoidEdiBB').val('')
    $('#CSAidenIdiBB').val('')
    $('#CSATpResEdiCCBB').val('')
    $('#CSACCNombreEdiBB').val('')
    tablaMostrarSalidas($('#CSingresoBB').val());
    exito("Notificacion", "Actualizado exitoso")

}
