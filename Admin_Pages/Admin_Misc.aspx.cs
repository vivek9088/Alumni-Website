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

public partial class Admin_Pages_Admin_Misc : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmd, cmduser;
    MySqlDataReader dr, drtopic, drpoll, druser;
    DataSet gds;
    static string[] eipath;
    static string dispvalue = "none/none",mid,tquery="",mpwd;

    protected void Page_Load(object sender, EventArgs e)
    {
        int indx = string.IsNullOrEmpty(Request.QueryString["snt"])?0:int.Parse(Request.QueryString["snt"]);
        string sh = string.IsNullOrEmpty(Request.QueryString["sh"]) ? "view1" : Request.QueryString["sh"], temp;
        temp = string.IsNullOrEmpty(Request.QueryString["divid"]) ? "addann" : Request.QueryString["divid"];

        mid = ConfigurationManager.AppSettings["mailid"];
        mpwd = ConfigurationManager.AppSettings["mailpwd"];
        if (temp == "addann")
        {
            addann.Attributes.Add("class", "active");
            delann.Attributes.Remove("class");
            div1.Style.Add("display", "block");
            div2.Style.Add("display", "none");
        }
        else
        {
            div1.Style.Add("display", "none");
            div2.Style.Add("display", "block");
            delann.Attributes.Add("class", "active");
            addann.Attributes.Remove("class");
        }
        switch (sh)
        {
            case "view1":
                upld.Attributes.Add("class", "active");
                export.Attributes.Remove("class");
                annonmsg.Attributes.Remove("class");
                sendmsg.Attributes.Remove("class");
                MultiView1.ActiveViewIndex = 0;
                break;
            case "view2":
                export.Attributes.Add("class", "active");
                upld.Attributes.Remove("class");
                annonmsg.Attributes.Remove("class");
                sendmsg.Attributes.Remove("class");
                MultiView1.ActiveViewIndex = 1;
                break;
            case "view3":
                annonmsg.Attributes.Add("class", "active");
                export.Attributes.Remove("class");
                upld.Attributes.Remove("class");
                sendmsg.Attributes.Remove("class");
                MultiView1.ActiveViewIndex = 2;
                break;
            case "view4":
                sendmsg.Attributes.Add("class", "active");
                export.Attributes.Remove("class");
                upld.Attributes.Remove("class");
                annonmsg.Attributes.Remove("class");
                MultiView1.ActiveViewIndex = 3;
                break;
        }

        if (indx==1)
        {
            postsentmsg.Text ="Message has been sent successfully" ;
            postsentmsg.Visible = true;
        }
        
        if (Page.IsPostBack)
        {
            string[] divvalues = dispvalue.Split('/');

            div3.Style.Add("display", divvalues[0]);
            div4.Style.Add("display", divvalues[1]);
        }
        if (!Page.IsPostBack)
        {

            getlist();
            try
            {
                con.Open();
                cmd = new MySqlCommand("select name from events where rowid > 1", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    evtlist.Items.Add(dr.GetString("name"));
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page_Load method of Admin_Misc page for " + Session["loginname"] + ": " + ex.Message);
            }
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        for (int i = 2000; i <= System.DateTime.Now.Year; i++)
        {
            specificyr.Items.Add(i.ToString());
            fromyr.Items.Add(i.ToString());
            toyr.Items.Add(i.ToString());
        }

    }
    protected void evtlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pname=evtlist.SelectedItem.Value;
        try
        {
            pname = string.IsNullOrEmpty(evtlist.SelectedItem.Value) ? "rowid > 1 limit 1" : "name='" + evtlist.SelectedItem.Value + "'";
            con.Open();
            cmd = new MySqlCommand("select img1path from events where "+pname, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            eipath = dr.GetString("img1path").Split('/');
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "EvtList method of Admin_Misc page for " + Session["loginname"] + ": " + ex.Message);

        }
    }
    protected void Photo_Upload_Click(object sender, EventArgs e)
    {
        HttpFileCollection file = HttpContext.Current.Request.Files;
        string path = "", ext, path1 = ""; ;
      
        for (int i = 0,cnt=1; i < file.Count; i++,cnt++)
        {
            HttpPostedFile post = file[i];
            ext = Path.GetExtension(post.FileName);
            if (ext == ".jpg" || ext == ".png")
            {
                path = Server.MapPath("../images/"+eipath[1]+"/" + yrlist.SelectedItem.Value + "/thumbs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    GenerateThumbnails(post.InputStream, path + "/img" + cnt + ext); 
                }
                else { GenerateThumbnails(post.InputStream, path+"/img"+ cnt +ext); }

                path1 = Server.MapPath("../images/" + eipath[1] + "/" + yrlist.SelectedItem.Value + "/slides");
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(path1);
                    GenerateThumbnails1(post.InputStream, path1 + "/img" + cnt + ext); 
                }
                else { GenerateThumbnails1(post.InputStream, path1 + "/img" + cnt + ext); }

                //post.SaveAs(Server.MapPath("images/temp/slides/" + post.FileName));
            }
            else
            {
                lblMsg.Text = "File not in correct format";
            }
        }
        if (DropDownList1.SelectedItem.Value == "1")
        {
            lblMsg.Text = "File Uploaded Successfully";
        }
        else if (DropDownList1.SelectedItem.Value != "1")
        {
            lblMsg.Text = "Files Uploaded Successfully";

        }
        lblMsg.ForeColor = System.Drawing.Color.Green;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int tot = Convert.ToInt32(DropDownList1.SelectedItem.Value), row, col, row1;
        FileUpload f;
        HtmlTableRow trow = new HtmlTableRow();
        HtmlTableCell tcell;
        if (tot == 1)
        {
            upload.Style.Add("display", "block");
        }
        else if (tot == 5)
        {
            upload.Style.Add("display", "none");
            for (int i = 1; i <= tot; i++)
            {
                trow = new HtmlTableRow();
                tcell = new HtmlTableCell();
                f = new FileUpload();
                f.ID = "fileupload" + i;
                tcell.Controls.Add(f);
                trow.Cells.Add(tcell);
                uploadtable.Controls.Add(trow);
            }
        }
        else
        {
            upload.Style.Add("display", "none");
            int sumtot = tot / 3;
            for (row = 0; row < sumtot; row++)
            {
                trow = new HtmlTableRow();
                for (col = 0; col < 3; col++)
                {
                    tcell = new HtmlTableCell();
                    f = new FileUpload();
                    f.ID = "fileupload" + row + col;
                    tcell.Controls.Add(f);
                    trow.Cells.Add(tcell);

                }
                uploadtable.Controls.Add(trow);
            }
            row1 = row;

            for (row = 0; row < 1; row++)
            {
                trow = new HtmlTableRow();
                for (col = 0; col < (tot % 3); col++)
                {
                    tcell = new HtmlTableCell();
                    f = new FileUpload();
                    f.ID = "fileupload" + row1 + col;
                    tcell.Controls.Add(f);
                    trow.Cells.Add(tcell);

                }
                uploadtable.Controls.Add(trow);
                row1++;
            }
        }
    }
    private void GenerateThumbnails(Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = 100;//(int)(image.Width * 0.1);
            var newHeight = 85;// (int)(image.Height * 0.1);
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
    private void GenerateThumbnails1(Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = 448;//(int)(image.Width * 0.2);
            var newHeight = 298;//(int)(image.Height * 0.2);
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

    protected void getlist()
    {
        try
        {
            con.Open();
            cmd = new MySqlCommand("  select column_name from information_schema.columns where table_name='alumnireg' order by column_name asc", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListBox1.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "GetList method of Admin_Misc page for " + Session["loginname"] + ": " + ex.Message);
        }
    }


    protected void add_Click(object sender, EventArgs e)
    {

        while (ListBox1.SelectedIndex >= 0)
        {
            ListBox2.Items.Add(ListBox1.SelectedItem.Value);
            ListBox1.Items.Remove(ListBox1.SelectedItem.Value);
        }
    }
    protected void remove_Click(object sender, EventArgs e)
    {
        while (ListBox2.SelectedIndex >= 0)
        {
            ListBox1.Items.Add(ListBox2.SelectedItem.Value);
            ListBox2.Items.Remove(ListBox2.SelectedItem.Value);
        }

        ArrayList alItems = new ArrayList();
        foreach (ListItem lst in this.ListBox1.Items)
        {
            alItems.Add(lst.Text);
        }
        alItems.Sort();
        ListBox1.DataSource = alItems;
        ListBox1.DataBind();
      
    }

    protected void showlist_Click(object sender, EventArgs e)
    {
        GridView1.Columns.Clear();
        string col = "";
        for (int i = 0; i < ListBox2.Items.Count; i++)
        {
          
            BoundField field1 = new BoundField();
            field1.DataField = ListBox2.Items[i].Value;
            
            if (ListBox2.Items[i].Value == "dob")
            { field1.DataFormatString = "{0:yyyy-MM-dd}"; }
            
            field1.HeaderText = ListBox2.Items[i].Value.Substring(0, 1).ToUpper() + ListBox2.Items[i].Value.Substring(1);
            field1.SortExpression = ListBox2.Items[i].Value;
            GridView1.Columns.Add(field1);
             col += ListBox2.Items[i].Value + ","; 
        }

        if (!string.IsNullOrEmpty(col))
        {
            exporterr.Visible = false;
            col = col.Remove(col.Length - 1);

            try
            {
                con.Open();
                tquery = "select " + col + " from alumnireg";
                MySqlDataAdapter da = new MySqlDataAdapter("select " + col + " from alumnireg", con);

                //gds = new DataSet();
                DataTable dt = new DataTable("table1");
                //da.Fill(gds, "alumnireg");
                da.Fill(dt);
                GridView1.DataSource = dt;// gds;
                GridView1.DataBind();
                Cache["table"] = dt;
                con.Close();

            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "ShowList method of Admin_Misc page for " + Session["loginname"] + ": " + ex.Message);

            }
        }
        else
        {
            exporterr.Visible = true;
            exporterr.Text = "No Records to display";
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        showlist_Click(sender, e);
    }
    protected void exportlist_Click(object sender, EventArgs e)
    {
        
        //Get the data from database into datatable
        if (tquery != "")
        {
            exporterr.Visible = false;
            MySqlDataAdapter da = new MySqlDataAdapter(tquery, con);
            DataTable dt = new DataTable("Result");
            da.Fill(dt);

            //Create a dummy GridView
            GridView GridView1 = new GridView();

            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=AlumniList.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            exporterr.Visible = true;
            exporterr.Text = "No Fields selected.Please select fields and click 'ShowList' button then click 'Export'.";
        }      
    }
    // this is for overriding the gridview render method
    public override void VerifyRenderingInServerForm(Control control)
    {
       
        
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        e.SortDirection = SortDirection.Ascending;
        RebindData(e.SortExpression.ToString(), e.SortDirection.ToString());
    }
    private void RebindData(string sColimnName, string sSortOrder)
    {
        DataTable dt = (DataTable)Cache["table"];
        dt.DefaultView.Sort = sColimnName + " asc";

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void yrlist_Init(object sender, EventArgs e)
    {
        for (int year = 2006; year <= System.DateTime.Now.Year; year++)
        {
            yrlist.Items.Add(year.ToString());
        }
    }
   
    protected void add_annon_Click(object sender, EventArgs e)
    {
        DateTime regdate = System.DateTime.Now, origin = new DateTime(2000, 1, 1, 0, 0, 0, 0);
        TimeSpan stamp = regdate - origin;
        int aid = stamp.Days + stamp.Seconds;

        try
        {

            con.Open();
            cmd = new MySqlCommand("insert into annon(msg_id,msg,msg_date) values(@inp1,@inp2,@inp3)", con);
            cmd.Parameters.AddWithValue("inp1", aid);
            cmd.Parameters.AddWithValue("inp2", anntext.Value);
            cmd.Parameters.AddWithValue("inp3", anndate.Text);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Add_annon method of Admin_Misc Page for " + Session["loginname"] + ":" + ex.Message);
        }
        finally
        {
            succmsg.Text = "Announcement added successfully";
            anndate.Text = "";
            anntext.Value = "";
        }
    }
    protected void annon_date_TextChanged(object sender, EventArgs e)
    {

    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        showannlist_Click(sender, e);
    }
    protected void showannlist_Click(object sender, EventArgs e)
    {
        MySqlCommand cmd = new MySqlCommand("select msg_id,msg from annon where msg_date='" + annon_date.Text + "'", con);
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds, "annon");
        if (ds.Tables["annon"].Rows.Count == 0)
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
            griderr.Visible = true;
        }
        else
        {
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int row = Convert.ToInt32(GridView2.DataKeys[e.RowIndex].Value);
        Response.Write("<font color='white'>" + row + "</font");
        MySqlCommand cmd = new MySqlCommand("delete from annon where msg_id=" + row, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            showannlist_Click(sender, e);
        }
        catch (Exception ex)
        {

            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "GridView2 RowDeleting event of Admin_misc page for " + Session["loginname"] + ":" + ex.Message);
        }

    }
    protected void optpost_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (optpost.SelectedIndex == 0)
        {
            div3.Style.Add("display", "none");
            div4.Style.Add("display", "none");
            dispvalue = "none/none";
        }
        else if (optpost.SelectedIndex == 1)
        {
            div3.Style.Add("display", "block");
            div4.Style.Add("display", "none");
            dispvalue = "block/none";
        }
        else if (optpost.SelectedIndex == 2)
        {
            div4.Style.Add("float", "left");
            div4.Style.Add("display", "block");
            div3.Style.Add("display", "none");
            dispvalue = "none/block";
        }

    }
    protected void Post_message_Click(object sender, EventArgs e)
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        int indx = optpost.SelectedIndex;
        int from, to;
        string specyr = "";

        switch (indx)
        { 
            case 0:
                
                try
                {
                con.Open();
                cmd = new MySqlCommand("select email from alumnireg",con);
                dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            mail.From = new MailAddress(mid, "Alumni Admin");
                            mail.To.Add(dr.GetString("email"));
                            mail.Subject = postsub.Text;
                            mail.Body = "Dear Alumni," + "<br/><br/>" + postmsg.Value + "<br/><br/>Please do not reply to this mail.Thank you.";
                            mail.IsBodyHtml = true;

                            if (!Page.IsPostBack)
                            {
                                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                                smtp.Port = 25;
                                smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                    }
                    dr.Close();
                con.Close();
                Response.Redirect("Admin_Misc.aspx?sh=view4&snt=1");//to avoid sending mail even after page refresh
                }
                catch (SmtpException sx)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Case 0 of Post_Msg method of Admin_Misc Page for " + Session["loginname"] + ":" + sx.Message);
                }
                catch (Exception ex)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Case 0 of Post_Msg method of Admin_Misc Page for " + Session["loginname"] + ":" + ex.Message);
                }
                break;

            case 1:
                try
                {
                    from = int.Parse(fromyr.SelectedItem.Value);
                    to = int.Parse(toyr.SelectedItem.Value);

                    con.Open();
                    cmd = new MySqlCommand("select email from alumnireg where batch >=" + from + " and batch <=" + to, con);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            mail.From = new MailAddress(mid, "Alumni Admin");
                            mail.To.Add(dr.GetString("email"));
                            mail.Subject = postsub.Text;
                            mail.Body = "Dear Alumni," + "<br/><br/>" + postmsg.Value + "<br/><br/>Please do not reply to this mail.Thank you.";
                            mail.IsBodyHtml = true;

                            if (!Page.IsPostBack)
                            {
                                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                                smtp.Port = 25;
                                smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                    }
                    dr.Close();
                    con.Close();
                    Response.Redirect("Admin_Misc.aspx?sh=view4&snt=1");//to avoid sending mail even after page refresh
                }
                catch (SmtpException sx)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Case 1 of Post_Msg method of Admin_Misc Page for " + Session["loginname"] + ":" + sx.Message);
                }
                catch (Exception ex)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Case 1 of Post_Msg method of Admin_Misc Page for " + Session["loginname"] + ":" + ex.Message);
     
                }
                break;

            case 2:
                try
                {
                    for (int i = 0; i < specificyr.Items.Count; i++)
                    {
                        if (specificyr.Items[i].Selected)
                        {
                            if (specyr == "")
                            { specyr = specificyr.Items[i].Value; }
                            else
                            { specyr +=  ","+specificyr.Items[i].Value ; }
                        }
                    }
                    con.Open();
                    cmd = new MySqlCommand("select email from alumnireg where batch IN ("+specyr+")", con);
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read()) 
                        {
                            mail.From = new MailAddress(mid, "Alumni Admin");
                            mail.To.Add(dr.GetString("email"));
                            mail.Subject = postsub.Text;
                            mail.Body = "Dear Alumni," + "<br/><br/>" + postmsg.Value + "<br/><br/>Thank you.<br/><br/> Yours Sincerely,<br/>Alumni Admin";
                            mail.IsBodyHtml = true;

                         if(!Page.IsPostBack)
                         {
                            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                            smtp.Port = 25;
                            smtp.Credentials = new System.Net.NetworkCredential(mid, mpwd);
                            smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                       }
                    }
                    dr.Close();
                    con.Close();
                   Response.Redirect("Admin_Misc.aspx?sh=view4&snt=1");
                }
                catch (SmtpException sx)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Case 2 of Post_Msg method of Admin_Misc Page for " + Session["loginname"] + ":" + sx.Message);
                }
                catch (Exception ex)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Case 2 of Post_Msg method of Admin_Misc Page for " + Session["loginname"] + ":" + ex.Message);
                }
                break;
        }
    }
}