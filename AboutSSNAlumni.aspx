<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderFooter.master" AutoEventWireup="true" CodeFile="AboutSSNAlumni.aspx.cs" Inherits="AboutSSNAlumni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
     #bigimg	a img {
		border: 0;
	}
	#largeImage {
		position: absolute;
		padding: 8px;
		background-color: #e3e3e3;
		border: 1px solid #bfbfbf;
	}
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<section id="content">
		<div class="top">
			<div class="container">
            <div class="clearfix">
				<h2 style="font-size:25px;text-align:right;"><img src="images/logobig3.png" />&nbsp;Welcome to the  SSN Alumni Network</h2>
                <img src="images/active.png" height="3px" width="100%" />      
             <br /><br />
              <center> <img alt="" src="images/about-alumni.jpg" style="height: 108px; width:950px;"/></center>
              <br />
            
				<p style="text-align:justify;line-height:2em">SSN Alumni Association has great pleasure in welcoming you. We hope and wish that you would be the role models to the present and all future batches of SSN students. At SSN we believe in fostering a strong alumni network that not only helps former students remain connected but also provide an avenue for the philanthropic spirit of successful alumni. The Alumni Association will provide a platform for sharing your intellectual, cultural, career and professional experiences not just with the present students but also with other alumni. Our goal in the Alumni Office is to enable you to remain connected with SSN in order to promote stronger connections, and help provide opportunities for dialogue, sharing knowledge, voluntary service, social interaction and philanthropy. </p>
            <br />
            </div>
         </div>
	</div>
    <div class="middle"> 
			<div class="container">
				<div class="wrapper">
               
					<div class="grid3 first">
                   						<ul class="categories">
							 <li><a href="#events" class="current1">Parent Body - Chennai Chapter</a></li>
                        <li><a href="#events">USA Chapter</a></li>
                        <li><a href="#events">SAR</a></li>
							<!--<span id="eventlinks" runat="server"></span>-->
						<li><a href="#events">Business Quiz</a></li>
						</ul>
					</div>
					<div class="grid9" id="events">
                    	<h2><%Response.Write(EventHead); %></h2>
	<p> <%Response.Write(EventStory); %>   </p>
						 <section class="images" id="bigimg">
							<figure><a href="#" id="aimg1" runat="server" class="hover"><img id="img1" height="136" width="191" runat="server" alt=""/></a></figure>
							<figure><a href="#" id="aimg2" runat="server" class="hover"><img id="img2" height="136" width="191" runat="server" alt=""/></a></figure>
							<figure><a href="#" id="aimg3" class="hover" runat="server"><img id="img3" height="136" width="191" runat="server"  alt=""/></a></figure>
						</section>
						
					</div>
				</div>
			</div>
		</div>
	</section>

</asp:Content>

