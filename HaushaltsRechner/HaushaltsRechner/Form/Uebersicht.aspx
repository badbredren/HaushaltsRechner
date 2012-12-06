<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Content.Master" AutoEventWireup="true" CodeBehind="Uebersicht.aspx.cs" Inherits="HaushaltsRechner.Form.Uebersicht" %>

<%@ Register src="../Views/UebersichtResult.ascx" tagname="UebersichtResult" tagprefix="uc1" %>
<%@ Register src="../Views/UebersichtSearch.ascx" tagname="UebersichtSearch" tagprefix="uc2" %>
<%@ Register src="../Views/UebersichtToolBar.ascx" tagname="UebersichtToolBar" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SearchPanel" runat="server">

    <uc2:UebersichtSearch ID="UebersichtSearch1" runat="server" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Toolbar" runat="server">

    <uc3:UebersichtToolBar ID="UebersichtToolBar1" runat="server" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ResultGrid" runat="server">

    <uc1:UebersichtResult ID="UebersichtResult1" runat="server" />

<asp:PlaceHolder runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            tryAddSubject();
        });

        var tryAddSubject = function () {
            if (movementSearchParameterObserver === undefined
                || haushaltsRechner.client.markup.movements.createGrid === undefined) {
                window.setTimeout(function () { tryAddSubject(); }, 100);
            }
            else {
                movementSearchParameterObserver.subscribe(haushaltsRechner.client.markup.movements.createGrid);
            }                    
        };

        function refreshSearch() {
            $('#<%= UebersichtResult1.RefreshButton.ClientID %>').click();
        }        

        function addEditMovementDialog() {
            haushaltsRechner.client.dialogs.addEditMovement(
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    refreshSearch
                );
        }

        function editSelectedMovement() {
            var m;
            m = haushaltsRechner.client.grids.getSelectedItemFromShownGrid(); //selectedMovement;
            if (m) {
                haushaltsRechner.client.dialogs.addEditMovement(
                    m.Amount,
                    m.Message,
                    m.MovementID,
                    m.AccountID,
                    m.DateAdded,
                    m.CategoryID,
                    m.ReasonID,
                    refreshSearch
                );
            } else {
                selectionError();
            }
        }

        function deleteSelectedMovement() {
            var m = haushaltsRechner.client.grids.getSelectedItemFromShownGrid(); //selectedMovement;
            if (m) {
                haushaltsRechner.connections.deleteMovement(m.MovementID, function (success) {
                    if (haushaltsRechner.stringToBoolean(success)) {
                        haushaltsRechner.client.dialogs.information(
                            LocalText.Success,
                            LocalText.MovementsDeletedSuccess);
                        refreshSearch();
                    }
                    else {
                        haushaltsRechner.client.dialogs.information(
                            LocalText.Failure,
                            LocalText.MovementsDeletedFailure);
                    }
                });
            } else {
                selectionError();
            }
        }

        function selectionError() {
            haushaltsRechner.client.dialogs.error(
                LocalText.Failure,
                LocalText.MovementsNoSelection);
        }
    </script>
</asp:PlaceHolder>
</asp:Content>
