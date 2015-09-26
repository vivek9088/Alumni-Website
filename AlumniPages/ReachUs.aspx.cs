using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
public partial class AlumniPages_ReachUs : System.Web.UI.Page
{
    public string nameerr, emailerr, bodyerr, suberr;
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
      string mid = System.Configuration.ConfigurationManager.AppSettings["mailid"],mpwd;
      mpwd = System.Configuration.ConfigurationManager.AppSettings["mailpwd"];
        if (string.IsNullOrEmpty(name.Text))
        { nameerr = "<font color='red'>Please enter a name</font>"; name.Text = ""; name.Focus(); }
        else if (subject.SelectedIndex == 0)
        {
            suberr = "<font color='red'>Please Select a Subject</font>";
        }
        else if (string.IsNullOrEmpty(email.Text))
        {
            suberr = "";  
            emailerr = "<font color='red'>Please enter an Email Id</font>";
            email.Text = "";
            email.Focus();
        }
     
        else if (string.IsNullOrEmpty(msg.Value))
        {
            emailerr = "";
            bodyerr = "<font color='red'>Please enter a message</font>"; msg.Focus(); 
        }
        else
        {
            bodyerr = "";            
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
            
                if (!flag)
                {
                    emailerr = "<font color='red'>Email Id is Wrong.</font>";
                    email.Focus();
                }
                else
                {
            emailerr = "";
            mail.From = new MailAddress(email.Text.Trim(), name.Text);
            mail.To.Add("alumniofficer@ssn.edu.in");
            mail.ReplyToList.Add(new MailAddress(email.Text.Trim()));
            mail.Subject = subject.SelectedItem.Value;
            mail.Body = "Respected Sir/Madam,"+"<br/><br/>I am "+name.Text+" and alumni of the college.<br/>"+msg.Value.Trim()+"<br/><br/>Thanking you,<br/><br/>Yours Faithfully,<br/>"+name.Text;
            mail.IsBodyHtml = true;

            if (!Page.IsPostBack)
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Your Mail has been successfully sent.');</script>", true);
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
   
    
}