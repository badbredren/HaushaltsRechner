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

/// <reference path="00-jquery-1.8.1.debug.js" />
/// <reference path="jquery-1.8.1.intellisense.js" />
/// <reference path="01-jquery-ui-1.8.23.debug.js" />
/// <reference path="10-jquery-maskMoney.debug.js" />
/// <reference path="10-jquery.maskedinput-1.3.min.debug.js" />
/// <reference path="15-Localisation.debug.js" />
/// <reference path="20-Json.debug.js" />
/// <reference path="30-Base.debug.js" />
/// <reference path="31-Connections.debug.js" />
/// <reference path="32-GUI.debug.js" />
"use strict";

function getBaseMessageDialog(iconClassName, msg) {
    var doc, div, p, span;

    doc = document;
    div = createDiv(doc);
    p = createParagraph(doc);
    span = createSpan(doc);
    p.appendChild(span);
    p.appendChild(doc.createTextNode(msg));
    div.appendChild(p);

    span.className = iconClassName;

    return div;
}

function informationDialog(title, msg) {
    var div;

    div = getBaseMessageDialog("ui-icon ui-icon-info JQueryUIIcon", msg);
    $(div).dialog({
        modal: true,
        resizable: false,
        height: 250,
        title: title,
        buttons: {
            "OK": function () {
                $(div).dialog("close");
            }
        }
    });
}

function confirmDialog(title, msg, confirmFunction) {
    var div;

    div = getBaseMessageDialog("ui-icon ui-icon-alert JQueryUIIcon", msg);
    
    $(div).dialog({
        modal: true,
        resizable: false,
        height: 250,
        title: title,
        buttons: {
            "Ok": function () {
                confirmFunction();
                $(div).dialog("close");
            },
            "Abbrechen": function () {
                $(div).dialog("close");
            }
        }
    });
}

function errorDialog(title, msg) {
    var div;

    div = getBaseMessageDialog("ui-icon ui-icon-circle-close JQueryUIIcon", msg);

    $(div).dialog({
        modal: true,
        resizable: false,
        height: 250,
        title: title,
        buttons: {
            "Ok": function () {
                $(div).dialog("close");
            }
        }
    });
}

function createBaseDialogDiv(height, width) {
    var div = createDivHeightWidth(document, height, width);
    return div;
}

function createHTMLDialog(width, height, html, title) {
    var div = createBaseDialogDiv(height, width);
    div.innerHTML = html;
    $(div).dialog({
        autoOpen: true,
        title: !isNullOrUndefined(title) ? title : ""
    });
}

function createDialogWithNode(width, height, node, title) {
    var div = createBaseDialogDiv(height, width);
    div.appendChild(node);
    $(div).dialog({
        autoOpen: true,
        title: !isNullOrUndefined(title) ? title : ""
    });
}

function addEditCategoryDialog(id, name) {
    var doc, div, divClear, lblName, txtName;

    doc = document;
    divClear = createClearBothDiv(doc);
    div = createMaxHeightWidthDiv(doc);
    txtName = createTextInput(doc);
    lblName = doc.createTextNode(getLocalText().Category);
}

