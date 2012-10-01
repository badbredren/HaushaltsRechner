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