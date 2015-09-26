<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="AlumniProfHome.aspx.cs" Inherits="AlumniPages_AlumniProfHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/jquery.akordeon.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../css/accordionmenu.css" type="text/css" media="all"/>   
    
    <style type="text/css">
        .sideright
        {
         float:right;
          }
        
        </style>
         <script type="text/javascript">
             $(document).ready(function () {
                 $('#buttons').akordeon();
                 $('#button-less').akordeon({ buttons: false, toggle: true, itemsOrder: [2, 0, 1] });

                 // if user clicked on button, the overlay layer or the dialogbox, close the dialog	
                 $('#close_me').click(function () {
                     $('#dialog-box').hide();
                     return false;
                 });

                 // if user resize the window, call the same function again
                 // to make sure the overlay fills the screen and dialogbox aligned to center	
                 $(window).resize(function () {

                     //only do it if the dialog box is not hidden
                     if (!$('#dialog-box').is(':hidden')) popupmsg();
                 });
             });

             //Popup dialog
             function popupmsg() {
                 $('#dialog-box').show();
             }
    
         //this code is for text area word count
             function taLimit() {
                 var taObj = event.srcElement;
                 if (taObj.value.length == taObj.maxLength * 1) return false;
             }

             function taCount(visCnt) {
                 var taObj = event.srcElement;
                 if (taObj.value.length > taObj.maxLength * 1) taObj.value = taObj.value.substring(0, taObj.maxLength * 1);
                 if (visCnt) visCnt.innerText = taObj.maxLength - taObj.value.length;
             }

         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <!--Alert Box -->
       <div id="AlertBox" class="alert">
<p id="errmsg1"></p>  
 <input id="reurl" runat="server" type="hidden" value="1" />   
           <input id="Button1" class="button" type="button" value="OK" onclick="javascript:HideMe()" />
</div>
<!--Alert Box End--> 

    <div class="grid9">
    
                       <h2 style="font-size:23px;float:right;"><img src="../images/logobig3.png" />
                           <asp:Label ID="profhead" runat="server" Text="My Profile"></asp:Label>
                           </h2><img src="../images/active.png" width="100%" height="3px" />
                           <br /><br />
                           <div id="maintop">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="top" style="float:left">
        <img src="../images/nophoto.jpg" visible="false" id="profimg" runat="server" height="75" width="75" />
        &nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="imgadd" runat="server"  OnClick="imgadd_Click" CausesValidation="false" Visible="false" ImageUrl="../images/addfriend2.png"  />
       &nbsp; <asp:Image ID="friimg" runat="server" ImageUrl="../images/friends.png" Visible="false" 
                           Height="24px" />
       &nbsp; <asp:ImageButton ID="closefri" runat="server" ImageUrl="../images/closefri.jpg" Visible="false" 
                           Height="24px" OnClick="closefri_Click" CausesValidation="false" />
       </div>
      <div id="side" style="float:left;height:75px;margin-left:5px">
         
       <a id="sndmsg" onclick="popupmsg()" runat="server" visible="false" href="#"><img src="../images/sendmsg.png" alt="" height="24" /></a>
<div id="dialog-box">
	<div class="dialog-content">
<textarea id="alumsg" maxlength="100" onkeypress="return taLimit()" onkeyup="return taCount(myCounter)" runat="server" cols="32" rows="4" style="resize:none;"></textarea><br />       	    
           &nbsp;&nbsp;Characters left: <b><span id="myCounter">100</span></b> <br />
           <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="textarea" runat="server" ErrorMessage="Please enter a message" ForeColor="Red" SetFocusOnError="true" ControlToValidate="alumsg"></asp:RequiredFieldValidator><br />
 <asp:Button  ID="SendMessage" ValidationGroup="textarea" CssClass="sendbutton" 
                runat="server" Text="Send Message" onclick="SendMessage_Click" />          <a id="close_me" class="closemsg">Close</a>
   </div>
