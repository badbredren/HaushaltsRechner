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
using HaushaltsRechner.Data.Model;
using System.Web.SessionState;
using HaushaltsRechner.Business.SearchParameter;
using HaushaltsRechner.Business.Manager;

namespace HaushaltsRechner
{
    /// <summary>
    /// Static class for accessing session related Properties
    /// </summary>
    public static class SessionManager
    {
        private static string currentUser = "CurrentUser";
        private static string searchparameter = "SearchParameter";

        /// <summary>
        /// Gets the current Sessionstate.
        /// </summary>
        /// <value>
        /// The Sessionstate.
        /// </value>
        public static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </summary>
        /// <value>
        /// The current <see cref="HaushaltsRechner.Data.Model.USER"/>.
        /// </value>
        public static USER CurrentUser
        {
            get
            {
                if (Session != null)
                {
                    return Session[currentUser] != null
                        ? UserManager.GetUserById((Session[currentUser] as USER).ID)
                        : null;
                }

                return null;
            }
            set
            {
                Session[currentUser] = value;
            }
        }

        /// <summary>
        /// Gets or sets the search parameter.
        /// </summary>
        /// <value>
        /// The search parameter.
        /// </value>
        public static SearchParameterBase SearchParameter
        {
            get
            {
                if (Session != null)
                {
                    return Session[searchparameter] != null
                        ? Session[searchparameter] as SearchParameterBase
                        : null;
                }
                return null;
            }
            set
            {
                Session[searchparameter] = value;
            }
        }

    }
}