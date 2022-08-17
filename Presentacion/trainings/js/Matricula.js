metodos = []



$("#btn-matricula").click(function (e) {
    e.preventDefault();
    $("#modal-matricula").modal();
});



function CargarMetodos() {
    let listas = ["#lstUsuarios", "#lstCargos","#lstUnididades"]

    listas.forEach(item => {
        $(item).html("");
    })

    metodos.forEach(metodo => {
        
        let itemList = document.createElement("div");
        itemList.className = "item-list";
        itemList.innerHTML = `      
            ${metodo.nombreMetodoMatr}<i class="fa fa-close btn-close-lst"></i>
        `
        $(itemList).attr("data-id", metodo.valorMetodoMetr)
        $(listas[metodo.metodoMatr]).prepend(itemList);
    });
}


function CargarMatricula(msg) {
    let matriculas = msg.d.matriculas;
    metodos = msg.d.metodos;
    let capacitacion = msg.d.capacitacion
    console.log(msg.d)
    let dtMatriculas = `
        <table class="table">
            <thead>
                <tr>
                    <th></th>
                    <th>Documento</th>
                    <th>Nombre</th>
                    <th>Cargo</th>
                    <th>Correo electrónico</th>
                    <th></th>
                </tr>
                <tr>
                    <th></th>
                    <th><input type="text" class="form-control" id="txtDocumento" /></th>
                    <th><input type="text" class="form-control" id="txtNombre" /></th>
                    <th><input type="text" class="form-control" id="txtCargo" /></th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
        `;

    matriculas.forEach(matricula => {
        dtMatriculas += `
            <tr>
                <td>
                    <div class="photo" style="background: url(${matricula.foto != "" ? `data:image/jpg;base64,${matricula.foto}` : "../Images/user.png"}); "></div> 
                <td>${matricula.usuario.GNCodUsu1}</td>
                <td>${matricula.usuario.GNNomUsu1}</td>
                <td>${matricula.usuario.GnCargo1}</td>
                <td>${matricula.usuario.GNCrusu1}</td>
                <td>
                    <div class="delete-matricula" onclick="DeleteMatricula(${matricula.usuario.GNCodUsu1})">
                        <img src="../Images/Delete.png" width="20"/>
                    </div>
                </td>
            </tr>
        `;
    });

    dtMatriculas += "</tdoby></table>"

    $("#tbParticipantes").html(dtMatriculas);
    DataTable("#tbParticipantes table")

    CargarMetodos();
    $("#txtTitulo").text(capacitacion.StrTEMA);
}

function DeleteMatricula(idUsuario) {
    let params = new URLSearchParams(window.location.search);
    let idAgenda = parseInt(params.get("idAgenda"));
    let datos = {
        "idUsuario": idUsuario,
        "idAgenda": idAgenda
    }

    ejecutarajax("Matricula.aspx/DeleteMatricula", datos, function (msg) {
        exito("Usuario desmatriculado", "El usuario ha sido dematriculado correctamente");
        GetMatriculas();
    })
}

$("#btnMatricular").click(function () {
    setMatricula();
    GetMatriculas();
    $("#modal-matricula").modal("hide");
});

async function GetMatriculas() {
    let params = new URLSearchParams(window.location.search);
    let idAgenda = parseInt(params.get("idAgenda"));
    let idCapacitacion = parseInt(params.get("idCapacitacion"));

    let datos = {
        "idAgenda": idAgenda,
        "documento": $("#txtDocumento").val() ?? "",
        "nombre": $("#txtNombre").val() ?? "",
        "cargo": $("#txtCargo").val() ?? "",
        "idCapacitacion": idCapacitacion
    }

    console.log(await ejecutarajax("Matricula.aspx/GetUsuariosMatriculados", datos, CargarMatricula));
}

$(document).ready(function () {
    GetMatriculas();
    $("#modal-matricula").on("hide.bs.modal", function () {
        CargarMetodos();
    })

    $("form").keypress(function (e) { if (e.keyCode == 13) e.preventDefault() }).change(function (e) { e.preventDefault()})
});


