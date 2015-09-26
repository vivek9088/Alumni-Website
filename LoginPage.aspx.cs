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
using System.Text.RegularExpressions;

public partial class LoginPage : System.Web.UI.Page
{
     MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
     MySqlCommand   cmd, cmdlogentry, cmdloghist,cmd1;
     MySqlDataReader  drgetuser, drpass,drpass1,drpass2, drlogentry;
     static string user, user1, pass, url,cmnd;
     int logent,entrypass,getid;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loginid.Focus();
        }
        if (Request.QueryString.Count > 0)
        {
            logmsg.Text = "Please Login with your Credentials to access the requested Page. Thank You!";
        }
        else { logmsg.Text = ""; }

    }
    protected void Loginbutton_Click(object sender, EventArgs e)
    {
        string loginhist_name = "";
        
       try
       {
            user = loginid.Text;
            //opening the connection
            con.Open();
            
            //to get group_id and differentiate user login -> alumni / student / admin
            cmd1 = new MySqlCommand("select f.group_id from forum_users f where username='"+user+"' or f.email='"+user+"'",con);
            drpass1 = cmd1.ExecuteReader();
            if (!drpass1.Read())
            {
                loginerr.Text = "<font color='red'>Invalid User/Password..Please Sign Up</font>";
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
            if (getid==4)
            {
                //to check whether loginame is an usrname or email
                cmd = new MySqlCommand("select l.logentry,f.password,f.username from login l,forum_users f where (f.username='" + user + "' or f.email='" + user + "') and l.username=f.username and l.role='Alumni' and f.group_id=4 and f.password=password('" + loginpwd.Text.Trim() + "')", con);
                
                drpass = cmd.ExecuteReader();

                //if user not registered then error is displayed
                if (!drpass.Read())
                {
                    loginerr.Text = "<font color='red'>Invalid User/Password..Please Sign Up</font>";
                    loginid.Text = "";
                    loginpwd.Text = "";
                    loginid.Focus();
                }
                else
                {//if login is successfull redirect to correct url
                    loginerr.Text = "";
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
                        cmd1 = new MySqlCommand("select * from login_hist where login_name='" + loginhist_name + "'",con);
                        drpass2 = cmd1.ExecuteReader();
                        if (drpass2.HasRows)
                        {
                            //to update login history table for a user
                        cmnd="update login_hist set login_prev=login_curr,login_curr=@logcur where login_name=@logname";
                        
                        }
                        else
                        {
                            //to insert the login date and time for a particular user to maintain login history
                            cmnd="insert into login_hist(login_name,login_curr) values(@logname,@logcur)";
                        }
                        drpass2.Close();
                        
                        cmdloghist=new MySqlCommand(cmnd,con);
                        cmdloghist.Parameters.AddWithValue("@logname", loginhist_name);
                        cmdloghist.Parameters.AddWithValue("@logcur",DateTime.Now.Date+ DateTime.Now.TimeOfDay);
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
                    loginerr.Text = "<font color='red'>Invalid Admin / Password.</font>";
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
                    cmdloghist.Parameters.AddWithValue("@logname",user);
                    cmdloghist.Parameters.AddWithValue("@logcur", DateTime.Now.Date + DateTime.Now.TimeOfDay);
                    cmdloghist.ExecuteNonQuery();
                      
                    Response.Redirect("Admin_Pages/Admin_index.aspx");
                }
            }

                //this code gets executed when user is guest
            else if (getid == 3)
            {
                loginerr.Text = "<font color='red'>Invalid Login. Guest Login not available.</font>";
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
}