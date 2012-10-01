using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaushaltsRechner.Controls.Admin
{
    public partial class AdminNavigationLink : System.Web.UI.UserControl
    {
        public string Text
        {
            get
            {
                return hlTarget.Text;
            }
            set
            {
                hlTarget.Text = value;
            }
        }

        public string Target
        {
            get
            {
                return hlTarget.NavigateUrl;
            }
            set
            {
                hlTarget.NavigateUrl = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}