


let Controles = {
    "celda": `
              <div class="d-flex justify-content-around">
                <div class="celda d-flex justify-content-around">
                <div>
              </div>
            `,
    "textBox":`
                <input type="text" class="form-control" />
              `,
    "textArea": `
                    <textarea class="form-control"></textarea>
                `,
    "label": `
                <input type="text" class="label" />
            `,
    "radio": `<div>
                <input type="radio" class="mr-1" /><input type="text" class="label" />
              </div>
            `,
    "checkBox": `
             <div>
                <input type="checkbox" class="mr-1" /><input type="text" class="label" />
             </div>
    `,
}

contextMenu({
    selector: ".celda",
    items: [
        {
            name: "Agregar",
            icon: "",
            event: function (d) {
                $(d.parentElement).append(` <div class="celda d-flex justify-content-around"><div>`)
            }
        },
        { name: "Eliminar", icon: "", event: function (d) { alert(d) } },
    ]
})

$.event.props.push('dataTransfer');

$(".canvas, .celda").on("dragover", function (e) {
    e.preventDefault();
})

$(".canvas").on("drop", function (e) {
    e.preventDefault();
    data = JSON.parse(e.dataTransfer.getData("text"));
    if(data.type == "celda")
        $(this).append(Controles[data.type]);
})

$(document).on("drop", ".celda", function (e) {
    e.preventDefault();
    data = JSON.parse(e.dataTransfer.getData("text"));
    console.log(data);
    $(this).append(Controles[data.type]);
});



$("#ctrLabel").on("dragstart", function (e) {
    data = {
        type: "label",
    };
    e.dataTransfer.setData("text", JSON.stringify(data));
})

$("#ctrCelda").on("dragstart", function (e) {
    data = {
        type: "celda",
    };
    e.dataTransfer.setData("text", JSON.stringify(data));
})

$("#ctrTextBox").on("dragstart", function (e) {
    data = {
        type: "textBox",
    };
    e.dataTransfer.setData("text", JSON.stringify(data));
})
$("#ctrTextArea").on("dragstart", function (e) {
    data = {
        type: "textArea",
    };
    e.dataTransfer.setData("text", JSON.stringify(data));
})

$("#ctrRadio").on("dragstart", function (e) {
    data = {
        type: "radio",
    };
    e.dataTransfer.setData("text", JSON.stringify(data));
})

("#ctrRadio").on("dragstart", function (e) {
    data = {
        type: "radio",
    };
    e.dataTransfer.setData("text", JSON.stringify(data));
})

$("#ctrRadio").on("dragstart", function (e) {
    if (e.target.class = "celda") {
        console.log(e.isPropagationStopped())

        coords = e.target.getBoundingClientRect();
        borderLeft = parseInt(scrollX + coords.x);
        borderRight = parseInt(scrollX + coords.x + coords.width - 1)
        if ((e.pageX >= borderLeft && e.pageX <= borderLeft + 1) || (e.pageX <= borderRight && e.pageX >= borderRight - 1)) {
            $(e.target).css("cursor", "col-resize");
        }
        else {
            $(e.target).css("cursor", "default");
        }
        e.stopPropagation();
        return false;
    }
});