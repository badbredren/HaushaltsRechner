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

function getLocalText() {
    var dateFormat = "dd.mm.yy";
    var currency = "€";
    var thousand = ".";
    var decimal = ",";
    var numberDateMaskFormat = "99.99.9999";
    var account = "Konto";
    var accountNew = "Neues Konto";
    var accountEdit = "Konto bearbeiten";
    var editUsersForAccount = function (name) {
        return "Benutzer für Konto \"" + name + "\" bearbeiten";
    };
    var amount = "Betrag";
    var category = "Kategorie";
    var movement = "Buchung";
    var movementDateAdded = "Buchungsdatum";
    var dateEdited = "letzte Bearbeitung";
    var reason = "Grund";
    var reasonOpional = "Grund (optional)";
    var user = "Benutzer";
    var userNew = "Neuer Benutzer";
    var userEdit = "Benutzer bearbeiten";
    var editRightsForUser = function (name) {
        return "Rechte für " + name + " bearbeiten";
    };
    var name = "Name";
    var sysAdmin = "Sys-Admin";
    var password = "Passwort";
    var deleteText = "Löschen";
    var deleteMovementConfirm = "Wollen Sie wirklich die ausgewählte Kontenbewegung löschen?";
    var deleteAccountConfirm = "Wollen Sie wirklich das ausgewählte Konto löschen?";

    return {
        DateFormat: dateFormat,
        Currency: currency,
        Thousand: thousand,
        Decimal: decimal,
        NumberDateMaskFormat: numberDateMaskFormat,
        Account: account,
        AccountNew: accountNew,
        AccountEdit: accountEdit,
        EditUsersForAccount: function (name) { return editUsersForAccount(name); },
        Amount: amount,
        Category: category,
        Movement: movement,
        MovementDateAdded: movementDateAdded,
        DateEdited: dateEdited,
        Reason: reason,
        ReasonOptional: reasonOpional,
        User: user,
        UserNew: userNew,
        UserEdit: userEdit,
        EditRightsForUser: function (name) { return editRightsForUser(name); },
        Name: name,
        SysAdmin: sysAdmin,
        Password: password,
        DeleteText: deleteText,
        DeleteMovementConfirm: deleteMovementConfirm,
        DeleteAccountConfirm: deleteAccountConfirm
    };
}