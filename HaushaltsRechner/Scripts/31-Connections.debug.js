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