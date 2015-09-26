<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO"%>


<script runat="server">
    StreamReader readfile;
    StreamWriter writefile;
    FileStream fs;
    string cnt;
   
    int counter;
    
    void Application_Start(object sender, EventArgs e) 
    {
        Application["onlineusers"] = 0;
        
                 
     }
    
     void Application_End(object sender, EventArgs e) 
     {
         //  Code that runs on application shutdown
         FileStream fs = new FileStream(Server.MapPath("hitcounter.txt"), FileMode.OpenOrCreate, FileAccess.Write);
         StreamWriter wr = new StreamWriter(fs);
         wr.WriteLine(counter.ToString());
         wr.Close();
         fs.Close();
       
     }
      
     void Application_Error(object sender, EventArgs e) 
     { 
         // Code that runs when an unhandled error occurs
     }

     void Session_Start(object sender, EventArgs e) 
     {
         // Code that runs when a new session is started
         //Session.Timeout = 15;
         
             Application.Lock();
             Application["onlineusers"] = Convert.ToInt32(Application["onlineusers"]) + 1;
             string path = (File.Exists(Server.MapPath("hitcounter.txt"))) ? Server.MapPath("hitcounter.txt") : Server.MapPath("../hitcounter.txt");
         
             if (File.Exists(path))
             {
                 readfile = File.OpenText(path);
                 cnt = readfile.ReadLine().ToString();
                 readfile.Close();
                 counter = Convert.ToInt32(cnt);
                 counter++;
                 Application["counter"] = counter;
                 fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                 writefile = new StreamWriter(fs);
                 writefile.WriteLine(Convert.ToString(counter));
                 writefile.Close();
                 fs.Close();
             }
             else
             {
                 counter = 0;
                 counter++;
                 Application["counter"] = counter;
                 fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                 writefile = new StreamWriter(fs);
                 writefile.WriteLine(Convert.ToString(counter));
                 writefile.Close();
                 fs.Close();
             }
     Application.UnLock();
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session.Clear();
        Application.Lock();
        Application["counter"] = Convert.ToInt32(Application["counter"].ToString()) - 1;
        Application.UnLock();
        
         
    }
       
</script>
