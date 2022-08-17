


//se obtine una lista de todos los radiobutons en el panel de las opciones
rdOpciones = document.querySelectorAll("#ContentPlaceHolder_pnOpciones input");
rdRoles = document.querySelectorAll(".pnel table input")

rdbCrear = document.getElementById("crear");
rdbEliminar = document.getElementById("eliminar");
rdbModificar = document.getElementById("modificar");
rdbConfirmar = document.getElementById("confirmar");

var datosOpc;



/*************************SECCIÓN PARA LOS EVENTOS**************************/


for (i = 0; i < rdOpciones.length; i++) {
    rdOpciones[i].addEventListener("change", (e) => {

        let flag = false;
        idRol = -1
        for (j = 0; j < rdRoles.length; j++) {
            if (rdRoles[j].checked) {
                flag = true;
                idRol = rdRoles[j].value;
                break;
            }
        }

        if (!flag) {
            error("Rol sin escojer", "Antes de seleccionar un opcion primero escoja un rol")
            e.target.checked = false;
            return;
        }


        else {
            datos = { 'idRol': idRol, 'idOpcion': e.target.value };
            datosS = JSON.stringify(datos);
            $.ajax({
                url: "ParametrizacionRoles.aspx/GetPermisos",
                data: datosS,
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                success: actualizarRds,
                error: function (result) {
                    alert("ERROR " + result.status + ' ' + result.statusText);
                }
            });
        }
    }, false);
}


var datos;

/**********************´SECCIÓN PAR LOS METODOS*************************/

const rdbPermisos = (e) => {
    blnEliminar = rdbEliminar.checked;
    blnCrear = rdbCrear.checked;
    blnModificar = rdbModificar.checked;
    blnConfirmar = rdbConfirmar.checked;

    idRol = -1
    for (j = 0; j < rdRoles.length; j++) {
        if (rdRoles[j].checked) {
            idRol = rdRoles[j].value;
            break;
        }
    }
    idOpcion = -1
    for (i = 0; i < rdOpciones.length; i++) {
        if (rdOpciones[i].checked) {
            idOpcion = rdOpciones[i].value;
        }
    }

    datos = {
        'eliminar': blnEliminar,
        'crear': blnCrear,
        'modificar': blnModificar,
        'confirmar': blnConfirmar,
        'idRol': parseInt(idRol),
        'idOpcion': parseInt(idOpcion)
    }

    datosS = JSON.stringify(datos);

    try {
        $.ajax({
            url: "ParametrizacionRoles.aspx/ModificarPermisos",
            data: datosS,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) { },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    } catch (ex) {

    }
}

const actualizarRds = (msg) => {
    datos = JSON.parse(msg.d);
    if (datos != null) {
        rdbConfirmar.checked = datos.BlnConfirmar;
        rdbCrear.checked = datos.BlnCrear;
        rdbEliminar.checked = datos.BlnEliminar;
        rdbModificar.checked = datos.BlnModificar;
    }
    else {
        rdbConfirmar.checked = false;
        rdbCrear.checked = false;
        rdbEliminar.checked = false;
        rdbModificar.checked = false;
    }
}



/*************************SECCIÓN PARA LOS EVENTOS**************************/

rdbCrear.addEventListener("change", rdbPermisos, false);
rdbEliminar.addEventListener("change", rdbPermisos, false);
rdbModificar.addEventListener("change", rdbPermisos, false);
rdbConfirmar.addEventListener("change", rdbPermisos, false);

