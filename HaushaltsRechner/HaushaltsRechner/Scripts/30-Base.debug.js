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
/// <reference path="20-Json.debug.js" />

"use strict";

(function (haushaltsRechner, $, undefined) {
    var doc = document;

    haushaltsRechner.observer = function (val) {
        /// <summary>
        /// Creates an object, to announce changes on an object in subscripe
        /// and publish mode
        /// </summary>
        /// <param name="val" type="Object">watchable object</param>
        /// <returns type="Object">Observer</returns>
        var callbacks = [],
            value = val !== undefined ? val : null;

        return {
            subscribe: function (callback) {
                /// <summary>
                /// adds observer
                /// </summary>
                /// <param name="callback" type="Function">
                /// Callback-Function: should accept one parameter
                /// </param>
                if (callbacks.indexOf(callback) === -1) {
                    callbacks.push(callback);
                }
            },
            unsubscribe: function (callback) {
                /// <summary>
                /// removes observer-function
                /// </summary>
                /// <param name="callback" type="Function">
                /// function, which should be removed
                /// </param>
                var index = callbacks.indexOf(callback);
                if (index !== -1) {
                    callbacks.splice(index, 1);
                }
            },
            notifyChange: function (val) {
                /// <summary>
                /// notifys all observers about changed object
                /// </summary>
                /// <param name="val" type="Object">
                /// new object, which observer should recieve
                /// </param>
                var i, len;
                if (val === value) {
                    return;
                }

                value = val;

                for (i = 0, len = callbacks.length; i < len; i += 1) {
                    callbacks[i](val);
                }
            }
        };
    };
        
    haushaltsRechner.getURL = function () {
        /// <summary>
        /// Get current URL
        /// </summary>
        /// <returns type="String">current URL</returns>
        return doc.URL;
    };

    haushaltsRechner.getURLParamless = function () {
        /// <summary>
        /// Gets current URL without parameters
        /// </summary>
        /// <returns type="String">URL</returns>
        var url = haushaltsRechner.getURL();
        if (url.indexOf("?") > -1) {
            return url.substr(0, url.indexOf("?") + 1);
        }

        return url;
    };

    haushaltsRechner.getFileNameOfURL = function () {
        /// <summary>
        /// Gets the current Filename in the URL
        /// </summary>
        /// <returns type="String">Filename</returns>
        var url = haushaltsRechner.getURL();
        return url.substring(url.lastIndexOf('/') + 1);
    };

    haushaltsRechner.stringToBoolean = function (str) {
        /// <summary>
        /// Converts any String to an Boolean value
        /// </summary>
        /// <param name="str">value to check</param>
        /// <returns type="Boolean">true, if "1", "true", "ja"</returns>
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
        case "yes":
            ret = true;
            break;
        default:
            ret = false;
            break;
        }
        return ret;
    };

    haushaltsRechner.isNullOrUndefined = function (val) {
        /// <summary>
        /// !!!DEPRECATED!!!, not necessarray
        /// Prüft den Wert auf null oder undefined
        /// </summary>
        /// <param name="val" type="Obejct">zu prüfender Wert</param>
        /// <returns type="Boolean"> true, wenn Wert null oder undefined</returns>
        if (val === undefined || val === null) {
            return true;
        }

        return false;
    };
   
    haushaltsRechner.getRandomString = function () {
        /// <summary>
        /// Get an random String with 8 characters
        /// Possible chars: "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz"
        /// </summary>
        /// <returns type="String">random String</returns>
        var chars, stringLength, randomstring, i, rnum;
        chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
        stringLength = 8;
        randomstring = '';
        for (i = 0; i < stringLength; i += 1) {
            rnum = Math.floor(Math.random() * chars.length);
            randomstring += chars.substring(rnum, rnum + 1);
        }
        return randomstring;
    };

    haushaltsRechner.createGUID = function () {
        /// <summary>
        /// Creates an pdeudo GUID looking String
        /// </summary>
        /// <returns type="String">created pseudo GUID</returns>
        var S4 = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        };
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    };

    haushaltsRechner.splitStringToDate = function (val) {
        /// <summary>
        /// Splits String into Date
        /// </summary>
        /// <param name="val">parseable string</param>
        /// <returns type="Date">date</returns>
        var day, month, year, date;
        if (!val) {
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
    };

    haushaltsRechner.convertMoneyToDecimal = function (val) {
        /// <summary>
        /// Converts money value to decimal string
        /// </summary>
        /// <param name="val">money string</param>
        /// <returns type="string">decimal</returns>
        return val.replace(LocalText.Currency, "").replace(LocalText.Thousand, ".");
    };

}(window.haushaltsRechner = window.haushaltsRechner || {}, jQuery));

(function (client, $, undefined) {
}(window.haushaltsRechner.client = window.haushaltsRechner.client || {}, jQuery));

(function (search, $, undefined) {
}(window.haushaltsRechner.client.search = window.haushaltsRechner.client.search || {}, jQuery));

(function (movements, $, undefined) {
}(window.haushaltsRechner.client.search.movements =
    window.haushaltsRechner.client.search.movements || {}, jQuery));

(function (reporting, $, undefined) {
}(window.haushaltsRechner.client.search.reporting =
    window.haushaltsRechner.client.search.reporting || {}, jQuery));

(function (dialogs, $, undefined) {
}(window.haushaltsRechner.client.dialogs = window.haushaltsRechner.client.dialogs || {}, jQuery));

(function (grids, $, undefined) {
}(window.haushaltsRechner.client.grids = window.haushaltsRechner.client.grids || {}, jQuery));

(function (charts, $, undefined) {
}(window.haushaltsRechner.client.charts = window.haushaltsRechner.client.charts || {}, jQuery));

(function (movements, $, undefined) {
})(window.haushaltsRechner.client.markup = window.haushaltsRechner.client.markup || {}, jQuery);

(function (movements, $, undefined) {
})(window.haushaltsRechner.client.markup.movements =
    window.haushaltsRechner.client.markup.movements || {}, jQuery);

(function (connections, $, undefined) {
}(window.haushaltsRechner.connections = window.haushaltsRechner.connections || {}, jQuery));

$(document).on({
    ajaxStart: function () {
        var doc = document;
        if (haushaltsRechner.getFileNameOfURL().indexOf("Admin") > -1) {
            doc.getElementById("AjaxLoaderGifAdmin").style.visibility = "visible";
            doc.getElementById("AjaxLoaderGifStandard").style.visibility = "hidden";
        }
        else {
            doc.getElementById("AjaxLoaderGifStandard").style.visibility = "visible";
            doc.getElementById("AjaxLoaderGifAdmin").style.visibility = "hidden";
        }

        $('#AjaxLoaderGifContainer').show();
    },
    ajaxStop: function () {
        $('#AjaxLoaderGifContainer').hide();
    }
});