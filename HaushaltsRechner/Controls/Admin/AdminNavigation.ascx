<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminNavigation.ascx.cs" Inherits="HaushaltsRechner.Controls.Admin.AdminNavigation" %>
<%@ Register src="AdminNavigationLink.ascx" tagname="AdminNavigationLink" tagprefix="uc1" %>
<uc1:AdminNavigationLink ID="anlUser" runat="server" Text="Benutzer" Target="../../Form/Admin/Admin.aspx?target=user" />
<uc1:AdminNavigationLink ID="anlAccount" runat="server" Text="Konten" Target="../../Form/Admin/Admin.aspx?target=account" />

