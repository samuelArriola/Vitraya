$("#btnMatricularCargo").on("click", function (e) {
    e.preventDefault();
    $.ajax({
        url: "MatricularCargo.aspx/getUsuarios",
        data: JSON.stringify({ "idCargo": parseInt($("#DropDownList2").val()) }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            let datos = JSON.parse(msg.d);
            matricularCargo(datos)
        },
        error: function (result) {
            alert("ERROR " + result.status + ' ' + result.statusText);
        }
    });
});

function matricularCargo(usuarios) {
    const params = new URLSearchParams(window.location.search);
    let idCapacitacion = parseInt(params.get("idCapacitacion"));
    $("#modal1").modal();
    var numUsu = 0;
    for (var i = 0,  n = usuarios.length; i < n; i++ ) {
        $.ajax({
            url: "MatricularCargo.aspx/MatricularUsuarioCargo",
            data: JSON.stringify({ "usuario": usuarios[i], "idCapacitacion": idCapacitacion}),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (msg) {
                $("#progress").attr(`style`,`width:${100 / n * ++numUsu}%`)
                document.getElementById("progress").innerText = parseInt(100 / n * numUsu) + "%";
            },
            error: function (result) {
                alert("ERROR " + result.status + ' ' + result.statusText);
            }
        });
    }
}