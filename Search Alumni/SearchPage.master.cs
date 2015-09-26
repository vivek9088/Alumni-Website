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
public partial class Search_Alumni_SearchPage : System.Web.UI.MasterPage
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdevents,cmdpoll,cmduser,cmdtotcnt;
    MySqlDataReader drevents,drpoll,druser,drtotcnt;
    public string  ans1, ans2, ans3, ans4,per1,per2,per3,per4,visitor,visitcnt;
    string user,pid;
    public int repcnt, usercnt, topcnt;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        int temp = int.Parse(Application["counter"].ToString());

        foruml.HRef = ConfigurationManager.AppSettings["forumurl"];
        jobl.HRef = ConfigurationManager.AppSettings["joburl"];

        Page.MaintainScrollPositionOnPostBack = true;

        if (temp >= 1 && temp < 10)
            visitcnt = "00000" + temp.ToString();
        else if (temp >= 10 && temp < 100)
            visitcnt = "0000" + temp.ToString();
        else if (temp >= 100 && temp < 1000)
            visitcnt = "000" + temp.ToString();
        else if (temp >= 1000 && temp < 10000)
            visitcnt = "00" + temp.ToString();
        else if (temp >= 10000 && temp < 100000)
            visitcnt = "0" + temp.ToString();
        else
            visitcnt = temp.ToString();

        if (Session["loginname"] == null)
        {
            reurl.Value = "3";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Your Current Session Expired..Please Login again!!!');</script>", true);

        }
        else
        {
            try
            {
                user = Session["loginname"].ToString();
                con.Open();

                if (!Page.IsPostBack)
                {
                    cmdpoll = new MySqlCommand("select pollid,pollquest,opt1,opt2,opt3,opt4 from poll where activate='Yes' order by rowid desc LIMIT 1", con);
                    drpoll = cmdpoll.ExecuteReader();
                    if (drpoll.Read())
                    {
                        pid = drpoll.GetString("pollid");
                        pollquest.Text = drpoll["pollquest"].ToString();
                        pollopt.Items.Insert(0, drpoll["opt1"].ToString());
                        pollopt.Items.Insert(1, drpoll["opt2"].ToString());
                        pollopt.Items.Insert(2, drpoll["opt3"].ToString());
                        pollopt.Items.Insert(3, drpoll["opt4"].ToString());
                       
                    }
                    else
                    {
                        pollerr.Visible = true;
                        vote.Visible = false;
                    }
                    drpoll.Close();
                }

                cmdtotcnt = new MySqlCommand("select count(subject) from forum_topics", con);
                drtotcnt = cmdtotcnt.ExecuteReader();
                if (drtotcnt.Read())
                {
                    topcnt = string.IsNullOrEmpty(drtotcnt.GetInt32(0).ToString()) ? 0 : drtotcnt.GetInt32(0);
                }
                else
                { topcnt = 0; }
                drtotcnt.Close();

                cmdtotcnt = new MySqlCommand("select count(username) from alumnireg where valid_mail=1", con);
                drtotcnt = cmdtotcnt.ExecuteReader();
                if (drtotcnt.Read())
                {
                    usercnt = string.IsNullOrEmpty(drtotcnt.GetInt32(0).ToString()) ? 0 : drtotcnt.GetInt32(0);
                }
                else
                { usercnt = 0; }
                drtotcnt.Close();

                cmdtotcnt = new MySqlCommand("select count(num_replies) from forum_topics", con);
                drtotcnt = cmdtotcnt.ExecuteReader();
                if (drtotcnt.Read())
                {
                    repcnt = string.IsNullOrEmpty(drtotcnt.GetInt32(0).ToString()) ? 0 : drtotcnt.GetInt32(0);
                }
                else
                {
                    repcnt = 0;
                }
                drtotcnt.Close();

                cmdtotcnt = new MySqlCommand("select login_name from login_hist where rowid =(select max(rowid) from login_hist)", con);
                drtotcnt = cmdtotcnt.ExecuteReader();
                if (drtotcnt.Read())
                {
                    visitor = string.IsNullOrEmpty(drtotcnt.GetString("login_name")) ? "-" : drtotcnt.GetString("login_name");
                }
                else
                { visitor = "-"; }
                drtotcnt.Close();
            }
            catch (Exception ex)
            {
                CreateLogFile errlog = new CreateLogFile();
                errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "page load of SearchAlumni Master for " + Session["loginname"] + ":" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
    protected void vote_Click1(object sender, EventArgs e)
    {

        MySqlCommand cmdvtupdt, cmd;
        MySqlDataReader dr;
        int optselect = pollopt.SelectedIndex;

        try
        {
            con.Open();

            cmd = new MySqlCommand("select * from voting where ipaddr='" + Request.UserHostAddress + "' and poll_id='" + pid + "'", con);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "Message('You can vote only once for a poll..Thank You!!! ');", true);
                votediv.Style.Add("display", "block");
                progbar.Style.Add("display", "none");
            }
            else
            {
                dr.Close();

                if (optselect < 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "Message('Please select an option..!!!');", true);
                    votediv.Style.Add("display", "block");
                    progbar.Style.Add("display", "none");
                }
                else
                {
                    switch (optselect)
                    {
                        case 0:

                            cmdvtupdt = new MySqlCommand("update poll set res1=res1+1 where pollquest='" + pollquest.Text + "'", con);
                            cmdvtupdt.ExecuteNonQuery();
                            break;
                        case 1:

                            cmdvtupdt = new MySqlCommand("update poll set res2=res2+1 where pollquest='" + pollquest.Text + "'", con);
                            cmdvtupdt.ExecuteNonQuery();
                            break;
                        case 2:

                            cmdvtupdt = new MySqlCommand("update poll set res3=res3+1 where pollquest='" + pollquest.Text + "'", con);
                            cmdvtupdt.ExecuteNonQuery();
                            break;
                        case 3:

                            cmdvtupdt = new MySqlCommand("update poll set res4=res4+1 where pollquest='" + pollquest.Text + "'", con);
                            cmdvtupdt.ExecuteNonQuery();
                            break;

                    }
                    //this code makes an entry into db for user entry
                    cmd = new MySqlCommand("insert into voting(ipaddr,vote,poll_id) values(@inp1,@inp2,@inp3)", con);
                    cmd.Parameters.AddWithValue("inp1", Request.UserHostAddress);
                    cmd.Parameters.AddWithValue("inp2", 1);
                    cmd.Parameters.AddWithValue("inp3", pid);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    pollopt.ClearSelection();
                    votediv.Style.Add("display", "none");
                    pollresult(pollquest.Text);
                }
            }

        }

        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "vote method of SearchPage_Master page for " + Session["loginname"] + ":" + ex.Message);
        }


    }
    protected void pollresult(string quest)
    {
        MySqlCommand cmdpollres;
        MySqlDataReader drpollres;
        try
        {
            progbar.Style.Add("display", "block");
            con.Open();
            if (quest == null)
            {
                cmdpollres = new MySqlCommand("select opt1,opt2,opt3,opt4,res1,res2,res3,res4,pollquest from poll order by rowid desc LIMIT 1", con);
            }
            else
            {
                cmdpollres = new MySqlCommand("select opt1,opt2,opt3,opt4,res1,res2,res3,res4,pollquest from poll where pollquest='" + quest + "'", con);
            }

            drpollres = cmdpollres.ExecuteReader();
            drpollres.Read();
            per1 = drpollres.GetInt32("res1").ToString() + "%";
            per2 = drpollres.GetInt32("res2").ToString() + "%";
            per3 = drpollres.GetInt32("res3").ToString() + "%";
            per4 = drpollres.GetInt32("res4").ToString() + "%";
            percent1.Style.Add("width", drpollres.GetInt32("res1") + "%");
            percent2.Style.Add("width", drpollres.GetInt32("res2") + "%");
            percent3.Style.Add("width", drpollres.GetInt32("res3") + "%");
            percent4.Style.Add("width", drpollres.GetInt32("res4") + "%");
            ans1 = drpollres.GetString("opt1");
            ans2 = drpollres.GetString("opt2");
            ans3 = drpollres.GetString("opt3");
            ans4 = drpollres.GetString("opt4");
            drpollres.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Poll Result method of SearchPage_Master page for " + Session["loginname"] + ":" + ex.Message);
        }
        finally
        {
            con.Close();

        }
    }

 
}
