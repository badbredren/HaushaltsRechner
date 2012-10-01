using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HaushaltsRechner.UIProcess;
using System.IO;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace HaushaltsRechner
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
#if !DEBUG
            MinifyJS();
#endif
        }

        private void MinifyJS()
        {
            var source = new StringBuilder();
            var folder = Server.MapPath("~/Scripts/");
            var dir = new DirectoryInfo(folder);

            foreach (var f in dir.GetFiles("*.debug.js"))
            {
                source.Append(File.ReadAllText(folder + f.Name));
            }

            source.Append(AddJavaScriptFeatures(Server.MapPath("~/Scripts/SlickGrid/")));

            var minifier = new Minifier();
            using (var sw = new StreamWriter(folder + "jsmin.min.js"))
            {
                sw.Write(minifier.MinifyJavaScript(source.ToString()));
            }
        }

        private StringBuilder AddJavaScriptFeatures(string folder)
        {
            var str = new StringBuilder();
            var dir = new DirectoryInfo(folder);

            //Dateien hinzufügen
            foreach (var file in dir.GetFiles())
            {
                str.Append(File.ReadAllText(file.FullName));
            }

            //tiefer liegende Ordner hinzufügen
            foreach (var f in dir.GetDirectories()) 
            {                
                str.Append(AddJavaScriptFeatures(f.FullName));
            }
            
            return str;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}