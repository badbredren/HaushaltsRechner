using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaushaltsRechner.UIProcess;

namespace HaushaltsRechner.Template
{
    public partial class Content : System.Web.UI.MasterPage
    {
        Dictionary<string, string> Sites;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Sites = new Dictionary<string, string>();
            var root = "../Form/";
            Sites.Add(root + "Start.aspx", "Startseite");
            Sites.Add(root + "Uebersicht.aspx", "Übersicht");
            Sites.Add(root + "Kategorie.aspx", "Kategorie");

            if (SessionManager.CurrentUser.ISADMIN == true)
            {
                Sites.Add(root + "Admin/Admin.aspx", "Administrator");
            }

            CreateMenu();
        }

        private void CreateMenu()
        {
            string script = string.Empty;
            foreach (var str in Sites)
            {
                script = string.Concat(script,
                    "getMenuPoint('",
                    str.Key,
                    "','",
                    str.Value,
                    "','",
                    MainMenu.ClientID,
                    "');");
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "STARTUPSCRIPTCONTENTMASTER_" + ClientID, script, true);
        }
    }
}