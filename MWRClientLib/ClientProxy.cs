using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProxyServer;
using ProxyServer.BusinessLayer;
using MWRCommonTypes;

namespace MWRClientLib
{
    public class ClientProxy : IClientInterface
    {
        protected ClientInterface.ClientInterfaceSoapClient client;

        public ClientProxy(ClientInterface.ClientInterfaceSoapClient soapClient)
        {
            client = soapClient;
        }

        #region IClientInterface Members

        public StateResponse GetStateRequest(ProxyServer.BusinessLayer.ClientAuthStruct auth, string guidStateGuid, DateTime time)
        {
            StateResponse response = new StateResponse();
            ClientInterface.StateResponse proxyResp = client.GetStateRequest(GetAuthData(auth), guidStateGuid, time);
            response.ErrorCode = proxyResp.ErrorCode;
            response.ErrorDescription = proxyResp.ErrorDescription;
            if (response.ErrorCode != 0)
            {
                return response;
            }
            response.State = new StateStruct();
            response.State.Active = proxyResp.State.Active;
            response.State.Guid = proxyResp.State.Guid;
            response.State.ID = proxyResp.State.ID;
            response.State.Machine = proxyResp.State.Machine;
            response.State.Name = proxyResp.State.Name;
            response.State.RegisteredDate = proxyResp.State.RegisteredDate;
            response.State.XmlInfo = proxyResp.State.XmlInfo;
            return response;
        }

        public TaskResponse GetTaskRequest(ProxyServer.BusinessLayer.ClientAuthStruct auth, int TaskID)
        {
            TaskResponse response = new TaskResponse();
            ClientInterface.TaskResponse proxyResp = client.GetTaskRequest(GetAuthData(auth), TaskID);
            response.ErrorCode = proxyResp.ErrorCode;
            response.ErrorDescription = proxyResp.ErrorDescription;
            response.TaskID = proxyResp.TaskID;
            response.Task = new TaskStruct();
            response.Task.Active = proxyResp.Task.Active;
            response.Task.DateChecked = proxyResp.Task.DateChecked;
            response.Task.DateCompleted = proxyResp.Task.DateCompleted;
            response.Task.DateRegistered = proxyResp.Task.DateRegistered;
            response.Task.DateSended = proxyResp.Task.DateSended;
            response.Task.DateToExecute = proxyResp.Task.DateToExecute;
            response.Task.Guid = proxyResp.Task.Guid;
            response.Task.ID = proxyResp.Task.ID;
            response.Task.Machine = proxyResp.Task.Machine;
            response.Task.Name = proxyResp.Task.Name;
            response.Task.State = proxyResp.Task.State;
            response.Task.User = proxyResp.Task.User;
            response.Task.XmlRequest = proxyResp.Task.XmlRequest;
            response.Task.XmlResponse = proxyResp.Task.XmlResponse;
				response.Task.ErrorDetails = proxyResp.Task.ErrorDetails;
            return response;
        }

