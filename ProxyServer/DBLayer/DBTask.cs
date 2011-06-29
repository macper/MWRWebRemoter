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
    public class DBTask : DatabaseObject<TaskStruct, int>
    {
        public DBTask()
        {
        }

        public DBTask(DataProvider dp)
            : base(dp)
        {
        }

        public void Load(int id)
        {
            string com = "SELECT * FROM tasks LEFT JOIN dictionary ON tasks.guid = dictionary.guid WHERE id = " + id.ToString();
            DbDataAdapter adapter = dataProvider.GetAdapter(dataProvider.GetCommand(com, Connection));
            DataTable res = new DataTable();
            adapter.Fill(res);
            if (res.Rows.Count != 1)
            {
                businessObject = null;
                return;
            }
            DataRow dr = res.Rows[0];
            businessObject = new TaskStruct();
            businessObject.DateCompleted = DBBaseObject.GetSQLDate(dr["date_completed"]);
            businessObject.DateRegistered = DBBaseObject.GetSQLDate(dr["date_registered"]);
            businessObject.DateSended = DBBaseObject.GetSQLDate(dr["date_sended"]);
            businessObject.DateToExecute = DBBaseObject.GetSQLDate(dr["date_to_execute"]);
            businessObject.Guid = dr["guid"].ToString();
            businessObject.State = (int)dr["state"];
            businessObject.XmlRequest = (string)dr["xml_request"];
            businessObject.XmlResponse = (string)dr["xml_response"];
            businessObject.Name = (string)dr["name"];
            businessObject.Active = (bool)((byte)dr["active"] == 1);
            businessObject.ID = id;
            businessObject.User = (int)dr["user"];
            businessObject.Machine = dr["machine"].ToString();
				businessObject.ErrorDetails = dr["error_description"] != DBNull.Value ? (string)dr["error_description"] : null;
            ID = id;
        }
        public override bool Save(TaskStruct objectToAdd)
        {
            string com = string.Format("INSERT INTO tasks(guid, [user], machine, state, xml_request, xml_response, date_registered, date_sended, date_completed, date_to_execute) VALUES ('{0}', {1}, '{2}', {3}, '{4}', '{5}', '{6}', {7}, {8}, {9})",
                objectToAdd.Guid, objectToAdd.User, objectToAdd.Machine, (int)objectToAdd.State, objectToAdd.XmlRequest, objectToAdd.XmlResponse, DateTime.Now.ToString(), DBBaseObject.FormatSQLDate(objectToAdd.DateSended), DBBaseObject.FormatSQLDate(objectToAdd.DateCompleted), DBBaseObject.FormatSQLDate(objectToAdd.DateToExecute));
            return ExecuteNonQuery(com);
        }

        public override bool Save()
        {
            string com = string.Format("UPDATE tasks SET guid = '{0}', [user] = {1}, machine = '{2}', state = {3}, xml_request = '{4}', xml_response = '{5}', date_registered = {6}, date_sended = {7}, date_completed = {8}, date_to_execute = {9}, error_description = '{11}' WHERE id = {10}",
                businessObject.Guid, businessObject.User, businessObject.Machine, (int)businessObject.State, businessObject.XmlRequest, businessObject.XmlResponse, DBBaseObject.FormatSQLDate(businessObject.DateRegistered), DBBaseObject.FormatSQLDate(businessObject.DateSended), DBBaseObject.FormatSQLDate(businessObject.DateCompleted), DBBaseObject.FormatSQLDate(businessObject.DateToExecute), businessObject.ID, businessObject.ErrorDetails);
            return ExecuteNonQuery(com);
        }

        public override bool Delete()
        {
            string com = "DELETE FROM tasks WHERE id = " + ID.ToString();
            return ExecuteNonQuery(com);
        }

        public bool Delete(int id)
        {
            ID = id;
            return Delete();
        }
    }
}
