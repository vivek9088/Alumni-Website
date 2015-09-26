<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Pages/Admin.master" AutoEventWireup="true" CodeFile="Admin_index.aspx.cs" Inherits="Alumni_Officer_Admin_index" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
       
      <style type="text/css">    
           
        </style>
  
<script type="text/javascript">
    </script>   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <!-- main div starts-->
<div id="main-div" style="float:left;width:100%;border:1px solid black;padding-left:5px;">

<div id="divbox1" name="divbox" style="float:left;width:100%;display:block;">
<h3>Welcome Administrator,</h3>
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
  <tr><td>Unread Messages</td><td>:</td><td>0</td></tr>
  <tr><td>Total Messages</td><td>:</td><td>0</td></tr>
  </table></center>
</div>
<div id="div3" style="float:left;width:320px">
 <center><asp:Image ID="updaetphoto" runat="server" Height="118px" Width="137px" 
         ImageUrl="../images/update.png" />
  <br /> <br />No Updates</center>
</div>
</div>
</div>

</div><!--main div ends-->
    
</asp:Content>