function createAddEditMovementDialog(
    amount, message, id, accountId, dateAdded, catId, reasId, callback) {
    var doc, divClear, div, txthiddenID, txtBxAmount, lblAmount, selAccount, lblAccount,
        txtDateAdded, lblDateAdded, txtBxMessage, lblMessage, selCategory, 
        lblCategory, selReasons, lblReasons, saveFunction, container;
    doc = document;
    //clear
    divClear = createClearBothDiv(doc);

    //Container
    div = createMaxHeightWidthDiv(doc);

    //ID
    txthiddenID = createHiddenInput(doc);

    //Amount
    txtBxAmount = createMaskMoneyTextInput(doc);
    lblAmount = doc.createTextNode(getLocalText().Amount);

    //Accounts
    selAccount = createSelect(doc);
    lblAccount = doc.createTextNode(getLocalText().Account);

    getAllValidAccounts(function (data) {
        var i, option, oLength;
        for (i = 0; i < data.length; i += 1) {
            option = doc.createElement("option");
            option.innerHTML = data[i].Name;
            option.value = data[i].ID;

            selAccount.appendChild(option);
        }

        if (!isNullOrUndefined(accountId)) {
            oLength = selAccount.options.length;
            for (i = 0; i < oLength; i += 1) {
                if (selAccount.options[i].value === accountId) {
                    selAccount.options[i].selected = true;
                }
                else {
                    selAccount.options[i].selected = false;
                }
            }
        }
    });

    //Datum
    txtDateAdded = createDatePickerTextInput(doc);
    lblDateAdded = doc.createTextNode(getLocalText().MovementDateAdded);

    //Nachricht
    txtBxMessage = createTextInput(doc);
    lblMessage = doc.createTextNode(getLocalText().ReasonOptional);

    //Kategorie
    selCategory = createSelect(doc);
    lblCategory = doc.createTextNode(getLocalText().Category);

    getAllCategories(function (data) {
        var i, option, oLength;
        for (i = 0; i < data.length; i += 1) {
            option = doc.createElement("option");
            option.innerHTML = data[i].NAME;
            option.value = data[i].ID;

            selCategory.appendChild(option);
        }

        if (!isNullOrUndefined(catId)) {
            oLength = selCategory.options.length;
            for (i = 0; i < oLength; i += 1) {
                if (selCategory.options[i].value === catId) {
                    selCategory.options[i].selected = true;
                }
                else {
                    selCategory.options[i].selected = false;
                }
            }
        }
    });

    selReasons = getReasonSelect(function () {
        if (!isNullOrUndefined(reasId)) {
            handleData(reasId, "ReasonService", "GetReasonById", function (data) {
                var r = jQuery.parseJSON(data);

                if (r.NAME !== "") {
                    selReasons.setValue(r.NAME);
                }
            });
        }
    });

    lblReasons = document.createTextNode(getLocalText().Reason);

    div.appendChild(lblAccount);
    div.appendChild(selAccount);

    div.appendChild($.clone(divClear));

    div.appendChild(lblAmount);
    div.appendChild(txtBxAmount);

    div.appendChild($.clone(divClear));

    div.appendChild(lblDateAdded);
    div.appendChild(txtDateAdded);

    div.appendChild($.clone(divClear));

    div.appendChild(lblReasons);
    div.appendChild(selReasons.div());

    div.appendChild($.clone(divClear));

    div.appendChild(lblCategory);
    div.appendChild(selCategory);

    div.appendChild(txthiddenID);

    if (!isNullOrUndefined(id)) {
        txthiddenID.value = id;
    }

    if (!isNullOrUndefined(dateAdded)) {
        txtDateAdded.value = dateAdded;
    }

    if (!isNullOrUndefined(amount)) {
        txtBxAmount.value = amount;
    }

    if (!isNullOrUndefined(message)) {
        txtBxMessage.value = message;
    }

    container = createBaseDialogDiv(350, 350);
    container.appendChild(div);

    saveFunction = function () {
        var newAccId, newCatId, newReasId, o, dateAdded, i;
        newAccId = "";
        newCatId = "";
        newReasId = "";

        for (i = 0; i < selAccount.options.length; i += 1) {
            if (selAccount.options[i].selected) {
                newAccId = selAccount.options[i].value;
                break;
            }
        }

        for (i = 0; i < selCategory.options.length; i += 1) {
            if (selCategory.options[i].selected) {
                newCatId = selCategory.options[i].value;
                break;
            }
        }

        o = {};
        if (txthiddenID.value !== "") {
            o.id = txthiddenID.value;
        }
        o.Amount = convertMoneyToDecimal(txtBxAmount.value);
        o.Message = txtBxMessage.value;

        dateAdded = splitStringToDate(txtDateAdded.value);

        o.DateAdded = dateAdded.month + "/" + dateAdded.day + "/" + dateAdded.year;
        o.CategoryID = newCatId;
        o.AccountID = newAccId;
        o.Reason = selReasons.value();

        if (isNullOrUndefined(id)) {
            handleData(o, "MovementService", "AddMovement", function (msg) {
                callback();
                $(container).dialog('close');
            });
        }
        else {
            handleData(o, "MovementService", "EditMovement", function (msg) {
                callback();
                $(container).dialog('close');
            });
        }
    };

    $(container).dialog({
        autoOpen: true,
        resizable: false,
        width: 350,
        height: 350,
        title: getLocalText().Movement,
        modal: true,
        buttons: {
            "Speichern": saveFunction,
            "Abbrechen": function () {
                $(container).dialog('close');
            }
        }
    });
}

