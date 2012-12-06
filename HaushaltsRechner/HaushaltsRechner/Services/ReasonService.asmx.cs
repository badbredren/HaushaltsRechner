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
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Business.Manager;
using Newtonsoft.Json;
using HaushaltsRechner.Mapper;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Service for interaction with <see cref="HaushaltsRechner.Data.Model.REASON"/>s
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ReasonService : System.Web.Services.WebService
    {
        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Data.Model.REASON"/>s.
        /// </summary>
        /// <param name="pre">The fisst charaters of the text.</param>
        /// <returns>List of <see cref="HaushaltsRechner.Data.Model.REASON"/>s</returns>
        [WebMethod]
        public string GetReasons(string pre)
        {
            var reasons = ReasonManager.GetReasons(pre);
            var lst = new List<Object>();
            foreach (var r in reasons)
            {
                lst.Add(
                    new 
                    { 
                        r.TEXT,
                        r.ID
                    });
            }
            return JsonConvert.SerializeObject(lst);
        }

        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Data.Model.REASON"/> by id.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns><see cref="HaushaltsRechner.Data.Model.REASON"/> or <c>null</c></returns>
        [WebMethod]
        public string GetReasonById(string data)
        {
            var id = JsonConvert.DeserializeObject<Guid>(data);
            
            if (id != Guid.Empty)
            {
                var r = ReasonManager.GetReason(id);
                var reason = new EditReason
                {
                    NAME = r.TEXT,
                    ID = r.ID
                };
                return JsonConvert.SerializeObject(reason);
            }

            return JsonConvert.SerializeObject(new REASON());
        }

        /// <summary>
        /// Gets all <see cref="HaushaltsRechner.Data.Model.REASON"/>s.
        /// </summary>
        /// <returns>all <see cref="HaushaltsRechner.Data.Model.REASON"/>s</returns>
        [WebMethod(true)]
        public string GetAllReasons()
        {
            return GetReasons(string.Empty);
        }
    }
}
