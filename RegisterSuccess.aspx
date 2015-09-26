<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderFooter.master" AutoEventWireup="true" CodeFile="RegisterSuccess.aspx.cs" Inherits="RegisterSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function Message(msg) {
        var id = 'AlertBox1';
        var left = 400, top = 200;
        document.getElementById(id).style.left = left + 'px';
        document.getElementById(id).style.top = top + 'px';
        document.getElementById(id).style.display = 'block';
        document.getElementById('msg1').innerHTML = msg;

        var sid = 'overlayhf';
        document.getElementById(sid).style.display = 'block';
    }
    function hideme() {
        document.getElementById('AlertBox1').style.display = 'none';

        var sid = 'overlayhf';
        document.getElementById(sid).style.display = 'none';
    }

    String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ""); };

    function checkEmptyValue(o) {
        if (o.value == '' || o.value.trim() == '')
            return false;
        else
            return true;
    }
    
</script>
<style type="text/css">  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <!--Alert Box -->
<div id="overlayhf" class="overlay1" style="display:none"></div>
       <div id="AlertBox1" class="alert">
<p id="msg1"></p>  
     <a class="close" onclick="javascript:hideme()"></a>
</div>
<!--Alert Box End-->

<section id="content">
		<div class="top">
			<div class="container">
            <div class="clearfix">

<h2 style="font-size:25px;float:right"> <img src="images/logobig3.png"/> USER REGISTRATION PROCESS</h2>
     <img src="images/active.png" height="3px" width="100%" />      
     <br /><br />
    <h2 style="font-size:23px;">Registered Successfully</h2>
  <font style="font-size:16px;line-height:1.5em;color:Gray;"> Thank you for registering with SSN Alumni. There's just one more step before you
    can login and start using your new account. We just sent you an activation email. Please check your email to
    retrieve it, and then click on the link in the email to confirm your new account(and email address). </font>
   <br /><br /><b>Note :</b>
   <font color="red">In Case if you didn't receive any mail from us please contact the admin for any help or queries. Thank you for your co-operation.</font>
</div><br /><br /><br /><br /><br /><br /><br /><br />
</div>
</div>
</section>
</asp:Content>

