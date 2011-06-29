using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ProxyServer.DBLayer
{
    public abstract class DataProvider
    {
        public abstract DbConnection GetConnection(string strConnection);
        public abstract DbCommand GetCommand(string cmdText, DbConnection connection);
        public abstract DbDataAdapter GetAdapter(DbCommand command);
        public abstract long GetLastID(DbCommand command);
        public abstract string GetLimitSQL(string originalSQL, int limitFrom, int limitTo);
    }
}
