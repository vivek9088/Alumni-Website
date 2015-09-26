using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
public partial class AboutSSNAlumni : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmddispevt, cmdevents, cmdimg;
    MySqlDataReader drdispevt, drevents, drimg;
    public string EventHead, EventStory, name, msg;
    static string evtid;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        evtid = Request.QueryString["eventid"];
        if (evtid == null) { evtid = "2"; }

        try
        {
            con.Open();
        /*
            cmdevents = new MySqlCommand("select rowid,name from events where rowid > 1 order by rowid", con);
            drevents = cmdevents.ExecuteReader();
            while (drevents.Read())
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                if (evtid == drevents.GetInt32("rowid").ToString())
                {
                    li.InnerHtml += "<li class='current1'><a href='AboutSSNAlumni.aspx?eventid=" + drevents.GetInt32("rowid") + "#events'>" + drevents.GetString("name") + "</a></li>";
                }
                else
                {
                    li.InnerHtml += "<li><a href='AboutSSNAlumni.aspx?eventid=" + drevents.GetInt32("rowid").ToString() + "#events'>" + drevents.GetString("name") + "</a></li>";
                }
                eventlinks.Controls.Add(li);
            }
            drevents.Close();
            */
           cmddispevt = new MySqlCommand("select name,detail,img1path,img2path,img3path from events where rowid=" + evtid, con);
            drdispevt = cmddispevt.ExecuteReader();
            drdispevt.Read();
            EventHead = drdispevt.GetString("name");
            EventStory = drdispevt.GetString("detail");
            img1.Src =  drdispevt.GetString("img1path")+"thumbs/img1.jpg";
            img2.Src =  drdispevt.GetString("img2path")+"thumbs/img2.jpg";
            img3.Src = drdispevt.GetString("img3path") + "thumbs/img3.jpg";
           aimg1.HRef = drdispevt.GetString("img1path") + "slides/img1.jpg";
            aimg2.HRef = drdispevt.GetString("img2path") + "slides/img2.jpg";
            aimg3.HRef = drdispevt.GetString("img3path") + "slides/img3.jpg";
         
            drdispevt.Close();

        }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("Logs/Errorlog"), "Page Load of Index Master Page:" + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }
}