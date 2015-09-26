using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
public partial class Admin_Pages_Admin_Poll : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sh = string.IsNullOrEmpty(Request.QueryString["sh"]) ? "view1" : Request.QueryString["sh"];
        MultiView1.ActiveViewIndex = (sh == "view1") ? 0 : 1;

        if (sh == "view1")
        {
            addpoll.Attributes.Add("class", "active");
            editpoll.Attributes.Remove("class");
        }
        else
        {
            editpoll.Attributes.Add("class", "active");
            addpoll.Attributes.Remove("class");
        }

   
        if (!Page.IsPostBack)
        {
            bindpolldata();
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (opt1.Text == "" || opt2.Text == "" || opt3.Text == "" || opt4.Text == "")
        {
            success.Text = "Please fill in all the Option values";
        }
       
        else
        {
            success.Text = "";
        try
        {
            con.Open();
            cmd = new MySqlCommand("insert into poll(pollquest,opt1,opt2,opt3,opt4,activate,polldate) values(@quest,@opt1,@opt2,@opt3,@opt4,@act,@date)", con);
            cmd.Parameters.AddWithValue("quest", pollquest.Value.Trim());
            cmd.Parameters.AddWithValue("opt1", opt1.Text.Trim());
            cmd.Parameters.AddWithValue("opt2", opt2.Text.Trim());
            cmd.Parameters.AddWithValue("opt3", opt3.Text.Trim());
            cmd.Parameters.AddWithValue("opt4", opt4.Text.Trim());
            cmd.Parameters.AddWithValue("activate", pollactivate.SelectedItem.Value);
            cmd.Parameters.AddWithValue("polldate", System.DateTime.Now.Date);
            cmd.ExecuteNonQuery();
            con.Close();
            success.Text = "Poll Question has been successfully Posted";
        
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Poll submit method of admin poll for " + Session["loginname"] + ":" + ex.Message);
        }
        
        }
    }


    protected void PollGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PollGridView1.PageIndex = e.NewPageIndex;
        bindpolldata();
    }

    protected void bindpolldata()
    {
        MySqlDataAdapter da = new MySqlDataAdapter("select rowid,pollquest,opt1,opt2,opt3,opt4,activate from poll", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "poll");
        PollGridView1.DataSource = ds;
        PollGridView1.DataBind();
    }
    protected void PollGridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        PollGridView1.EditIndex = -1;
        bindpolldata();
    }
    protected void PollGridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PollGridView1.EditIndex = e.NewEditIndex;
        bindpolldata();
    }
    protected void PollGridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int row = Convert.ToInt32(PollGridView1.DataKeys[e.RowIndex].Value);
        MySqlCommand cmd = new MySqlCommand("delete from poll where rowid=" + row, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "PollGridView RowDelete method of admin poll page for " + Session["loginname"] + ":" + ex.Message);
     
        }
        bindpolldata();
    }

    protected void PollGridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row;
        row = PollGridView1.Rows[e.RowIndex];
     
        TextBox t,t1,t2,t3,t4,t5;
        t = (TextBox)row.Cells[2].Controls[0];
        t1 = (TextBox)row.Cells[3].Controls[0];
        t2 = (TextBox)row.Cells[4].Controls[0];
        t3 = (TextBox)row.Cells[5].Controls[0];
        t4 = (TextBox)row.Cells[6].Controls[0];
        t5 = (TextBox)row.Cells[7].Controls[0];
        
        int eno = Convert.ToInt32(PollGridView1.DataKeys[e.RowIndex].Value);
        
        MySqlCommand cmd = new MySqlCommand("update poll set pollquest='" + t.Text + "',opt1='" + t1.Text + "',opt2='" + t2.Text + "',opt3='" + t3.Text + "',opt4='" + t4.Text + "',activate='" + t5.Text + "' where rowid=" + eno, con);

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "PollGridView RowUpdate method of admin Poll for " + Session["loginname"] + ":" + ex.Message);
     
        }
        PollGridView1.EditIndex = -1;
        bindpolldata();
    }
}