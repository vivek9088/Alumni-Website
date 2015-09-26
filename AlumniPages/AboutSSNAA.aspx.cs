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
public partial class AlumniPages_AboutSSNAA : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmddispevt, cmdevents, cmdimg;
    MySqlDataReader drdispevt, drevents, drimg;
    public string EventHead, EventStory, name, msg;
    static string evtid;
 
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        if (Request.QueryString.Count==0) { evtid = "2"; }
        else
        { evtid = Request.QueryString["eventid"]; }
        try
        {
            con.Open();

            cmddispevt = new MySqlCommand("select name,detail,img1path,img2path,img3path from events where rowid=" + evtid, con);
            drdispevt = cmddispevt.ExecuteReader();
            drdispevt.Read();
            EventHead = drdispevt.GetString("name");
            EventStory = drdispevt.GetString("detail");
            img1.Src = "../"+drdispevt.GetString("img1path") + "thumbs/img1.jpg";
            img2.Src = "../" + drdispevt.GetString("img2path") + "thumbs/img2.jpg";
            img3.Src = "../" + drdispevt.GetString("img3path") + "thumbs/img3.jpg";
            aimg1.HRef = "../" + drdispevt.GetString("img1path") + "slides/img1.jpg";
            aimg2.HRef = "../" + drdispevt.GetString("img2path") + "slides/img2.jpg";
            aimg3.HRef = "../" + drdispevt.GetString("img3path") + "slides/img3.jpg";
         
            drdispevt.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load of AboutSSNAA Page for " + Session["loginname"] + ":" + ex.Message);
        }
    }
}