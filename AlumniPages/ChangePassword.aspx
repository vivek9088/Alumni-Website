<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/AlumniHeadFoot.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="AlumniPages_ChangePassword" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    
      td:last-child
{
    padding-top:3px;
    }
    td:nth-child(2){font-weight:bold;}


</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="top">
<div class="container">
<div class="clearfix">
<h2 style="font-size:25px;text-align:right;"><img src="../images/logobig3.png" />Change Password</h2>
             <img src="../images/active.png" width="100%" height="3px" /><br /><br />

<center>
       <div id="did_you_know">

<h2 style="font-size:23px;"><img src="../images/password1.png" height="40px" style="margin-top:-10px;"/>  Password Credentials</h2><br />
<table cellpadding="5">
<tr>
<td>New Password</td><td>:</td><td align="justify"><asp:TextBox ID="newpass" TextMode="Password" runat="server" Font-Size="16px"></asp:TextBox><font style="font-size:22px" color="red">*</font></td><td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a password" ControlToValidate="newpass" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:PasswordStrength ID="newpass_PasswordStrength" runat="server" 
        Enabled="True" TargetControlID="newpass">
    </asp:PasswordStrength>
    </td>
</tr>
<tr>
<td>Confirm New Password</td><td>:</td><td align="justify"><asp:TextBox ID="confpass" TextMode="Password" runat="server" Font-Size="16px"></asp:TextBox><font style="font-size:22px" color="red">*</font></td><td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter confirm password" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" ControlToValidate="confpass"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch" ControlToCompare="newpass" ControlToValidate="confpass" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:CompareValidator>
</td>
</tr>
</table>
<br />

       <div id="contactsform" style="width:100px;margin-left:25px;">
<asp:LinkButton ID="changePassword" CssClass="button" runat="server" Font-Underline="false" 
                onclick="changePassword_Click" >Submit</asp:LinkButton>
                </div>
                </div>
                </center>
                 <!--Alert Box -->
       <div id="AlertBox" class="alert">
<p id="errmsg1"></p>  
 <input id="reurl" runat="server" type="hidden" value="1" />   
           <input id="Button1" class="button" type="button" value="OK" onclick="javascript:HideMe()" />
</div>
<!--Alert Box End-->
</div><br /><br />
</div>
</div>

</asp:Content>

