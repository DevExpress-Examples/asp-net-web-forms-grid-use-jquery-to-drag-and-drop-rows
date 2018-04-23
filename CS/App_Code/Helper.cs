using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DevExpress.Web.ASPxGridView;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper {
    private const string droppableSource = "droppableDS";
    private const string draggableSource = "draggableDS";


    public void BindGrids(ASPxGridView grid1, ASPxGridView grid2) {
        

        grid1.DataSource = GetDraggableDataSource();
        grid1.DataBind();

        grid2.DataSource = GetDroppableDataSource();
        grid2.DataBind();
    }
    public DataTable GetDraggableDataSource() {

        if (HttpContext.Current.Session[draggableSource] == null) {
            var command = "SELECT CategoryID, CategoryName, Description FROM Categories";
            var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + HttpContext.Current.Server.MapPath(@"~/App_Data/Categories + products.mdb");
            var adapter = new System.Data.OleDb.OleDbDataAdapter(command, connectionString);
            var dt = new DataTable();
            adapter.Fill(dt);
            HttpContext.Current.Session[draggableSource] = dt;
        }
        return HttpContext.Current.Session[draggableSource] as DataTable;
    }
    public DataTable GetDroppableDataSource() {
        if (HttpContext.Current.Session[droppableSource] == null)
            HttpContext.Current.Session[droppableSource] = CreateDraggableTable();
        return HttpContext.Current.Session[droppableSource] as DataTable;
    }
    public object CreateDraggableTable() {
        var dt = new DataTable();
        dt.Columns.Add("CategoryID",typeof(Int32));
        dt.Columns.Add("CategoryName", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        return dt;
    }
}