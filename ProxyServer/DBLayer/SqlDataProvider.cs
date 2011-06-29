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
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ProxyServer.DBLayer
{
    public class SqlDataProvider : DataProvider
    {
        public override System.Data.Common.DbConnection GetConnection(string strConnection)
        {
            return new SqlConnection(strConnection);
        }

        public override System.Data.Common.DbCommand GetCommand(string cmdText, System.Data.Common.DbConnection connection)
        {
            return new SqlCommand(cmdText, (SqlConnection)connection);
        }

        public override System.Data.Common.DbDataAdapter GetAdapter(System.Data.Common.DbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        public override long GetLastID(System.Data.Common.DbCommand command)
        {
            SqlCommand sqlCmd = command as SqlCommand;
            sqlCmd.CommandText = "SELECT @@IDENTITY";
            object toCast = sqlCmd.ExecuteScalar();
            if (toCast == DBNull.Value)
            {
                return -1;
            }
            return decimal.ToInt32((decimal)toCast);
        }

        public override string GetLimitSQL(string originalSQL, int limitFrom, int limitTo)
        {
            return string.Empty;
        }
    }
}
