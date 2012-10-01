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
using HaushaltsRechner.UIProcess;
using System.IO;
using System.Text;
using Microsoft.Ajax.Utilities;

namespace HaushaltsRechner
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
#if !DEBUG
            MinifyJS();
#endif
        }

        private void MinifyJS()
        {
            var source = new StringBuilder();
            var folder = Server.MapPath("~/Scripts/");
            var dir = new DirectoryInfo(folder);

            foreach (var f in dir.GetFiles("*.debug.js"))
            {
                source.Append(File.ReadAllText(folder + f.Name));
            }

            source.Append(AddJavaScriptFeatures(Server.MapPath("~/Scripts/SlickGrid/")));

            var minifier = new Minifier();
            using (var sw = new StreamWriter(folder + "jsmin.min.js"))
            {
                sw.Write(minifier.MinifyJavaScript(source.ToString()));
            }
        }

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