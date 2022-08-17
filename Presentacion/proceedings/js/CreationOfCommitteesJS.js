
function CargarCronograma(msg){
    actas = JSON.parse(msg.d);
    console.log(actas);
    let tbCronograma = "";
    actas.forEach(acta => {
        tbCronograma += `
            <tr>
                <td>${acta.StrNombre}</td>
                <td>${acta.StrCoordinador}</td>
                <td>${acta.DtmFecInicio}</td>
                <td>${acta.StrLugarReun}</td>
            </tr>
        `;
    });
    $("#tbCronograma").html(tbCronograma);
    DataTable("#tableComite")
}     

function GetCronograma() {
    datos = {
        idReunion: $("#ContentPlaceHolder_ddlComites").val(),
        FecInicio: new Date($("#txtFecha2").val() == "" ? "01-01-3000" : $("#txtFecha2").val()),
        nombre: $("#txtNombre").val()
    };

    console.log(datos)
    ejecutarajax("CreationOfCommittees.aspx/GetCronograma", datos, CargarCronograma)
}

GetCronograma();
$("#ContentPlaceHolder_ddlComites").on("change", e => { e.preventDefault(); GetCronograma() })
$("#txtFecha2").on("change", e => { e.preventDefault(); GetCronograma() })
$("#txtNombre").on("keyup", e => { e.preventDefault(); GetCronograma() })

$(document).ready(function () {
    
});