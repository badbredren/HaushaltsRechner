<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UebersichtSearch.ascx.cs" Inherits="HaushaltsRechner.Controls.UebersichtSearch" %>

<div class="Search_TextHeader Search_LeftColumn">Buchungsdatum</div>

<div class="Clear"></div>
    
<div class="Search_LeftColumn">von</div><div class="Search_RightColumn">bis</div> 
     
<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:TextBox ID="tbDateFrom" runat="server" />
</div>
<div class="Search_RightColumn">
    <asp:TextBox ID="tbDateTo" runat="server" />
</div>

<div class="Clear"></div>

<div class="Search_TextHeader Search_LeftColumn">Eingabedatum</div>

<div class="Clear"></div>
    
<div class="Search_LeftColumn">von</div><div class="Search_RightColumn">bis</div> 
     
<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:TextBox ID="tbEditFrom" runat="server" />
</div>
<div class="Search_RightColumn">
    <asp:TextBox ID="tbEditTo" runat="server" />
</div>

<div class="Clear"></div>
<div class="Search_TextHeader Search_LeftColumn">Eintrag von</div>
<div class="Search_TextHeader Search_RightColumn">Konto</div>

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
<div class="Search_TextHeader Search_LeftColumn">Grund</div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:ListBox ID="lbReason" runat="server" Rows="1" 
        DataSourceID="edsReason" DataTextField="TEXT" DataValueField="ID" 
        ondatabound="lbReason_DataBound" />
</div>
<div class="Clear"></div>

<div class="Search_TextHeader Search_LeftColumn">Betrag</div>

<div class="Clear"></div>

<div class="Search_LeftColumn">von</div>
<div class="Search_RightColumn">bis</div>

<div class="Clear"></div>

<div class="Search_LeftColumn">
    <asp:TextBox ID="tbAmountFrom" runat="server" />
</div>
<div class="Search_RightColumn">
    <asp:TextBox ID="tbAmountTo" runat="server" />
</div>

