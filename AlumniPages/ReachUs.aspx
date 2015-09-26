<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/AlumniHeadFoot.master" AutoEventWireup="true" CodeFile="ReachUs.aspx.cs" Inherits="AlumniPages_ReachUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
 </style>
 <link media="all" href="../css/contactform.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

		<div class="top">
			<div class="container">
				<div class="clearfix">
<div class="grid3 first">
						<h2 style="font-size:25px">Did you know?</h2>
						<div class="img-wrap1"><figure><img src="../images/reachus.jpg" alt=""></figure></div>
						<p>You can reach us with any queries through Contact Form.</p>
						
                        </div>
					<div class="grid9">
						<h2 style="font-size:25px;text-align:right;"><img src="../images/logobig3.png" /> Contact Us</h2>
             <img src="../images/active.png" width="100%" height="3px" /><br />
					  <div id="body1">
    <div id="container1">
	<div id="form2">	
			<fieldset><legend>Contact form</legend>
				<p class="first">
					<label for="name">Name</label>
					<asp:TextBox ID="name" runat="server" ontextchanged="name_TextChanged"></asp:TextBox>
                     <%=nameerr %>
				</p>
				<p>
					<label for="web">Subject</label>
			<asp:DropDownList ID="subject" runat="server" oninit="subject_Init" CssClass="dropdown"
                                        Width="258px" onselectedindexchanged="subject_SelectedIndexChanged"></asp:DropDownList>
                   <%=suberr %>
				</p>			
				<p>
					<label for="email">Email</label>
					<asp:TextBox ID="email" runat="server" ontextchanged="email_TextChanged"></asp:TextBox>
                    <%=emailerr %>
				</p>
			</fieldset>
			<fieldset>																			
				<p>
					<label for="message">Message</label>
					<textarea name="message" id="msg" runat="server" cols="30" rows="10"></textarea>
                    <%=bodyerr %>
				</p>								
			</fieldset>			
			<p class="submit">
            <br />
            <button id="SendMail" type="button" onserverclick="SendMail_Click" runat="server">Submit</button>
            </p>		
		</div>	
</div>				
</div>	
</div><br />
                     <!--Alert Box -->
       <div id="AlertBox" class="alert">
<p id="errmsg1"></p>  
 <input id="reurl" runat="server" type="hidden" value="1" />   
           <input id="Button1" class="button" type="button" value="OK" onclick="javascript:HideMe()" />
</div>
<!--Alert Box End-->     
				</div>
                </div>
                </div>
		<div class="middle">
			<div class="container">
				<div class="clearfix">
					<div class="grid3 first">
						<h2 style="font-size:23px">Reach Us</h2>
						<div class="wrapper">
							<dl class="departments">
								<dt>Alumni Officer:</dt>
								<dd><span>Name:</span>Asha</dd>
								<dd><span>Telephone:</span>044-27475063-65,27475844-46</dd>
								<dd><span>E-mail:</span><a href="mailto:alumniofficer@ssn.edu.in">alumniofficer@ssn.edu.in</a></dd>
								
								<dt>SSN Instituion:</dt>
                               <dd><span>Address:</span> SSNCE</dd>
                               <dd>
                   Old Mahabalipuram Road,<br /> Kalavakam,<br />
                   Chennai - 603 110<br />
                   </dd>
                                <dd><span>Telephone:</span>044 27469700</dd>
								<dd><span>E-mail:</span><a href="mailto:info@ssn.edu.in">info@ssn.edu.in</a></dd>
								
								
							</dl>
						</div>
					</div>
					<div class="grid9">
						<h2 style="font-size:23px">Route Map</h2>
						<div class="img-box">
                        <iframe width="750" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=SSN+College+of+Engineering,+Tamil+Nadu,+India&amp;aq=0&amp;sll=16.573023,81.408691&amp;sspn=12.905793,19.753418&amp;vpsrc=6&amp;ie=UTF8&amp;hq=SSN+College+of+Engineering,+Tamil+Nadu,+India&amp;t=h&amp;ll=12.750823,80.19732&amp;spn=0.007325,0.009141&amp;z=16&amp;output=embed"></iframe>
                        <br />
                        <small><a href="http://maps.google.com/maps?f=q&amp;source=embed&amp;hl=en&amp;geocode=&amp;q=SSN+College+of+Engineering,+Tamil+Nadu,+India&amp;aq=0&amp;sll=16.573023,81.408691&amp;sspn=12.905793,19.753418&amp;vpsrc=6&amp;ie=UTF8&amp;hq=SSN+College+of+Engineering,+Tamil+Nadu,+India&amp;t=h&amp;ll=12.750823,80.19732&amp;spn=0.007325,0.009141&amp;z=16" style="color:#0000FF;text-align:left" target="_blank">View Larger Map</a></small>
					
						</div>	
                    </div>
				</div>
			</div>
		</div>

</asp:Content>

