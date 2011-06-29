using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyServer.BusinessLayer;

namespace ProxyServer
{
    public interface IServerInterface
    {
        WebResponse UpdateStateRequest(ServerAuthStruct auth, string stateGuid, string informationXml);
        WebResponse UpdateTaskRequest(ServerAuthStruct auth, int taskID, int newState, string xmlResponse, string errorInfo);
        GroupTaskResponse GetTasks(ServerAuthStruct auth);
        DictionaryResponse GetActiveStates(ServerAuthStruct auth);
        DictionaryResponse GetActiveTasks(ServerAuthStruct auth);
    }
}
