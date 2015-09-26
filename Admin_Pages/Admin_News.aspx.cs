using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
public partial class Admin_Pages_Admin_News : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    MySqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sh = string.IsNullOrEmpty(Request.QueryString["sh"]) ? "view1" : Request.QueryString["sh"];
        MultiView1.ActiveViewIndex = (sh == "view1") ? 0 : 1;

        if (sh == "view1")
        {
            addnews.Attributes.Add("class", "active");
            editnews.Attributes.Remove("class");
        }
        else
        {
            editnews.Attributes.Add("class", "active");
            addnews.Attributes.Remove("class");
        }

        if (!Page.IsPostBack)
        {
            bindnewsdata();
        }
    }
    
    protected void NewsAdd_Click(object sender, EventArgs e)
    {
        string path = "",temp;
         string[] id;
        int chk;
        try
        {
         
            con.Open();

            cmd=new MySqlCommand("select max(newsid) from news",con);
            dr=cmd.ExecuteReader();
            dr.Read();
            id=dr.GetString("max(newsid)").Split('_');
            chk=int.Parse(id[1])+1;
            dr.Close();

            if(chk<10)
            {
            temp="n_0"+chk.ToString();
            }
            else
            {
            temp="n_"+chk.ToString();
            }

            cmd = new MySqlCommand("insert into news(headline,story,name,email,attachpath,newsid) values(@head,@stry,@name,@mail,@path,@nid)", con);
            cmd.Parameters.AddWithValue("nid",temp);
            cmd.Parameters.AddWithValue("head", newshead.Text.Trim());
            cmd.Parameters.AddWithValue("stry", newsdetail.Value.Trim());
            cmd.Parameters.AddWithValue("name", newsname.Text.Trim());
            cmd.Parameters.AddWithValue("mail", newsemail.Text.Trim());

            if (newsattach.HasFile==false)
            {
                path = "";
            }
            else
            {
                path = "News/Files/" + newsattach.PostedFile.FileName;
            }
            cmd.Parameters.AddWithValue("path", path);
            cmd.ExecuteNonQuery();
            con.Close();

            clearbox();
            success1.Text = "News has been successfully Posted";
          
          
          
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "News add method of admin news for " + Session["loginname"] + ":" + ex.Message);
        }
       
    }
    //to clear all the textboxes
    protected void clearbox()
    {
        foreach (Control c in table1.Controls)
        {
            if (c.GetType() == typeof(TextBox))
            {
                ((TextBox)c).Text = "";
            }
        }
       
    }
    protected void NewsGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        NewsGridView1.PageIndex = e.NewPageIndex;
        bindnewsdata();
    }
    protected void bindnewsdata()
    {
        MySqlDataAdapter da = new MySqlDataAdapter("select rowid,newsid,headline,name from news", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "news");
        NewsGridView1.DataSource = ds;
        NewsGridView1.DataBind();
    }

    protected void NewsGridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int row = Convert.ToInt32(NewsGridView1.DataKeys[e.RowIndex].Value);
        MySqlCommand cmd = new MySqlCommand("delete from news where rowid=" + row, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            bindnewsdata();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "NewsGridView RowDeleting event of Admin_News page for " + Session["loginname"] + ":"+ex.Message);
        }
    }
    
}