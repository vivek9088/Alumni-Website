<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="BirthdayList.aspx.cs" Inherits="AlumniPages_BirthdayList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style type="text/css">    
        </style>
        
        <script type="text/javascript" src="../Scripts/paging.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

   				
                <div class="grid9">
<h2 style="font-size:25px;text-align:right;"><img src="../images/logobig3.png" /> Alumni Birthday List</h2>
             <img src="../images/active.png" width="100%" height="3px" /><br />
<br />
<ul>
<li>
<h2 style="font-size:23px">Today</h2>
<table id="todaytab" runat="server" cellpadding="5">

</table>
<asp:Label Visible="false" ID="errmsg" runat="server" Text="" ForeColor="Red" Font-Size="16px"></asp:Label>
</li>
<li>
<br /><h2 style="font-size:23px">Upcoming</h2>
    <table id="upcometab" runat="server">
    <thead>
        <tr>
            <th>
            </th>
            <th>
            </th>
            <th>
            </th>
        </tr>
    </thead>
    <tbody>
    </tbody>
</table><br />
<div id="pageNavPosition"></div>
<asp:Label ID="month1" Visible="false" runat="server" Text="" ForeColor="Red" Font-Size="16px"></asp:Label>
</li>
</ul>
</div>

 <script type="text/javascript"><!--
     var pager = new Pager('<%=upcometab.ClientID%>', 10);
     pager.init();
     pager.showPageNav('pager', 'pageNavPosition');
     pager.showPage(1);
    //--></script>
</asp:Content>

