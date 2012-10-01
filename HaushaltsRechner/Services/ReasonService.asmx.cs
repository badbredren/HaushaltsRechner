using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Business.Manager;
using Newtonsoft.Json;
using HaushaltsRechner.UIProcess.Mapper;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für ReasonService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Um das Aufrufen dieses Webdiensts aus einem Skript mit ASP.NET AJAX zuzulassen, heben Sie die Auskommentierung der folgenden Zeile auf. 
    [System.Web.Script.Services.ScriptService]
    public class ReasonService : System.Web.Services.WebService
    {

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
                        r.TEXT    
                    });
            }
            return JsonConvert.SerializeObject(lst);
        }

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
    }
}
