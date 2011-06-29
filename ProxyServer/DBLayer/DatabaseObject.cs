using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MWRCommonTypes;

namespace ProxyServer.DBLayer
{
    public abstract class DatabaseObject<T, K> : DBBaseObject where T:BusinessObject 
    {
        protected T businessObject;
        public T BusinessObject
        {
            get { return businessObject; }
            set { businessObject = value; }
        }

        protected K id;
        public K ID
        {
            get { return id; }
            set { id = value; }
        }

        protected long lastID;
        public long LastID
        {
            get { return lastID; }
        }

        public DatabaseObject()
        {
        }

        public DatabaseObject(DataProvider dp) : base(dp)
        {
        }

        public DatabaseObject(DataProvider dp, DbConnection connection) : base(dp)
        {
            dataProvider = dp;
            Connection = connection;
        }

        /// <summary>
        /// Dodaje nowy obiekt biznesowy do bazy. Zwraca ID nowo dodanego rekordu
        /// </summary>
        /// <param name="objectToAdd"></param>
        /// <returns></returns>
        public abstract bool Save(T objectToAdd);
        /// <summary>
        /// Updatuje bieżący obiekt biznesowy
        /// </summary>
        public abstract bool Save();
        /// <summary>
        /// Usuwa bieżący obiekt biznesowy
        /// </summary>
        public abstract bool Delete();

        protected bool ExecuteNonQuery(string strCommand)
        {
            using (DbCommand command = dataProvider.GetCommand(strCommand, Connection))
            {
                bool res = command.ExecuteNonQuery() == 1;
                lastID = dataProvider.GetLastID(command);
                return res;
            }
        }


   }
}
