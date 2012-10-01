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