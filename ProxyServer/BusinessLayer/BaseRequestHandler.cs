using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Data.Common;
using ProxyServer.DBLayer;

namespace ProxyServer.BusinessLayer
{
    public abstract class BaseRequestHandler : IDisposable
    {
        protected DbConnection connection;
        protected DataProvider dataProvider;

        public BaseRequestHandler()
        {
            ConnectionStringSettingsCollection connStrings = WebConfigurationManager.ConnectionStrings;
            ConnectionStringSettings cs = connStrings[WebConfigurationManager.AppSettings["CurrentDBProvider"]];

            if (cs.ProviderName == "System.Data.SqlClient")
            {
                connection = new System.Data.SqlClient.SqlConnection(cs.ConnectionString);
                dataProvider = new SqlDataProvider();
            }

            connection.Open();
        }

        public abstract void Dispose();
    }
}
