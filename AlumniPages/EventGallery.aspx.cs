using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;


public partial class AlumniPages_EventGallery : System.Web.UI.Page
{
    MySqlConnection con=new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    MySqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> list=new List<string>();
       
        if (!Page.IsPostBack)
        {
            try
            {
                con.Open();
                cmd = new MySqlCommand("select img1path,name,evt_id from events where rowid not in (2)", con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        string lt;

                        lt = "<img src='../" + dr.GetString("img1path") + "slides/img1.jpg' height='150' width='220'/><br/>";
                        lt += "<a href='EventList.aspx?evtid=" + dr.GetString("evt_id") + "'>" + dr.GetString("name") + "</a>";
                        list.Add(lt);

                    }
                    dr.Close();
                    con.Close();
                    eventslist.DataSource = list;
                    eventslist.DataBind();
                }
                else
                {
                    err.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load method of Event Gallery page for " + Session["loginname"] + ":" + ex.Message);
            }
        }
    }
}