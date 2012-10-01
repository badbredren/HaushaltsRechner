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
    public static class RightsManager
    {
        public static List<string> GetAllFeatures()
        {
            var lst = GetAdminFeatures();

            lst.Add(Feature.Kategorie.ToString());
            lst.Add(Feature.Uebersicht.ToString());

            return lst;
        }

        public static List<RIGHT> GetRights()
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.RIGHT.ToList();
            }
        }

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
    }
}
