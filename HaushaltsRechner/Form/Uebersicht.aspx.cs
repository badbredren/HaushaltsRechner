using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaushaltsRechner.Form
{
    public partial class Uebersicht : LoginFormBase
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeControls();
            this.Title = "Übersicht | Haushaltsrechner";
        }

        protected override void InitializeControls()
        {
            base.InitializeControls();
            UebersichtSearch1.StartSearch += new EventHandler(UebersichtSearch1_StartSearch);
        }

        void UebersichtSearch1_StartSearch(object sender, EventArgs e)
        {
            UebersichtResult1.InitializeControls();
        }
    }
}