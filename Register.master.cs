using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Security;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
public partial class Register : System.Web.UI.MasterPage
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand  cmdtopuser,cmdimg,cmdpoll,cmd,cmdtotcnt,cmd1,cmdloghist;
    MySqlDataReader druser,drimg,drpoll,drgetuser,drtotcnt,drguser,drvisit,drpass1,drpass,drpass2;
    public string  ans1,ans2,ans3,ans4,per1,per2,per3,per4,visitor,visitcnt;
    public int usercnt,topcnt,repcnt,getid,logent,entrypass;
    static string quest,imgpath="Alumni_Images/",imgpath1="images/",pid,user,url,cmnd;

    
    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (Page.IsPostBack)
        { Page.MaintainScrollPositionOnPostBack = true; }*/

        int temp = int.Parse(Application["counter"].ToString());

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

        try
        {
            // to retrieve number of alumni,topics,replies count
            con.Open();

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

            cmdtotcnt = new MySqlCommand("select login_name from login_hist where login_curr =(select max(login_curr) from login_hist where login_name not in ('admin','root'))", con);
            drtotcnt = cmdtotcnt.ExecuteReader();
            if (drtotcnt.Read())
            {
                visitor = string.IsNullOrEmpty(drtotcnt.GetString("login_name")) ? "-" : drtotcnt.GetString("login_name");
                drtotcnt.Close();               
                cmdtotcnt = new MySqlCommand("select regid from alumnireg where username='" + visitor + "'", con);
                drvisit = cmdtotcnt.ExecuteReader();
                if (drvisit.Read())
                {
                    visitlink.HRef = "LoginPage.aspx?urlid=pf&visitid=" + visitor + "&myid=" + drvisit.GetInt32("regid");
                }
                else
                {
                    visitlink.HRef = "#";
                }
                drvisit.Close();
            }
            else
            { visitor = "-"; visitlink.HRef = "#"; }

           
            if (!Page.IsPostBack)
            {
            cmdpoll = new MySqlCommand("select pollid,pollquest,opt1,opt2,opt3,opt4 from poll where activate='Yes' order by rowid desc LIMIT 1", con);
            drpoll = cmdpoll.ExecuteReader();
            if (drpoll.Read())
            {
                pid = drpoll["pollid"].ToString();
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
            
            //to display recent registrant photo and details
            cmdimg = new MySqlCommand("select imgpath,username,fname,lname,batch,branch from alumnireg order by rand() limit 1", con);
            drimg = cmdimg.ExecuteReader();

            if (drimg.Read())
            {
                if (drimg.IsDBNull(0) || File.Exists(Server.MapPath(drimg.GetString("imgpath"))) == false)
                {
                    recentimg.Src = "images/nophoto1.jpg";
                }
                else
                {
                    recentimg.Src = drimg.GetString("imgpath");
                }
                flname.Text = drimg.GetString("fname") + " " + drimg.GetString("lname");
                branbat.Text = string.IsNullOrEmpty(drimg.GetString("branch")) ? "" : drimg.GetString("branch") + " - " + drimg.GetInt32("batch");

            }
            else
            {
                flname.Text = branbat.Text = "";
            }
            drimg.Close();            
            
            // to display the list of recent registrants
            cmdtopuser = new MySqlCommand("select concat(fname,' ',lname),username,imgpath from alumnireg Order by rowid desc LIMIT 10", con);
            druser = cmdtopuser.ExecuteReader();
            if (druser.HasRows)
            {
                while (druser.Read())
                {
                    string st="";
                    if (druser.IsDBNull(2) || File.Exists(Server.MapPath(druser.GetString("imgpath"))) == false)
                    {
                        st = "images/nophoto.jpg";
                    }
                    else
                    {
                        st = druser.GetString("imgpath");
                    }
                    recreg.Controls.Add(new LiteralControl("<li>"
+ "<div>"
+ "<table cellspacing='2'>"
+ "<tr align='middle'>"
+ "<td><img width='35' height='35' src='"+st+"'/></td><td>" + druser.GetString(0) + "</td>"
+ "</tr>"
+ "</table>"
+ "</div>"
+ "</li>"));
                }
            }
            else
            {
                rcterr.Visible = true;
                rcterr.Text = "No Recent Registrants";
            }
            druser.Close();
       }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("Logs/Errorlog"),"Page_Load of Register master page:"+ ex.Message);
        }
        finally
        {
            con.Close();
        }   
 
    }

    protected void login_Click(object sender, EventArgs e)
    {
        string loginhist_name = "";

        try
        {
            user = loginid.Text;
            //opening the connection
            con.Open();

            //to get group_id and differentiate user login -> alumni / student / admin
            cmd1 = new MySqlCommand("select f.group_id from forum_users f where username='" + user + "' or f.email='" + user + "'", con);
            drpass1 = cmd1.ExecuteReader();
            if (!drpass1.Read())
            {
                login_err.InnerHtml = "Invalid Username/Password";
                login_err.Style["display"] = "block";
                loginbox_value.Value = "block";
                loginid.Text = "";
                loginpwd.Text = "";
                loginid.Focus();
            }
            else
            {
                getid = drpass1.GetInt32("group_id");
            }
            drpass1.Close();

            //this code gets executed when user is alumni only
            if (getid == 4)
            {
                //to check whether loginame is an usrname or email
                cmd = new MySqlCommand("select l.logentry,f.password,f.username from login l,forum_users f where (f.username='" + user + "' or f.email='" + user + "') and l.username=f.username and l.role='Alumni' and f.group_id=4 and f.password=password('" + loginpwd.Text.Trim() + "')", con);
                drpass = cmd.ExecuteReader();

                //if user not registered then error is displayed
                if (!drpass.Read())
                {

                    login_err.InnerHtml = "Invalid Username/Password";
                    login_err.Style["display"] = "block";
                    loginbox_value.Value = "block";
                    loginid.Text = "";
                    loginpwd.Text = "";
                    loginid.Focus();
                }
                else
                {//if login is successfull redirect to correct url
                    Session["loginname"] = loginhist_name = drpass.GetString("username");
                    entrypass = drpass.GetInt32("logentry");

                    string urlid = Request.QueryString["urlid"];
                    switch (urlid)
                    {
                        case "sh":
                            url = "Search Alumni/SearchAlumni.aspx";
                            break;
                        case "fm":
                            url = "http://ssnunite.com/PeopleTalk";
                            break;
                        case "jb":
                            url = "http://ssnunite.com/job";
                            break;
                        case "pf":
                            url = "AlumniPages/AlumniProfHome.aspx?vid=" + Request.QueryString["visitid"] + "&myid=" + Request.QueryString["myid"];
                            break;
                        default:
                            url = "AlumniPages/AlumniHome.aspx";
                            break;
                    }

                    drpass.Close();

                    //if alumni has permission to enter into site
                    if (entrypass == 2)
                    {
                        cmd1 = new MySqlCommand("select * from login_hist where login_name='" + loginhist_name + "'", con);
                        drpass2 = cmd1.ExecuteReader();
                        if (drpass2.HasRows)
                        {
                            //to update login history table for a user
                            cmnd = "update login_hist set login_prev=login_curr,login_curr=@logcur where login_name=@logname";

                        }
                        else
                        {
                            //to insert the login date and time for a particular user to maintain login history
                            cmnd = "insert into login_hist(login_name,login_curr) values(@logname,@logcur)";
                        }
                        drpass2.Close();

                        cmdloghist = new MySqlCommand(cmnd, con);
                        cmdloghist.Parameters.AddWithValue("@logname", loginhist_name);
                        cmdloghist.Parameters.AddWithValue("@logcur", DateTime.Now.Date + DateTime.Now.TimeOfDay);
                        cmdloghist.ExecuteNonQuery();

                        con.Close();

                        if (!string.IsNullOrEmpty(url))
                        {
                            Response.Redirect(url);
                        }
                    }
                    //if no validate by admin then redirect to invalid login page
                    else
                    { Response.Redirect("InvalidLogin.aspx?prevpage=" + Request.UrlReferrer); }
                    /*entrypass condition finishes here*/
                }
            }
            //this code gets executed when user is admin / power admin
            else if (getid == 1)
            {
                cmd = new MySqlCommand("select f.password from forum_users f where f.username='" + user + "' and f.group_id=1 and f.password=password('" + loginpwd.Text.Trim() + "')", con);
                drpass = cmd.ExecuteReader();

                //if user not registered then error is displayed
                if (!drpass.Read())
                {
                    login_err.InnerHtml = "Invalid Admin / Password";
                    login_err.Style["display"] = "block";
                    loginbox_value.Value = "block";
                    loginid.Text = "";
                    loginpwd.Text = "";
                    loginid.Focus();
                }
                else
                {

                    Session["loginname"] = user;
                    drpass.Close();
                    cmd = new MySqlCommand("select * from login_hist where login_name=@name", con);
                    cmd.Parameters.AddWithValue("@name", user);
                    drpass = cmd.ExecuteReader();
                    if (drpass.HasRows)
                    {
                        cmnd = "update login_hist set login_prev=login_curr,login_curr=@logcur where login_name=@logname";
                    }
                    else
                    {
                        //to insert the login date and time for a particular user to maintain login history
                        cmdloghist = new MySqlCommand("insert into login_hist(login_name,login_curr) values(@logname,@logcur)", con);

                    }
                    drpass.Close();
                    cmdloghist = new MySqlCommand(cmnd, con);
                    cmdloghist.Parameters.AddWithValue("@logname", user);
                    cmdloghist.Parameters.AddWithValue("@logcur", DateTime.Now.Date + DateTime.Now.TimeOfDay);
                    cmdloghist.ExecuteNonQuery();

                    Response.Redirect("Admin_Pages/Admin_index.aspx");
                }
            }

                //this code gets executed when user is guest
            else if (getid == 3)
            {
                login_err.InnerHtml = "Invalid Login. Guest Login not available";
                login_err.Style["display"] = "block";
                loginbox_value.Value = "block";
                loginid.Text = "";
                loginpwd.Text = "";
                loginid.Focus();
            }

            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Login method of Login Page:" + ex.Message);
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
              log.ErrorLog(Server.MapPath("Logs/Errorlog"), "vote method of HeaderFooter page:" + ex.Message);
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
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Poll Result method of HeaderFooter page:" + ex.Message);
        }
        finally
        {
            con.Close();

        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        

    }
}
