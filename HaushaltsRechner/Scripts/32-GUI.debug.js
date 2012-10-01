/// <reference path="00-jquery-1.8.1.debug.js" />
/// <reference path="jquery-1.8.1.intellisense.js" />
/// <reference path="01-jquery-ui-1.8.23.debug.js" />
/// <reference path="10-jquery-maskMoney.debug.js" />
/// <reference path="10-jquery.maskedinput-1.3.min.debug.js" />
/// <reference path="15-Localisation.debug.js" />

/// <reference path="20-Json.debug.js" />

/// <reference path="30-Base.debug.js" />


"use strict";
function deleteChildNodes(el) {
    if (el.childNodes.length > 0) {
        while (el.childNodes.length > 0) {
            el.removeChild(el.firstChild);
        }
    }
}

function getMenuPoint(link, name, targetID) {
    var div = document.createElement("div");
    div.innerHTML = name;

    $(div).click(function () {
        window.location = link;
    }).addClass("Menu_Entry");

    if (document.URL.indexOf(link.substring(2)) > -1) {
        $(div).addClass("Menu_Entry_Active");
    }
    document.getElementById(targetID).appendChild(div);
}

function getReasonSelect(callback) {
    var doc, randStr, container, input, datalist;
    doc = document;
    randStr = "reason" + getRandomString();

    container = createDiv(doc);
    input = createTextInput(doc);
    input.setAttribute("list", randStr);
    input.name = randStr;

    datalist = doc.createElement("datalist");
    datalist.id = randStr;

    $(input).keyup(function () {
        deleteChildNodes(datalist);
        if (input.value !== "") {
            getReasons(input.value, function (reasons) {
                var i, opt;
                for (i = 0; i < reasons.length; i += 1) {
                    opt = doc.createElement("option");
                    opt.value = reasons[i].TEXT;

                    datalist.appendChild(opt);
                }
            });
        }
    });

    container.appendChild(input);
    container.appendChild(datalist);

    callback();

    return {
        "div": function () {
            return container;
        },
        "value": function () {
            return input.value;
        },
        "setValue": function (val) {
            if (!isNullOrUndefined(val)) {
                input.value = val;
            }
        }
    };
}

function makeDatePicker(txtBoxID) {
    $('#' + txtBoxID).datepicker({
        dateFormat: getLocalText().DateFormat
    }).maskSimple(getLocalText().NumberDateMaskFormat);
}

function makeDatePickerByElement(txtBox) {
    $(txtBox).datepicker({
        dateFormat: getLocalText().DateFormat
    }).maskSimple(getLocalText().NumberDateMaskFormat);
}

function makeMoneyMask(txtBoxID) {
    $('#' + txtBoxID).maskMoney({
        symbol: getLocalText().Currency,
        showSymbol: true,
        thousands: getLocalText().Thousand,
        decimal: getLocalText().Decimal,
        symbolStay: true,
        allowNegative: true,
        defaultZero: false,
        allowZero: true
    });
}

function makeMoneyMaskByElement(txtBox) {
    $(txtBox).maskMoney({
        symbol: getLocalText().Currency,
        showSymbol: true,
        thousands: getLocalText().Thousand,
        decimal: getLocalText().Decimal,
        symbolStay: true,
        allowNegative: true,
        defaultZero: false,
        allowZero: true
    });
}

function createTextInput(doc) {
    var txt = doc.createElement("input");
    txt.type = "text";
    return txt;
}

function createHiddenInput(doc) {    
    var hidden = createTextInput(doc);
    hidden.style.visibility = "hidden";
    return hidden;
}

function createPasswordInput(doc) {
    var txtPw = doc.createElement("input");
    txtPw.type = "password";
    return txtPw;
}

function createMaskMoneyTextInput(doc) {
    var txt = createTextInput(doc);
    makeMoneyMaskByElement(txt);
    return txt;
}

function createDatePickerTextInput(doc) {
    var txt = createTextInput(doc);
    makeDatePickerByElement(txt);
    return txt;
}

function createCheckBoxInput(doc) {
    var cb = doc.createElement("input");
    cb.type = "checkbox";
    return cb;
}

function createDiv(doc) {
    return doc.createElement("div");
}

function createMaxHeightWidthDiv(doc) {
    var d = createDiv(doc);
    d.style.width = "100%";
    d.style.height = "100%";
    return d;
}

function createDivHeightWidth(doc, height, width){
    var d = createDiv(doc);
    d.style.width = width + "px";
    d.style.heigth = height + "px";
    return d;
}

function createClearBothDiv(doc) {
    var d = createDiv(doc);
    d.className = "Clear";
    return d;
}

function createSelect(doc) {
    return doc.createElement("select");
}

function createSpan(doc) {
    return doc.createElement("span");
}

function createParagraph(doc) {
    return doc.createElement("p");
}