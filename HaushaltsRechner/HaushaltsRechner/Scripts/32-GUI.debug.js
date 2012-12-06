/*
This file is part of HaushaltsRechner.

HaushaltsRechner is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

HaushaltsRechner is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with HaushaltsRechner.  If not, see <http://www.gnu.org/licenses/>.

Diese Datei ist Teil von HaushaltsRechner.

HaushaltsRechner ist Freie Software: Sie können es unter den Bedingungen
der GNU General Public License, wie von der Free Software Foundation,
Version 3 der Lizenz oder (nach Ihrer Option) jeder späteren
veröffentlichten Version, weiterverbreiten und/oder modifizieren.

HaushaltsRechner wird in der Hoffnung, dass es nützlich sein wird, aber
OHNE JEDE GEWÄHELEISTUNG, bereitgestellt; sogar ohne die implizite
Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
Siehe die GNU General Public License für weitere Details.

Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

/// <reference path="00-jquery-1.8.3.debug.js" />
/// <reference path="jquery-1.8.3.intellisense.js" />
/// <reference path="01-jquery-ui-1.9.2.debug.js" />
/// <reference path="10-jquery-maskMoney.debug.js" />
/// <reference path="10-jquery.maskedinput-1.3.min.debug.js" />
/// <reference path="15-Localisation.debug.js" />

/// <reference path="20-Json.debug.js" />

/// <reference path="30-Base.debug.js" />

"use strict";
(function (client, $, undefined) {
    var doc = document,
        hr = haushaltsRechner;
    
    client.clearElement = function (element) {
        /// <summary>
        /// Clears selects from options && all input fields from user input
        /// </summary>
        /// <param name="element">select or input</param>
        //selects
        if (element.selectedIndex) {
            element.selectedIndex = 0;
            return;
        }
        //input allgemein
        else if (element.value) {
            element.value = '';
            
            //checkbox
            if (element.checked) {
                element.checked = false;
            }
        }
    };

    client.clearElementsByElements = function (elements) {
        /// <summary>
        /// Clears selects from options && all input fields from user input
        /// for an array of elements
        /// </summary>
        /// <param name="elements">array of elements</param>
        var i, len, elArray, el;

        if (!elements) {
            return;
        }

        if (elements.length) {
            for (i = 0, len = elements.length; i < len; i += 1) {
                el = elements[i];
                client.clearElement(el);               
            }
        }
        else {
            client.clearElement(elements);
        }
    };

    client.clearElementByElementIds = function (elements) {
        /// <summary>
        /// Clears selects from options && all input fields from user input
        /// for an array of elementIDs
        /// </summary>
        /// <param name="elements">array of elementIDs</param>
        var i, len, elArray;

        if (elements.length) {
            elArray = [];

            for (i = 0, len = elements.length; i < len; i += 1) {
                elArray.push(doc.getElementById(elements[i]));
            }

            client.clearElementsByElements(elArray);
        }
        else {
            client.clearElement(doc.getElementById(elements));
        }
    };

    client.deleteChildNodes = function (el) {
        /// <summary>
        /// Deletes all childNodes of the Element
        /// </summary>
        /// <param name="el">element</param>
        var child;
        if (el.childNodes.length > 0) {
            while (el.childNodes.length > 0) {
                child = el.firstChild;

                if (child && child.id) {
                    child.id = "";
                }
                el.removeChild(el.firstChild);
            }
        }
    };

    client.menuPoint = function (link, name, targetID) {
        /// <summary>
        /// Creates a menuEntry
        /// </summary>
        /// <param name="link">location to go after clicking the element</param>
        /// <param name="name"> Text of the menupoint</param>
        /// <param name="targetID">targetID for the menupoint</param>
        var div = client.div();
        div.innerHTML = name;

        $(div).click(function () {
            window.location = link;
        }).addClass("Menu_Entry");

        if (doc.URL.indexOf(link.substring(2)) > -1) {
            $(div).addClass("Menu_Entry_Active");
        }
        doc.getElementById(targetID).appendChild(div);
    };

    client.headline = function (number) {
        /// <summary>
        /// Creates an h-Element with the given number
        /// </summary>
        /// <param name="number">number of headline</param>
        /// <returns type="object">h-element</returns>
        return doc.createElement('h' + (number || '1'));
    };

    client.headlineHtml = function (number, innerHtml) {
        /// <summary>
        /// Creates an h-Element with the given number and the given innerHtml
        /// </summary>
        /// <param name="number">number of headline</param>
        /// <param name="innerHtml">innerHtml of the h-Element</param>
        /// <returns type="object">h-Element</returns>
        var h = client.headline(number || 1);
        h.innerHTML = innerHtml || "";
        return h;
    };

    client.horizontalLine = function () {
        /// <summary>
        /// Creates hr-Element
        /// </summary>
        /// <returns type="object">hr-Element</returns>
        return doc.createElement("hr");
    };

    client.label = function (text) {
        /// <summary>
        /// Creates a textNode with the given text
        /// </summary>
        /// <param name="text">text</param>
        /// <returns type="object">textnode-element</returns>
        return doc.createTextNode(text || "");
    };

    client.input = function () {
        /// <summary>
        /// Creates an input
        /// </summary>
        /// <returns type="object">input-element</returns>
        return doc.createElement("input");
    };

    client.textBox = function () {
        /// <summary>
        /// Creates an input field with type=text
        /// </summary>
        /// <returns type="object">input[text]-field</returns>
        var txt = client.input();
        txt.type = "text";
        return txt;
    };

    client.hiddenTextBox = function () {
        /// <summary>
        /// Creates an input field with type=text and style.visibility=hidden
        /// </summary>
        /// <returns type="object">input[text]-field(hidden)</returns>
        var txt = client.textBox();
        txt.style.visibility = "hidden";
        return txt;
    };

    client.passwordTextBox = function () {
        /// <summary>
        /// Creates an input field with type=password
        /// </summary>
        /// <returns type="object">input[password]-field</returns>
        var txt = client.input();
        txt.type = "password";
        return txt;
    };

    client.checkBox = function () {
        /// <summary>
        /// Creates an input field with type=checkbox
        /// </summary>
        /// <returns type="object">input[checkbox]-field</returns>
        var txt = client.input();
        txt.type = "checkbox";
        return txt;
    };

    client.select = function () {
        /// <summary>
        /// Creates a select-Element
        /// </summary>
        /// <returns type="object">select-element</returns>
        return doc.createElement("select");
    };

    client.option = function () {
        /// <summary>
        /// Creates an option-Element
        /// </summary>
        /// <returns type="obejct">option-Element</returns>
        return doc.createElement("option");
    };

    client.span = function () {
        /// <summary>
        /// Creates a span-element
        /// </summary>
        /// <returns type="object">span-element</returns>
        return doc.createElement("span");
    };

    client.spanWithClass = function (className) {
        /// <summary>
        ///  Creates a span-element with the given className
        /// </summary>
        /// <param name="className">className to add</param>
        /// <returns type="object">span-element</returns>/
        var span = client.span();
        span.className = className || "";
        return span;
    };

    client.paragraph = function () {
        /// <summary>
        /// Creates a p-Element
        /// </summary>
        /// <returns type="object">p-element</returns>
        return doc.createElement("p");
    };

    client.div = function () {
        /// <summary>
        /// Creates a div-element
        /// </summary>
        /// <returns type="object">div-element</returns>
        return doc.createElement("div");
    };

    client.divWithClass = function (className) {
        /// <summary>
        /// Creates a div-element with the given className
        /// </summary>
        /// <param name="className">className to add</param>
        /// <returns type="object">div-element</returns>
        var d = client.div();
        d.className = className || '';
        return d;
    };

    client.divClearBoth = function () {
        /// <summary>
        /// Creates a div-element with className "Clear"
        /// </summary>
        /// <returns type="object">div-element</returns>
        return client.divWithClass("Clear");
    };

    client.divHeightWidthByPx = function (height, width) {
        /// <summary>
        /// Creates a div element with given height and width in pixel
        /// </summary>
        /// <param name="height">height of div</param>
        /// <param name="width">width of div</param>
        /// <returns type="object">div-element</returns>
        var d = client.div();
        d.style.width = (width || "150") + "px";
        d.style.height = (height || "150") + "px";
        return d;
    };

    client.divHeightWidthByPercent = function (height, width) {
        /// <summary>
        /// Creates a div element with given height and width in percent
        /// </summary>
        /// <param name="height">height of div</param>
        /// <param name="width">width of div</param>
        /// <returns type="object">div-element</returns>
        var d = client.div();
        d.style.width = (width || "100") + "%";
        d.style.height = (height || "100") + "%";
        return d;
    };

    client.divMaxHeightWidth = function () {
        /// <summary>
        /// Creates a div-element with 100% height and width
        /// </summary>
        /// <returns type="object">div-element</returns>
        return client.divHeightWidthByPercent(100, 100);
    };

    client.maskMoneyByElement = function (txtBox) {
        /// <summary>
        /// masks an input[text]-field as moneyMasked TextBox
        /// </summary>
        /// <param name="txtBox"input[text]-field></param>
        if (!txtBox) {
            return;
        }

        $(txtBox).maskMoney({
            symbol: LocalText.Currency,
            showSymbol: true,
            thousands: LocalText.Thousand,
            decimal: LocalText.Decimal,
            symbolStay: true,
            allowNegative: true,
            defaultZero: false,
            allowZero: true
        });
    };

    client.maskMoneyByElementID = function (txtBoxID) {
        /// <summary>
        /// masks an input[text]-field as moneyMasked TextBox
        /// </summary>
        /// <param name="txtBox"input[text]-field ID></param>
        var el = doc.getElementById(txtBoxID);

        client.maskMoneyByElement(el);
    };

    client.maskMoneyTextBox = function () {
        /// <summary>
        /// Creates an input[text]-Field as moneyMasked TextBox
        /// </summary>
        /// <returns type="object">input[text]-element</returns>
        var txt = client.textBox();
        client.maskMoneyByElement(txt);

        return txt;
    };

    client.datePickerByElement = function (txtBox) {
        /// <summary>
        /// formats an input[text]-field as datepicker TextBox
        /// </summary>
        /// <param name="txtBox">input[text]-field</param>
        $(txtBox).datepicker({
            dateFormat: LocalText.DateFormat
        }).maskSimple(LocalText.NumberDateMaskFormat);
    };

    client.datePickerByElementID = function (txtBoxID) {
        /// <summary>
        /// formats an input[text]-field as datepicker TextBox
        /// </summary>
        /// <param name="txtBox">input[text]-field ID</param>
        var el = doc.getElementById(txtBoxID);

        client.datePickerByElement(el);
    };

    client.datePickerTextBox = function () {
        /// <summary>
        /// Creates an input[text]-Field as datepicker TextBox
        /// </summary>
        /// <returns type="object">input[text]-element</returns>
        var txt = client.textBox();
        client.datePickerByElement(txt);

        return txt;
    };
}(window.haushaltsRechner.client = window.haushaltsRechner.client || {}, jQuery));