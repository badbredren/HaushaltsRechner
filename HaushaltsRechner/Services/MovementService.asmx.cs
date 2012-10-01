using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using HaushaltsRechner.UIProcess.Mapper;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.UIProcess;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Business.SearchParameter;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für MovementService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Um das Aufrufen dieses Webdiensts aus einem Skript mit ASP.NET AJAX zuzulassen, heben Sie die Auskommentierung der folgenden Zeile auf. 
    [System.Web.Script.Services.ScriptService]
    public class MovementService : System.Web.Services.WebService
    {
        [WebMethod(true)]
        public string GetAllMovements(string data)
        {
            var para = JsonConvert.DeserializeObject<UebersichtSearchParameter>(data);
            var retValues = new List<GridMovement>();
            var movements = MovementManager.GetMovementsBySearchparameter(
                para,
                SessionManager.CurrentUser.ID);

            foreach (var m in movements)
            {
                retValues.Add(
                    new GridMovement
                    {
                        ID = m.ID,
                        Amount = m.AMOUNT,
                        CategoryName = m.CATEGORY.NAME,
                        ReasonText = m.REASON.TEXT,
                        UserName = m.USER.NAME,
                        AccountName = m.ACCOUNT.NAME,
                        DateAdded = m.DATE_ADDED.ToShortDateString(),
                        DateEdit = m.DATE_EDIT.HasValue ? m.DATE_EDIT.Value.ToShortDateString() : string.Empty,
                        Message = m.MESSAGE,
                        CategoryID = m.CATEGORY_ID,
                        ReasonID = m.REASON_ID,
                        AccountID = m.ACCOUNT_ID
                    });
            }

            return JsonConvert.SerializeObject(objects);
        }

        [WebMethod(true)]
        public string AddMovement(string data)
        {
            try
            {
                var movement = JsonConvert.DeserializeObject<NewMovement>(data);
                var en = new HaushaltsrechnerEntities();

                var acc = en.ACCOUNT.FirstOrDefault(a=>a.ID == movement.AccountID);
                var cat = en.CATEGORY.FirstOrDefault(c=>c.ID == movement.CategoryID);
                var user = en.USER.FirstOrDefault(u => u.ID == SessionManager.CurrentUser.ID);
                var rea = en.REASON.Where(r => r.TEXT == movement.Reason);

                REASON reason;
                if (rea.Any())
                {
                    reason = rea.First();
                }
                else
                {
                    reason = new REASON
                    {
                        ID = Guid.NewGuid(),
                        TEXT = movement.Reason
                    };
                    en.REASON.AddObject(reason);
                }

                var m = new MOVEMENT
                {
                    ID = movement.ID,
                    DATE_ADDED = movement.DateAdded,
                    DATE_EDIT = DateTime.Now,
                    AMOUNT = Decimal.Parse(movement.Amount.Replace("€",string.Empty)),
                    MESSAGE = movement.Message,
                    USER = user,
                    ACCOUNT = acc,
                    CATEGORY = cat,
                    REASON = reason
                };
                
                en.MOVEMENT.AddObject(m);
                en.SaveChanges();
                return bool.TrueString;
            }
            catch
            {
                return bool.FalseString;
            }
        }


        [WebMethod(true)]
        public string EditMovement(string data)
        {
            try
            {
                var movement = JsonConvert.DeserializeObject<NewMovement>(data);
                var en = new HaushaltsrechnerEntities();

                var acc = en.ACCOUNT.FirstOrDefault(a => a.ID == movement.AccountID);
                var cat = en.CATEGORY.FirstOrDefault(c => c.ID == movement.CategoryID);
                var user = en.USER.FirstOrDefault(u => u.ID == SessionManager.CurrentUser.ID);
                var rea = en.REASON.Where(r => r.TEXT == movement.Reason);

                REASON reason;
                if (rea.Any())
                {
                    reason = rea.First();
                }
                else
                {
                    reason = new REASON
                    {
                        ID = Guid.NewGuid(),
                        TEXT = movement.Reason
                    };
                    en.REASON.AddObject(reason);
                }

                var mov = en.MOVEMENT.FirstOrDefault(m => m.ID == movement.ID);

                mov.DATE_EDIT = DateTime.Now;
                mov.DATE_ADDED = movement.DateAdded;
                mov.AMOUNT = Decimal.Parse(movement.Amount.Replace("€", string.Empty));
                mov.MESSAGE = movement.Message;
                mov.ACCOUNT = acc;
                mov.CATEGORY = cat;
                mov.REASON = reason;                

                en.SaveChanges();
                return bool.TrueString;
            }
            catch
            {
                return bool.FalseString;
            }
        }

        [WebMethod]
        public string DeleteMovement(string data) 
        {
            var id = JsonConvert.DeserializeObject<Guid>(data);
            if (MovementManager.DeleteMovement(id))
            {
                return bool.TrueString;
            }

            return bool.FalseString;
        }
    }
}
