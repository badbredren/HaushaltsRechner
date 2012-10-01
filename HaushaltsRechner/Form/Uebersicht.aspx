<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Content.Master" AutoEventWireup="true" CodeBehind="Uebersicht.aspx.cs" Inherits="HaushaltsRechner.Form.Uebersicht" %>

<%@ Register src="../Controls/UebersichtResult.ascx" tagname="UebersichtResult" tagprefix="uc1" %>
<%@ Register src="../Controls/UebersichtSearch.ascx" tagname="UebersichtSearch" tagprefix="uc2" %>
<%@ Register src="../Controls/UebersichtToolBar.ascx" tagname="UebersichtToolBar" tagprefix="uc3" %>

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
                || createUebersichtResultGrid === undefined) {
                window.setTimeout(function () { tryAddSubject(); }, 100);
            }
            else {
                movementSearchParameterObserver.subscribe(createUebersichtResultGrid);
            }                    
        };

        function refreshSearch() {
            $('#<%= UebersichtResult1.RefreshButton.ClientID %>').click();
        }        

        function addEditMovementDialog() {
            createAddEditMovementDialog(
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
            m = getSelectedItemFromShownGrid(); //selectedMovement;
            if (!isNullOrUndefined(m) && m !== "") {
                createAddEditMovementDialog(
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
            var m = getSelectedItemFromShownGrid(); //selectedMovement;
            if (!isNullOrUndefined(m) && m !== "") {
                deleteMovement(m.MovementID, function (success) {
                    if (stringToBoolean(success)) {
                        informationDialog("Erfolg", "Die Kontenbewegung wurde erfolgreich gelöscht");
                        refreshSearch();
                    }
                    else {
                        informationDialog("Fehlschlag", "Die Kontenbewegung wurde nicht gelöscht");
                    }
                });
            } else {
                selectionError();
            }
        }

        function selectionError() {
            errorDialog("Fehlschlag", "Es konnte keine ausgewählte Kontenbewegung gefunden werden");
        }
    </script>
</asp:PlaceHolder>
</asp:Content>
