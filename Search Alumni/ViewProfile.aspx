<%@ Page Title="" Language="C#" MasterPageFile="~/Search Alumni/SearchPage.master" AutoEventWireup="true" CodeFile="ViewProfile.aspx.cs" Inherits="Search_Alumni_ViewProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../css/viewprofile.css"  media="all" rel="Stylesheet" type="text/css"/>
<script type="text/javascript">
    function popupme() {
        document.getElementById('cbox').style.display = "block";
        document.getElementById('cfoot').style.display = "block";
    }
    function hidepopup() {
        document.getElementById('cbox').style.display = "none";
        document.getElementById('cfoot').style.display = "none";
    }
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center><h2 style="font-size:23px;text-align:right"><img src="../images/logobig3.png" /> Alumni Search Details</h2><hr /></center>
<br />
<div id="img-wrap" style="height:100px">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div style="float:left">
        <img alt="" height="75" width="75" id="img1" runat="server" />
           &nbsp;&nbsp;&nbsp;
<asp:ImageButton ID="imgadd" runat="server"  OnClick="imgadd_Click" CausesValidation="false" Visible="false" ImageUrl="../images/addfriend2.png"  />
 &nbsp; <asp:Image ID="friimg" runat="server" ImageUrl="../images/friends.png" Visible="false" 
                           Height="24px" />
                           &nbsp; 
<asp:ImageButton ID="closefri" runat="server" ImageUrl="../images/closefri.jpg" Visible="false" 
                           Height="24px" OnClick="closefri_Click" CausesValidation="false" />
                           &nbsp;</div> 
                           <div id="joint" style="float:left">
 <a id="sndmsg" onclick="popupme()" runat="server" visible="false" href="#"><img src="../images/sendmsg.png" alt="" height="24" /></a>
 
<div id="cbox">
            <textarea id="alumsg" style="resize:none" runat="server" cols="38" rows="4"></textarea><br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a message" ForeColor="Red" SetFocusOnError="true" ControlToValidate="alumsg"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="SendMessage" CausesValidation="false" CssClass="submitbutton" 
                runat="server" Text="Send Message" onclick="SendMessage_Click" />
            <div id="cfoot"><img src="../images/arrow.gif" /></div>
</div>     
</div>
                           </ContentTemplate>
                           <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="SendMessage" EventName="Click" />
                           </Triggers>
       </asp:UpdatePanel>

</div>

<table id="table1" align="center" border="0" cellpadding="2" cellspacing="1" width="100%" >
<colgroup>
    	<col class="oce-first" />
    </colgroup>
<tr ><td width="40%" >
<b>Name</b></td><td>:</td>
<td width="60%" align="left">
<b> 
    <asp:Label ID="name" runat="server" Text=""></asp:Label>
 </b>
</td></tr>
<tr><td width="40%" >
<b>Date Of Birth</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
   <asp:Label ID="dob" runat="server" Text=""></asp:Label>
 </b>
</td></tr>
<tr ><td width="40%" >
<b>Branch</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
  <asp:Label ID="branch" runat="server" Text=""></asp:Label>
</b>
</td></tr>

<tr><td width="40%" >
<b>Batch</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
  <asp:Label ID="batch" runat="server" Text=""></asp:Label>
</b>
</td></tr>

<tr ><td width="40%" >
<b>Organisation</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
   <asp:Label ID="org" runat="server" Text=""></asp:Label>
</b>
</td></tr>

<tr><td width="40%" >
<b>Designation</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
   <asp:Label ID="desig" runat="server" Text=""></asp:Label>
</b>
</td></tr>

<tr ><td width="40%" >
<b>Personal Email-Id</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
   <asp:Label ID="peremail" runat="server" Text=""></asp:Label>
 </b>
</td></tr>

<tr><td width="40%" >
<b>Company Email-Id</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
   <asp:Label ID="cmpemail" runat="server" Text=""></asp:Label>
 </b>
</td></tr>

<tr ><td width="40%" >
<b>City</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
  <asp:Label ID="city" runat="server" Text=""></asp:Label>
 </b>
</td></tr>
<tr><td width="40%" >
<b>State</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
  <asp:Label ID="state" runat="server" Text=""></asp:Label>
 </b>
</td></tr>
<tr ><td width="40%" >
<b>Country</b></td><td>:
</td>
<td width="60%" align="left">
<b> 
  <asp:Label ID="country" runat="server" Text=""></asp:Label>
 </b>
</td></tr>
</table>
  <br />
        <a id="link" class="backlink" runat="server">< &nbsp;&nbsp;Back</a>
</asp:Content>