function addEditUserDialog(id, name, sysAdmin, pw) {
    var doc, divClear, div, txthiddenID, txtName, lblName, txtPw, lblpw,
        cbAdmin, lblsysAdmin, container, saveFunction;
    doc = document;
    //clear
    divClear = createClearBothDiv(doc);

    //Container
    div = createMaxHeightWidthDiv(doc);

    //ID
    txthiddenID = createHiddenInput(doc);

    //Name
    txtName = createTextInput(doc);
    lblName = doc.createTextNode(getLocalText().Name);

    //Passwort
    txtPw = createPasswordInput(doc);
    lblpw = doc.createTextNode(getLocalText().Password);

    //SysAdmin
    cbAdmin = createCheckBoxInput(doc);
    lblsysAdmin = doc.createTextNode(getLocalText().SysAdmin);

    //Füllen
    if (!isNullOrUndefined(id)) {
        txthiddenID.value = id;
    }

    if (!isNullOrUndefined(name)) {
        txtName.value = name;
    }

    if (!isNullOrUndefined(pw)) {
        txtPw.value = pw;
    }

    if (!isNullOrUndefined(sysAdmin)) {
        cbAdmin.checked = sysAdmin;
    }

    div.appendChild(lblName);
    div.appendChild(txtName);

    div.appendChild($.clone(divClear));

    div.appendChild(lblpw);
    div.appendChild(txtPw);

    div.appendChild($.clone(divClear));

    div.appendChild(lblsysAdmin);
    div.appendChild(cbAdmin);

    div.appendChild(txthiddenID);

    container = createBaseDialogDiv(350, 350);
    container.appendChild(div);

    saveFunction = function () {
        var o = {};
        o.Name = txtName.value;
        o.Password = txtPw.value;
        o.SysAdmin = cbAdmin.checked.toString();

        if (isNullOrUndefined(id)) {
            handleData(o, "UserService", "AddUser", function (success) {
                if (stringToBoolean(success)) {
                    $(container).dialog('close');
                }
            }, true);
        } else {
            o.id = txthiddenID.value;
            handleData(o, "UserService", "EditUser", function (success) {
                if (stringToBoolean(success)) {
                    $(container).dialog('close');
                }
            }, true);
        }
    };

    $(container).dialog({
        autoOpen: true,
        resizable: false,
        width: 350,
        height: 350,
        title: isNullOrUndefined(id) ? getLocalText().UserNew : getLocalText().UserEdit,
        modal: true,
        buttons: {
            "Speichern": saveFunction,
            "Abbrechen": function () {
                $(container).dialog('close');
            }
        }
    });
}

function editRightsForUser(id, name) {
    var doc, divClear, div, container, saveFunction, rightsReady;
    doc = document;
    //clear
    divClear = createClearBothDiv(doc);

    //Container
    div = createMaxHeightWidthDiv(doc);

    container = createBaseDialogDiv(350, 350);
    container.appendChild(div);

    saveFunction = function () {
        var rights, divs, i, d, txt, cb, o;
        rights = [];
        divs = container.firstChild.firstChild.children;

        for (i = 0; i < divs.length; i += 1) {
            d = divs[i];
            if (d.hasChildNodes()) {
                txt = d.firstChild;
                cb = d.children[0];
                o = {};
                o.ID = cb.value;
                o.Name = txt.nodeValue;
                o.Active = cb.checked;
                rights.push(o);
            }
        }
        editUserRights(id, rights, function (success) {
            $(container).dialog('close');
        });
    };

    $(container).dialog({
        autoOpen: true,
        resizable: false,
        width: 350,
        height: 350,
        title: getLocalText().EditRightsForUser(name),
        modal: true,
        buttons: {
            "Speichern": saveFunction,
            "Abbrechen": function () {
                $(container).dialog('close');
            }
        }
    });

    rightsReady = function (r) {
        var rightsContainer, rights, len, i, right, rightContainer, lblCb, cbRight;

        rightsContainer = createDiv(doc);
        rights = jQuery.parseJSON(r);
        len = rights.length;
        for (i = 0; i < len; i += 1) {
            right = rights[i];

            rightContainer = createDiv(doc);

            lblCb = doc.createTextNode(right.Name);

            cbRight = createCheckBoxInput(doc);
            cbRight.value = right.ID;
            cbRight.checked = right.Active;

            rightContainer.appendChild(lblCb);
            rightContainer.appendChild(cbRight);

            rightsContainer.appendChild(rightContainer);
            rightsContainer.appendChild($.clone(divClear));
        }

        div.appendChild(rightsContainer);
    };

    handleData(id, "UserService", "GetRightsByUserID", rightsReady, true);
}

