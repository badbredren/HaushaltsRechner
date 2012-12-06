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
"use strict";

function parseParameter(text, values) {
	/// <summary>
	/// replaces numbers like "{0}" in the given text with the values
	/// </summary>
	/// <param name="text">text</param>
	/// <param name="values">values to include</param>
	/// <returns type=""></returns>
    var i, len, para, retText = text;
    if (!text || !values || !values.length || values.length <= 0) {
        return text;
    }

    for (i = 0, len = values.length; i < len; i += 1) { 
        para = "{" + i + "}";
        if (retText.indexOf(para) > -1) {
            retText = retText.replace(para, values[i]);
        }
    }

    return retText;
}

getLocalText.prototype.getText = function () {
    /// <summary>
    /// Creates Object with all local resources
    /// </summary>
/// <returns type="object">all local resources</returns>
    var locText = new getLocalText();
    return {
        Account: this.account,
        AccountEdit: this.accountEdit,
        AccountMovements: this.accountMovements,
        AccountNew: this.accountNew,
        Accounts: this.accounts,
        Add: this.add,
        Admin: this.admin,
        Amount: this.amount,
        Cancel: this.cancel,
        Category: this.category,
        CategoryChart: this.CategoryChart,
        CategoryEdit: this.CategoryEdit,
        CategoryNew: this.CategoryNew,
        CategoryPart: this.categoryPart,
        CategorySummary: this.CategorySummary,
        Confirm: this.confirm,
        CreatedFrom: this.createdFrom,
        Currency: this.currency,
        DateEdited: this.dateEdited,
        DateFormat: this.dateFormat,
        Decimal: this.decimalSeparator,
        Delete: this.delete,
        DeleteAccountConfirm: this.deleteAccountConfirm,
        DeleteMovementConfirm: this.deleteMovementConfirm,
        DeleteText: this.deleteText,
        EditRights: this.editRights,
        EditRightsForUser: function (name) {
            return parseParameter(locText.editRightsForUser, [name]);
        },
        EditUsersForAccount: function (name) {
            return parseParameter(locText.editUsersForAccount, [name]);
        },
        Error: this.error,
        ErrorNameEmpty: this.errorNameEmpty,
        ErrorPasswordsMatch : this.errorPasswordsMatch,
        Failure: this.failure,
        From: this.from,
        Home: this.home,
        Incomming: this.Incomming,
        Language: this.language,
        MoneyDirectionType : this.moneyDirectionType,
        Movement: this.movement,
        MovementDateAdded: this.movementDateAdded,
        MovementDateCreated: this.movementDateCreated,
        MovementsDeletedFailure: this.movementsDeletedFailure,
        MovementsDeletedSuccess: this.movementsDeletedSuccess,
        MovementsNoSelection: this.movementsNoSelection,
        Name: this.name,
        NumberDateMaskFormat: this.numberDateMaskFormat,
        OK: this.ok,
        Outgoing: this.Outgoing,
        Overview: this.Overview,
        Password: this.password,
        PasswordNew: this.passwordNew,
        PasswordOld: this.passwordOld,
        PeriodOfTime: this.periodOfTime,
        Reason: this.reason,
        ReasonOptional: this.reasonOptional,
        Reasons: this.reasons,
        Reporting: this.reporting,
        Reset: this.reset,
        Save: this.save,
        Search: this.search,
        ServerError: this.serverError,
        Success: this.success,
        SysAdmin: this.sysAdmin,
        Thousand: this.thousandsSeparator,
        User: this.user,
        UserDelete: this.userDelete,
        UserDeleteConfirm: this.userDeleteConfirm,
        UserDeletedFailure: this.userDeletedFailure,
        UserDeletedSuccess: this.userDeletedSuccess,
        UserEdit: this.userEdit,
        UserNew: this.userNew,
        UsersNoSelection: this.usersNoSelection
    };
};

var LocalText = (new getLocalText()).getText();