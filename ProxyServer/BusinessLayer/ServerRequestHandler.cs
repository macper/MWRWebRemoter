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
using ProxyServer.DBLayer;
using System.Web.Configuration;
using System.Collections.Generic;

namespace ProxyServer.BusinessLayer
{
    public class ServerRequestHandler : BaseRequestHandler
    {
        public bool Authorize(ServerAuthStruct auth)
        {
            DBLayer.DBMachine machine = new ProxyServer.DBLayer.DBMachine(dataProvider);
            machine.Connection = connection;
            machine.Load(auth.MachineGuid);
            if (machine.BusinessObject != null)
            {
                return machine.BusinessObject.Name == auth.AuthToken;
            }
            return false;
        }

        public bool UpdateState(string stateGuid, string xmlInfo, string machine)
        {
            DBState dbState = new DBState(dataProvider);
            dbState.Connection = connection;
            StateStruct state = new StateStruct();
            state.Guid = stateGuid;
            state.XmlInfo = xmlInfo;
            state.Machine = machine;
            return dbState.Save(state);
        }

        public bool MWRStateExists(string guid, ref bool active)
        {
            DBDictionary dbDictionary = new DBDictionary(dataProvider);
            dbDictionary.Connection = connection;
            dbDictionary.Load(guid);
            if (dbDictionary.BusinessObject != null)
            {
                active = dbDictionary.BusinessObject.Active;
                return true;
            }
            return false;
        }

        public TaskStruct GetTask(int id)
        {
            DBTask dbTask = new DBTask(dataProvider);
            dbTask.Connection = connection;
            dbTask.Load(id);
            return dbTask.BusinessObject;
        }

        public bool UpdateTask(TaskStruct task)
        {
            DBTask dbTask = new DBTask(dataProvider);
            dbTask.Connection = connection;
            dbTask.BusinessObject = task;
            dbTask.ID = task.ID;
            return dbTask.Save();
        }

        public TaskStruct[] GetTasks(string machineGuid)
        {
            DBTaskCollection dbTasks = new DBTaskCollection(dataProvider);
            dbTasks.Connection = connection;
            dbTasks.LoadActiveAndToExecute(machineGuid);
            if (dbTasks.List != null && dbTasks.List.Count > 0)
            {
                TaskStruct[] ret = new TaskStruct[dbTasks.List.Count];
                for (int i = 0; i < ret.Length; i++)
                {
                    dbTasks.List[i].Connection = connection;
                    dbTasks.List[i].BusinessObject.DateSended = DateTime.Now;
                    dbTasks.List[i].BusinessObject.State = (int)MWRCommonTypes.Enum.TaskState.Sended;
                    dbTasks.List[i].Save();
                    ret[i] = dbTasks.List[i].BusinessObject;
                }
                return ret;
            }
            return null;
        }

        public List<DictionaryEntry> GetActiveStates()
        {
            DBDictionaryCollection dbDictionary = new DBDictionaryCollection(dataProvider);
            dbDictionary.Connection = connection;
            return dbDictionary.LoadActiveStates();
        }

        public List<DictionaryEntry> GetActiveTasks()
        {
            DBDictionaryCollection dbDictionary = new DBDictionaryCollection(dataProvider);
            dbDictionary.Connection = connection;
            return dbDictionary.LoadActiveTasks();
        }

        #region IDisposable Members

        public override void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }

        #endregion
    }
}
