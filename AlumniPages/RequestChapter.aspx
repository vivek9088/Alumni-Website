<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="RequestChapter.aspx.cs" Inherits="AlumniPages_RequestChapter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="Stylesheet" href="../css/inputtext.css" media="all" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="grid9">
    <h2 style="font-size:25px;float:right"><img src="../images/logobig3.png" /> Alumni Chapter Request</h2>
    <img src="../images/active.png" width="100%" height="3px" />
    <br />
    <br />
<br />
<p style="color:Black;font-size:14px;">
Being an SSN Alumni, if your interested to start a new chapter in your locale,
here are few requirements :</p>
<ol style="list-style-type:decimal;padding-left:20px;color:Black;">
<li> There should be minimum of 50 members of SSN alumni residing in the locality.</li>
<li> The Office Bearers of the Chapter should be selected.</li>
 <li>Make sure there is atleast one get together in your locality every year.</li>
</ol>
<br />
<p style="color:Black;font-size:14px;">For further details and help from the PARENT team, raise the request to the Alumni Officer to help you :
</p>
<br />
<table id="chaptab" width="100%" cellpadding="5" >
<tr><td>Chapter Name</td>
<td>:</td>
<td><asp:TextBox ID="chapname" runat="server" Width="175px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a name" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapname"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only Aplhabets allowed" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapname" ValidationExpression="^[\sa-zA-Z]*"></asp:RegularExpressionValidator>
</td>
</tr>

<tr>
<td>Batch</td>
<td>:</td>
<td><label class="label"><asp:DropDownList ID="chapbatch" runat="server" Width="125px" 
        oninit="chapbatch_Init">
    </asp:DropDownList>
    </label>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select a batch" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapbatch"></asp:RequiredFieldValidator>
</td>
</tr>

<tr>
<td>Location (State, Country)</td>
<td>:</td>
<td><asp:TextBox ID="chaploc" runat="server" Width="175px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter a location" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chaploc"></asp:RequiredFieldValidator>
<br /><font color="red">Ex. Mumbai, India</font></td>
</tr>

<tr>
<td>Email-Id</td>
<td>:</td>
<td><asp:TextBox ID="chapemail" runat="server" Width="175px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter mail-id" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapemail"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Email-Id in wrong format" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapemail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
</td>
</tr>

<tr>
<td>Contact No.</td>
<td>:</td>
<td><asp:TextBox ID="chapnum" runat="server" Width="175px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter phone number" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapnum"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only numbers allowed" Display="Dynamic" SetFocusOnError="true" ControlToValidate="chapnum" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
</td>
</tr>

<tr>
<td>Request Message</td>
<td>:</td>
<td> <textarea id="chapmsg" style="resize:none;" cols="50" rows="4" runat="server"></textarea> </td>
</tr>
<tr><td></td><td></td><td></td></tr>
<tr>
<td></td>
<td></td>
<td>
 <asp:ImageButton ID="chapsubmit" runat="server" ImageUrl="../images/submit.jpg" 
        onclick="chapsubmit_Click" />
</td>
</tr>
</table><br />

<!--Alert Box -->
       <div id="AlertBox" class="alert">
<p id="errmsg1"></p>  
 <input id="reurl" runat="server" type="hidden" value="1" />   
           <input id="Button1" class="button" type="button" value="OK" onclick="javascript:HideMe()" />
</div>
<!--Alert Box End-->
</div>
</asp:Content>

