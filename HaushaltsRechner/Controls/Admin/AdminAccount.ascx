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

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminAccount.ascx.cs" Inherits="HaushaltsRechner.Controls.Admin.AdminAccount" %>

<asp:GridView ID="gvAccount" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="ID" 
    DataSourceID="edsAccount" Width="100%"
    CellPadding="4" ForeColor="#333333" GridLines="None" 
    onselectedindexchanged="GridView1_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
            SortExpression="ID" />
        <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" />
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

<asp:EntityDataSource ID="edsAccount" runat="server" 
    ConnectionString="name=HaushaltsrechnerEntities" 
    DefaultContainerName="HaushaltsrechnerEntities" EnableFlattening="False" 
    EntitySetName="ACCOUNT">
</asp:EntityDataSource>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <script type="text/javascript">
        function editSelectedAccount() {
            var mStr, a;
            mStr = selectedAccount;
            if (!isNullOrUndefined(mStr) && mStr !== "") {
                a = JSON.parse(mStr);
                addEditAccountDialog(
                    a.ID,
                    a.Name
                );
            } else {
                selectionError();
            }
        }

        function deleteSelectedAccount() {
            var mStr, u;
            mStr = selectedAccount;
            if (!isNullOrUndefined(mStr) && mStr !== "") {
                u = JSON.parse(mStr);
                deleteAccount(u.ID, function (success) {
                    if (stringToBoolean(success)) {
                        informationDialog("Erfolg", "Das Konto wurde erfolgreich gelöscht");
                    }
                    else {
                        informationDialog("Fehlschlag", "Das Konto wurde nicht gelöscht");
                    }
                });
            } else {
                selectionError();
            }
        }

        function editSelectedAccountUsers() {
            var mStr, u;
            mStr = selectedAccount;
            if (!isNullOrUndefined(mStr) && mStr !== "") {
                u = JSON.parse(mStr);
                editAccountUsersDialog(u.ID, u.Name);
            } else {
                selectionError();
            }
        }

        function selectionError() {
            errorDialog("Fehlschlag", "Es konnte kein ausgewähltes Konto gefunden werden");
        }
    </script>
</asp:PlaceHolder>