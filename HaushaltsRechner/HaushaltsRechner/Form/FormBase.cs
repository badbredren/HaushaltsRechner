using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HaushaltsRechner.Form
{
    /// <summary>
    /// Base class for all Forms
    /// </summary>
    public class FormBase : System.Web.UI.Page
    {
        /// <summary>
        /// Sets <see cref="P:System.Web.UI.Page.Culture" /> 
        /// and <see cref="P:System.Web.UI.Page.UICulture" /> for the current Therad.
        /// </summary>
        protected override void InitializeCulture()
        {
            var test = Resources.Default.home;

            base.InitializeCulture();
            var culture = Global.ResolveCulture(Request);
            UICulture = culture.TwoLetterISOLanguageName;
            Culture = culture.Name;
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            test = Resources.Default.home;
        }

        protected void SetTitle(string title)
        {
            this.Title = title + " | " + Global.HaushaltsRechner;
        }
    }
}