<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUser.ascx.cs" Inherits="HaushaltsRechner.Controls.Admin.AdminUser" %>
<asp:GridView ID="gvUser" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="ID" 
    DataSourceID="edsUser" Width="100%"
    CellPadding="4" ForeColor="#333333" GridLines="None" 
    onselectedindexchanged="GridView1_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
            SortExpression="ID" />
        <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" />
        <asp:BoundField DataField="PASSWORT" HeaderText="PASSWORT" 
            SortExpression="PASSWORT" />
        <asp:CheckBoxField DataField="ISADMIN" HeaderText="ISADMIN" 
            SortExpression="ISADMIN" />
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
</asp:GridView>
<asp:EntityDataSource ID="edsUser" runat="server" 
    ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" 
    EntitySetName="USER">
</asp:EntityDataSource>
<asp:PlaceHolder runat="server">
    <script type="text/javascript">
        function editSelectedUser() {
            var mStr, u;
            mStr = selectedUser;
            if (!isNullOrUndefined(mStr) && mStr !== "") {
                u = JSON.parse(mStr);
                addEditUserDialog(
                   u.ID,
                   u.Name,
                   u.SysAdmin,
                   u.Password
                );
            } else {
                SelectionError();
            }
        }

        function editSelectedUserRights() {
            var mStr, u;
            mStr = selectedUser;
            if (!isNullOrUndefined(mStr) && mStr !== "") {
                u = JSON.parse(mStr);
                editRightsForUser(
                   u.ID,
                   u.Name
                );
            } else {
                selectionError();
            }
        }

        function deleteSelectedUser() {
            var mStr, u;
            mStr = selectedUser;
            if (!isNullOrUndefined(mStr) && mStr !== "") {
                u = JSON.parse(mStr);
                deleteUser(u.ID, function (success) {
                    if (stringToBoolean(success)) {
                        informationDialog("Erfolg", "Der Nutzer wurde erfolgreich gelöscht");
                    }
                    else {
                        informationDialog("Fehlschlag", "Der Nutzer wurde nicht gelöscht");
                    }
                });
            } else {
                selectionError();
            }
        }

        function selectionError() {
            errorDialog("Fehlschlag", "Es konnte kein ausgewählter Nutzer gefunden werden");
        }
    </script>
</asp:PlaceHolder>