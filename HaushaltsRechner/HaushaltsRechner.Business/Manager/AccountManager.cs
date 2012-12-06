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

namespace HaushaltsRechner.Business.Manager
{
    /// <summary>
    /// Provides CRUD methods to interact with <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>
    /// </summary>
    public static class AccountManager
    {
        /// <summary>
        /// Get an account by its ID
        /// </summary>
        /// <param name="id">ID of the <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/></param>
        /// <returns>
        /// <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> to the ID or null, if no 
        /// <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> was found. 
        /// </returns>
        public static ACCOUNT GetAccountById(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.ACCOUNT.FirstOrDefault(a => a.ID == id);
            }
        }

        /// <summary>
        /// Creates an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> with the specified name and one 
        /// <see cref="HaushaltsRechner.Data.Model.USER"/>
        /// </summary>
        /// <param name="name">Name of the new <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/></param>
        /// <param name="userId">ID of the first <see cref="HaushaltsRechner.Data.Model.USER"/>
        /// of the <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/></param>
        /// <returns>
        /// The created <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>
        /// or an old <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>, if Name is already in DB
        /// </returns>
        public static ACCOUNT CreateAccount(string name, Guid userId)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var user = en.USER.FirstOrDefault(u => u.ID == userId);
                var aOld = en.ACCOUNT.First(r => r.NAME == name && r.USER.Any(u => u.ID == user.ID));

                if (aOld == null)
                {
                    var a = new ACCOUNT
                    {
                        ID = Guid.NewGuid(),
                        NAME = name
                    };

                    a.USER.Add(user);
                    en.ACCOUNT.AddObject(a);
                    en.SaveChanges();

                    return a;
                }

                return aOld;
            }
        }

        /// <summary>
        /// Returns all <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>s, the <see cref="HaushaltsRechner.Data.Model.USER"/> contains
        /// </summary>
        /// <param name="id">ID of the <see cref="HaushaltsRechner.Data.Model.USER"/></param>
        /// <returns>List of <see cref="ACCOUNT"/></returns>
        public static List<ACCOUNT> GetAccountsByUserId(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.ACCOUNT.Where(a => a.USER.Any(u => u.ID == id)).ToList();
            }
        }

        /// <summary>
        /// Adds an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>.
        /// </summary>
        /// <param name="name">The new name.</param>
        /// <returns><c>false</c>, if <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> already exists</returns>
        public static bool AddAccount(string name)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var aOld = en.ACCOUNT.FirstOrDefault(r => r.NAME == name);

                if (aOld == null)
                {
                    var a = new ACCOUNT
                    {
                        ID = Guid.NewGuid(),
                        NAME = name
                    };

                    en.ACCOUNT.AddObject(a);
                    en.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Edits an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newName">The new name.</param>
        /// <returns><c>true</c>, if edition successfull </returns>
        public static bool EditAccount(Guid id, string newName)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var acc = en.ACCOUNT.FirstOrDefault(a=>a.ID == id);
                if(acc == null || string.IsNullOrEmpty(newName))
                {
                    return false;
                }

                acc.NAME = newName;
                en.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Edits an <see cref="ACCOUNT"/> user.
        /// </summary>
        /// <param name="accId">The <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/> id.</param>
        /// <param name="userId">The <see cref="HaushaltsRechner.Data.Model.USER"/> id.</param>
        /// <param name="set">
        /// if set to <c>true</c>,
        /// <see cref="HaushaltsRechner.Data.Model.USER"/> will be added to <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>
        /// </param>
        /// <returns><c>true</c>, if edition successfull</returns>
        public static bool EditAccountUsers(Guid accId, Guid userId, bool set)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var acc = en.ACCOUNT.FirstOrDefault(a => a.ID == accId);
                if (acc == null || userId == Guid.Empty)
                {
                    return false;
                }

                var user = en.USER.FirstOrDefault(u => u.ID == userId);
                if (user == null)
                {
                    return false;
                }

                if (set && !acc.USER.Contains(user))
                {
                    acc.USER.Add(user);
                }
                else if (!set && acc.USER.Contains(user))
                {
                    acc.USER.Remove(user);                    
                }

                en.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Deletes an <see cref="HaushaltsRechner.Data.Model.ACCOUNT"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><c>true</c>, if deletion successfull</returns>
        public static bool DeleteAccount(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var acc = en.ACCOUNT.FirstOrDefault(a => a.ID == id);
                if (acc == null)
                {
                    return false;
                }

                acc.USER.Clear();

                var movements = acc.MOVEMENT.ToList();
                acc.MOVEMENT.Clear();
                foreach (var m in movements)
                {
                    en.MOVEMENT.DeleteObject(m);
                }

                en.ACCOUNT.DeleteObject(acc);
                en.SaveChanges();

                return true;
            }
        }
    }
}