<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_Pages/Admin.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Admin_Users.aspx.cs" Inherits="Admin_Pages_Admin_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script  type="text/javascript">
    function selectAll_valid(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
        return true;
    }
    function selectAll_invalid(invoker) {
        var inputElements = document.getElementsByTagName('input');
        for (var i = 0; i < inputElements.length; i++) {
            var myElement = inputElements[i];
            if (myElement.type === "checkbox") {
                myElement.checked = invoker.checked;
            }
        }
        return true;
    }
    function openDialog() {
        $('#wrapper').css('display', 'block');
        $('#boxpopup').css('display', 'block');
        $('#overlay').fadeIn('fast');
        $('#boxpopup').animate({ 'left': '30%' }, 500);
    }
    function closeDialog(prospectElementID) {
        $(function ($) {
            $(document).ready(function () {
                $('#' + prospectElementID).css('position', 'absolute');
                $('#' + prospectElementID).animate({ 'left': '-100%' }, 500, function () {
                    $('#' + prospectElementID).css('position', 'fixed');
                    $('#' + prospectElementID).css('left', '100%');
                    $('#overlay').fadeOut('fast');
                    $('#wrapper').css('display', 'none');
                });
            });
        });
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <!--This popup box is used to display user search detail -->
  <div id="wrapper" style="display:none">
<div id="overlay" class="overlay"></div>
<div id="boxpopup" class="box">
	<a onclick="javascript:closeDialog('boxpopup')" class="boxclose"></a>
 	<div id="detbox1">
     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
           <ContentTemplate>
       <table>
       <tr>
       <td>Enter Name</td><td>:</td>
       <td><asp:TextBox ID="entername" ValidationGroup="one" runat="server"></asp:TextBox>
       <br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="one" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ControlToValidate="entername" runat="server" ErrorMessage="Please enter a name or alphabet"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ValidationGroup="one" ErrorMessage="Only Alphabets allowed" ValidationExpression="^[a-zA-Z]*" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ControlToValidate="entername" ></asp:RegularExpressionValidator>
       </td>
       
      <td><asp:Button ID="generate" runat="server" 
               Text="Show List" CssClass="submitbutton" ValidationGroup="one" onclick="generate_Click" />
       </td>
      </tr>
       </table><br />
          
            <asp:GridView runat="server" ID="gridview2" AutoGenerateColumns="False" AllowPaging="true"
                          onPageIndexChanging="gridview2_PageIndexChanging" CellPadding="5"
                         PageSize="10">
                         <RowStyle HorizontalAlign="Center"/>
                         <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <AlternatingRowStyle BackColor="maroon" Font-Bold="true" />
                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="old_fname" HeaderText="FirstName"/>
                            <asp:BoundField DataField="old_lname" HeaderText="LastName"/>
                            <asp:BoundField DataField="old_dob" DataFormatString="{0:yyyy-MM-dd}" HeaderText="DOB"/>
                        </Columns>
                    </asp:GridView>
               <asp:Label ID="divmsg" runat="server" ForeColor="GreenYellow" Text="No Details Found" Visible="false"></asp:Label>
           </ContentTemplate>
           </asp:UpdatePanel>
    </div>
</div>
</div>


<h2 style="float:right;font-size:23px"><img src="../images/logobig3.png" /> User Details</h2>
<img src="../images/active.png" width="100%" height="3px" />
<br /><br />
<ul class="menu">
<li><a href="Admin_Users.aspx?sh=view1" id="valid" title="1" class="active" runat="server" >Unite Users</a></li>
<li><a id="invalid" title="2" href="Admin_Users.aspx?sh=view2" runat="server">Invalid Users</a></li>
<li><a id="adminuser" title="3" href="Admin_Users.aspx?sh=view3" runat="server">Manage Admin</a></li>
			</ul><!-- Menu end -->
        <br /><br /><br />
        <center>
        <div id="did_you_know" style="height:inherit;width:inherit">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

        <asp:View ID="view1" runat="server">
        <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
         <asp:GridView runat="server" ID="gridviewchk" AutoGenerateColumns="False" AllowPaging="true"
                         DataKeyNames="regid" onPageIndexChanging="gridviewchk_PageIndexChanging" CellPadding="5"
                         PageSize="10">
                         <RowStyle HorizontalAlign="Center"/>
                         <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <AlternatingRowStyle BackColor="maroon" Font-Bold="true" />
                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="ALL"><HeaderTemplate><asp:CheckBox ID="CheckBoxAll" runat="server" Text="ALL" onClick=" return selectAll_valid(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" /></ItemTemplate></asp:TemplateField>
                            <asp:BoundField DataField="regid" HeaderText="UserID" SortExpression="regid" />
                            <asp:BoundField DataField="fname" HeaderText="FirstName"/>
                            <asp:BoundField DataField="lname" HeaderText="LastName"/>
                            <asp:BoundField DataField="dob" DataFormatString="{0:yyyy-MM-dd}" HeaderText="DOB"/>
                            <asp:BoundField DataField="branch" HeaderText="Branch" />
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Label ID="lblmsg" runat="server" Text="No Data Found" Visible="false"></asp:Label>
                    <br /><br />
                    <asp:Button ID="btndel" CssClass="submitbutton" runat="server" Text="Delete Selected" OnClick="btndel_Click" />
            <br /><br />
        </asp:View>

        <asp:View ID="view2" runat="server">
         <br />
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
            
          <table cellpadding="5">
          <tr>
          <td>
         <asp:GridView runat="server" ID="gridview1" AutoGenerateColumns="False" AllowPaging="true"
                         DataKeyNames="regid"
                onPageIndexChanging="gridview1_PageIndexChanging" CellPadding="5"
                         PageSize="10" AutoGenerateEditButton="True" 
                onrowcancelingedit="gridview1_RowCancelingEdit" 
                onrowediting="gridview1_RowEditing" onrowupdating="gridview1_RowUpdating" >
                <EditRowStyle ForeColor="White" />
                         <RowStyle HorizontalAlign="Center"/>
                         <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <AlternatingRowStyle BackColor="maroon" Font-Bold="true" />
                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                           
                        <Columns>
                        <asp:TemplateField HeaderText="ALL"><HeaderTemplate><asp:CheckBox ID="CheckBoxAll" runat="server" Text="ALL" onClick=" return selectAll_invalid(this)" /></HeaderTemplate><ItemTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" /></ItemTemplate></asp:TemplateField>
                            <asp:BoundField DataField="regid" ReadOnly="true" HeaderText="UserID" SortExpression="regid" />
                            <asp:BoundField DataField="fname" HeaderText="FirstName"/>
                            <asp:BoundField DataField="lname" HeaderText="LastName"/>
                            <asp:BoundField DataField="dob" DataFormatString="{0:yyyy-MM-dd}" HeaderText="DOB" ApplyFormatInEditMode="true"/>
                            <asp:BoundField DataField="username" HeaderText="UserName"/>
                            <asp:BoundField DataField="incorrect" HeaderText="Incorrect Value Given" ReadOnly="true" />
                            </Columns>
                    </asp:GridView>
                    </td>
                   <td align="right" valign="middle">
                   <a href="#" onclick="javascript:openDialog()" id="viewdet" runat="server">View Details</a>
                   </td>
                   </tr>
                 </table>
                 </ContentTemplate>
            </asp:UpdatePanel>
                    <br />
                    <asp:Label ID="lblmsg1" runat="server" Text="No Data Found" Visible="false"></asp:Label>
                    <br /><br />
                    <asp:Button ID="btnapprove" CssClass="submitbutton" runat="server" CausesValidation="false" Text="Approve Selected" OnClick="btnapprove_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btndel1" CssClass="submitbutton" CausesValidation="false" runat="server" Text="Delete Selected" OnClick="btndel1_Click" />
                    <br /><br />
                    
        </asp:View>

        <asp:View ID="view3" runat="server">
        <br />
        <ul class="menu">
<li><a href="Admin_Users.aspx?sh=view3&adid=addad" id="addad" title="1" class="active" runat="server" >Add</a></li>
<li><a id="delad" title="2" href="Admin_Users.aspx?sh=view3&adid=delad" runat="server">Delete</a></li>
        </ul>
        <br />
        <div id="div1" runat="server" style="display:block">
        <h2 style="font-size:23px"><u>Create New Admin User</u></h2>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
            
        <table id="addadmin" cellpadding="5">
        <tr>
        <td>Login For</td><td>:</td>
        <td>
            <asp:RadioButtonList ID="utype" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" 
             RepeatLayout="Flow" onselectedindexchanged="utype_SelectedIndexChanged">
            <asp:ListItem Selected="True">Forum</asp:ListItem>
            <asp:ListItem>Job</asp:ListItem>
            <asp:ListItem>Student</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        </tr>
          <tr><td></td><td></td><td></td></tr>
        <tr>
        <td>Login Type</td><td>:</td>
        <td>
             <asp:RadioButtonList ID="logtype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Selected="True" Value="1" Text="Admin"></asp:ListItem>
            <asp:ListItem Text="Guest" Value="3"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        </tr>
          <tr><td></td><td></td><td></td></tr>
        <tr>
        <td>Username</td>
        <td>:</td>
        <td><asp:TextBox ID="admin_name" runat="server"></asp:TextBox><font color="red" style="font-size:24px">*</font><br/>
            <asp:RequiredFieldValidator ValidationGroup="last" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a username" ForeColor="Red" SetFocusOnError="true" ControlToValidate="admin_name"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td>Password</td>
        <td>:</td>
        <td><asp:TextBox ID="admin_pass1" TextMode="Password" runat="server"></asp:TextBox><font color="red" style="font-size:24px">*</font><br />
            <asp:RequiredFieldValidator ValidationGroup="last" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a password" ForeColor="Red" SetFocusOnError="true" ControlToValidate="admin_pass1"></asp:RequiredFieldValidator>
        </td>
        </tr>
        
        <tr>
        <td>Email</td>
        <td>:</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:TextBox ID="admin_email" runat="server" AutoPostBack="true"
                ontextchanged="admin_email_TextChanged"></asp:TextBox><font color="red" style="font-size:24px">*</font>
                </ContentTemplate>
                <Triggers><asp:PostBackTrigger ControlID="admin_email" /></Triggers>
                </asp:UpdatePanel>
            <asp:Label ID="emailerr" runat="server" Text="" Visible="false" ></asp:Label>
            <asp:RequiredFieldValidator ValidationGroup="last" ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please enter an Mail-Id" ForeColor="Red" SetFocusOnError="true" ControlToValidate="admin_email"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ValidationGroup="last" ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="Email-Id format is wrong" ForeColor="Red" Display="Dynamic" 
                SetFocusOnError="true" ControlToValidate="admin_email" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>        
        </td>
        </tr>
        <tr><td></td><td></td><td></td></tr>
        <tr>
        <td>Company Name</td><td>:</td>
        <td>
        <asp:TextBox Enabled="false" ID="cmpname" runat="server"></asp:TextBox><font color="red" style="font-size:24px">*</font><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red" SetFocusOnError="true" ControlToValidate="cmpname" ErrorMessage="Please enter company name" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Format is state,country.Comma is must. Ex: TamilNadu,India" ForeColor="Red" ControlToValidate="cmpname" Display="Dynamic" SetFocusOnError="true" ValidationExpression="^[a-zA-Z]*,{1}"></asp:RegularExpressionValidator>
         </td>
        </tr>
         <tr>
        <td>Company Location</td><td>:</td>
        <td>
        <asp:TextBox ID="cmploc" Enabled="false" runat="server"></asp:TextBox><font color="red" style="font-size:24px">*</font><br />
           Ex: state,country
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red" SetFocusOnError="true" ControlToValidate="cmploc" ErrorMessage="Please enter company name"></asp:RequiredFieldValidator>
         </td>
        </tr>
        </table>
        </ContentTemplate>
            </asp:UpdatePanel>
        <br />
            <asp:Label ID="admin_msg" runat="server" Text="Admin Created Successfully" ForeColor="GreenYellow" Visible="false"></asp:Label>
        <br /><br />
            <asp:Button ID="add_adminuser" ValidationGroup="last" runat="server" 
                CssClass="submitbutton" Text="Add Admin" onclick="add_adminuser_Click" />
            </div>

            <div id="div2" runat="server" style="display:none">
               <asp:GridView  ID="AdminGridView1" runat="server" AllowPaging="True" 
        CellPadding="5" HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
        onpageindexchanging="AdminGridView1_PageIndexChanging" 
        AutoGenerateColumns="False" PageIndex="1" ShowFooter="True" 
        onrowdeleting="AdminGridView1_RowDeleting" 
        AutoGenerateDeleteButton="True" DataKeyNames="regid" >
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="5" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Right" />
    <Columns>
        <asp:BoundField DataField="group_id" HeaderText="GroupId" />
        <asp:BoundField DataField="regid" HeaderText="RegId" />
        <asp:BoundField DataField="username" HeaderText="Username" />
    <asp:BoundField DataField="email" HeaderText="Email" />
  </Columns>
       </asp:GridView>
              <asp:Label ID="admsg" runat="server" Text="No Users Found" ForeColor="GreenYellow" Visible="false"></asp:Label>
            </div>
            <br /><br />

        </asp:View>
</asp:MultiView>
</div>
</center>

</asp:Content>

