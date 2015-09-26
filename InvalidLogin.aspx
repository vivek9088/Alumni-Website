<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/HeaderFooter.master" CodeFile="InvalidLogin.aspx.cs" Inherits="InvalidLogin" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content> 
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
    <div class="top">
    <div class="container">
    <div class="clearfix">
    <h2 style="font-size:25px;;float:right"> <img src="images/logobig3.png" />  Invalid Login Attempt</h2>
     <img src="images/active.png" height="3px" width="100%" />      
    <br />
    <br />
   Dear User,<br /><br />Your not able to login to the alumni Site because your details are not still <font color="red" style="font-weight:bold;">authenticated by admin</font>.<br /> <br />You will receive a mail from admin very soon.<br /><br />
   Please click the back button to return to the previous page.<br /><br />
   Thank you!!!<br />
   <br />
   <a id="link" class="backlink" onserverclick="Button1_Click" runat="server">< &nbsp;&nbsp;Back</a>
   </div>
   <br />
   </div>
   </div>
   </section>
   </asp:Content>