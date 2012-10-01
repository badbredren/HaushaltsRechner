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

/// <reference path="32-GUI.debug.js" />
/// <reference path="00-jquery-1.8.1.debug.js" />
/// <reference path="jquery-1.8.1.intellisense.js" />
/// <reference path="01-jquery-ui-1.8.23.debug.js" />
/// <reference path="10-jquery-maskMoney.debug.js" />
/// <reference path="10-jquery.event.drag.debug.js" />
/// <reference path="10-jquery.maskedinput-1.3.min.debug.js" />
/// <reference path="15-Localisation.debug.js" />
/// <reference path="20-Json.debug.js" />
/// <reference path="30-Base.debug.js" />
/// <reference path="31-Connections.debug.js" />
/// <reference path="SlickGrid/slick.core.js" />
/// <reference path="SlickGrid/slick.dataview.js" />
/// <reference path="SlickGrid/slick.editors.js" />
/// <reference path="SlickGrid/slick.formatters.js" />
/// <reference path="SlickGrid/slick.grid.js" />
/// <reference path="SlickGrid/slick.groupitemmetadataprovider.js" />
/// <reference path="SlickGrid/slick.remotemodel.js" />
/// <reference path="SlickGrid/Plugins/slick.cellcopymanager.js" />
/// <reference path="SlickGrid/Plugins/slick.cellrangedecorator.js" />
/// <reference path="SlickGrid/Plugins/slick.cellrangeselector.js" />
/// <reference path="SlickGrid/Plugins/slick.cellselectionmodel.js" />
/// <reference path="SlickGrid/Plugins/slick.checkboxselectcolumn.js" />
/// <reference path="SlickGrid/Plugins/slick.headerbuttons.js" />
/// <reference path="SlickGrid/Plugins/slick.headermenu.js" />
/// <reference path="SlickGrid/Plugins/slick.rowmovemanager.js" />
/// <reference path="SlickGrid/Plugins/slick.rowselectionmodel.js" />
/// <reference path="SlickGrid/Plugins/slick.autotooltips.js" />
/// <reference path="SlickGrid/Controls/slick.pager.js" />
/// <reference path="SlickGrid/Controls/slick.columnpicker.js" />
var grid, dataView;

function getSelectedItemFromShownGrid() {
    if (!grid) {
        return null;
    }

    var index = grid.getSelectedRows();
    if (index && index.length === 1) {
        return grid.getDataItem(index);
    }
}

function resultGrid(divID, pagerID, searchParameter) {
    var data = [];    

    var createGridFucntion = function () {
        var options, columns, pager,
            sortcol = "title", sortdir = 1;

        function comparer(a, b) {
            var x = a[sortcol], y = b[sortcol];
            return (x == y ? 0 : (x > y ? 1 : -1));
        }

        columns = [
            { id: "sel", name: "#", field: "num", behavior: "select", cssClass: "cell-selection", width: 40, cannotTriggerInsert: true, resizable: false, selectable: false },
            { id: "Amount", name: getLocalText().Amount, field: "Amount", formatter: Slick.Formatters.Currency },
            { id: "CategoryName", name: getLocalText().Category, field: "CategoryName" },
            { id: "ReasonText", name: getLocalText().Reason, field: "ReasonText" },
            { id: "UserName", name: getLocalText().User, field: "UserName" },
            { id: "AccountName", name: getLocalText().Account, field: "AccountName" },
            { id: "DateAdded", name: getLocalText().MovementDateAdded, field: "DateAdded" },
            { id: "DateEdit", name: getLocalText().DateEdited, field: "DateEdit" }
        ];

        options = {
            editable: false,
            //enableAddRow: false,
            enableCellNavigation: true,
            asyncEditorLoading: true,
            forceFitColumns: true,
            multiSelect: false,
            enableCellRangeSelection: true
        };

        dataView = new Slick.Data.DataView({ inlineFilters: false});

        grid = new Slick.Grid('#' + divID, dataView, columns, options);
        grid.setSelectionModel(new Slick.RowSelectionModel());

        pager = new Slick.Controls.Pager(dataView, grid, $("#" + pagerID));

        dataView.onPagingInfoChanged.subscribe(function (e, pagingInfo) {
            var isLastPage = pagingInfo.pageNum == pagingInfo.totalPages - 1;
            var enableAddRow = isLastPage || pagingInfo.pageSize == 0;
            var options = grid.getOptions();

            if (options.enableAddRow != enableAddRow) {
                grid.setOptions({ enableAddRow: enableAddRow });
            }
        });

        dataView.onRowCountChanged.subscribe(function (e, args) {
            grid.updateRowCount();
            grid.render();
        });

        dataView.onRowsChanged.subscribe(function (e, args) {
            grid.invalidateRows(args.rows);
            grid.render();
        });

        dataView.beginUpdate();
        dataView.setItems(data);
        dataView.endUpdate();
        grid.setOptions({ enableAddRow: false });
    };

    $(function () {
        if (!isNullOrUndefined(searchParameter)) {
            handleData(searchParameter, "MovementService", "GetAllMovements", function (msg) {
                var movements = jQuery.parseJSON(msg);
                var len = movements.length;
                for (var i = 0; i < len; i++) {
                    var d = (data[i] = {});
                    var mov = movements[i];

                    d["id"] = "id_" + i;
                    d["num"] = i + 1;
                    d["MovementID"] = mov.ID;
                    d["Amount"] = mov.Amount;
                    d["CategoryName"] = mov.CategoryName;
                    d["ReasonText"] = mov.ReasonText;
                    d["UserName"] = mov.UserName;
                    d["AccountName"] = mov.AccountName;
                    d["DateAdded"] = mov.DateAdded;
                    d["DateEdit"] = mov.DateEdit;
                    d["Message"] = mov.Message;
                    d["AccountID"] = mov.AccountID;
                    d["ReasonID"] = mov.ReasonID;
                    d["CategoryID"] = mov.CategoryID;
                }
                createGridFucntion();

            }, false);
        }
        else {
            createGridFucntion();
        }
        
       

    });
}