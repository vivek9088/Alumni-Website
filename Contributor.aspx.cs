using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
public partial class Contributor : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
    
        int id = Convert.ToInt32(Request.QueryString["urlid"]);
    switch (id)
    { 
        case 1:
        Response.Write("<script language='javascript'>setTimeout(function () { window.location = 'index.aspx'; }, 3000);</script>");
        break;
       
        case 2:
        Response.Write("<script language='javascript'>setTimeout(function () { window.location = 'RegisterForm.aspx'; }, 3000);</script>");
        break;
      
        case 3:
        Response.Write("<script language='javascript'>setTimeout(function () { window.location = 'AlumniHome.aspx'; }, 3000);</script>");
       break; 

        case 4:
        Response.Write("<script language='javascript'>setTimeout(function () { window.location = 'Search Alumni/SearchAlumni.aspx'; }, 3000);</script>");
        break;

        case 5:
        Response.Write("<script language='javascript'>setTimeout(function () { window.location = 'AlumniPages/AlumniProfHome.aspx'; }, 3000);</script>");
        break;
        }
    
    }
}