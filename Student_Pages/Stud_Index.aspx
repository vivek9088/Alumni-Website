<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Pages/Stud_Admin.master" AutoEventWireup="true" CodeFile="Stud_Index.aspx.cs" Inherits="Student_Pages_Stud_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 
  
  <div id="divbox1" name="divbox" style="float:left;width:100%;display:block;">
<h2 style="font-size:23px">Welcome Student Administrator,</h2>
<div id="block" style="width:100%">
<div id="div1" style="float:left;width:320px">
    <center><asp:Image ID="adminphoto" runat="server" ImageUrl="../images/admin2.png" 
            Height="118px" Width="137px" /></center>
  <br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Your last visit is on <asp:Label ID="logdate" runat="server" Text=""></asp:Label>
   at <asp:Label ID="logtime" runat="server" Text=""></asp:Label>
</div>
<div id="div2" style="float:left;width:320px">
<center><asp:Image ID="inboxphoto" runat="server" ImageUrl="../images/inbox.png" 
         Height="118px" Width="137px" />
  <br /> <br />
  <table>
  <tr><td>Student Registered</td><td>:</td><td>
      <asp:Label ID="studcnt" runat="server" Text=""></asp:Label></td></tr>
   </table></center>
</div>
</div>
</div>
</asp:Content>

