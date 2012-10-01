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
using HaushaltsRechner.UIProcess;
using HaushaltsRechner.Data.Model;
using System.Web.UI.WebControls.Expressions;
using HaushaltsRechner.Business.SearchParameter;

namespace HaushaltsRechner.Controls
{
    public partial class UebersichtSearch : System.Web.UI.UserControl
    {
        public event EventHandler StartSearch;
        
        private void OnStartSearch()
        {
            if (StartSearch != null)
            {
                StartSearch(this, null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }

        protected void InitializeControls()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "UEBERSICHTSEARCH" + ClientID, "DocumentReady_" + ClientID + "();", true);
        }

        public void FilterAccounts(object sender, CustomExpressionEventArgs e)
        {
            var userID = SessionManager.CurrentUser.ID;
            e.Query = e.Query.Cast<ACCOUNT>().Where(a => a.USER.Any(u => u.ID == userID));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var para = new UebersichtSearchParameter();
            
            DateTime dFrom;
            if (DateTime.TryParse(tbDateFrom.Text, out dFrom))
            {
                para.DateFrom = dFrom;
            }
            else
            {
                para.DateFrom = DateTime.MinValue;
            }

            DateTime dTo;
            if (DateTime.TryParse(tbDateTo.Text, out dTo))
            {
                para.DateTo = dTo.AddHours(23.9);
            }
            else
            {
                para.DateTo = DateTime.MinValue;
            }

            DateTime dEditFrom;
            if (DateTime.TryParse(tbEditFrom.Text, out dEditFrom))
            {
                para.EditFrom = dEditFrom;
            }
            else
            {
                para.EditFrom = DateTime.MinValue;
            }

            DateTime dEditTo;
            if (DateTime.TryParse(tbEditTo.Text, out dEditTo))
            {
                para.EditTo = dEditTo.AddHours(23.9);
            }
            else
            {
                para.EditTo = DateTime.MinValue;
            }

            Guid accID;
            if (Guid.TryParse(lbAccount.SelectedValue, out accID))
            {
                para.AccountID = accID;
            }

            Guid userID;
            if (Guid.TryParse(lbUser.SelectedValue, out userID))
            {
                para.UserID = userID;
            }

            Guid reasonID;
            if (Guid.TryParse(lbReason.SelectedValue, out reasonID))
            {
                para.ReasonID = reasonID;
            }

            decimal aFrom;
            if (tbAmountFrom.Text != string.Empty && decimal.TryParse(tbAmountFrom.Text.Replace("€", string.Empty).Replace(".", string.Empty), out aFrom))
            {
                para.ValueFrom = aFrom;
            }
            else
            {
                para.ValueFrom = decimal.MinValue;
            }

            decimal aTo;
            if (tbAmountTo.Text != string.Empty && decimal.TryParse(tbAmountTo.Text.Replace("€", string.Empty).Replace(".", string.Empty), out aTo))
            {
                para.ValueTo = aTo;
            }
            else
            {
                para.ValueTo = decimal.MinValue;
            }

            SessionManager.SearchParameter = para;
            OnStartSearch();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if(SessionManager.SearchParameter != null && SessionManager.SearchParameter.GetType() == typeof(UebersichtSearchParameter))
            {
                tbDateFrom.Text = string.Empty;
                tbDateTo.Text = string.Empty;
                tbEditFrom.Text = string.Empty;
                tbEditTo.Text = string.Empty;
                tbAmountFrom.Text = string.Empty;
                tbAmountTo.Text = string.Empty;
                lbAccount.SelectedIndex = 0;
                lbUser.SelectedIndex = 0;
                
                SessionManager.SearchParameter = null;
                OnStartSearch();
            }
        }

        protected void lbUser_DataBound(object sender, EventArgs e)
        {
            lbUser.Items.Insert(0, string.Empty);
        }

        protected void lbAccount_DataBound(object sender, EventArgs e)
        {
            lbAccount.Items.Insert(0, string.Empty);
        }

        protected void lbReason_DataBound(object sender, EventArgs e)
        {
            lbReason.Items.Insert(0, string.Empty);
        }
    }
}