using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;

public partial class AlumniPages_AlumniProfHome : System.Web.UI.Page
{
    
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdforum, cmdprof,cmdnews,cmdfri,cmd;
    MySqlDataReader drforum, drprof,drnews,dr;
    static int fromid,toid,checkval;
    static string userdet,imgpath="../Alumni_Images/",urlpath,mid,msg_to,mpwd;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        //imgadd.Style.Add("margin-top", "15px");
        //sndmsg.Style.Add("margin-top", "15px");
        mid = ConfigurationManager.AppSettings["mailid"];
        mpwd = ConfigurationManager.AppSettings["mailpwd"];
        try
        {
            con.Open();
        if (Request.QueryString.Count == 0)
        {
                userdet = Session["loginname"].ToString();
                imgadd.Visible = false;
                sndmsg.Visible = false;
                
       }
        else
        {
            toid = Convert.ToInt32(Request.QueryString["myid"]);
            fromid = Convert.ToInt32(Request.QueryString["toid"]);
            userdet =decodepwd(Request.QueryString["vid"]);

            cmd = new MySqlCommand("select accept from user_friends where ((req_from_id=" + fromid + " and req_to_id=" + toid + ") or (req_from_id=" + toid + " and req_to_id=" + fromid + ")) and accept=1", con);
            dr = cmd.ExecuteReader();
            checkval =(dr.Read())? 1:0;
            
                if (checkval == 0)
                {
                    imgadd.Visible = true;
                    sndmsg.Visible = true;
                    show_spaces.Visible = true;
                }
                else
                {
                    friimg.Visible = true;
                    closefri.Visible = true;
                    sndmsg.Visible = true;
                    show_spaces.Visible = true;
                }
                dr.Close();

            profhead.Text = userdet+"'s Profile";
            profimg.Visible = true;
            if (File.Exists(Server.MapPath(imgpath + Request.QueryString["myid"] + ".png")))
            {
                profimg.Src = imgpath + Request.QueryString["myid"] + ".png";
            }
            
        }
            
            cmdprof = new MySqlCommand("select * from alumnireg where username='"+userdet+"'",con);
            drprof = cmdprof.ExecuteReader();
            drprof.Read();
            
            alu_role.Text = drprof.GetString("role");
            alu_fname.Text = drprof.GetString("fname");
            alu_lname.Text = drprof.GetString("lname");
            alu_gender.Text = drprof.GetString("gender");
            alu_email.Text = drprof.GetString("email");

            DateTime dt = Convert.ToDateTime(drprof.GetString("dob"));
            alu_dob.Text = dt.ToString("MMM, d");
            
            alu_degree.Text = drprof.GetString("degree");
            alu_branch.Text = drprof.GetString("branch");
            alu_batch.Text = drprof.GetInt32("batch").ToString();
            alu_country.Text = drprof.GetString("country");
            alu_state.Text = drprof.GetString("state");
            alu_city.Text = drprof.GetString("city");
            
            alu_number.Text =drprof.GetString("visibility").Substring(0,1)=="1"? drprof.GetString("number"):"-";
            alu_addr.Text =drprof.GetString("visibility").Substring(1,1)=="1"?string.IsNullOrEmpty(drprof.GetString("address"))?"-":drprof.GetString("address"):"-";
            

            other_hobby.Text =string.IsNullOrEmpty(drprof["hobby"].ToString()) ? "-" : drprof.GetString("hobby");
            other_moment.Text = string.IsNullOrEmpty(drprof["ssnmoment"].ToString()) ? "-" : drprof.GetString("ssnmoment");
            other_movie.Text = string.IsNullOrEmpty(drprof["movies"].ToString()) ? "-" : drprof.GetString("movies");
            other_music.Text = string.IsNullOrEmpty(drprof["music"].ToString()) ? "-" : drprof.GetString("music");
            other_myself.Text = string.IsNullOrEmpty(drprof["aboutme"].ToString()) ? "-" : drprof.GetString("aboutme");
            
            string[] instname = string.IsNullOrEmpty(drprof["inst_name"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("inst_name").Split('/');
            string[] instloc =string.IsNullOrEmpty(drprof["inst_loc"].ToString())?new string[]{"-","-","-","-"}: drprof.GetString("inst_loc").Split('/');
            string[] instcourse = string.IsNullOrEmpty(drprof["inst_course"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("inst_course").Split('/');
            string[] instyr = string.IsNullOrEmpty(drprof["inst_passyr"].ToString())?new string[]{"-","-","-","-"}: drprof.GetString("inst_passyr").Split('/');
            
            if (instname[0] == "-" && instloc[0]=="-" && instcourse[0]=="-" && instyr[0]=="-")
            {
                edu1.Visible = false;
                norecord.Visible = true;
            }
            else
            {
                norecord.Visible = false;
                edu1.Visible = true;
                instname1.Text = instname[0];
                instloc1.Text = instloc[0];
                instcourse1.Text = instcourse[0];
                instyr1.Text = instyr[0];
            }

            if (instname[1] == "-" && instloc[1] == "-" && instcourse[1] == "-" && instyr[1] == "-")
            {
                edu2.Visible = false;
                norecord.Visible = true;
            }
            else
            {
                norecord.Visible = false;
                edu2.Visible = true;
                instname2.Text = instname[1];
                instloc2.Text = instloc[1];
                instcourse2.Text = instcourse[1];
                instyr2.Text = instyr[1];
            }

            if (instname[2] == "-" && instloc[2] == "-" && instcourse[2] == "-" && instyr[2] == "-")
            {
                edu3.Visible = false;
                norecord.Visible = true;
            }
            else
            {
                norecord.Visible = false;
                edu3.Visible = true;
                instname3.Text = instname[2];
                instloc3.Text = instloc[2];
                instcourse3.Text = instcourse[2];
                instyr3.Text = instyr[2];
            }

            if (instname[3] == "-" && instloc[3] == "-" && instcourse[3] == "-" && instyr[3] == "-")
            {
                edu4.Visible = false;
                norecord.Visible = true;
            }
            else
            {
                norecord.Visible = false;
                edu4.Visible = true;
                instname4.Text = instname[3];
                instloc4.Text = instloc[3];
                instcourse4.Text = instcourse[3];
                instyr4.Text = instyr[3];
            }

            string[] cmname =string.IsNullOrEmpty(drprof["org"].ToString())?new string[]{"-","-","-","-"}: drprof.GetString("org").Split('/');
            string[] cmdesig = string.IsNullOrEmpty(drprof["desig"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("desig").Split('/');
            string[] cmloc = string.IsNullOrEmpty(drprof["cmploc"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("cmploc").Split('/');
            string[] cmyrs = string.IsNullOrEmpty(drprof["expyrs"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("expyrs").Split('/');

            if (cmname[0] == "-" && cmloc[0] == "-" && cmyrs[0] == "-" && cmdesig[0] == "-")
            {
                work1.Visible = false;
            }
            else
            {
                work1.Visible = true;
                cmpname1.Text = cmname[0];
                cmpdsg1.Text = cmdesig[0];
                cmploc1.Text = cmloc[0];
                cmpyr1.Text = cmyrs[0].Equals("-") ? "-" : Checkexp(int.Parse(cmyrs[0]));
            }

            if (cmname[1] == "-" && cmloc[1] == "-" && cmyrs[1] == "-" && cmdesig[1] == "-")
            {
                work2.Visible = false;
            }
            else
            {
                work2.Visible = true;
                cmpname2.Text = cmname[1];
                cmpdsg2.Text = cmdesig[1];
                cmploc2.Text = cmloc[1];
                cmpyr2.Text = cmyrs[1].Equals("-") ? "-" : Checkexp(int.Parse(cmyrs[1]));
            }

            if (cmname[2] == "-" && cmloc[2] == "-" && cmyrs[2] == "-" && cmdesig[2] == "-")
            {
                work3.Visible = false;
            }
            else
            {
                work3.Visible = true;
                cmpname3.Text = cmname[2];
                cmpdsg3.Text = cmdesig[2];
                cmploc3.Text = cmloc[2];
                cmpyr3.Text = cmyrs[2].Equals("-") ? "-" : Checkexp(int.Parse(cmyrs[2]));
            }

            if (cmname[3] == "-" && cmloc[3] == "-" && cmyrs[3] == "-" && cmdesig[3] == "-")
            {
                work4.Visible = false;
            }
            else
            {
                work4.Visible = true;
                cmpname4.Text = cmname[3];
                cmpdsg4.Text = cmdesig[3];
                cmploc4.Text = cmloc[3];
                cmpyr4.Text = cmyrs[3].Equals("-") ? "-" : Checkexp(int.Parse(cmyrs[3]));
            }
             drprof.Close();
             con.Close(); 
        }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "In Page Load of AlumniProfileHome for " + Session["loginname"] + ": " + ex.Message);
        }
    }
    protected string decodepwd(string depas)
    {
        try
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(depas);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Decode method of AlumniProfileHome page for " + Session["loginname"] + ":" + ex.Message);
            return "conversion error";
        }
    }
 
    protected string Checkexp(int num)
    {
       
        string dispmsg="";
        if (num < 0)
        {
            dispmsg =num + (num == 1 ? " month" : " months");
        }
        else if (num > 0 && num < 13)
        {
            dispmsg = num + (num == 1 ? " month" : " months");
        }

        else if (num > 12)
        {
            dispmsg = (num / 12) + " yr " + (num % 12) + ((num % 12) == 1 ? " month" : " months");
        }
            return dispmsg;
    }

    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
    urlpath = Request.UrlReferrer.ToString();
        //int num;
        imgadd.Visible = false;
        friimg.Visible = true;
        closefri.Visible = true;
        sndmsg.Visible = true;
        //friimg.Style.Add("margin-top", "15px");
        //closefri.Style.Add("margin-top", "15px"); 
        //sndmsg.Style.Add("margin-top", "15px");
        try
        {
          // num=Convert.ToInt32(Request.QueryString["yr"]);
           
            con.Open();
          
            cmdfri = new MySqlCommand("insert into user_friends(req_from,req_to,request,req_to_id,accept,req_from_id) values(@from,@to,@req,@toid,@acp,@fromid)", con);
            cmdfri.Parameters.AddWithValue("from", Session["loginname"].ToString());
            cmdfri.Parameters.AddWithValue("to", userdet);
            cmdfri.Parameters.AddWithValue("req", 1);
            cmdfri.Parameters.AddWithValue("toid", toid);
            cmdfri.Parameters.AddWithValue("acp", 1);
            cmdfri.Parameters.AddWithValue("fromid", fromid);
            cmdfri.ExecuteNonQuery();

            cmdfri = new MySqlCommand("insert into user_friends(req_from,req_to,request,req_to_id,accept,req_from_id) values(@from,@to,@req,@toid,@acp,@fromid)", con);
            cmdfri.Parameters.AddWithValue("to", Session["loginname"].ToString());
            cmdfri.Parameters.AddWithValue("from", userdet);
            cmdfri.Parameters.AddWithValue("req", 1);
            cmdfri.Parameters.AddWithValue("fromid", toid);
            cmdfri.Parameters.AddWithValue("acp", 1);
            cmdfri.Parameters.AddWithValue("toid", fromid);
            cmdfri.ExecuteNonQuery();
           
            con.Close();
          
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Image Add method of AlumniProfileHome page for " + Session["loginname"] + ": " + ex.Message);
        }
        Response.Redirect(urlpath);
    }

    protected void closefri_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        urlpath = Request.UrlReferrer.ToString();
            con.Open();
            //this code deletes the friend requests for both alumni
            cmdfri = new MySqlCommand("delete from user_friends where (req_to_id=@toid or req_from_id=@toid) and (req_to_id=@fromid or req_from_id=@fromid)", con);
            cmdfri.Parameters.AddWithValue("toid", toid);
            cmdfri.Parameters.AddWithValue("fromid", fromid);
            cmdfri.ExecuteNonQuery();

            //this code deletes all the messages sent/received from/to alumni
            if (Session["loginname"]!=null)
            {
                cmdfri = new MySqlCommand("delete from message where (msg_from=@p1 or msg_to=@p1) and (msg_from=@p2 or msg_to=@p2)", con);
                cmdfri.Parameters.AddWithValue("p1", userdet);
                cmdfri.Parameters.AddWithValue("p2", Session["loginname"].ToString());
                cmdfri.ExecuteNonQuery();
            }
            con.Close();
            closefri.Visible = false;
            friimg.Visible = false;
            sndmsg.Visible = true;
            imgadd.Visible = true;
            
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Closefri method of AlumniProfileHome page for " + Session["loginname"] + ": " + ex.Message);
        }

        Response.Redirect(urlpath);
    }

    protected void SendMessage_Click(object sender, EventArgs e)
    {
        string frm = Session["loginname"].ToString();
        MailMessage mail = new MailMessage();
        
        SmtpClient smtp = new SmtpClient();
       
        try
        {
            con.Open();
            cmd = new MySqlCommand("insert into message(msg_from,msg_to,msg,msg_date) values(@m1,@m2,@m3,@m4)", con);
            cmd.Parameters.AddWithValue("m1", frm);
            cmd.Parameters.AddWithValue("m2", userdet);
            cmd.Parameters.AddWithValue("m3", string.IsNullOrEmpty(alumsg.Value) ? "No Message" : alumsg.Value);
            cmd.Parameters.AddWithValue("m4", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand("select email from forum_users where username='" + userdet+"'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            msg_to = dr.GetString(0);
            dr.Close();
            con.Close();
            
            //To send Notification to the receipent for the message send     
            mail.From = new MailAddress(mid, "Alumni Admin");
            mail.To.Add("sridharvivek88@gmail.com");//msg_to);
            mail.Subject = "Message from " + frm;
            mail.Body = alumsg.Value +"<br/> To reply please follow this link <a href=\"ssnunite.com/LoginPage.aspx\" target=\"_blank\">www.ssnunite.com</a>";
            mail.IsBodyHtml = true;
            if (!Page.IsPostBack)
            {
            smtp.Host = "smtp.gmail.com"; 
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
            smtp.EnableSsl = true;
            smtp.Send(mail);
              }
            
        }
        catch (SmtpException sf)
        {

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Your Mail has not been sent.');</script>", true);
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "SendMessage method of AlumniProfileHome page for " + Session["loginname"] + ": " + sf.Message);
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "SendMessage method of AlumniProfileHome page for " + Session["loginname"] + ": " + ex.Message);
        }

    }
}