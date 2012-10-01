using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    public static class AccountManager
    {
        public static ACCOUNT GetAccountById(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.ACCOUNT.FirstOrDefault(a => a.ID == id);
            }
        }

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

        public static List<ACCOUNT> GetAccountsByUserId(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.ACCOUNT.Where(a => a.USER.Any(u => u.ID == id)).ToList();
            }
        }

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

        public static bool EditAccount(Guid id, string newName)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var acc = en.ACCOUNT.FirstOrDefault(a=>a.ID == id);
                if(acc == null || newName == null || newName == string.Empty)
                {
                    return false;
                }

                acc.NAME = newName;
                en.SaveChanges();

                return true;
            }
        }

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