using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxSiteMapControl;
using System.Collections.Specialized;

public partial class ASPxperience_SiteMapControl_BuildSiteMapFromDB_BuildSiteMapFromDB : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        SiteMapDataSource dataSource = GenerateSiteMapHierarchy(AccessDataSource1);
        dataSource.ShowStartingNode = false;

        ASPxSiteMapControl1.DataSource = dataSource;
        ASPxSiteMapControl1.DataBind();
    }
    protected SiteMapDataSource GenerateSiteMapHierarchy(SqlDataSource dataSource) {
        SiteMapDataSource ret = new SiteMapDataSource();

        DataSourceSelectArguments arg = new DataSourceSelectArguments("ParentID");
        DataView dataView = dataSource.Select(arg) as DataView;
        DataTable table = dataView.Table;
        DataRow rowRootNode = table.Select("ParentID = 0")[0];

        UnboundSiteMapProvider provider = new UnboundSiteMapProvider(rowRootNode["NavigateURL"].ToString(), rowRootNode["Text"].ToString());
        AddNodeToProviderRecursive(provider.RootNode, rowRootNode["ID"].ToString(), table, provider);
        ret.Provider = provider;
        return ret;
    }

    private void AddNodeToProviderRecursive(SiteMapNode parentNode, string parentID,
        DataTable table, UnboundSiteMapProvider provider) {

        DataRow[] childRows = table.Select("ParentID = " + parentID);
        foreach (DataRow row in childRows) {
            SiteMapNode childNode = CreateSiteMapNode(row, provider);
            provider.AddSiteMapNode(childNode, parentNode);
            AddNodeToProviderRecursive(childNode, row["ID"].ToString(), table, provider);
        }
    }
    private SiteMapNode CreateSiteMapNode(DataRow dataRow, UnboundSiteMapProvider provider) {
        return provider.CreateNode(dataRow["NavigateURL"].ToString(), dataRow["Text"].ToString(), "", null, new NameValueCollection());
    }
}
