using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
public partial class VerifyMail : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdmail,cmdentry,cmdfr;
    protected void Page_Load(object sender, EventArgs e)
    {
       string uname,code;
        uname=decodepwd( Request.QueryString["uid"]);
        code=Request.QueryString["aucode"];

        try
        {
            con.Open();

            cmdmail = new MySqlCommand("update alumnireg set valid_mail=1 where username='" + uname + "' and auth_code='" + code + "'", con);
            cmdmail.ExecuteNonQuery();

            cmdentry = new MySqlCommand("update login set logentry=2 where username='" + uname + "'", con);
            cmdentry.ExecuteNonQuery();
            
            cmdfr = new MySqlCommand("update forum_users set group_id=4 where username='" + uname + "'", con);
            cmdfr.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Page_Load method of VerifyMail page :" + ex.Message);
            
        }
        finally
        {
            con.Close();
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
            log.ErrorLog(Server.MapPath("Logs/Errorlog"), "Decode method of VerifyMail page :" + ex.Message);
            return "conversion error";
        }

    }
}