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
using System.Collections.Generic;

namespace ProxyServer.DBLayer
{
    public class DBDictionaryCollection : DatabaseCollectionObject<DictionaryEntry, string>
    {
        public DBDictionaryCollection(DataProvider dp)
            : base(dp)
        {
        }

        public override void Load()
        {
            string com = "SELECT * FROM dictionary";
            LoadProtected(com);
        }

        public List<DictionaryEntry> LoadActiveStates()
        {
            string com = "SELECT * FROM dictionary WHERE active = 1 AND type = 0";
            LoadProtected(com);
            List<DictionaryEntry> list = new List<DictionaryEntry>();
            foreach (DBDictionary dbObj in List)
            {
                list.Add(dbObj.BusinessObject);
            }
            return list;
        }

        public List<DictionaryEntry> LoadActiveTasks()
        {
            string com = "SELECT * FROM dictionary WHERE active = 1 AND type = 1";
            LoadProtected(com);
            List<DictionaryEntry> list = new List<DictionaryEntry>();
				if (List != null)
				{
					foreach (DBDictionary dbObj in List)
					{
						list.Add(dbObj.BusinessObject);
					}
				}
            return list;
        }

        public List<DictionaryEntry> Load(int type, int status)
        {
            string com = "SELECT * FROM dictionary WHERE type ";
            if (type == -1)
            {
                com += " >= 0";
            }
            else
            {
                com += " = " + type.ToString();

            }
            if (status != -1)
            {
                com += " AND active = " + status.ToString();
            }
            LoadProtected(com);
            List<DictionaryEntry> list = new List<DictionaryEntry>();
            foreach (DBDictionary dbObj in List)
            {
                list.Add(dbObj.BusinessObject);
            }
            return list;
        }

        protected void LoadProtected(string command)
        {
            DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(command, Connection));
            DataTable res = new DataTable();
            adapter.Fill(res);
            if (res.Rows.Count > 0)
            {
                innerList = new System.Collections.Generic.List<DatabaseObject<DictionaryEntry, string>>(res.Rows.Count);

                foreach (DataRow current in res.Rows)
                {
                    DBDictionary dbDictionary = new DBDictionary();
                    DictionaryEntry businessObject = new DictionaryEntry();
                    businessObject.Active = (byte)current["active"] == 1;
                    businessObject.Guid = current["guid"].ToString();
                    businessObject.Name = (string)current["name"];
                    businessObject.Type = (MWRCommonTypes.Enum.ObjectType)(int)current["type"];
                    businessObject.Assembly = (string)current["assembly"];
                    businessObject.TypeOf = (string)current["typeof"];
                    if (current["config"] != DBNull.Value)
                    businessObject.Config = (string)current["config"];
                    dbDictionary.ID = businessObject.Guid;
                    dbDictionary.BusinessObject = businessObject;
                    innerList.Add(dbDictionary);
                }
            }
        }
    }
}
