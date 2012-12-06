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
using HaushaltsRechner.Business.Mapper;
using HaushaltsRechner.Business.Security;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Framework.Mapper;
using HaushaltsRechner.Mapper;
using Newtonsoft.Json;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Service for interaction with <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>s
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class CategoryService : System.Web.Services.WebService
    {

        /// <summary>
        /// Gets all <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>s
        /// </summary>
        /// <returns></returns>
        [WebMethod(true)]
        public string GetAllCategories()
        {
            var cats = CategoryManager.GetAllCategories().Select(
                c => new GUIDStringMapperBase 
                { 
                    ID = c.ID,
                    NAME = c.NAME
                });

            return JsonConvert.SerializeObject(cats);
        }

        /// <summary>
        /// Gets the movements by <see cref="HaushaltsRechner.Data.Model.CATEGORY"/> ID.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>stringified list of <see cref="HaushaltsRechner.Business.Mapper.GridMovement"/>s</returns>
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

        /// <summary>
        /// Adds the <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if addition successfull</returns>
        [WebMethod(true)]
        public string AddCategory(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminCategory))
            {
                return bool.FalseString;
            }

            try
            {
                var u = JsonConvert.DeserializeObject<NameAsString>(data);
                if (CategoryManager.AddCategory(u.Name))
                {
                    return bool.TrueString;
                }
            }
            catch
            {
            }

            return bool.FalseString;
        }

        /// <summary>
        /// Edits the <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if edition successfull</returns>
        [WebMethod(true)]
        public string EditCategory(string data)
        {
            if (!RightsManager.HasRight(SessionManager.CurrentUser.ID, Feature.AdminCategory))
            {
                return bool.FalseString;
            }

            try
            {
                var u = JsonConvert.DeserializeObject<GUIDStringMapperBase>(data);
                if (CategoryManager.EditCategory(u.ID, u.NAME))
                {
                    return bool.TrueString;
                }
            }
            catch
            {
            }

            return bool.FalseString;
        }
    }
}
