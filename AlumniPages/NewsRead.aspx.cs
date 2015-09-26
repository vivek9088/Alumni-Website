using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class AlumniPages_NewsRead : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    MySqlDataReader dr;
    string nid;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        nid = Request.QueryString["news_id"];
        try
        {
            con.Open();
            cmd = new MySqlCommand("select * from news where newsid='" + nid + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                article.Text = "<h1 style='font-size:25px'>" + dr.GetString("headline").Substring(0, 1).ToUpper() + dr.GetString("headline").Substring(1).ToLower() + "</h1><br/><br/><img alt='' src='../images/tag.png' />&nbsp;Alumni News<br/><br/>";
                article.Text += " <p style='text-align: justify;'>" + dr.GetString("story") + "</p>";
                article.Text += "<p style='text-align: justify;'><a href='http://alumni.ssn.edu.in/alumni-stories.php'>http://alumni.ssn.edu.in/alumni-stories.php</a></p>";
                dr.Close();
                con.Close();
            }
            else
            {
                rerr.Visible = true;
            }

        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page load method of NewsRead page for " + Session["loginname"] + ":" + ex.Message);
        }
    }
}