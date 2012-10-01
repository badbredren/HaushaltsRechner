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