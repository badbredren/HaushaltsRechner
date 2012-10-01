<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UebersichtResult.ascx.cs" Inherits="HaushaltsRechner.Controls.UebersichtResult" %>

<%--<asp:GridView ID="gvMovements" runat="server" AutoGenerateColumns="False" 
    DataSourceID="edsMovements" AllowPaging="True" 
    CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
    onselectedindexchanged="gvMovements_SelectedIndexChanged">
    <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" />
            
    <Columns>
        <asp:CommandField ShowSelectButton="True" />
        <asp:BoundField DataField="AMOUNT" HeaderText="Betrag" ReadOnly="True" 
            SortExpression="AMOUNT" />
        <asp:BoundField DataField="CATEGORY_NAME" HeaderText="Kategorie" 
            ReadOnly="True" SortExpression="CATEGORY_NAME" />
        <asp:BoundField DataField="REASON_TEXT" HeaderText="Grund" ReadOnly="True" 
            SortExpression="REASON_TEXT" />
        <asp:BoundField DataField="USER_NAME" HeaderText="von" ReadOnly="True" 
            SortExpression="USER_NAME" />
        <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Konto" 
            ReadOnly="True" SortExpression="ACCOUNT_NAME" />
        <asp:BoundField DataField="DATE_ADDED" HtmlEncode="false" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Buchungsdatum" ReadOnly="True" 
            SortExpression="DATE_ADDED" />
        <asp:BoundField DataField="DATE_EDIT" HtmlEncode="false" DataFormatString="{0:dd.MM.yyyy}" HeaderText="Eingabedatum" ReadOnly="True" 
            SortExpression="DATE_EDIT" />
        <asp:TemplateField >
            <ItemTemplate>
            <div style="display:none;">
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# Bind("ID") %>' />
            </div>
            </ItemTemplate>
            
        </asp:TemplateField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
            
</asp:GridView>--%>
<div id="UebersichtGridContainer" style="width:99.9%;height:490px;"></div>
<div id="UbersichtGridPager" style="width:99.9%;height:20px;"></div>
<%--<asp:Label ID="lblCount" runat="server"></asp:Label> Datensätze gefunden--%>
<asp:TextBox ID="tbHiddenSelectedMovement" runat="server" style="visibility:hidden" />
<%--<asp:Button ID="btnHiddenRefresh" runat="server" style="visibility:hidden" 
    onclick="btnHiddenRefresh_Click" />--%>
<asp:Button ID="btnHiddenRefresh" runat="server" style="visibility:hidden" 
    OnClientClick="refreshMovementSearch(); return false;" />
<asp:EntityDataSource ID="edsMovements" runat="server" 
    ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" 
    EntitySetName="V_OVERVIEW" AutoGenerateOrderByClause="true">
</asp:EntityDataSource>

<asp:QueryExtender ID="QueryExtender2" runat="server" 
    TargetControlID="edsMovements" >
    <asp:CustomExpression OnQuerying="FilterMovements" />
</asp:QueryExtender>

<asp:PlaceHolder runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            resultGrid("UebersichtGridContainer", "UbersichtGridPager");
        });
        function refreshMovementSearch() {
            createUebersichtResultGrid(searchParameter);
        }
        function createUebersichtResultGrid(searchParameter) {
            resultGrid("UebersichtGridContainer", "UbersichtGridPager", searchParameter);
        }

    </script>
</asp:PlaceHolder>