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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KategorieSearch.ascx.cs" Inherits="HaushaltsRechner.Views.KategorieSearch" %>

<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.periodOfTime %></div>

<div class="Clear"></div>
    
<div class="Search_LeftColumn"><%= Resources.Default.from %></div>
<div class="Search_RightColumn"><%= Resources.Default.to %></div> 
     
<div class="Clear"></div>

<div class="Search_LeftColumn">
    <input type="text" id="tbDateFrom" />
</div>
<div class="Search_RightColumn">
    <input type="text" id="tbDateTo" />
</div>

<div class="Clear"></div>

<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.type %></div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <select id="lbType" size="1"></select>
</div>

<div class="Clear"></div>

<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.moneyDirectionType %></div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <select id="lbDirectionType" size="1"></select>
</div>

<div class="Clear"></div>

<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.movementDateCreated %></div>

<div class="Clear"></div>
    
<div class="Search_LeftColumn"><%= Resources.Default.from %></div>
<div class="Search_RightColumn"><%= Resources.Default.to %></div> 
     
<div class="Clear"></div>

<div class="Search_LeftColumn">
    <input type="text" id="tbEditFrom" />
</div>
<div class="Search_RightColumn">
    <input type="text" id="tbEditTo" />
</div>

<div class="Clear"></div>
<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.accounts %></div>
<div class="Search_TextHeader Search_RightColumn"><%= Resources.Default.reasons %></div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:CheckBoxList ID="chkBxLstAccounts" runat="server" 
        DataSourceID="edsAccounts" DataTextField="NAME" DataValueField="ID" CssClass="Search_CheckBoxLists" RepeatLayout="Flow" />
</div>
<div class="Search_RightColumn">
    <asp:CheckBoxList ID="chkBxLstReasons" runat="server" 
        DataSourceID="edsReasons" DataTextField="TEXT" DataValueField="ID" CssClass="Search_CheckBoxLists" RepeatLayout="Flow" />    
</div>

<div class="Clear"></div>

<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.amount %></div>

<div class="Clear"></div>
    
<div class="Search_LeftColumn"><%= Resources.Default.from %></div>
<div class="Search_RightColumn"><%= Resources.Default.to %></div> 

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <input type="text" id="tbAmountFrom" />
</div>
<div class="Search_RightColumn">
    <input type="text" id="tbAmountTo" />
</div>

<div class="Clear"></div>
<hr />
<div class="Search_Buttons"> 
    <asp:ImageButton 
        runat="server" 
        ID="btnSearch" 
        OnClientClick="notifySearchParameterChanged(); return false;"
        Height="32px" 
        Width="32px"
        ImageUrl="~/App_Themes/Spacer.gif" 
        CssClass="Find3232"  />
    <asp:ImageButton 
        runat="server" 
        ID="btnReset" 
        OnClientClick="clearInputFields(); return false;"
        Height="32px" 
        Width="32px"
        ImageUrl="~/App_Themes/Spacer.gif" 
        CssClass="Clear3232"  />    
</div>

<asp:EntityDataSource ID="edsReasons" runat="server" ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" EntitySetName="REASON">
    </asp:EntityDataSource>
<asp:EntityDataSource ID="edsAccounts" runat="server" ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" EntitySetName="ACCOUNT">
    </asp:EntityDataSource>

<asp:PlaceHolder ID="PlaceHolder1" runat="server" >
    <script type="text/javascript">
        var reportingSearchParameterObserver;
        $(document).ready(function () {
            reportingSearchParameterObserver = new haushaltsRechner.observer(null);
        });

        function clearInputFields() {
            haushaltsRechner.client.clearElementByElementIds([
                "tbDateFrom",
                "tbDateTo",
                "tbEditFrom",
                "tbEditTo",
                "<%= chkBxLstAccounts.ClientID %>",
                "<%= chkBxLstReasons.ClientID %>",
                "tbAmountFrom",
                "tbAmountTo",
                "lbType",
                "lbDirectionType"
            ]);

            haushaltsRechner.client.charts.searchParameter = null;

            reportingSearchParameterObserver.notifyChange(haushaltsRechner.client.charts.searchParameter);
        }

        function notifySearchParameterChanged() {

            haushaltsRechner.client.charts.searchParameter =
                haushaltsRechner.client.search.reporting.buildSearchParameter(
                "tbDateFrom",
                "tbDateTo",
                "tbEditFrom",
                "tbEditTo",
                "<%= chkBxLstAccounts.ClientID %>",
                "<%= chkBxLstReasons.ClientID %>",
                "tbAmountFrom",
                "tbAmountTo",
                "lbType",
                "lbDirectionType");

            reportingSearchParameterObserver.notifyChange(haushaltsRechner.client.charts.searchParameter);
        }

        function DocumentReady_<%= ClientID %>() {
            var client = haushaltsRechner.client, reportingTypesArrived, reportingDirectionTypesArrived, typesArrived;

            typesArrived = function (data, selectID) {
                var select, option, fragment, i, len, val, doc = document;
                select = doc.getElementById(selectID);
                fragment = doc.createDocumentFragment();

                for (i = 0, len = data.length; i < len; i += 1) {
                    val = data[i];
                    option = client.option();
                    option.innerHTML = val.Name;
                    option.value = val.Number;
                    fragment.appendChild(option);
                }

                select.appendChild(fragment);
            }

            reportingTypesArrived = function (data) {
                typesArrived(data, "lbType");
            }

            reportingDirectionTypesArrived = function (data) {
                typesArrived(data, "lbDirectionType");
            }

            haushaltsRechner.connections.getReportingTypes(reportingTypesArrived);
            haushaltsRechner.connections.getReportingDirectionTypes(reportingDirectionTypesArrived);
           
            client.datePickerByElementID('tbDateFrom');
            client.datePickerByElementID('tbDateTo');
            client.datePickerByElementID('tbEditFrom');
            client.datePickerByElementID('tbEditTo');

            client.maskMoneyByElementID('tbAmountFrom');
            client.maskMoneyByElementID('tbAmountTo');

        }
    </script>
</asp:PlaceHolder>