<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="AlumniPages_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
        .dash {
border-top: 1px dashed #EEE9E9;
margin-top: 0px;
}
.grid9 a
{
    text-decoration:none;
    }

.grid9 a:hover
{
    text-decoration:underline;
    }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="grid9">
<h2 style="font-size:25px;float:right"><img src="../images/logobig3.png" /> Alumni News</h2>
<img src="../images/active.png" width="100%" height="3px" />
<br /><br />
    

    <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
    <%#Container.DataItem %>
    </ItemTemplate>
    </asp:Repeater>
    <asp:Label ID="nerr" runat="server" Text="No News available" Visible="false"></asp:Label>
</div>

</asp:Content>

