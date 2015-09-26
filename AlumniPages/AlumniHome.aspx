<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/AlumniHeadFoot.master" AutoEventWireup="true" CodeFile="AlumniHome.aspx.cs" Inherits="AlumniPages_AlumniHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="../css/Eventdetail.css" type="text/css" rel="Stylesheet" media="all" />
<script type="text/javascript" src="../Scripts/image-slider.js"></script>
<link href="../css/image-slider.css" rel="stylesheet" type="text/css" /> 
<script type="text/javascript">

    function openEventDialog() {
        $('#wrapper1').css('display', 'block');
        $('#boxpopup1').css('display', 'block');
        $('#overlay1').fadeIn('fast');
        $('#boxpopup1').animate({ 'left': '30%' }, 500);

    }
    function closeEventDialog(prospectElementID) {
        $(function ($) {
            $(document).ready(function () {
                $('#' + prospectElementID).css('position', 'absolute');
                $('#' + prospectElementID).animate({ 'left': '-100%' }, 500, function () {
                    $('#' + prospectElementID).css('position', 'fixed');
                    $('#' + prospectElementID).css('left', '100%');
                    $('#overlay1').fadeOut('fast');
                    $('#wrapper1').css('display', 'none');

                });
            });
        });
    }
    function showme() {
        document.getElementById('showedit').style.display = "block";
    }
    function hideme1() {
        document.getElementById('showedit').style.display = "none";
    }

   
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="top">
			<div class="container">
				<div class="clearfix"> 
                <div style="float:left"><h2 style="font-size:25px;color:#06cbe2;">Welcome <asp:Label ID="showuser" runat="server" Text=""></asp:Label>,</h2></div>
                <div style="float:right"><h2 style="text-align:right;font-size:18px">Last login was on <asp:Label ID="logdatetime" runat="server" Text=""></asp:Label> </h2>
                </div><br /><br /><br />
                
					<section id="gallery">
						<br />
						 <div id="sliderFrame">
                             <div id="slider">
                                   <img src="../images/slide1.jpg" alt="SSN Management Group Members" />
                                   <img src="../images/slide2.jpg" alt="SSN Admin Block" />
                                   <img src="../images/slide3.jpg" alt="SSN Lawn" />
                                   <img src="../images/slide5.jpg" alt="SSN Auditorium"/>
                                   <img src="../images/slide6.jpg" alt="SSN Auditorium"/>
                                   <img src="../images/slide7.jpg" alt="SSN Lawn"/>
                                   <img src="../images/slide8.jpg" alt="Lawn at night view"/>
                             </div>
                        </div>
					</section>
					<section id="intro">
						<div class="inner">
						    <h2 style="font-size:23px">Recently Registered Alumni</h2>
                              <section class="images">
                              <div id="addlist">
                              <table id="recentlist" runat="server" cellpadding="6">
                              </table>
                             </div>
                    	</section>
                        
					  	</div>
                    </section>
             </div>
    </div>
    </div>
		<div class="middle">
			<div class="container">
				<div class="wrapper">
					<div class="grid3 first">
                    <h2>Events</h2>
						<ul class="categories">
							 <span id="eventlinks" runat="server"></span>
						</ul>
					</div>
					<div class="grid9" id="events">
						<h2><asp:Label ID="EventHead" runat="server" Text=""></asp:Label>  </h2>
                        	<p>  
                            <asp:Label ID="EventStory" runat="server" Text=""></asp:Label> 
                             <a href="#" class="more" onclick="javascript:openEventDialog()">Read More</a>
                            </p>
						 <section class="images" id="bigimg">
						    <figure><a href="#" id="aimg1" runat="server" class="hover"><img id="img1" height="136" width="191" runat="server" alt=""></img></a></figure>
							<figure><a href="#" id="aimg2" runat="server" class="hover"><img id="img2" height="136" width="191" runat="server" alt=""></img></a></figure>
							<figure><a href="#" id="aimg3" class="hover" runat="server"><img id="img3" height="136" width="191" runat="server"  alt=""></img></a></figure>
						</section>
					</div>
				</div>
			</div>
		</div>
		
     <!--This popup box is used to display Messages-->
  <div id="wrapper1" style="display:none">
<div id="overlay1" class="overlay1"></div>
<div id="boxpopup1" class="box1">
	<a onclick="javascript:closeEventDialog('boxpopup1')" class="boxclose1"></a>
 	<div id="detbox1">
         <asp:Label ID="evtdet" runat="server" Text=""></asp:Label>
    </div>
</div>
</div>
</asp:Content>