        public TaskResponse CreateTaskRequest(ProxyServer.BusinessLayer.ClientAuthStruct auth, MWRCommonTypes.TaskStruct task)
        {
            TaskResponse response = new TaskResponse();
            ClientInterface.TaskStruct reqStruct = new MWRClientLib.ClientInterface.TaskStruct();
            reqStruct.Active = task.Active;
            reqStruct.DateChecked = task.DateChecked;
            reqStruct.DateCompleted = task.DateCompleted;
            reqStruct.DateRegistered = task.DateRegistered;
            reqStruct.DateSended = task.DateSended;
            reqStruct.Guid = task.Guid;
            reqStruct.ID = task.ID;
            reqStruct.Machine = task.Machine;
            reqStruct.Name = task.Name;
            reqStruct.State = task.State;
            reqStruct.User = task.User;
            reqStruct.XmlRequest = task.XmlRequest;
            reqStruct.XmlResponse = task.XmlResponse;
            reqStruct.DateToExecute = task.DateToExecute;
            ClientInterface.TaskResponse proxyResp = client.CreateTaskRequest(GetAuthData(auth), reqStruct);
            response.ErrorCode = proxyResp.ErrorCode;
            response.ErrorDescription = proxyResp.ErrorDescription;
				if (response.ErrorCode == 0)
				{
					response.TaskID = proxyResp.TaskID;
					response.Task = new TaskStruct();
					response.Task.Active = proxyResp.Task.Active;
					response.Task.DateChecked = proxyResp.Task.DateChecked;
					response.Task.DateCompleted = proxyResp.Task.DateCompleted;
					response.Task.DateRegistered = proxyResp.Task.DateRegistered;
					response.Task.DateSended = proxyResp.Task.DateSended;
					response.Task.Guid = proxyResp.Task.Guid;
					response.Task.ID = proxyResp.Task.ID;
					response.Task.Machine = proxyResp.Task.Machine;
					response.Task.Name = proxyResp.Task.Name;
					response.Task.State = proxyResp.Task.State;
					response.Task.User = proxyResp.Task.User;
					response.Task.XmlRequest = proxyResp.Task.XmlRequest;
					response.Task.XmlResponse = proxyResp.Task.XmlResponse;
					response.Task.DateToExecute = proxyResp.Task.DateToExecute;
				}
            return response;
        }

        public WebResponse DeleteTaskRequest(ProxyServer.BusinessLayer.ClientAuthStruct auth, int TaskID)
        {
            WebResponse response = new WebResponse();
            ClientInterface.WebResponse proxyResp = client.DeleteTaskRequest(GetAuthData(auth), TaskID);
            response.ErrorCode = proxyResp.ErrorCode;
            response.ErrorDescription = proxyResp.ErrorDescription;
            return response;
        }

        protected ClientInterface.ClientAuthStruct GetAuthData(ClientAuthStruct baseAuth)
        {
            ClientInterface.ClientAuthStruct ret = new MWRClientLib.ClientInterface.ClientAuthStruct();
            ret.MachineGuid = baseAuth.MachineGuid;
            ret.UserName = baseAuth.UserName;
            ret.Password = baseAuth.Password;
            return ret;
        }

        public GroupTaskResponse GetTasks(ClientAuthStruct auth, DateTime dateFrom, DateTime dateTo, int[] states, string guid, int startIndex, int rowsCount)
        {
            GroupTaskResponse response = new GroupTaskResponse();
            ClientInterface.ArrayOfInt arrayStates = new MWRClientLib.ClientInterface.ArrayOfInt();
            arrayStates.AddRange(states);
            ClientInterface.GroupTaskResponse proxyResp = client.GetTasks(GetAuthData(auth), dateFrom, dateTo, arrayStates, guid, startIndex, rowsCount);
            response.ErrorCode = proxyResp.ErrorCode;
            response.ErrorDescription = proxyResp.ErrorDescription;
            if (proxyResp.Tasks != null && proxyResp.Tasks.Length > 0)
            {
                response.Tasks = new TaskStruct[proxyResp.Tasks.Length];

                for (int i = 0; i < response.Tasks.Length; i++)
                {
                    response.Tasks[i] = new TaskStruct();
                    response.Tasks[i].Active = proxyResp.Tasks[i].Active;
                    response.Tasks[i].DateChecked = proxyResp.Tasks[i].DateChecked;
                    response.Tasks[i].DateCompleted = proxyResp.Tasks[i].DateCompleted;
                    response.Tasks[i].DateRegistered = proxyResp.Tasks[i].DateRegistered;
                    response.Tasks[i].DateSended = proxyResp.Tasks[i].DateSended;
                    response.Tasks[i].DateToExecute = proxyResp.Tasks[i].DateToExecute;
                    response.Tasks[i].Guid = proxyResp.Tasks[i].Guid;
                    response.Tasks[i].ID = proxyResp.Tasks[i].ID;
                    response.Tasks[i].Machine = proxyResp.Tasks[i].Machine;
                    response.Tasks[i].Name = proxyResp.Tasks[i].Name;
                    response.Tasks[i].State = proxyResp.Tasks[i].State;
                    response.Tasks[i].User = proxyResp.Tasks[i].User;
                    response.Tasks[i].XmlRequest = proxyResp.Tasks[i].XmlRequest;
                    response.Tasks[i].XmlResponse = proxyResp.Tasks[i].XmlResponse;
						  response.Tasks[i].ErrorDetails = proxyResp.Tasks[i].ErrorDetails;
                }
            }
				response.TotalCount = proxyResp.TotalCount;
            return response;
        }

