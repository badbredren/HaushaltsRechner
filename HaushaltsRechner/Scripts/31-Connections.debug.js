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
"use strict";

function handleData(data, serviceName, methodName, callback, admin) {

    var svc = "../Services/" + serviceName + ".asmx/" + methodName;
    svc = admin ? "../" + svc : svc;
    $.ajax({
        type: "POST",
        url: svc,
        data: "{'data':'" + JSON.stringify(data) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            callback(msg.d);
        }
    });
}

function getAllCategories(callback) {
    $.ajax({
        type: "POST",
        url: "../Services/CategoryService.asmx/GetAllCategories",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            callback(JSON.parse(msg.d));
        }
    });
}

function getAllValidAccounts(callback) {
    $.ajax({
        type: "POST",
        url: "../Services/AccountService.asmx/GetAllValidAccounts",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            callback(JSON.parse(msg.d));
        }
    });
}

function getReasons(pre, callback) {
    $.ajax({
        type: "POST",
        url: "../Services/ReasonService.asmx/GetReasons",
        data: "{'pre': '" + pre + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            callback(JSON.parse(msg.d));
        }
    });
}

function editUserRights(userId, rights, callback) {
    $.ajax({
        type: "POST",
        url: "../../Services/UserService.asmx/EditUserRights",
        data: "{'userID': '" + userId + "', 'rights': '" + JSON.stringify(rights) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            callback(msg.d);
        }
    });
}

function editAccountUsers(accountId, users, callback) {
    $.ajax({
        type: "POST",
        url: "../../Services/AccountService.asmx/EditAccountUser",
        data: "{'accountID': '" + accountId + "', 'users': '" + JSON.stringify(users) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            callback(msg.d);
        }
    });
}

function deleteUser(id, callback) {
    confirmDialog("Nutzer löschen", "Wollen Sie wirklich den ausgewählten Nutzer löschen?", function () {
        handleData(id, "UserService", "DeleteUser", function (msg) { callback(msg); }, true);
    });
}

function deleteMovement(id, callback) {
    confirmDialog(getLocalText().DeleteText, getLocalText().DeleteMovementConfirm, function () {
        handleData(id, "MovementService", "DeleteMovement", function (msg) { callback(msg); }, true);
    });
}

function deleteAccount(id, callback) {
    confirmDialog(getLocalText().DeleteText, getLocalText().DeleteAccountConfirm, function () {
        handleData(id, "AccountService", "DeleteAccount", function (msg) { callback(msg); }, true);
    });
}