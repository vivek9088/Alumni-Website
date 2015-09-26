<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Admin_Pages/Admin.master" AutoEventWireup="true" CodeFile="Admin_Misc.aspx.cs" Inherits="Admin_Pages_Admin_Misc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2 style="float:right;font-size:23px"><img src="../images/logobig3.png" /> Other Services</h2>
<img src="../images/active.png" width="100%" height="3px" />
<br /><br />
<ul class="menu">
<li><a href="Admin_Misc.aspx?sh=view1" id="upld" title="1" class="active" runat="server" >Photo Upload</a>
</li>
<li><a id="export" title="2" href="Admin_Misc.aspx?sh=view2" runat="server" >Export List</a></li>
<li><a id="annonmsg" title="3" href="Admin_Misc.aspx?sh=view3" runat="server" >Announcement</a></li>
<li><a id="sendmsg" title="4" href="Admin_Misc.aspx?sh=view4" runat="server" >Send Message</a></li>
</ul>
            <!-- Menu end -->
        <br /><br />
        <br /><br />
<center>
<div id="did_you_know" style="width:inherit;height:inherit">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
    <asp:View ID="view1" runat="server">
    
    <br />
    <table id="seldet">
    <tr>
    <td>Select an Event</td>
    <td>:</td>
    <td><asp:DropDownList ID="evtlist" runat="server" 
            onselectedindexchanged="evtlist_SelectedIndexChanged"></asp:DropDownList> </td>
    </tr>
    <tr>
    <td>Select Year</td>
    <td>:</td>
    <td><asp:DropDownList ID="yrlist" runat="server" oninit="yrlist_Init"></asp:DropDownList> </td>
    </tr>
    <tr>
    <td> Number of files to Upload</td>
    <td>:</td>
    <td><asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged"> 
    <asp:ListItem Text="--Select Number Of Files--"></asp:ListItem>
            <asp:ListItem Text="1"></asp:ListItem>
        <asp:ListItem Text="5"></asp:ListItem>
        <asp:ListItem Text="10"></asp:ListItem>
        <asp:ListItem Text="15"></asp:ListItem>
        <asp:ListItem Text="20"></asp:ListItem>
        </asp:DropDownList>
    </td>
    </tr>
    </table>
     <br />
       <div id="upload" runat="server" style="display:none;">
           <asp:FileUpload ID="Upload1" runat="server" />
       </div>
        <table id="uploadtable" runat="server">
        </table>
        <br />
        <asp:Button ID="Photo_Upload" runat="server" Text="Upload" CssClass="submitbutton" OnClick="Photo_Upload_Click" />
       <br /> <br />
    <asp:Label ID="lblMsg" Font-Size="16px" ForeColor="GreenYellow" runat="server" Text=""></asp:Label>
    </asp:View>

    <asp:View ID="view2" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
<table id="list">
<tr>
<td>Field Names </td><td><b>:</b></td><td><asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Height="100" Width="120"></asp:ListBox>
</td>
<td>
<table id="table2">
<tr>
<td> <asp:Button ID="add" runat="server" Text=">" CausesValidation="false" onclick="add_Click" /></td>
</tr>
<tr><td><asp:Button ID="remove" runat="server" Text="<" onclick="remove_Click" CausesValidation="false" /> </td>
</tr>
</table>
</td>
<td></td><td>Selected Fields </td><td><b>:</b></td><td> <asp:ListBox SelectionMode="Multiple"  Height="100" Width="120" ID="ListBox2" runat="server"></asp:ListBox></td>
</tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
<br /><br />
 <asp:Button ID="showlist" CausesValidation="false" runat="server" Text="Show List" 
        onclick="showlist_Click" /> 
    <asp:Button ID="exportlist" CausesValidation="false" runat="server" 
        Text="Export" onclick="exportlist_Click" /> 
    <br /><br />
    
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" HeaderStyle-HorizontalAlign="Left"
         CellPadding="5" HorizontalAlign="center" ForeColor="#333333"
        GridLines="None" AllowSorting="true"    onpageindexchanging="GridView1_PageIndexChanging" 
        AutoGenerateColumns="False" PageIndex="1"
         ShowFooter="True" PageSize="20" onsorting="GridView1_Sorting">
        <PagerSettings PageButtonCount="6" Mode="NumericFirstLast" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Right" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="12" ForeColor="White"/>
<AlternatingRowStyle BackColor="White" />
<Columns>
</Columns>
    </asp:GridView>
       <asp:Label ID="exporterr" ForeColor="red" runat="server" Text="" Visible="false"></asp:Label>
	<br /><br />
    </asp:View>

    <asp:View ID="view3" runat="server">
    <br />
    <ul class="menu">
<li><a id="addann" title="3" href="Admin_Misc.aspx?sh=view3&divid=addann" class="active" runat="server">Add</a></li>
<li><a id="delann" title="4" href="Admin_Misc.aspx?sh=view3&divid=delann" runat="server">Delete</a></li>
</ul>
    <br /><br />
    <div id="div1" runat="server" style="display:block">
    <table id="anontab" cellpadding="5">
    <tr>
    <td>Message</td>
    <td>:</td>
    <td><textarea rows="4" cols="40" id="anntext" runat="server" maxlength="100">