        public DictionaryResponse GetDictionaryEntries(ClientAuthStruct auth, int type, int state)
        {
            DictionaryResponse response = new DictionaryResponse();
            ClientInterface.DictionaryResponse responseProxy = client.GetDictionaryEntries(GetAuthData(auth), type, state);
            response.ErrorCode = responseProxy.ErrorCode;
            response.ErrorDescription = responseProxy.ErrorDescription;
            if (responseProxy.DictionaryTable != null && responseProxy.DictionaryTable.Length > 0)
            {
                response.DictionaryTable = new DictionaryEntry[responseProxy.DictionaryTable.Length];
                for (int i = 0; i < responseProxy.DictionaryTable.Length; i++)
                {
                    response.DictionaryTable[i] = new DictionaryEntry();
                    response.DictionaryTable[i].Active = responseProxy.DictionaryTable[i].Active;
                    response.DictionaryTable[i].Assembly = responseProxy.DictionaryTable[i].Assembly;
                    response.DictionaryTable[i].Config = responseProxy.DictionaryTable[i].Config;
                    response.DictionaryTable[i].Guid = responseProxy.DictionaryTable[i].Guid;
                    response.DictionaryTable[i].Name = responseProxy.DictionaryTable[i].Name;
                    response.DictionaryTable[i].Type = (MWRCommonTypes.Enum.ObjectType)(int)responseProxy.DictionaryTable[i].Type;
                    response.DictionaryTable[i].TypeOf = responseProxy.DictionaryTable[i].TypeOf;
                }
            }
            return response;
        }
        public UserDataResponse GetUserData(ClientAuthStruct auth)
        {
            UserDataResponse response = new UserDataResponse();
            ClientInterface.UserDataResponse proxyResponse = client.GetUserData(GetAuthData(auth));
            response.ErrorCode = proxyResponse.ErrorCode;
            response.ErrorDescription = proxyResponse.ErrorDescription;
				if (response.ErrorCode == 0)
				{
					if (proxyResponse.MachinesList != null && proxyResponse.MachinesList.Length > 0)
					{
						response.MachinesList = new MachineWithPrivilleges[proxyResponse.MachinesList.Length];
						for (int i = 0; i < proxyResponse.MachinesList.Length; i++)
						{
							response.MachinesList[i] = new MachineWithPrivilleges();
							response.MachinesList[i].Description = proxyResponse.MachinesList[i].Description;
							response.MachinesList[i].Guid = proxyResponse.MachinesList[i].Guid;
							response.MachinesList[i].IP = proxyResponse.MachinesList[i].IP;
							response.MachinesList[i].Name = proxyResponse.MachinesList[i].Name;
							response.MachinesList[i].Privilleges = proxyResponse.MachinesList[i].Privilleges.ToArray();
						}
					}
					response.User = new User();
					response.User.Group = proxyResponse.User.Group;
					response.User.ID = proxyResponse.User.ID;
					response.User.Name = proxyResponse.User.Name;
					response.User.Password = proxyResponse.User.Password;
					response.User.RegisterDate = proxyResponse.User.RegisterDate;
				}

            return response;
        }

        #endregion

        #region IClientInterface Members


        public WebResponse UpdateDictionary(ClientAuthStruct auth, string Guid, int newState, string Config)
        {
            WebResponse response = new WebResponse();
            ClientInterface.WebResponse proxyResponse = client.UpdateDictionary(GetAuthData(auth), Guid, newState, Config);
            response.ErrorCode = proxyResponse.ErrorCode;
            response.ErrorDescription = proxyResponse.ErrorDescription;
            return response;
        }

        #endregion
    }
}
