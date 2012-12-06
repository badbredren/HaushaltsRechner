using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Business.SearchParameter;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Framework.Helper;
using HaushaltsRechner.Framework.Mapper;
using HaushaltsRechner.Mapper;
using Newtonsoft.Json;

namespace HaushaltsRechner.Services
{
    /// <summary>
    /// Service for creating reports
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ReportingService : System.Web.Services.WebService
    {
        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Business.SearchParameter.ReportingSearchParameterType"/>
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetReportingTypes()
        {            
            var types = EnumHelper.GetValues<ReportingSearchParameterType>()
                .Select(
                    t =>
                        new IntegerStringMapperBase
                        {
                            Number = (int)t,
                            Name = HttpContext.GetGlobalResourceObject("Default", t.ToString()).ToString()
                        })
                    .ToList();

            return JsonConvert.SerializeObject(types);
        }

        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Business.SearchParameter.ReportingSearchParameterDirectionType"/>
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetReportingDirectionTypes()
        {
            var types = EnumHelper.GetValues<ReportingSearchParameterDirectionType>()
                .Select(
                    t =>
                        new IntegerStringMapperBase
                        {
                            Number = (int)t,
                            Name = HttpContext.GetGlobalResourceObject("Default", t.ToString()).ToString()
                        })
                    .ToList();

            return JsonConvert.SerializeObject(types);
        }

        /// <summary>
        /// Gets the report.
        /// </summary>
        /// <param name="data">stringified JSON-Data.</param>
        /// <returns>Report</returns>
        [WebMethod(true)]
        public string GetReport(string data)
        {
            var para = JsonConvert.DeserializeObject<ReportingSearchParameter>(data);
            var catSumList = ReportingManager.GetCategorySummaray(para);           

            return JsonConvert.SerializeObject(catSumList);
            
        }
    }
}
