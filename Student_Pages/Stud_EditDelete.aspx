<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Pages/Stud_Admin.master" AutoEventWireup="true" CodeFile="Stud_EditDelete.aspx.cs" Inherits="Student_Pages_Stud_EditDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2 style="font-size:23px;float:right;"><img src="../images/logobig3.png" />
                           Edit/Delete Student
                           </h2><img src="../images/active.png" width="100%" height="3px" />
               
                      <br /><br />
<center>
      <asp:Button ID="btnselectall" runat="server" OnClick="btnselectall_Click"  Text="SelectAll" />

<asp:Button ID="btnclearall" runat="server" OnClick="btnclearall_Click"  Text="ClearAll" />

<asp:Button ID="btndelete" runat="server" OnClick="btndelete_Click"  Text="DeleteAll" />
<br /><br />
   <asp:GridView ID="GridView1" runat="server" CellPadding="6" ForeColor="#333333" GridLines="None" AutoGenerateEditButton="true"
 AutoGenerateColumns="False" DataKeyNames="rowid"
OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<RowStyle BackColor="#EFF3FB" />
<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />

<Columns>
<asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chk1" runat="server" />
</ItemTemplate>
<HeaderStyle HorizontalAlign="Left" />
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:BoundField DataField="rowid" HeaderText="Id" ReadOnly="True"/>
<asp:BoundField DataField="username" HeaderText="Student name" />
<asp:BoundField DataField="emailid" HeaderText="Email-ID"  />
</Columns>

</asp:GridView>
</center>
</asp:Content>

