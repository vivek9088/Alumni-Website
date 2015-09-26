using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Security;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
public partial class RegisterForm : System.Web.UI.Page
{
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdaludb, cmdlogdb, cmduser,cmdnewdb;
    MySqlDataReader druser;
    public string res, imgname, rcterr,degerr,brerr,generr,baterr;
    static string bkdob = "",userimg,imgsave,temp,stringIpAddress;
    static List<string> clist = new List<string>();
    static int a, b, c, d;
    Random rnd = new Random();
    DateTime regdate = System.DateTime.Now, origin = new DateTime(2000, 1, 1, 0, 0, 0, 0);
    static string time_stamp;
    TimeSpan stamp;
    

    protected void Page_Init(object sender, EventArgs e)
{
    StreamReader read = File.OpenText(Server.MapPath("companylist.txt"));
    while ((temp = read.ReadLine()) != null)
           {
               if (!clist.Contains(temp))
               {
                   clist.Add(temp);
               }
           }
           read.Close();
        num1.Text=new Random().Next(1,10).ToString();
        num2.Text =  new Random().Next(int.Parse(num1.Text)+1, 10).ToString();

}
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Page.IsPostBack)
        {
           
             if (!(String.IsNullOrEmpty(password.Text.Trim())) || !(String.IsNullOrEmpty(confirmpass.Text.Trim())))
            {
                password.Attributes["value"] = password.Text;
                confirmpass.Attributes["value"] = confirmpass.Text;
            }
            password.Text = password.Attributes["value"];
            confirmpass.Text = confirmpass.Attributes["value"];

        }
       if (!Page.IsPostBack)
       {
            stamp = regdate - origin;
           time_stamp = stamp.Days.ToString() + stamp.Milliseconds.ToString();
       }
    }
    
    protected void batch_Init(object sender, EventArgs e)
    {
        batch.Items.Add("-- Select a Batch --");
        for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
        {
            batch.Items.Add(yr.ToString());
        }
    }
    protected void country_Init(object sender, EventArgs e)
    {
        string indpath = Server.MapPath("countrystate/countrieslist.txt");
        FileStream fs;
        StreamReader read;
        string line;

        country.Items.Add("--Select a location--");
        cmp_loc.Items.Add("--Select a location--");
        prev_loc.Items.Add("--Select a location--");

        fs = new FileStream(indpath, FileMode.Open, FileAccess.Read);
        read = new StreamReader(fs);
        while ((line = read.ReadLine()) != null)
        {
            country.Items.Add(line);
            cmp_loc.Items.Add(line);
            prev_loc.Items.Add(line);
        }
        read.Close();
        fs.Close(); 

    }
    protected void degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (degree.SelectedItem.Value == "PG")
        {
            branchug.Visible = false;
            branchpg.Visible = true;
            uglab.Visible = false;
            pglab.Visible = true;
            branchpg.Focus();
        }
        else if (degree.SelectedItem.Value == "UG")
        {
            branchug.Visible = true;
            branchpg.Visible = false;
            uglab.Visible = true;
            pglab.Visible = false;
            branchug.Focus();
        }
    }

    protected void upload_Click(object sender, EventArgs e)
    {
            string type = Path.GetExtension(photoupload.PostedFile.FileName),save;//,save1;
            HttpPostedFile imgfile = photoupload.PostedFile;
           
            if (type == "")
            {
                photo.Text = "<font color='red'>Please Upload an Image..</font>";

            }
            else if (imgfile.ContentLength > 1048576)//file size must be less than 1mb
            {
                photo.Text = "<font color='red'>Image size must be less than 1 mb..</font>";

            }
            else if (type == ".JPG" || type == ".jpg" || type == ".JPEG" || type == ".jpeg" || type == ".PNG" || type == ".png")
            {
                imgsave = "yes";
                userimg =time_stamp+".png";

                save = Server.MapPath("Alumni_Images/" +userimg);
                GenerateThumbnails(imgfile.InputStream, save);

                photo.Text = "<font color='green'>Image</font><font color='black'> "+photoupload.FileName+"</font><font color='green'> was uploaded successfully</font>";
                showimg.ImageUrl = "Alumni_Images/" + userimg;
            }
            else
            {
                photo.Text = "<font color='red'>Upload Failed..Only Jpg or Png Images Allowed.</font>";

            }
        }

    //image compression takes place here
    private void GenerateThumbnails(Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth =100; //(int)(image.Width * scaleFactor);
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

    protected void day_Init(object sender, EventArgs e)
    {
        day.Items.Add("Day");
            for (int dy = 1; dy <= 31; dy++)
            {
                day.Items.Add(dy.ToString());
            }
         }
    protected void month_Init(object sender, EventArgs e)
    {
         month.Items.Add("Month");
            for (int yr = 1; yr <= 12; yr++)
            {
                month.Items.Add(yr.ToString());
            }
      
    }
    protected void year_Init(object sender, EventArgs e)
    {
          year.Items.Add("Year");
            for (int yr = 1970; yr <= System.DateTime.Now.Year; yr++)
            {
                year.Items.Add(yr.ToString());
            }
        }

    protected void startmon_Init(object sender, EventArgs e)
    {
         startmon.Items.Add("Month");
            for (int yr = 1; yr <= 12; yr++)
            {
                startmon.Items.Add(yr.ToString());
            }
       
    }
    protected void startyr_Init(object sender, EventArgs e)
    { startyr.Items.Add("Year");

            for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
            {
                startyr.Items.Add(yr.ToString());
            }
         }
       
    protected void leavemon_Init(object sender, EventArgs e)
    {
                 leavemon.Items.Add("Month");
            for (int yr = 1; yr <= 12; yr++)
            {
                leavemon.Items.Add(yr.ToString());
            }
       
    }
    protected void leaveyr_Init(object sender, EventArgs e)
    {
           leaveyr.Items.Add("Year");
            for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
            {
                leaveyr.Items.Add(yr.ToString());
            }
          }
            
        protected void joinmon_Init(object sender, EventArgs e)
    {
      

            joinmon.Items.Add("Month");
            for (int yr = 1; yr <= 12; yr++)
            {
                joinmon.Items.Add(yr.ToString());
            }
        
    }
    protected void joinyr_Init(object sender, EventArgs e)
    {
        

            joinyr.Items.Add("Year");
            for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
            {
                joinyr.Items.Add(yr.ToString());
            }
        
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
       
        DateTime dt, dt1, startdt, enddt;
        TimeSpan ts, ts1;
        decimal cur_yr;
        int yrnow = System.DateTime.Now.Year;
        char[] chars = "abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string auth_code = string.Empty,prev_yr="-",visible="";
        Random random = new Random();
        
        visible = (num_visi.SelectedIndex == 0) ? "1" + ((addr_visi.SelectedIndex == 0) ? "1" : "2") : "2" +((addr_visi.SelectedIndex == 0) ? "1" : "2");

        for (int i = 0; i < 5; i++)
        {
            int x = random.Next(1, chars.Length);
            //Don't Allow Repetation of Characters
            if (!auth_code.Contains(chars.GetValue(x).ToString()))
                auth_code += chars.GetValue(x);
            else
                i--;
        }

        dt = Convert.ToDateTime(joinyr.SelectedItem.Value + "-" + joinmon.SelectedItem.Value);
        dt1 = System.DateTime.Now.Date;
        ts =dt1 - dt;
        cur_yr =Math.Abs(Math.Round(Convert.ToDecimal(ts.Days) / 30,0));

        if (startyr.SelectedIndex > 0 && startmon.SelectedIndex > 0 && leaveyr.SelectedIndex > 0 && leavemon.SelectedIndex > 0)
        {
            startdt = Convert.ToDateTime(startyr.SelectedItem.Value + "-" + startmon.SelectedItem.Value);
            enddt = Convert.ToDateTime(leaveyr.SelectedItem.Value + "-" + leavemon.SelectedItem.Value);
            ts1 = enddt - startdt;
            prev_yr =Math.Abs(Math.Round(Convert.ToDecimal(ts1.Days) / 30,0)).ToString();
        }
         if (!(int.Parse(numres.Text) == (int.Parse(num1.Text) + int.Parse(num2.Text))))
        {
            numerr.Visible = true;
            numerr.ForeColor = Color.Red;
            numerr.Text = "Wrong Answer!!!";
            numres.Text = "";
            numres.Focus();
          
        }
        else
        {
            numerr.Visible = true;
            numerr.ForeColor = Color.Green;
            numerr.Text = "Correct Answer!!!";

            bkdob = year.SelectedItem.Value + "-" + month.SelectedItem.Value + "-" + day.SelectedItem.Value;
            try
            {
                con.Open();

                //this is to insert into Forum database
                //to get the ip address of a system
                stringIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (stringIpAddress == null) //may be the HTTP_X_FORWARDED_FOR is null
                {
                    stringIpAddress = Request.ServerVariables["REMOTE_ADDR"];//we can use REMOTE_ADDR
                }
                cmdnewdb = new MySqlCommand("insert into forum_users(regid,username,password,email,registration_ip,registered,group_id,notify_with_post,auto_notify) values(@aluid,@name1,password(@pass2),@mail1,@ip,@reg,3,1,1)", con);
                cmdnewdb.Parameters.AddWithValue("aluid", int.Parse(time_stamp));
                cmdnewdb.Parameters.AddWithValue("name1", username.Text.Trim());
                cmdnewdb.Parameters.AddWithValue("pass2", confirmpass.Text.Trim());
                cmdnewdb.Parameters.AddWithValue("mail1", emailid.Text.Trim());
                cmdnewdb.Parameters.AddWithValue("ip", stringIpAddress);
                cmdnewdb.Parameters.AddWithValue("reg", regdate);
                cmdnewdb.ExecuteNonQuery();

                //this is to insert into alumni database
                cmdaludb = new MySqlCommand("insert into alumnireg(regid,role,fname,lname,gender,dob,email,username,degree,branch,batch,org,desig,country,state,city,zipcode,number,address,regdate,expyrs,cmploc,auth_code,imgpath,visibility) values(@aluid,@urole,@firname,@lasname,@gen,@udob,@uemail,@user,@udegree,@ubranch,@ubatch,@uorg,@udesig,@cntry,@ustate,@ucity,@code,@num,@uaddress1,@dt,@yrs,@cloc,@acode,@image,@visi)", con);
                cmdaludb.Parameters.AddWithValue("@aluid",int.Parse(time_stamp));
                cmdaludb.Parameters.AddWithValue("@firname", firstname.Text.Trim().ToLower());
                cmdaludb.Parameters.AddWithValue("@lasname", lastname.Text.Trim().ToLower());
                cmdaludb.Parameters.AddWithValue("@gen", gender.SelectedItem.Value);
                cmdaludb.Parameters.AddWithValue("@udob", string.IsNullOrEmpty(bkdob)?"0000-00-00":bkdob);
                cmdaludb.Parameters.AddWithValue("@uemail", emailid.Text.Trim());
                cmdaludb.Parameters.AddWithValue("@user", username.Text.Trim());
                cmdaludb.Parameters.AddWithValue("@ubranch",branchug.Visible==true?branchug.SelectedItem.Value:branchpg.SelectedItem.Value);

                string torg = string.IsNullOrEmpty(org.Text.Trim()) ? "-" : org.Text.Trim(), tporg = string.IsNullOrEmpty(prevorg.Text.Trim()) ? "-" : prevorg.Text.Trim();
                string tdesig = string.IsNullOrEmpty(desig.Text.Trim()) ? "-" : desig.Text.Trim(), tpdesig = string.IsNullOrEmpty(prevdesig.Text.Trim()) ? "-" : prevdesig.Text.Trim();
                string tcloc = (cmp_loc.SelectedIndex == 0) ? "-" : cmp_loc.SelectedItem.Value, tploc = (prev_loc.SelectedIndex == 0) ? "-" : prev_loc.SelectedItem.Value;
                cmdaludb.Parameters.AddWithValue("@acode", auth_code);
                cmdaludb.Parameters.AddWithValue("@uorg", torg + "/" + tporg + "/-/-");
                cmdaludb.Parameters.AddWithValue("@udesig", tdesig + "/" + tpdesig + "/-/-");
                cmdaludb.Parameters.AddWithValue("@cloc", tcloc + "/" + tploc + "/-/-");
                cmdaludb.Parameters.AddWithValue("@yrs", cur_yr + "/" + prev_yr + "/-/-");
                cmdaludb.Parameters.AddWithValue("@udegree", degree.Items[degree.SelectedIndex].Value);
                cmdaludb.Parameters.AddWithValue("@num", number.Text);
                cmdaludb.Parameters.AddWithValue("@uaddress1", address.Text);
                cmdaludb.Parameters.AddWithValue("@code", zipcode.Text);
                cmdaludb.Parameters.AddWithValue("@cntry", country.SelectedItem.Value);
                cmdaludb.Parameters.AddWithValue("@ucity", city.Text);
                cmdaludb.Parameters.AddWithValue("@ustate", state.Text);
                cmdaludb.Parameters.AddWithValue("@ubatch", batch.SelectedItem.Value);
                cmdaludb.Parameters.AddWithValue("@urole", "Alumni");
                cmdaludb.Parameters.AddWithValue("@dt", regdate.ToString("yyyy-MM-dd hh:mm:ss"));
                cmdaludb.Parameters.AddWithValue("@image", string.IsNullOrEmpty(userimg)?"":"Alumni_Images/" + userimg);
                cmdaludb.Parameters.AddWithValue("@visi", visible);

                // executes query to store details of alumni in database
                cmdaludb.ExecuteNonQuery();

                
                // this is to insert into login database 
                cmdlogdb = new MySqlCommand("insert into login(regid,username,logentry,emailid,role,tri_det) values(@aluid,@usernm,@entry,@mailid,@urole,0)", con);
                cmdlogdb.Parameters.AddWithValue("@aluid", int.Parse(time_stamp));
                cmdlogdb.Parameters.AddWithValue("@usernm", username.Text.Trim());
                cmdlogdb.Parameters.AddWithValue("@entry", 1);
                cmdlogdb.Parameters.AddWithValue("@mailid", emailid.Text.Trim());
                cmdlogdb.Parameters.AddWithValue("@urole", "Alumni");
                cmdlogdb.ExecuteNonQuery();// execute query to store username and password in login DB
                con.Close();
              
               Response.Redirect("RegisterSuccess.aspx?tst_id=" + time_stamp);
            }
            catch (Exception ex)
            {

                CreateLogFile Err = new CreateLogFile();
                Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), "Submit method of Register Page for "+ firstname.Text+lastname.Text+" :" + ex.Message);
            }
        }
            }

    protected void username_TextChanged(object sender, EventArgs e)
    {
        status.Visible = true;
        if (username.Text.Trim() != "")
        {
            if (username.Text.ToLower() == "admin"||username.Text.ToLower() == "root")
            {
                status.Text = "<font color='red'>Cannot use this name as username..Sorry</font>";
                username.Focus();
            }
            else if (username.Text.Contains("@"))
            {
                status.Text = "<font color='red'>Username cannot contain @ symbol</font>";
                username.Focus();
            }
            else if (username.Text.Contains(" "))
            {
        
                status.Text = "<font color='red'>Username cannot contain spaces between words</font>";
                username.Focus();
            }
            else
            {
                con.Open();
                cmduser = new MySqlCommand("select username from login where username=@user1", con);
                cmduser.Parameters.AddWithValue("@user1", username.Text.Trim());
                druser = cmduser.ExecuteReader();
                druser.Read();
                if (druser.HasRows)  // if the username is already in database then return true
                {
                    status.Text = "<font color='red'>Username Not Available</font>";
                    username.Focus();
                }
                else
                {
                    status.Text = "<font color='green'>Username is Available</font>";
                    password.Focus();
                }

                druser.Close();
                con.Close();
            }
        }//first if ends here
        else //outer if's else
        {
            status.Text = "<font color='red'>Please enter an Username</font";
            username.Focus();
        }
    }
   
    protected void BackLink_Click(object sender, EventArgs e)
    {
        if (BackLink.Text == "Back to Step1")
        {
                BackLink.Visible = false;
                table1.Style.Add("display", "block");
                table2.Style.Add("display", "none");
                table3.Style.Add("display", "none");
                ForwardLink.Text = "Proceed to Step2";
                ForwardLink.ValidationGroup = "first";
            
        }
        else if (BackLink.Text == "Back to Step2")
        {
            if (imgsave == "no")
            {
                photo.Visible = true;
                photo.Text = "<font color='red'>Please Upload an Image..</font>";
            }
            else
            {
                photo.Visible = false;
                BackLink.Visible = true;
                ForwardLink.Visible = true;
                table1.Style.Add("display", "none");
                table2.Style.Add("display", "block");
                table3.Style.Add("display", "none");
                ForwardLink.Text = "Proceed to Step3";
                ForwardLink.ValidationGroup = "second";
                BackLink.ValidationGroup = "third";
                BackLink.Text = "Back to Step1";
                           }
        }
    }

    protected void branchug_Init(object sender, EventArgs e)
    {
        branchug.Items.Clear();
        branchug.Items.Add("--Select a branch--");
        branchug.Items.Add("B.E (EEE)");
        branchug.Items.Add("B.E (CSE)");
        branchug.Items.Add("B.E (ECE)");
        branchug.Items.Add("B.E (BME)");
        branchug.Items.Add("B.E (ME)");
        branchug.Items.Add("B.Tech (IT)");
        branchug.Items.Add("B.Tech (Chemical)");
    }
    protected void branchpg_Init(object sender, EventArgs e)
    {
        branchpg.Items.Clear();
        branchpg.Items.Add("--Select a branch--");
        branchpg.Items.Add("MCA");
        branchpg.Items.Add("MBA");
        branchpg.Items.Add("M.E (Communication Systems)");
        branchpg.Items.Add("M.E (CSE)");
        branchpg.Items.Add("M.E (Applied Electronics)");
        branchpg.Items.Add("M.E (Power Electronics & Drives)");
        branchpg.Items.Add("M.E (Computer and Communication)");
        branchpg.Items.Add("M.E. in VLSI Design (ECE Department)");
        branchpg.Items.Add("M.E. in SE (CSE Department)");
        branchpg.Items.Add("MSC. IT");
    }
    protected void ForwardLink_Click(object sender, EventArgs e)
    {
       int dobyr=Convert.ToInt16(year.SelectedItem.Value),dobmn=Convert.ToInt16(month.SelectedItem.Value),dobdy=Convert.ToInt16(day.SelectedItem.Value),totdy=DateTime.DaysInMonth(dobyr,dobmn);
       
        if (ForwardLink.Text == "Proceed to Step2")
            {
            if(dobdy>totdy)
            {
            doberr.Visible=true;
            doberr.Text="Please select correct DOB";
            }
            else if ((branchpg.Visible == true && branchpg.SelectedIndex == 0) || (branchug.Visible == true && branchug.SelectedIndex == 0))
            {
                brancherr.Visible = true;
                brancherr.Text = "Please a select a branch";
                doberr.Visible = false;
            }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "Message('Please update your profile with Personal,Educational and Work Details once you login into the site. Thank you!!! ');", true);
                    confirmpass.Attributes.Add("onchange", "if(checkEmptyValue(this)) {Message('Below information are collected as inputs for your profile in job portal. Please update your profile with previous job details. Thank you!!! '); }");
                    doberr.Visible = false;
                    brancherr.Visible = false;
                    ForwardLink.ValidationGroup = "second";
                    ForwardLink.Text = "Proceed to Step3";
                    table1.Style.Add("display", "none");
                    table2.Style.Add("display", "block");
                    table3.Style.Add("display", "none");
                    BackLink.Visible = true;
                    BackLink.Text = "Back to Step1";
                }
            }
            else if (ForwardLink.Text == "Proceed to Step3")
            {
               
            if (joinmon.SelectedIndex == 0 || joinyr.SelectedIndex == 0)
            {
                joinerr.Visible = true;
                joinerr.Text = "Please select correct Date Format";
            }
            else
            {
                joinerr.Visible = false;
                ForwardLink.Visible = false;
                table1.Style.Add("display", "none");
                table2.Style.Add("display", "none");
                table3.Style.Add("display", "block");
                BackLink.Visible = true;
                BackLink.Text = "Back to Step2";
                ForwardLink.ValidationGroup = "second";
            }
        }
    }
   
    protected void emailid_TextChanged(object sender, EventArgs e)
    {
        emailerr.Visible = true;
        if (emailid.Text.Trim() != "")
        {
            if (emailid.Text.Contains(" "))
            {
                emailerr.ForeColor = Color.Red;
                emailerr.Text = "<font color='red'>Email-Id cannot contain spaces between words</font>";
                emailid.Focus();
            }
            else
            {
                con.Open();
                cmduser = new MySqlCommand("select emailid from login where emailid=@user1", con);
                cmduser.Parameters.AddWithValue("@user1", emailid.Text.Trim());
                druser = cmduser.ExecuteReader();
                druser.Read();
                if (druser.HasRows)  // if the emailid is already in database then return true
                {
                    emailerr.ForeColor = Color.Red;
                    emailerr.Text = "Email-Id Not Available";
		    emailid.Text="";
                    emailid.Focus();
                }
                else
                {
                    emailerr.ForeColor = Color.Green;
                    emailerr.Text = "Email-Id is Available";
                   username.Focus();
                }

                druser.Close();
                con.Close();
            }
        }//first if ends here
        else //outer if's else
        {
            emailerr.ForeColor = Color.Red;
            emailerr.Text = "Please enter an Email-Id";
            emailid.Focus();

        }
    }
    
       
}