using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Collections;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdsent;
    MySqlDataReader dr;
    MySqlDataAdapter da;
    static int clicked;    
    protected void Page_Load(object sender, EventArgs e)
    {
        
//Response.Redirect("index.aspx");

      /*  string status = "";
        int chk,ver_id=4647336;
       
        
        con.Open();
        cmdsent = new MySqlCommand("select concat(fname,' ',lname),username,imgpath from alumnireg Order by rowid desc LIMIT 6 ", con);
                  
     dr = cmdsent.ExecuteReader();

     dr.Read();  
     dr.Close();
     con.Close();
   */
       
    }

   
   /* protected void editimage_Click(object sender, EventArgs e)
    {
        string filename = "";
        string path,qry="";
        if (FileUpload1.HasFile)
        {
            filename =Path.GetExtension( FileUpload1.PostedFile.FileName);
            path = Server.MapPath("Alumni_Images/") +clicked+filename;
            FileUpload1.PostedFile.SaveAs(path);
            string onlyname = "Alumni_Images/" + clicked + filename ; 
            Image1.ImageUrl =onlyname;

            con.Open();
            cmdsent = new MySqlCommand("select imgpath from alumnireg where regid="+clicked, con);
            dr = cmdsent.ExecuteReader();
            dr.Read();
           if(dr.IsDBNull(0)||string.IsNullOrEmpty(dr.GetString("imgpath")))
           {
           qry="update alumnireg set imgpath='"+onlyname+"' where regid="+clicked;
           }
            dr.Close();

            if (qry != "")
            {
                cmdsent = new MySqlCommand(qry, con);
                cmdsent.ExecuteNonQuery();
            }
            con.Close();
        }
    }*/
   
}