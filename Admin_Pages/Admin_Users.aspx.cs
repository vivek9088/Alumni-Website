using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Drawing;


public partial class Admin_Pages_Admin_Users : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd, cmduser;
    MySqlDataReader dr, drtopic, drpoll, druser;
    DataSet gds;
    static string mid, mpwd;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sh = string.IsNullOrEmpty(Request.QueryString["sh"]) ? "view1" : Request.QueryString["sh"],temp;
        temp = string.IsNullOrEmpty(Request.QueryString["adid"]) ? "addad" : Request.QueryString["adid"];
        MultiView1.ActiveViewIndex = (sh == "view1") ? 0 : (sh == "view2") ? 1:2;
        Page.MaintainScrollPositionOnPostBack = true;

        mid = ConfigurationManager.AppSettings["mailid"];
        mpwd = ConfigurationManager.AppSettings["mailpwd"];

        if (Page.IsPostBack)
        {

            if (!(String.IsNullOrEmpty(admin_pass1.Text.Trim())))
            {
                admin_pass1.Attributes["value"] = admin_pass1.Text;
            }
            admin_pass1.Text = admin_pass1.Attributes["value"];
        }

        if (sh == "view1")
        {
            valid.Attributes.Add("class", "active");
            invalid.Attributes.Remove("class");
            adminuser.Attributes.Remove("class");
        }
        else if(sh == "view2")
        {
            invalid.Attributes.Add("class", "active");
            valid.Attributes.Remove("class");
            adminuser.Attributes.Remove("class");
        }
        else if (sh == "view3")
        {
            adminuser.Attributes.Add("class", "active");
            valid.Attributes.Remove("class");
            invalid.Attributes.Remove("class");
          
            if (Request.QueryString["t"] == "1")
            { admin_msg.Visible = true; }
        }
        if (temp == "addad")
        {
            addad.Attributes.Add("class", "active");
            delad.Attributes.Remove("class");
            div1.Style.Add("display", "block");
            div2.Style.Add("display", "none");
        }
        else
        {
            delad.Attributes.Add("class", "active");
            addad.Attributes.Remove("class");
            div2.Style.Add("display", "block");
            div1.Style.Add("display", "none");
        }
        if (!IsPostBack)
        { 
            bind_valid(); 
            bind_invalid();
            bindadmindata();
        }
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        ArrayList a = new ArrayList();
        for (int i = 0; i < gridviewchk.Rows.Count; i++)
        {
            CheckBox chkbx = (CheckBox)gridviewchk.Rows[i].Cells[0].FindControl("CheckBox1");

            if (chkbx.Checked)
            {
                a.Add(gridviewchk.Rows[i].Cells[1].Text);
            }
        }
        deleterecord(a);
        bind_valid();
    }

    public void bind_valid()
    {
        MySqlCommand cmd = new MySqlCommand("select regid,fname,lname,dob,branch from alumnireg where valid_mail=1", con);
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds,"alumnireg");
        if (ds.Tables[0].Rows.Count == 0)
        {
            gridviewchk.DataSource = ds;
            gridviewchk.DataBind();
            lblmsg.Visible = true;
        }
        else
        {
            gridviewchk.DataSource = ds;
            gridviewchk.DataBind();
        }
    }
    public void deleterecord(ArrayList ar)
    {
        try
        {
            con.Open();
            foreach (object i in ar)
            {
                string str = "delete from alumnireg where regid=" + i + "";
                MySqlCommand cmd = new MySqlCommand(str, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("delete from login where regid=" + i + "", con);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("delete from forum_users where regid=" + i + "", con);
                cmd.ExecuteNonQuery();

            }
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Delete Record method of Admin_users page for " + Session["loginname"] + ": " + ex.Message);
        }
    }

    protected void gridviewchk_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridviewchk.PageIndex = e.NewPageIndex;
        bind_valid();
    }
    
    public void bind_invalid()
    {
        MySqlCommand cmd = new MySqlCommand("select regid,fname,lname,dob,branch,username,incorrect from alumnireg where valid_mail=0", con);
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "alumnireg");
        if (ds.Tables[0].Rows.Count == 0)
        {
            gridview1.DataSource = ds;
            gridview1.DataBind();
            lblmsg1.Visible = true;
            viewdet.Visible = false;
        }
        else
        {
        viewdet.Visible = true;
            gridview1.DataSource = ds;
            gridview1.DataBind();
        }
    }
    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridview1.PageIndex = e.NewPageIndex;
        bind_invalid();
    }
    protected void gridview1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridview1.EditIndex = -1;
        bind_invalid();
    }
    protected void gridview1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridview1.EditIndex = e.NewEditIndex;
        bind_invalid();
    }
    protected void gridview1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row;
        row = gridview1.Rows[e.RowIndex];

        //this is to get the values from textboxes in edit mode
        TextBox  t1, t2, t3;
        
        t1 = (TextBox)row.Cells[3].Controls[0];
        t2 = (TextBox)row.Cells[4].Controls[0];
        t3 = (TextBox)row.Cells[5].Controls[0];
        
        int eno = Convert.ToInt32(gridview1.DataKeys[e.RowIndex].Value);

        MySqlCommand cmd = new MySqlCommand("update alumnireg set fname='" + t1.Text + "',lname='" + t2.Text + "',dob='" + t3.Text  + "' where regid=" + eno, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "GridView1_RowUpdating method of Admin_users page for " + Session["loginname"] + ":" + ex.Message);
        }
        gridview1.EditIndex = -1;
        bind_invalid();
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        ArrayList a = new ArrayList();
        for (int i = 0; i < gridview1.Rows.Count; i++)
        {
            CheckBox chkbx = (CheckBox)gridview1.Rows[i].Cells[1].FindControl("CheckBox1");

            if (chkbx.Checked)
            {
                a.Add(gridview1.Rows[i].Cells[2].Text);
            }
        }
        approverecord(a);
        lblmsg1.Visible = true;
        lblmsg1.Text = "Approved Selected Users";
        bind_invalid();
        
    }
    public void approverecord(ArrayList ar)
    {
        try
        {
            con.Open();
            foreach (object i in ar)
            {
                MySqlCommand cmd = new MySqlCommand("update alumnireg set valid_mail=1 where regid=" + i, con);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("update login set logentry=2 where regid=" + i, con);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("update forum_users set group_id=4 where regid=" + i, con);
                cmd.ExecuteNonQuery();

                sendmail(i);

                cmd = new MySqlCommand("update alumnireg set incorrect='Success' where regid=" + i, con);
                cmd.ExecuteNonQuery();

            }
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Approve Record method of Admin_users page for " + Session["loginname"] + ": " + ex.Message);
        }
        
    }
    protected void sendmail(object temp)
    {
        MySqlCommand cmdmail;
        MySqlDataReader drmail;
        MailMessage mail = new MailMessage();
        string msgbody, encodeuser;

        try
        {
            
            cmdmail = new MySqlCommand("select email,username from alumnireg where regid=" + temp, con);
            drmail = cmdmail.ExecuteReader();
            drmail.Read();

            mail.From = new MailAddress("ssntribute@gmail.com", "Alumni Admin");
            mail.To.Add(drmail.GetString("email"));
            mail.Subject = "Registration Details Approved";
            encodeuser = drmail.GetString("username");

            msgbody = "Dear Alumni," + "<br/><br/>Your registeration details have being verified and approved by the alumni officer successfully. You can now login into the site by clicking the link below.<br/><br/>Your Username is : " + encodeuser;
            msgbody += "<br/><br/><a href='http://ssnunite.com/LoginPage.aspx' target='_blank'>Click this link to Login into the site</a>" + "<br/><br/>Thank you";
            mail.Body = msgbody;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            if(!Page.IsPostBack)
            {
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential(mid,mpwd);
            smtp.EnableSsl = true;
            smtp.Send(mail);
            }
            drmail.Close();
           
        }
        catch (SmtpException sx)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Send Mail method of Admin_users page for " + Session["loginname"] + ":" + sx.Message);
        }
                
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Send Mail method of Admin_users page for " + Session["loginname"] + ": " + ex.Message);
        }
        
    }
   
    protected void btndel1_Click(object sender, EventArgs e)
    {
        ArrayList a = new ArrayList();
        for (int i = 0; i < gridview1.Rows.Count; i++)
        {
            CheckBox chkbx = (CheckBox)gridview1.Rows[i].Cells[1].FindControl("CheckBox1");

            if (chkbx.Checked)
            {
                a.Add(gridview1.Rows[i].Cells[2].Text);
            }
        }
        deleterecord(a);
        bind_invalid();
    }

    protected void add_adminuser_Click(object sender, EventArgs e)
    {
         DateTime regdate = System.DateTime.Now, origin = new DateTime(2000, 1, 1, 0, 0, 0, 0);
         string time_stamp,ipaddr;
         TimeSpan stamp;

        stamp = regdate - origin;
        time_stamp = stamp.Days.ToString() + stamp.Milliseconds.ToString();

        ipaddr= Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipaddr == null) //may be the HTTP_X_FORWARDED_FOR is null
        {
            ipaddr = Request.ServerVariables["REMOTE_ADDR"];//we can use REMOTE_ADDR
        }

            try
            {
                con.Open();
                cmd = new MySqlCommand("insert into forum_users(regid,username,password,email,registration_ip,registered,group_id,title,location) values(@aluid,@name1,password('@pass2'),@mail1,@ip,@reg,@gid,@tile,@loc)", con);
                cmd.Parameters.AddWithValue("aluid", int.Parse(time_stamp));
                if (utype.SelectedIndex == 2)
                {
                    cmd.Parameters.AddWithValue("name1", "stud_" + admin_name.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("name1", admin_name.Text.Trim());
                }
                cmd.Parameters.AddWithValue("pass2", admin_pass1.Text.Trim());
                cmd.Parameters.AddWithValue("mail1", admin_email.Text.Trim());
                cmd.Parameters.AddWithValue("ip", ipaddr);
                cmd.Parameters.AddWithValue("reg", regdate);
                cmd.Parameters.AddWithValue("gid", logtype.SelectedItem.Value);
                if (utype.SelectedIndex == 1)
                {
                        cmd.Parameters.AddWithValue("tile", cmpname.Text.Trim());
                        cmd.Parameters.AddWithValue("loc", cmploc.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("tile", null);
                    cmd.Parameters.AddWithValue("loc", null);
                }
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "add_adminuser method of Admin_users page for " + Session["loginname"] + ": " + ex.Message);
            }
            finally
            {
                Response.Redirect("Admin_Users.aspx?sh=view3&t=1#div1");
            }
        
    }
    protected void AdminGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        AdminGridView1.PageIndex = e.NewPageIndex;
        bindadmindata();
    }
  
    protected void bindadmindata()
    {
    MySqlDataAdapter da = new MySqlDataAdapter("select group_id,regid,username,email from forum_users", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "forum_users");
        if (ds.Tables[0].Rows.Count == 0)
        {
            admsg.Visible = true;
            AdminGridView1.DataSource = ds;
            AdminGridView1.DataBind();
        }
        else
        {
        admsg.Visible = false;
            AdminGridView1.DataSource = ds;
            AdminGridView1.DataBind();
        }
    }

    protected void AdminGridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int row = int.Parse(AdminGridView1.DataKeys[e.RowIndex].Value.ToString());
        MySqlCommand cmd = new MySqlCommand("delete from forum_users where regid=" + row, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindadmindata();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "AdminGridView1_RowDeleting method of Admin_users page for " + Session["loginname"] + ": " + ex.Message);
        }
    }

    
    protected void admin_email_TextChanged(object sender, EventArgs e)
    {
        emailerr.Visible = true;
        if (admin_email.Text.Trim() != "")
        {
            if (admin_email.Text.Contains(" "))
            {
                emailerr.ForeColor = Color.Red;
                emailerr.Text = "<br/>Email-Id cannot contain spaces between words";
                admin_email.Focus();
            }
            else
            {
                con.Open();
                cmduser = new MySqlCommand("select email from forum_users where email=@user1", con);
                cmduser.Parameters.AddWithValue("@user1", admin_email.Text.Trim());
                druser = cmduser.ExecuteReader();
                druser.Read();
                if (druser.HasRows)  // if the emailid is already in database then return true
                {
                    emailerr.ForeColor = Color.Red;
                    emailerr.Text = "<br/>Email-Id Not Available";
                    admin_email.Focus();
                }
                else
                {
                    emailerr.ForeColor = Color.GreenYellow;
                    emailerr.Text = "<br/>Email-Id is Available";
                }

                druser.Close();
                con.Close();
            }
        }//first if ends here
        else //outer if's else
        {
            emailerr.ForeColor = Color.Red;
            emailerr.Text = "<br/>Please enter an Email-Id";
            admin_email.Focus();
        }
    }
    protected void generate_Click(object sender, EventArgs e)
    {
        gridview2.PageIndex = 0;
        MySqlDataAdapter da = new MySqlDataAdapter("select old_fname,old_lname,old_dob from oldalumnireg where old_fname like '"+entername.Text.Trim()+"%'", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "oldalumnireg");
        if (ds.Tables[0].Rows.Count == 0)
        {
            divmsg.Visible = true;
           gridview2.DataSource = ds;
            gridview2.DataBind();
        }
        else
        {
            gridview2.DataSource = ds;
            gridview2.DataBind();
            divmsg.Visible = false;
        }
    }
    protected void gridview2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridview2.PageIndex = e.NewPageIndex;
        generate_Click(sender, e);
    }
    protected void utype_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indx = utype.SelectedIndex;
        admin_msg.Visible = false;
        switch (indx)
        {
            case 0:// for forum admin and guest
                logtype.Items[0].Value = "1";
                logtype.Items[1].Value = "3";
                logtype.Items[1].Enabled = true;
                logtype.Items[0].Enabled = true;
                cmpname.Enabled = false;
                cmploc.Enabled = false;
                break;
            case 1:// for job guest user
                logtype.Items[0].Enabled = false;
                logtype.Items[1].Value = "6";
                logtype.Items[1].Enabled = true;
              cmpname.Enabled = true;
              cmploc.Enabled = true;
                break;
            case 2:// for student admin
                logtype.Items[0].Enabled = true;
                logtype.Items[1].Enabled = false;
                logtype.Items[0].Value = "5"; 
                cmpname.Enabled = false;
                cmploc.Enabled = false;
                break;
        }
    }
}