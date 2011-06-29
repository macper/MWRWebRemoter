using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyServer.BusinessLayer;

namespace ProxyServer
{
    public interface IClientInterface
    {
        StateResponse GetStateRequest(ClientAuthStruct auth, string guidStateGuid, DateTime time);
        TaskResponse GetTaskRequest(ClientAuthStruct auth, int TaskID);
        TaskResponse CreateTaskRequest(ClientAuthStruct auth, MWRCommonTypes.TaskStruct task);
        WebResponse DeleteTaskRequest(ClientAuthStruct auth, int TaskID);
        GroupTaskResponse GetTasks(ClientAuthStruct auth, DateTime dateFrom, DateTime dateTo, int[] states, string guid, int startIndex, int rowsCount);
        DictionaryResponse GetDictionaryEntries(ClientAuthStruct auth, int type, int state);
        UserDataResponse GetUserData(ClientAuthStruct auth);
        WebResponse UpdateDictionary(ClientAuthStruct auth, string Guid, int newState, string Config);
    }
}
