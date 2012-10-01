<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminAccountToolBar.ascx.cs" Inherits="HaushaltsRechner.Controls.Admin.AdminAccountToolBar" %>

<asp:ImageButton 
    ID="ibAddAccount" 
    runat="server" 
    AlternateText="Add" 
    Height="32px" 
    Width="32px" 
    OnClientClick="addEditAccountDialog();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Add3232" />

<asp:ImageButton 
    ID="ibEditUser" 
    runat="server" 
    AlternateText="Edit" 
    Height="32px" 
    Width="32px" 
    OnClientClick="editSelectedAccount();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Edit3232"/>

<asp:ImageButton 
    ID="ibDeleteUser" 
    runat="server" 
    AlternateText="Delete" 
    Height="32px" 
    Width="32px" 
    OnClientClick="deleteSelectedAccount();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Delete3232"/>

<asp:ImageButton 
    ID="ibEditAccountUsers" 
    runat="server" 
    AlternateText="Delete" 
    Height="32px" 
    Width="32px" 
    OnClientClick="editSelectedAccountUsers();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="UserGroupEdit3232"/>