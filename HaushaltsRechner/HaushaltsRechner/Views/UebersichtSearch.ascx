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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UebersichtSearch.ascx.cs" Inherits="HaushaltsRechner.Views.UebersichtSearch" %>

<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.movementDateAdded %></div>

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
<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.createdFrom %></div>
<div class="Search_TextHeader Search_RightColumn"><%= Resources.Default.account %></div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:ListBox ID="lbUser" runat="server" Rows="1" 
        DataSourceID="edsUser" DataTextField="NAME" DataValueField="ID" 
        ondatabound="lbUser_DataBound" />
</div>
<div class="Search_RightColumn">
    <asp:ListBox ID="lbAccount" runat="server" Rows="1"
        DataSourceID="edsAccounts" DataTextField="NAME" DataValueField="ID" 
        ondatabound="lbAccount_DataBound"  />
</div>

<div class="Clear"></div>
<div class="Search_TextHeader Search_LeftColumn"><%= Resources.Default.reason %></div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:ListBox ID="lbReason" runat="server" Rows="1" 
        DataSourceID="edsReason" DataTextField="TEXT" DataValueField="ID" 
        ondatabound="lbReason_DataBound" />
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
<asp:EntityDataSource ID="edsUser" runat="server" 
    ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" 
    EntitySetName="USER" Select="it.[NAME], it.[ID]">
</asp:EntityDataSource>

<asp:EntityDataSource ID="edsAccounts" runat="server" 
    ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" 
    EntitySetName="ACCOUNT">
</asp:EntityDataSource>

<asp:EntityDataSource ID="edsReason" runat="server" 
    ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" 
    EntitySetName="REASON">
</asp:EntityDataSource>

<asp:QueryExtender ID="QueryExtender2" runat="server" 
    TargetControlID="edsAccounts" >
    <asp:CustomExpression OnQuerying="FilterAccounts" />
</asp:QueryExtender>

<asp:PlaceHolder runat="server" >
    <script type="text/javascript">
        var movementSearchParameterObserver;
        $(document).ready(function () {
            movementSearchParameterObserver = new haushaltsRechner.observer(null);
        });
        
        function clearInputFields() {
            haushaltsRechner.client.clearElementByElementIds([
                "tbDateFrom",
                "tbDateTo",
                "tbEditFrom",
                "tbEditTo",
                "<%= lbUser.ClientID %>",
                "<%= lbAccount.ClientID %>",
                "<%= lbReason.ClientID %>",
                "tbAmountFrom",
                "tbAmountTo"
            ]);

            haushaltsRechner.client.grids.searchParameter = null;

            movementSearchParameterObserver.notifyChange(haushaltsRechner.client.grids.searchParameter);
        }

        function notifySearchParameterChanged() {
            var doc, tbDateFrom, tbDateTo, tbEditFrom, tbEditTo,
                lbUser, lbAccount, lbReason, tbAmountFrom, tbAmountTo, p, dummyVal, dummyDate,
                hr = haushaltsRechner;

            doc = document;

            tbDateFrom = doc.getElementById("tbDateFrom");
            tbDateTo = doc.getElementById("tbDateTo");
            tbEditFrom = doc.getElementById("tbEditFrom");
            tbEditTo = doc.getElementById("tbEditTo");
            lbUser = doc.getElementById("<%= lbUser.ClientID %>");
            lbAccount = doc.getElementById("<%= lbAccount.ClientID %>");
            lbReason = doc.getElementById("<%= lbReason.ClientID %>");
            tbAmountFrom = doc.getElementById("tbAmountFrom");
            tbAmountTo = doc.getElementById("tbAmountTo");
            
            p = { };

            p.ValueFrom = tbAmountFrom.value
                ? hr.convertMoneyToDecimal(tbAmountFrom.value)
                : undefined;
            
            p.ValueTo = tbAmountTo.value
                ? hr.convertMoneyToDecimal(tbAmountTo.value)
                : undefined;

            p.UserID = lbUser.options[lbUser.selectedIndex].value || undefined;
            
            p.AccountID = lbAccount.options[lbAccount.selectedIndex].value || undefined;
            
            p.ReasonID = lbReason.options[lbReason.selectedIndex].value || undefined;
            
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

            haushaltsRechner.client.grids.searchParameter = p;

            movementSearchParameterObserver.notifyChange(haushaltsRechner.client.grids.searchParameter);
        }

        function DocumentReady_<%= ClientID %>(){
            var client = haushaltsRechner.client;

            client.datePickerByElementID('tbDateFrom');
            client.datePickerByElementID('tbDateTo');
            client.datePickerByElementID('tbEditFrom');
            client.datePickerByElementID('tbEditTo');

            client.maskMoneyByElementID('tbAmountFrom');
            client.maskMoneyByElementID('tbAmountTo');
        }
    </script>
</asp:PlaceHolder>