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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using HaushaltsRechner.UIProcess;
using HaushaltsRechner.Data.Model;
using HaushaltsRechner.Business.Manager;
using HaushaltsRechner.Business.SearchParameter;
using Newtonsoft.Json;
using HaushaltsRechner.UIProcess.Mapper;

namespace HaushaltsRechner.Controls
{
    public partial class UebersichtResult : System.Web.UI.UserControl
    {
        public string SelectedMovement
        {
            get { return tbHiddenSelectedMovement.Text; }
        }

        public Button RefreshButton
        {
            get { return btnHiddenRefresh; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        public void InitializeControls()
        {
            //gvMovements.DataSource = null;
            //gvMovements.DataBind();
        }

        public void FilterMovements(object sender, CustomExpressionEventArgs e)
        {
            //if (SessionManager.SearchParameter == null || SessionManager.SearchParameter.GetType() != typeof(UebersichtSearchParameter))
            //{
            //    e.Query = e.Query.Cast<V_OVERVIEW>().Where(m => m.ID == Guid.Empty);
            //    lblCount.Text = "0";
            //    return;
            //}

            //e.Query = MovementManager.GetMovementsBySearchparameter(
            //    (UebersichtSearchParameter)SessionManager.SearchParameter,
            //    SessionManager.CurrentUser.ID);           
        }

        protected void gvMovements_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (gvMovements.SelectedRow != null)
            //{
            //    var idStr = (gvMovements.SelectedRow.Cells[8].Controls[1] as Label).Text;
            //    Guid id;
            //    if (Guid.TryParse(idStr, out id))
            //    {
            //        var m = MovementManager.GetMovementById(id);

                    
            //        var mov = new EditMovement
            //        {
            //            ID = m.ID,
            //            ACCOUNT_ID=m.ACCOUNT_ID,
            //            CATEGORY_ID = m.CATEGORY_ID,
            //            REASON_ID = m.REASON_ID,
            //            MESSAGE = m.MESSAGE,
            //            AMOUNT = m.AMOUNT,
            //            DATE_ADDED = m.DATE_ADDED.ToShortDateString()
            //        };

            //        ScriptManager.RegisterStartupScript(this, GetType(), "SELECTMOVEMENT_"
            //            + ClientID, "var selectedMovement = '" + JsonConvert.SerializeObject(mov) + "';", true);
            //    }
            //}            
        }

        protected void btnHiddenRefresh_Click(object sender, EventArgs e)
        {
            InitializeControls();
        }
    }
}