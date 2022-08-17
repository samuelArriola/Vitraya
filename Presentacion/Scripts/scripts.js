function ejecutarajax (url, datos, success)  {
    return $.ajax({
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

let infoMenu;

async function CargarMenu() {
    infoMenu = (await ejecutarajax("../Index.aspx/GetInfoMenu", { nombre: $("#txtSearchMenuP").val() })).d

    let menu = "";
    let menuMovil = "";

    infoMenu.forEach((item,i) => {
        menu += `<li data-index="${i}"><a>${item.Modulo}<i class="${item.Icono}"></i></a></li>`;

        menuMovil += `<div class="col col-3 col-sm-2 icon">
                           <a data-index="${i}"><i class="${item.Icono}"></i>
                           <h6>${item.Modulo}</h6></a>
                       </div> `

        if (i == 0) {
            let menu2 = "";
            item.Opciones.forEach(opcion => {
                menu2 += `<li><a href="${opcion.StrPrefijo}">${opcion.StrNombre}</a></li>`;
            });
            $(".submenu ul").html(menu2);
        }
    });

    $("#menu").html(menu);
    $("#continer-icons").html(menuMovil)


}



$(document).on("click", ".submenu ul li", function (e) {
    console.log(this);
    $("#p-menu-movil").addClass("menu-movil-hidden");
    $("#p-menu-movil").removeClass("menu-movil-display");
});

async function cargarMenuMovil() {
    infoMenu = (await ejecutarajax("../Index.aspx/GetInfoMenu", { nombre: $("#txtSearchMovil").val() })).d

    let menuMovil = "";

    infoMenu.forEach((item, i) => {
        menu += `<li data-index="${i}"><a>${item.Modulo}<i class="${item.Icono}"></i></a></li>`;
    });

    $("#menu").html(menu);
    $("#continer-icons").html(menuMovil)

}

$("#txtSearchMovil").keypress(async function (e) { if (e.keyCode == 13) await cargarMenuMovil() });
$("#txtSearchMenuP").keypress(async function (e) { if (e.keyCode == 13) await CargarMenu() });


$(document).on("mouseover","#menu li", function (e) {
    let index = parseInt($(this).attr("data-index"));
    let menu = "";
    infoMenu[index].Opciones.forEach(opcion => {
        menu += `<li><a href="${opcion.StrPrefijo}">${opcion.StrNombre}</a></li>`;
    })
    $(".submenu ul").html(menu);
});


    CargarMenu();


$("#btnMenu").click(function (e) {
    if ($(".p-menu").hasClass("menu-visible")) {
        $(".p-menu").removeClass("menu-visible");
        $(".p-menu").addClass("menu-hidden")
    }
    else {
        $(".p-menu").addClass("menu-visible");
        $(".p-menu").removeClass("menu-hidden")
    }
})

$("#btnMenuMovil").click(function (e) {
    $("#p-menu-movil").removeClass("menu-movil-hidden");
    $("#p-menu-movil").addClass("menu-movil-display");
})
$("#btnCloseMenu").click(function (e) {
    $("#p-menu-movil").addClass("menu-movil-hidden");
    $("#p-menu-movil").removeClass("menu-movil-display");
})

$(document).on("click", ".icon a", function (e) {
    let index = parseInt($(this).attr("data-index"));
    let menu = ""
    infoMenu[index].Opciones.forEach(opcion => {
        menu += `<li><a href="${opcion.StrPrefijo}">${opcion.StrNombre}</a></li>`;
    })

    $("#menuModal").modal();
    $("#menuModal .modal-body ul").html(menu);
});

$(document).click(function (e) {
    let c = $(".p-menu").offset();
    let w = $(".p-menu").width();
    let h = $(".p-menu").height();

    if (!(e.pageX >= c.left && e.pageX <= c.left + w && e.pageY >= c.top && e.pageY <= c.top + h) && !$(e.target).is("[btn-menu]") && $(".p-menu").hasClass("menu-visible"))
        $(".p-menu").removeClass("menu-visible").addClass("menu-hidden")
})

$(".menu-right div ul li, .menu-right i").click(function (e) {
    $(".p-menu").removeClass("menu-visible");
    $(".p-menu").addClass("menu-hidden")
});