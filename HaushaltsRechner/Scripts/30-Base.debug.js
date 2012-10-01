﻿/*
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
/// <reference path="20-Json.debug.js" />

"use strict";
$(document).on({
    ajaxStart: function () {
        if (getFilenameOfUrl().indexOf("Admin") > -1) {
            document.getElementById("AjaxLoaderGifAdmin").style.visibility = "visible";
            document.getElementById("AjaxLoaderGifStandard").style.visibility = "hidden";
        }
        else {
            document.getElementById("AjaxLoaderGifStandard").style.visibility = "visible";
            document.getElementById("AjaxLoaderGifAdmin").style.visibility = "hidden";
        }

        $('#AjaxLoaderGifContainer').show();
    },
    ajaxStop: function () {
        $('#AjaxLoaderGifContainer').hide();
    }
});

function Observer(val) {
    var callbacks = [],
        value = val !== undefined ? val : null;

    return {
        subscribe: function (callback) {
            if (callbacks.indexOf(callback) === -1) {
                callbacks.push(callback);
            }
        },
        unsubscribe: function (callback) {
            var index = callbacks.indexOf(callback);
            if (index !== -1) {
                callbacks.splice(index, 1);
            }
        },
        notifyChange: function (val) {
            if (val === value) {
                return;
            }

            value = val;

            var len = callbacks.length;
            for (var i = 0; i < len; i++) {
                callbacks[i](val);
            }
        }
    };
}

function getUrl() {
    return document.URL;
}

function getUrlParamless() {
    var url = getUrl();
    if (url.indexOf("?") > -1) {
        return url.substr(0, url.indexOf("?") + 1);
    }

    return url;
}

function getFilenameOfUrl() {
    var url = getUrl();
    return url.substring(url.lastIndexOf('/') + 1);
}

function stringToBoolean(str) {
    var val, ret;
    val = str.toLowerCase();
    ret = false;

    switch (val) {
    case "1":
        ret = true;
        break;
    case "true":
        ret = true;
        break;
    case "ja":
        ret = true;
        break;
    default:
        ret = false;
        break;
    }
    return ret;
}

function isNullOrUndefined(val) {
    if (val === undefined || val === null) {
        return true;
    }

    return false;
}

function getRandomString() {
    var chars, stringLength, randomstring, i, rnum;
    chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
    stringLength = 8;
    randomstring = '';
    for (i = 0; i < stringLength; i += 1) {
        rnum = Math.floor(Math.random() * chars.length);
        randomstring += chars.substring(rnum, rnum + 1);
    }
    return randomstring;
}

function createGuid() {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

function splitStringToDate(val) {
    var day, month, year, date;
    if (isNullOrUndefined(val) || val === "") {
        return null;
    }

    day = val.substring(0, val.indexOf("."));
    val = val.substring(val.indexOf(".") + 1);
    month = val.substring(0, val.indexOf("."));
    val = val.substring(val.indexOf(".") + 1);
    year = val;

    date = {};
    date.day = day;
    date.month = month;
    date.year = year;

    return date;
}

function convertMoneyToDecimal(val) {
    return val.replace(getLocalText().Currency, "").replace(getLocalText().Thousand, ".");
}