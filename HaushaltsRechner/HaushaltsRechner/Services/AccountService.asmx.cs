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
using Newtonsoft.Json;
using HaushaltsRechner.Mapper;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Business.Security;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Service for interaction with <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AccountService : System.Web.Services.WebService
    {
        /// <summary>
        /// Gets all valid <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> for the loged in <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <returns>stringified List of <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>s</returns>
        [WebMethod(true)]
        public string GetAllValidAccounts()
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var userID = SessionManager.CurrentUser.ID;
                var accs = en.ACCOUNT.Where(a => a.USER.Any(u=>u.ID == userID))
                    .Select(a=>
                        new NameAsString
                        {
                            ID = a.ID,
                            Name = a.NAME
                        })
                    .ToList();
                return JsonConvert.SerializeObject(accs);
            }
        }

        /// <summary>
        /// Adds an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>
        /// </summary>
        /// <param name="data">stringified JSON-Data</param>
        /// <returns>(string)true, if success</returns>
        [WebMethod(true)]
        public string AddAccount(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminAccount))
            {
                return bool.FalseString;
            }

            try
            {
                var u = JsonConvert.DeserializeObject<NameAsString>(data);
                if (AccountManager.AddAccount(u.Name))
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
        /// Edits an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data</param>
        /// <returns>(string)true, if success</returns>
        [WebMethod(true)]
        public string EditAccount(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminAccount))
            {
                return bool.FalseString;
            }

            try
            {
                var u = JsonConvert.DeserializeObject<NameAsString>(data);
                if (AccountManager.EditAccount(u.ID, u.Name))
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
        /// Deletes an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data</param>
        /// <returns>(string)true, if success</returns>
        [WebMethod(true)]
        public string DeleteAccount(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminAccount))
            {
                return bool.FalseString;
            }

            try
            {
                var id = JsonConvert.DeserializeObject<Guid>(data);
                if (AccountManager.DeleteAccount(id))
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
        /// Gets an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> <see cref="HaushaltsRechner.Data.Model.USER"/>s.
        /// </summary>
        /// <param name="data">stringified JSON-Data</param>
        /// <returns>(string)true, if success</returns>
        [WebMethod(true)]
        public string GetAccountUsers(string data) 
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminAccount))
            {
                return bool.FalseString;
            }

            try
            {
                var id = JsonConvert.DeserializeObject<Guid>(data);
                var accountUser = new List<AccountUser>();

                using (var en = new HaushaltsrechnerEntities())
                {
                    var account = en.ACCOUNT.FirstOrDefault(a => a.ID == id);
                    if (account == null)
                    {
                        return bool.FalseString;
                    }

                    foreach (var u in en.USER) 
                    {
                        accountUser.Add(new AccountUser
                            {
                                ID = u.ID,
                                Name = u.NAME,
                                InAccount = account.USER.Contains(u)
                            });
                    }                    

                    return JsonConvert.SerializeObject(accountUser);
                }
            }
            catch 
            { 
            }

            return bool.FalseString;
        }

        /// <summary>
        /// Edits an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="accountID">The account ID.</param>
        /// <param name="users">The users.</param>
        /// <returns>(string)true, if success</returns>
        [WebMethod(true)]
        public string EditAccountUser(string accountID, string users) 
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminAccount))
            {
                return bool.FalseString;
            }

            try
            {
                var id = Guid.Parse(accountID);

                var accountUsers = JsonConvert.DeserializeObject<List<AccountUser>>(users);
  
                foreach (var u in accountUsers)
                {
                    AccountManager.EditAccountUsers(id, u.ID, u.InAccount);
                }
                
            }
            catch { }

            return bool.FalseString;
        }
    }
}
