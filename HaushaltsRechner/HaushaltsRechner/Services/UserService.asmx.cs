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
using System.Web.Services;
using HaushaltsRechner.Business.Manager;
using Newtonsoft.Json;
using HaushaltsRechner.Mapper;
using HaushaltsRechner.Business.Security;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Service for interaction with <see cref="HaushaltsRechner.Data.Model.USER"/>
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        /// <summary>
        /// Logs in the <see cref="HaushaltsRechner.Data.Model.USER"/>
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns></returns>
        [WebMethod(true)]
        public string UserLogin(string data)
        {
            var userData = JsonConvert.DeserializeObject<UserLogin>(data);

            var user = UserManager.LoginUser(userData.NAME, userData.Password);

            if (user != null)
            {
                SessionManager.CurrentUser = user;
                return bool.TrueString;
            }
            return bool.FalseString;
        }

        /// <summary>
        /// Adds the <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if success</returns>
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

        /// <summary>
        /// Edits the <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if success</returns>
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
                if (UserManager.EditUser(u.ID, u.Name, u.Password, u.Language, u.SysAdmin))
                {
                    return bool.TrueString;
                }
            }
            catch
            {
            }

            return bool.FalseString;
        }

        /// <summary>
        /// Gets the rights by <see cref="HaushaltsRechner.Data.Model.USER"/> ID.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>stringified JSON- List of <see cref="HaushaltsRechner.Mapper.ActiveRight"/></returns>
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

        /// <summary>
        /// Edits the <see cref="HaushaltsRechner.Data.Model.USER"/> rights.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="rights">The rights.</param>
        /// <returns>(string)true, if success</returns>
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

        /// <summary>
        /// Deletes the <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if success</returns>
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

        /// <summary>
        /// Gets the user <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>stringified JSON of <see cref="HaushaltsRechner.Mapper.NewUser"/></returns>
        [WebMethod(true)]
        public string GetUserData(string data) 
        {
            var user = SessionManager.CurrentUser;

            var retUser = new NewUser
            {
                ID = user.ID,
                Name = user.NAME,
                Password =
                    string.IsNullOrEmpty(user.PASSWORT)
                        ? string.Empty
                        : Cryptography.DecryptStringFromBytes_AES(user.PASSWORT),
                Language = user.CULTURE
            };

            return JsonConvert.SerializeObject(retUser);
        }

        /// <summary>
        /// Changes the <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if success</returns>
        [WebMethod(true)]
        public string ChangeUser(string data)
        {
            var user = JsonConvert.DeserializeObject<NewUser>(data);

            if (SessionManager.CurrentUser.ID != user.ID)
            {
                return string.Empty;
            }

            if (UserManager.EditUser(user.ID, user.Name, user.Password, user.Language, null))
            {
                return string.Empty;
            }

            return bool.FalseString;
        }
    }
}