using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

public partial class _Default : System.Web.UI.Page {
    Helper helper = new Helper();
    protected void Page_Init(object sender, EventArgs e) {
        if (!IsPostBack)
            Session.Clear();

        helper.BindGrids(GridFrom, GridTo);

    }
    protected void cbPanel_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e) {
        var rowKey = e.Parameter.Split('|')[0];
        var leftToRight = Convert.ToBoolean(e.Parameter.Split('|')[1]);

        var source = leftToRight ? helper.GetDraggableDataSource() : helper.GetDroppableDataSource();
        var target = leftToRight ? helper.GetDroppableDataSource() : helper.GetDraggableDataSource();

        //update target datasource
        var sourceRow = source.AsEnumerable()
            .Where(x => x.Field<int>("CategoryID") == Convert.ToInt32(rowKey))
            .SingleOrDefault();
        target.ImportRow(sourceRow);

        //remove source data
        source.Rows.Remove(sourceRow);

        GridFrom.DataBind();
        GridTo.DataBind();
    }
}