using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaushaltsRechner.UIProcess;

namespace HaushaltsRechner.Form
{
    public class LoginFormBase : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (SessionManager.CurrentUser == null)
            {
                    Response.Redirect("~/Form/Start.aspx");
            }
        }

        protected virtual void InitializeControls()
        {

        }
    }
}