<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUserToolBar.ascx.cs" Inherits="HaushaltsRechner.Controls.Admin.AdminUserToolBar" %>

<asp:ImageButton 
    ID="ibAddUser" 
    runat="server" 
    AlternateText="Add" 
    Height="32px" 
    Width="32px" 
    OnClientClick="addEditUserDialog();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Add3232" />

<asp:ImageButton 
    ID="ibEditUser" 
    runat="server" 
    AlternateText="Edit" 
    Height="32px" 
    Width="32px" 
    OnClientClick="editSelectedUser();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Edit3232"/>

<asp:ImageButton 
    ID="ibDeleteUser" 
    runat="server" 
    AlternateText="Delete" 
    Height="32px" 
    Width="32px" 
    OnClientClick="deleteSelectedUser();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Delete3232"/>

<asp:ImageButton 
    ID="obEditUserRights" 
    runat="server" 
    AlternateText="Edit Rights" 
    Height="32px" 
    Width="32px" 
    OnClientClick="editSelectedUserRights();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Rights3232"/>