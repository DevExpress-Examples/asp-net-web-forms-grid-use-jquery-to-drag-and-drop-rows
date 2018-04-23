using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using System.Collections;

public partial class _Default : System.Web.UI.Page
{
    DataSet ds;

    protected void Page_Init()
    {         
        if (!IsPostBack)
        {
            //setup empty dataset
            if (!IsCallback)
            {
                ds = new DataSet();
                DataTable tb = new DataTable();
                tb.Columns.Add("ID", typeof(int));
                tb.Columns.Add("ItemCode", typeof(string));
                tb.Columns.Add("Qty", typeof(int));
                tb.PrimaryKey = new DataColumn[] { tb.Columns["ID"] };
                ds.Tables.Add(tb);
                Session["DataSet"] = ds;
            }
        }
        else
        {
            if (IsCallback)
            {
                ds = (DataSet)Session["DataSet"];
                ASPxGridView1.DataSource = ds.Tables[0];
                ASPxGridView1.DataBind();
            }
        }
    }

    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ds = (DataSet)Session["DataSet"];
        ASPxGridView gridView = (ASPxGridView)sender;
        DataTable dataTable = ds.Tables[0];
        DataRow row = dataTable.NewRow();
        e.NewValues["ID"] = dataTable.Rows.Count;
        IDictionaryEnumerator enumerator = e.NewValues.GetEnumerator();
        enumerator.Reset();
        while (enumerator.MoveNext())
            if (enumerator.Key.ToString() != "Count")
                row[enumerator.Key.ToString()] = enumerator.Value;
        gridView.CancelEdit();
        e.Cancel = true;
        dataTable.Rows.Add(row);
    }
}
