using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.UIProcess;
using Newtonsoft.Json;
using HaushaltsRechner.UIProcess.Mapper;
using HaushaltsRechner.Business.Security;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Um das Aufrufen dieses Webdiensts aus einem Skript mit ASP.NET AJAX zuzulassen, heben Sie die Auskommentierung der folgenden Zeile auf. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        [WebMethod(true)]
        public string UserLogin(string data)
        {
            var userData = JsonConvert.DeserializeObject<UserLogin>(data);
            
            var user = UserManager.GetUserById(userData.ID);
            if (user != null)
            {
                SessionManager.CurrentUser = user;
                return bool.TrueString;
            }
            return bool.FalseString;
        }

        [WebMethod(true)]
        public string AddUser(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminUser))
            {
                return bool.FalseString;
            }

            try
            {
                var u = JsonConvert.DeserializeObject<NewUser>(data);
                if (UserManager.AddUser(u.Name, u.Password, u.SysAdmin))
                {
                    return bool.TrueString;
                }
            }
            catch
            {
            }

            return bool.FalseString;
        }

        [WebMethod(true)]
        public string EditUser(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminUser))
            {
                return bool.FalseString;
            }

            try
            {
                var u = JsonConvert.DeserializeObject<NewUser>(data);
                if (UserManager.EditUser(u.ID, u.Name, u.Password, u.SysAdmin))
                {
                    return bool.TrueString;
                }
            }
            catch
            {
            }

            return bool.FalseString;
        }

        [WebMethod]
        public string GetRightsByUserID(string data)
        {
            var id = JsonConvert.DeserializeObject<Guid>(data);
            
            var allRights = RightsManager.GetRights();
            var rights = RightsManager.GetRights(id);

            var activeRights = new List<ActiveRight>();

            foreach (var r in allRights)
            {
                activeRights.Add(new ActiveRight
                {
                    ID = r.ID,
                    Name = r.NAME,
                    Active = rights.Any(ri => ri.ID == r.ID) ? true : false
                });
            }

            return JsonConvert.SerializeObject(activeRights);
        }

        [WebMethod]
        public string EditUserRights(string userID, string rights)
        {
            var id = Guid.Parse(userID);
            var ri = JsonConvert.DeserializeObject<List<ActiveRight>>(rights);

            foreach (var r in ri)
            {
                RightsManager.SetRight(id, r.ID, r.Active);
            }

            return bool.TrueString;
        }

        [WebMethod]
        public string DeleteUser(string data)
        {
            var id = JsonConvert.DeserializeObject<Guid>(data);
            if (UserManager.DeleteUser(id))
            {
                return bool.TrueString;
            }

            return bool.FalseString;
        }
    }

}