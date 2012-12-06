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
/// <reference path="31-Connections.debug.js" />
/// <reference path="32-GUI.debug.js" />
"use strict";

(function (dialogs, $, undefined) {
    var hr = haushaltsRechner,
        con = hr.connections,
        client = hr.client,
        grids = client.grids;

    //private functions
    function getBaseMessageDialog(iconClassName, msg) {
        /// <summary>
        /// Creates base dialog with iconClassName (uses Sprites) and the given message
        /// </summary>
        /// <param name="iconClassName">icon to show</param>
        /// <param name="msg">message to show</param>
        /// <returns type="object">div-element as jQuery-UI dialog</returns>
        var div = client.div(),
            p = client.paragraph(),
            span = client.spanWithClass(iconClassName);

        p.appendChild(span);
        p.appendChild(client.label(msg));
        div.appendChild(p);

        return div;
    }

    function createHTMLDialog(width, height, html, title) {
        /// <summary>
        /// Creates dialog with the given width, heigt, html and title
        /// </summary>
        /// <param name="width">Width of dialog</param>
        /// <param name="height">Height of dialog</param>
        /// <param name="html">innerHtml of dialog</param>
        /// <param name="title">title of dialog</param>
        var div = client.divHeightWidthByPx(height, width);
        div.innerHTML = html;
        $(div).dialog({
            autoOpen: true,
            title: title || ""
        });
    }

    function createDialogWithNode(width, height, node, title) {
        /// <summary>
        /// Creates dialog with the given width, heigt, html and title
        /// </summary>
        /// <param name="width">Width of dialog</param>
        /// <param name="height">Height of dialog</param>
        /// <param name="node">childNode for dialog</param>
        /// <param name="title">title of dialog</param>
        var div = client.divHeightWidthByPx(height, width);

        if (node) {
            div.appendChild(node);
        }

        $(div).dialog({
            autoOpen: true,
            title: title || "",
            width: width,
            height: height,
            close: function (event, ui) {
                client.deleteChildNodes(div);
            }
        });
    }

    function createSaveCancelButtons(saveCallback, cancelCallback) {
        /// <summary>
        /// Creates an object with save and cancel properties
        /// </summary>
        /// <param name="saveCallback">
        /// callback-function: will be called, if [save] button is clicked
        /// </param>
        /// <param name="cancelCallback">
        /// callback-function: will be called, if [cancel] button is clicked
        /// </param>
        var saveText, cancelText, i18nButtons;

        saveText = LocalText.Save;
        cancelText = LocalText.Cancel;

        i18nButtons = {};
        i18nButtons[saveText] = saveCallback;
        i18nButtons[cancelText] = cancelCallback;

        return i18nButtons;
    }

    //public functions
    dialogs.information = function (title, msg) {
        /// <summary>
        /// Creates a simple information dialog
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="msg">message</param>
        var div = getBaseMessageDialog("ui-icon ui-icon-info JQueryUIIcon", msg),
            ok = LocalText.OK,
            i18nbuttons = {};

        i18nbuttons[ok] = function () {
            $(div).dialog("close");
        };

        $(div).dialog({
            modal: true,
            resizable: false,
            height: 250,
            title: title,
            buttons: i18nbuttons
        });
    };

    dialogs.confirm = function (title, msg, confirmFunction) {
        /// <summary>
        /// Creates a simple conformation dialog
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="msg">message</param>
        /// <param name="confirmFunction">
        /// callback-function: is excecuted, when [OK] is clicked
        /// </param>
        var div = getBaseMessageDialog("ui-icon ui-icon-info JQueryUIIcon", msg),
            ok = LocalText.OK,
            cancel = LocalText.Cancel,
            i18nbuttons = {};

        i18nbuttons[ok] = function () {
            confirmFunction();
            $(div).dialog("close");
        };
        
        i18nbuttons[cancel] = function () {
            $(div).dialog("close");
        };

        $(div).dialog({
            modal: true,
            resizable: false,
            height: 250,
            title: title,
            buttons: i18nbuttons
        });
    };

    dialogs.error = function (title, msg) {
        /// <summary>
        /// Creates a simple error dialog
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="msg">mesasge</param>
        var div = getBaseMessageDialog("ui-icon ui-icon-circle-close JQueryUIIcon", msg),
            ok = LocalText.OK,
            i18nbuttons = {};

        i18nbuttons[ok] = function () {
            $(div).dialog("close");
        };

        $(div).dialog({
            modal: true,
            resizable: false,
            height: 250,
            title: title,
            buttons: i18nbuttons
        });
    };

    dialogs.addEditMovement = function (
        amount,
        message,
        id,
        accountId,
        dateAdded,
        catId,
        reasId,
        callback) {
        /// <summary>
        /// Creates a dialog to add or edit movements
        /// </summary>
        /// <param name="amount">start amount, if set</param>
        /// <param name="message">start message, if set</param>
        /// <param name="id">if set, movement will be edited</param>
        /// <param name="accountId">start accountID, if set</param>
        /// <param name="dateAdded">start dateAdded, if set</param>
        /// <param name="catId">start categoryID, if set</param>
        /// <param name="reasId">start reasonID, if set</param>
        /// <param name="callback">
        /// callback-function: will be excecuted, when movement is saved
        /// </param>
        var divClear, div, txtBxAmount, lblAmount, selAccount, lblAccount,
            txtDateAdded, lblDateAdded, txtBxMessage, lblMessage, selCategory,
            lblCategory, txtReasons, lblReasons, saveFunction,
            container, autoCompleteReasonSource, i18nButtons;
        //clear
        divClear = client.divClearBoth();

        //Container
        div = client.divMaxHeightWidth();

        //Amount
        txtBxAmount = client.maskMoneyTextBox();
        lblAmount = client.label(LocalText.Amount);

        //Accounts
        selAccount = client.select();
        lblAccount = client.label(LocalText.Account);

        con.getAllValidAccounts(function (data) {
            var i, option, oLength, dLength;
            for (i = 0, dLength = data.length; i < dLength; i += 1) {
                option = client.option();
                option.innerHTML = data[i].Name;
                option.value = data[i].ID;

                selAccount.appendChild(option);
            }

            if (accountId) {
                for (i = 0, oLength = selAccount.options.length; i < oLength; i += 1) {
                    if (selAccount.options[i].value === accountId) {
                        selAccount.options[i].selected = true;
                    }
                    else {
                        selAccount.options[i].selected = false;
                    }
                }
            }
        });

        //Date
        txtDateAdded = client.datePickerTextBox();
        lblDateAdded = client.label(LocalText.MovementDateAdded);

        //Nachricht
        txtBxMessage = client.textBox();
        lblMessage = client.label(LocalText.ReasonOptional);

        //Kategorie
        selCategory = client.select();
        lblCategory = client.label(LocalText.Category);

        con.getAllCategories(function (data) {
            var i, option, dLength, oLength;
            for (i = 0, dLength = data.length; i < dLength; i += 1) {
                option = client.option();
                option.innerHTML = data[i].NAME;
                option.value = data[i].ID;

                selCategory.appendChild(option);
            }

            if (catId) {
                for (i = 0, oLength = selCategory.options.length; i < oLength; i += 1) {
                    selCategory.options[i].selected = 
                        selCategory.options[i].value === catId;
                }
            }
        });

        txtReasons = client.textBox();

        autoCompleteReasonSource = function (request, response) {
            con.getReasons(request.term, function (names) {
                response($.map(names, function (item) {
                    return {
                        label: item.TEXT,
                        value: item.TEXT
                    };
                }));
            });
        };

        $(txtReasons).autocomplete({
            source: autoCompleteReasonSource,
            minLength: 1,
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        });

        lblReasons = client.label(LocalText.Reason);

        $(div).append(
            lblAccount, selAccount, $.clone(divClear),
            lblAmount, txtBxAmount, $.clone(divClear),
            lblDateAdded, txtDateAdded, $.clone(divClear),
            lblReasons, txtReasons, $.clone(divClear),
            lblCategory, selCategory);

        txtDateAdded.value = dateAdded || null;
        txtBxAmount.value = amount || null;
        txtBxMessage.value = message || null;

        container = client.divHeightWidthByPx(350, 350);
        container.appendChild(div);

        saveFunction = function () {
            var newAccId = "",
                newCatId = "",
                newReasId = "",
                obj = {},
                dateAdded, i, aLength;

            for (i = 0, aLength = selAccount.options.length; i < aLength; i += 1) {
                if (selAccount.options[i].selected) {
                    newAccId = selAccount.options[i].value;
                    break;
                }
            }

            for (i = 0, aLength = selCategory.options.length; i < aLength; i += 1) {
                if (selCategory.options[i].selected) {
                    newCatId = selCategory.options[i].value;
                    break;
                }
            }

            obj.Amount = hr.convertMoneyToDecimal(txtBxAmount.value);
            obj.Message = txtBxMessage.value;

            dateAdded = hr.splitStringToDate(txtDateAdded.value);

            obj.DateAdded = dateAdded.month + "/" + dateAdded.day + "/" + dateAdded.year;
            obj.CategoryID = newCatId;
            obj.AccountID = newAccId;
            obj.Reason = txtReasons.value;

            if (!id) {
                con.handleData(obj, "MovementService", "AddMovement", function (msg) {
                    callback();
                    $(container).dialog('close');
                });
            }
            else {
                obj.id = id;
                con.handleData(obj, "MovementService", "EditMovement", function (msg) {
                    callback();
                    $(container).dialog('close');
                });
            }
        };
        
        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: 350,
            height: 350,
            title: LocalText.Movement,
            modal: true,
            buttons: i18nButtons
        });
    };

    dialogs.addEditUser = function (id, name, sysAdmin, pw) {
        /// <summary>
        /// Creates a dialog to add or edit users
        /// </summary>
        /// <param name="id">if set, user will be edited</param>
        /// <param name="name">start name, if set</param>
        /// <param name="sysAdmin">start System Admin flag, if set</param>
        /// <param name="pw">start password if set</param>
        var divClear, div, txtName, lblName, txtPw, lblpw,
            cbAdmin, lblsysAdmin, container, saveFunction, i18nButtons;
        //clear
        divClear = client.divClearBoth();

        //Container
        div = client.divMaxHeightWidth();

        //Name
        txtName = client.textBox();
        lblName = client.label(LocalText.Name);

        //Passwort
        txtPw = client.passwordTextBox();
        lblpw = client.label(LocalText.Password);

        //SysAdmin
        cbAdmin = client.checkBox();
        lblsysAdmin = client.label(LocalText.SysAdmin);

        //Füllen
        txtName.value = name || null;
        txtPw.value = pw || null;
        cbAdmin.checked = sysAdmin || false;

        $(div).append(lblName, txtName, $.clone(divClear), lblsysAdmin, cbAdmin);

        container = client.divHeightWidthByPx(350, 350);
        container.appendChild(div);

        saveFunction = function () {
            var obj = {};
            obj.Name = txtName.value;
            obj.Password = txtPw.value;
            obj.SysAdmin = cbAdmin.checked.toString();

            if (!id) {
                con.handleData(obj, "UserService", "AddUser", function (success) {
                    if (hr.stringToBoolean(success)) {
                        $(container).dialog('close');
                    }
                }, true);
            } else {
                obj.id = id;
                con.handleData(obj, "UserService", "EditUser", function (success) {
                    if (hr.stringToBoolean(success)) {
                        $(container).dialog('close');
                    }
                }, true);
            }
        };
        
        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: 350,
            height: 350,
            title: !id ? LocalText.UserNew : LocalText.UserEdit,
            modal: true,
            buttons: i18nButtons
        });
    };

    dialogs.editRightsForUser = function (id, name) {
        /// <summary>
        /// Creates a dialog to edit rights for a user
        /// </summary>
        /// <param name="id">userID</param>
        /// <param name="name">the name of the user</param>
        var divClear, div, container, saveFunction, rightsReady, i18nButtons;

        //clear
        divClear = client.divClearBoth();

        //Container
        div = client.divMaxHeightWidth();

        container = client.divHeightWidthByPx(350, 350);
        container.appendChild(div);

        saveFunction = function () {
            var rights, divs, i, d, obj, len, cb;
            rights = [];
            divs = container.firstChild.firstChild.children;

            for (i = 0, len = divs.length; i < len; i += 1) {
                d = divs[i];
                if (d.hasChildNodes()) {
                    cb = d.children[0];
                    obj = {};
                    obj.ID = cb.value;
                    obj.Name = d.firstChild.nodeValue;
                    obj.Active = cb.checked;
                    rights.push(obj);
                }
            }
            con.editUserRights(id, rights, function (success) {
                $(container).dialog('close');
            });
        };

        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: 350,
            height: 350,
            title: LocalText.EditRightsForUser(name),
            modal: true,
            buttons: i18nButtons
        });

        rightsReady = function (r) {
            var rightsContainer, rights, len, i, right, rightContainer, lblCb, cbRight;

            rightsContainer = client.div();
            rights = $.parseJSON(r);

            for (i = 0, len = rights.length; i < len; i += 1) {
                right = rights[i];

                rightContainer = client.div();

                lblCb = client.label(right.Name);

                cbRight = client.checkBox();
                cbRight.value = right.ID;
                cbRight.checked = right.Active;

                $(rightContainer).append(lblCb, cbRight);
                $(rightsContainer).append(rightContainer, $.clone(divClear));
            }

            div.appendChild(rightsContainer);
        };

        con.handleData(id, "UserService", "GetRightsByUserID", rightsReady, true);
    };

    dialogs.addEditAccount = function (id, name) {
        /// <summary>
        /// Creates a dialog to add or edit an account
        /// </summary>
        /// <param name="id">ID of the account</param>
        /// <param name="name">name of the account</param>
        var divClear, div, txtName, lblName, container, saveFunction, i18nButtons;

        //clear
        divClear = client.divClearBoth();

        //Container
        div = client.divMaxHeightWidth();

        //Name
        txtName = client.textBox();
        lblName = client.label(LocalText.Name);

        //Füllen
        txtName.value = name || null;

        $(div).append(lblName, txtName);

        container = client.divHeightWidthByPx(350, 350);
        container.appendChild(div);

        saveFunction = function () {
            var o = {};
            o.Name = txtName.value;

            if (!id) {
                con.handleData(o, "AccountService", "AddAccount", function (success) {
                    if (hr.stringToBoolean(success)) {
                        $(container).dialog('close');
                    }
                }, true);
            } else {
                o.id = id;
                con.handleData(o, "AccountService", "EditAccount", function (success) {
                    if (hr.stringToBoolean(success)) {
                        $(container).dialog('close');
                    }
                }, true);
            }
        };

        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: 350,
            height: 350,
            title: !id ? LocalText.AccountNew : LocalText.AccountEdit,
            modal: true,
            buttons: i18nButtons
        });
    };

    dialogs.category = function () {
        /// <summary>
        /// Creates a dialog to add and edit categories
        /// </summary>
        var divClear = client.divClearBoth(),
            div = client.divMaxHeightWidth(),
            txtNameNew = client.textBox(),
            txtNameEdit = client.textBox(),
            lbCategories = client.select(),
            lblName = client.label(LocalText.Name),
            container = client.divHeightWidthByPx(height, width),
            h3New = client.headlineHtml(3, LocalText.CategoryNew),
            h3Edit = client.headlineHtml(3, LocalText.CategoryEdit),
            divNew = client.div(),
            divEdit = client.div(),
            height = 500,
            width = 500,
            saveFunction, i18nButtons;

        con.getAllCategories(function (vals) {
            var i, option, dLength;

            for (i = 0, dLength = vals.length; i < dLength; i += 1) {
                option = client.option();
                option.innerHTML = vals[i].NAME;
                option.value = vals[i].ID;

                lbCategories.appendChild(option);
            }

            txtNameEdit.value = lbCategories.options[lbCategories.selectedIndex].text;
        });

        $(divNew).append($.clone(lblName), txtNameNew);
        $(divEdit).append(lbCategories, $.clone(lblName), txtNameEdit);

        lbCategories.onchange = function () {
            txtNameEdit.value = lbCategories.options[lbCategories.selectedIndex].text;
        };

        $(div).append(
            h3New,
            divNew,
            h3Edit,
            divEdit
            );

        $(div).accordion();

        divNew.style.height = "100px";
        divEdit.style.height = "100px";

        container.appendChild(div);

        saveFunction = function () {
            var o = {},
                activeIndex = $(div).accordion("option", "active");

            if (activeIndex === 0) {
                o.Name = txtNameNew.value;
                con.handleData(o, "CategoryService", "AddCategory", function (success) {
                    if (hr.stringToBoolean(success)) {
                        $(container).dialog('close');
                    }
                }, false);
            } else {
                o.NAME = txtNameEdit.value;
                o.ID = lbCategories.options[lbCategories.selectedIndex].value;
                con.handleData(o, "CategoryService", "EditCategory", function (success) {
                    if (hr.stringToBoolean(success)) {
                        $(container).dialog('close');
                    }
                }, false);
            }
        };

        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: width,
            height: height,
            title: LocalText.Category,
            modal: true,
            buttons: i18nButtons
        });
    };

    dialogs.editAccountUsers = function (id, name) {
        /// <summary>
        /// Creates a dialog to edit users of an account
        /// </summary>
        /// <param name="id">ID of the account</param>
        /// <param name="name">name of the account</param>
        var usersReady, saveFunction, div, divClear, container, i18nButtons;

        //clear
        divClear = client.divClearBoth();

        //Container
        div = client.divMaxHeightWidth();

        container = client.divHeightWidthByPx(350, 350);
        container.appendChild(div);

        saveFunction = function () {
            var users, divs, len, i, d, obj, cb;
            users = [];
            divs = container.firstChild.firstChild.children;

            for (i = 0, len = divs.length; i < len; i += 1) {
                d = divs[i];
                if (d.hasChildNodes()) {
                    cb = d.children[0];
                    obj = {};
                    obj.ID = cb.value;
                    obj.Name = d.firstChild.nodeValue;
                    obj.InAccount = cb.checked;
                    users.push(obj);
                }
            }
            con.editAccountUsers(id, users, function (success) {
                $(container).dialog('close');
            });
        };

        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: 350,
            height: 350,
            title: LocalText.EditUsersForAccount(name),
            modal: true,
            buttons: i18nButtons
        });


        usersReady = function (u) {
            var users, len, i, user, usersContainer, userContainer, lblcb, cbUser;

            users = $.parseJSON(u);
            usersContainer = client.div();

            for (i = 0, len = users.length; i < len; i += 1) {
                user = users[i];
                userContainer = client.div();

                lblcb = client.label(user.Name);
                cbUser = client.checkBox();
                cbUser.value = user.ID;
                cbUser.checked = user.InAccount;

                $(userContainer).append(lblcb, cbUser);
                $(usersContainer).append(userContainer, $.clone(divClear));
            }

            div.appendChild(usersContainer);
        };

        con.handleData(id, "AccountService", "GetAccountUsers", usersReady, true);
    };

    dialogs.showMovements = function (movementsArray) {
        /// <summary>
        /// Creates a dialog to show movements in a grid
        /// </summary>
        /// <param name="movementsArray">array of movements to show</param>
        var container = client.divHeightWidthByPx(550, 850),
        divData = client.divWithClass("MovementsGridData"),
        divPager = client.divWithClass("MovementsGridPager");

        if (!movementsArray ||
            !movementsArray.length ||
            movementsArray.length <= 0) {
            return;
        }

        divData.id = "ReportingMovementsGridData";
        divPager.id = "ReportingMovementsGridPager";

        $(container).append(divData, divPager);

        createDialogWithNode(850, 550, container, movementsArray[0].CategoryName);

        grids.createGrid(
            "ReportingMovementsGridData",
            "ReportingMovementsGridPager",
            movementsArray);
    };

    dialogs.userOptions = function () {
        /// <summary>
        /// Creates a dialog to edit user options
        /// </summary>
        var divClear = client.divClearBoth(),
            div = client.divMaxHeightWidth(),
            txtName = client.textBox(),
            containerPw = client.div(),
            jQcontainerPw = $(containerPw),
            containerPwHidden = client.div(),
            jQcontainerPwHidden = $(containerPwHidden),
            lblPwContainer = client.label(LocalText.Password),
            txtPw = client.passwordTextBox(),
            txtPwNew = client.passwordTextBox(),
            txtPwNewConfirm = client.passwordTextBox(),
            editIcon = client.divWithClass("Edit3232"),
            lblName = client.label(LocalText.Name),
            lblPw = client.label(LocalText.PasswordOld),
            lblPwNew = client.label(LocalText.PasswordNew),
            lblPwNewConfirm = client.label(LocalText.Confirm),
            lblLanguages = client.label(LocalText.Language),
            lbLanguages = client.select(),
            container = client.divHeightWidthByPx(height, width),
            height = 200,
            width = 200,
            insertUserData,
            saveFunction,
            id,
            i18nButtons;

        insertUserData = function (data) {
            var user = $.parseJSON(data), i, len;

            txtName.value = user.Name;
            txtPw.value = user.Password;

            if (user.Language) {
                for (i = 0, len = lbLanguages.options.length; i < len; i += 1) {
                    lbLanguages.options[i].selected =
                        lbLanguages.options[i].value === user.Language;
                }
            }

            id = user.ID;
        };

        saveFunction = function () {
            var obj = {}, i, len;
            if (!jQcontainerPw.is(":hidden")) {
                if (txtPwNew.value !== txtPwNewConfirm.value) {
                    dialogs.error(LocalText.Error, localText.ErrorPasswordsMatch);
                    return;
                }

                obj.Password = txtPwNew.value;
            }

            if (!txtName.value) {
                dialogs.error(LocalText.Error, LocalText.ErrorNameEmpty);
                return;
            }

            obj = {
                ID: id,
                Name: txtName.value
            };

            for (i = 0, len = lbLanguages.options.length; i < len; i += 1) {
                if (lbLanguages.options[i].selected) {
                    obj.Language = lbLanguages.options[i].value;
                    break;
                }
            }

            con.handleData(obj, "UserService", "ChangeUser", function (msg) {
                if (!msg) {
                    $(container).dialog('close');
                    document.location.reload(true);
                }
                else {
                    dialogs.error(LocalText.Error, LocalText.ServerError);
                }
            }, false);
        };

        con.getLanguages(function (data) {
            var i, len = data.length, option;

            for (i = 0; i < len; i++) {
                option = client.option();

                option.innerHTML = data[i].Name;
                option.value = data[i].Value;
                lbLanguages.appendChild(option);
            }

            con.handleData("", "UserService", "GetUserData", insertUserData, false);
        });

        txtPw.disabled = true;

        containerPw.style.display = "none";
        containerPw.style.width = "100%";

        $(editIcon).click(function () {
            if (jQcontainerPw.is(":hidden")) {
                jQcontainerPw.show();
                jQcontainerPwHidden.hide();
            } else {
                jQcontainerPw.hide();
                jQcontainerPwHidden.show();
            }
        });

        jQcontainerPwHidden.append(
            lblPwContainer, editIcon
        );

        jQcontainerPw.append(
            lblPw, txtPw, $.clone(divClear),
            lblPwNew, txtPwNew, $.clone(divClear),
            lblPwNewConfirm, txtPwNewConfirm);

        $(div).append(
            lblName, txtName, $.clone(divClear),
            containerPw, containerPwHidden, $.clone(divClear),
            lblLanguages, lbLanguages);

        container.appendChild(div);

        i18nButtons = createSaveCancelButtons(
            saveFunction,
            function () {
                $(container).dialog('close');
            });

        $(container).dialog({
            autoOpen: true,
            resizable: false,
            width: 350,
            height: 350,
            title: LocalText.UserEdit,
            modal: true,
            buttons: i18nButtons
        });
    };
}(window.haushaltsRechner.client.dialogs = window.haushaltsRechner.client.dialogs || {}, jQuery));