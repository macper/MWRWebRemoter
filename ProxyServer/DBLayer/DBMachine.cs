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
using System.Text;
using MWRCommonTypes;

namespace ProxyServer.DBLayer
{
    public class DBMachine : DatabaseObject<Machine, string>
    {
        public DBMachine()
        {
        }

        public DBMachine(DataProvider dp)
            : base(dp)
        {
        }

        public void Load(string guid)
        {
            string strSel = string.Format("SELECT * FROM machines WHERE guid = '{0}'", guid);
            using (DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(strSel, Connection)))
            {
                DataTable res = new DataTable();
                adapter.Fill(res);
                if (res.Rows.Count != 1)
                {
                    businessObject = null;
                    return;
                }
                businessObject = new Machine();
                DataRow current = res.Rows[0];
                businessObject.Name = (string)current["name"];
                businessObject.Description = current["description"] == DBNull.Value ? string.Empty : (string)current["description"];
                businessObject.IP = (string)current["ip"];
                businessObject.Guid = current["guid"].ToString();
                id = current["guid"].ToString();
            }
        }
        public override bool Save(Machine objectToAdd)
        {
            string strComm = string.Format("INSERT INTO machines (guid, name, ip, description) VALUES ('{0}', '{1}', '{2}', '{3}')",
                objectToAdd.Guid, objectToAdd.Name, objectToAdd.IP, objectToAdd.Description);
            return ExecuteNonQuery(strComm);
        }

        public override bool Save()
        {
            string strUpdate = string.Format("UPDATE machines SET name = '{0}', ip = '{1}', description = '{2}' WHERE guid = '{3}'",
                businessObject.Name, businessObject.IP, businessObject.Description, ID);

            return ExecuteNonQuery(strUpdate);
        }

        public override bool Delete()
        {
            string strDel = string.Format("DELETE FROM machines WHERE guid = '{0}'", this.ID);
            businessObject = null;
            return ExecuteNonQuery(strDel);
        }

        public bool Delete(string guid)
        {
            string strDel = string.Format("DELETE FROM machines WHERE guid = '{0}'", guid);
            return ExecuteNonQuery(strDel);
        }

        public bool AddPrivillges(int[] privilleges, int groupID)
        {
            StringBuilder strBld = new StringBuilder();
            if (businessObject == null)
            {
                throw new Exception("Najpierw załaduj obiekt!");
            }
            foreach (int priv in privilleges)
            {
                strBld.AppendFormat("INSERT INTO privilleges(group_id, machine, permission) VALUES ({0},'{1}',{2});", groupID, ID, priv);
            }
            
            int rows = 0;
            using (DbCommand command = dataProvider.GetCommand(strBld.ToString(), Connection))
            {
                rows = command.ExecuteNonQuery();
            }

            return (rows == privilleges.Length);
        }

        public bool ClearPrivilleges(int groupID)
        {
            if (businessObject == null)
            {
                throw new Exception("Najpierw załaduj obiekt!");
            }

            string com = string.Format("DELETE FROM privilleges WHERE group_id = {0} AND machine = '{1}'", groupID, ID);
            DbCommand command = dataProvider.GetCommand(com, Connection);
            return command.ExecuteNonQuery() > 0;
        }

        public int[] GetPrivilleges(string machineGuid, int group)
        {
            string com = string.Format("SELECT permission FROM privilleges WHERE group_id = {0} AND machine = '{1}'", group, machineGuid);
            int[] resPriv = null;
            using (DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(com, Connection)))
            {
                DataTable res = new DataTable();
                adapter.Fill(res);
                if (res.Rows.Count > 0)
                {
                    resPriv = new int[res.Rows.Count];
                    for (int i = 0; i < resPriv.Length; i++)
                    {
                        resPriv[i] = (int)res.Rows[i][0];
                    }
                }
            }
            return resPriv;
        }
    }
}
