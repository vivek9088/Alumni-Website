using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class AlumniProfile : System.Web.UI.Page
{
    MySqlConnection con= new MySqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString);
    MySqlCommand cmdprof,cmdper,cmdwork,cmdedu,cmdother;
    MySqlDataReader drprof;
    static string user;
    string[] cyears;
    protected void Page_Load(object sender, EventArgs e)
    {
        
         try
            {
                user = Session["loginname"].ToString();
                if (!Page.IsPostBack)
                {
                    //to show months and years in worktab
                    showmon();
                    showyr();

                    con.Open();
                    cmdprof = new MySqlCommand("select * from alumnireg where username='" + user + "'", con);
                    drprof = cmdprof.ExecuteReader();
                    drprof.Read();

                    num_visi.SelectedIndex = drprof.GetString("visibility").Substring(0, 1) == "1" ? 0 : 1;
                    addr_visi.SelectedIndex = drprof.GetString("visibility").Substring(1, 1) == "1" ? 0 : 1;

                    alu_role.Text = drprof.GetString("role");
                    alu_fname.Text = drprof.GetString("fname");
                    alu_lname.Text = drprof.GetString("lname");
                    alu_gender.Text = drprof.GetString("gender");
                    alu_email.Text = drprof.GetString("email");
                    DateTime dt = Convert.ToDateTime(drprof.GetString("dob"));
                    alu_dob.Text = dt.ToString("yyyy-MM-dd");
                    alu_batch.Text = drprof.GetInt32("batch").ToString();
                    alu_degree.Text = drprof.GetString("degree");
                    alu_branch.Text = drprof.GetString("branch");
                    alu_country.Text = drprof.GetString("country");
                    alu_state.Text = drprof.GetString("state");
                    alu_city.Text = drprof.GetString("city");
                    alu_number.Text = drprof.GetString("number");
                    alu_addr.Text = drprof.GetString("address");

                    other_hobby.Text = string.IsNullOrEmpty(drprof["hobby"].ToString()) ? "-" : drprof.GetString("hobby");
                    other_moment.Text = string.IsNullOrEmpty(drprof["ssnmoment"].ToString()) ? "-" : drprof.GetString("ssnmoment");
                    other_movie.Text = string.IsNullOrEmpty(drprof["movies"].ToString()) ? "-" : drprof.GetString("movies");
                    other_music.Text = string.IsNullOrEmpty(drprof["music"].ToString()) ? "-" : drprof.GetString("music");
                    other_myself.Text = string.IsNullOrEmpty(drprof["aboutme"].ToString()) ? "-" : drprof.GetString("aboutme");

                    string[] instname = string.IsNullOrEmpty(drprof["inst_name"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("inst_name").Split('/');
                    string[] instloc = string.IsNullOrEmpty(drprof["inst_loc"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("inst_loc").Split('/');
                    string[] instcourse = string.IsNullOrEmpty(drprof["inst_course"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("inst_course").Split('/');
                    string[] instyr = string.IsNullOrEmpty(drprof["inst_passyr"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("inst_passyr").Split('/');


                    alu_inst.Text = instname[0];
                    alu_inst1.Text = instname[1];
                    alu_inst2.Text = instname[2];
                    alu_inst3.Text = instname[3];
                    alu_iloc.Text = instloc[0];
                    alu_iloc1.Text = instloc[1];
                    alu_iloc2.Text = instloc[2];
                    alu_iloc3.Text = instloc[3];
                    alu_course.Text = instcourse[0];
                    alu_course1.Text = instcourse[1];
                    alu_course2.Text = instcourse[2];
                    alu_course3.Text = instcourse[3];
                    alu_passyr.Text = instyr[0];
                    alu_passyr1.Text = instyr[1];
                    alu_passyr2.Text = instyr[2];
                    alu_passyr3.Text = instyr[3];

                    string[] cmname = string.IsNullOrEmpty(drprof["org"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("org").Split('/');
                    string[] cmdesig = string.IsNullOrEmpty(drprof["desig"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("desig").Split('/');
                    string[] cmloc = string.IsNullOrEmpty(drprof["cmploc"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("cmploc").Split('/');
                    
                    cyears= string.IsNullOrEmpty(drprof["expyrs"].ToString()) ? new string[] { "-", "-", "-", "-" } : drprof.GetString("expyrs").Split('/');

                    cmp1.Text = cmname[0];
                    cmp2.Text = cmname[1];
                    cmp3.Text = cmname[2];
                    cmp4.Text = cmname[3];
                    desig1.Text = cmdesig[0];
                    desig2.Text = cmdesig[1];
                    desig3.Text = cmdesig[2];
                    desig4.Text = cmdesig[3];
                    loc1.Text = cmloc[0];
                    loc2.Text = cmloc[1];
                    loc3.Text = cmloc[2];
                    loc4.Text = cmloc[3];
                  
                    drprof.Close();
                }
            }
            catch (Exception ex)
            {
                CreateLogFile errlog = new CreateLogFile();
                errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Page Load method of AlumniProfile for " + Session["loginname"] + ":" + ex.Message);
            }
            finally
            {
                con.Close();
            }
     }
        
       protected void alu_degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (alu_degree.SelectedItem.Value == "PG")
        {
            alu_branch.Items.Clear();
            alu_branch.Items.Add("--Select a branch--");
            alu_branch.Items.Add("MCA");
            alu_branch.Items.Add("MBA");
            alu_branch.Items.Add("M.E (Communication Systems)");
            alu_branch.Items.Add("M.E (CSE)");
            alu_branch.Items.Add("M.E (Applied Electronics)");
            alu_branch.Items.Add("M.E (Power Electronics & Drives)");
            alu_branch.Items.Add("M.E (Computer and Communication)");
            alu_branch.Items.Add("M.E. in VLSI Design (ECE Department)");
            alu_branch.Items.Add("M.E. in SE (CSE Department)");
            alu_branch.Items.Add("MSC. IT");
        }
        else if (alu_degree.SelectedItem.Value == "UG")
        {
            alu_branch.Items.Clear();
            alu_branch.Items.Add("--Select a branch--");
            alu_branch.Items.Add("B.E (EEE)");
            alu_branch.Items.Add("B.E (CSE)");
            alu_branch.Items.Add("B.E (ECE)");
            alu_branch.Items.Add("B.E (BME)");
            alu_branch.Items.Add("B.E (ME)");
            alu_branch.Items.Add("B.Tech (IT)");
            alu_branch.Items.Add("B.Tech (Chemical)");
        }
    }
    protected void alu_branch_Init(object sender, EventArgs e)
    {
        alu_branch.Items.Clear();
        alu_branch.Items.Add("--Select a branch--");
        alu_branch.Items.Add("MCA");
        alu_branch.Items.Add("MBA");
        alu_branch.Items.Add("M.E (Communication Systems)");
        alu_branch.Items.Add("M.E (CSE)");
        alu_branch.Items.Add("M.E (Applied Electronics)");
        alu_branch.Items.Add("M.E (Power Electronics & Drives)");
        alu_branch.Items.Add("M.E (Computer and Communication)");
        alu_branch.Items.Add("M.E. in VLSI Design (ECE Department)");
        alu_branch.Items.Add("M.E. in SE (CSE Department)");
        alu_branch.Items.Add("MSC. IT");
    }
    protected void alu_batch_Init(object sender, EventArgs e)
    {
        alu_batch.Items.Add("-- Select a Batch --");
        for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
        {
            alu_batch.Items.Add(yr.ToString());
        }
    }
    protected void alu_country_Init(object sender, EventArgs e)
    {
        string indpath = Server.MapPath("../countrystate/countrieslist.txt");
        FileStream fs;
        StreamReader read;
        string line;

        fs = new FileStream(indpath, FileMode.Open, FileAccess.Read);
        read = new StreamReader(fs);
        while ((line = read.ReadLine()) != null)
        {
            alu_country.Items.Add(line);
        }
        read.Close();
        fs.Close();

    }

 
    protected void Submit_prof_Click(object sender, EventArgs e)
    {
     
        try
        {
            con.Open();
            cmdper = new MySqlCommand("update alumnireg set role=@urole, fname=@firname,lname=@lstname,gender=@gen,dob=@birth,email=@mail,batch=@bat,degree=@deg,branch=@bran,city=@ucity,state=@ustate,country=@cntry,address=@add1,number=@num where username='"+user+"'",con);
            cmdper.Parameters.AddWithValue("@urole",alu_role.Text);
            cmdper.Parameters.AddWithValue("@firname", alu_fname.Text);
            cmdper.Parameters.AddWithValue("@lstname", alu_lname.Text);
            cmdper.Parameters.AddWithValue("@gen", alu_gender.SelectedItem.Value);
            cmdper.Parameters.AddWithValue("@birth", alu_dob.Text);
            cmdper.Parameters.AddWithValue("@mail", alu_email.Text);
            cmdper.Parameters.AddWithValue("@bran", alu_branch.SelectedItem.Value);
            cmdper.Parameters.AddWithValue("@deg", alu_degree.SelectedItem.Value);
            cmdper.Parameters.AddWithValue("@bat", alu_batch.SelectedItem.Value);
            cmdper.Parameters.AddWithValue("@ucity", alu_city.Text);
            cmdper.Parameters.AddWithValue("@ustate", alu_state.Text);
            cmdper.Parameters.AddWithValue("@cntry", alu_country.SelectedItem.Value);
            cmdper.Parameters.AddWithValue("@add1", alu_addr.Text);
            cmdper.Parameters.AddWithValue("@num", alu_number.Text);
         
            cmdper.ExecuteNonQuery();
            con.Close(); 
        }
          catch (Exception ex)
              {
                  CreateLogFile errlog = new CreateLogFile();
                  errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Submit Prof of Alumni Profile for " + Session["loginname"] + ":" + ex.Message);
              }
              
    }
    protected void Submit_work_Click(object sender, EventArgs e)
    {
        string cmpname = cmp1.Text + "/" + cmp2.Text + "/" + cmp3.Text + "/" + cmp4.Text ;
        string cmpdsg = desig1.Text + "/" + desig2.Text + "/" + desig3.Text + "/" + desig4.Text;
        string cmploc = loc1.Text + "/" + loc2.Text + "/" + loc3.Text + "/" + loc4.Text;
        string cmpyrs ="";
        string[] cyears=new string[] { "-", "-", "-", "-" };
        DateTime dt1, dt2;
        TimeSpan ts;

        try
        {

            if (fmon1.SelectedIndex == 0 || fyear1.SelectedIndex == 0 || tmon1.SelectedIndex == 0 || tyear1.SelectedIndex == 0 || string.Compare(fyear1.SelectedItem.Value, tyear1.SelectedItem.Value) > 0)
            {
                cmpyrs = cyears[0] + "/" + cyears[1] + "/" + cyears[2] + "/" + cyears[3];
            }
            else
            {
                dt1 = Convert.ToDateTime(fyear1.SelectedItem.Value + "-" + fmon1.SelectedItem.Value);
                dt2 = Convert.ToDateTime(tyear1.SelectedItem.Value + "-" + tmon1.SelectedItem.Value);
                ts = dt2 - dt1;
                cyears[0] = Math.Round(Convert.ToDecimal(Math.Abs(ts.Days)) / 30, 0).ToString();
            }
            if (fmon2.SelectedIndex == 0 || fyear2.SelectedIndex == 0 || tmon2.SelectedIndex == 0 || tyear2.SelectedIndex == 0 || string.Compare(fyear2.SelectedItem.Value, tyear2.SelectedItem.Value) > 0)
            {
                cmpyrs = cyears[0] + "/" + cyears[1] + "/" + cyears[2] + "/" + cyears[3];
            }
            else
            {
                dt1 = Convert.ToDateTime(fyear2.SelectedItem.Value + "-" + fmon2.SelectedItem.Value);
                dt2 = Convert.ToDateTime(tyear2.SelectedItem.Value + "-" + tmon2.SelectedItem.Value);
                ts = dt2 - dt1;
                cyears[1] = Math.Round(Convert.ToDecimal(Math.Abs(ts.Days)) / 30, 0).ToString();
            }
            if (fmon3.SelectedIndex == 0 || fyear3.SelectedIndex == 0 || tmon3.SelectedIndex == 0 || tyear3.SelectedIndex == 0 || string.Compare(fyear3.SelectedItem.Value, tyear3.SelectedItem.Value) > 0)
            {
                cmpyrs = cyears[0] + "/" + cyears[1] + "/" + cyears[2] + "/" + cyears[3];
            }
            else
            {
                dt1 = Convert.ToDateTime(fyear3.SelectedItem.Value + "-" + fmon3.SelectedItem.Value);
                dt2 = Convert.ToDateTime(tyear3.SelectedItem.Value + "-" + tmon3.SelectedItem.Value);
                ts = dt2 - dt1;
                cyears[2] = Math.Round(Convert.ToDecimal(Math.Abs(ts.Days)) / 30, 0).ToString();
            }

            if (fmon4.SelectedIndex == 0 || fyear4.SelectedIndex == 0 || tmon4.SelectedIndex == 0 | tyear4.SelectedIndex == 0 || string.Compare(fyear4.SelectedItem.Value, tyear4.SelectedItem.Value) > 0)
            {
                cmpyrs = cyears[0] + "/" + cyears[1] + "/" + cyears[2] + "/" + cyears[3];
            }
            else
            {
                dt1 = Convert.ToDateTime(fyear4.SelectedItem.Value + "-" + fmon4.SelectedItem.Value);
                dt2 = Convert.ToDateTime(tyear4.SelectedItem.Value + "-" + tmon4.SelectedItem.Value);
                ts = dt2 - dt1;
                cyears[3] = Math.Round(Convert.ToDecimal(Math.Abs(ts.Days)) / 30, 0).ToString();
            }
            cmpyrs = cyears[0] + "/" + cyears[1] + "/" + cyears[2] + "/" + cyears[3];

            con.Open();
            cmdwork = new MySqlCommand("update alumnireg set org=@cname,desig=@deg,expyrs=@yrs,cmploc=@loc where username='"+user+"'", con);
            cmdwork.Parameters.AddWithValue("@cname", cmpname);
            cmdwork.Parameters.AddWithValue("@deg", cmpdsg);
           cmdwork.Parameters.AddWithValue("@yrs", cmpyrs);
            cmdwork.Parameters.AddWithValue("@loc", cmploc);

            cmdwork.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Submit work of Alumni Profile for " + Session["loginname"] + ":" + ex.Message);
        }
             
    }

protected void  Submit_other_Click(object sender, EventArgs e)
{
    try
    {
        con.Open();
        cmdother=new MySqlCommand("update alumnireg set aboutme=@me,ssnmoment=@mnt,hobby=@hob,music=@mus,movies=@mov where username='"+user+"'",con);
        cmdother.Parameters.AddWithValue("@me",other_myself.Text);
        cmdother.Parameters.AddWithValue("@mnt",other_moment.Text);
        cmdother.Parameters.AddWithValue("@hob",other_hobby.Text);
        cmdother.Parameters.AddWithValue("@mus",other_music.Text);
        cmdother.Parameters.AddWithValue("@mov",other_movie.Text);

        cmdother.ExecuteNonQuery();
        con.Close();
    }
     catch (Exception ex)
        {
            CreateLogFile errlog = new CreateLogFile();
            errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Submit other of Alumni Profile for " + Session["loginname"] + ":" + ex.Message);
        }
             
}

protected void Submit_edu_Click(object sender, EventArgs e)
{
    string instname = alu_inst.Text + "/" + alu_inst1.Text + "/" + alu_inst2.Text + "/" + alu_inst3.Text;
    string instloc = alu_iloc.Text + "/" + alu_iloc1.Text + "/" + alu_iloc2.Text + "/" + alu_iloc3.Text;
    string instcourse = ((alu_course.SelectedIndex == 0) ? "" : alu_course.SelectedItem.Value) + "/" + ((alu_course1.SelectedIndex == 0) ? "" : alu_course1.SelectedItem.Value) + "/" + ((alu_course2.SelectedIndex == 0) ? "" : alu_course2.SelectedItem.Value) + "/" + ((alu_course3.SelectedIndex == 0) ? "" : alu_course3.SelectedItem.Value);
    string instyr = alu_passyr.Text + "/" + alu_passyr1.Text + "/" + alu_passyr2.Text + "/" + alu_passyr3.Text;
    try
    {
        con.Open();
        cmdedu = new MySqlCommand("update alumnireg set inst_name=@iname,inst_course=@sub,inst_loc=@loca,inst_passyr=@yr where username='" + user + "'", con);
        cmdedu.Parameters.AddWithValue("@iname",instname);
        cmdedu.Parameters.AddWithValue("@sub", instcourse);
        cmdedu.Parameters.AddWithValue("@loca", instloc);
        cmdedu.Parameters.AddWithValue("@yr", instyr);

        cmdedu.ExecuteNonQuery();
        con.Close();
    }
    catch (Exception ex)
    {
        CreateLogFile errlog = new CreateLogFile();
        errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "Submit education method of Alumni Profile for " + Session["loginname"] + ":" + ex.Message);
    }
    
}


protected void showyr()
{
    fyear1.Items.Add("Year");
    tyear1.Items.Add("Year");
    fyear2.Items.Add("Year");
    tyear2.Items.Add("Year");
    fyear3.Items.Add("Year");
    tyear3.Items.Add("Year");
    fyear4.Items.Add("Year");
    tyear4.Items.Add("Year");

    for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
    {
        fyear1.Items.Add(yr.ToString());
        tyear1.Items.Add(yr.ToString());
        fyear2.Items.Add(yr.ToString());
        tyear2.Items.Add(yr.ToString());
        fyear3.Items.Add(yr.ToString());
        tyear3.Items.Add(yr.ToString());
        fyear4.Items.Add(yr.ToString());
        tyear4.Items.Add(yr.ToString());

    }
}

protected void showmon()
{
    fmon1.Items.Add("Month");
    tmon1.Items.Add("Month");
    fmon2.Items.Add("Month");
    tmon2.Items.Add("Month");
    fmon3.Items.Add("Month");
    tmon3.Items.Add("Month");
    fmon4.Items.Add("Month");
    tmon4.Items.Add("Month");

    for (int mon = 1; mon <= 12; mon++)
    {
        fmon1.Items.Add(mon.ToString());
        tmon1.Items.Add(mon.ToString());
        fmon2.Items.Add(mon.ToString());
        tmon2.Items.Add(mon.ToString());
        fmon3.Items.Add(mon.ToString());
        tmon3.Items.Add(mon.ToString());
        fmon4.Items.Add(mon.ToString());
        tmon4.Items.Add(mon.ToString());
    }

}
protected void advanceopt_Click(object sender, EventArgs e)
{
    string vis = "";
    vis = (num_visi.SelectedIndex == 0) ? "1" + ((addr_visi.SelectedIndex == 0) ? "1" : "2") : "2" + ((addr_visi.SelectedIndex == 0) ? "1" : "2");
    MySqlCommand cmd = new MySqlCommand("update alumnireg set visibility='"+vis+"' where username='"+user+"'", con);
    try
    {
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
    catch (Exception ex)
    {
        CreateLogFile errlog = new CreateLogFile();
        errlog.ErrorLog(Server.MapPath("../Logs/Errorlog"), "advanceopt method of Alumni Profile for " + Session["loginname"] + ":" + ex.Message);
 
    }
    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "showadv();", true);
}
}