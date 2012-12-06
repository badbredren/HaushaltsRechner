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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUser.ascx.cs" Inherits="HaushaltsRechner.Views.Admin.AdminUser" %>
<asp:GridView ID="gvUser" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="ID" 
    DataSourceID="edsUser" Width="100%"
    CellPadding="4" ForeColor="#333333" GridLines="None" 
    onselectedindexchanged="GridView1_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
            SortExpression="ID" />
        <asp:BoundField DataField="NAME"  SortExpression="NAME" />
        <asp:BoundField DataField="PASSWORT" 
            SortExpression="PASSWORT" />
        <asp:CheckBoxField DataField="ISADMIN" 
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
            if (mStr) {
                u = $.parseJSON(mStr);
                haushaltsRechner.client.dialogs.addEditUser(
                   u.ID,
                   u.Name,
                   u.SysAdmin,
                   u.Password
                );
            } else {
                selectionError();
            }
        }

        function editSelectedUserRights() {
            var mStr, u;
            mStr = selectedUser;
            if (mStr) {
                u = $.parseJSON(mStr);
                haushaltsRechner.client.dialogs.editRightsForUser(
                   u.ID,
                   u.Name
                );
            } else {
                selectionError();
            }
        }

        function deleteSelectedUser() {
            var mStr, u,
                hr = haushaltsRechner;
            mStr = selectedUser;
            if (mStr) {
                u = $.parseJSON(mStr);
                hr.connections.deleteUser(u.ID, function (success) {
                    if (hr.stringToBoolean(success)) {
                        hr.client.dialogs.information(
                            LocalText.Success,
                            LocalText.UserDeletedSuccess);
                    }
                    else {
                        hr.client.dialogs.information(
                            LocalText.Failure,
                            LocalText.UserDeletedFailure);
                    }
                });
            } else {
                selectionError();
            }
        }

        function selectionError() {
            haushaltsRechner.client.dialogs.error(
                LocalText.Failure,
                LocalText.UsersNoSelection);
        }
    </script>
</asp:PlaceHolder>