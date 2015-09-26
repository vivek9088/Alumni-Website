using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    
    [WebMethod]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {

        string[] cmpname_list = {"Tata Consultancy Services (TCS)","Infosys Technologies Ltd","Wipro Limited","Hewlett-Packard India Pvt. Ltd","IBM India Limited",
"Satyam Computer Services Ltd.","HCL Technologies Ltd.","Intel Technology India Pvt. Ltd.","CISCO Systems India Pvt. Ltd.",
"Patni Computer Systems Ltd.","Cognizant Technology Solutions India","HCL Infosystems Ltd.","Mahindra-British Telecom",
"Redington (India) Limited","i-Flex Solutions Ltd.","Oracle India Pvt. Ltd.","Moser Baer India Ltd.","Microsoft Corporation (India) Pvt. Ltd.",
"NIIT Limited","TATA Infotech Ltd.","iGATE Global Solutions Ltd.","Ingram Micro India (P) Ltd","Sun Microsystems India Pvt Ltd",
"Polaris Software Lab Ltd.","Samsung India Electronics Ltd.","Perot Systems TSI (India) Limited","SAP India Pvt. Ltd.","American Power Conversion",
"Celetronix India Pvt. Ltd.","Computer Associates India Pvt. Ltd","Dell Computer India Pvt. Ltd.","Hexaware Technologies","Larsen & Toubro Infotech Ltd.",
"Siemens Information Systems Ltd.","Mastek Ltd.","CMS Computers Ltd.","Texas Instruments India Pvt. Ltd","Honeywell Technology Solutions Lab",
"Acer India (Pvt) Ltd.","MphasiS BFL Ltd.","Datacraft India Limited","Nortel Networks (I) Pvt Ltd","Syntel (India) Ltd.",
"Flextronics Software Systems Ltd","Kanbay Software India Pvt. Ltd.","Rolta India Ltd.","Infinite Computer Solutions (I) Pvt Ltd",
"GTL Limited","Covansys (I) Pvt. Ltd.","Sify Limited","Zensar Technologies Ltd.","Tulip IT Services Ltd","Zenith Computers Limited",
"Sonata Software Ltd","D-Link (India) Ltd.","Mascon Global Limited","3i Infotech Limited","WeP Peripherals Ltd.","Computer Sciences Corporation India",
"Philips Innovation Campus","Xansa India","Canon India Pvt. Ltd.","TVS Electronics Ltd.","Infotech Enterprises Ltd.","KPIT Cummins Infosystems Limited",
"ITC Infotech India","MindTree Consulting Pvt. Ltd.","PCS Technology Ltd.","Sasken Communication Technologies","Tally Solutions Pvt. Ltd.",
"Ramco Systems Ltd.","Epson India Pvt. Ltd.","Numeric Power Systems Ltd.","Rashi Peripherals Pvt. Ltd","Network Solutions Pvt. Ltd.",
"TATA Elxsi Ltd.","Aftek Infosys Limited","EMC Data Storage Systems India","Tata Technologies Ltd.","Geometric Software Solutions Co.",
"Cranes Software International Limited","Accel Frontline Limited","Mediaman Infotech Pvt. Ltd.","Priya Limited - IT Products Division",
"Keane India Ltd.","Persistent Systems Pvt. Ltd.","Aptech Limited","Lipi Data Systems Ltd.","Cadence Design Systems (India)",
"Intex Technologies (India) Ltd","Aditi Technologies Pvt Ltd","MICROTEK Limited","Subex Systems Limited","Blue Star Infotech Limited",
"Aztec Software & Technology Services","Pentamedia Graphics Ltd.","TATA Interactive Systems","Birlasoft Limited","DB Power Electronics (P) Ltd",
"Atlanta IT Solutions Pvt. Ltd","RMSI Pvt. Ltd.",
};
        // Return matching Company Name
        return (from m in cmpname_list where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
    }
}
