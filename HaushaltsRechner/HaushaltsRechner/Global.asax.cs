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
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.Text;
using Microsoft.Ajax.Utilities;
using System.Globalization;
using System.Collections;
//using System.Resources;
using System.Threading;

namespace HaushaltsRechner
{
    public class Global : System.Web.HttpApplication
    {
        public static List<CultureInfo> cultures = new List<CultureInfo>();
        public static readonly string HaushaltsRechner = "Haushaltsrechner";

        protected void Application_Start(object sender, EventArgs e)
        {
            cultures.Add(new CultureInfo("de-DE"));
            cultures.Add(new CultureInfo("en-US"));
            WriteGlobalizationScriptFiles();
#if !DEBUG
            MinifyJS();
#endif
        }

        /// <summary>
        /// Writes minified JavaScript files for different cultures
        /// </summary>
        private void MinifyJS()
        {
            var source = new StringBuilder();
            var folder = Server.MapPath("~/Scripts/");
            var dir = new DirectoryInfo(folder);
            var files = dir.GetFiles("*.debug.js");

            for(var i = 0; i< files.Length; i += 1) 
            {
                var file = files[i];
                source.Append(File.ReadAllText(folder + file.Name));
            }

            source.Append(AddJavaScriptFeatures(Server.MapPath("~/Scripts/SlickGrid/")));
            source.Append(AddJavaScriptFeatures(Server.MapPath("~/Scripts/HighCharts/")));

            foreach (var culture in cultures)
            {
                var cultureSource = source.ToString();

                var cultureFileSource = File.ReadAllText(Server.MapPath("~/Scripts/")
                            + "14-GlobalisationResource." + culture.Name + ".js");

                cultureSource += cultureFileSource;

                var minifier = new Minifier();
                using (var sw = new StreamWriter(folder + "jsmin.min." + culture.Name + ".js"))
                {
                    sw.Write(minifier.MinifyJavaScript(cultureSource.ToString()));
                }
            }
        }

        /// <summary>
        /// Writes globalizing files for de-DE && en-US
        /// </summary>
        private void WriteGlobalizationScriptFiles()
        {
            foreach (var culture in cultures)
            {
                var objList = new List<object>();
                var valueString = new StringBuilder();

                valueString.Append("function getLocalText(){\n");

                var resources =
                    Resources.Default.ResourceManager.GetResourceSet(culture, true, true);

                foreach (DictionaryEntry res in resources)
                {
                    valueString.Append("this.");
                    valueString.Append(res.Key);
                    valueString.Append("='");
                    valueString.Append(res.Value);
                    valueString.Append("'");
                    valueString.Append(",\n");
                }

                //letztes Komma entfernen
                valueString.Remove(valueString.Length - 2, 1);
                valueString.Append("};");

                using (var sw = new StreamWriter(
                            Server.MapPath("~/Scripts/")
                            + "14-GlobalisationResource." + culture.Name + ".js"))
                {
                    sw.Write(valueString);
                }
            }
        }

        /// <summary>
        /// Reads all *.js file recursivly from a folder
        /// !Deletes folders after writing reading files!
        /// </summary>
        /// <param name="folder">Folder, which contains *.js files</param>
        /// <returns>All scripts from folder</returns>
        private StringBuilder AddJavaScriptFeatures(string folder)
        {
            var str = new StringBuilder();
            var dir = new DirectoryInfo(folder);

            //Dateien hinzufügen
            foreach (var file in dir.GetFiles())
            {
                str.Append(File.ReadAllText(file.FullName));
            }

            //tiefer liegende Ordner hinzufügen
            foreach (var f in dir.GetDirectories()) 
            {                
                str.Append(AddJavaScriptFeatures(f.FullName));
            }

            return str;
        }

        /// <summary>
        /// Gets the users cultureinfo from the browser specs
        /// or, if set, from the loged in users CULTURE info
        /// </summary>
        /// <returns>from the users browser</returns>
        public static CultureInfo ResolveCulture(HttpRequest request)
        {
            var user = SessionManager.CurrentUser;

            if (user != null && !string.IsNullOrEmpty(user.CULTURE))
            {
                try
                {
                    return CultureInfo.CreateSpecificCulture(user.CULTURE);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }

            var languages = request.UserLanguages;

            if (languages == null || languages.Length == 0)
            {
                return null;
            }

            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the users RegionInfo from the browser specs
        /// or, if set, from the loged in users CULTURE info
        /// </summary>
        /// <returns>from the users browser</returns>
        public static RegionInfo ResolveCountry(HttpRequest request)
        {
            CultureInfo culture = ResolveCulture(request);

            if (culture != null)
            {
                return new RegionInfo(culture.LCID);
            }

            return null;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}