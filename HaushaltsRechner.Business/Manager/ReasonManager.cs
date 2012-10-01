using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    public static class ReasonManager
    {
        public static REASON GetReason(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.REASON.FirstOrDefault(r => r.ID == id);
            }
        }

        public static List<REASON> GetReasons(string pre)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.REASON.Where(r => r.TEXT.StartsWith(pre)).ToList();
            }
        }

        public static REASON CreateReason(string text)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var rOld = en.REASON.Where(r => r.TEXT == text);

                if (!rOld.Any())
                {
                    var r = new REASON
                    {
                        ID = Guid.NewGuid(),
                        TEXT = text
                    };

                    en.REASON.AddObject(r);
                    en.SaveChanges();

                    return r;
                }

                return rOld.First();
            }
        }
    }
}
