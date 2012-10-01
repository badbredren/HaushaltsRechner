using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaushaltsRechner.UIProcess;
using HaushaltsRechner.Business.Security;
using HaushaltsRechner.Controls.Admin;

namespace HaushaltsRechner.Form.Admin
{
    public partial class Admin : HaushaltsRechner.Form.LoginFormBase
    {
        private string _path = "~/Controls/Admin/";

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (SessionManager.CurrentUser == null || !RightsManager.HasAdminRight(SessionManager.CurrentUser.ID))
            {
                Response.Redirect("~/Form/Uebersicht.aspx");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var id = SessionManager.CurrentUser.ID;
            var target = Request["target"];

            if (target == null)
            {
                if (RightsManager.HasRight(id, Feature.AdminUser))
                {
                    target = "user";
                }
                else if (RightsManager.HasRight(id, Feature.AdminAccount))
                {
                    target = "account";
                }
            }

            if (target == "user" && RightsManager.HasRight(id, Feature.AdminUser))
            {
                LoadControlIntoPlaceHolder(phAdminToolbar, LoadControl(_path + "AdminUserToolBar.ascx"));
                LoadControlIntoPlaceHolder(phAdminContent, LoadControl(_path + "AdminUser.ascx"));
            }
            else if (target == "account" && RightsManager.HasRight(id,Feature.AdminAccount))
            {
                LoadControlIntoPlaceHolder(phAdminToolbar, LoadControl(_path + "AdminAccountToolBar.ascx"));
                LoadControlIntoPlaceHolder(phAdminContent, LoadControl(_path + "AdminAccount.ascx"));
            }
            else
            {
                Response.Redirect("~/Form/Uebersicht.aspx");
            }                                  
        }

        protected void LoadControlIntoPlaceHolder(Control ph, Control c)
        {
            ph.Controls.Clear();
            ph.Controls.Add(c);
        }
    }
}