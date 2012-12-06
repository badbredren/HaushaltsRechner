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
using Newtonsoft.Json;
using HaushaltsRechner.Mapper;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Business.SearchParameter;
using HaushaltsRechner.Business.Mapper;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Service for interaction with <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class MovementService : System.Web.Services.WebService
    {
        /// <summary>
        /// Gets all <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>all <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s </returns>
        [WebMethod(true)]
        public string GetAllMovements(string data)
        {
            var para = JsonConvert.DeserializeObject<MovementSearchParameter>(data);
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
            return JsonConvert.SerializeObject(retValues);
        }

        /// <summary>
        /// Adds the <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if addition successfull</returns>
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
        
        /// <summary>
        /// Edits the <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if edition successfull</returns>
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

        /// <summary>
        /// Deletes the <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>(string)true, if deletion successfull</returns>
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
