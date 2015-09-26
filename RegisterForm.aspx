<%@ Page Title="" Language="C#" MasterPageFile="~/Register.master" AutoEventWireup="true" CodeFile="RegisterForm.aspx.cs" Inherits="RegisterForm" ViewStateMode="Enabled" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <link media="all" type="text/css" rel="Stylesheet" href="css/RegisterForm.css" />  
  
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

<style type="text/css">
  #divwidth
        {
          width: 150px;  
        }
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Alert Box -->
<div id="overlayhf" class="overlay1" style="display:none"></div>
       <div id="AlertBox1" class="alert">
<p id="msg1"></p>  
     <a class="close" onclick="javascript:hideme()"></a>
</div>
<!--Alert Box End-->
           <h2 style="font-size:23px;float:right"> <img src="images/logobig3.png"/>  Register Form</h2>
     <img src="images/active.png" height="3px" width="100%" />      <br />
            <br />  	    
			            <!--table 1 starts here-->
                      <table id="table1" class="bolddot firstwd secondwd" style="display:block;" cellpadding="5" runat="server">
<thead><tr><th><legend>Step 1</legend></th></tr></thead>
                 <tbody>
            <tr>
                <td >First Name</td><td>:</td>
                <td >
                    <asp:TextBox ID="firstname" runat="server" ></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                          <br />  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="first" 
                        runat="server" Display="Dynamic" ForeColor="red"
                                ControlToValidate="firstname" ErrorMessage="First Name Please" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                        runat="server" ControlToValidate="firstname" ForeColor="red" ValidationGroup="first"
                                ErrorMessage="Only Alphabets Allowed" ValidationExpression="^[\sa-zA-Z]*" 
                                Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                </td>
                <td>Enter your first name not father's name</td>
            </tr>
            <tr>
                <td >Last Name / Initial</td><td>:</td>
                <td >
                    <asp:TextBox ID="lastname" runat="server"></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" ValidationGroup="first"
                        runat="server" ErrorMessage="Last Name Please" ControlToValidate="lastname" 
                        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="red"
                        ErrorMessage="No Numbers Allowed" ControlToValidate="lastname" Display="Dynamic" ValidationGroup="first" 
                        SetFocusOnError="True" ValidationExpression="^[\sa-zA-Z]*"></asp:RegularExpressionValidator>
                </td>
                <td>Enter your last name or your initial</td>
            </tr>
             <tr>
                <td >Gender</td><td>:</td>
                <td >
                <label class="label">
                <asp:DropDownList ID="gender" runat="server" AppendDataBoundItems="true" Width="180px">
                <asp:ListItem >--Select a Gender--</asp:ListItem>
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList></label><font color="Red" style="font-size:22px">*</font>
                 <br />   <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ValidationGroup="first" runat="server" ErrorMessage="Please select a Gender" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="gender" InitialValue="--Select a Gender--"></asp:RequiredFieldValidator>
                </td>
                <td>Please select a Gender</td>
            </tr>
             <tr>
                <td >DOB (MM/DD/YYYY)</td><td>:</td>
                <td >
                     <label class="label">
                    <asp:DropDownList ID="year" runat="server" OnInit="year_Init" Width="65px">
                    </asp:DropDownList>
                    </label>
 
                     <label class="label">
                    <asp:DropDownList ID="month" runat="server" OnInit="month_Init" 
                             Width="70px">
                    </asp:DropDownList>
                    </label>
                    
                     <label class="label">
                    <asp:DropDownList ID="day" runat="server" OnInit="day_Init" Width="55px">
                    </asp:DropDownList>
                    </label>
                
                  <font color="Red" style="font-size:22px">*</font>
                   &nbsp;
                  <br />  <asp:Label ID="doberr" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                    </td>
                <td>Please select a date of birth</td>
            </tr>
             <tr>
                <td>Batch (Passout year)</td><td>:</td>
                <td> <label class="label"> <asp:DropDownList ID="batch" runat="server" oninit="batch_Init" Width="180px"></asp:DropDownList></label>&nbsp;<font color="Red" style="font-size:22px">*</font>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="Please select a batch" ControlToValidate="batch" 
                        Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidationGroup="first"
                        InitialValue="-- Select a Batch --" ></asp:RequiredFieldValidator>
                    </td>
                <td>Please select the correct batch</td>
            </tr>
            <tr>
            <td >Degree<br/>
                 <br/>Branch</td><td>:<br /><br />:</td>
             <td>
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
                  <label class="label">
                  <asp:DropDownList ID="degree" runat="server" AutoPostBack="true" 
                         onselectedindexchanged="degree_SelectedIndexChanged" Width="180px">
                     <asp:ListItem>--Select a Degree--</asp:ListItem>
                <asp:ListItem>UG</asp:ListItem>
                <asp:ListItem>PG</asp:ListItem>
                     </asp:DropDownList></label>&nbsp;<font color="Red" style="font-size:22px">*</font>
                     <br />
                     <asp:RequiredFieldValidator ID="degreevalidate" ValidationGroup="first" runat="server" ErrorMessage="Please Select a Degree" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ControlToValidate="degree" InitialValue="--Select a Degree--"></asp:RequiredFieldValidator>
                     <br />
                      <label class="label" id="uglab" runat="server" visible="false">
                     <asp:DropDownList ID="branchug" runat="server" Width="180px" Visible="false" 
                         oninit="branchug_Init"></asp:DropDownList></label>
                     <label class="label" id="pglab" runat="server" visible="true">
                     <asp:DropDownList ID="branchpg" runat="server" Width="180px" Visible="true" 
                         oninit="branchpg_Init"></asp:DropDownList></label>
                    <font color="Red" style="font-size:22px">*</font>
 <br />
                    <asp:Label ID="brancherr" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                 </ContentTemplate>
                 </asp:UpdatePanel>
                   </td>
                <td>Please select correct Degree<br />
                    <br />Please select correct Branch</td>

                      </tr>
       
            </tbody>      
        </table>
        <!-- table 1 ends here-->
   
				<!-- table 2 starts here-->
                <table id="table2" class="bolddot firstwd secondwd" cellpadding="5" style="display:none;" runat="server">
                <thead><tr><th><legend>Step 2</legend></th></tr></thead>
                <tbody>
                <tr><td ></td><td></td><td></td></tr>
                 <tr>
                <td >Contact Number</td><td>:</td>
                <td>
                    <asp:TextBox ID="number" runat="server" MaxLength="10"></asp:TextBox>
                   <font color="Red" style="font-size:22px">*</font>
                 <br />  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="second" runat="server" ErrorMessage="Please enter contact number" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ControlToValidate="number"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ForeColor="red"
                        ControlToValidate="number" Display="Dynamic" ErrorMessage="Only Numbers Allowed" ValidationGroup="second" 
                        ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                </td>
                <td>Please enter 10-digit mobile number</td>
            </tr>
            <tr>
            <td>Contact Number Visiblity</td><td>:</td>
            <td>
                <asp:RadioButtonList ID="num_visi" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Selected="True">On</asp:ListItem>
                <asp:ListItem>Off</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
                <tr>
                <td  >Email Id</td><td>:</td>
                <td >
                    <asp:UpdatePanel ID="emailpanel" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="emailid" runat="server" placeholder="example@domain.com" 
                            AutoPostBack="true" ontextchanged="emailid_TextChanged"></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                    </ContentTemplate>
                    <Triggers>
                    <asp:PostBackTrigger ControlID="emailid" />
                    </Triggers>
                  </asp:UpdatePanel>
                    <asp:Label ID="emailerr" runat="server" Text="" Visible="false" ></asp:Label>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="red"
                        ControlToValidate="emailid" Display="Dynamic" ErrorMessage="Enter Email-id please" ValidationGroup="second"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ForeColor="red"
                        ErrorMessage="Email-Id is wrong" ControlToValidate="emailid" Display="Dynamic" ValidationGroup="second" 
                        SetFocusOnError="True" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
                <td>Example:abc@gmail.com</td>
            </tr>
            <tr>
                <td  >Username</td><td>:</td>
                <td >
                <asp:UpdatePanel ID="userpanel" runat="server">
                <ContentTemplate>
                                    <asp:TextBox ID="username" runat="server" OnTextChanged="username_TextChanged" AutoPostBack="true"
                                        ToolTip="Cannot Contain @ symbol"></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                                        <br />  <asp:Label ID="status" runat="server" Text="" Visible="false" ></asp:Label>
                                        </ContentTemplate>     
                                        <Triggers>
                                        <asp:PostBackTrigger ControlID="username"/>
                                        </Triggers>
                                        </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="username"
                                        ErrorMessage="Please enter a Username" Display="Dynamic" ForeColor="red" ValidationGroup="second"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" 
                                        ControlToValidate="username" Display="Dynamic" ForeColor="red"
                                        ErrorMessage="No special characters allowed" ValidationGroup="second" 
                                        ValidationExpression="^[a-zA-Z0-9]*" 
                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                       
                </td>
                <td>Username cannot contain any special characters</td>
            </tr>
            <tr>
                <td  >Password</td><td>:</td>
                <td >
                    <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                    <asp:PasswordStrength ID="password_PasswordStrength" runat="server"
                        Enabled="True" TargetControlID="password">
                    </asp:PasswordStrength>
                  <br />  <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="red"
                        ErrorMessage="Please enter password" ControlToValidate="password" ValidationGroup="second" 
                        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
                <td>No special charatcers allowed</td>
            </tr>
            <tr>
                <td  >Confirm Password</td><td>:</td>
                <td >
                    <asp:TextBox ID="confirmpass" runat="server" TextMode="Password"></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="red" ValidationGroup="second"
                        ControlToCompare="password" ControlToValidate="confirmpass" Display="Dynamic" 
                        ErrorMessage="Password do not match" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="confirmpass" runat="server" ErrorMessage="Please re-enter password" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="second"></asp:RequiredFieldValidator>
                </td>
                <td>Re-type the password as it is</td>
            </tr>
             <tr>
                <td >Current Organization</td><td>:</td>
                <td>
                    <asp:TextBox ID="org" runat="server"></asp:TextBox>    
                    <font color="Red" style="font-size:22px">*</font>
                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="second" runat="server" ErrorMessage="Please enter an organization" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ControlToValidate="org"></asp:RequiredFieldValidator>
                </td>
                <td> Please enter the current company name</td>
            </tr>
            <tr>
                <td  >Current Designation</td><td>:</td>
                <td >
                    <asp:TextBox ID="desig" runat="server"></asp:TextBox><font color="Red" style="font-size:22px">*</font>
                    <br />
                    <asp:RequiredFieldValidator ValidationGroup="second" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter a designation" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ControlToValidate="desig"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ForeColor="red"
                        ErrorMessage="Only Alphabets Allowed" ControlToValidate="desig" Display="Dynamic" ValidationGroup="second"
                        SetFocusOnError="True" ValidationExpression="^[\s|a-z|A-Z]*"></asp:RegularExpressionValidator>
                   </td>
                <td>Please enter the current designation</td>
            </tr>
            <tr>
            <td >Company Location</td><td>:</td>
            <td> <label class="label"><asp:DropDownList ID="cmp_loc" runat="server" Width="180px">
                </asp:DropDownList></label><font color="Red" style="font-size:22px">*</font>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="second" runat="server" ErrorMessage="Please select Company Location" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ControlToValidate="cmp_loc" InitialValue="--Select a location--"></asp:RequiredFieldValidator>
            </td>
            <td>Please select a Location</td>
            </tr>
            <tr>
                <td  >Date of Joining (MM/YYYY)</td><td>:</td>
                <td > <label class="label">
                   <asp:DropDownList ID="joinmon" runat="server" OnInit="joinmon_Init" Width="75px">
                    </asp:DropDownList></label>
                  <label class="label">   <asp:DropDownList ID="joinyr" runat="server" 
                        OnInit="joinyr_Init" Width="65px">
                    </asp:DropDownList></label><font color="Red" style="font-size:22px">*</font>
                 <br />  <asp:Label ID="joinerr" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                   
                        </td>
                <td>Please select Joining year</td>
            </tr>
                      <tr>
                <td >Previous Organization</td><td>:</td>
                  <td >        
                    <asp:TextBox ID="prevorg" runat="server"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ForeColor="red"
                        ErrorMessage="Only Alphabets Allowed" ControlToValidate="prevorg" Display="Dynamic" ValidationGroup="second" 
                        SetFocusOnError="True" ValidationExpression="^[\s|a-z|A-Z]*"></asp:RegularExpressionValidator>
                       </td>
                <td> Please enter the previous company name</td>
            </tr>
            <tr>
                <td  >Previous Designation</td><td>:</td>
                <td >
                    <asp:TextBox ID="prevdesig" runat="server"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ForeColor="red"
                        ErrorMessage="Only Alphabets Allowed" ControlToValidate="prevdesig" Display="Dynamic" ValidationGroup="second" 
                        SetFocusOnError="True" ValidationExpression="^[\s|a-z|A-Z]*"></asp:RegularExpressionValidator>
                   </td>
                <td>Please enter the previous designation</td>
            </tr>
            <tr>
            <td >Company Location</td><td>:</td>
            <td> <label class="label"><asp:DropDownList ID="prev_loc" runat="server" Width="180px">
                </asp:DropDownList></label>
            </td>
            <td>Please select a Location</td>
            </tr>
            <tr>
                <td  >Period From (MM/YYYY)</td><td>:</td>
                <td > <label class="label">
                    <asp:DropDownList ID="startmon" runat="server" OnInit="startmon_Init" 
                        Width="75px">
                    </asp:DropDownList></label>
                     <label class="label">
                    <asp:DropDownList ID="startyr" runat="server" OnInit="startyr_Init" 
                        Width="65px">
                    </asp:DropDownList></label>
                   </td>
                <td>Please select the Join year</td>
            </tr>
            <tr>
                <td  >Period To (MM/YYYY)</td><td>:</td>
                <td > <label class="label">
                    <asp:DropDownList ID="leavemon" runat="server" OnInit="leavemon_Init" 
                        Width="75px">
                    </asp:DropDownList></label>
                     <label class="label">
                    <asp:DropDownList ID="leaveyr" runat="server" OnInit="leaveyr_Init" 
                        Width="65px">
                    </asp:DropDownList></label>
                                       </td>
                <td>Please select the Resign year</td>
            </tr>
             </tbody>
         </table>
             <!--table 2 ends here-->
        <br />
           <!-- table 3 starts here-->
    	     <table id="table3" class="bolddot firstwd secondwd" cellpadding="5" style="display:none;" runat="server"> 
             <thead><tr><th><legend>Step 3</legend></th></tr></thead>
             <tbody>
             <tr><td></td><td></td><td></td></tr>
               
             <tr>
               <td >Resident Country<br/>
                 <br/>State</td>
               <td>:<br /><br />:</td>
               <td>
                  <label class="label">
                 <asp:DropDownList ID="country" runat="server" oninit="country_Init" 
                       Width="180px"></asp:DropDownList>
                       </label>&nbsp;<font color="Red" style="font-size:22px">*</font>
                     <br />
                    <asp:RequiredFieldValidator ValidationGroup="third" ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please select a country" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ControlToValidate="country" InitialValue="--Select a location--"></asp:RequiredFieldValidator>
                     <br />
                     <asp:TextBox ID="state" runat="server"></asp:TextBox>&nbsp;<font color="Red" style="font-size:22px">*</font>
                    <br />
                    <asp:RequiredFieldValidator ValidationGroup="third" ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please enter a State" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="state" InitialValue="--Please select a state--"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="third" ID="RegularExpressionValidator10" runat="server" ForeColor="red"
                        ErrorMessage="No Numbers Allowed" ControlToValidate="state" Display="Dynamic" 
                        SetFocusOnError="True" ValidationExpression="^[\s|a-z|A-Z]*"></asp:RegularExpressionValidator>
                 
                   </td>
                <td>Please select a Country<br />
                    <br />Please enter a State
                    </td>
               
            </tr>
           
            <tr>
                <td >City</td><td>:</td>
                <td>
                    <asp:TextBox ID="city" runat="server"></asp:TextBox>&nbsp;<font color="Red" style="font-size:22px">*</font>
                    <br />
                    <asp:RequiredFieldValidator ValidationGroup="third" ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please enter a city" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="city"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="third" ID="RegularExpressionValidator11" runat="server" ForeColor="red"
                        ErrorMessage="No Numbers Allowed" ControlToValidate="city" Display="Dynamic" 
                        SetFocusOnError="True" ValidationExpression="^[\s|a-z|A-Z]*"></asp:RegularExpressionValidator>
                  
                </td>
                <td>Please enter correct city for the state mentioned</td>
            </tr>
             <tr>
                <td >Zip Code</td><td>:</td>
                <td>
                    <asp:TextBox ID="zipcode" runat="server"></asp:TextBox>&nbsp;
                    <br />
                    <asp:RegularExpressionValidator ValidationGroup="third" ID="RegularExpressionValidator12" runat="server" ForeColor="red"
                        ErrorMessage="Only Numbers Allowed" ControlToValidate="zipcode" Display="Dynamic" 
                        SetFocusOnError="True" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>
                  
                </td>
                <td>Please enter correct Zip Code format</td>
            </tr>
           
            <tr>
            <td >
            Address</td><td>:</td>
            <td> <asp:TextBox ID="address" runat="server"></asp:TextBox>
                 </td>
            <td>Example No.12/2, Ravi Flats,... etc 
            </td>
            </tr>
            <tr>
            <td>Address Visiblity</td><td>:</td>
            <td>
            <asp:RadioButtonList ID="addr_visi" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Selected="True">On</asp:ListItem>
                <asp:ListItem>Off</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            </tr>
             <tr>
                <td   >Upload Photo</td><td>:</td>
                <td >
                    <asp:FileUpload  ID="photoupload" runat="server" /><asp:Image ID="showimg" Height="50" Width="50" runat="server" /><br />
                    <asp:Button ID="upload" runat="server" Text="Upload Photo" CausesValidation="false"
                        onclick="upload_Click" /><br /><br />
                    <asp:Label ID="photo" runat="server" Text=""></asp:Label>
                </td>
                <td>Please upload only Png or Jpeg photos only</td>
            </tr>
            <tr>
            <td >Answer the Security Question?</td><td>:</td>
            <td>
                <asp:Label ID="num1" runat="server" Text=""></asp:Label> + 
                <asp:Label ID="num2" runat="server" Text=""></asp:Label> =
                <asp:TextBox ID="numres" Width="50" runat="server" MaxLength="2"></asp:TextBox>&nbsp;<font color="Red" style="font-size:22px">*</font>
                <br />
                <asp:Label ID="numerr" runat="server"  Visible="false" Text="" ></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="third" ErrorMessage="Please enter an answer" ForeColor="Red" ControlToValidate="numres" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="third" ErrorMessage="Only digits allowed" ForeColor="Red" Display="Dynamic" ControlToValidate="numres" SetFocusOnError="true" ValidationExpression="^[0-9]*"></asp:RegularExpressionValidator>

             </td>
            <td>
            </td>
            </tr>
            
            <tr><td></td><td></td><td></td></tr>
          <tr><td></td><td></td><td>  <asp:Button ID="Submit" ValidationGroup="third" runat="server" Text="Register" onclick="Submit_Click" CausesValidation="True" 
         CssClass="buttonreg" /> </td>     
  <td></td></tr>  
  
  </tbody>
  </table>		
          <!-- table 3 ends here-->

        <div id="process">
            <asp:LinkButton ID="BackLink" Font-Bold="true" Font-Size="Medium"  PostBackUrl="#" runat="server" Visible="false" CausesValidation="true" OnClick="BackLink_Click"></asp:LinkButton>
             &nbsp;&nbsp;    <asp:LinkButton Font-Bold="true" Font-Size="Medium" PostBackUrl="#" CausesValidation="true" ValidationGroup="first"  ID="ForwardLink" OnClick="ForwardLink_Click" runat="server">Proceed to Step2</asp:LinkButton>
        </div>  
    <br /><h3 runat="server" id="h3" style="color:Red"><b>*</b> - Required Field</h3>
  	    <br />

</asp:Content>

