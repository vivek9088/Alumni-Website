using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
public partial class Student_Pages_Stud_Index : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand  cmduser;
    MySqlDataReader druser;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        string name;
        try
        {
            name=Session["loginname"].ToString();
            con.Open();
            cmduser = new MySqlCommand("select login_date,login_time from login_hist where rowid=(select max(rowid)-1 from login_hist where login_name='" + name + "')", con);
            druser = cmduser.ExecuteReader();
            if (druser.Read())
            {
                logdate.Text = druser.GetDateTime("login_date").ToShortDateString();
                logtime.Text = druser.GetString("login_time");
            }
            else
            {
                logdate.Text = "0000-00-00";
                logtime.Text = "00:00:00";
            }
            druser.Close();

            //to count the number of student registered 
            cmduser = new MySqlCommand("select count(username) from forum_users where branch=(select branch from forum_users where username='"+name+"'))",con);
            druser = cmduser.ExecuteReader();
            druser.Read();
            studcnt.Text = string.IsNullOrEmpty(druser.GetInt32(0).ToString()) ? "0" : druser.GetInt32(0).ToString();
            druser.Close();

            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load of HeaderFooter Page for " + Session["loginname"] + ":" + ex.Message);
     
        }
    }
}