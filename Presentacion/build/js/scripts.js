indexItemBox = 20;

let numRowsToDisplay = 20
let numPageIndex = 1;
let numPags = 0;

function createPagination(selector) {
    let navTable;
    if ($(selector).parent().hasClass("tableContainer")) {
        let isVextElementNav = $(selector).parent().next().hasClass("navTable");
        navTable = isVextElementNav ? $(selector).parent().next()[0] : document.createElement("nav");
    }
    else {
        let isVextElementNav = $(selector).next().hasClass("navTable");
        navTable = isVextElementNav ? $(selector).next()[0] : document.createElement("nav");
    }

    $(navTable).addClass("navTable")
    navTable.innerHTML = "";

    if (numPags <= 5) {
        for (let i = 1; i <= numPags; i++) {
            navTable.innerHTML += `
                <input type="radio" class="d-none" id="rn${i}" name="nav-table" ${i == numPageIndex ? "checked" : ""}>
                <label for="rn${i}" class="btn-nav" data-index="${i}">${i}</label>
            `
        }
    }
    else {
        
        if(numPageIndex <= 3) {
            for (let i = 1; i <= 5; i++) {
                navTable.innerHTML += `
                    <input type="radio" class="d-none" id="rn${i}" name="nav-table" ${i == numPageIndex ? "checked" : ""}>
                    <label for="rn${i}" class="btn-nav" data-index="${i}">${i}</label>
                `
            }
            navTable.innerHTML += `
                <input type="radio" class="d-none" id="rnn" name="nav-table" />
                <label for="rnn" class="btn-next" >></label>

                <input type="radio" class="d-none" id="rnu" name="nav-table" >
                <label for="rnu" class="btn-nav" data-index="${numPags}">>></label>
            `
        }
        else if (numPageIndex >= numPags - 2) {
            navTable.innerHTML += `
                <input type="radio" class="d-none" id="rni" name="nav-table" >
                <label for="rni" class="btn-nav" data-index="1"><<</label>

                <input type="radio" class="d-none" id="rnp" name="nav-table" />
                <label for="rnp" class="btn-previus" ><</label>
            `;
            for (let i = numPags - 5; i <= numPags; i++) {
                navTable.innerHTML += `
                    <input type="radio" class="d-none" id="rn${i}" name="nav-table" ${i == numPageIndex ? "checked" : ""} >
                    <label for="rn${i}" class="btn-nav" data-index="${i}">${i}</label>
                `
            }
        }
        else {
            navTable.innerHTML += `
                <input type="radio" class="d-none" id="rni" name="nav-table" >
                <label for="rni" class="btn-nav" data-index="1"><<</label>

                <input type="radio" class="d-none" id="rnp" name="nav-table" />
                <label for="rnp" class="btn-previus" ><</label>
            ` ;

            for (let i = numPageIndex - 2; i <= numPageIndex + 2; i++) {
                navTable.innerHTML += `
                    <input type="radio" class="d-none" id="rn${i}" name="nav-table" ${i == numPageIndex ? "checked" : ""}>
                    <label for="rn${i}" class="btn-nav" data-index="${i}">${i}</label>
                `
            }
            navTable.innerHTML += `
                <input type="radio" class="d-none" id="rnn" name="nav-table" />
                <label for="rnn" class="btn-next" >></label>

                <input type="radio" class="d-none" id="rnu" name="nav-table" >
                <label for="rnu" class="btn-nav" data-index="${numPags}">>></label>
            `;
        }
    }
    if ($(selector).parent().hasClass("tableContainer"))
        $(selector).parent().after(navTable);
    else
        $(selector).after(navTable);
}

function DataTable(selector, numRowsD = 20) {

    numRowsToDisplay = numRowsD;

    $(`${selector}>tbody>tr`).addClass("d-none");
    let rows = $(`${selector} tbody>tr`);
    numPags = parseInt((rows.length - 1) / numRowsToDisplay) + 1

    for (let i = 0; i < numRowsToDisplay; i++) {
        $(`${selector}>tbody>tr`).eq(i).removeClass("d-none");
    }

    createPagination(selector)
}

$(document).on("click", ".btn-nav, .btn-next,  .btn-previus", function (e) {
    let index;
    if ($(e.target).attr("class") == "btn-nav") {
        index = parseInt($(this).attr("data-index"));
        numPageIndex = index
    }
    else if ($(e.target).attr("class") == "btn-next") {
        index = ++numPageIndex;
    }
    else {
        index = --numPageIndex;
    }
    let selector = $(e.target.parentElement).prev().hasClass("tableContainer") ? $(e.target.parentElement).prev().find("table") : $(e.target.parentElement).prev() ;

    createPagination(selector);


    rows = selector.find("tbody tr");

    rows.addClass("d-none");

    console.log((index - 1) * numRowsToDisplay)

    for (let i = (index - 1) * numRowsToDisplay, j = i + numRowsToDisplay; i < j && i < rows.length; i++) {
        $(rows).eq(i).removeClass("d-none");
    }

});


class slcAutoComplete {
    data = [];
    constructor(input, lstAutocomplete, url, action, datos = null) {
        this.indexItemBox = -1;
        this.input = input;
        this.lstAutocomplete = lstAutocomplete;
        this.url = url;
        this.action = action;
        this.datos = datos;
    }

