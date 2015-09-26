<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Admin_Pages/Admin.master" AutoEventWireup="true" CodeFile="Admin_Events.aspx.cs" Inherits="Admin_Pages_Admin_Events" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Events Div starts here-->

<h2 style="float:right;font-size:23px"><img src="../images/logobig3.png" /> Event Details</h2>
<img src="../images/active.png" width="100%" height="3px" />
    <!--Event menu-->
        <br /><br />
    <ul class="menu">
<li><a href="Admin_Events.aspx?sh=view1" id="addevents" title="1" runat="server" class="active" >Add</a></li>
			<li><a id="editevents" title="2" href="Admin_Events.aspx?sh=view2" runat="server" >Edit / Delete</a></li>
				</ul><!-- Menu end -->
    <br /><br />
    <center>
     <div id="did_you_know" style="height:inherit;width:90%">
        <br /><br />
<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
<asp:View ID="view1" runat="server">
<div id="div1" style="width:90%;">
    <br />
    
    <table cellpadding="3" style="text-align:left">
            <tr><td>Event Name</td><td>:</td><td>
                <asp:TextBox ID="eventname" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a name" ForeColor="Red" SetFocusOnError="true" ControlToValidate="eventname"></asp:RequiredFieldValidator>
                </td></tr>
                 <tr><td>Event Head</td><td>:</td><td>
                <asp:TextBox ID="eventhead" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter a title" ForeColor="Red" SetFocusOnError="true" ControlToValidate="eventhead"></asp:RequiredFieldValidator>
                </td></tr>
                <tr><td>Event Venue</td><td>:</td><td>
                <asp:TextBox ID="eventvenue" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a venue" ForeColor="Red" SetFocusOnError="true" ControlToValidate="eventvenue" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only Alphabets Allowed" ControlToValidate="eventvenue" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                </td></tr>
            <tr><td>Event Details</td><td>:</td><td>
                <FTB:FreeTextBox ID="eventdetail" runat="server" Height="200px"></FTB:FreeTextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter event detail" ForeColor="Red" SetFocusOnError="true" ControlToValidate="eventdetail"></asp:RequiredFieldValidator>
                </td></tr>
             <tr><td>Event Date</td><td>:</td><td>
                 <asp:UpdatePanel ID="DatePanel1" runat="server">
                 <ContentTemplate>
                     <asp:DropDownList ID="year" runat="server" oninit="year_Init">
                     </asp:DropDownList>
                     <asp:DropDownList ID="month" runat="server" oninit="month_Init" 
                         onselectedindexchanged="month_SelectedIndexChanged">
                     </asp:DropDownList>
                     <asp:DropDownList ID="day" runat="server" oninit="day_Init">
                     </asp:DropDownList>
                 </ContentTemplate>
                 <Triggers><asp:PostBackTrigger ControlID="month" /></Triggers>
                 </asp:UpdatePanel>
                   </td></tr>
                   <tr><td>Image Folder</td><td>:</td>
                   <td>
                       <asp:UpdatePanel ID="FolderPanel2" runat="server">
                       <ContentTemplate>
                       <asp:DropDownList ID="folderlist" runat="server">
                       </asp:DropDownList>
                   <asp:TextBox ID="folder" runat="server" Visible="false"></asp:TextBox> (or)<asp:CheckBox 
                           ID="folderopt" Text="Create New" runat="server" AutoPostBack="true"
                           oncheckedchanged="folderopt_CheckedChanged" /> 
                           <br />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter image folder name" ForeColor="Red" SetFocusOnError="true" ControlToValidate="folder"></asp:RequiredFieldValidator>
                           </ContentTemplate>
                           <Triggers><asp:PostBackTrigger ControlID="folderopt"/></Triggers>
                           </asp:UpdatePanel>
                        </td>
                        </tr>
            <tr><td>Attachment-1</td><td>:</td><td><asp:FileUpload ID="eventfile1" runat="server" /> </td></tr>
            <tr><td>Attachment-2</td><td>:</td><td><asp:FileUpload ID="eventfile2" runat="server" /> </td></tr>
            <tr><td>Attachment-3</td><td>:</td><td><asp:FileUpload ID="eventfile3" runat="server" /> </td></tr>
            <tr><td></td><td></td><td></td></tr> 
            <tr><td></td><td></td><td> <asp:Label ID="success2" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
            </table>
          <br />    
          <asp:Button ID="EventAdd" CssClass="submitbutton" runat="server" Text="Add Event" onclick="EventAdd_Click" />
          <br /><br />
        
