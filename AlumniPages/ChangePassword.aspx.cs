using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
public partial class AlumniPages_ChangePassword : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    
    MySqlCommand cmd,cmd1;
    string username;
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Session["loginname"] == null)
        {
            reurl.Value = "3";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Your Current Session Expired..Please Login again!!!');</script>", true);
        }
            else
            {
           username = Session["loginname"].ToString();
        }
        if (!Page.IsPostBack)
        {
            newpass.Focus();
        }
    }
    protected void changePassword_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            cmd = new MySqlCommand("update forum_users set password=password(@pass) where username='" + username + "'", con);
            cmd.Parameters.AddWithValue("pass", confpass.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Password has been changed successfully.');</script>", true);
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Change Password method of Changepassword page for " + Session["loginname"] + ":"+ex.Message);
        }
    }
}