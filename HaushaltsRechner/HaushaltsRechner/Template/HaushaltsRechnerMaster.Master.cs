﻿/*
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;

namespace HaushaltsRechner.Template
{
    /// <summary>
    /// Masterpage for HaushaltsRechner
    /// </summary>
    public partial class HaushaltsRechnerMaster : System.Web.UI.MasterPage
    {
        CultureInfo culture;

        protected void Page_Load(object sender, EventArgs e)
        {
            culture = Global.ResolveCulture(Request);
            
#if DEBUG
            var scriptName = "~/Scripts/14-GlobalisationResource." + culture.Name + ".js";
            sm.Scripts.Add(new ScriptReference(scriptName));

            var dir = new DirectoryInfo(Server.MapPath("~/Scripts/"));

            foreach (var f in dir.GetFiles("*.debug.js"))
            {
                sm.Scripts.Add(new ScriptReference("~/Scripts/" + f.Name));
            }

            AddJavaScriptFeature("~/Scripts/SlickGrid/");
            AddJavaScriptFeature("~/Scripts/HighCharts/");

#else
            sm.Scripts.Add(new ScriptReference("~/Scripts/jsmin.min." + culture.Name + ".js"));
#endif
        }

        /// <summary>
        /// Adds the java script feature.
        /// </summary>
        /// <param name="folder">The folder of the feature to add.</param>
        private void AddJavaScriptFeature(string folder)
        {
            var dir = new DirectoryInfo(Server.MapPath(folder));

            foreach (var file in dir.GetFiles())
            {
                sm.Scripts.Add(new ScriptReference(folder + file.Name));
            }

            foreach (var d in dir.GetDirectories())
            {
                AddJavaScriptFeature(folder + d.Name + "/");
            }
        }
    }
}