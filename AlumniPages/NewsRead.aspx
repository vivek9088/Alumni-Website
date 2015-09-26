<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="NewsRead.aspx.cs" Inherits="AlumniPages_NewsRead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="grid9">
<h2 style="font-size:25px;float:right"><img src="../images/logobig3.png" /> Alumni News</h2>
<img src="../images/active.png" width="100%" height="3px" /><br />
<br />   
    <asp:Label ID="article" runat="server" Text=""></asp:Label>
    <asp:Label ID="rerr" runat="server" Text="No News available" Visible="false"></asp:Label>

</div>
</asp:Content>

