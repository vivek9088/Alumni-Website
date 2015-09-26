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

public partial class AlumniPages_AlumniHome : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmddispevt, cmdevents, cmdpoll, cmduser;
    MySqlDataReader drdispevt, drevents, drpoll, druser;
    public string pollquest;
    string user, evtid, tbatch, cour, photo;
    HtmlTableCell tc1, tc2;
    HtmlTableRow tr;

    protected void Page_Load(object sender, EventArgs e)
    {
                    try
            {
                evtid = Request.QueryString["eventid"];
                if (evtid == null) { evtid = "e_01"; }

                user = Session["loginname"].ToString();

                con.Open();
                cmduser = new MySqlCommand("select fname,lname from alumnireg where username='"+user+"'", con);
                druser = cmduser.ExecuteReader();
                druser.Read();
                showuser.Text = druser.GetString("fname") + " " + druser.GetString("lname");
                druser.Close();

                cmduser = new MySqlCommand("select fname,lname,batch,branch,desig,org,imgpath from alumnireg where username not in('"+user+"') and batch=(select batch from alumnireg where username='" + user + "') order by rowid desc limit 3", con);
        
                druser = cmduser.ExecuteReader();
                if (druser.HasRows)
                {
                    while (druser.Read())
                    {
                        tr = new HtmlTableRow();
                        tc1 = new HtmlTableCell();
                        tc2 = new HtmlTableCell();

                        string[] desigcmp = string.IsNullOrEmpty(druser["desig"].ToString()) ? new string[] { "-" } : druser.GetString("desig").Split('/');
                        string[] orgcmp = string.IsNullOrEmpty(druser["org"].ToString()) ? new string[] { "-" } : druser.GetString("org").Split('/');
                        string temptxt = "";

                        if (File.Exists(Server.MapPath(druser.IsDBNull(6) ? "" : druser.GetString("imgpath"))))
                        {
                            photo = "<img  id='photo' width='65' height='70' runat='server' alt='No Image to Display' src='../" + druser.GetString("imgpath") + "'></img>"; 
                        }
                        else
                        {
                            photo = "<img  id='photo' width='65' height='70' runat='server' alt='No Image to Display' src='../images/nophoto.jpg'></img>";
                        }

                        cour = string.IsNullOrEmpty(druser["branch"].ToString()) ? "course" : druser.GetString("branch");
                        tbatch = string.IsNullOrEmpty(druser["batch"].ToString()) ? "-" : druser.GetUInt32("batch").ToString();

                        if (orgcmp[0].Length <= 5)
                        {
                           temptxt="I work as " + desigcmp[0].Substring(0, 1).ToUpper() + desigcmp[0].Substring(1) + " in " + orgcmp[0].ToUpper();
                        }
                        else
                        {
                            temptxt = "I work as " + desigcmp[0].Substring(0, 1).ToUpper() + desigcmp[0].Substring(1) + " in " + orgcmp[0].Substring(0, 1).ToUpper() + orgcmp[0].Substring(1).ToLower() + "";
                        }
                        
                        tc1.Controls.Add(new LiteralControl("<figure><a href='#'>" + photo + "</a></figure>"));
                        tc2.Controls.Add(new LiteralControl("I am " + druser.GetString("fname") + " " + druser.GetString("lname") + "<br/>I completed my " + cour + " during " + tbatch + "<br/>"+temptxt));

                        tr.Controls.Add(tc1);
                        tr.Controls.Add(tc2);
                        
                        recentlist.Rows.Add(tr);
                    }
                    druser.Close();
                }

                //this code shows the login date and time
                cmduser = new MySqlCommand("select login_prev,login_curr from login_hist where login_name='" + user + "' order by rowid desc", con);
                druser = cmduser.ExecuteReader();
                druser.Read();
                if (druser.IsDBNull(0))
                {
                    logdatetime.Text = druser["login_curr"].ToString();
                }
                else
                {
                    logdatetime.Text = druser["login_prev"].ToString(); 
                }
                druser.Close();

                //this is for showing the events details with images
                cmdevents = new MySqlCommand("select evt_id,name from events order by rowid limit 5", con);
                drevents = cmdevents.ExecuteReader();
                while (drevents.Read())
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    if (evtid == drevents.GetString("evt_id").ToString())
                    {
                        li.InnerHtml += "<li class='current1'><a href='AlumniHome.aspx?eventid=" + drevents.GetString("evt_id") + "#events'>" + drevents.GetString("name") + "</a></li>";
                    }
                    else
                    {
                        li.InnerHtml += "<li><a href='AlumniHome.aspx?eventid=" + drevents.GetString("evt_id").ToString() + "#events'>" + drevents.GetString("name") + "</a></li>";
                    }
                    eventlinks.Controls.Add(li);
                }
                drevents.Close();

                //this code displays the event details and their images
                cmddispevt = new MySqlCommand("select head,detail,img1path,img2path,img3path from events where evt_id='" + evtid+"'", con);
                drdispevt = cmddispevt.ExecuteReader();
                if (drdispevt.Read())
                {
                    EventHead.Text = drdispevt.GetString("head");
                    EventStory.Text = drdispevt.GetString("detail").Substring(0, (drdispevt.GetString("detail").Length) / 2) + " <b>...</b><br/><br/>";
                    evtdet.Text = drdispevt.GetString("detail");
                    img1.Src = (drdispevt.IsDBNull(2)) ? "../images/nophoto1.jpg" : File.Exists("../"+Server.MapPath(drdispevt.GetString("img1path"))) ? "../images/nophoto1.jpg" : "../"+drdispevt.GetString("img1path") + "slides/img1.jpg";
                    img2.Src = (drdispevt.IsDBNull(3)) ? "../images/nophoto1.jpg" : File.Exists("../" + Server.MapPath(drdispevt.GetString("img2path"))) ? "../images/nophoto1.jpg" : "../" + drdispevt.GetString("img2path") + "slides/img2.jpg";
                    img3.Src = (drdispevt.IsDBNull(4)) ? "../images/nophoto1.jpg" : File.Exists("../" + Server.MapPath(drdispevt.GetString("img3path"))) ? "../images/nophoto1.jpg" : "../" + drdispevt.GetString("img3path") + "slides/img3.jpg";
                    aimg1.HRef = (drdispevt.IsDBNull(2)) ? "../images/nophoto1.jpg" : File.Exists("../" + Server.MapPath(drdispevt.GetString("img1path"))) ? "../images/nophoto1.jpg" : "../" + drdispevt.GetString("img1path") + "slides/img1.jpg";
                    aimg2.HRef = (drdispevt.IsDBNull(3)) ? "../images/nophoto1.jpg" : File.Exists("../" + Server.MapPath(drdispevt.GetString("img2path"))) ? "../images/nophoto1.jpg" : "../" + drdispevt.GetString("img2path") + "slides/img2.jpg";
                    aimg3.HRef = (drdispevt.IsDBNull(4)) ? "../images/nophoto1.jpg" : File.Exists("../" + Server.MapPath(drdispevt.GetString("img3path"))) ? "../images/nophoto1.jpg" : "../" + drdispevt.GetString("img3path") + "slides/img3.jpg";
                  
                }
                else
                {
                    EventHead.Text = "No Event details available.";
                    EventStory.Visible = img1.Visible = img2.Visible = img3.Visible = false;
                }
                  drdispevt.Close();
                  con.Close();
            }
            catch (Exception ex)
            {
                CreateLogFile errlog = new CreateLogFile();
                errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load of Alumni_Home for " + Session["loginname"] + ":" + ex.Message);
            }
        }
}