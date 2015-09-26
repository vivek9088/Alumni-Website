using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
public partial class AlumniPages_EventList : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd;
    MySqlDataReader dr;
    string event_id, imgpath;
    
    List<string> list = new List<string>();
       
    protected void Page_Load(object sender, EventArgs e)
    {
        int cnt=1;
        try
        {
            event_id = Request.QueryString["evtid"];

            con.Open();
            cmd = new MySqlCommand("select name,img1path from events where evt_id='" + event_id + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                imgpath = "../" + dr.GetString("img1path");

                HiddenField1.Value = imgpath;
                
                header1.Text = dr.GetString("name").Substring(0, 1).ToUpper() + dr.GetString("name").Substring(1).ToLower();

                DirectoryInfo files = new DirectoryInfo(Server.MapPath(imgpath + "thumbs/"));
                FileInfo[] f = files.GetFiles();

                foreach (FileInfo temp in f)
                {
                LiteralControl imgfile =new LiteralControl("<!-- start entry-->"
            +"<div class='thumbnailimage'><div class='thumb_container'><div class='large_thumb'>"
            +"<img src='"+ imgpath + "slides/" + temp.Name +"' class='large_thumb_image' alt='thumb' />"
            +"<img src='"+ imgpath + "slides/" + temp.Name +"' class='large_image' rel='Image-"+cnt+++"' />"
            +"<div class='large_thumb_border'></div><div class='large_thumb_shine'></div></div></div></div>"
            +"<!--end entry-->");
               
                showimgs.Controls.Add(imgfile);
                }
            }
            else
            {
                everr.Visible = true;
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "page load method of Eventlist page for " + Session["loginname"] + ":" + ex.Message);
        }
   }
}