</textarea><br/>
<font size="1">(Maximum characters: 100)</font><br/>
</td>
   </tr>
    <tr>
    <td>Date</td>
    <td>:</td>
    <td><asp:TextBox ID="anndate" runat="server" ReadOnly="True" ValidationGroup="first"></asp:TextBox>
        <asp:CalendarExtender ID="anndate_CalendarExtender" runat="server"
            Enabled="True" PopupPosition="Right" Format="yyyy-MM-dd" TargetControlID="anndate">
        </asp:CalendarExtender>
        <br />
        <asp:RequiredFieldValidator ValidationGroup="first" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter date" ForeColor="Red" SetFocusOnError="true" ControlToValidate="anndate"></asp:RequiredFieldValidator>
     </td>
    </tr>
    </table>
    <br />
        <asp:Button ID="add_annon" ValidationGroup="first" runat="server" CssClass="submitbutton" Text="Add" 
            onclick="add_annon_Click" />
    <br /><br />        
        <asp:Label ID="succmsg" runat="server" Text="" ForeColor="GreenYellow"></asp:Label>
        </div>
        
        <div id="div2" runat="server" style="display:none">
       Select Announcement Date : 
            <asp:TextBox ID="annon_date" runat="server" 
                ontextchanged="annon_date_TextChanged"></asp:TextBox>
            <asp:CalendarExtender ID="annon_date_CalendarExtender" runat="server" 
                Enabled="True" Format="yyyy-MM-dd" PopupPosition="Right" 
                TargetControlID="annon_date">
            </asp:CalendarExtender>
            <br /><br />
            <asp:Button CssClass="submitbutton" ID="showannlist" runat="server" Text="Show List" 
                onclick="showannlist_Click" />
            <br /><br />
           
             <asp:GridView ID="GridView2" AutoGenerateDeleteButton="true" runat="server" 
                    AllowPaging="True" HeaderStyle-HorizontalAlign="Left" DataKeyNames="msg_id"
         CellPadding="5" HorizontalAlign="center" ForeColor="#333333"
        GridLines="None" AllowSorting="true"    onpageindexchanging="GridView2_PageIndexChanging" 
        AutoGenerateColumns="False" PageIndex="1"
         ShowFooter="True" PageSize="10" onrowdeleting="GridView2_RowDeleting" >
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Right" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="12" ForeColor="White"/>
<AlternatingRowStyle BackColor="White" />
<Columns>
<asp:BoundField DataField="msg_id" HeaderText="Id" />
<asp:BoundField DataField="msg" HeaderText="Message" />
</Columns>
    </asp:GridView>
   
    <br />
            <asp:Label ID="griderr" runat="server" Text="No Data Found" Visible="false"></asp:Label>

        </div>
    <br />
    </asp:View>
    
    <asp:View ID="view4" runat="server">
    <h2 style="font-size:23px"><u>Send a message to the Alumni</u></h2>
    <br />
    <table>
    <tr>
    <td>To</td><td>:</td><td><asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:RadioButtonList Height="25" CellSpacing="10"  ID="optpost" runat="server" 
            AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow" 
            onselectedindexchanged="optpost_SelectedIndexChanged">
    <asp:ListItem Text="All" Selected="True"></asp:ListItem>
    <asp:ListItem Text="From & To year"></asp:ListItem>
    <asp:ListItem Text="Specific years"></asp:ListItem>
    </asp:RadioButtonList>
   <br />
   <div id="div3" style="display:none;" runat="server">
   <table>
   <tr>
   <td>From Year</td>
   <td>:</td>
   <td><asp:DropDownList ID="fromyr" runat="server">
        </asp:DropDownList>  </td>
   
    <td><strong>To Year</strong></td>
   <td>:</td>
   <td><asp:DropDownList ID="toyr" runat="server">
        </asp:DropDownList>  </td>
   </tr>
   </table>
   </div>
   <div id="div4" style="display:none;" runat="server">
       <asp:ListBox ID="specificyr" runat="server" SelectionMode="Multiple" Rows="5" Width="75">
       </asp:ListBox> <p style="text-align:center">(Multiple Selection Allowed)</p>
   </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </td>
    </tr>
    <tr>
    <td>Subject</td><td>:</td><td>
        <asp:TextBox ID="postsub" runat="server" Width="380px" Font-Size="18px"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a subject" ControlToValidate="postsub" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
    <td>Message</td><td>:</td><td>
        <textarea id="postmsg" cols="60" rows="8" runat="server"></textarea><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter a message" SetFocusOnError="true" ForeColor="Red" ControlToValidate="postmsg"></asp:RequiredFieldValidator>
    </td>
    </tr>
    </table>
        <asp:Button ID="Post_message" runat="server" Text="Send Message" 
            CssClass="submitbutton" onclick="Post_message_Click" />
    <br /><br />
        <asp:Label ID="postsentmsg" runat="server" Visible="false" Text="" ForeColor="GreenYellow"></asp:Label>
    <br /><br />
    </asp:View>

    </asp:MultiView>
    </div>
    </center>
</asp:Content>

