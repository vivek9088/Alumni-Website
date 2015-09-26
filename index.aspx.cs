using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

public partial class index : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmddispevt, cmdevents, cmdimg;
    MySqlDataReader drdispevt, drevents,drimg;
    public string  name,msg;
    static string evtid;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
/*string domainName = Request.Url.Host.ToLower();
if(domainName.Contains("priyawedsram.com")){
    Response.Redirect("http://priyawedsram.com/Ram/index.html");
    return;
}*/
        //string branch,path;
        evtid = Request.QueryString["eventid"];
        if (evtid == null)  {evtid = "e_01";  }
       
        
        if(evtid=="e_01")
        {
        //abtunite.Attributes.Add("class", "current1");

        }
        else
        {

          //abtunite.Attributes.Remove("class");
        }
        try
       {
            con.Open();

            cmdevents = new MySqlCommand("select msg from annon where msg_date=curdate() order by msg_id desc LIMIT 1",con);
            drevents = cmdevents.ExecuteReader();
            if (drevents.Read())
            {
                annonmsg.Text = drevents.GetString("msg");
            }
            else
            {
                drevents.Close();

                /*cmdevents = new MySqlCommand("SELECT msg from annon where msg_date=(select max(msg_date) from annon where msg_date < curdate()) order by msg_id desc limit 1", con);
                drevents = cmdevents.ExecuteReader();
                if (drevents.Read())
                {
                    annonmsg.Text =drevents.GetString("msg");
                }
                else
                { annonmsg.Text = "No Announcement today"; }
                drevents.Close();*/
                annonmsg.Text = "<a href=#  onclick='javascript:shownews()'>Click Here for NewsLetter</a>";
            }
            drevents.Close();

        //this code displays all events name but only first five events is displayed
            cmdevents = new MySqlCommand("select evt_id,name from events order by rowid LIMIT 5", con);
            drevents = cmdevents.ExecuteReader();
            if (drevents.HasRows)
            {
                while (drevents.Read())
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    
                    if (evtid == drevents.GetString("evt_id").ToString())
                    {
                        li.InnerHtml += "<li class='current1'><a href='index.aspx?eventid=" + drevents.GetString("evt_id") + "#events'>" + drevents.GetString("name") + "</a></li>";
                    }
                    else
                    {
                        li.InnerHtml += "<li><a href='index.aspx?eventid=" + drevents.GetString("evt_id").ToString() + "#events'>" + drevents.GetString("name") + "</a></li>";
                    }
                    eventlinks.Controls.Add(li);
                }
                       }
            drevents.Close();

            //this code displays the event details and their images
            cmddispevt = new MySqlCommand("select head,detail,img1path,img2path,img3path from events where evt_id='" + evtid+"'", con);
            drdispevt = cmddispevt.ExecuteReader();
            if (drdispevt.Read())
            {
                EventHead.Text = drdispevt.GetString("head");
                EventStory.Text = drdispevt.GetString("detail").Substring(0, (drdispevt.GetString("detail").Length)/2)+" <b>...</b>";
                evtdet.Text = drdispevt.GetString("detail");
                img1.Src = (drdispevt.IsDBNull(2)) ? "images/nophoto1.jpg" : string.IsNullOrEmpty(drdispevt.GetString("img1path")) ? "images/nophoto1.jpg" : drdispevt.GetString("img1path") + "slides/img1.jpg";
                img2.Src = (drdispevt.IsDBNull(3)) ? "images/nophoto1.jpg" : string.IsNullOrEmpty(drdispevt.GetString("img2path")) ? "images/nophoto1.jpg" : drdispevt.GetString("img2path") + "slides/img2.jpg";
                img3.Src = (drdispevt.IsDBNull(4)) ? "images/nophoto1.jpg" : string.IsNullOrEmpty(drdispevt.GetString("img3path")) ? "images/nophoto1.jpg" : drdispevt.GetString("img3path") + "slides/img3.jpg";
                aimg1.HRef = (drdispevt.IsDBNull(2)) ? "images/nophoto1.jpg" : string.IsNullOrEmpty(drdispevt.GetString("img1path")) ? "images/nophoto1.jpg" : drdispevt.GetString("img1path") + "slides/img1.jpg";
                aimg2.HRef = (drdispevt.IsDBNull(3)) ? "images/nophoto1.jpg" : string.IsNullOrEmpty(drdispevt.GetString("img2path")) ? "images/nophoto1.jpg" : drdispevt.GetString("img2path") + "slides/img2.jpg";
                aimg3.HRef = (drdispevt.IsDBNull(4)) ? "images/nophoto1.jpg" : string.IsNullOrEmpty(drdispevt.GetString("img3path")) ? "images/nophoto1.jpg" : drdispevt.GetString("img3path") + "slides/img3.jpg";
                   
            }
            else
            {
                EventHead.Text = "No Event details available.";
                EventStory.Visible =img1.Visible=img2.Visible=img3.Visible= false;
            }
            drdispevt.Close();

       }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("Logs/Errorlog"), "Page Load of Index Page:" + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }
}