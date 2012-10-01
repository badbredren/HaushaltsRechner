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

        [WebMethod(true)]
        public string GetCategorySummary()
        {
            var userID = SessionManager.CurrentUser.ID;
            var catSumList = new List<CategorySummary>();
            using (var en = new HaushaltsrechnerEntities())
            {                
                var movements = en.MOVEMENT.Where(
                    m =>
                        m.ACCOUNT.USER.Any(u =>
                            u.ID == userID));
                var categories = en.CATEGORY;

                foreach (var cat in categories)
                {
                    var movInCat = movements.Where(
                        m =>
                            m.CATEGORY_ID == cat.ID);

                    var movInCatsGridMovementList = new List<GridMovement>();

                    foreach (var m in movInCat)
                    {
                        movInCatsGridMovementList.Add(new GridMovement
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

                    var catSum = new CategorySummary(cat.ID, cat.NAME, movInCatsGridMovementList);
                    catSumList.Add(catSum);
                }
                
                return JsonConvert.SerializeObject(catSumList);
            }
        }
    }
}
