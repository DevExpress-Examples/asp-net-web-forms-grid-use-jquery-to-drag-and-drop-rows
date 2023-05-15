Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private helper As New Helper()
	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		If Not IsPostBack Then
			Session.Clear()
		End If

		helper.BindGrids(GridFrom, GridTo)

	End Sub
	Protected Sub cbPanel_Callback(ByVal sender As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase)
		Dim rowKey = e.Parameter.Split("|"c)(0)
		Dim leftToRight = Convert.ToBoolean(e.Parameter.Split("|"c)(1))

		Dim source = If(leftToRight, helper.GetDraggableDataSource(), helper.GetDroppableDataSource())
		Dim target = If(leftToRight, helper.GetDroppableDataSource(), helper.GetDraggableDataSource())

		'update target datasource
		Dim sourceRow = source.AsEnumerable().Where(Function(x) x.Field(Of Integer)("CategoryID") = Convert.ToInt32(rowKey)).SingleOrDefault()
		target.ImportRow(sourceRow)

		'remove source data
		source.Rows.Remove(sourceRow)

		GridFrom.DataBind()
		GridTo.DataBind()
	End Sub
End Class