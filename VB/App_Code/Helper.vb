Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.SessionState
Imports DevExpress.Web.ASPxGridView

''' <summary>
''' Summary description for Helper
''' </summary>
Public Class Helper
    Private Const droppableSource As String = "droppableDS"
    Private Const draggableSource As String = "draggableDS"


    Public Sub BindGrids(ByVal grid1 As ASPxGridView, ByVal grid2 As ASPxGridView)


        grid1.DataSource = GetDraggableDataSource()
        grid1.DataBind()

        grid2.DataSource = GetDroppableDataSource()
        grid2.DataBind()
    End Sub
    Public Function GetDraggableDataSource() As DataTable

        If HttpContext.Current.Session(draggableSource) Is Nothing Then
            Dim command = "SELECT CategoryID, CategoryName, Description FROM Categories"
            Dim connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & HttpContext.Current.Server.MapPath("~/App_Data/Categories + products.mdb")
            Dim adapter = New System.Data.OleDb.OleDbDataAdapter(command, connectionString)
            Dim dt = New DataTable()
            adapter.Fill(dt)
            HttpContext.Current.Session(draggableSource) = dt
        End If
        Return TryCast(HttpContext.Current.Session(draggableSource), DataTable)
    End Function
    Public Function GetDroppableDataSource() As DataTable
        If HttpContext.Current.Session(droppableSource) Is Nothing Then
            HttpContext.Current.Session(droppableSource) = CreateDraggableTable()
        End If
        Return TryCast(HttpContext.Current.Session(droppableSource), DataTable)
    End Function
    Public Function CreateDraggableTable() As Object
        Dim dt = New DataTable()
        dt.Columns.Add("CategoryID",GetType(Int32))
        dt.Columns.Add("CategoryName", GetType(String))
        dt.Columns.Add("Description", GetType(String))
        Return dt
    End Function
End Class