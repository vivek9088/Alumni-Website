using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
public partial class AlumniPages_Email : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdmail;
    MySqlDataReader drmail;
    static string loginuser,mailfrom, maildispname;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            msgto.Focus();
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
                cmdmail = new MySqlCommand("select email,fname,lname,batch from alumnireg where username='" + loginuser + "'", con);
                drmail = cmdmail.ExecuteReader();
                drmail.Read();
                maildispname = drmail.GetString("fname");
                if (drmail.GetString("lname").Length > 0)
                {
                    maildispname += " " + drmail.GetString("lname");
                }
                maildispname += " [" + drmail.GetInt32("batch").ToString() + "]";
                mailfrom = drmail.GetString("email");
                drmail.Close();
            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page load of Email Page for " + Session["loginname"] + ":" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void SendMail_Click(object sender, EventArgs e)
    {
        string path = "", mid = ConfigurationManager.AppSettings["mailid"], mpwd = ConfigurationManager.AppSettings["mailpwd"];
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(mailfrom, maildispname+"- SSN Alumni");
        mail.ReplyToList.Add(new MailAddress(mailfrom));
       
        // to add multiple To email id's 
        if (msgto.Text.Contains(";"))
        {
            string[] str = msgto.Text.Split(';');
            for (int i = 0; i < str.Length; i++)
                if (str[i] != String.Empty)
                    mail.To.Add(str[i]);
        }
        else
        {
            mail.To.Add(msgto.Text.Trim());
        }

        // mail subject
        mail.Subject = msgsub.Text.Trim();

        // to add multiple BCC email id's    
        if (msgbcc.Text != "")
        {
            if (msgbcc.Text.Contains(";"))
            {
                string[] str = msgbcc.Text.Split(';');
                for (int i = 0; i < str.Length; i++)
                    if (str[i] != String.Empty)
                        mail.Bcc.Add(str[i]);
            }
            else
            {
                mail.Bcc.Add(msgbcc.Text.Trim());
            }
        }

        // to add multiple CC email id's    
        if (msgcc.Text != "")
        {
            if (msgcc.Text.Contains(";"))
            {
                string[] str1 = msgcc.Text.Split(';');
                for (int i = 0; i < str1.Length; i++)
                    if (str1[i] != String.Empty)
                        mail.CC.Add(str1[i]);
            }
            else
            {
                mail.CC.Add(msgcc.Text.Trim());
            }
        }

        mail.IsBodyHtml = true;
        // mail body     
        if (msgbody.Text != "")
        {
            mail.Body = "Hai," + "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + msgbody.Text.Trim();
                
        }
        //file attachments to mail
        HttpFileCollection uploads = HttpContext.Current.Request.Files;
        if (uploads.Count > 0)
        {
            for (int i = 0; i < uploads.Count; i++)
            {
                HttpPostedFile upload = uploads[i];
                if (uploads[i].ContentLength > 0)
                {
                    string c = Path.GetFileName(upload.FileName);
                    path = Server.MapPath("MailUploadedFiles/") + c;
                    upload.SaveAs(path);
                    mail.Attachments.Add(new Attachment(path));
                }
               
            }
        }
        //  mail.IsBodyHtml = true;
        try
        {
            SmtpClient smtp = new SmtpClient();
            if (!Page.IsPostBack)
            {
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            Response.Redirect("AlumniProfHome.aspx");
        }
        catch (Exception sx)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Send Mail of Email Page for " + Session["loginname"] + ":" + sx.Message);
           
        }
    }

    protected string[] GetRecords(string prefixText)
    {
        MySqlConnection con1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
        MySqlDataAdapter cmd1 = new MySqlDataAdapter("select email from alumnireg where email like '" + prefixText + "%'", con1);
        DataTable dt = new DataTable();
        cmd1.Fill(dt);
        string[] items = new string[dt.Rows.Count];

        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr[0].ToString(), i);
            i++;
        }
        con1.Close();
        return items; 
       
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        AlumniPages_Email em = new AlumniPages_Email();
        string[] list= em.GetRecords(prefixText);
        return list;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        AlumniPages_Email em = new AlumniPages_Email();
        string[] list1 = em.GetRecords(prefixText);
        return list1;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        AlumniPages_Email em = new AlumniPages_Email();       
        string[] list2 = em.GetRecords(prefixText);
        return list2;
    }

}