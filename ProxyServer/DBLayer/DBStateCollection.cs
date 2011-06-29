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
using System.Data.Common;

namespace ProxyServer.DBLayer
{
    public class DBStateCollection : DatabaseCollectionObject<StateStruct, int>
    {
        public DBStateCollection(DataProvider dp)
            : base(dp)
        {
        }

        public override void Load()
        {
            string com = "SELECT date_completed, info_xml, machine, user, name, active, states.guid, type AS 'guid' FROM states LEFT JOIN dictionary ON states.guid = dictionary.guid";
            DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(com, Connection));
            DataTable res = new DataTable();
            adapter.Fill(res);
            if (res.Rows.Count > 0)
            {
                innerList = new System.Collections.Generic.List<DatabaseObject<StateStruct, int>>(res.Rows.Count);
                foreach (DataRow dr in res.Rows)
                {
                    DBState dbState = new DBState();
                    DataRow current = dr;
                    StateStruct businessObject = dbState.BusinessObject;
                    businessObject = new StateStruct();

                    businessObject.Guid = (string)current["guid"];
                    businessObject.Active = (int)current["active"] == 1;
                    businessObject.Name = (string)current["name"];
                    businessObject.RegisteredDate = (DateTime)current["date_completed"];
                    businessObject.XmlInfo = (string)current["info_xml"];
                    businessObject.Machine = (string)current["machine"];

                    innerList.Add(dbState);
                }
            }
        }
    }
}