<div class="Clear"></div>
<hr />
<div class="Search_Buttons">
    <%--<asp:ImageButton 
        runat="server" 
        ID="btnSearch" 
        AlternateText="Suchen" 
        onclick="btnSearch_Click"
        Height="32px" 
        Width="32px"
        ImageUrl="~/App_Themes/Spacer.gif" 
        CssClass="Find3232"  />
        <asp:ImageButton 
        runat="server" 
        ID="btnReset" 
        AlternateText="Zurücksetzen" 
        onclick="btnReset_Click"
        Height="32px" 
        Width="32px"
        ImageUrl="~/App_Themes/Spacer.gif" 
        CssClass="Clear3232"  />--%>
    <asp:ImageButton 
        runat="server" 
        ID="btnSearch" 
        AlternateText="Suchen" 
        OnClientClick="notifySearchParameterChanged(); return false;"
        Height="32px" 
        Width="32px"
        ImageUrl="~/App_Themes/Spacer.gif" 
        CssClass="Find3232"  />
    <asp:ImageButton 
        runat="server" 
        ID="btnReset" 
        AlternateText="Zurücksetzen" 
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
        var searchParameter, movementSearchParameterObserver;
        $(document).ready(function () {
            movementSearchParameterObserver = new Observer(null);
        });
        
        function clearInputFields() {
            var doc, tbDateFrom, tbDateTo, tbEditFrom, tbEditTo,
                lbUser, lbAccount, lbReason, tbAmountFrom, tbAmountTo, p, dummyVal, dummyDate;

            doc = document;

            tbDateFrom = doc.getElementById("<%= tbDateFrom.ClientID %>");
            tbDateTo = doc.getElementById("<%= tbDateTo.ClientID %>");
            tbEditFrom = doc.getElementById("<%= tbEditFrom.ClientID %>");
            tbEditTo = doc.getElementById("<%= tbEditTo.ClientID %>");
            lbUser = doc.getElementById("<%= lbUser.ClientID %>");
            lbAccount = doc.getElementById("<%= lbAccount.ClientID %>");
            lbReason = doc.getElementById("<%= lbReason.ClientID %>");
            tbAmountFrom = doc.getElementById("<%= tbAmountFrom.ClientID %>");
            tbAmountTo = doc.getElementById("<%= tbAmountTo.ClientID %>");

            tbDateFrom.value = "";
            tbDateTo.value = "";
            tbEditFrom.value = "";
            tbEditTo.value = "";
            tbAmountFrom = "";
            tbAmountTo.value = "";
            lbUser.selectedIndex = 0;
            lbAccount.selectedIndex = 0;
            lbReason.selectedIndex = 0;

            searchParameter = null;

            movementSearchParameterObserver.notifyChange(searchParameter);
        }

        function notifySearchParameterChanged() {
            var doc, tbDateFrom, tbDateTo, tbEditFrom, tbEditTo,
                lbUser, lbAccount, lbReason, tbAmountFrom, tbAmountTo, p, dummyVal, dummyDate;

            doc = document;

            tbDateFrom = doc.getElementById("<%= tbDateFrom.ClientID %>");
            tbDateTo = doc.getElementById("<%= tbDateTo.ClientID %>");
            tbEditFrom = doc.getElementById("<%= tbEditFrom.ClientID %>");
            tbEditTo = doc.getElementById("<%= tbEditTo.ClientID %>");
            lbUser = doc.getElementById("<%= lbUser.ClientID %>");
            lbAccount = doc.getElementById("<%= lbAccount.ClientID %>");
            lbReason = doc.getElementById("<%= lbReason.ClientID %>");
            tbAmountFrom = doc.getElementById("<%= tbAmountFrom.ClientID %>");
            tbAmountTo = doc.getElementById("<%= tbAmountTo.ClientID %>");
            
            p = { };

            dummyVal = tbAmountFrom.value;
            if (dummyVal !== "") {
                p.ValueFrom = convertMoneyToDecimal(dummyVal);
            }

            dummyVal = tbAmountTo.value;
            if (dummyVal !== "") {
                p.ValueTo = convertMoneyToDecimal(dummyVal);
            }

            dummyVal = lbUser.options[lbUser.selectedIndex].value;
            if (dummyVal !== "") {
                p.UserID = dummyVal;
            }

            dummyVal = lbAccount.options[lbAccount.selectedIndex].value;
            if (dummyVal !== "") {
                p.AccountID = dummyVal;
            }

            dummyVal = lbReason.options[lbReason.selectedIndex].value;
            if (dummyVal !== "") {
                p.ReasonID = dummyVal;
            }

            dummyVal = tbDateFrom.value;
            if (dummyVal !== "") {
                dummyDate = splitStringToDate(dummyVal);
                p.DateFrom = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
            }

            dummyVal = tbDateTo.value;
            if (dummyVal !== "") {
                dummyDate = splitStringToDate(dummyVal);
                p.DateTo = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
            }

            dummyVal = tbEditFrom.value;
            if (dummyVal !== "") {
                dummyDate = splitStringToDate(dummyVal);
                p.EditFrom = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
            }

            dummyVal = tbEditTo.value;
            if (dummyVal !== "") {
                dummyDate = splitStringToDate(dummyVal);
                p.EditTo = dummyDate.month + "/" + dummyDate.day + "/" + dummyDate.year;
            }
            searchParameter = p;

            movementSearchParameterObserver.notifyChange(searchParameter);
        }

        function DocumentReady_<%= ClientID %>(){
            var tbDateFromID, tbDateToID, tbDateToID, tbEditFromID,
                tbEditToID, tbAmountFromID, tbAmountToID;
            tbDateFromID = '<%= tbDateFrom.ClientID %>';
            tbDateToID = '<%= tbDateTo.ClientID %>';
            tbEditFromID = '<%= tbEditFrom.ClientID %>';
            tbEditToID = '<%= tbEditTo.ClientID %>';
            tbAmountFromID = '<%= tbAmountFrom.ClientID %>';
            tbAmountToID = '<%= tbAmountTo.ClientID %>';

            makeDatePicker(tbDateFromID);
            makeDatePicker(tbDateToID);
            makeDatePicker(tbEditFromID);
            makeDatePicker(tbEditToID);

            makeMoneyMask(tbAmountFromID);
            makeMoneyMask(tbAmountToID);
        }
    </script>
</asp:PlaceHolder>
