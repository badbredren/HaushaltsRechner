/*
    This file is part of HaushaltsRechner.

    HaushaltsRechner is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HaushaltsRechner is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HaushaltsRechner.  If not, see <http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von HaushaltsRechner.

    HaushaltsRechner ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Option) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.

    HaushaltsRechner wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHELEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Mapper;
using HaushaltsRechner.Business.Security;
using Newtonsoft.Json;

namespace HaushaltsRechner.Views.Admin
{
    /// <summary>
    /// Code behind class for AdminUser.ascx
    /// </summary>
    public partial class AdminUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvUser.Columns[1].HeaderText = Resources.Default.name;
            gvUser.Columns[2].HeaderText = Resources.Default.password;
            gvUser.Columns[3].HeaderText = Resources.Default.admin;
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