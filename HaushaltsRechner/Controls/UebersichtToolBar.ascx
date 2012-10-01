<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UebersichtToolBar.ascx.cs" Inherits="HaushaltsRechner.Controls.UebersichtToolBox" %>

<asp:ImageButton 
    ID="ImageButton1" 
    runat="server" 
    AlternateText="Add" 
    Height="32px" 
    Width="32px" 
    OnClientClick="addEditMovementDialog();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Add3232" />

<asp:ImageButton 
    ID="ImageButton2" 
    runat="server" 
    AlternateText="Edit" 
    Height="32px" 
    Width="32px" 
    OnClientClick="editSelectedMovement();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Edit3232"/>
    
<asp:ImageButton 
    ID="ibDeleteUser" 
    runat="server" 
    AlternateText="Delete" 
    Height="32px" 
    Width="32px" 
    OnClientClick="deleteSelectedMovement();return false;" 
    ImageUrl="~/App_Themes/Spacer.gif" 
    CssClass="Delete3232"/>
