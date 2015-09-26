using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
public partial class RegisterSuccess : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdsent;
    MySqlDataReader dr;
    int ver_id;
    string mid = ConfigurationManager.AppSettings["mailid"], oldpath, newpath, mpwd = ConfigurationManager.AppSettings["mailpwd"]; 
    protected void Page_Load(object sender, EventArgs e)
    {
                
        if (!Page.IsPostBack)
        {
            ver_id = string.IsNullOrEmpty(Request.QueryString["tst_id"])?0:int.Parse(Request.QueryString["tst_id"]);

            try
            {
                //this code is to change the image path according to forum id for a user
                con.Open();
                cmdsent = new MySqlCommand("select id from forum_users where regid=" + ver_id, con);
                dr = cmdsent.ExecuteReader();
                dr.Read();
                oldpath = "Alumni_Images/" + ver_id + ".png";

                if (File.Exists(Server.MapPath(oldpath)))
                {
                    newpath = "Alumni_Images/" + dr.GetInt32("id") + ".png";
                    File.Copy(Server.MapPath(oldpath), Server.MapPath(newpath));
                    File.Delete(Server.MapPath(oldpath));
                }
                else
                {
                    newpath = "";
                }
                dr.Close();
                cmdsent = new MySqlCommand("update alumnireg set imgpath='" + newpath + "' where regid=" + ver_id, con);
                cmdsent.ExecuteNonQuery();
                con.Close();

                if (ver_id != 0)
                {
                    verify_details();
                }
            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Page Load method of RegisterSuccess page for " +ver_id+":"+ ex.Message);
            }
        }
    }
    protected void verify_details()
    {
        MySqlCommand cmdmail, cmdsent;
        MySqlDataReader drmail,dr;
        MailMessage mail = new MailMessage();
        string msgbody, encodeuser,status="";
        ArrayList sentlist = new ArrayList();
       int chk;
       
        try
        {
            con.Open();

            //this code is to retrieve all the email's of valid alumni's and send them mail
            cmdmail = new MySqlCommand("select new.auth_code,new.email,f.username,f.password from alumnireg new,oldalumnireg as oreg,forum_users f  where new.fname=lower(oreg.old_fname) and new.dob=oreg.old_dob and new.mailsent=0 and new.username=f.username and new.regid="+ver_id, con);
            
            drmail = cmdmail.ExecuteReader();

            if (drmail.Read())
            {
                status = "Success";
                mail.From = new MailAddress(mid, "Alumni Admin");
                mail.To.Add(drmail.GetString("email"));
                mail.Subject = "Registration Successfully Completed";
                encodeuser = encodepwd(drmail.GetString("username"));

                msgbody = "Dear Alumni," + "<br/><br/>You have registered successfully on the SSN Alumni site.<br/>";
                msgbody += "<a href='http://ssnunite.com/VerifyMail.aspx?aucode=" + drmail.GetString("auth_code") + "&uid=" + encodeuser + "' target='_blank'>Click this link to Verify your Email-Id </a>" + "<br/><br/>Thank you for registering. ";
                mail.Body = msgbody;
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
                }
                catch (SmtpException se)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "Message('Mail has not been sent to you due to the following problems:<br/>1.Server Problem.<br/>2.Your Register Id may not exist in our database.<br/>3.The Connection to the SMTP has expired.<br/><br/>Try refreshing the page again or contact the admin immediately.');", true);
                }
            }
            else
            {
                drmail.Close();

                //the below three queries will set incorrect value for a registered user against firstname,lastname,dob
                cmdsent = new MySqlCommand("select count(*) from oldalumnireg as old,alumnireg as nreg where lower(old.old_fname)=lower(nreg.fname) and nreg.regid=" + ver_id, con);
                chk = Convert.ToInt32(cmdsent.ExecuteScalar());
                if (chk > 0)
                { status = ""; }
                else
                {
                    if (status != "")
                    { status += ","; }
                    status = "First Name";
                }


                cmdsent = new MySqlCommand("select count(*) from oldalumnireg as old,alumnireg as nreg where old.old_dob=nreg.dob and nreg.regid=" + ver_id, con);
                chk = Convert.ToInt32(cmdsent.ExecuteScalar());
                if (chk > 0)
                { status += ""; }
                else
                {
                    if (status != "")
                    { status += ","; }
                    status += "DOB";
                }

                cmdsent = new MySqlCommand("select count(*) from oldalumnireg as old,alumnireg as nreg where lower(old.old_lname)=lower(nreg.lname) and nreg.regid=" + ver_id, con);
                chk = Convert.ToInt32(cmdsent.ExecuteScalar());
                if (chk > 0)
                { status += ""; }
                else
                {
                    if (status != "")
                    { status += ","; } status += "Last Name";
                }

                cmdsent = new MySqlCommand("select username,email from alumnireg where regid=" + ver_id, con);
                dr = cmdsent.ExecuteReader();
                dr.Read();

                mail.From = new MailAddress(mid, "Alumni Admin");
                mail.To.Add(dr.GetString("email"));
                mail.Subject = "Registration Process Incomplete";

                msgbody = "Dear Alumni," + "<br/><br/>You have registered successfully on the SSN Alumni site.";
                msgbody += "<br/>But it seems your known to be an invalid alumnus.Please verify your registered details with the alumni officer to gain access to the site.<br/><br/>Your Registration Id is :" + ver_id + ".<br/><br/>Please provide your registration id,registration date,fname,lname,dob,email-Id,batch,branch to track you details easily.<br/>";
                msgbody += "<br/><br/>Thank you for registering. ";

                mail.Body = msgbody;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(mail);
                }
                catch (SmtpException se)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "Message('Mail has not been sent to you due to the following problems:<br/>1.Server Problem.<br/>2.Your Register Id may not exist in our database.<br/>3.The Connection to the SMTP has expired.<br/><br/>Try refreshing the page again or contact the admin immediately.');", true);
                }
                dr.Close();

                status = string.IsNullOrEmpty(status) ? "No Details Available" : status;
            }
        
        drmail.Close();

            cmdsent = new MySqlCommand("update alumnireg set incorrect='"+status+"' where regid=" + ver_id, con);
        cmdsent.ExecuteNonQuery();
        con.Close();

       //to redirect to same page to avoid sending mail again to same username because of server postback incase of page refresh
            Response.Redirect("RegisterSuccess.aspx");
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Verify Details Method of Register Success page: " + ex.Message);
         
        }
       
    }

    //encryption function that returns  encrypted username to send over net
    protected string encodepwd(string enpas)
    {
        try
        {
            byte[] encData_byte = new byte[enpas.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(enpas);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;

        }
        catch (Exception ex)
        {
            throw new Exception("Error in base64Encode" + ex.Message);
        }
    }
}