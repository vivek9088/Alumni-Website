using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using MySql.Data.MySqlClient;
public partial class ForgotPassword : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdgetpass,cmd,cmd1;
    MySqlDataReader drgetpass,dr;
    static string temp,msg,mid=ConfigurationManager.AppSettings["mailid"],mpwd=ConfigurationManager.AppSettings["mailpwd"];
    int a, b, c, d;
    Random rnd = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            emailid.Focus();
        }
    }
    
    protected void Submit_Click(object sender, EventArgs e)
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        int check;
        string strNewPassword = GeneratePassword();  //This is where you'd call the new password string.

        if (string.IsNullOrEmpty(emailid.Text))
        {
            emailerr.Text = "Please enter an Email Id";
            alertdiv.Style.Add("display", "block");
            emailid.Text = "";
            emailid.Focus();
        }
        else
        {
            alertdiv.Style.Add("display", "none");
            try
            {
                con.Open();


                //updating password for the given mailid in forum_users table
                cmd = new MySqlCommand("update forum_users set password=password('" + strNewPassword + "') where email='" + emailid.Text.Trim() + "'", con);
                int num = cmd.ExecuteNonQuery();

                if (num == 0)
                {
                    alertdiv.Style.Add("display", "block");
                    emailerr.Text = "Not a Registered Mail-Id. Please enter a Registered Mail-Id"; emailid.Focus();
                }
                else
                {
                    alertdiv.Style.Add("display", "none");
                    emailerr.Text = "";

                    //validating emailid access to the site
                    cmd1 = new MySqlCommand("select l.username,l.logentry from login l,forum_users f where l.emailid='" + emailid.Text.Trim() + "' and f.email='" + emailid.Text.Trim() + "'", con);
                    dr = cmd1.ExecuteReader();
                    dr.Read();
                    check = dr.GetInt32("logentry");
                    if (check == 1)
                    {
                        msg = "<h1>The following email was sent to you by Alumni Administrator.</h1><br />";
                        msg += "<p>Apparently, you don't have the protocol to login into the site. <br />";
                        msg += "<br/>Your still not verified as <b>Valid Alumni</b> by Site Admin.<br/>";
                        msg += "Please contact Alumni Officer (alumniofficer@ssn.edu.in) for any queries or problems in acessing the site.</p><br/>";
                        msg += "<br/>Please do not reply to this mail.<br/><br/> Thank you!";
                    }
                    else
                    {
                        msg = "<h1>The following email was sent to you by Alumni Administrator.</h1><br />";
                        msg += "<p>Apparently, you needed your password reset - So here it is: <br />";
                        msg += "<br/>Username     :<b>" + dr.GetString("username") + "</b></p>";
                        msg += "New Password : <b>" + strNewPassword + "</b></p><br />";
                        msg += "<br/><a href='http://ssnunite.com/LoginPage.aspx'>Click here to login and verify your new password</a><br/>";
                        msg += "<br/>Please do not reply to this mail.Any queries please write to alumniofficer@ssn.edu.in.<br/><br/> Thank you!";

                    }
                    con.Close();

                    mail.From = new MailAddress(mid, "noreply");
                    mail.To.Add(emailid.Text.Trim());
                    mail.Subject = "SSN Alumni - Forgot Password";

                    mail.IsBodyHtml = true;
                    mail.Body = msg;
                    try
                    {
                        if (!Page.IsPostBack)
                        {
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 25;
                            smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                        alertdiv.Style.Add("display", "block");
                        emailerr.Text = "Password has been sent to your mail. Please check it. Thank you.";
                    }
                    catch (SmtpException se)
                    {
                        alertdiv.Style.Add("display", "block");
                        emailerr.Text = "<b>Mail has not been sent to you.One of the following errors may have occured:-</b><br/>1.The connection to the SMTP server failed.<br/>2.The operation timed out.<br/>3.The message could not be delivered to one or more of the recipients in MailMessage.To, MailMessage.CC, or MailMessage.Bcc.";
                    }
                }
            }

            catch (Exception ex)
            {
                CreateLogFile errlog = new CreateLogFile();
                errlog.ErrorLog(Server.MapPath("Logs/Errorlog"), "Submit method of ForgotPassword Page :" + ex.Message);
                //alertdiv.Style.Add("display", "block");
                //emailerr.Text = ex.Message;
            }
        }
        }
    public string GeneratePassword()
    {
        //string PasswordLength = "10";
        string NewPassword = "";

        //This one tells you which characters are allowed in this new password
        string allowedChars = "";
        allowedChars = "1,2,3,4,5,6,7,8,9,0";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "~,!,@,#,$,%,^,&,*,+,?";

        //char[] sep = { ',' };
        string[] arr = allowedChars.Split(',');
        string IDString = "";
        string temp = "";

        //utilize the "random" class
        Random rand = new Random();

        //and lastly - loop through the generation process...
       // for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)

            for (int i = 0; i <10; i++)
            {
            temp = arr[rand.Next(0, arr.Length)];
            IDString += temp;
            NewPassword = IDString;
        }
        return NewPassword;
    }
}