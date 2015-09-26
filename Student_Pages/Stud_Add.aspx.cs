using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

public partial class Student_Pages_Stud_Add : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdadd;
    MySqlDataReader dr;
    string mybranch;
    protected void Page_Load(object sender, EventArgs e)
    {
        string name;
        try
        {
            con.Open();
            name = Session["loginname"].ToString();
            cmdadd = new MySqlCommand("select branch from forum_users where username='" + name + "'", con);
            dr = cmdadd.ExecuteReader();
            dr.Read();
            mybranch = dr.GetString("branch");
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load method of Stud_add page for " + Session["loginname"] + ":" + ex.Message);
        }
            if (Page.IsPostBack)
            {

                if (!(String.IsNullOrEmpty(pass.Text.Trim())))
                {
                    pass.Attributes["value"] = pass.Text;

                }
                pass.Text = pass.Attributes["value"];

            }
       
    }
   
    protected void smon_Init(object sender, EventArgs e)
    {
        smon.Items.Add("Month");
        for (int yr = 1; yr <= 12; yr++)
        {
            smon.Items.Add(yr.ToString());
        }
    }
    protected void sday_Init(object sender, EventArgs e)
    {
        sday.Items.Add("Day");
        for (int dy = 1; dy <= 31; dy++)
        {
            sday.Items.Add(dy.ToString());
        }

    }
    protected void syr_Init(object sender, EventArgs e)
    {
        syr.Items.Add("Year");
        for (int yr = 1975; yr <= System.DateTime.Now.Year; yr++)
        {
            syr.Items.Add(yr.ToString());
        }

    }
   
    
    protected void Add_Stud_Click(object sender, EventArgs e)
    {
        
        string date;
        if (sday.SelectedIndex == 0 || smon.SelectedIndex == 0 || syr.SelectedIndex == 0)
        {
            doberr.Visible = true;
            doberr.Text = "Please select correct Date Format";
        }
        else
        {
            
            date=syr.SelectedItem.Value+"-"+smon.SelectedItem.Value+"-"+sday.SelectedItem.Value;
            
            doberr.Text = "";
            try
            {
                con.Open();
                cmdadd = new MySqlCommand("insert into forum_users(group_id,username,password,registered,doj,branch) values(@gid,@uname,password('@pass'),@regdate,@join,@bat)", con);
                cmdadd.Parameters.AddWithValue("@gid", 3);
                cmdadd.Parameters.AddWithValue("@uname", name.Text.Trim());
                cmdadd.Parameters.AddWithValue("@pass", pass.Text.Trim());
                cmdadd.Parameters.AddWithValue("@regdate", System.DateTime.Now);
                cmdadd.Parameters.AddWithValue("@join", date);
                cmdadd.Parameters.AddWithValue("@bat", mybranch);

                cmdadd.ExecuteNonQuery();
                con.Close();

                msg.Text = "Student added Successfully";
            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"),"Add Stud method of Stud_add Page for " + Session["loginname"] + ":"+ex.Message);
            }
        }

    }
    protected void sday_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this condition is to check for february has 29 or 28 days only.
        sday.Text = (smon.SelectedItem.Value == "2" && int.Parse(sday.SelectedItem.Value) > System.DateTime.DaysInMonth(int.Parse(syr.SelectedItem.Value), int.Parse(smon.SelectedItem.Value))) ? System.DateTime.DaysInMonth(int.Parse(syr.SelectedItem.Value), int.Parse(smon.SelectedItem.Value)).ToString() : sday.SelectedItem.Value;
       
    }
}