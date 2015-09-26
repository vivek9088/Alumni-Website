using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
public partial class Search_Alumni_SearchList : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdresult;
    MySqlDataReader drresult;
    protected string getid, getvalue, query;
    protected string[] getvalues;
    public string type, value;
    public int cnt = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        getid = Request.QueryString["id"].ToString();

            switch (getid)
            {
                case "1":
                    type = "Class";
                    getvalues = Request.QueryString["search"].ToString().Split(',');
                    value = getvalues[0] + "," + getvalues[1] + "," + getvalues[2];
                    query = "select * from alumnireg where degree='" + getvalues[0] + "' and branch='" + getvalues[1] + "' and batch='" + getvalues[2] + "' and username not in('"+Session["loginname"]+"')";
                    break;

                case "2":
                    type = "Name";
                    getvalue = Request.QueryString["search"].ToString();
                    value = getvalue;
                    query = "select * from alumnireg where fname like '" + getvalue + "%' and username not in('" + Session["loginname"] + "')";
                    break;

                case "3":
                    type = "Year";
                    getvalue = Request.QueryString["search"].ToString();
                    value = getvalue;
                    query = "select * from alumnireg where batch='" + getvalue + "' and username not in('" + Session["loginname"] + "')";
                    break;

                case "4":
                    string value1;
                    type = "Location";
                    getvalues = Request.QueryString["search"].ToString().Split(',');
                    value = getvalues[0] + ", " + getvalues[1];
                    value1 = getvalues[1].ToLower();
                    if (value1 == "city")
                    { query = "select * from alumnireg where city='" + getvalues[0] + "' and username not in('" + Session["loginname"] + "')"; }
                    else if (value1 == "state")
                    { query = "select * from alumnireg where state='" + getvalues[0] + "' and username not in('" + Session["loginname"] + "')"; }
                    else if (value1 == "country")
                    { query = "select * from alumnireg where country='" + getvalues[0] + "' and username not in('" + Session["loginname"] + "')"; }

                    break;
            }
    displayresult(query, getid);
    }

    protected void displayresult(string resquery, string queryid)
    {
        
        HtmlTableRow row;
        HtmlTableCell col1,col2,col3,col4,imgcol; 
        HyperLink link;
        Image img;
        string path;
        try
        {
            con.Open();
           cmdresult = new MySqlCommand(resquery, con);
            drresult = cmdresult.ExecuteReader();
            if (drresult.HasRows)
            {
                switch (queryid)
                {
                    
                    case "4":
                        while (drresult.Read())
                        {
                            row = new HtmlTableRow();
                            col1 = new HtmlTableCell(); col2 = new HtmlTableCell(); col3 = new HtmlTableCell(); col4 = new HtmlTableCell();
                             link = new HyperLink();
                             imgcol = new HtmlTableCell();
                             img = new Image();

                            col1.InnerHtml = cnt.ToString() + ".";
                            row.Cells.Add(col1);

                            //if (File.Exists(Server.MapPath("../Alumni_Images/" + drresult.GetString("regid") + ".png")))
                            if (File.Exists(Server.MapPath("../"+drresult.GetString("imgpath"))))
                            {
                                path = "../"+drresult.GetString("imgpath");
                            }
                            else
                            { path = "../images/nophoto.jpg"; }

                            img.ImageUrl = path;
                            img.Height = 35;
                            img.Width = 35;
                            imgcol.Controls.Add(img);
                            row.Cells.Add(imgcol);

                            link.Text = drresult.GetString("fname") + " " + drresult.GetString("lname");
                            link.NavigateUrl = "ViewProfile.aspx?profid=" +encodepwd(drresult.GetString("username")) + "&searchid=" + getid + "&searchid1=" + Request.QueryString["search"].ToString();
                            col2.Controls.Add(link);
                            col2.Align = "center";
                            row.Cells.Add(col2);

                            col3.InnerHtml = drresult.GetString("branch") + ", " + drresult.GetString("batch");
                            row.Cells.Add(col3);

                            col4.InnerHtml = getvalues[0];
                            row.Cells.Add(col4);

                            
                            resulttable.Rows.Add(row);
                            cnt += 1;
                        }
                        break;
                    default:
                        while (drresult.Read())
                        {
                            row = new HtmlTableRow();
                            col1 = new HtmlTableCell(); col2 = new HtmlTableCell(); col3 = new HtmlTableCell(); col4 = new HtmlTableCell();
                            link = new HyperLink();
                            imgcol = new HtmlTableCell();
                            img = new Image();

                            col1.InnerHtml = cnt.ToString() + ".";
                            row.Cells.Add(col1);

                            
                            if(File.Exists(Server.MapPath("../" + drresult.GetString("imgpath"))))
                            {
                                path= "../" + drresult.GetString("imgpath");
                            }
                            else
                            { path = "../images/nophoto.jpg"; }
                            
                            img.ImageUrl =path;
                            img.Height = 35;
                            img.Width = 35;
                            imgcol.Controls.Add(img);
                            row.Cells.Add(imgcol);

                            link.Text = drresult.GetString("fname") + " " + drresult.GetString("lname");
                            link.NavigateUrl = "ViewProfile.aspx?profid=" +encodepwd(drresult.GetString("username")) + "&searchid=" + getid + "&searchid1=" + Request.QueryString["search"].ToString();
                            col2.Controls.Add(link);
                            col2.Align = "center";
                            row.Cells.Add(col2);

                            col3.InnerHtml = drresult.GetString("branch") + ", " + drresult.GetString("batch");
                            row.Cells.Add(col3);

                            resulttable.Rows.Add(row);
                            cnt += 1;
                        }
                        break;
                }
                drresult.Close();
                cnt -= 1;
            }
            else
            {
                row = new HtmlTableRow();
                col1 = new HtmlTableCell(); 
               
                col1.InnerHtml = "No Results Found";
                row.Cells.Add(col1);
                resulttable.Rows.Add(row);
                cnt -= 1;
            }
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Display result method of SearchList Page for " + Session["loginname"] + ":" + ex.Message);
        }
        finally
        {
            con.Close();
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