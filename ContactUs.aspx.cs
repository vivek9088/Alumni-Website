using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text.RegularExpressions;
public partial class ContactUs : System.Web.UI.Page
{
    string mid = System.Configuration.ConfigurationManager.AppSettings["mailid"],mpwd=System.Configuration.ConfigurationManager.AppSettings["mailpwd"];
    public string nameerr, batcherr, emailerr, bodyerr, suberr, succ;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            name.Focus();
        }
    }
    
    protected void subject_Init(object sender, EventArgs e)
    {
        subject.Items.Add("--Please Select a Subject--");
        subject.Items.Add("General Enquiries");
        subject.Items.Add("Membership Registration");
        subject.Items.Add("Alumni Stories");
        subject.Items.Add("Request for New Chapter");

    }
    protected void SendMail_Click(object sender, EventArgs e)
    {
        MailMessage mail = new MailMessage();
        
        
        if (string.IsNullOrEmpty(name.Text))
        { nameerr = "<font color='red'>Please enter a name</font>"; name.Text = ""; name.Focus(); }
        else if (subject.SelectedIndex == 0)
        {
            suberr = "<font color='red'>Please Select a Subject</font>";
        }
        else         if (string.IsNullOrEmpty(msg.Value))
        {
            suberr = ""; 
            bodyerr = "<font color='red'>Please enter a message</font>"; msg.Focus(); 
        }
        else    if (string.IsNullOrEmpty(email.Text))
            {
            bodyerr = "";
                emailerr = "<font color='red'>Please enter an Email Id</font>";
                email.Text = "";
                email.Focus();
        }
        else
        {
                        
             //this is used to retrieve the from address in webconfig file in appsettings
            // mail.From=new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["fromaddress"]);
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                   + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                   + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                   + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                   + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            bool flag=false;

            flag = reStrict.IsMatch(email.Text);
            emailerr = "";
                if (!flag)
                {
                    emailerr = "<font color='red'>Email Id is Wrong.</font>";
                    email.Text = "";
                    email.Focus();
                }
                else
                {
                    mail.From = new MailAddress(email.Text.Trim(), name.Text);
                    emailerr = "";
            
            mail.To.Add("alumniofficer@ssn.edu.in");
            mail.ReplyToList.Add(new MailAddress(email.Text.Trim()));
            mail.Subject = subject.SelectedItem.Value;
            mail.Body = "Respected Sir/Madam,"+"<br/><br/>I am "+name.Text+"and i am alumni of this college.<br/><br/>"+msg.Value.Trim()+"<br/><br/>Thanking you,<br/><br/>Yours Faithfully,<br/>"+name.Text;
            mail.IsBodyHtml = true;

                    try
                    {
            if (!Page.IsPostBack)
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            succ = "<font color='black'>Mail Successfully Sent</font>";
                }
catch(SmtpException se)
{
   Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "Mail has not been sent to you due to the following problems:<br/>1.Server Problem.<br/>2.Your Register Id may not exist in our database.<br/>3.The Connection to the SMTP has expired.<br/><br/>Try refreshing the page again or contact the admin immediately.", true);
   CreateLogFile logfile = new CreateLogFile();
    logfile.ErrorLog(Server.MapPath("Logs/Errorlog"),"Send Mail method of ContactUs page for "+Session["loginname"]+":"+se.Message);
}
                }
            }
         
         }
    protected void name_TextChanged(object sender, EventArgs e)
    {
        nameerr = "";
    }
    protected void email_TextChanged(object sender, EventArgs e)
    {
        emailerr = "";
    }
    protected void subject_SelectedIndexChanged(object sender, EventArgs e)
    {
        suberr = "";
    }
    protected void batch_SelectedIndexChanged(object sender, EventArgs e)
    {
        batcherr = "";
    }
}