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
