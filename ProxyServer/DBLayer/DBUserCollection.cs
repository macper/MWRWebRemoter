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
using MWRCommonTypes;

namespace ProxyServer.DBLayer
{
    public class DBUserCollection : DatabaseCollectionObject<User, int>
    {
        public DBUserCollection(DataProvider dp)
            : base(dp)
        {
        }

        public override void Load()
        {
            string sqlComm = "SELECT * FROM users";
            using (DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(sqlComm, Connection)))
            {
                DataTable res = new DataTable();
                adapter.Fill(res);
                innerList = new System.Collections.Generic.List<DatabaseObject<User, int>>(res.Rows.Count);
                if (res.Rows.Count > 0)
                {
                    foreach (DataRow row in res.Rows)
                    {
                        User user = new User();
                        user.Name = (string)row["name"];
                        user.Password = (string)row["password"];
                        user.Group = (int)row["group_id"];
                        user.RegisterDate = (DateTime)row["registered"];
                        DBUser dbUser = new DBUser();
                        dbUser.BusinessObject = user;
                        dbUser.ID = (int)row["id"];
                        user.ID = dbUser.ID;
                        innerList.Add(dbUser);
                    }
                }
            }
        }
    }
}
