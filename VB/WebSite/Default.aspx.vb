Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView
Imports System.Collections

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private ds As DataSet

	Protected Sub Page_Init()
		If (Not IsPostBack) Then
			'setup empty dataset
			If (Not IsCallback) Then
				ds = New DataSet()
				Dim tb As New DataTable()
				tb.Columns.Add("ID", GetType(Integer))
				tb.Columns.Add("ItemCode", GetType(String))
				tb.Columns.Add("Qty", GetType(Integer))
				tb.PrimaryKey = New DataColumn() { tb.Columns("ID") }
				ds.Tables.Add(tb)
				Session("DataSet") = ds
			End If
		Else
			If IsCallback Then
				ds = CType(Session("DataSet"), DataSet)
				ASPxGridView1.DataSource = ds.Tables(0)
				ASPxGridView1.DataBind()
			End If
		End If
	End Sub

	Protected Sub ASPxGridView1_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
		ds = CType(Session("DataSet"), DataSet)
		Dim gridView As ASPxGridView = CType(sender, ASPxGridView)
		Dim dataTable As DataTable = ds.Tables(0)
		Dim row As DataRow = dataTable.NewRow()
		e.NewValues("ID") = dataTable.Rows.Count
		Dim enumerator As IDictionaryEnumerator = e.NewValues.GetEnumerator()
		enumerator.Reset()
		Do While enumerator.MoveNext()
			If enumerator.Key.ToString() <> "Count" Then
				row(enumerator.Key.ToString()) = enumerator.Value
			End If
		Loop
		gridView.CancelEdit()
		e.Cancel = True
		dataTable.Rows.Add(row)
	End Sub
End Class
