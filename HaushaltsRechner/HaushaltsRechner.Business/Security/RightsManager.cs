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
using System.Text;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Security
{
    /// <summary>
    /// Provides methods to manage user rights
    /// </summary>
    public static class RightsManager
    {
        /// <summary>
        /// Gets all features.
        /// </summary>
        /// <returns>List of all Features</returns>
        public static List<string> GetAllFeatures()
        {
            var lst = GetAdminFeatures();

            lst.Add(Feature.Kategorie.ToString());
            lst.Add(Feature.Uebersicht.ToString());

            return lst;
        }

        /// <summary>
        /// Gets all rights.
        /// </summary>
        /// <returns>List of all <see cref="RIGHT"/></returns>
        public static List<RIGHT> GetRights()
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.RIGHT.ToList();
            }
        }

        /// <summary>
        /// Gets the admin features.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAdminFeatures()
        {
            var lst = new List<string>();
            lst.Add(Feature.Admin.ToString());
            lst.Add(Feature.AdminAccount.ToString());
            lst.Add(Feature.AdminCategory.ToString());
            lst.Add(Feature.AdminMovement.ToString());
            lst.Add(Feature.AdminUser.ToString());

            return lst;
        }

        /// <summary>
        /// Determines whether the specified <see cref="USER"/> has right to use a <see cref="Feature"/>.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="feat">The <see cref="Feature"/> to check.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="USER"/> has right; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasRight(Guid userID, Feature feat)
        {
            var featName = feat.ToString();
            using(var en = new HaushaltsrechnerEntities())
            {
                var right = en.RIGHT.FirstOrDefault(r => r.NAME == featName);
                var user = en.USER.FirstOrDefault(u=>u.ID == userID);

                return (right != null && user != null)
                    ? HasRight(user, right)
                    : false;
            }
        }

        /// <summary>
        /// Gets the <see cref="RIGHT"/>s for a <see cref="USER"/>
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public static List<RIGHT> GetRights(Guid userID)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var user = en.USER.FirstOrDefault(u => u.ID == userID);
                
                if (user != null)
                {
                    return user.RIGHT.ToList();
                }
                return null;
            }
        }

        /// <summary>
        /// Determines whether a <see cref="USER"/> has any Admin-<see cref="RIGHT"/>
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns>
        ///   <c>true</c> if a <see cref="USER"/> has any Admin-<see cref="RIGHT"/>; 
        ///   otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAdminRight(Guid userID)
        {            
            var rights = GetRights(userID);
            var adminFeatures = GetAdminFeatures();
            
            if (rights.Any(r => adminFeatures.Contains(r.NAME)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the <see cref="RIGHT"/> for a <see cref="USER"/>.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="rightID">The right ID.</param>
        /// <param name="set">
        /// if set to <c>true</c> set <see cref="RIGHT"/>.
        /// if set to <c>false</c> remove <see cref="RIGHT"/></param>
        /// <returns></returns>
        public static bool SetRight(Guid userID, Guid rightID, bool set)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var user = en.USER.FirstOrDefault(u => u.ID == userID);
                var right = en.RIGHT.FirstOrDefault(r => r.ID == rightID);

                if (user == null || right == null)
                {
                    return false;
                }

                if (set)
                {
                    if (!user.RIGHT.Contains(right))
                    {
                        user.RIGHT.Add(right);
                    }
                }
                else
                {
                    if (user.RIGHT.Contains(right))
                    {
                        user.RIGHT.Remove(right);
                    }
                }
                en.SaveChanges();
            }

            return true;
        }

        private static bool HasRight(USER user, RIGHT right)
        {
            if (user.ISADMIN.HasValue && user.ISADMIN.Value)
            {
                return true;
            }

            var uId = user.ID;
            var rId = right.ID;

            using (var en = new HaushaltsrechnerEntities())
            {
                return en.USER.Any(u => u.ID == uId && u.RIGHT.Any(r => r.ID == rId));
            }
        }
    }
}
