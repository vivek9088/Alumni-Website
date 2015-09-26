using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class AlumniPages_News : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    MySqlDataReader dr;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> list = new List<string>();

        if (!Page.IsPostBack)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("select headline,story,name,newsid from news", con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string lt;
                        string newsstry = (dr.GetString("story").Length > 50) ? dr.GetString("story").Substring(0, 100) : dr.GetString("story").Substring(0, 10);
                        lt = "<h2 style='font-size:23px' 'title'><a title='" + dr.GetString("headline").Substring(0, 1).ToUpper() + dr.GetString("headline").Substring(1).ToLower() + "' href='#'>" + dr.GetString("headline").Substring(0, 1).ToUpper() + dr.GetString("headline").Substring(1).ToLower() + "</a></h2>";
                        lt += "<p style='text-align: justify;'>" + newsstry + "..</p>";
                        lt += "<img src='../images/read_more.png'/><a href='NewsRead.aspx?news_id=" + dr.GetString("newsid") + "'>Read more</a><br/><br/><hr class='dash'/>";
                        list.Add(lt);
                    }

                    dr.Close();
                    con.Close();

                    Repeater1.DataSource = list;
                    Repeater1.DataBind();
                }
                else
                {
                    nerr.Visible = true;
                }
            }

            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page load method of News page for " + Session["loginname"] + ":" + ex.Message);
            }
        }
    }
}