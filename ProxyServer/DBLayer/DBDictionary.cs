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
using System.Data.Common;
using MWRCommonTypes;

namespace ProxyServer.DBLayer
{
    public class DBDictionary : DatabaseObject<DictionaryEntry, string>
    {
        public DBDictionary()
        {
        }

        public DBDictionary(DataProvider dp)
            : base(dp)
        {
        }

        public void Load(string guid)
        {
            string com = string.Format("SELECT * FROM dictionary WHERE guid = '{0}'", guid);
            DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(com, Connection));
            DataTable res = new DataTable();
            adapter.Fill(res);
            if (res.Rows.Count != 1)
            {
                businessObject = null;
                return;
            }
            DataRow current = res.Rows[0];
            businessObject = new DictionaryEntry();
            businessObject.Active = (byte)current["active"] == 1;
            businessObject.Guid = current["guid"].ToString();
            businessObject.Name = (string)current["name"];
            businessObject.Type = (MWRCommonTypes.Enum.ObjectType)(int)current["type"];
            businessObject.Assembly = (string)current["assembly"];
            businessObject.TypeOf = (string)current["typeof"];
            businessObject.Config = current["config"] == DBNull.Value ? "" : (string)current["config"];
            ID = businessObject.Guid.ToString();
        }

        public override bool Save(DictionaryEntry objectToAdd)
        {
            string com = string.Format("INSERT INTO dictionary(guid, name, active, type, config) VALUES ('{0}', '{1}', {2}, {3}, '{4}')",
                objectToAdd.Guid, objectToAdd.Name, objectToAdd.Active == true ? 1 : 0, (int)objectToAdd.Type);
            return ExecuteNonQuery(com);
        }

        public override bool Save()
        {
            string com = string.Format("UPDATE dictionary SET name = '{1}', active = {2}, type = {3}, config = '{4}' WHERE guid = '{5}'",
                businessObject.Guid, businessObject.Name, businessObject.Active == true ? 1 : 0, (int)businessObject.Type, businessObject.Config, ID);
            return ExecuteNonQuery(com);
        }

        public override bool Delete()
        {
            string com = string.Format("DELETE FROM dictionary WHERE guid = '{0}'", ID);
            return ExecuteNonQuery(com);
        }

        public bool Delete(string id)
        {
            ID = id;
            return Delete();
        }
    }
}
