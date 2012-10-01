using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.UIProcess.Mapper;
using Newtonsoft.Json;

namespace HaushaltsRechner.Controls.Admin
{
    public partial class AdminAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvAccount.SelectedRow != null)
            {
                var idStr = gvAccount.SelectedRow.Cells[1].Text;
                Guid id;
                if (Guid.TryParse(idStr, out id))
                {
                    var a = AccountManager.GetAccountById(id);

                    var account = new NewAccount
                    {
                        ID = a.ID,
                        Name = a.NAME
                    };

                    ScriptManager.RegisterStartupScript(this, GetType(), "SELECTACCOUNT_"
                        + ClientID, "var selectedAccount  = '" + JsonConvert.SerializeObject(account) + "';", true);
                }
            }
        }
    }
}