function addEditAccountDialog(id, name) {
    var doc, divClear, div, txthiddenID, txtName, lblName, container, saveFunction;
    
    doc = document;
    //clear
    divClear = createClearBothDiv(doc);

    //Container
    div = createMaxHeightWidthDiv(doc);

    //ID
    txthiddenID = createHiddenInput(doc);

    //Name
    txtName = createTextInput(doc);
    lblName = doc.createTextNode(getLocalText().Name);

    //Füllen
    if (!isNullOrUndefined(id)) {
        txthiddenID.value = id;
    }

    if (!isNullOrUndefined(name)) {
        txtName.value = name;
    }

    div.appendChild(lblName);
    div.appendChild(txtName);

    div.appendChild(txthiddenID);

    container = createBaseDialogDiv(350, 350);
    container.appendChild(div);

    saveFunction = function () {
        var o = {};
        o.Name = txtName.value;

        if (isNullOrUndefined(id)) {
            handleData(o, "AccountService", "AddAccount", function (success) {
                if (stringToBoolean(success)) {
                    $(container).dialog('close');
                }
            }, true);
        } else {
            o.id = txthiddenID.value;
            handleData(o, "AccountService", "EditAccount", function (success) {
                if (stringToBoolean(success)) {
                    $(container).dialog('close');
                }
            }, true);
        }
    };

    $(container).dialog({
        autoOpen: true,
        resizable: false,
        width: 350,
        height: 350,
        title: isNullOrUndefined(id) ? getLocalText().AccountNew : getLocalText().AccountEdit,
        modal: true,
        buttons: {
            "Speichern": saveFunction,
            "Abbrechen": function () {
                $(container).dialog('close');
            }
        }
    });
}

function editAccountUsersDialog(id, name) {

    var usersReady, saveFunction, doc, div, divClear, container;

    doc = document;

    //clear
    divClear = createClearBothDiv(doc);

    //Container
    div = createMaxHeightWidthDiv(doc);

    container = createBaseDialogDiv(350, 350);
    container.appendChild(div);

    saveFunction = function () {
        var users, divs, i, d, txt, cb, o;
        users = [];
        divs = container.firstChild.firstChild.children;

        for (i = 0; i < divs.length; i += 1) {
            d = divs[i];
            if (d.hasChildNodes()) {
                txt = d.firstChild;
                cb = d.children[0];
                o = {};
                o.ID = cb.value;
                o.Name = txt.nodeValue;
                o.InAccount = cb.checked;
                users.push(o);
            }
        }
        editAccountUsers(id, users, function (success) {
            $(container).dialog('close');
        });
    };

    $(container).dialog({
        autoOpen: true,
        resizable: false,
        width: 350,
        height: 350,
        title: getLocalText().EditUsersForAccount(name),
        modal: true,
        buttons: {
            "Speichern": saveFunction,
            "Abbrechen": function () {
                $(container).dialog('close');
            }
        }
    });


    usersReady = function (u) {
        var users, len, i, user, usersContainer, userContainer, lblcb, cbUser;
        
        users = jQuery.parseJSON(u);
        usersContainer = createDiv(doc);

        len = users.length;

        for (i = 0; i < len; i++) {
            user = users[i];
            userContainer = createDiv(doc);

            lblcb = doc.createTextNode(user.Name);
            cbUser = createCheckBoxInput(doc);
            cbUser.value = user.ID;
            cbUser.checked = user.InAccount;
           
            userContainer.appendChild(lblcb);
            userContainer.appendChild(cbUser);

            usersContainer.appendChild(userContainer);
            usersContainer.appendChild($.clone(divClear));
        }

        div.appendChild(usersContainer);
    };

    handleData(id, "AccountService", "GetAccountUsers", usersReady, true);
}