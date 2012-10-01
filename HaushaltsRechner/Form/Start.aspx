<%@ Page Title="" Language="C#" MasterPageFile="~/Template/HaushaltsRechner.Master" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="HaushaltsRechner.Form.Start" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="Start_Content">
        <center>
<%
    foreach (var user in users)
    {
        %>
            <div class="Start_User" onclick="loginUser('<%= user.ID %>');"><%= user.NAME %></div>
        <%
    }
     %>
        </center>
    </div>
    <script type="text/javascript">
        function loginUser(id) {
            var o = {};
            o.id = id;
            o.pw = "";

            handleData(o, "UserService", "UserLogin", function (msg) {
                if (stringToBoolean(msg) === true) {
                    window.location = 'Uebersicht.aspx';
                }
                else {

                }
            });
        }

    </script>
</asp:Content>
