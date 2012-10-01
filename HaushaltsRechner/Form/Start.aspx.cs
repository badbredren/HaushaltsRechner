using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaushaltsRechner.UIProcess;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Business.Manager;

namespace HaushaltsRechner.Form
{
    public partial class Start : System.Web.UI.Page
    {
        protected IQueryable<USER> users;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Startseite | Haushaltsrechner";
            users = UserManager.GetAllUser();

            //string roundtrip = Cryptography.decryptStringFromBytes_AES(encrypted, myRijndael.Key, myRijndael.IV);
        }
    }
}