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
public partial class Admin_Pages_Admin_Events : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd, cmduser;
    MySqlDataReader dr;
    string[] list;
    string st="";
    List<string> imgname = new List<string>();
    static int choice;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sh = string.IsNullOrEmpty(Request.QueryString["sh"]) ? "view1" : Request.QueryString["sh"],showdiv;
        MultiView1.ActiveViewIndex = (sh == "view1") ? 0 : 1;
        showdiv = string.IsNullOrEmpty(Request.QueryString["eid"]) ? "evtedit" : Request.QueryString["eid"];
        if (showdiv == "evtedit")
        {
            evtedit.Attributes.Add("class", "active");
            evtdel.Attributes.Remove("class");
            ediv1.Style.Add("display", "block");
            ediv2.Style.Add("display","none");
        }
        else
        {
            evtdel.Attributes.Add("class", "active");
            evtedit.Attributes.Remove("class");
            ediv2.Style.Add("display", "block");
            ediv1.Style.Add("display", "none");
        }
        
        if (sh == "view1")
        {
            addevents.Attributes.Add("class", "active");
            editevents.Attributes.Remove("class"); 
        }
        else
        { editevents.Attributes.Add("class", "active");
        addevents.Attributes.Remove("class");
        }

        if (!Page.IsPostBack)
        {
            try
            {
                con.Open();

                cmd = new MySqlCommand("select name,head,venue,detail,dttime from events where evt_id='e_01'", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                evt_name.Text = dr.GetString("name");
                evt_head.Text = dr.GetString("head");
                evt_det.Text = dr.GetString("detail");
                evt_venue.Text = dr.IsDBNull(2) ? "" : dr.GetString("venue");
                evt_date.Text = dr.IsDBNull(4) ? "" : dr.GetDateTime("dttime").ToString();
                dr.Close();
                

                cmd = new MySqlCommand("select img1path from events where rowid < 6", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list = dr.GetString("img1path").Split('/');
                    imgname.Add(list[1].ToString());
                }

                dr.Close();

                foreach (string s in imgname)
                {
                    folderlist.Items.Add(s);
                }

                bindeventdata();
                con.Close();
                bindeventlist();
            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load method of Admin_Events for " + Session["loginname"] + ": " + ex.Message);
            }
        }
    }
    private void bindeventlist()
    {
        MySqlDataAdapter da = new MySqlDataAdapter("select evt_id from events", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "events");
        eventlist.DataSource = ds;
        eventlist.DataTextField = "evt_id";
        eventlist.DataValueField = "evt_id";
        eventlist.DataBind();
    }

    protected void EventAdd_Click(object sender, EventArgs e)
    {
        string path,path1,evtdate,pth,evid="";
        int chkid;
        string[] temp;
        HttpFileCollection files;
       
        if(year.SelectedIndex==0||month.SelectedIndex==0||day.SelectedIndex==0)
        {
        success2.Text="Date Format incorrect";
        }
       
        else
        {
            success2.Text = "";    
            try
            {
                files = HttpContext.Current.Request.Files;
                
                evtdate = year.SelectedItem.Value + "-" + month.SelectedItem.Value + "-" + day.SelectedItem.Value;
                //to create a folder for the particular event in images folder

                path = "../images/"+ folder.Text.Trim() + "/" + year.SelectedItem.Value+"/slides";
                path1 = "../images/" + folder.Text.Trim() + "/" + year.SelectedItem.Value + "/thumbs";

                Directory.CreateDirectory(Server.MapPath(path));
                Directory.CreateDirectory(Server.MapPath(path1));

                //to compress the uploaded images and store it in event folder
                if (files.Count > 0)
                {
                    for (int i = 1; i <= files.Count; i++)
                    {
                        HttpPostedFile post = files[i];
                        Stream strm = post.InputStream;
                        string slideFile = Server.MapPath(path) + "/img" + i.ToString();
                        string tempFile = Server.MapPath(path1) + "/img" + i.ToString();
                        //Based on scalefactor image size will vary

                        GenerateThumbnails(0.2, strm, slideFile);
                        GenerateThumbnails(0.1, strm, tempFile);
                    }
                }
                con.Open();

                cmd = new MySqlCommand("select max(evt_id) from events", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                temp = dr.GetString("max(evt_id)").Split('_');
                chkid = int.Parse(temp[1]) + 1;
                dr.Close();

                evid = chkid > 10 ? "evt_" + chkid.ToString() : "evt_0" + chkid.ToString();

                cmd = new MySqlCommand("insert into events(name,head,detail,dttime,venue,img1path,img2path,img3path,evt_id) values(@ename,@ehead,@edetail,@edttime,@evenue,@path1,@path2,@path3,@eid)", con);
                cmd.Parameters.AddWithValue("eid", evid);
                cmd.Parameters.AddWithValue("ename", eventname.Text.Trim());
                cmd.Parameters.AddWithValue("ehead", eventhead.Text.Trim());
                cmd.Parameters.AddWithValue("edetail", eventdetail.Text);
                cmd.Parameters.AddWithValue("edttime", evtdate);
                cmd.Parameters.AddWithValue("evenue", eventvenue.Text.Trim());
                if (choice == 1)
                {
                    pth = string.IsNullOrEmpty(folder.Text.Trim()) ? "" : folder.Text.Trim();
                    cmd.Parameters.AddWithValue("path1", "images/" + folder.Text.Trim() + "/" + year.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("path2", "images/" + folder.Text.Trim() + "/" + year.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("path3", "images/" + folder.Text.Trim() + "/" + year.SelectedItem.Value);
                }
                else if(choice==2)
                {
                    cmd.Parameters.AddWithValue("path1", "images/" + folderlist.SelectedItem.Value + "/" + year.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("path2", "images/" + folderlist.SelectedItem.Value + "/" + year.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("path3", "images/" + folderlist.SelectedItem.Value + "/" + year.SelectedItem.Value);
                }

                cmd.ExecuteNonQuery();
                con.Close();
                success2.Text = "Event has been successfully Posted";

            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Event Add Method of Admin_Events for " + Session["loginname"] + ": " + ex.Message);
            }

        }
    }

    private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }

    protected void EventGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EventGridView1.PageIndex = e.NewPageIndex;
        bindeventdata();
    }
    protected void bindeventdata()
    {
        MySqlDataAdapter da = new MySqlDataAdapter("select evt_id,name,head,detail from events", con);
        DataSet ds = new DataSet();
        da.Fill(ds, "events");
        EventGridView1.DataSource = ds;
        EventGridView1.DataBind();
    }

    protected void EventGridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       string row = EventGridView1.DataKeys[e.RowIndex].Value.ToString();
        MySqlCommand cmd = new MySqlCommand("delete from events where evt_id='" + row+"'", con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        bindeventdata();
    }
  
    protected void year_Init(object sender, EventArgs e)
    {
        year.Items.Add("Year");
        for (int i = 2012;i<=2020; i++)
        {
            year.Items.Add(i.ToString()); 
        }
    }
    protected void month_Init(object sender, EventArgs e)
    {
        month.Items.Add("Month");
        for (int i = 1; i <= 12; i++)
        {month.Items.Add(i.ToString()); }

    }
    protected void day_Init(object sender, EventArgs e)
    {
        day.Items.Add("Day");
        for (int i = 1; i <= 31; i++)
        { day.Items.Add(i.ToString()); }
    }
    protected void month_SelectedIndexChanged(object sender, EventArgs e)
    {
        day.Items.Clear();
        day.Items.Add("Day");
        switch (month.SelectedItem.Value)
        { //for months containing 31 days
            case "1":
            case "3":
            case "5":
            case "7":
            case "8":
            case "10":
            case "12":

                for (int dy = 1; dy <= 31; dy++)
                {
                    day.Items.Add(dy.ToString());
                }
                break;

            //for months containing 30 days
            case "4":
            case "6":
            case "9":
            case "11":

                for (int dy = 1; dy <= 30; dy++)
                {
                    day.Items.Add(dy.ToString());
                }
                break;

            //for february month
            default:

                for (int dy = 1; dy <= 28; dy++)
                {
                    day.Items.Add(dy.ToString());
                }
                break;
        }
        day.Focus();
    }
    protected void folderopt_CheckedChanged(object sender, EventArgs e)
    {
        if (folderopt.Checked == true)
        {
            folder.Visible = true;
            folderlist.Visible = false;
            choice = 2;
        }
        else
        {
            folderlist.Visible = true;
            folder.Visible = false;
            choice = 1;
        }
    }
    protected void eventlist_SelectedIndexChanged(object sender, EventArgs e)
    {
       st = eventlist.SelectedItem.Value;

        try
        {
            con.Open();
            cmd = new MySqlCommand("select name,head,venue,detail,dttime from events where evt_id='" + st + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            evt_name.Text = dr.GetString("name");
            evt_head.Text = dr.GetString("head");
            evt_det.Text = dr.GetString("detail");
            evt_venue.Text = dr.IsDBNull(2) ? "" : dr.GetString("venue");
            evt_date.Text = dr.IsDBNull(4) ? "" : dr.GetString("dttime");
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "SelectedIndexChange event of Admin_Events for " + Session["loginname"] + ": " + ex.Message);
        }
    }
    protected void Event_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            cmd = new MySqlCommand("update events set name=@e1,head=@e2,venue=@e3,dttime=@e4,detail=@e5 where evt_id='" + eventlist.SelectedItem.Value + "'", con);
            cmd.Parameters.AddWithValue("e1", evt_name.Text);
            cmd.Parameters.AddWithValue("e2", evt_head.Text);
            cmd.Parameters.AddWithValue("e3", evt_venue.Text);
            cmd.Parameters.AddWithValue("e4", evt_date.Text == "" ? "0000-00-00" : evt_date.Text);
            cmd.Parameters.AddWithValue("e5", evt_det.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Event Edit method of Admin_Events for " + Session["loginname"] + ": " + ex.Message);
        }
    }
}