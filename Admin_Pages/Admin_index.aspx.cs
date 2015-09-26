using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class Alumni_Officer_Admin_index : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmduser;
    MySqlDataReader druser;
            
    protected void Page_Load(object sender, EventArgs e)
    {
       try
        {
            con.Open();
            cmduser = new MySqlCommand("select login_prev,login_curr from login_hist where login_name='" + Session["loginname"].ToString()+"'" , con);
            druser = cmduser.ExecuteReader();
            druser.Read();
            if (druser.IsDBNull(0))
            {
                logdate.Text = druser.GetDateTime("login_curr").ToShortDateString();
                logtime.Text = druser.GetDateTime("login_curr").ToShortTimeString();
            }
            else
            {
                logdate.Text = druser.GetDateTime("login_prev").ToShortDateString();
                logtime.Text = druser.GetDateTime("login_prev").ToShortTimeString();
            }
            
            druser.Close();
                     
       }
        catch (Exception ex)
        {
            CreateLogFile log1 = new CreateLogFile();
            log1.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load method of Admin_Index for " + Session["loginname"] + ":" + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }
    
}
