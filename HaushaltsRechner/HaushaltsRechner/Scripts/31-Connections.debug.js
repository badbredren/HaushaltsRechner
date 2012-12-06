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
"use strict";

(function (connections, $, undefined) {

    var doc = document,
        hr = haushaltsRechner,
        con = hr.connections,
        client = hr.client,
        dialogs = client.dialogs;

    connections.handleData = function (data, serviceName, methodName, callback, admin) {
        /// <summary>
        /// base function to handle calls to webservices
        /// </summary>
        /// <param name="data">sendable data for the server</param>
        /// <param name="serviceName">Name of the Service-class on the Server</param>
        /// <param name="methodName">Method name to call of the class</param>
        /// <param name="callback">function, which handles the returning data</param>
        /// <param name="admin">if true, Service was called from admin folder</param>
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
    };

    connections.getAllCategories = function (callback) {
        /// <summary>
        /// Get all categories from the server
        /// </summary>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        $.ajax({
            type: "POST",
            url: "../Services/CategoryService.asmx/GetAllCategories",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                callback($.parseJSON(msg.d));
            }
        });
    };

    connections.getAllValidAccounts = function (callback) {
        /// <summary>
        /// Gets all Accounts of the current User
        /// </summary>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        $.ajax({
            type: "POST",
            url: "../Services/AccountService.asmx/GetAllValidAccounts",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                callback($.parseJSON(msg.d));
            }
        });
    };

    connections.getReasons = function (pre, callback) {
        /// <summary>
        /// Gets the Reasons with the specified starting chars in pre
        /// </summary>
        /// <param name="pre">starting chars of reasons</param>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        $.ajax({
            type: "POST",
            url: "../Services/ReasonService.asmx/GetReasons",
            data: "{'pre': '" + pre + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                callback($.parseJSON(msg.d));
            }
        });
    };

    connections.editUserRights = function (userId, rights, callback) {
        /// <summary>
        /// edits the rights for the user with the given ID
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <param name="rights">Rights to edit</param>
        /// <param name="callback">callback-Function, should accept one parameter</param>
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
    };

    connections.editAccountUsers = function (accountId, users, callback) {
        /// <summary>
        /// Edit users for an account
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="users">users</param>
        /// <param name="callback">callback-Function, should accept one parameter</param>
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
    };

    connections.deleteUser = function (id, callback) {
        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        dialogs.confirm(
            LocalText.UserDelete,
            LocalText.UserDeleteConfirm,
            function () {
                con.handleData(
                    id,
                    "UserService",
                    "DeleteUser",
                    function (msg) { callback(msg); }, true);
            });
    };

    connections.deleteMovement = function (id, callback) {
        /// <summary>
        /// Deletes a movement
        /// </summary>
        /// <param name="id">Movement ID</param>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        dialogs.confirm(LocalText.DeleteText, LocalText.DeleteMovementConfirm, function () {
            con.handleData(
                id,
                "MovementService",
                "DeleteMovement",
                function (msg) { callback(msg); }, true);
        });
    };

    connections.deleteAccount = function (id, callback) {
        /// <summary>
        /// Deletes an Account
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        dialogs.confirm(LocalText.DeleteText, LocalText.DeleteAccountConfirm, function () {
            con.handleData(
                id,
                "AccountService",
                "DeleteAccount",
                function (msg) { callback(msg); }, true);
        });
    };

    connections.getReportingTypes = function (callback) {
        /// <summary>
        /// Gets Reporting types
        /// </summary>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        $.ajax({
            type: "POST",
            url: "../Services/ReportingService.asmx/GetReportingTypes",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                callback($.parseJSON(msg.d));
            }
        });
    };

    connections.getReportingDirectionTypes = function (callback) {
        /// <summary>
        /// Gets Reporting direction types
        /// </summary>
        /// <param name="callback">callback-Function, should accept one parameter</param>
        $.ajax({
            type: "POST",
            url: "../Services/ReportingService.asmx/GetReportingDirectionTypes",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                callback($.parseJSON(msg.d));
            }
        });
    };

    connections.loginUser = function (name, password) {
        /// <summary>
        /// Login an user
        /// If login successfull, relocate to Uebersicht.aspx
        /// </summary>
        /// <param name="name">Name of the user</param>
        /// <param name="password">Password of the user</param>
        var o = {
            NAME: name,
            Password: password
        };

        connections.handleData(o, "UserService", "UserLogin", function (msg) {
            if (hr.stringToBoolean(msg)) {
                window.location = 'Uebersicht.aspx';
            }
        });
    };

    connections.getLanguages = function (callback) {
        connections.handleData("", "CommonService", "GetLanguages", function (msg) {
            callback($.parseJSON(msg));
        }, false);
    };
}(window.haushaltsRechner.connections = window.haushaltsRechner.connections || {}, jQuery));