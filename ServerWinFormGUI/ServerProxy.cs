using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyServer;
using ProxyServer.BusinessLayer;
namespace ServerWinFormGUI
{
    public class ServerProxy : IServerInterface
    {
        protected ServerInterface.ServerInterfaceSoapClient soapClient;

        public ServerProxy(ServerInterface.ServerInterfaceSoapClient client)
        {
            soapClient = client;
        }

        #region IServerInterface Members

        public ProxyServer.BusinessLayer.WebResponse UpdateStateRequest(ProxyServer.BusinessLayer.ServerAuthStruct auth, string stateGuid, string informationXml)
        {
            WebResponse response = new WebResponse();
            ServerInterface.WebResponse localRes = soapClient.UpdateStateRequest(GetAuth(auth), stateGuid, informationXml);
            response.ErrorCode = localRes.ErrorCode;
            response.ErrorDescription = localRes.ErrorDescription;
            return response;
        }

		  public ProxyServer.BusinessLayer.WebResponse UpdateTaskRequest(ProxyServer.BusinessLayer.ServerAuthStruct auth, int taskID, int newState, string xmlResponse, string errorDetails)
        {
            WebResponse response = new WebResponse();
            ServerInterface.WebResponse localRes = soapClient.UpdateTaskRequest(GetAuth(auth), taskID, newState, xmlResponse, errorDetails);
            response.ErrorCode = localRes.ErrorCode;
            response.ErrorDescription = localRes.ErrorDescription;
            return response;
        }

        public ProxyServer.BusinessLayer.GroupTaskResponse GetTasks(ProxyServer.BusinessLayer.ServerAuthStruct auth)
        {
            GroupTaskResponse grResp = new GroupTaskResponse();
            ServerInterface.GroupTaskResponse locRes = soapClient.GetTasks(GetAuth(auth));
            grResp.ErrorCode = locRes.ErrorCode;
            grResp.ErrorDescription = locRes.ErrorDescription;
            if (grResp.ErrorCode != 0)
            {
                return grResp;
            }
            grResp.Tasks = new MWRCommonTypes.TaskStruct[locRes.Tasks.Length];
            for (int i = 0; i < locRes.Tasks.Length; i++)
            {
                grResp.Tasks[i] = new MWRCommonTypes.TaskStruct();
                grResp.Tasks[i].DateChecked = locRes.Tasks[i].DateChecked;
                grResp.Tasks[i].DateCompleted = locRes.Tasks[i].DateCompleted;
                grResp.Tasks[i].DateRegistered = locRes.Tasks[i].DateRegistered;
                grResp.Tasks[i].DateSended = locRes.Tasks[i].DateSended;
                grResp.Tasks[i].Guid = locRes.Tasks[i].Guid;
                grResp.Tasks[i].ID = locRes.Tasks[i].ID;
                grResp.Tasks[i].Machine = locRes.Tasks[i].Machine;
                grResp.Tasks[i].State = locRes.Tasks[i].State;
                grResp.Tasks[i].XmlRequest = locRes.Tasks[i].XmlRequest;
                grResp.Tasks[i].XmlResponse = locRes.Tasks[i].XmlResponse;
            }
            return grResp;
        }

        protected ServerInterface.ServerAuthStruct GetAuth(ProxyServer.BusinessLayer.ServerAuthStruct auth)
        {
            ServerInterface.ServerAuthStruct newAuth = new ServerInterface.ServerAuthStruct();
            newAuth.MachineGuid = auth.MachineGuid;
            newAuth.AuthToken = auth.AuthToken;
            return newAuth;
        }

        public DictionaryResponse GetActiveStates(ServerAuthStruct auth)
        {
            ServerInterface.DictionaryResponse servResponse = soapClient.GetActiveStates(GetAuth(auth));
            return CommonResponse(servResponse);
        }

        public DictionaryResponse GetActiveTasks(ServerAuthStruct auth)
        {
            ServerInterface.DictionaryResponse servResponse = soapClient.GetActiveTasks(GetAuth(auth));
            return CommonResponse(servResponse);
        }

        protected DictionaryResponse CommonResponse(ServerInterface.DictionaryResponse servResponse)
        {
            DictionaryResponse response = new DictionaryResponse();
            if (servResponse.ErrorCode == 0)
            {
                response.DictionaryTable = new MWRCommonTypes.DictionaryEntry[servResponse.DictionaryTable.Length];
                for (int i = 0; i < servResponse.DictionaryTable.Length; i++)
                {
                    MWRCommonTypes.DictionaryEntry entry = new MWRCommonTypes.DictionaryEntry();
                    entry.Active = servResponse.DictionaryTable[i].Active;
                    entry.Assembly = servResponse.DictionaryTable[i].Assembly;
                    entry.Config = servResponse.DictionaryTable[i].Config;
                    entry.Guid = servResponse.DictionaryTable[i].Guid;
                    entry.Name = servResponse.DictionaryTable[i].Name;
                    entry.Type = (MWRCommonTypes.Enum.ObjectType)(int)servResponse.DictionaryTable[i].Type;
                    entry.TypeOf = servResponse.DictionaryTable[i].TypeOf;
                    response.DictionaryTable[i] = entry;
                }
            }
            response.ErrorCode = servResponse.ErrorCode;
            response.ErrorDescription = servResponse.ErrorDescription;
            return response;
        }

        #endregion
    }
}
