using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using MySql.Data.MySqlClient;
using System.Configuration;
public partial class AlumniPages_RequestChapter : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdmail;
    MySqlDataReader drmail;
    static string loginuser, maildispname;
     protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            chapname.Focus();
        }
        if (Session["loginname"] == null)
        {
            reurl.Value = "3";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Your Current Session Expired..Please Login again!!!');</script>", true);
        }
        else
        {

            try
            {
                loginuser = Session["loginname"].ToString();
                con.Open();
                cmdmail = new MySqlCommand("select fname,lname from alumnireg where username='" + loginuser + "'", con);
                drmail = cmdmail.ExecuteReader();
                drmail.Read();
                maildispname = drmail.GetString("fname");
                if (drmail.GetString("lname").Length > 0)
                {
                    maildispname += " " + drmail.GetString("lname");
                }
                drmail.Close();
            }

            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page load of Request Chapter Page for " + Session["loginname"] + ":" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
      
    }
    protected void chapbatch_Init(object sender, EventArgs e)
    {
        chapbatch.Items.Add("-- Select a Batch --");
        for (int yr = 1998; yr <= System.DateTime.Now.Year; yr++)
        {
            chapbatch.Items.Add(yr.ToString());
        }
    }
    protected void chapsubmit_Click(object sender, ImageClickEventArgs e)
    {
        string mid = ConfigurationManager.AppSettings["mailid"], mpwd = ConfigurationManager.AppSettings["mailpwd"];
        MailMessage mail = new MailMessage();
        //this is used to retrieve the from address in webconfig file in appsettings
        // mail.From=new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["fromaddress"]);

        mail.From = new MailAddress(chapemail.Text,maildispname+" - SSN Alumni");
        mail.ReplyToList.Add(new MailAddress(chapemail.Text));
        mail.To.Add("alumniofficer@ssn.edu.in");
       
        // mail subject
        mail.Subject = "Request for Starting New Chapter";

        // mail body     
        if (chapmsg.Value != "")
        {
            mail.Body = "Respected Sir/Madam," + "<br/>" + "<p style='padding-left:120px;'>We want to create a new chapter named '" + chapname.Text + " Chapter' at location " + chaploc.Text + ".We belong to the batch "+chapbatch.SelectedItem.Value+".</p><p>"+chapmsg.Value+"</p><p>The contact details are as follows:-<br/><br/>Email-Id  : "+chapemail.Text+"<br/><br/>Contact Number  :"+chapnum.Text+"</p> "; 
        }
        else
        { mail.Body = ""; }

        //  mail.IsBodyHtml = true;
        if (!Page.IsPostBack)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
         Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Request for starting a new chapter has been sent successfully. You may receive a mail or call from alumni offcier for verification and enquiries. Thank you !!!');</script>", true);

          clearboxes();
        mail.Dispose();
    }
    protected void clearboxes()
    {
        chapemail.Text = "";
        chapbatch.SelectedIndex = -1;
        chaploc.Text = "";
        chapmsg.Value = "";
        chapname.Text = "";
        chapnum.Text = "";
    }
}