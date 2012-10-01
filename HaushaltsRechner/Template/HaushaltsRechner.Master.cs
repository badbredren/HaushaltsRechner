using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace HaushaltsRechner.Template
{
    public partial class HaushaltsRechner : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
#if DEBUG
            var dir = new DirectoryInfo(Server.MapPath("~/Scripts/"));

            foreach (var f in dir.GetFiles("*.debug.js"))
            {
                sm.Scripts.Add(new ScriptReference("~/Scripts/" + f.Name));
            }

            AddJavaScriptFeature("~/Scripts/SlickGrid/");

#else
            sm.Scripts.Add(new ScriptReference("~/Scripts/jsmin.min.js"));

#endif
        }

        private void AddJavaScriptFeature(string folder)
        {
            var dir = new DirectoryInfo(Server.MapPath(folder));

            foreach (var file in dir.GetFiles())
            {
                sm.Scripts.Add(new ScriptReference(folder + file.Name));
            }

            foreach (var d in dir.GetDirectories())
            {
                AddJavaScriptFeature(folder + d.Name + "/");
            }
        }
    }
}