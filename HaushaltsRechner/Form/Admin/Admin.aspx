<%@ Page Title="" Language="C#" MasterPageFile="~/Template/HaushaltsRechner.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="HaushaltsRechner.Form.Admin.Admin" %>
<%@ Register src="../../Controls/Admin/AdminNavigation.ascx" tagname="AdminNavigation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float:left;width:30%;height:100%;">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <uc1:AdminNavigation ID="AdminNavigation1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float:left;width:69%;height:33px;">
                <asp:PlaceHolder runat="server" ID="phAdminToolbar">
                </asp:PlaceHolder>
            </div>
            <div style="float:left;width:69%">
                <asp:PlaceHolder runat="server" ID="phAdminContent">
                </asp:PlaceHolder>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
