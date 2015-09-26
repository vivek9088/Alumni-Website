<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/Alumni.master" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="AlumniPages_Email" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript" src="../Scripts/FileUpload.js"></script>
    
   <style type="text/css">
   textarea{resize:none;}
   button{
		width:150px;
		height:37px;
		line-height:37px;		
		border:none;
		background:url(../images/form1/form_button.gif) no-repeat 0 0;
		color:#fff;
		cursor:pointer;
		text-align:center;
		}				
	
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="grid9">
<h2 style="font-size:25px;text-align:right;"><img src="../images/logobig3.png" /> Compose Email</h2>
             <img src="../images/active.png" width="100%" height="3px" /><br /><br />
 <table width="100%">
  <tr>
  <td >To</td>  <td>:</td>
        <td>    
            <asp:TextBox ID="msgto" runat="server" Width="100%" Height="30px" TextMode="MultiLine"
                ToolTip="Use ';' to seperate Mail Id's"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="mail" ErrorMessage="Please enter a mail-id" ControlToValidate="msgto" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:AutoCompleteExtender ID="msgto_AutoCompleteExtender" runat="server" CompletionInterval="2" MinimumPrefixLength="1" 
                DelimiterCharacters=";" Enabled="True" ServiceMethod="GetCompletionList" 
                ServicePath="" TargetControlID="msgto" UseContextKey="True" ShowOnlyCurrentWordInCompletionListItem="true">
            </asp:AutoCompleteExtender>
      </td></tr>                
      <tr>
      <td >Cc</td>  <td>:</td>
        <td>    <asp:TextBox ID="msgcc" runat="server" Width="100%" TextMode="MultiLine" 
                Height="30px" ToolTip="Use ';' to seperate Mail Id's"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="mail" ErrorMessage="Please enter a mail-id" ControlToValidate="msgcc" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:AutoCompleteExtender ID="msgcc_AutoCompleteExtender" runat="server" CompletionInterval="2" MinimumPrefixLength="1" 
                DelimiterCharacters=";" Enabled="True" ServiceMethod="GetCompletionList2"  ShowOnlyCurrentWordInCompletionListItem="true"
                ServicePath="" TargetControlID="msgcc" UseContextKey="True">
            </asp:AutoCompleteExtender>
         </td> </tr>  
         <tr>
         <td >Bcc</td>  <td>:</td>
          <td>  <asp:TextBox ID="msgbcc" runat="server" Width="100%" TextMode="MultiLine" 
                  Height="30px" ToolTip="Use ';' to seperate Mail Id's"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="mail" ErrorMessage="Please enter a mail-id" ControlToValidate="msgbcc" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
              <asp:AutoCompleteExtender ID="msgbcc_AutoCompleteExtender" runat="server" CompletionInterval="2" MinimumPrefixLength="1" 
                  DelimiterCharacters=";" Enabled="True" ServiceMethod="GetCompletionList3"  ShowOnlyCurrentWordInCompletionListItem="true"
                  ServicePath="" TargetControlID="msgbcc" UseContextKey="True">
              </asp:AutoCompleteExtender>
           </td> </tr>
           <tr>
           <td >Subject</td>  <td>:</td>
            <td><asp:TextBox ID="msgsub" runat="server" Width="324px" Height="22px"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="mail" runat="server" ErrorMessage="Please enter a Subject" ControlToValidate="msgsub" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
            </td></tr>
            <tr>
            <td >Attachment
                </td>  <td>:</td>
            <td><p id="upload-area"> 
   <input id="File1" type="file" runat="server" size="40" /> 
</p> </td></tr>
<tr><td></td><td></td><td><input id="AddFile" type="button" value="Add file" onclick="addFileUploadBox()"  /></td>
            </tr>
            <tr>
            <td>Message</td>
            <td>:</td>
            <td> <asp:TextBox ID="msgbody" runat="server" Height="165px" TextMode="MultiLine" 
                Width="525px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="mail" runat="server" ErrorMessage="Please enter a message" ControlToValidate="msgbody" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                      </td>
            </tr>
     <tr>
         <td>
         </td>
         <td>
         </td><td></td>
         </tr>
          <tr><td></td><td></td>
          <td>
            <button id="Button1" validationgroup="mail" type="button" onserverclick="SendMail_Click" runat="server">Send Mail</button></td></tr>
 </table>
    <br />
    <!--Alert Box -->
       <div id="AlertBox" class="alert">
<p id="errmsg1"></p>  
 <input id="reurl" runat="server" type="hidden" value="1" />   
           <input id="Button2" class="button" type="button" value="OK" onclick="javascript:HideMe()" />
</div>
<!--Alert Box End-->
    				</div>	

</asp:Content>

