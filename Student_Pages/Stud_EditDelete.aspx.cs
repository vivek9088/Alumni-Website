using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
public partial class Student_Pages_Stud_EditDelete : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    MySqlDataReader dr;
    MySqlDataAdapter da;
    string mybatch;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            con.Open();

            cmd = new MySqlCommand("select branch from forum_users where username='" + Session["loginname"].ToString() + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            mybatch = dr.GetString("branch");
            dr.Close();
           
            if (!Page.IsPostBack)
            {
                binddata();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load of Student Edit/Delete Page for " + Session["loginname"] + ":" + ex.Message);
     
        }
    }
    protected void binddata()
    {
        MySqlDataAdapter da = new MySqlDataAdapter("select id,username,password from forum_users where branch='"+mybatch+"'", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "users");
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void btnselectall_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chk1");
            cb.Checked = true;
        }
    }
    protected void btnclearall_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow r in GridView1.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chk1");
            cb.Checked = false;
        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string ds = "";
        foreach (GridViewRow r in GridView1.Rows)
        {
            CheckBox cb = (CheckBox)r.FindControl("chk1");
            if (cb.Checked)
            {
                int eno = Convert.ToInt32(GridView1.DataKeys[r.RowIndex].Value);
                ds = "delete from forum_users where id=" + eno;
                MySqlCommand cmd = new MySqlCommand(ds, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        binddata();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row;
        row = GridView1.Rows[e.RowIndex];
        TextBox t;
        t = (TextBox)row.Cells[3].Controls[0];
        string nename = t.Text;
        t = (TextBox)row.Cells[4].Controls[0];
        string eid = t.Text;
        int eno = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        MySqlCommand cmd = new MySqlCommand("update forum_users set username='" + nename + "',password=password('" + eid + "') where id=" + eno, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            binddata();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "GridView RowUpdating method of Stud_EditDel page for " + Session["loginname"] + ":"+ex.Message);
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        binddata();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        binddata();
    }

}