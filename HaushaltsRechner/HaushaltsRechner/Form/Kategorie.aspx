<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Content.Master" AutoEventWireup="true" CodeBehind="Kategorie.aspx.cs" Inherits="HaushaltsRechner.Form.Kategorie" %>

<%-- 
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
--%>
<%@ Register Src="~/Views/KategorieSearch.ascx" TagPrefix="uc1" TagName="KategorieSearch" %>
<%@ Register Src="~/Views/KategorieResult.ascx" TagPrefix="uc1" TagName="KategorieResult" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SearchPanel" runat="server">
    <uc1:KategorieSearch runat="server" ID="KategorieSearch" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Toolbar" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ResultGrid" runat="server">

    <uc1:KategorieResult runat="server" ID="KategorieResult" />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            tryAddSubject();
        });

        var tryAddSubject = function () {
            if (reportingSearchParameterObserver === undefined
                || window.haushaltsRechner.client.markup.reporting.createReport === undefined) {
                window.setTimeout(function () { tryAddSubject(); }, 100);
            }
            else {
                reportingSearchParameterObserver.subscribe(haushaltsRechner.client.markup.reporting.createReport);
            }                    
        };

        function refreshSearch() {
            $('#<%= KategorieResult.RefreshButton.ClientID %>').click();
        }   
        </script>
        </asp:PlaceHolder>
</asp:Content>
