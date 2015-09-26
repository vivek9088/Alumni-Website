<%@ Page Title="" Language="C#" MasterPageFile="~/Search Alumni/SearchPage.master" AutoEventWireup="true" CodeFile="SearchAlumni.aspx.cs" Inherits="Search_Alumni_SearchAlumni" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" href="../css/inputtext.css" media="all" />
    <script type="text/javascript">

        function showdiv(div_number) {

            document.getElementById('div1').style.display = 'none';
            document.getElementById('div2').style.display = 'none';
            document.getElementById('div3').style.display = 'none';
            document.getElementById('div4').style.display = 'none';

            // just show the div we want
            document.getElementById('div' + div_number).style.display = 'block';

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
					<div id="div1" style="display:block;">
                  <center ><h2 style="font-size:23px;float:right"><img src="../images/logobig3.png" />  Search By Class</h2>
                  <img src="../images/active.png" height="3px" width="100%" />
                  </center> <br />
                
                    <table align="center" border="0"  cellpadding="5" width="600" cellspacing="1">
<tr><td align="center" colspan="2">
 
</td></tr><tr><td colspan="2"></td></tr>

<tr><td width="40%" align="right">
<b>Course&nbsp;&nbsp;&nbsp;:</b>
<br /><br />
<b>Branch&nbsp;&nbsp;&nbsp;:</b>
</td>
<td width="60%" align="left">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
                 <label class="label"> <asp:DropDownList ID="degree" runat="server" AutoPostBack="true" 
                         OnSelectedIndexChanged="degree_SelectedIndexChanged" Width="140px">
                     <asp:ListItem Value="-1">--Select a Degree--</asp:ListItem>
                <asp:ListItem>UG</asp:ListItem>
                <asp:ListItem>PG</asp:ListItem>
                     </asp:DropDownList>
                 </label>
                     <br /><br />
                   <label class="label"> <asp:DropDownList ID="branch" runat="server" oninit="branch_Init" Width="140px"></asp:DropDownList>
        </label>
                </ContentTemplate>
                 </asp:UpdatePanel>
</td></tr>
 
<tr><td></td><td></td></tr>
<tr><td width="40%" align="right">
<b>Batch&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</b>
</td>
<td width="60%" align="left">
  <label class="label">  <asp:DropDownList ID="batch" runat="server"  Width="140px" OnInit="batch_Init">
    </asp:DropDownList>
    </label>
</td></tr>
<tr><td></td><td></td></tr>
<tr><td width="40%" >&nbsp;</td>
<td width="60%" >
    <asp:Button ID="classsearch" runat="server" Text="Search" CausesValidation="False" CssClass="accbutton"  onclick="classsearch_Click" />
 
</td>
</tr>
</table>

<br />
                    </div>
                    <div id="div2" style="display:none;">
                     <center ><h2  style="font-size:23px;float:right"> <img src="../images/logobig3.png" />  Search By Name</h2>
                  <img src="../images/active.png" height="3px" width="100%" />
                     </center> 
                    <br />
<table align="center" border="0" cellpadding="2" width="600" cellspacing="1">
  <tr>
            <td  align="center" colspan="2">
            
    </td>
  </tr>
  <tr >
            <td width="40%" align="right">
			<b>First Name&nbsp;&nbsp;&nbsp;:</b></td>
			<td align="left" width="60%"><asp:TextBox ID="name" runat="server" Width="150"></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="No Numbers Allowed" Display="Dynamic" ControlToValidate="name" 
                    ValidationExpression="^[\sa-zA-Z]*"></asp:RegularExpressionValidator>
            </td>
            </tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td>
    		<asp:Button ID="namesearch" runat="server" Text="Search" onclick="namesearch_Click" CssClass="accbutton" CausesValidation="False" /></td>
  </tr>
	</table>
    <br />
                    </div>
                    <div id="div3" style="display:none" >
                     <center ><h2  style="font-size:23px;float:right"><img src="../images/logobig3.png" />  Search By Year</h2>
                  <img src="../images/active.png" height="3px" width="100%" />
                     </center> <br />
                    <table align="center" border="0" cellpadding="2" width="600" cellspacing="1">
  <tr>
            <td align="center" colspan="2">
            
    </td>
  </tr>
  <tr  >
            <td width="40%" align="right">
			<b>Year&nbsp;&nbsp;&nbsp;:</b></td>
			<td align="left" width="60%">
            <label class="label" ><asp:DropDownList ID="year" runat="server"  Width="80px" OnInit="year_Init">
    </asp:DropDownList>
            </label></td>
            </tr>
            <tr><td></td><td></td></tr>
            <tr><td></td>
            <td>
                <asp:Button ID="yearsearch" runat="server" Text="Search" onclick="yearsearch_Click" CssClass="accbutton" CausesValidation="False" />
    </td>
  </tr>
	</table>
    <br />
                    </div>
                    <div id="div4" style="display:none;">
                     <center ><h2  style="font-size:23px;float:right"> <img src="../images/logobig3.png" />  Search By Location</h2>
                  <img src="../images/active.png" height="3px" width="100%" />
                     
                     </center> <br />
                    <table align="center" border="0" cellpadding="5" width="600" cellspacing="1">
  <tr>
            <td align="center" colspan="2">
           
    </td>
  </tr>
  <tr >
                       <td width="40%" align="right">
			<b>Location&nbsp;&nbsp;&nbsp;:</b></td>
			<td align="left" width="60%"><asp:TextBox ID="location" runat="server" Width="133"></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator2" runat="server" 
                    ErrorMessage="No Numbers Allowed" Display="Dynamic" ControlToValidate="name" 
                    ValidationExpression="^[\sa-zA-Z]*"></asp:RegularExpressionValidator>
			
    </td>
    </tr>
    <tr  >
    <td align="right">
                <b>Location Type&nbsp;:
              </b>
               </td>
               <td><label class="label"><asp:DropDownList ID="loclist" runat="server" Width="133">
                  <asp:ListItem Selected="True">City</asp:ListItem>
                <asp:ListItem>State</asp:ListItem>
                <asp:ListItem>Country</asp:ListItem>
                </asp:DropDownList>
               </label>
                </td></tr>
                <tr ><td></td>
                 <td>
                <asp:Button ID="locsearch" runat="server" Text="Search"  onclick="locsearch_Click" CssClass="accbutton" CausesValidation="False" /></td></tr>
    
      </table>
      <br />
                    </div>
                       	
</asp:Content>