</div>
</div>
                           </ContentTemplate>
                           <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="SendMessage" EventName="Click" />
                           </Triggers>
       </asp:UpdatePanel>
       <span id="show_spaces" runat="server" visible="false"><br /><br /><br /><br /></span>
       </div>
        
    <div id="demo-wrapper">
        <div class="akordeon" id="buttons">
            <div class="akordeon-item expanded">
                <div class="akordeon-item-head">
                    <div class="akordeon-item-head-container">
                        <div class="akordeon-heading">
                           Personal Info
                        </div>
                    </div>
                </div>
                <div class="akordeon-item-body">
                    <div class="akordeon-item-content">
                            <table id="table-1" width="100%">
                    <tr>
                    <td>
                    <!-- starting of subtable - 1-->
                    <table id="subtable-1" width="100%">
                    <tr>
                    <td>User Role</td><td>:</td>
                    <td><asp:Label ID="alu_role" runat="server" Text=""></asp:Label> </td>
                    </tr>
                    <tr>
                    <td>First Name</td><td>:</td>
                    <td><asp:Label ID="alu_fname" runat="server" Text=""></asp:Label> </td>
                    </tr>
                    <tr>
                    <td>Last Name</td><td>:</td>
                    <td><asp:Label ID="alu_lname" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Gender</td><td>:</td>
                    <td><asp:Label ID="alu_gender" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Date Of Birth</td><td>:</td>
                    <td><asp:Label ID="alu_dob" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Email-Id</td><td>:</td>
                    <td><asp:Label ID="alu_email" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Batch</td><td>:</td>
                    <td><asp:Label ID="alu_batch" runat="server" Text=""/></td>
                    </tr>
                     </table><!-- end of subtable - 1-->
                     </td>
                    <td>
                    <!-- starting of subtable - 2-->
                    <table id="subtable-2" width="100%">
                    <tr>
                    <td>Degree</td><td>:</td>
                    <td><asp:Label ID="alu_degree" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Branch</td><td>:</td>
                    <td><asp:Label ID="alu_branch" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>City </td><td>:</td>
                    <td><asp:Label ID="alu_city" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>State </td><td>:</td>
                    <td><asp:Label ID="alu_state" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Country </td><td>:</td>
                    <td><asp:Label ID="alu_country" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Address </td><td>:</td>
                    <td><asp:Label ID="alu_addr" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Contact Number</td><td>:</td>
                    <td><asp:Label ID="alu_number" runat="server" Text=""/></td>
                    </tr>
                     </table><!-- end of subtable - 2-->
                    </td>
                    </tr>
                    </table>
                                           
                    </div>
                </div>
            </div>
            <div class="akordeon-item">
                <div class="akordeon-item-head">
                    <div class="akordeon-item-head-container">
                        <div class="akordeon-heading">
                            Educational Info
                        </div>
                    </div>
                </div>
                <div class="akordeon-item-body">
                    <div class="akordeon-item-content">
                        <table id="edutable" width="100%" border="2px">
                    <tr>
                    <th>Institution</th>
                    <th>Location</th>
                    <th>Course</th>
                    <th>Year of Passing</th>
                    </tr>
                   <tr id="edu1" runat="server">
                   <td><asp:Label ID="instname1" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instloc1" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instcourse1" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instyr1" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr id="edu2" runat="server">
                   <td><asp:Label ID="instname2" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instloc2" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instcourse2" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instyr2" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr id="edu3" runat="server">
                   <td><asp:Label ID="instname3" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instloc3" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instcourse3" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instyr3" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr id="edu4" runat="server">
                   <td><asp:Label ID="instname4" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instloc4" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instcourse4" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="instyr4" runat="server" Text=""></asp:Label></td>
                   </tr>
                    </table>
                    <br /><span id="norecord" runat="server" style="text-align:center" visible="false">No data found</span>
                    </div>
                </div>
            </div>
            
            <div class="akordeon-item">
                <div class="akordeon-item-head">
                    <div class="akordeon-item-head-container">
                        <div class="akordeon-heading">
                        Organization Info
                        </div>
                    </div>
                </div>
                <div class="akordeon-item-body">
                    <div class="akordeon-item-content">
                      <table id="worktable" width="100%" border="2px">
                    <tr>
                    <th>Company Name</th>
                    <th>Designation</th>
                    <th>Location</th>
                    <th>Years of Experience</th>
                    </tr>
                      <tr id="work1" runat="server">
                   <td><asp:Label ID="cmpname1" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpdsg1" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmploc1" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpyr1" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr id="work2" runat="server">
                   <td><asp:Label ID="cmpname2" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpdsg2" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmploc2" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpyr2" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr id="work3" runat="server">
                   <td><asp:Label ID="cmpname3" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpdsg3" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmploc3" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpyr3" runat="server" Text=""></asp:Label></td>
                   </tr>
                   <tr id="work4" runat="server">
                   <td><asp:Label ID="cmpname4" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpdsg4" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmploc4" runat="server" Text=""></asp:Label></td>
                   <td><asp:Label ID="cmpyr4" runat="server" Text=""></asp:Label></td>
                   </tr>
                    </table>  
                    </div>
                </div>
            </div>

            <div class="akordeon-item">
                <div class="akordeon-item-head">
                    <div class="akordeon-item-head-container">
                        <div class="akordeon-heading">
                        About Me
                        </div>
                    </div>
                </div>
                <div class="akordeon-item-body">
                    <div class="akordeon-item-content">
                              <table id="othertable" width="100%" cellpadding="5">
                    <tr>
                    <td>About Myself</td><td>:</td>
                    <td><asp:Label ID="other_myself" runat="server" Text=""></asp:Label> </td>
                    </tr>
                    <tr>
                    <td>Unforgettable Moment in SSN</td><td>:</td>
                    <td><asp:Label ID="other_moment" runat="server" Text=""></asp:Label> </td>
                    </tr>
                    <tr>
                    <td>Hobbies</td><td>:</td>
                    <td><asp:Label ID="other_hobby" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Music</td><td>:</td>
                    <td><asp:Label ID="other_music" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Movies</td><td>:</td>
                    <td><asp:Label ID="other_movie" runat="server" Text=""/></td>
                    </tr>
                     </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
          <br />
            </div>
</asp:Content>

