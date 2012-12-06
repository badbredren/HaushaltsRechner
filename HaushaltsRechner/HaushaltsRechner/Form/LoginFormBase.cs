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

namespace HaushaltsRechner.Form
{
    /// <summary>
    /// Base class for all aspx sites to check LoginState
    /// </summary>
    public class LoginFormBase : FormBase
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (SessionManager.CurrentUser == null)
            {
                    Response.Redirect("~/Form/Start.aspx");
            }
        }

        protected virtual void InitializeControls()
        {

        }
    }
}