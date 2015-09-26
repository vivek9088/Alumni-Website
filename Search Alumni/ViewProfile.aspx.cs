using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
public partial class Search_Alumni_ViewProfile : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdgetprof,cmdfri,cmd;
    MySqlDataReader drprof,drfri,dr;
    static string newid,aluid,userdet;
    static int fromid, toid, checkval;

    protected void Page_Load(object sender, EventArgs e)
    {
       // imgadd.Style.Add("margin-top", "15px");
        //sndmsg.Style.Add("margin-top", "15px");

        aluid =decodepwd(Request.QueryString["profid"]);
        newid = Request.QueryString["searchid"].ToString();

        link.HRef = "SearchList.aspx?id=" + newid+"&search="+Request.QueryString["searchid1"].ToString();

        try
        {
            con.Open();

            //to get id of user who sent request
            cmd=new MySqlCommand("select id from forum_users where username='"+Session["loginname"]+"'",con);
            dr=cmd.ExecuteReader();
            dr.Read();
            fromid =dr.GetInt32("id");
            dr.Close();

            //to get id of user who accepted request
            cmd = new MySqlCommand("select id from forum_users where username='" +aluid + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            toid = dr.GetInt32("id");
            dr.Close();

            cmd = new MySqlCommand("select accept from user_friends where ((req_from_id=" + fromid + " and req_to_id=" + toid + ") or (req_from_id=" + toid + " and req_to_id=" + fromid + ")) and accept=1", con);
            dr = cmd.ExecuteReader();
            checkval = (dr.Read()) ? 1 : 0;
            dr.Close();

            if (checkval == 0)
            {
                imgadd.Visible = true;
                sndmsg.Visible = true;
            }
            else
            {
                friimg.Visible = true;
                closefri.Visible = true;
                sndmsg.Visible = true;
            }

            cmdgetprof = new MySqlCommand("select regid,username,fname,lname,batch,branch,org,desig,dob,city,state,country,email,cmpemail,imgpath from alumnireg where username='" + aluid + "'", con);
            drprof = cmdgetprof.ExecuteReader();
            drprof.Read();
            
            userdet = drprof.GetString("username");
            
            if(File.Exists(Server.MapPath("../"+drprof.GetString("imgpath"))))
            {
                img1.Src = "../" + drprof.GetString("imgpath");
            }
            else
            {
                img1.Src = "../images/nophoto.jpg";
            }
            //to display the profile details on the page.
            name.Text = string.Concat(drprof["fname"].ToString().Substring(0, 1).ToUpper(), drprof["fname"].ToString().Substring(1)) + " " + string.Concat(drprof["lname"].ToString().Substring(0, 1).ToUpper(), drprof["lname"].ToString().Substring(1));

            DateTime dt =Convert.ToDateTime(drprof.GetString("dob"));
            dob.Text = dt.ToString("MMM, d");
            branch.Text = string.IsNullOrEmpty(drprof.GetString("branch")) ? "-" : drprof.GetString("branch");
            batch.Text = string.IsNullOrEmpty(drprof.GetInt32("batch").ToString()) ? "-" : drprof.GetInt32("batch").ToString();
            
            string[] org1 = drprof.GetString("org").Split('/');
            org.Text = org1[0];

            string[] desig1 = drprof.GetString("desig").Split('/');
            desig.Text = desig1[0];

            peremail.Text = string.IsNullOrEmpty(drprof.GetString("email")) ? "-" : drprof.GetString("email");
            cmpemail.Text = string.IsNullOrEmpty(drprof.GetString("cmpemail")) ? "-" : drprof.GetString("cmpemail");
            city.Text = string.IsNullOrEmpty(drprof.GetString("city")) ? "-" : drprof.GetString("city");
            state.Text = string.IsNullOrEmpty(drprof.GetString("state")) ? "-" : drprof.GetString("state");
            country.Text = string.IsNullOrEmpty(drprof.GetString("country")) ? "-" : drprof.GetString("country");
            drprof.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load of View Profile for " + Session["loginname"] + ":" + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }

    protected void imgadd_Click(object sender, ImageClickEventArgs e)
    {
        //int num;
        imgadd.Visible = false;
        friimg.Visible = true;
        closefri.Visible = true;
        sndmsg.Visible = true;
        /*friimg.Style.Add("margin-top", "15px");
        closefri.Style.Add("margin-top", "15px");
        sndmsg.Style.Add("margin-top", "15px");*/
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
    }

    protected void closefri_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            con.Open();
            //this code deletes the friend requests for both alumni
            cmdfri = new MySqlCommand("delete from user_friends where (req_to_id=@toid or req_from_id=@toid) and (req_to_id=@fromid or req_from_id=@fromid)", con);
            cmdfri.Parameters.AddWithValue("toid", toid);
            cmdfri.Parameters.AddWithValue("fromid", fromid);
            cmdfri.ExecuteNonQuery();

            //this code deletes all the messages sent/received from/to alumni
            if (Session["loginname"] != null)
            {
                cmdfri = new MySqlCommand("delete from message where (msg_from=@p1 or msg_to=@p1) and (msg_from=@p2 or msg_to=@p2)", con);
                cmdfri.Parameters.AddWithValue("p1", userdet);
                cmdfri.Parameters.AddWithValue("p2", Session["loginname"].ToString());
                cmdfri.ExecuteNonQuery();
            }
            con.Close();
            closefri.Visible = false;
            friimg.Visible = false;
            sndmsg.Visible = false;
            imgadd.Visible = true;

        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Closefri method of AlumniProfileHome page for " + Session["loginname"] + ": " + ex.Message);
        }
    }

    protected void SendMessage_Click(object sender, EventArgs e)
    {
        string frm = Session["loginname"].ToString();
        try
        {
            con.Open();
            cmd = new MySqlCommand("insert into message(msg_from,msg_to,msg,msg_date,msg_time) values(@m1,@m2,@m3,@m4,@m5)", con);
            cmd.Parameters.AddWithValue("m1", frm);
            cmd.Parameters.AddWithValue("m2", userdet);
            cmd.Parameters.AddWithValue("m3", string.IsNullOrEmpty(alumsg.Value) ? "No Message" : alumsg.Value);
            cmd.Parameters.AddWithValue("m4", System.DateTime.Now.Date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("m5", System.DateTime.Now.TimeOfDay);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "SendMessage method of AlumniProfileHome page for " + Session["loginname"] + ": " + ex.Message);
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
}