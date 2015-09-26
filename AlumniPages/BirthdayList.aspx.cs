using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
public partial class AlumniPages_BirthdayList : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdbday;
    MySqlDataReader drbday;
    protected void Page_Load(object sender, EventArgs e)
    {
        int today=DateTime.Now.Day,month=DateTime.Now.Month,year=DateTime.Now.Year;
        HtmlTableRow row;
        HtmlTableCell cell, cell1, cell2;

        try
        {
            con.Open();
            cmdbday = new MySqlCommand("select fname,lname,dob,imgpath from alumnireg where day(dob) IN (" + today + ") and month(dob) IN (" + month + ")", con);
            drbday = cmdbday.ExecuteReader();

            if (drbday.HasRows)
            {
                while (drbday.Read())
                {
                string fullname = drbday.GetString("fname") + " " + drbday.GetString("lname");
                DateTime bday = Convert.ToDateTime(drbday.GetString("dob")),newdate=new DateTime(year,bday.Month,bday.Day);
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell1 = new HtmlTableCell();
                    cell2 = new HtmlTableCell();
                    cell.VAlign = "Middle";
                    cell1.VAlign = "Middle";
                    cell2.VAlign = "Middle";
                    cell.Width = "30%";
                    cell1.Width = "40%";
                    cell2.Width = "20%";
                   
                    cell.Controls.Add(new LiteralControl(fullname));
                    cell1.Controls.Add(new LiteralControl(newdate.ToLongDateString()));
                    if(drbday.IsDBNull(3))
                    {
                    cell2.Controls.Add(new LiteralControl("<img height='35' width='35' src='../images/nophoto.jpg'/>"));
                    }
                    if (System.IO.File.Exists(Server.MapPath("../" + drbday.GetString("imgpath"))))
                    {
                        cell2.Controls.Add(new LiteralControl("<img height='35' width='35' src='../" + drbday.GetString("imgpath") + "'/>"));
                    }
                    else
                    {
                        cell2.Controls.Add(new LiteralControl("<img height='35' width='35' src='../images/nophoto.jpg'/>"));
                    }
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell);
                    row.Cells.Add(cell1);
                    todaytab.Rows.Add(row);
                }

            }
            else
            {
                errmsg.Visible = true;
                errmsg.Text = "No Alumni Birthday is found today";
            }
            drbday.Close();

            cmdbday = new MySqlCommand("select fname,lname,dob,imgpath from alumnireg where month(dob) IN (" + month + ") and day(dob) > day(now()) order by day(dob)", con);
           
            drbday = cmdbday.ExecuteReader();
            if (drbday.HasRows)
            {
                while (drbday.Read())
                {
                string fullname = drbday.GetString("fname") + " " + drbday.GetString("lname");
                DateTime bday = Convert.ToDateTime(drbday.GetString("dob")), newday;
                int day = bday.Day, mon = bday.Month, yr = DateTime.Now.Year;
                newday = new DateTime(yr, mon, day);
                    row = new HtmlTableRow();
                    cell = new HtmlTableCell();
                    cell1 = new HtmlTableCell();
                    cell2 = new HtmlTableCell();
                    cell.VAlign = "Middle";
                    cell1.VAlign = "Middle";
                    cell2.VAlign = "Middle";
                    cell.Width = "30%";
                    cell1.Width = "40%";
                    cell2.Width = "20%";
                    cell.Controls.Add(new LiteralControl(fullname));
                    cell1.Controls.Add(new LiteralControl(newday.ToLongDateString()));
                    if (drbday.IsDBNull(3))
                    {
                        cell2.Controls.Add(new LiteralControl("<img height='35' width='35' src='../images/nophoto.jpg'/>"));
                    }
                    if (System.IO.File.Exists(Server.MapPath("../" + drbday.GetString("imgpath"))))
                    {
                        cell2.Controls.Add(new LiteralControl("<img height='35' width='35' src='../" + drbday.GetString("imgpath") + "'/>"));
                    }
                    else
                    {
                        cell2.Controls.Add(new LiteralControl("<img height='35' width='35' src='../images/nophoto.jpg'/>"));
                    }
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell);
                    row.Cells.Add(cell1);
                    upcometab.Rows.Add(row);
                }
                drbday.Close();
            }
            else
            {
                month1.Visible = true;
                month1.Text = "No Alumni Birthday found in this month ";
            }
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load method of BirthdayList page for " + Session["loginname"] + ": " + ex.Message);
        }
    }
}