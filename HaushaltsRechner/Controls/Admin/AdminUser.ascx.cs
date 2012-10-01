using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.UIProcess.Mapper;
using HaushaltsRechner.Business.Security;
using Newtonsoft.Json;

namespace HaushaltsRechner.Controls.Admin
{
    public partial class AdminUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvUser.SelectedRow != null)
            {
                var idStr = gvUser.SelectedRow.Cells[1].Text;
                Guid id;
                if (Guid.TryParse(idStr, out id))
                {
                    var m = UserManager.GetUserById(id);
                    var pass = string.Empty;
                    
                    if(m.PASSWORT != null && m.PASSWORT != string.Empty)
                    {
                        pass = Cryptography.DecryptStringFromBytes_AES(m.PASSWORT);
                    }

                    var user = new NewUser
                    {
                        ID = m.ID,
                        Name = m.NAME,
                        Password = pass,
                        SysAdmin = m.ISADMIN.HasValue ? m.ISADMIN.Value : false
                    };

                    ScriptManager.RegisterStartupScript(this, GetType(), "SELECTMOVEMENT_"
                        + ClientID, "var selectedUser = '" + JsonConvert.SerializeObject(user) + "';", true);
                }
            }  
        }
    }
}