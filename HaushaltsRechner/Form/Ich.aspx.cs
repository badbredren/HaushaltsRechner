using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaushaltsRechner.Form
{
    public partial class Ich : LoginFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Persönliche Übersicht | Haushaltsrechner";
        }
    }
}