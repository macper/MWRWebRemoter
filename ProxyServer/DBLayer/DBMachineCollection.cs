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
    public class DBMachineCollection : DatabaseCollectionObject<Machine, string>
    {
        DBMachineCollection(DataProvider dp)
            : base(dp)
        {
        }

        public override void Load()
        {
            string com = "SELECT * FROM machines";
            using (DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(com, Connection)))
            {
                DataTable res = new DataTable();
                adapter.Fill(res);
                innerList = new List<DatabaseObject<Machine, string>>(res.Rows.Count);
                foreach (DataRow dr in res.Rows)
                {
                    DBMachine dbMachine = new DBMachine();
                    dbMachine.BusinessObject = new Machine();
                    dbMachine.BusinessObject.Guid = (string)dr["guid"];
                    dbMachine.BusinessObject.Name = (string)dr["name"];
                    dbMachine.BusinessObject.Description = (string)dr["description"];
                    dbMachine.BusinessObject.IP = (string)dr["ip"];
                    dbMachine.ID = (string)dr["guid"];
                    innerList.Add(dbMachine);
                }
            }
        }
    }
}
