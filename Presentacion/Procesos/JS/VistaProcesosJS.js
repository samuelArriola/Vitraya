tbProcesos = $("#tbProcesos");
txtNombreProceso = $("#txtNombreProceso");
txtPrefijoProceso = $("#txtPrefijoProceso");
slcTipo = $("#slcTipo");
txtPadreProceso = $("#txtPadreProceso");
slcEstado = $("#slcEstado");
tbodoyTbProcesos = $("#tbodoyTbProcesos");


//$("form").submit(function (e) { e.preventDefault() });
$("form").keypress(function (e) { if (e.keyCode == 13) e.preventDefault() })

function CargarProcesos(msg) {
    let procesos = msg.d;
    let dtProcesos = "";

    procesos.forEach(proceso => {

        let estados = { "0": "En Creacion", "1": "Activo", "2": "Inactivo" }

        dtProcesos += `
            <tr>
                <td>${proceso.StrNomPro}</td>
                <td>${proceso.StrPrefijo}</td>
                <td>${proceso.StrTipo}</td>
                <td>${proceso.StrNomProPadre}</td>
                <td>${estados[proceso.StrEstado]}</td>
                <td><a href="CrearProceso.aspx?idProceso=${proceso.IntOIdProceso}"><i class="fa fa-eye"></i></a></td>
            </tr>
        `
    });


    tbodoyTbProcesos.html(dtProcesos);
    DataTable("#tbProcesos");

}

function GetProcesos() {
    let datos = {
        nombreProceso: txtNombreProceso.val(),
        prefijo: txtPrefijoProceso.val(),
        tipo: slcTipo.val(),
        nomProcesoPadre: txtPadreProceso.val(),
        estado: slcEstado.val()
    }

    ejecutarajax("VistaProcesos.aspx/GetProcesos", datos, CargarProcesos)
}

GetProcesos();

txtNombreProceso.keypress(e => { if (e.keyCode == 13) GetProcesos() });
txtPrefijoProceso.keypress(e => { if (e.keyCode == 13) GetProcesos() });
txtPadreProceso.keypress(e => { if (e.keyCode == 13) GetProcesos() });
slcTipo.change(GetProcesos);
slcEstado.change(GetProcesos);
