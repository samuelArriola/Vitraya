
let solicitudes;

txtTema = $("#txtTema");
fecha1 = $("#fecha1");
fecha2 = $("#fecha2");
txtLugar = $("#txtLugar");
lbTema = $("#lbTema");
lbFecha = $("#lbFecha");
lbEje = $("#lbEje");
listaArch = $("#listaArch");
lbLugar = $("#lbLugar");
lbUnidad = $("#lbUnidad");
lbModalidad = $("#lbModalidad");
lbLink = $("#lbLink")
lbReponsable = $("#lbReponsable");
lbInfomacion = $("#lbInfomación");
lbExamen = $("#lbExamen");
lbSubtemas = $("#lbSubtemas");
btnAceptarSoliciutd = $("#btnAceptarSoliciutd");

function CargarSolicitudes(msg) {
    solicitudes = msg.d;
    console.log(solicitudes);
    dataTable = "";
    solicitudes.forEach((solicitud, i ) => {
        dataTable += `
            <tr>
                <td>${solicitud.StrTema}</td>
                <td>${moment(solicitud.DtmFecha).format("DD/MM/YYYY")}</td>
                <td>${solicitud.StrLugar}</td>
                <td><i class="glyphicon glyphicon-eye-open" id="btnViewS" data-id="${solicitud.IntOidCpsolicitud}"></i></td>
                <td><i class="glyphicon glyphicon-trash" id="dtnDeleteS" data-id="${solicitud.IntOidCpsolicitud}" ></i></td>
            <tr>
        `; 
    });

    $("#tableSolicitudes tbody").html(dataTable);
}

function GetSolicitudes() {
    info = {
        'tema': txtTema.val(),
        'fecha1': fecha1.val() == "" ? "01/01/1800" : fecha1.val(),
        'fecha2': fecha2.val() == "" ? "01/01/3000" : fecha2.val(),
        'lugar': txtLugar.val()
    }
    console.log(info);

    ejecutarajax("AdministrarSolicitudes.aspx/GetSolcitudesCaps", info, CargarSolicitudes)
}



function CargarSolicitud(msg) {
    let datos = msg.d;
    let solicitud = datos[0];
    let subtemas = datos[1];
    let ejeTematico = datos[2];
    let archivos = datos[3];
    let archExamen = datos[4];

    console.log(datos);

    lbTema.text(solicitud.StrTema);
    lbFecha.html(moment(solicitud.DtmFecha).format("DD/MM/YYYY") + " desde " + moment(solicitud.DtmHoraInicial).format("HH:mm a") + " hasta " + moment(solicitud.DtmHoraFinal).format("HH:mm a"))
    lbEje.text(ejeTematico.StrEJETEMATICO);
    lbLugar.text(solicitud.StrLugar);
    lbUnidad.text(solicitud.StrUnidadFuncional);
    lbModalidad.text(solicitud.StrModalidad);
    lbReponsable.text(solicitud.StrResponsable);
    lbLink.html(`<a target="_blank" href="${solicitud.StrLink}">clic aqui</a>`);

    

    lbInfomacion.html(`<textarea readonly  style="resize:none;overflow:hidden" class='form-control' >${solicitud.StrInfoMatricula}</textarea>`);


    text = document.getElementsByTagName("textarea")[0];
    text.style.height = 'auto';
    text.style.padding = "0";
    text.style.height = (text.scrollHeight) + 'px';


    lbExamen.html(`${archExamen.StrNombre}<a href="https://localhost:44310//proceedings/Archivos.aspx?id=${archExamen.IntOidGNArchivo}">&nbsp;&nbsp;<i class="glyphicon glyphicon-download-alt"></i></a>`)

    dataTableArch = "<ul>";

    archivos.forEach(archivo => {
        
        dataTableArch += `
           
            <li>${archivo.StrNombre}&nbsp;&nbsp;<a  href="https://localhost:44310//proceedings/Archivos.aspx?id=${archivo.IntOidGNArchivo}"></a><i class="glyphicon glyphicon-download-alt"></i></li>
        `;
    });

    dataTableArch += "</ul>";
    listaArch.html(dataTableArch)

    let listSubtemas = "<ul>";

    subtemas.forEach(subtema => {
        if (subtema.IntContexto == 1)
        listSubtemas += `<li>${subtema.StrSUBTEMA}</li>`;
    });

    listSubtemas += `</ul>`;

    lbSubtemas.html(listSubtemas);

    btnAceptarSoliciutd.attr("data-id", solicitud.IntOidCpsolicitud);

}



btnAceptarSoliciutd.on("click",function (e) {
    e.preventDefault();
    let idSolicitud = parseInt(this.getAttribute("data-id"));
    let justificacion = $("#txtJustificacion").val();
    ejecutarajax(
        "AdministrarSolicitudes.aspx/AceptarSolicitud",
        { idSolicitud: idSolicitud, justificacion: justificacion },
        function ()
        {
            exito("Solicitud aceptada", "Se ha enviado una notificación por correo electrónico de la solicitud aceptada");
        }
    );
})
$(document).on("click", "#btnViewS", (e) => {
    let btn = e.target;
    let idSolicitud = parseInt(btn.getAttribute("data-id"));

    ejecutarajax("AdministrarSolicitudes.aspx/GetSolicitud", { 'idSolicitud': idSolicitud }, CargarSolicitud);
    $(".modal").modal();
});

$(document).ready(function () {
    GetSolicitudes();
});




