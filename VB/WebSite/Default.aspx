<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="ASPxperience_SiteMapControl_BuildSiteMapFromDB_BuildSiteMapFromDB" %>

<%@ Register Assembly="DevExpress.Web.v8.1" Namespace="DevExpress.Web.ASPxSiteMapControl"
	TagPrefix="dxsm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Binding the ASPxSiteMapControl to a database</title>
</head>
<body>
	<form id="form1" runat="server">

		<div>
			<dxsm:ASPxSiteMapControl ID="ASPxSiteMapControl1" runat="server">
			</dxsm:ASPxSiteMapControl>
			<asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/SiteMapData.mdb"
				SelectCommand="SELECT * FROM [SiteMapData]"></asp:AccessDataSource>
		</div>
	</form>
</body>
</html>
