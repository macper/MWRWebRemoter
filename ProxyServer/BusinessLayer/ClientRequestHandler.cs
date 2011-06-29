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
using System.Web.Configuration;
using MWRCommonTypes;
using ProxyServer.DBLayer;
using System.Collections.Generic;

namespace ProxyServer.BusinessLayer
{
    public class ClientRequestHandler : BaseRequestHandler
    {
        public bool Authorize(ClientAuthStruct auth, IMWRObject request)
        {
            DBUser dbUser = new DBUser(dataProvider);
            dbUser.Connection = connection;
            dbUser.Load(auth.UserName, auth.Password);
            if (dbUser.BusinessObject == null)
            {
                return false;
            }
            return true;
        }

        public StateStruct GetState(string stateGuid, string MachineGuid)
        {
            DBState dbState = new DBState(dataProvider);
            dbState.Connection = connection;

            dbState.Load(stateGuid, MachineGuid);
            return dbState.BusinessObject;
        }

        public TaskStruct GetTask(int TaskID)
        {
            DBTask dbTask = new DBTask(dataProvider);
            dbTask.Connection = connection;
            dbTask.Load(TaskID);
            return dbTask.BusinessObject;
        }

        public bool CreateTask(TaskStruct taskStruct, ref int newTaskID)
        {
            DBTask dbTask = new DBTask(dataProvider);
            dbTask.Connection = connection;

            if (dbTask.Save(taskStruct))
            {
                newTaskID = (int)dbTask.LastID;
                return true;
            }
            return false;
        }

        public bool DeleteTask(int TaskID)
        {
            DBTask dbTask = new DBTask(dataProvider);
            dbTask.Connection = connection;
            return dbTask.Delete(TaskID);
        }

        public TaskStruct[] GetTasks(string machineGuid, DateTime from, DateTime to, int[] states, string taskGuid, int startIndex, int rowsCount, out int totalRowsCount)
        {
            DBTaskCollection dbTask = new DBTaskCollection(dataProvider);
            dbTask.Connection = connection;
            dbTask.Load(machineGuid, from, to, states, taskGuid, startIndex, rowsCount, out totalRowsCount);
            if (dbTask.List != null)
            {
                TaskStruct[] ret = new TaskStruct[dbTask.List.Count];
                for (int i = 0; i < ret.Length; i++)
                {
                    ret[i] = dbTask.List[i].BusinessObject;
                }
                return ret;
            }
            return null;
        }

        public DictionaryEntry[] GetDictionaryEntries(int type, int state)
        {
            DBDictionaryCollection dbDictionary = new DBDictionaryCollection(dataProvider);
            dbDictionary.Connection = connection;
            List<DictionaryEntry> list = dbDictionary.Load(type, state);
            if (list.Count > 0)
            {
                return list.ToArray();
            }
            return null;
        }

        public void GetUserData(string userName, string password, out MachineWithPrivilleges [] machines, out User user)
        {
            DBUser dbUser = new DBUser(dataProvider);
            dbUser.Connection = connection;
            dbUser.Load(userName, password);
            user = dbUser.BusinessObject;

            DBMachineToPrivCollection dbMachineCol = new DBMachineToPrivCollection(dataProvider);
            dbMachineCol.Connection = connection;
            machines = dbMachineCol.Load(dbUser.BusinessObject.Group);
        }

        public bool UpdateDictionary(string Guid, int newState, string newConfig)
        {
            DBDictionary dbDictionary = new DBDictionary(dataProvider);
            dbDictionary.Connection = connection;
            dbDictionary.Load(Guid);
            dbDictionary.BusinessObject.Active = newState == 1;
            dbDictionary.BusinessObject.Config = newConfig;
            return dbDictionary.Save();
        }

        #region IDisposable Members

        public override void Dispose()
        {
            connection.Dispose();
        }

        #endregion
    }
}
