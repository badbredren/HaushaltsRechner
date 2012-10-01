using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.UIProcess;
using HaushaltsRechner.UIProcess.Mapper;
using Newtonsoft.Json;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für CategoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Um das Aufrufen dieses Webdiensts aus einem Skript mit ASP.NET AJAX zuzulassen, heben Sie die Auskommentierung der folgenden Zeile auf. 
    [System.Web.Script.Services.ScriptService]
    public class CategoryService : System.Web.Services.WebService
    {

        [WebMethod(true)]
        public string GetAllCategories()
        {
            var cats = CategoryManager.GetAllCategories().ToList();

            return JsonConvert.SerializeObject(cats);
        }

        [WebMethod(true)]
        public string GetMovementsByCategoryID(string data)
        {
            var id = JsonConvert.DeserializeObject<Guid>(data);
            var userID = SessionManager.CurrentUser.ID;
            var retValues = new List<GridMovement>();

            using (var en = new HaushaltsrechnerEntities())
            {
                var movements = en.MOVEMENT.Where(
                    m => 
                        m.CATEGORY_ID == id &&
                        m.ACCOUNT.USER.Any(u=>u.ID == userID));

                foreach (var m in movements)
                {
                    retValues.Add(new GridMovement
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

                return JsonConvert.SerializeObject(retValues);                
            }
        }
    }
}
