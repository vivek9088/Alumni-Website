using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search_Alumni_SearchAlumni : System.Web.UI.Page
{
    public string namemsg, locmsg, classmsg, branchmsg, batchmsg;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void degree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (degree.SelectedItem.Value == "PG")
        {
            branch.Items.Clear();
            branch.Items.Add("--Select a Branch--");
            branch.Items.Add("MCA");
            branch.Items.Add("MBA");
            branch.Items.Add("M.E (Communication Systems)");
            branch.Items.Add("M.E (CSE)");
            branch.Items.Add("M.E (Applied Electronics)");
            branch.Items.Add("M.E (Power Electronics & Drives)");
            branch.Items.Add("M.E (Computer and Communication)");
            branch.Items.Add("M.E in VLSI Design (ECE Department)");
            branch.Items.Add("M.E in SE (CSE Department)");
            branch.Items.Add("MSC IT");
        }
        else if (degree.SelectedItem.Value == "UG")
        {
            branch.Items.Clear();
            branch.Items.Add("--Select a Branch--");
            branch.Items.Add("B.E (EEE)");
            branch.Items.Add("B.E (CSE)");
            branch.Items.Add("B.E (ECE)");
            branch.Items.Add("B.E (BME)");
            branch.Items.Add("B.E (ME)");
            branch.Items.Add("B.Tech (IT)");
            branch.Items.Add("B.Tech (Chemical)");
        }
    }
    protected void branch_Init(object sender, EventArgs e)
    {
        branch.Items.Clear();
        branch.Items.Add("--Select a Branch--");
        branch.Items.Add("MCA");
        branch.Items.Add("MBA");
        branch.Items.Add("M.E (Communication Systems)");
        branch.Items.Add("M.E (CSE)");
        branch.Items.Add("M.E (Applied Electronics)");
        branch.Items.Add("M.E (Power Electronics & Drives)");
        branch.Items.Add("M.E (Computer and Communication)");
        branch.Items.Add("M.E in VLSI Design (ECE Department)");
        branch.Items.Add("M.E in SE (CSE Department)");
        branch.Items.Add("MSC IT");
    }
    protected void batch_Init(object sender, EventArgs e)
    {
        batch.Items.Add("--Select a Batch--");
        for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
        {
            batch.Items.Add(yr.ToString());
        }
    }
    protected void namesearch_Click(object sender, EventArgs e)
    {
        if (name.Text.Trim() == "")
        {
            namemsg = "Please enter a Name..";
        }
        else
        {
            Response.Redirect("SearchList.aspx?id=2&search=" + name.Text.Trim());
        }
    }
    protected void yearsearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("SearchList.aspx?id=3&search=" + year.SelectedItem.Value);
    }
    protected void year_Init(object sender, EventArgs e)
    {
        for (int yr = 2000; yr <= System.DateTime.Now.Year; yr++)
        {
            year.Items.Add(yr.ToString());
        }

    }
    protected void locsearch_Click(object sender, EventArgs e)
    {
        string lid;
        if (location.Text.Trim() == "")
        {
            locmsg = "Please enter a Location..";
        }
        else
        {
            lid = location.Text.Trim() + "," + loclist.SelectedItem.Value;
            Response.Redirect("SearchList.aspx?id=4&search=" + lid);
        }

    }
    protected void classsearch_Click(object sender, EventArgs e)
    {
        string cid;
        if (degree.SelectedIndex == 0)
        { classmsg = "Please select a Course.."; }
        else if (branch.SelectedIndex == 0)
        { classmsg = ""; branchmsg = "Please select a Branch.."; }
        else if (batch.SelectedIndex == 0)
        { branchmsg = ""; batchmsg = "Please select a Batch.."; }
        else
        {
            batchmsg = "";
            cid = degree.SelectedItem.Value + "," + branch.SelectedItem.Value + "," + batch.SelectedItem.Value;
            Response.Redirect("SearchList.aspx?id=1&search=" + cid);
        }
    }
    
}