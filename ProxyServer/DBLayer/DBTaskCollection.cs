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
using System.Text;
using System.Data.SqlClient;

namespace ProxyServer.DBLayer
{
    public class DBTaskCollection : DatabaseCollectionObject<TaskStruct, int>
    {
        public DBTaskCollection(DataProvider dp)
            : base(dp)
        {
        }

        public override void Load()
        {
            Load(null);   
        }

        public void LoadActiveAndToExecute(string machineGuid)
        {
            Load(string.Format(" state = {0} AND machine = '{1}' AND date_to_execute <= '{2}'", 1, machineGuid, DateTime.Now));
        }

        public void Load(MWRCommonTypes.Enum.TaskState state, string machineGuid)
        {
            Load(string.Format(" state = {0} AND machine = '{1}'", (int)state, machineGuid));
        }

        public void Load(string machineGuid, DateTime dateFrom, DateTime dateTo, int[] states, string guid, int startIndex, int rowsCount, out int totalRowsCount)
        {
            StringBuilder strBld = new StringBuilder("WITH Results AS (SELECT ROW_NUMBER() OVER (ORDER BY date_registered DESC) AS Row, id, tasks.[guid], [state], [name] FROM tasks LEFT JOIN dictionary ON tasks.guid = dictionary.guid");

			StringBuilder strBldWhere = new StringBuilder(" WHERE ");
			strBldWhere.AppendFormat(" machine = '{0}' ", machineGuid);
            if (dateFrom != DateTime.MinValue)
            {
				strBldWhere.AppendFormat(" AND date_registered >= '{0}'", dateFrom.ToString());
            }
            if (dateTo != DateTime.MinValue)
            {
				strBldWhere.AppendFormat(" AND date_registered <= '{0}'", dateTo.ToString());
            }
            if (states.Length != 0)
            {
				strBldWhere.Append(" AND state IN (");
                foreach (int state in states)
                {
                    strBldWhere.Append(state.ToString());
                    strBldWhere.Append(",");
                }
				strBldWhere.Remove(strBldWhere.Length - 1, 1);
				strBldWhere.Append(")");
            }
            if (guid != null && guid != string.Empty && guid != "0")
            {
				strBldWhere.AppendFormat(" AND tasks.guid = '{0}'", guid); 
            }

			string strTotalRowsCount = "SELECT COUNT(*) FROM tasks " + strBldWhere.ToString();

			using (SqlCommand cmd = new SqlCommand(strTotalRowsCount, (SqlConnection)connection))
			{
				object res = cmd.ExecuteScalar();
				if (res != null && res != DBNull.Value)
				{
					totalRowsCount = (int)res;
				}
				else
				{
					totalRowsCount = 0;
				}
			}

			strBld.Append(strBldWhere.ToString());
			strBld.Append(")");

			strBld.AppendFormat("SELECT * FROM Results WHERE Row >= {0} AND Row <{1}", startIndex, startIndex + rowsCount);
			LoadInner(strBld.ToString(), true);
        }

        protected void Load(string where)
        {
            string com = "SELECT * FROM tasks LEFT JOIN dictionary ON tasks.guid = dictionary.guid ";
            if (where != null && where != string.Empty)
            {
                com += " WHERE " + where;
            }
				LoadInner(com, false);
        }

		protected void LoadInner(string sql, bool forClient)
		{
			DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(sql, Connection));
			DataTable res = new DataTable();
			adapter.Fill(res);

			if (res.Rows.Count > 0)
			{
				innerList = new System.Collections.Generic.List<DatabaseObject<TaskStruct, int>>(res.Rows.Count);
				foreach (DataRow dr in res.Rows)
				{
					DBTask dbTask = new DBTask(dataProvider);
					TaskStruct businessObject = new TaskStruct();
					businessObject.ID = (int)dr["id"];
					businessObject.Name = (string)dr["name"];
					businessObject.State = (int)dr["state"];
					businessObject.Guid = dr["guid"].ToString();

					if (!forClient)
					{
						businessObject.DateCompleted = DBBaseObject.GetSQLDate(dr["date_completed"]);
						businessObject.DateRegistered = DBBaseObject.GetSQLDate(dr["date_registered"]);
						businessObject.DateSended = DBBaseObject.GetSQLDate(dr["date_sended"]);
						businessObject.DateToExecute = DBBaseObject.GetSQLDate(dr["date_to_execute"]);
						businessObject.XmlRequest = (string)dr["xml_request"];
						businessObject.XmlResponse = (string)dr["xml_response"];
						businessObject.Active = (bool)((byte)dr["active"] == 1);
						businessObject.User = (int)dr["user"];
						businessObject.Machine = dr["machine"].ToString();
						businessObject.ErrorDetails = dr["error_description"] != DBNull.Value ? (string)dr["error_description"] : null;
					}
					
					dbTask.BusinessObject = businessObject;

					innerList.Add(dbTask);
				}
			}
		}
    }
}
