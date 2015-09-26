<%@ Page Title="" Language="C#" MasterPageFile="~/HeaderFooter.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="Scripts/image-slider.js"></script>
<link href="css/Eventdetail.css" type="text/css" rel="Stylesheet" media="all" />
    <link href="css/image-slider.css" rel="stylesheet" type="text/css" /> 
<style type="text/css">

    .hi
        {
       font-size:15px;color:#E83709;line-height:40px;margin-bottom:25px;
        }
        #bigimg	a img {
		border: 0;
	}
</style>
<script type="text/javascript">
 
    function openEventDialog() {
        $('#wrapper').css('display', 'block');
        $('#boxpopup').css('display', 'block');
        $('#overlay').fadeIn('fast');
        $('#boxpopup').animate({ 'left': '30%' }, 500);
        //$("#annon").animate({ right: '-450px' }, 'slow'); will have no effect in code if uncommented
    }
    function closeEventDialog(prospectElementID) {
        $(function ($) {
            $(document).ready(function () {
                $('#' + prospectElementID).css('position', 'absolute');
                $('#' + prospectElementID).animate({ 'left': '-100%' }, 500, function () {
                    $('#' + prospectElementID).css('position', 'fixed');
                    $('#' + prospectElementID).css('left', '100%');
                    $('#overlay').fadeOut('fast');
                    $('#wrapper').css('display', 'none');
                    // $("#annon").animate({ right: '-10px' }, 'slow');will have no effect in code if uncommented
                });
            });
        });
    }
    $(document).ready(function () {
        $("#annon").animate({ right: '-10px' }, 'slow');
        return false;
    });
            // When clicking on the button close or the mask layer the popup closed
    $('a.closepic').live('click', function () {
        $('#mask , .login-popup').fadeOut(300, function () {
            $('#mask').remove();
        });
        return false;
    });
    function shownews() {
        //this code is for image display on body load
        var loginBox = '#login-box';
        $(loginBox).fadeIn(2000);

        //Set the center alignment padding + border
        var popMargTop = ($(loginBox).height() + 24) / 2;
        var popMargLeft = ($(loginBox).width() + 24) / 2;

        $(loginBox).css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });

        // Add the mask to body
        $('body').append('<div id="mask"></div>');
        $('#mask').fadeIn(2000);

    }
</script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!--This popup box is used to display event detail -->
  <div id="wrapper" style="display:none">
<div id="overlay" class="overlay"></div>
<div id="boxpopup" class="box">
	<a onclick="javascript:closeEventDialog('boxpopup')" class="boxclose"></a>
 	<div id="detbox">
        <asp:Label ID="evtdet" runat="server" Text=""></asp:Label>
    </div>
</div>
</div>
 
 <!--This popup for displaying tribute image as startup-->
<div id="login-box" class="login-popup">
    <a href="#" class="closepic"><img src="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>
    <embed src="Alumni Brochure.swf" height="590" width="880"/>
          </div>
 
<!--This Div is for Announcement -->
 <div id="annon">
   <table cellpadding="5">
   <tr>
   <td valign="middle"><img src="images/annon.gif"/></td>
   <td valign="middle" style="font-size:22px">Announcement</td>
   </tr>
   </table>
    <img src="images/active.png" height="3px" width="100%"/>
    <br /><br />
    <asp:Label ID="annonmsg" runat="server" Text=""></asp:Label>
    </div>

    <section id="content">
		<div class="top">
			<div class="container">
				<div class="clearfix">
               	<section id="gallery">
                    <br />
						 <div id="sliderFrame">
                             <div id="slider">
                                   <img src="images/slide1.jpg" alt="SSN Management Group Members" />
                                   <img src="images/slide2.jpg" alt="SSN Admin Block" />
                                   <img src="images/slide3.jpg" alt="SSN Lawn" />
                                   <img src="images/slide5.jpg" alt="SSN Auditorium"/>
                                   <img src="images/slide6.jpg" alt="SSN Auditorium"/>
                                   <img src="images/slide7.jpg" alt="SSN Lawn"/>
                                   <img src="images/slide8.jpg" alt="Lawn at night view"/>
                             </div>
                        </div>
					</section>
					<section id="intro">
						<div class="inner">
							<h2>SSN Unite</h2>
						    <img src="images/t1.png" height="200" width="200" />
                       </div>
                    </section>
              </div>
			</div>
		</div>
		<div class="middle"> 
			<div class="container">
				<div class="wrapper">
               
					<div class="grid3 first">
                
               		<ul class="categories">
                          <span id="eventlinks" runat="server"></span>
                        
                      </ul>
					</div>
                  <div class="grid9" id="events" style="display:block">
                    	<h2><asp:Label ID="EventHead" runat="server"></asp:Label></h2>
	<p>
    <asp:Label ID="EventStory" runat="server"></asp:Label> 
    <br /><br />
     <a href="#" class="more" onclick="javascript:openEventDialog()">Read More</a>
     </p>
						 <section class="images" id="bigimg">
							<figure ><a id="aimg1" href="#" runat="server" class="hover"><img id="img1" height="136" width="191" runat="server" alt=""/></a></figure>
							<figure ><a href="#" id="aimg2" runat="server" class="hover"><img id="img2" height="136" width="191" runat="server" alt=""/></a></figure>
							<figure ><a href="#" id="aimg3" class="hover" runat="server"><img id="img3" height="136" width="191" runat="server"  alt=""/></a></figure>
						</section>
						
					</div>
				</div>
			</div>
		</div>
        
          </section>

</asp:Content>

