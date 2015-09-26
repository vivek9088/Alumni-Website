<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="AlumniProfile.aspx.cs" Inherits="AlumniProfile" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <title>Edit Profile</title>
    <link rel="stylesheet" href="../css/accordionmenu.css" type="text/css" media="all"/>
    <link href="../css/jquery.akordeon.css" rel="stylesheet" type="text/css" />
    <style type="text/css">	
 /*.radio
{
   line-height:2em;
    }*/
/*.listtop{margin-top:10px; }  */
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('#buttons').akordeon();
        $('#button-less').akordeon({ buttons: false, toggle: true, itemsOrder: [2, 0, 1] });
    });
    function showadv() {
        document.getElementById('advopt').style.display = "block";
        document.getElementById('advanced').removeAttribute("src");
        document.getElementById('advanced').setAttribute("src", "../images/minus.gif");
        document.getElementById('advanced').removeAttribute("onclick");
        document.getElementById('advanced').setAttribute("onclick", "hideme()");
    }
    function hideme() {
        document.getElementById('advopt').style.display = "none";
        document.getElementById('advanced').removeAttribute("src");
        document.getElementById('advanced').setAttribute("src", "../images/plus.gif");
        document.getElementById('advanced').removeAttribute("onclick");
        document.getElementById('advanced').setAttribute("onclick", "showadv()");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="grid9">
   
    <h2 style="font-size:23px;text-align:right"><img src="../images/logobig3.png" />Alumni Profile Details</h2>
    <img src="../images/active.png" width="100%" height="3px" /><br /><br />
            	 
                     
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
                    <table id="subtable-1" width="100%" cellpadding="5" style="vertical-align:middle">
                    <tr>
                    <td>User Role</td><td>:</td>
                    <td> <asp:Label ID="alu_role" runat="server" Text=""></asp:Label> </td>
                    </tr>
                    <tr>
                    <td>First Name</td><td>:</td>
                    <td><asp:TextBox ID="alu_fname" runat="server" Text=""></asp:TextBox> </td>
                    </tr>
                    <tr>
                    <td>Last Name</td><td>:</td>
                    <td><asp:TextBox ID="alu_lname" runat="server" Text=""/></td>
                    </tr>                        
                    <tr>
                    <td>Gender</td><td>:</td>
                    <td valign="middle"> <asp:RadioButtonList CssClass="radio" ID="alu_gender" runat="server" RepeatLayout="Flow" 
                                RepeatDirection="Horizontal" >
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            </asp:RadioButtonList>
                        
                    </td>
                    </tr>
                    <tr>
                    <td>Date Of Birth</td><td>:</td>
                    <td><asp:TextBox ID="alu_dob" onkeypress="return false;" runat="server" Text=""/>
                        <asp:CalendarExtender ID="alu_dob_CalendarExtender" runat="server" Format="yyyy-MM-dd" 
                            Enabled="True" TargetControlID="alu_dob" DaysModeTitleFormat="dd/MM/yyyy" PopupPosition="Right">
                            </asp:CalendarExtender>
                      
                    </td>
                    </tr>
                    <tr>
                    <td>Email-Id</td><td>:</td>
                    <td><asp:TextBox ID="alu_email" runat="server" Text=""/></td>
                     </tr>
                    <tr>
                    <td>Batch</td><td>:</td>
                    <td><label class="label"><asp:DropDownList CssClass="listtop" ID="alu_batch" runat="server" OnInit="alu_batch_Init" Width="135px"></asp:DropDownList></label>
                    </td>
                    </tr>
                     </table><!-- end of subtable - 1-->
                     </td>
                    <td>
                    <!-- starting of subtable - 2-->
                    <table id="subtable-2" width="100%" cellpadding="5">
                    <tr>
                     <td>Degree<br/>Branch</td><td>:<br />:</td>
             <td>
             <asp:UpdatePanel ID="profpanel" runat="server">
             <ContentTemplate>
             <label class="label">
                  <asp:DropDownList ID="alu_degree" runat="server" Width="135px" AutoPostBack="true" onselectedindexchanged="alu_degree_SelectedIndexChanged">
                     <asp:ListItem Value="-1">--Select a Degree--</asp:ListItem>
                <asp:ListItem>UG</asp:ListItem>
                <asp:ListItem>PG</asp:ListItem>
                     </asp:DropDownList></label>
               <br /><label class="label">
                    <asp:DropDownList CssClass="listtop" ID="alu_branch" runat="server" oninit="alu_branch_Init" Width="135px" ></asp:DropDownList></label>
                    </ContentTemplate>
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="alu_degree" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                   </td>
                </tr>
                    <tr>
                    <td>City </td><td>:</td>
                    <td><asp:TextBox ID="alu_city" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>State </td><td>:</td>
                    <td><asp:TextBox ID="alu_state" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Country </td><td>:</td>
                    <td><label class="label"><asp:DropDownList CssClass="listtop" ID="alu_country" oninit="alu_country_Init" Width="135px" runat="server">
                        </asp:DropDownList>
                    </label>
                    </td>
                    </tr>
                    <tr>
                    <td>Contact Number</td><td>:</td>
                    <td><asp:TextBox ID="alu_number" runat="server" Text=""/></td>
                    </tr>
                    <tr>
                    <td>Address </td><td>:</td>
                    <td><asp:TextBox ID="alu_addr" runat="server" Text="" TextMode="MultiLine" Width="90%"/></td>
                    </tr>
                     </table><!-- end of subtable - 2-->
                    </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr><td><asp:Button ID="Submit_prof" runat="server" Text="Update" CssClass="accbutton" CausesValidation="false" 
                            onclick="Submit_prof_Click" /></td></tr>
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
                    <table id="edutable" width="100%" >
                    <tr>
                    <th>Institution</th>
                    <th>Location(Country)</th>
                    <th>Course</th>
                    <th>Year of Passing</th>
                    </tr>
                    </table>
                    <table id="edurow" width="100%" runat="server" style="text-align:center">
                    <tr>
                    <td><asp:TextBox ID="alu_inst" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="alu_iloc" runat="server"></asp:TextBox></td>
                    <td>
                        <label class="label">
                            <asp:DropDownList ID="alu_course" runat="server">
                   <asp:ListItem>--Select a Course--</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                    <asp:ListItem>Graduation</asp:ListItem>
                    <asp:ListItem>Diploma</asp:ListItem>
                    <asp:ListItem>PG</asp:ListItem>
                    </asp:DropDownList>
                        </label>
                    </td>
                    <td><asp:TextBox ID="alu_passyr" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="alu_inst1" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="alu_iloc1" runat="server"></asp:TextBox></td>
                    <td><label class="label"><asp:DropDownList ID="alu_course1" runat="server">
                    <asp:ListItem>--Select a Course--</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                    <asp:ListItem>Graduation</asp:ListItem>
                    <asp:ListItem>Diploma</asp:ListItem>
                    <asp:ListItem>PG</asp:ListItem>
                    </asp:DropDownList>
                    </label>
                    </td>
                    <td><asp:TextBox ID="alu_passyr1" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="alu_inst2" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="alu_iloc2" runat="server"></asp:TextBox></td>
                    <td>  <label class="label">
                            <asp:DropDownList ID="alu_course2" runat="server">
                    <asp:ListItem>--Select a Course--</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                    <asp:ListItem>Graduation</asp:ListItem>
                    <asp:ListItem>Diploma</asp:ListItem>
                    <asp:ListItem>PG</asp:ListItem>
                    </asp:DropDownList>
                    </label>
                    </td>
                    <td><asp:TextBox ID="alu_passyr2" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="alu_inst3" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="alu_iloc3" runat="server"></asp:TextBox></td>
                    <td> <label class="label">
                            <asp:DropDownList ID="alu_course3" runat="server">
                    <asp:ListItem>--Select a Course--</asp:ListItem>
                    <asp:ListItem>X</asp:ListItem>
                    <asp:ListItem>XII</asp:ListItem>
                    <asp:ListItem>Graduation</asp:ListItem>
                    <asp:ListItem>Diploma</asp:ListItem>
                    <asp:ListItem>PG</asp:ListItem>
                    </asp:DropDownList>
                    </label>
                    </td>
                    <td><asp:TextBox ID="alu_passyr3" runat="server"></asp:TextBox></td>
                    </tr>
                    </table>
                   <br />
                    <asp:Button ID="Submit_edu" runat="server" Text="Update"  CausesValidation="false"
                            onclick="Submit_edu_Click" CssClass="accbutton" />
                    
                    
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
                    <table id="worktable" width="100%">
                    <tr>
                    <th align="left">Company Name</th>
                    <th align="left">Designation</th>
                    <th align="left">Location</th>
                    <th>Years of Experience</th>
                    </tr>
                    </table>
                    <table id="workrow" width="100%" runat="server">
                    <tr>
                    <td><asp:TextBox ID="cmp1" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="desig1" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="loc1" runat="server"></asp:TextBox></td>
                    <td>
                    <table id="worktab1">
                    <tr>
                    <td>Period From</td><td>:</td>
                    <td><label class="label"><asp:DropDownList ID="fmon1" runat="server" Width="70">
                        </asp:DropDownList>
                    </label>
                        <label class="label">
                            <asp:DropDownList ID="fyear1" runat="server" Width="65">
                        </asp:DropDownList>
                        </label>
                        </td>
                        </tr>
                        <tr>
                        <td>Period To</td><td>:</td>
                        <td>
                            <label class="label">
                                <asp:DropDownList ID="tmon1" runat="server" Width="70">
                        </asp:DropDownList>
                            </label>
                                <label class="label"> 
                                    <asp:DropDownList ID="tyear1" runat="server" Width="65">
                        </asp:DropDownList>
                                </label>
                        </td>
                        </tr>
                    
                    </table>
                    </td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="cmp2" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="desig2" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="loc2" runat="server"></asp:TextBox></td>
                      <td>
                    <table id="worktab2">
                    <tr>
                    <td>Period From</td><td>:</td>
                    <td>
                        <label class="label">
                            <asp:DropDownList ID="fmon2" runat="server" Width="70"> 
                        </asp:DropDownList>
                        </label>
                            <label class="label">
                                <asp:DropDownList ID="fyear2" runat="server" Width="65">
                        </asp:DropDownList>
                            </label>
                        </td>
                        </tr>
                        <tr>
                        <td>Period To</td><td>:</td>
                        <td>
                            <label class="label">
                                <asp:DropDownList ID="tmon2" runat="server" Width="70">
                        </asp:DropDownList>
                            </label>
                                <label class="label">
                                    <asp:DropDownList ID="tyear2" runat="server" Width="65">
                        </asp:DropDownList>
                                </label>
                        </td>
                        </tr>
                    
                    </table>
                    </td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="cmp3" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="desig3" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="loc3" runat="server"></asp:TextBox></td>
                     <td>
                    <table id="worktab3">
                    <tr>
                    <td>Period From</td><td>:</td>
                    <td>
                        <label class="label">
                            <asp:DropDownList ID="fmon3" runat="server" Width="70">
                        </asp:DropDownList>
                        </label>
                            <label class="label">
                                <asp:DropDownList ID="fyear3" runat="server" Width="65">
                        </asp:DropDownList>
                            </label>
                        </td>
                        </tr>
                        <tr>
                        <td>Period To</td><td>:</td>
                        <td>
                            <label class="label">
                                <asp:DropDownList ID="tmon3" runat="server" Width="70">
                        </asp:DropDownList>
                            </label>
                                <label class="label">
                                    <asp:DropDownList ID="tyear3" runat="server" Width="65">
                        </asp:DropDownList>
                                </label>
                        </td>
                        </tr>
                    
                    </table>
                    </td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="cmp4" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="desig4" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="loc4" runat="server"></asp:TextBox></td>
                     <td>
                    <table id="worktab4">
                    <tr>
                    <td>Period From</td><td>:</td>
                    <td>
                        <label class="label">
                            <asp:DropDownList ID="fmon4" runat="server" Width="70">
                        </asp:DropDownList>
                        </label>
                            <label class="label"> 
                                <asp:DropDownList ID="fyear4" runat="server" Width="65">
                        </asp:DropDownList>
                            </label>
                        </td>
                        </tr>
                        <tr>
                        <td>Period To</td><td>:</td>
                        <td>
                            <label class="label">
                                <asp:DropDownList ID="tmon4" runat="server" Width="70">
                        </asp:DropDownList>
                            </label>
                                <label class="label"> 
                                    <asp:DropDownList ID="tyear4" runat="server" Width="65">
                        </asp:DropDownList>
                                </label>
                        </td>
                        </tr>
                    
                    </table>
                    </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                    <td><asp:Button ID="Submit_work" runat="server" Text="Update" CausesValidation="false"
                            onclick="Submit_work_Click" CssClass="accbutton" /></td>
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
              
                    <table id="othertable" width="100%">
                    <tr>
                    <td>About Myself</td><td>:</td>
                    <td><asp:TextBox ID="other_myself" runat="server" Text="" Width="90%" TextMode="MultiLine"></asp:TextBox> </td>
                    </tr>
                    <tr>
                    <td>Unforgettable Moment in SSN</td><td>:</td>
                    <td><asp:TextBox ID="other_moment" runat="server" Text="" Width="90%" TextMode="MultiLine"></asp:TextBox> </td>
                    </tr>
                    <tr>
                    <td>Hobbies</td><td>:</td>
                    <td><asp:TextBox ID="other_hobby" runat="server" Text="" Width="90%" TextMode="MultiLine"/></td>
                    </tr>
                    <tr>
                    <td>Music</td><td>:</td>
                    <td><asp:TextBox ID="other_music" runat="server" Text="" Width="90%" TextMode="MultiLine"/></td>
                    </tr>
                    <tr>
                    <td>Movies</td><td>:</td>
                    <td><asp:TextBox ID="other_movie" runat="server" Text="" Width="90%" TextMode="MultiLine"/></td>
                    </tr>
                    <tr><td></td></tr>
                    <tr><td><asp:Button ID="Submit_other" runat="server" Text="Update" CausesValidation="false"
                            onclick="Submit_other_Click" CssClass="accbutton" /></td></tr>
                     </table>
                    
                      </div>
                </div>
            </div>
            
        </div>
        
    </div>
	
            <br />   
    <h2 style="font-size:18px"><img src="../images/plus.gif" style="margin-top:7px" id="advanced" onclick="showadv()" />&nbsp;Show Visibility Options</h2>
               
    <div id="advopt" style="display:none;">
    <table cellpadding="5" id="adv">
     <tr>
            <td>Contact Number Visiblity</td><td>:</td>
            <td>
                <asp:RadioButtonList ID="num_visi" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem>On</asp:ListItem>
                <asp:ListItem>Off</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
             <tr>
            <td>Address Visiblity</td><td>:</td>
            <td>
                <asp:RadioButtonList ID="addr_visi" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem>On</asp:ListItem>
                <asp:ListItem>Off</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
    </table>
    <br />
        <asp:Button ID="advanceopt" CausesValidation="false" runat="server"  
            Text="Update" CssClass="submitbutton" onclick="advanceopt_Click" />
    </div>
   
        </div>
</asp:Content>

