<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="EventGallery.aspx.cs" Inherits="AlumniPages_EventGallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="grid9">
<h2 style="font-size:23px;float:right;"><img src="../images/logobig3.png" />  Events</h2>
<img src="../images/active.png" height="3px;" width="100%" />
<br /><br />
    <asp:DataList ID="eventslist" runat="server" RepeatColumns="3" ItemStyle-HorizontalAlign="Center">
    <ItemTemplate>
    <%#Container.DataItem %>
    </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="err" runat="server" Text="No Events Found" Visible="false"></asp:Label>
</div>
</asp:Content>

