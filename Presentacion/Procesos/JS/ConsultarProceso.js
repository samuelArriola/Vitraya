


let txtNompro = $("#txtNompro");
let txtTipopro = $("#ContentPlaceHolder_ddlTipoPro");


var datos;

const cargarDatos = () => {
    ejecutarajax(
        "ConsultarProcesos.aspx/GetProcesos",
        {
            "Tipo": txtTipopro.val(),
            "Nombre": txtNompro.val(),
        },
        cargarTablaProcesos
    )
}

const cargarTablaProcesos = (msg) => {
    datos = JSON.parse(msg.d);
    tabla = "";
    for (var i = 0; i < datos.length; i++) {
        tabla += `
            <tr>
                <td>${i + 1}</td>
                <td>${datos[i].StrTipo}</td>
                <td>${datos[i].StrNomPro}</td>
                <td><a href=\"PrintProceso.aspx?OIdProceso=${datos[i].IntOIdProceso}\" target = \"iFrameCPro\" >Exportar</a></td>
                <td><a href=\"EditProceso.aspx?OIdProceso=${datos[i].IntOIdProceso}\" ><i class="fa fa-edit ml-2" id="EditPro"></i></a></td>
                <td><a href="#" onClick = "deleteProceso(${i})"><i class="fa fa-trash" id="DeletePro" ></i></a></td>
            </tr>
        `
        $("#tbProcesos tbody").html(tabla);
    }
}

cargarDatos();

txtTipopro.on("keyup", cargarDatos);
txtNompro.on("keyup", cargarDatos);


const deleteProceso = (wi) => {
    ejecutarajax(
        "ConsultarProcesos.aspx/GetDeleteProcesos",
        {
            "OIdProceso": datos[wi].IntOIdProceso
        },
        ImprimirMensage
    )
}

const ImprimirMensage = (msg) => {
    let isDelete = JSON.parse(msg.d);
    if (isDelete) {
        exito("success", "Proceso eliminado");
        cargarDatos();       
    }    
    else
        error("error","No se puede Eliminar el proceso, posee relaciones")
}