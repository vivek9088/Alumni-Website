<%@ Page Title="" Language="C#" MasterPageFile="~/Search Alumni/SearchPage.master" AutoEventWireup="true" CodeFile="SearchList.aspx.cs" Inherits="Search_Alumni_SearchList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" href="../css/jquery-ui-1.8.5.custom.css" type="text/css" media="all"/>
	<script type="text/javascript" src="../Scripts/jquery-1.4.2.min.js" ></script>
	<script type="text/javascript" src="../Scripts/jquery.cycle.all.js"></script>
	<script type="text/javascript" src="../Scripts/jquery-ui-1.8.5.custom.min.js"></script>
   <link rel="stylesheet" href="../css/reset.css" type="text/css" media="all"/>
	<link rel="stylesheet" href="../css/grid.css" type="text/css" media="all"/>
	<link rel="stylesheet" href="../css/style.css" type="text/css" media="all"/>
         
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>
<h2 style="font-size:22px;text-align:right;"><img src="../images/logobig3.png" /> Search Results</h2><hr />
     </center>
  
          <center>
           <table border="0" cellpadding="2" width="100%" cellspacing="0">
  <tr>
            <td width="20%" style="text-align:center" height="25">
               <strong>
              <b>Search Results for <%Response.Write(type); %>&nbsp;:</b>
               <b style="color:Black;"><%Response.Write(value); %> </b>
               </strong>
    </td>
  </tr>
</table>
<div class="table" style="overflow-y:scroll;height:300px;">
         <table id="resulttable" runat="server" cellpadding="5">
   
         </table>
     </div>      
          <table border="0" cellpadding="2" width="100%" cellspacing="0">
  <tr>
            <td width="20%" style="text-align:center" height="25">
               <strong><b><%Response.Write(cnt);%>&nbsp;Match(es)&nbsp;Found</b></strong>
             
    </td>
  </tr>
</table>
           </center>
          <br /><br />
        <a id="link" class="backlink" href="SearchAlumni.aspx">< &nbsp;&nbsp;Back</a>
    
</asp:Content>

