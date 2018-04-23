Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxSiteMapControl
Imports System.Collections.Specialized

Partial Public Class ASPxperience_SiteMapControl_BuildSiteMapFromDB_BuildSiteMapFromDB
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim dataSource As SiteMapDataSource = GenerateSiteMapHierarchy(AccessDataSource1)
		dataSource.ShowStartingNode = False

		ASPxSiteMapControl1.DataSource = dataSource
		ASPxSiteMapControl1.DataBind()
	End Sub
	Protected Function GenerateSiteMapHierarchy(ByVal dataSource As SqlDataSource) As SiteMapDataSource
		Dim ret As SiteMapDataSource = New SiteMapDataSource()

		Dim arg As DataSourceSelectArguments = New DataSourceSelectArguments("ParentID")
		Dim dataView As DataView = TryCast(dataSource.Select(arg), DataView)
		Dim table As DataTable = dataView.Table
		Dim rowRootNode As DataRow = table.Select("ParentID = 0")(0)

		Dim provider As UnboundSiteMapProvider = New UnboundSiteMapProvider(rowRootNode("NavigateURL").ToString(), rowRootNode("Text").ToString())
		AddNodeToProviderRecursive(provider.RootNode, rowRootNode("ID").ToString(), table, provider)
		ret.Provider = provider
		Return ret
	End Function

	Private Sub AddNodeToProviderRecursive(ByVal parentNode As SiteMapNode, ByVal parentID As String, ByVal table As DataTable, ByVal provider As UnboundSiteMapProvider)

		Dim childRows As DataRow() = table.Select("ParentID = " & parentID)
		For Each row As DataRow In childRows
			Dim childNode As SiteMapNode = CreateSiteMapNode(row, provider)
			provider.AddSiteMapNode(childNode, parentNode)
			AddNodeToProviderRecursive(childNode, row("ID").ToString(), table, provider)
		Next row
	End Sub
	Private Function CreateSiteMapNode(ByVal dataRow As DataRow, ByVal provider As UnboundSiteMapProvider) As SiteMapNode
		Return provider.CreateNode(dataRow("NavigateURL").ToString(), dataRow("Text").ToString(), "", Nothing, New NameValueCollection())
	End Function
End Class
