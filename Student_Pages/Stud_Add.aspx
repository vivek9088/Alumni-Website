<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Pages/Stud_Admin.master" AutoEventWireup="true" CodeFile="Stud_Add.aspx.cs" Inherits="Student_Pages_Stud_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
  
  </style>
 <link href="../css/Admin-menu.css" media="all" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
   <h2 style="font-size:23px;float:right;"><img src="../images/logobig3.png" />
                           Add Student
                           </h2><img src="../images/active.png" width="100%" height="3px" />
               
                      <br /><br />
<center>
<div id="did_you_know" style="height:390px">
<h2 style="font-size:25px">Student Details</h2>
<img src="../images/HR css/bg.png" height="3" width="176px" style="margin-top:-10px"  />
    
<br />
<table cellpadding="7">
<tr><td>Student UserName</td><td>:</td><td><asp:TextBox ID="name" runat="server" Font-Size="Medium"></asp:TextBox>
<br />  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name cannot be blank" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ControlToValidate="name"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only Alphabets allowed" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ControlToValidate="name" ValidationExpression="^[\sa-zA-Z]*"></asp:RegularExpressionValidator>
</td></tr>
<tr><td></td><td></td><td style="color:gray">(Example : Ajay,Kumar,etc..)</td></tr>
<tr>
<td>Student Password</td><td>:</td>
<td><asp:TextBox ID="pass" TextMode="Password" Font-Size="Medium" runat="server"></asp:TextBox> 

</td>
</tr>
<tr><td>Student DOJ</td><td>:</td><td>
    
    <asp:DropDownList ID="syr" runat="server" oninit="syr_Init">
    </asp:DropDownList>
    <asp:DropDownList ID="smon" runat="server" oninit="smon_Init">
    </asp:DropDownList>
     <asp:DropDownList ID="sday" runat="server" oninit="sday_Init" 
        AutoPostBack="True" onselectedindexchanged="sday_SelectedIndexChanged">
    </asp:DropDownList>
    
                  <br />  <asp:Label ID="doberr" ForeColor="Red" runat="server" Visible="False"></asp:Label>
  </td></tr>
</table>
<br />
    <asp:Label ID="msg" runat="server" Text="" ForeColor="Red"></asp:Label>
<br /><br />
    
    <div class="login">
    <asp:LinkButton ID="Add_Student" OnClick="Add_Stud_Click" ForeColor="black" Font-Underline="false" runat="server">Add Student</asp:LinkButton>
    </div>
  
</div>
</center>
 
</asp:Content>

