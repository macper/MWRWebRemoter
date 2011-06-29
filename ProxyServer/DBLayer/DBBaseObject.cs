using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ProxyServer.DBLayer
{
    public class DBBaseObject : IDisposable
    {
        protected DataProvider dataProvider;

        protected DbConnection connection;
        public DbConnection Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

        public DBBaseObject()
        {
        }

        public DBBaseObject(DataProvider dataProv)
        {
            dataProvider = dataProv;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }

        #endregion

        public static string FormatSQLDate(DateTime date)
        {
            if (date <= DateTime.MinValue || date >= DateTime.MaxValue)
            {
                return "NULL";
            }
            return string.Format("'{0}'", date.ToString());
        }

        public static DateTime GetSQLDate(object obj)
        {
            if (obj == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            return (DateTime)obj;
        }
    }
}
