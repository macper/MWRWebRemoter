using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace ProxyServer.DBLayer
{
    public class MySQLDataProvider : DataProvider
    {
        public override System.Data.Common.DbConnection GetConnection(string connString)
        {
            return new MySqlConnection(connString);
        }

        public override System.Data.Common.DbCommand GetCommand(string cmdText, System.Data.Common.DbConnection connection)
        {
            return new MySqlCommand(cmdText, (MySqlConnection)connection);
        }

        public override System.Data.Common.DbDataAdapter GetAdapter(DbCommand command)
        {
            return new MySqlDataAdapter((MySqlCommand)command);
        }

        public override long GetLastID(DbCommand command)
        {
            return ((MySqlCommand)command).LastInsertedId;
        }

        public override string GetLimitSQL(string originalSQL, int limitFrom, int limitTo)
        {
            return string.Format("{0} LIMIT {1},{2}", originalSQL, limitFrom.ToString(), limitTo.ToString());
        }
    }
}