    setAutocomplete = function() {

        $(this.input).keyup(async e => {

            let ItemBox = $(this.lstAutocomplete).find(".item-autocomplete");
            switch (e.keyCode) {
                case 38: {
                    if (this.indexItemBox <= 0) 
                        this.indexItemBox = ItemBox.length - 1;
                    else 
                        this.indexItemBox--;

                    ItemBox.removeClass("item-selected")
                    $(ItemBox.get(this.indexItemBox)).addClass("item-selected")
                    $(this.lstAutocomplete).scrollTop(ItemBox.get(this.indexItemBox).offsetTop)
                    e.target.value = $(ItemBox.get(this.indexItemBox)).text()
                    break;
                }
                case 40: {
                    if (this.indexItemBox >= ItemBox.length - 1) 
                        this.indexItemBox = 0;
                    else 
                        this.indexItemBox++;
                    
                    ItemBox.removeClass("item-selected")
                    $(ItemBox.get(this.indexItemBox)).addClass("item-selected")
                    $(this.lstAutocomplete).scrollTop(ItemBox.get(this.indexItemBox).offsetTop)
                    e.target.value = $(ItemBox.get(this.indexItemBox)).text()
                    break;
                }
                case 13: {
                    if (this.indexItemBox != -1) {
                        let itemData = this.data[this.indexItemBox];
                        this.indexItemBox = -1;
                        $(this.lstAutocomplete).css({ "max-height": "0", "border": "0" });
                        this.action(itemData);
                    }
                    break;
                }
                default: {
                    this.setBoxAutocomplete()
                    break;
                }
            }
        }).focus(e => {
            $(this.input).val("");
            $(this.lstAutocomplete).scrollTop(0);
            this.setBoxAutocomplete();
            this.indexItemBox = -1;
            $(this.lstAutocomplete).css({ "max-height": "300px", "border": "1px solid #000"});
        }).blur(e => {
            let flag = false;
            for (let i = 0, length = this.data.length; i < length; i++) {
                if (this.input.val() == this.data[i].text) {
                    flag = true;
                    break;
                }
            }
            if (!flag) {
                this.input.val("");
            }
            this.indexItemBox = -1;
            $(this.lstAutocomplete).css({ "max-height": "0", "border": "0" });
        })
    }

    setBoxAutocomplete = async function () {
        let d = {};

        if (this.datos == null) {
            d = {
                "name": this.input.val()
            }
        }
        else {
            d = {
                "name": this.input.val(),
                "nombreDepartamento": this.datos.val(),
            }
        }

        this.data = (await this.GetData(this.url, d)).d
        let lstAutocomplete = "";
        for (let i = 0; i < this.data.length; i++) {
            let item = this.data[i];
            lstAutocomplete += `<div class="item-autocomplete">${item.text}</div>`;
        }
        $(this.lstAutocomplete).html(lstAutocomplete);

        [...this.lstAutocomplete.querySelectorAll(".item-autocomplete")].forEach((item, index) => {
            $(item).on("mousedown",e => {
                let itemData = this.data[index];
                this.indexItemBox = -1;
                $(this.lstAutocomplete).css({ "max-height": "0", "border": "0" });
                this.action(itemData);
            })
        })
    }

    GetData = function(url,data) {
        return $.ajax({
            url: url,
            data: JSON.stringify(data),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
        });
    }
}


let ctxMenuSender;


function contextMenu(data) {
    
    lista = document.createElement("ul")

    $(lista).css({
        "padding": "2px",
        "border": "solid 1px #ccc",
        "background": "#fff",
        "border-radius": "3px",
        "position": "absolute",
        "z-index": "10000",
        "overflow": "hidden",
        "min-width": "150px",
        "box-shadow": "1px 1px 2px rgba(0,0,0,.5)"
    });

    lista.id = "contextMenu"

    data.items.forEach(item => {
        domItem = document.createElement("li");
        $(domItem).css({
            "padding": "3px",
            "cursor": "pointer",
            "user-select": "none",
            "color": "#000",
        });

        $(domItem).hover(
            function() {
                $(this).css("background","#eee")
            },
            function () {
                $(this).css("background", "#fff")
            }
        )

        $(domItem).html(`
            <i class="${item.icon} mr-2"></i>${item.name}
        `)

        $(domItem).on("mousedown", {event: item.event}, function (e) {
            e.data.event(ctxMenuSender);
        });

        $(lista).append(domItem);
    });

    $(document.body).on("mousedown blur",function () {
        ctxMenu = document.getElementById("contextMenu")
        if (ctxMenu) {
            document.body.removeChild(ctxMenu)
        }
    })


    $(document).on("mousedown", data.selector, function (e) {
        ctxMenuSender = e.target;

        if (e.button == 2) {
            $(document.body).append(lista);
            $(lista).css("top", `${e.pageY+1}px`);
            $(lista).css("left", `${e.pageX+1}px`);
        }
    });


    $(document).on("contextmenu", ".celda", function (e) {
        e.preventDefault();
        return false;
    })

    
}