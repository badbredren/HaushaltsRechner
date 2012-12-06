/// <reference path="jquery-1.8.3.intellisense.js" />
/// <reference path="jquery-1.8.3.intellisense.js" />
/// <reference path="jquery-1.8.3.intellisense.js" />
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
/// <reference path="00-jquery-1.8.3.intellisense.js" />
/// <reference path="01-jquery-ui-1.9.2.debug.js" />
/// <reference path="10-jquery-maskMoney.debug.js" />
/// <reference path="10-jquery.event.drag.debug.js" />
/// <reference path="10-jquery.event.drag.live.debug.js" />
/// <reference path="10-jquery.maskedinput-1.3.min.debug.js" />
/// <reference path="15-Localisation.debug.js" />
/// <reference path="20-Json.debug.js" />
/// <reference path="30-Base.debug.js" />
/// <reference path="31-Connections.debug.js" />
/// <reference path="32-GUI.debug.js" />
/// <reference path="33-Dialogs.debug.js" />
/// <reference path="34-Grids.debug.js" />
/// <reference path="34-Charts.debug.js" />
/// <reference path="35-rgbcolor.debug.js" />
"use strict";

(function (reporting, $, undefined) {
    var doc = document,
        hr = haushaltsRechner;

    reporting.buildSearchParameter = function (
        tbDateFromID,
        tbDateToID,
        tbEditFromID,
        tbEditToID,
        chkBxLstAccountID,
        chkBxLstReasonID,
        tbAmountFromID,
        tbAmountToID,
        lbTypeID,
        lbDirectionTypeID) {
        var tbDateFrom, tbDateTo, tbEditFrom, tbEditTo,
            chkBxLstAccount, chkBxLstReason, tbAmountFrom, tbAmountTo,
            p, dummyVal, dummyDate, lbType, lbDirectionType, accountChkbxArray,
            reasonoChkBxArray, len, i, chkBx;


        doc = document;

        tbDateFrom = doc.getElementById(tbDateFromID);
        tbDateTo = doc.getElementById(tbDateToID);
        tbEditFrom = doc.getElementById(tbEditFromID);
        tbEditTo = doc.getElementById(tbEditToID);
        lbType = doc.getElementById(lbTypeID);
        lbDirectionType = doc.getElementById(lbDirectionTypeID);
        tbAmountFrom = doc.getElementById(tbAmountFromID);
        tbAmountTo = doc.getElementById(tbAmountToID);
        accountChkbxArray = $('#' + chkBxLstAccountID + ' input:checkbox');
        reasonoChkBxArray = $('#' + chkBxLstReasonID + ' input:checkbox');

        p = {};

        //Accounts
        p.Accounts = [];
        for (i = 0, len = accountChkbxArray.length; i < len; i += 1) {
            chkBx = accountChkbxArray[i];
            if (chkBx.checked) {
                p.Accounts.push(chkBx.value);
            }
        }

        //Reasons
        p.Reasons = [];
        for (i = 0, len = reasonoChkBxArray.length; i < len; i += 1) {
            chkBx = reasonoChkBxArray[i];
            if (chkBx.checked) {
                p.Reasons.push(chkBx.value);
            }
        }

        //Types
        p.Type = lbType.value;

        //DirectionTypes
        p.DirectionType = lbDirectionType.value;

        p.AmountFrom = tbAmountFrom.value ?
            hr.convertMoneyToDecimal(tbAmountFrom.value) :
            undefined;

        p.AmountTo = tbAmountTo.value ?
            hr.convertMoneyToDecimal(tbAmountTo.value) :
            undefined;

        dummyVal = tbDateFrom.value;
        if (dummyVal) {
            dummyDate = hr.splitStringToDate(dummyVal);
            p.DateFrom = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
        }

        dummyVal = tbDateTo.value;
        if (dummyVal) {
            dummyDate = hr.splitStringToDate(dummyVal);
            p.DateTo = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
        }

        dummyVal = tbEditFrom.value;
        if (dummyVal) {
            dummyDate = hr.splitStringToDate(dummyVal);
            p.EditFrom = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
        }

        dummyVal = tbEditTo.value;
        if (dummyVal) {
            dummyDate = hr.splitStringToDate(dummyVal);
            p.EditTo = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
        }

        return p;
    };
}(window.haushaltsRechner.client.search.reporting =
    window.haushaltsRechner.client.search.reporting || {}, jQuery));