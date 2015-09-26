<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server"> 
    
    <!--<link href="css/js-image-slider.css" rel="stylesheet" type="text/css" />
    <link href="css/slider.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/js-image-slider.js" type="text/javascript"></script>
    <script src="Scripts/jquery-slider.js" type="text/javascript"></script>
     -->
     <style type="text/css">
     /*
.div1, .div2 {width:800px;margin:0 auto;}
.div1 {margin-top:30px;margin-bottom:60px;text-align:center;line-height:20px;}
.div1 P {font-size:18px;}
.div1 a, .div2 a {color:#07C;}
.div2 {margin-top:70px;}
.div2 li {padding-top:6px;padding-bottom:6px;}
.floatLeft {float:left;}*/
div{margin :0 auto;width:500px;}
   </style>
   
<script type="text/javascript">
   /* function move_right() {
        var get_rght = document.getElementById('mar');
        get_rght.style.marginRight += 5;
    }
  
    function move_left() {

        var get_left = document.getElementById('mar');
        get_left.style.marginLeft += 5;
        
    }

    function rotate() {
        document.getElementById('AnIdNotADiv').style.marginTop = value + "px";
         document.write(myMarginTop + 'gg');
    }*/
    
</script>

 
    <title></title> 
      
</head>
<body onload="rotate();">
		<form id="form_1" runat="server">
    
   <!-- <div class="div2" style="background-color:white;padding:40px 0 50px 20px;border:1px solid #E3E3E3;">
        <div class="floatLeft">
            <div id="mcts1">
                <img src="images/android/2012/thumbs/img1.jpg" />
                <img src="images/android/2012/thumbs/img2.jpg" />
                <img src="images/android/2012/thumbs/img3.jpg" />
                <img src="images/android/2012/thumbs/img4.jpg" />
                <img src="images/android/2012/thumbs/img5.jpg" />
                <img src="images/android/2012/thumbs/img6.jpg" />
                <img src="images/android/2012/thumbs/img7.jpg" />
                <img src="images/android/2012/thumbs/img8.jpg" />
                <img src="images/android/2012/thumbs/img9.jpg" />
                <img src="images/android/2012/thumbs/img10.jpg" />
                <img src="images/android/2012/thumbs/img11.jpg" />
            </div>
        </div>
        <div class="floatLeft">
            <div id="sliderFrame">
                <div id="slider">
                    <img src="images/android/2012/slides/img1.jpg" alt="#slideshow-1"/>
                    <img src="images/android/2012/slides/img2.jpg" alt="#slideshow-2" />
                    <img src="images/android/2012/slides/img3.jpg" alt="#slideshow-3" />
                    <img src="images/android/2012/slides/img4.jpg" alt="#slideshow-5" />
                    <img src="images/android/2012/slides/img5.jpg" alt="#slideshow-6" />
                    <img src="images/android/2012/slides/img6.jpg" alt="#slideshow-7" />
                    <img src="images/android/2012/slides/img7.jpg" alt="#slideshow-8" />
                    <img src="images/android/2012/slides/img8.jpg" alt="#slideshow-9" />
                    <img src="images/android/2012/slides/img9.jpg"  alt="#slideshow-10"/>
                    <img src="images/android/2012/slides/img10.jpg"  alt="#slideshow-11"/>
                    <img src="images/android/2012/slides/img11.jpg"  alt="#slideshow-12"/>
                </div>
            </div>
        </div>
        <div style="clear:both;"></div>
        <div style="display:none;">
            <div id="slideshow-1">
                <h3>Welcome to Menucool jQuery Slideshow</h3>
                This demo shows how the jQuery slideshow (or Thumbnail Slider if using the pure JavaScript) can work together with the JavaScript Image Slider.
            </div>
            <div id="slideshow-2">
                <h3>Enhanced Slideshow Effect</h3>
                The jQuery Slideshow/thumbnail slider works nicely together with the JavaScript Image Slider which greatly enhanced the slideshow with an added aesthetic appeal.
            </div>
            <div id="slideshow-3">
                <h3>SEO Friendly</h3>                    
                The markup is valid HTML5 and SEO optimzied, with all content always being available to search engines. 
            </div>
            <div id="slideshow-4">
                <h3>A Tool for Web Designer</h3>
                Its goal is to simplify the process of creating professional image/content slideshow for the web and mobile devices. This jQuery Slideshow will entertain your audience while scrolling what feature your website has.
            </div>
            <div id="slideshow-5">
                <h3>JavaScript Image Slider is driven by the jQuery/Thumbnail Slideshow</h3>                    
                Each slide of the JavaScript Image Slider is triggered by the thumbnail scrolling.
            </div>
            <div id="slideshow-6">
                <h3>Rich HTML</h3>                    
                Rich HTML content can be added to the thumbnails, or to the description of each JavaScript Image Slider slide. 
            </div>
            <div id="slideshow-7">
                <h3>Fully Customizable</h3>                    
                The jQuery Slideshow, or Thumbnail Slider if using the pure JavaScript, can be horizontal or vertical, and its styles and behaviors are fully customizable by tweaking the CSS and the JavaScipt. 
            </div>
            <div id="slideshow-8">
                <h3>Varying Width, or Varying Height</h3>                    
                The varying size (either varying width for the horizontal jQuery slideshow, or varying height for the vertical slider) is allowed for the thumbnails. 
            </div>
            <div id="slideshow-9">
                <h3>A Great Way to Showcase Your Products</h3>                    
                    It is a perfect way to showcase your products. This script allows you to easily animate any series of elements, by sequentially scrolling them.
            </div>
            <div id="slideshow-11">
                <h3>Wide Browsers and Devices Support</h3>                    
                The carousel is completely configurable and compatible with all major browsers (including ie6+, Firefox Chrome, Opera, Safari) and mobile platforms like iphone / ipad. 
            </div>
            <div id="slideshow-12">
                <h3>A Great Carousel</h3>  
                Use this stunning carousel to entertain your readers while scrolling what feature your website has. It has a very smooth user interface that gives your slider an added aesthetic appeal. 
            </div>
            <div id="slideshow-10">
                <h3>Easy to Use</h3>                    
                Enhance your website by adding a unique and attractive slider! And it is easy: download, copy & paster, tweak the styles to your desires. Well, that's it.
            </div>
        </div> 
    </div>
   
</div>
-->
    <div id='show_me'>
    <div id='mar'>Welcome to HTML</div>
    </div>



        </form>	
 </body> 
</html>