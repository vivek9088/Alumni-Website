<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderFooter.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section id="content">
		<div class="top">
			<div class="container">
            <div class="clearfix">
				<h2 style="font-size:25px;float:right"><img src="images/logobig3.png" /> Forgot Password</h2>
              <img src="images/active.png" height="3px" width="100%" />      
             <br /><br />
             
				<p style="color:Black;">Kindly submit your email  here and we will send  your password to your email id.</p>
            <br />
            <table>
            <tr>
            <td style="color:Black;"> Enter Email-Id here </td><td><b>:</b></td>
            <td><asp:TextBox ID="emailid" runat="server"></asp:TextBox>
           <br /> 
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email Id format is wrong" ControlToValidate="emailid" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
            </tr>
            </table>
           <br />
           
            <div id="contactsform" style="width:100px;margin-left:100px;">
            <asp:Button ID="Submit" runat="server" Text="Submit" CausesValidation="false" CssClass="button" 
                    onclick="Submit_Click"></asp:Button> 

               </div>
           
            </div><br /><br />
                    <div class="ui-widget" id="alertdiv" style="display:none" runat="server">
	<div class="ui-state-error ui-corner-all" style="padding: 0 .7em;">
		<p><span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
		<strong>Alert:</strong> <asp:Label ID="emailerr" ForeColor="white" runat="server" Text=""></asp:Label></p>
	</div>
</div>

            <br /><br />
         </div>
	</div>

	</section>

</asp:Content>

