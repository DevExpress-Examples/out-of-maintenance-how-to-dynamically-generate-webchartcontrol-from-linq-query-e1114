Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Collections


Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		If (Not IsPostBack) Then
			Dim dataContext As New NorthwindDataContext()

			 'productsQuery will have a IQueryable<Category> type
			Dim productsQuery = From category In dataContext.Categories _
			                    Where category.CategoryID = 1 _
			                    Join product In dataContext.Products On category Equals product.Category _
			                    Select New With {Key .Category = category.CategoryName, Key .Product = product.ProductName, Key .Price = product.UnitPrice}

			Dim series As New DevExpress.XtraCharts.Series("Series1", DevExpress.XtraCharts.ViewType.RadarArea)

			series.DataSource = productsQuery
			series.ArgumentDataMember = "Product"
			series.ValueDataMembers.AddRange(New String() { "Price" })

			WebChartControl1.Series.Add(series)
			WebChartControl1.DataBind()
			CType(WebChartControl1.Diagram, DevExpress.XtraCharts.RadarDiagram).AxisY.Range.MaxValue= 50
		End If

	End Sub

End Class
