<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderFooter.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        #table1 a
        {text-decoration:none;}
        #table1 a:hover
        {
            text-decoration:underline;
            }
 #links a
{text-decoration:none;font-size:18px;}
#links a:hover
{ text-decoration:underline;}
            
#table1 td:first-child
{
    font-size:14px;
    }
    #table1 td:nth-child(2)
    {color:white;
     font-weight:bold;}
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section id="content">
		<div class="top">
			<div class="container">
            <div class="clearfix">
            <!--Alert Box -->
       <div id="AlertBox" class="alert">
<p id="errmsg1"></p>  
 <input id="reurl" runat="server" type="hidden" value="1" />   
           <input id="Button1" class="submitbutton" type="button" value="OK" onclick="javascript:HideMe()" />
</div>
<!--Alert Box End-->
        <h2 style="font-size:25px;text-align:right"><img src="images/logobig3.png" />&nbsp;Login Page</h2>
         <img src="images/active.png" height="3px" width="100%" />      
        <br /><br />
     
        <center>
               <div id="did_you_know" style="height:inherit">
        <h2 style="font-size:23px;"><img src="images/login2.png" height="60px" style="margin-top:-10px;"/> Alumni Check-In</h2>
        
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                
            	<table class="textbox" id="table1" style="line-height:2em;" cellpadding="5">
               <tr><td >Username</td>
                <td>:</td>
                <td>
                <asp:TextBox ID="loginid" CssClass="userimg" runat="server" placeholder="Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="16px" runat="server" ErrorMessage="*" ControlToValidate="loginid" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="emailerr" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
                </tr>
                <tr><td>Password</td>
                <td>:</td>
                <td>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="Loginbutton">
                <asp:TextBox ID="loginpwd" runat="server" CssClass="passimg" TextMode="Password" 
                        placeholder="Password"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="16px" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="loginpwd" Display="Dynamic"></asp:RequiredFieldValidator>
                </asp:Panel>
                </td>               
                </tr>
                <tr><td colspan="3"></td></tr>
                <tr><td colspan="3" align="center">
<asp:Label ID="loginerr" ForeColor="Red" runat="server" Text=""></asp:Label>
             </td>
             </tr>
             <tr><td colspan="3" align="center">
<asp:Label ID="logmsg" ForeColor="Red" runat="server" Text=""></asp:Label>
             </td>
             </tr>
</table>
                 <div id="contactsform" style="width:100px;margin-left:25px;">
<asp:LinkButton ID="Loginbutton" CssClass="button" runat="server" Font-Underline="false" onclick="Loginbutton_Click">Login</asp:LinkButton>
               </div><br /><br /><br /> 
      <div id="links">
                <a href="RegisterForm.aspx">Sign Up</a> &nbsp;<font color="silver">|</font>&nbsp;
                <a class="forgot" href="ForgotPassword.aspx">Forgot your password?</a>
                <br /><br />
                </div>  
	      </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="Loginbutton" />
                    </Triggers>
             </asp:UpdatePanel>
                
		        </div>
                </center>
</div>
<br />
</div>
</div>
</section>

</asp:Content>