</div>
</asp:View>
<asp:View ID="view2" runat="server">
<ul class="menu">
<li><a href="Admin_Events.aspx?sh=view2&eid=evtedit" id="evtedit" title="1" runat="server" class="active" >Edit</a></li>
			<li><a id="evtdel" title="2" href="Admin_Events.aspx?sh=view2&eid=evtdel" runat="server" >Delete</a></li>
				</ul><!-- Menu end -->
    <br /><br />
<div id="ediv1" runat="server" style="display:block">
   
    Select Event Id : <asp:DropDownList ID="eventlist" runat="server" AutoPostBack="true" 
            onselectedindexchanged="eventlist_SelectedIndexChanged">
    </asp:DropDownList>
 <br /><br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table cellpadding="3" style="text-align:left">
            <tr><td>Event Name</td><td>:</td><td>
                <asp:TextBox ID="evt_name" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter a name" ForeColor="Red" SetFocusOnError="true" ControlToValidate="evt_name"></asp:RequiredFieldValidator>
                </td></tr>
                 <tr><td>Event Head</td><td>:</td><td>
                <asp:TextBox ID="evt_head" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter a title" ForeColor="Red" SetFocusOnError="true" ControlToValidate="evt_head"></asp:RequiredFieldValidator>
                </td></tr>
                <tr><td>Event Venue</td><td>:</td><td>
                <asp:TextBox ID="evt_venue" runat="server"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please enter a venue" ForeColor="Red" SetFocusOnError="true" ControlToValidate="evt_venue"></asp:RequiredFieldValidator>
                </td></tr>
                  <tr><td>Event Date</td><td>:</td><td>
                 <asp:TextBox ID="evt_date" runat="server"></asp:TextBox>
                   <asp:CalendarExtender ID="evt_date_CalendarExtender" runat="server" 
                     Enabled="True" TargetControlID="evt_date" Format="yyyy-MM-dd" PopupPosition="Right"></asp:CalendarExtender>
                   </td></tr>
            <tr><td></td><td></td><td></td></tr> 
            <tr><td>Event Details</td><td>:</td><td>
                <FTB:FreeTextBox ID="evt_det" runat="server" Height="200px"></FTB:FreeTextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please enter event detail" ForeColor="Red" SetFocusOnError="true" ControlToValidate="evt_det"></asp:RequiredFieldValidator>
                </td></tr>
         <tr><td></td><td></td><td></td></tr>    
            <tr><td></td><td></td><td> <asp:Label ID="evt_err" runat="server" Text="" ForeColor="Red"></asp:Label></td></tr>
            </table>
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="eventlist" EventName="SelectedIndexChanged" />
            </Triggers>
           </asp:UpdatePanel>
          <br />    
    <asp:Button ID="Event_Edit" runat="server" Text="Edit Event" 
        CssClass="submitbutton" onclick="Event_Edit_Click" />
     
    <br /><br />
</div>
    
<div id="ediv2" style="display:none" runat="server">
   
   <asp:GridView  ID="EventGridView1" runat="server" AllowPaging="True" 
        CellPadding="5" HorizontalAlign="Center" ForeColor="#333333" GridLines="None" 
        onpageindexchanging="EventGridView1_PageIndexChanging" 
        AutoGenerateColumns="False" PageIndex="1" ShowFooter="True" 
        onrowdeleting="EventGridView1_RowDeleting" 
        AutoGenerateDeleteButton="True" DataKeyNames="evt_id"  >
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
        <PagerSettings PageButtonCount="5" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Right" />
    <Columns>
        <asp:BoundField DataField="evt_id" HeaderText="Event Id" />
        <asp:BoundField DataField="name" HeaderText="Name" />
    <asp:BoundField DataField="head" HeaderText="Head" />
        
  </Columns>
  
       </asp:GridView>
       <br /><br />
</div>
</asp:View>
</asp:MultiView>
  </div>
</center>
<!-- Events div ends here-->
</asp:Content>

