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
    public class DBState : DatabaseObject<StateStruct, int>
    {
        public DBState()
        {
        }

        public DBState(DataProvider dp)
            : base(dp)
        {
        }

        public void Load(int id)
        {
            Load_protected(" int = " + id.ToString());
        }

        public void Load(string stateGuid, string MachineGuid)
        {
           DoLoad(string.Format("SELECT TOP 1 date_completed, info_xml, machine, name, active, states.guid, type AS 'guid' FROM states LEFT JOIN dictionary ON states.guid = dictionary.guid WHERE states.guid = '{0}' AND machine = '{1}' ORDER BY date_completed DESC", stateGuid, MachineGuid));           
        }

        protected void DoLoad(string sqlCommand)
        {
            DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(sqlCommand, Connection));
            DataTable res = new DataTable();
            adapter.Fill(res);
            if (res.Rows.Count != 1)
            {
                businessObject = null;
                return;
            }
            businessObject = new StateStruct();
            DataRow current = res.Rows[0];
            businessObject.Guid = current["guid"].ToString();
            businessObject.Active = (byte)current["active"] == 1;
            businessObject.Name = (string)current["name"];
            businessObject.RegisteredDate = (DateTime)current["date_completed"];
            businessObject.XmlInfo = current["info_xml"] == DBNull.Value ? string.Empty : (string)current["info_xml"];
            businessObject.Machine = current["machine"].ToString();
        }

        protected void Load_protected(string where)
        {
            if (where != null)
            {
                DoLoad(GetSelectSQL() + " WHERE " + where);
            } 
        }

        protected string GetSelectSQL()
        {
            return "SELECT date_completed, info_xml, machine, name, active, states.guid, type AS 'guid' FROM states LEFT JOIN dictionary ON states.guid = dictionary.guid";
        }

        public override bool Save(StateStruct objectToAdd)
        {
            string com = string.Format("INSERT INTO states(guid, date_completed, machine, info_xml) VALUES ('{0}', '{3}', '{1}', '{2}')",
                objectToAdd.Guid,  objectToAdd.Machine, objectToAdd.XmlInfo, DateTime.Now.ToString());
            return ExecuteNonQuery(com);
        }

        public override bool Save()
        {
            string com = string.Format("UPDATE states SET guid = '{0}', date_completed = '{1}', machine = {2}, info_xml = '{4}' WHERE id = {5}",
                businessObject.Guid, businessObject.RegisteredDate, businessObject.Machine, businessObject.XmlInfo, ID);
            return ExecuteNonQuery(com);
        }

        public override bool Delete()
        {
            string com = string.Format("DELETE FROM states WHERE id = {0}", ID);
            return ExecuteNonQuery(com);
        }

        public bool Delete(int id)
        {
            string com = string.Format("DELETE FROM states WHERE id = {0}", id);
            return ExecuteNonQuery(com);
        }
    }
}
