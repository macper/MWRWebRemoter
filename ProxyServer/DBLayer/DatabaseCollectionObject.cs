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
using MWRCommonTypes;
using System.Collections.Generic;
using System.Data.Common;

namespace ProxyServer.DBLayer
{
    public abstract class DatabaseCollectionObject<T, K> : DBBaseObject where T: BusinessObject
    {
        protected List<DatabaseObject<T, K>> innerList;
        public List<DatabaseObject<T, K>> List
        {
            get { return innerList; }
        }

        public DatabaseCollectionObject(DataProvider dp) : base(dp)
        {
        }

        public DatabaseCollectionObject(DataProvider dp, DbConnection connection) : base(dp)
        {
            Connection = connection;
        }

        public abstract void Load();
    }
}
