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