let AutoCompleteUsuario = new slcAutoComplete(
    $("#txtUsuario"),
    $(".box-autocomplete")[0],
    "Matricula.aspx/GetUsers",
    function (data) {
        let itemList = document.createElement("div");
        itemList.className = "item-list";
        itemList.innerHTML = `      
            ${data.text}<i class="fa fa-close btn-close-lst"></i>
        `
        $(itemList).attr("data-id",data.value)

        $("#lstUsuarios").prepend(itemList);
    }
);

let AutoCompleteCargo = new slcAutoComplete(
    $("#txtCargo"),
    $(".box-autocomplete")[1],
    "Matricula.aspx/GetCargos",
    function (data) {
        let itemList = document.createElement("div");
        itemList.className = "item-list";
        itemList.innerHTML = `      
            ${data.text}<i class="fa fa-close btn-close-lst"></i>
        `
        $(itemList).attr("data-id", data.value)

        $("#lstCargos").prepend(itemList);
    }
);

let AutoCompleteMatricula = new slcAutoComplete(
    $("#txtUnidadFuncional"),
    $(".box-autocomplete")[2],
    "Matricula.aspx/GetUnidadesFuncionales",
    function (data) {
        let itemList = document.createElement("div");
        itemList.className = "item-list";
        itemList.innerHTML = `      
            ${data.text}<i class="fa fa-close btn-close-lst"></i>
        `
        $(itemList).attr("data-id", data.value)

        $("#lstUnididades").prepend(itemList);
    }
);
AutoCompleteUsuario.setAutocomplete();
AutoCompleteCargo.setAutocomplete();
AutoCompleteMatricula.setAutocomplete();
//metodo que al hacer click sobre la x de la etiqueta del un subtema lo elimina
$(document).on("click", ".btn-close-lst", function () {
    this.parentElement.parentElement.removeChild(this.parentElement);
});

function getDataMatricula() {
    datos = [];
    [...document.querySelectorAll("#lstCargos .item-list")].forEach(divCargo => {
        datos.push({
            "metodoMatr": 1,
            "valorMetodoMetr": parseInt($(divCargo).attr("data-id")),
            "nombreMetodoMatr": divCargo.innerText.trim(),
        })
    });

    [...document.querySelectorAll("#lstUnididades .item-list")].forEach(divCargo => {
        datos.push({
            "metodoMatr": 2,
            "valorMetodoMetr": parseInt($(divCargo).attr("data-id")),
            "nombreMetodoMatr": divCargo.innerText.trim(),
        })
    });


    [...document.querySelectorAll("#lstUsuarios .item-list")].forEach(divCargo => {
        datos.push({
            "metodoMatr": 0,
            "valorMetodoMetr": parseInt($(divCargo).attr("data-id")),
            "nombreMetodoMatr": divCargo.innerText.trim(),
        })
    });

    return datos;
}

function setMatricula() {
    let params = new URLSearchParams(window.location.search);
    let idCapacitacion = parseInt(params.get("idCapacitacion"));
    let idAgenda = parseInt(params.get("idAgenda"));

    datosMatricula = {
        "datos": getDataMatricula(),
        "idCapacitacion": idCapacitacion,
        "idAgenda": idAgenda
    };

    ejecutarajax("Matricula.aspx/SetMatricula", datosMatricula, function () { });
}

$(document).on("keypress", "#txtDocumento", function (e) {
    if (e.keyCode == 13) {
        GetMatriculas();
    }
});

$(document).on("keypress", "#txtCargo", function (e) {
    if (e.keyCode == 13) {
        GetMatriculas();
    }
});

$(document).on("keypress", "#txtNombre", function (e) {
    if (e.keyCode == 13) {
        GetMatriculas();
    }
});

$("form").keypress(function (e) {
    if (e.keyCode == 13) {
        e.preventDefault();
    }
});