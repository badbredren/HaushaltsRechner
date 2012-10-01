using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaushaltsRechner.Data.Model;
using System.Security.Cryptography;
using HaushaltsRechner.Business.Security;

namespace HaushaltsRechner.Business.Manager
{
    public static class UserManager
    {
        public static IQueryable<USER> GetAllUser()
        {
            var en = new HaushaltsrechnerEntities();

            return en.USER;
        }

        public static USER GetUserById(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.USER.FirstOrDefault(u => u.ID == id);
            }
        }

        public static USER GetUserByName(string name)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.USER.FirstOrDefault(u => u.NAME == name);
            }
        }

        public static bool AddUser(string name, string pw, bool sysAdmin)
        {            
            if (name == string.Empty)
            {
                return false;
            }

            using (var en = new HaushaltsrechnerEntities())
            {
                var pass = string.Empty;
                if (pw != string.Empty)
                {
                    byte[] encrypted = Cryptography.EncryptStringToBytes_AES(pw);

                    foreach (var e in encrypted)
                    {
                        pass += (char)e;
                    }
                }
                var u = new USER
                {
                    NAME = name,
                    PASSWORT = pass,
                    ISADMIN = sysAdmin,
                    ID = Guid.NewGuid()
                };
                en.USER.AddObject(u);
                en.SaveChanges();
                return true;
            }
        }

        public static bool EditUser(Guid id, string name, string pw, bool sysAdmin)
        {
            if (id == Guid.Empty || name == string.Empty)
            {
                return false;
            }

            using (var en = new HaushaltsrechnerEntities())
            {
                var pass = string.Empty;
                if (pw != string.Empty)
                {
                    byte[] encrypted = Cryptography.EncryptStringToBytes_AES(pw);

                    foreach (var e in encrypted)
                    {
                        pass += (char)e;
                    }
                }
                var user = en.USER.FirstOrDefault(u => u.ID == id);

                if (user == null)
                {
                    return false;
                }

                user.NAME = name;
                user.PASSWORT = pass;
                user.ISADMIN = sysAdmin;

                en.SaveChanges();
                return true;
            }
        }

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
    }
}