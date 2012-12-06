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
using HaushaltsRechner.Data.Model;
using System.Security.Cryptography;
using HaushaltsRechner.Business.Security;
using System.Globalization;

namespace HaushaltsRechner.Business.Manager
{
    /// <summary>
    /// Provides CRUD methods to interact with <see cref="HaushaltsRechner.Data.Model.USER"/>
    /// </summary>
    public static class UserManager
    {
        /// <summary>
        /// Gets all <see cref="HaushaltsRechner.Data.Model.USER"/>s.
        /// </summary>
        /// <returns>All <see cref="HaushaltsRechner.Data.Model.USER"/>s</returns>
        public static IQueryable<USER> GetAllUser()
        {
            var en = new HaushaltsrechnerEntities();

            return en.USER;
        }

        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Data.Model.USER"/> by id.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns><see cref="HaushaltsRechner.Data.Model.USER"/> or null</returns>
        public static USER GetUserById(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.USER.FirstOrDefault(u => u.ID == id);
            }
        }

        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Data.Model.USER"/> by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><see cref="HaushaltsRechner.Data.Model.USER"/> or null</returns>
        public static USER GetUserByName(string name)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.USER.FirstOrDefault(u => u.NAME == name);
            }
        }

        /// <summary>
        /// Adds a <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="pw">The password.</param>
        /// <param name="sysAdmin">if set to <c>true</c> System Admin flag will be set.</param>
        /// <returns><c>true</c>, if addition successfull</returns>
        public static bool AddUser(string name, string pw, bool sysAdmin)
        {
            if (name == string.Empty)
            {
                return false;
            }

            using (var en = new HaushaltsrechnerEntities())
            {
                var pass = string.Empty;

                var testUser = en.USER.FirstOrDefault(u => u.NAME == name);

                if (testUser != null)
                {
                    return false;
                }

                if (pw != string.Empty)
                {
                    byte[] encrypted = Cryptography.EncryptStringToBytes_AES(pw);

                    foreach (var e in encrypted)
                    {
                        pass += (char)e;
                    }
                }

                var user = new USER
                {
                    NAME = name,
                    PASSWORT = pass,
                    ISADMIN = sysAdmin,
                    ID = Guid.NewGuid()
                };
                en.USER.AddObject(user);
                en.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Edit a <see cref="HaushaltsRechner.Data.Model.USER" />.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The new name.</param>
        /// <param name="pw">The new password.</param>
        /// <param name="lang">The language.</param>
        /// <param name="sysAdmin">new sys admin.</param>
        /// <returns>
        ///   <c>true</c>, if edition successfull
        /// </returns>
        public static bool EditUser(Guid id, string name, string pw, string lang, bool? sysAdmin)
        {
            if (id == Guid.Empty || name == string.Empty)
            {
                return false;
            }

            using (var en = new HaushaltsrechnerEntities())
            {
                var testUser = en.USER.FirstOrDefault(u => u.NAME == name && u.ID != id);

                if (testUser != null)
                {
                    return false;
                }

                var user = en.USER.FirstOrDefault(u => u.ID == id);

                if (user == null)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(pw))
                {
                    var pass = string.Empty;
                    byte[] encrypted = Cryptography.EncryptStringToBytes_AES(pw);

                    foreach (var e in encrypted)
                    {
                        pass += (char)e;
                    }

                    user.PASSWORT = pass;
                }

                try 
                { 
                    var culture = CultureInfo.CreateSpecificCulture(lang);
                    user.CULTURE = culture.Name;
                }
                catch 
                { 
                }

                user.NAME = name;
                user.ISADMIN = sysAdmin.HasValue ? sysAdmin.Value : user.ISADMIN;                

                en.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Deletes a <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><c>true</c>, if deletion sucessfull</returns>
        public static bool DeleteUser(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var user = en.USER.FirstOrDefault(u => u.ID == id);
                if (user == null)
                {
                    return false;
                }

                user.RIGHT.Clear();

                var mov = en.MOVEMENT.Where(m => m.USER_ID == id);
                foreach (var m in mov)
                {
                    m.USER = null;
                }

                var acc = en.ACCOUNT.Where(a => a.USER.Any(u => u.ID == id));
                foreach (var a in acc)
                {
                    a.USER.Remove(user);
                }

                en.USER.DeleteObject(user);
                en.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="HaushaltsRechner.Data.Model.USER"/> or null</returns>
        public static USER LoginUser(string name, string password)
        {
            var user = GetUserByName(name);

            if (user == null)
            {
                return null;
            }

            var pass = string.Empty;

            if (password != string.Empty)
            {
                byte[] encrypted = Cryptography.EncryptStringToBytes_AES(password);

                foreach (var e in encrypted)
                {
                    pass += (char)e;
                }
            }

            if (user.PASSWORT == pass)
            {
                return user;
            }

            return null;
        }
    }
}