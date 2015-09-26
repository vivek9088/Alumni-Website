<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="EventList.aspx.cs" Inherits="AlumniPages_EventList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/styles.css" />
    
    <script type="text/javascript" src="../Scripts/cufon-yui.js"></script>
    <script type="text/javascript">
    $(document).ready(function () {
    //var default_image = '../images/tribute/2012/slides/img1.jpg';
    var value = document.getElementById('<%=HiddenField1.ClientID%>').value + 'slides/img1.jpg';
    var default_image = value;
    var default_caption = 'Image-1';
    /*Load The Default Image*/
    loadPhoto(default_image, default_caption);

    function loadPhoto($url, $caption) {
        /*Image pre-loader*/
        showPreloader();
        var img = new Image();
        jQuery(img).load(function () {
            jQuery(img).hide();
            hidePreloader();
        }).attr({ "src": $url });

        $('#largephoto')
        // .css('width', '443px')
        //.css('height', '369px')
        //.css('background-repeat', 'no-repeat')
                .css('background-image', 'url("' + $url + '")');
        $('#largephoto').data('caption', $caption);
    }

    /* When a thumbnail is clicked*/
    $('.thumb_container').click(function () {

        var handler = $(this).find('.large_image');
        var newsrc = handler.attr('src');
        var newcaption = handler.attr('rel');
        loadPhoto(newsrc, newcaption);
    });

    /*When the main photo is hovered over*/
    $('#largephoto').hover(function () {
        var currentCaption = ($(this).data('caption'));
        var largeCaption = $(this).find('#largecaption');
        largeCaption.stop();
        largeCaption.css('opacity', '0.9');
        largeCaption.find('.captionContent').html(currentCaption);
        largeCaption.fadeIn()
        largeCaption.find('.captionShine').stop();
        largeCaption.find('.captionShine').css("background-position", "-550px 0");
        largeCaption.find('.captionShine').animate({ backgroundPosition: '550px 0' }, 700);
        Cufon.replace('.captionContent');
    },

	 function () {
	     var largeCaption = $(this).find('#largecaption');
	     largeCaption.find('.captionContent').html('');
	     largeCaption.fadeOut();

	 });
    /* When a thumbnail is hovered over*/
    $('.thumb_container').hover(function () {
        $(this).find(".large_thumb").stop().animate({ marginLeft: -7, marginTop: -7 }, 200);
        $(this).find(".large_thumb_shine").stop();
        $(this).find(".large_thumb_shine").css("background-position", "-99px 0");
        $(this).find(".large_thumb_shine").animate({ backgroundPosition: '99px 0' }, 700);
    }, function () {
        $(this).find(".large_thumb").stop().animate({ marginLeft: 0, marginTop: 0 }, 200);
    });

    function showPreloader() {
        $('#loader').css('background-image', 'url("../images/interface/loader.gif")');
    }

    function hidePreloader() {
        $('#loader').css('background-image', 'url("")');
    }
});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
<div class="grid9">
<h2 style="font-size:25px;float:right"><img src="../images/logobig3.png" />  
    <asp:Label ID="header1" runat="server" Text=""></asp:Label></h2>
<img src="../images/active.png" height="3px;" width="100%" />

    <div id="container">
        <div class="mainframe">
            <div id="largephoto">
                <div id="loader">
                </div>
                <div id="largecaption">
                    <div class="captionShine">
                    </div>
                    <div class="captionContent">
                    </div>
                </div>
                <div id="largetrans">
                </div>
            </div>
        </div>
        <div class="thumbnails" id="showimgs" runat="server">
            <br />
        </div>
    </div>
    <asp:DataList ID="EventList" runat="server" RepeatColumns="4">
    <ItemTemplate>
   <div id="bigimg" >
   
   <%#Container.DataItem %>
   
   </div>
    </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="everr" Visible="false" runat="server" Text="No Images Found"></asp:Label>
    <br /><br />
            <a id="link" href="EventGallery.aspx" class="backlink" runat="server">< &nbsp;&nbsp;Back</a>

</div>
    
</asp:Content>

