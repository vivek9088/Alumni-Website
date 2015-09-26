using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Data;
using AjaxControlToolkit;
using System.Drawing.Drawing2D;
public partial class AlumniPages_Alumni : System.Web.UI.MasterPage
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdevents,cmdrating,cmduser,cmdforum,cmdnews,cmdbatch,cmdfri;
    MySqlDataReader drevents,drrating,druser,drforum,drnews,drbatch,drfri;
    static string dispname, mailfrom, usrname, ubranch, gen, imgpath = "../Alumni_Images/", imgpath1 = "../images/",samp_list,mid,mpwd;
    string[][] list = new string[5][];
  protected int tcnt = 0, rcnt = 0;
    static int row,temp,urowid,temp1,row1,ubatch,rid,cnt_forum,cntforum;
  HyperLink link;
  HtmlTableCell td;
  HtmlTableRow tr;
  Label lbl;
  bool answer; 
  List<string> names = new List<string>();
   List<int> nameid = new List<int>();
   LinkButton fri_link;  
    protected void Page_Load(object sender, EventArgs e)
    {
    cnt_forum =cntforum= 0;
        mid=ConfigurationManager.AppSettings["mailid"];
        mpwd = ConfigurationManager.AppSettings["mailpwd"];
        Page.MaintainScrollPositionOnPostBack = true;
       
        if (Session["loginname"] == null)
        {
            reurl.Value = "3";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Your Current Session Expired..Please Login again!!!');", true);
        }
        else
        {
          try
           {
            usrname = Session["loginname"].ToString();
            
                 con.Open();
              
                 cmduser = new MySqlCommand("select id from forum_users where username='" + usrname + "'", con);
                 druser = cmduser.ExecuteReader();
                 druser.Read();
                 rid = druser.GetInt32("id");
                 druser.Close();

                cmduser = new MySqlCommand("select regid,gender,fname,lname,email,imgpath,batch,branch from alumnireg where username='" + usrname + "'", con);
                druser = cmduser.ExecuteReader();
                druser.Read();
                urowid = druser.GetInt32("regid");
                ubranch = druser.GetString("branch");
                ubatch = druser.GetInt32("batch");
                uname.Text = druser.GetString("fname").Substring(0, 1).ToUpper() + druser.GetString("fname").Substring(1).ToLower();
                gen = druser.GetString("gender").Substring(0, 1);
                if (gen == "M")
                { usrimg.Src = imgpath1 + "editm.png"; }
                else
                { usrimg.Src = imgpath1 + "editf.png"; }

                dispname = druser.GetString("fname") + " ";
                if (druser.GetString("lname").Length > 0)
                {
                    dispname += druser.GetString("lname");
                }
                mailfrom = druser.GetString("email");

                if (!druser.IsDBNull(5) && File.Exists(Server.MapPath("../" + druser.GetString("imgpath"))))
                {
                    userimg.Src = "../" + druser.GetString("imgpath");
                    delpic.Visible = true;
                }
                else
                {
                    userimg.Src = "../images/nophoto.jpg";
                    delpic.Visible = false;
                }
                
               druser.Close();
                con.Close();

                if (!Page.IsPostBack)
                {
                    showbatchmate(ubatch);
                    showlist();
                    showmsg();
                }
                  
               con.Open();
            //to get the number of forum replies for the current user
            cmdrating = new MySqlCommand("select count(subject),sum(num_replies) from forum_topics where poster='" + usrname + "'", con);
            drrating = cmdrating.ExecuteReader();
            if (drrating.Read())
            {
                topcnt.Text =drrating.IsDBNull(0)?"0":drrating.GetInt32(0).ToString();
                repcnt.Text = drrating.IsDBNull(1) ? "0" : drrating.GetInt32(1).ToString();
            }
            drrating.Close();

            //this code just used to retieve the details of a forum topic
            cmdforum = new MySqlCommand("select poster,subject,date_format(From_UnixTime(last_post),'%D %b,%Y at %r'),last_poster from forum_topics order by id desc LIMIT 5", con);
            drforum = cmdforum.ExecuteReader();

            if (drforum.HasRows)
            {
                while (drforum.Read())
                {//to store the details of a particular topic in a string and then split into array to refer easily
                samp_list = drforum.GetString("subject") + "/" + drforum.GetString("poster") + "/" + drforum.GetValue(3) + "/" + drforum.GetValue(2);
                list[cnt_forum] = samp_list.Split('/');
                cnt_forum++;
                    }
            }
            else
            {
            forumdiv.Visible = false;
                upderr.Visible = true;
            }
            drforum.Close();

            //to display the forum updates
           
            while (cntforum < cnt_forum)
                {
                cmdrating=new MySqlCommand("select imgpath from alumnireg where username='"+list[cntforum][1]+"'",con);
     drrating = cmdrating.ExecuteReader();
     if (drrating.Read())
         {
         answer = !drrating.IsDBNull(0) && File.Exists(Server.MapPath("../" + drrating.GetString("imgpath"))) ? true:false;
         flist.Controls.Add(new LiteralControl(" <li><div><img src='" + (answer == true ? "../" 
             +drrating.GetString("imgpath") :"../images/nophoto.jpg") +"' id='forumimg' runat='server' alt='' "
         +"style='float: left'/><div class='subnewsdiv' id='marquee1' runat='server'><a title='"
         + list[cntforum][0] + "' href='http://ssnunite.com/PeopleTalk'>" + (list[cntforum][0].Length > 20 ? list[cntforum][0].Substring(0, 20)
         +".." : list[cntforum][0]) + "</a>&nbsp;by " + list[cntforum][1] + "<br/>Last Post by " + list[cntforum][2]
         + "<br/>on " + list[cntforum][3] + "</div></div></li>"));
         }
     else
     {
     flist.Controls.Add(new LiteralControl(" <li><div><img src='../images/nophoto.jpg' id='forumimg' runat='server' alt='' style='float: left'/><div class='subnewsdiv' id='marquee1' runat='server'><a title='" + list[cntforum][0] + "' href='http://ssnunite.com/PeopleTalk'>"+ (list[cntforum][0].Length > 30 ? list[cntforum][0].Substring(0, 30) : list[cntforum][0]) + "</a>&nbsp;by " + list[cntforum][1] + "<br/>Last Post by " + list[cntforum][2] + "<br/>on " + list[cntforum][3] + "</div></div></li>"));
         }
     drrating.Close(); 
     cntforum++;
         }
    
               //to display the news updates
            cmdnews = new MySqlCommand("select newsid,headline from news order by rowid desc LIMIT 5", con);
            drnews = cmdnews.ExecuteReader();
            if (drnews.HasRows)
            {
                while (drnews.Read())
                {
                    link = new HyperLink();
                    link.Text = drnews.GetString("headline").Substring(0, 20) + "...<img src='" + imgpath1 + "new.gif' alt='No Image Display'/><br/><br/>";
                   // link.ToolTip = drnews.GetString("headline").Substring(0,Convert.ToInt32(drnews.GetString("headline").Length)/2+10)+"..";
                    link.ToolTip = drnews.GetString("headline");
                    link.NavigateUrl = "NewsRead.aspx?news_id=" + drnews.GetString("newsid");
                    marquee2.Controls.Add(link);
                }
                
            }
            else
            {
                marquee2.Visible = false;
                newserr.Visible = true;
            }
            drnews.Close();
            con.Close();
         
               }
                catch (Exception ex)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page load of Alumni Master for " + Session["loginname"] + ":" + ex.Message);
                }
        }
    }
  
    //this method is to display all the messages for a particular user when logged in
    protected void showmsg()
    {
        string msg_date="";
        DateTime d;
       int len = 0;
       try
       {
           con.Open();

           cmdnews = new MySqlCommand("select msg_from,msg,msg_date from message where msg_from='" + usrname + "' and msg_date >= (select login_prev from login_hist where login_name='"+usrname+"') order by rowid desc", con);
           drnews = cmdnews.ExecuteReader();
           
           if (drnews.HasRows)
           {
              while (drnews.Read())
               {
                   len = drnews.GetString("msg").Length;
                   d=Convert.ToDateTime(drnews.GetString("msg_date"));
                   msg_date =GetPrettyDate(d);
                    
                   if (len <= 40)
                   {
                       detboxmsg.Controls.Add(new LiteralControl(" <div id=\"main-feed\" class=\"cfix\"><section id=\"sec\">"
                        + "<div class=\"mini-feed\"><ul><li>"
                        + "<img src=\"../Alumni_images/4.png\" class=\"tweet-avatar\" title=\"mattaussaguel\" height=\"48\" width=\"48\">"
                        + "<span><b style=\"color:#f24c15\">" + drnews.GetString("msg_from") + "</b>: " + drnews.GetString("msg")
                        + "</span><span class=\"tweet-time\">"+msg_date+"</span>"
                        +"</li></ul></div></section></div><br/><br/>"));
                       //detboxmsg.Controls.Add(new LiteralControl("<div class='divmsg'><img src='../images/msgpic.png' alt=''/>&nbsp;<b>" + drnews.GetString("msg_from") + ":</b>&nbsp;&nbsp;" + drnews.GetString("msg") + "<br/>about 1 day ago</div><br />"));
                   }
                   else
                   {
                       detboxmsg.Controls.Add(new LiteralControl(" <div id=\"main-feed\" class=\"cfix\"><section id=\"sec\">"
                        + "<div class=\"mini-feed\"><ul><li>"
                        + "<img src=\"../Alumni_images/4.png\" class=\"tweet-avatar\" title=\"mattaussaguel\" height=\"48\" width=\"48\">"
                        + "<span><b style=\"color:#f24c15\">" + drnews.GetString("msg_from") + "</b>: " + drnews.GetString("msg").Substring(0,40) +"<br/>" + drnews.GetString("msg").Substring(41)
                        + "</span><span class=\"tweet-time\">"+msg_date+"</span></li></ul></div></section></div><br/><br/>"));
                       //detboxmsg.Controls.Add(new LiteralControl("<div class='divmsg'><img src='../images/msgpic.png' alt=''/>&nbsp;<b>" + drnews.GetString("msg_from") + ":</b>&nbsp;&nbsp;" + drnews.GetString("msg") + "<br/>about 1 day ago</div><br />"));
                   }
                  }
           }
           else
           {
               detboxmsg.Controls.Add(new LiteralControl("No Messages"));
           }
           drnews.Close();
           con.Close();
       }
       catch (Exception ex)
       {
           CreateLogFile log = new CreateLogFile();
           log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Showmsg method of Alumni Master for " + Session["loginname"] + ":" + ex.Message);
       }
    }

    // this calculates the minutes,seconds,days ago of a message
    static string GetPrettyDate(DateTime d)
    {
	// 1.Get time span elapsed since the date.
	TimeSpan s = DateTime.Now.Subtract(d);

	// 2.Get total number of days elapsed.
	int dayDiff = (int)s.TotalDays;

	// 3.Get total number of seconds elapsed.
	int secDiff = (int)s.TotalSeconds;

	// 4.Don't allow out of range values.
	if (dayDiff < 0 || dayDiff >= 31)
	{
	    return null;
	}

	// 5.Handle same-day times.
	if (dayDiff == 0)
	{
	    // A.Less than one minute ago.
	    if (secDiff < 60)
	    {
		return "just now";
	    }
	    // B.Less than 2 minutes ago.
	    if (secDiff < 120)
	    {
		return "1 minute ago";
	    }
	    // C.Less than one hour ago.
	    if (secDiff < 3600)
	    {
		return string.Format("{0} minutes ago",
		    Math.Floor((double)secDiff / 60));
	    }
	    // D.Less than 2 hours ago.
	    if (secDiff < 7200)
	    {
		return "1 hour ago";
	    }
	    // E.Less than one day ago.
	    if (secDiff < 86400)
	    {
		return string.Format("{0} hours ago",
		    Math.Floor((double)secDiff / 3600));
	    }
	}
	// 6.Handle previous days.
	if (dayDiff == 1)
	{
	    return "yesterday";
	}
	if (dayDiff < 7)
	{
	    return string.Format("{0} days ago",dayDiff);
	}
	if (dayDiff < 31)
	{
	    return string.Format("{0} weeks ago",
		Math.Ceiling((double)dayDiff / 7));
	}
	return null;
    }

    //this is for displaying batchmate list    
    protected void showbatchmate(int batyear)
    {
        row = 0; temp = 0;
        names.Clear();
        nameid.Clear();
        //batchyear.SelectedValue =batyear.ToString();
       
       try
        {
            con.Open();
            cmdbatch = new MySqlCommand("select f.username,f.id from forum_users f,alumnireg n where n.batch=" + batyear + " and f.username=n.username and n.branch='"+ubranch+"' and f.username NOT IN ('" + usrname + "')", con);
           
            drbatch = cmdbatch.ExecuteReader();
           
            if (drbatch.HasRows)
            {
                //read all the name's and id's of that batch
                while (drbatch.Read())
                {
                    
                    names.Add(drbatch.GetString("username"));
                    nameid.Add(drbatch.GetInt32("id"));
                }
                //generate photo of those names who are in the batch
                while (temp < names.Count)
                {
                    tr = new HtmlTableRow();
                    for (int col = 0; col < 4; col++)
                    {
                        td = new HtmlTableCell();
                        if (!File.Exists(Server.MapPath(imgpath + nameid[row] + ".png")))
                        {
                            td.Controls.Add(new LiteralControl("<figure><a title='"+names[row]+"' href='AlumniProfHome.aspx?vid=" +encodepwd(names[row]) + "&myid=" + nameid[row] + "&toid=" + rid + "'><img src='../images/nophoto.jpg' height='30' width='25' /></a></figure>"));
                        }
                        else
                        {
                            td.Controls.Add(new LiteralControl("<figure><a title='" + names[row] + "' href='AlumniProfHome.aspx?vid=" + encodepwd(names[row]) + "&myid=" + nameid[row] + "&toid=" + rid + "'><img src='../Alumni_Images/" + nameid[row] + ".png' height='30' width='25' /></a></figure>"));
                        }
                        tr.Controls.Add(td);
                        row++;
                        if (row > names.Count - 1)
                        {
                            break;
                        }
                    }
                    temp = row;
                    batchmate.Rows.Add(tr);
                }
                batchmsg.Visible = false;
                sectiontab1.Style.Add("display", "block");
            }
            else
            {
                batchmsg.Visible = true;
                sectiontab1.Style.Add("display", "none");
            }
            drbatch.Close();
            con.Close();
       }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Show Batchmate method of Alumni Master for " + Session["loginname"] + ":" + ex.Message);
             
        }
      
    }
      
    protected void showlist()
    {
        row1 = 0;
        temp1 = 0;
        names.Clear();
        nameid.Clear();
        MySqlCommand cmdfribat;  
        //this is for displaying friend list
        try
        {
            con.Open();
            cmdfribat = new MySqlCommand("select req_to,req_to_id from user_friends where req_from='" + usrname + "' and req_from_id=" + rid + " and accept=1 and req_to not in ('" + usrname + "')", con);
            drbatch = cmdfribat.ExecuteReader();
            //generate friend list of the user who logged in.
            if (drbatch.HasRows)
            {
                while (drbatch.Read())
                {
                    names.Add(drbatch.GetString("req_to"));
                    nameid.Add(drbatch.GetInt32("req_to_id"));
                }

                while (temp1 < names.Count)
                {
                    tr = new HtmlTableRow();
                    for (int col = 0; col < 4; col++)
                    {
                        td = new HtmlTableCell();
                        if (!File.Exists(Server.MapPath(imgpath + nameid[row1] + ".png")))
                        {
                            td.Controls.Add(new LiteralControl("<figure><a title='" + names[row1] + "' href='AlumniProfHome.aspx?vid=" + encodepwd(names[row1]) + "&myid=" + nameid[row1] + "&toid=" + rid + "'><img src='../images/nophoto.jpg' height='30' width='25' /></a></figure>"));
                        }
                        else
                        {
                            td.Controls.Add(new LiteralControl("<figure><a title='" + names[row1] + "' href='AlumniProfHome.aspx?vid=" + encodepwd(names[row1]) + "&myid=" + nameid[row1] + "&toid=" + rid + "'><img src='../Alumni_Images/" + nameid[row1] + ".png' height='30' width='25' /></a></figure>"));
                        }
                        tr.Controls.Add(td);
                        row1++;
                        if (row1 > names.Count - 1)
                        {
                            break;
                        }
                    }
                    temp1 = row1;
                    fri_list.Rows.Add(tr);
                }
                buddymsg.Visible = false;
                sectiontab.Style.Add("display", "block");
            }
            else
            {
                buddymsg.Visible = true;
                sectiontab.Style.Add("display", "none");
            }
            drbatch.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile log = new CreateLogFile();
            log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Show List method of Alumni Master for " + Session["loginname"] + ":" + ex.Message);
             
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
    protected void send_Click(object sender, EventArgs e)
    {
        MailMessage invite = new MailMessage();
        
        string[] namelist = fri_name.Text.Split(';');
        string[] maillist = fri_mail.Text.Split(';');

        if (namelist.Length > 5)
        {
            errmsg.Text = "Max 5 Names only allowed";
            fri_name.Focus();
        }
        else if (maillist.Length > 5)
        {
            errmsg.Text = "Max 5 Email ID only allowed";
            fri_mail.Focus();
        }
        else
        {
            errmsg.Text = "";
            try
            {
                for (int cnt = 0; cnt < 5; cnt++)
                {
                    invite.From = new MailAddress(mailfrom, dispname);
                    invite.To.Add(maillist[cnt].Trim());
                    invite.Subject = "Invite From Friend";
                    invite.Body = "Dear " + namelist[cnt].Trim() + "," + "<br/>";
                    invite.Body += "I just wanted to inform you that SSN has come up with an Alumni site.<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thought it would be a good idea if you could look it up.";
                    invite.Body += "<br/>" + "<a href='http://ssnunite.com/index.aspx'>Click here to visit the SSN alumni site</a>" + "<br/><br/><br/>";
                    invite.Body += "Warm Regards" + "<br/><br/>" + dispname;
                    invite.IsBodyHtml = true;
                 
                    if (!Page.IsPostBack)
                    {
                        SmtpClient mailsend = new SmtpClient();
                        mailsend.Host = "smtp.gmail.com";
                        mailsend.Port = 25;
                        mailsend.Credentials = new System.Net.NetworkCredential(mid,mpwd);
                        mailsend.EnableSsl = true;
                        mailsend.Send(invite);
                    }
                }
            }
            catch (SmtpException sx)
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "DoSomething('Due to some server problem mail has not been delivered successfully. Please try after sometime. Thank You!!!');</script>", true);
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Send method of Alumni Master for " + Session["loginname"] + ":" + sx.Message);

            }
            catch (Exception ex)
            {
                CreateLogFile log = new CreateLogFile();
                log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Send method of Alumni Master for " + Session["loginname"] + ":" + ex.Message);
            
            }
         }
      }
  
    protected void editimage_Click(object sender, EventArgs e)
    {
        string filename = "", path, qry = "", type = Path.GetExtension(FileUpload1.PostedFile.FileName);
        HttpPostedFile imgfile = FileUpload1.PostedFile;

       
            if (type == "")
            { photo.Text = "<font color='red'>Please Upload an Image..</font>"; }
            else if (imgfile.ContentLength > 1048576)//file size must be less than 1mb
            {
                photo.Text = "<font color='red'>Image size must be less than 1 mb..</font>";
            }
            else
            {
                try
                {
                    con.Open();
                    cmduser = new MySqlCommand("select imgpath from alumnireg where regid=" + urowid, con);
                    druser = cmduser.ExecuteReader();
                    druser.Read();

                    if (druser.IsDBNull(0) || string.IsNullOrEmpty(druser.GetString("imgpath")))
                    {
                        qry = "update alumnireg set imgpath='Alumni_Images/" + rid + ".png' where regid=" + urowid;
                        filename = "Alumni_Images/" + rid + ".png";
                        path = Server.MapPath("../"+filename);
                    }
                    else
                    {
                        filename = druser.GetString("imgpath");
                        path = Server.MapPath("../" + filename);
                        qry = "update alumnireg set imgpath='" + filename + "' where regid=" + urowid;

                    }

                    GenerateThumbnails(FileUpload1.PostedFile.InputStream, path);
                    //FileUpload1.PostedFile.SaveAs(path);

                    druser.Close();

                    if (qry != "")
                    {
                        cmduser = new MySqlCommand(qry, con);
                        cmduser.ExecuteNonQuery();
                    }
                    con.Close();

                    userimg.Src = filename;
                    Response.Redirect("AlumniProfHome.aspx");
                    
                }
                catch (Exception ex)
                {
                    CreateLogFile log = new CreateLogFile();
                    log.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Edit Image method of Alumni Master for " + Session["loginname"] + ":" + ex.Message);

                }
            }
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OnLoad", "showme();", true);
    }

    //image compression takes place here
    private void GenerateThumbnails(Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = 100; //(int)(image.Width * scaleFactor);
            var newHeight = 100;//(int)(image.Height * scaleFactor);
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

    protected void delpic_Click(object sender, EventArgs e)
    {
        con.Open();
        if (File.Exists(Server.MapPath("../Alumni_Images/" + rid + ".png")))
        {
            File.Delete(Server.MapPath("../Alumni_Images/" + rid + ".png"));
        }
        cmduser=new MySqlCommand("update alumnireg set imgpath='' where regid="+urowid,con);
        cmduser.ExecuteNonQuery();
        con.Close();
        delpic.Visible = false;
        userimg.Src = "../images/nophoto.jpg";
    }
    
}
