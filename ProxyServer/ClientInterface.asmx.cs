using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using ProxyServer.BusinessLayer;
using Microsoft.Practices.EnterpriseLibrary.Logging;


namespace ProxyServer
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientInterface : System.Web.Services.WebService, IClientInterface
    {

        [WebMethod]
        public StateResponse GetStateRequest(ClientAuthStruct auth, string guidStateGuid, DateTime time)
        {
            StateResponse response = new StateResponse();
            using (ClientRequestHandler handler = new ClientRequestHandler())
            {
                try
                {
                    if (handler.Authorize(auth, null))
                    {
                        response.State = handler.GetState(guidStateGuid, auth.MachineGuid);
                        if (response.State == null)
                        {
                            response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Warning, string.Format("GetStateRequest - State nie istnieje ({0})", guidStateGuid));
                        }
                        else
                        {
                            if (!response.State.Active)
                            {
                                response.State = null;
                                response.ErrorCode = (int)ResponseCode.RequestedObjectIsDisabled;
                                LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Warning, string.Format("GetStateRequest - State nieaktywny ({0})", guidStateGuid));
                            }
                            response.ErrorCode = (int)BusinessLayer.ResponseCode.OK;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, "GetStateRequest - OK");
                        }
                    }
                    else
                    {
                        LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Warning, string.Format("Autoryzacja nieudana! Użytkownik {0} hasło {1}", auth.UserName, auth.Password));
                        response.ErrorCode = (int)BusinessLayer.ResponseCode.AuthorizationFailed;
                    }
                }
                catch (Exception exc)
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    response.ErrorDescription = exc.ToString();
                    LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.InternalError, System.Diagnostics.TraceEventType.Error, exc.ToString());
                }
            }
            return response;
        }

        [WebMethod]
        public TaskResponse GetTaskRequest(ClientAuthStruct auth, int TaskID)
        {
            TaskResponse response = new TaskResponse();
            using (ClientRequestHandler handler = new ClientRequestHandler())
            {
                try
                {
                    if (handler.Authorize(auth, null))
                    {
                        response.Task = handler.GetTask(TaskID);
                        if (response.Task == null)
                        {
                            response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Warning, string.Format("GetTaskRequest - Task nie istnieje ({0})", TaskID));
                        }
                        else
                        {
                            if (!response.Task.Active)
                            {
                                response.Task = null;
                                response.ErrorCode = (int)ResponseCode.RequestedObjectIsDisabled;
                                LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Warning, string.Format("GetTaskRequest - Task nie istnieje ({0})", TaskID));
                            }
                            else
                            {
                                response.ErrorCode = (int)BusinessLayer.ResponseCode.OK;
                                LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, "GetTaskRequest - OK");
                            }
                        }
                    }
                    else
                    {
                        LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Warning, string.Format("Autoryzacja nieudana! Użytkownik {0} hasło {1}", auth.UserName, auth.Password));
                        response.ErrorCode = (int)BusinessLayer.ResponseCode.AuthorizationFailed;
                    }
                }
                catch (Exception exc)
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    response.ErrorDescription = exc.ToString();
                    LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.InternalError, System.Diagnostics.TraceEventType.Error, exc.ToString());
                }
            }
            return response;
        }

        [WebMethod]
        public TaskResponse CreateTaskRequest(ClientAuthStruct auth, MWRCommonTypes.TaskStruct task)
        {
            TaskResponse response = new TaskResponse();
            using (ClientRequestHandler handler = new ClientRequestHandler())
            {
                try
                {
                    if (handler.Authorize(auth, null))
                    {
                        int taskID = -1;
                        if (handler.CreateTask(task, ref taskID))
                        {
                            response.Task = task;
                            response.TaskID = taskID;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, "CreateTaskRequest - OK");
                            response.ErrorCode = (int)ResponseCode.OK;
                        }
                        else
                        {
                            response.ErrorCode = (int)ResponseCode.IncorrectDataError;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Error, string.Format("Nie udało się dodać Taska ({0})", task.Guid));
                        }
                    }
                    else
                    {
                        response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                        LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Warning, "Autoryzacja nieudana - użytkownik " + auth.UserName);
                    }
                }
                catch (Exception exc)
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    response.ErrorDescription = exc.ToString();
                }
            }
            return response;
        }

        [WebMethod]
        public WebResponse DeleteTaskRequest(ClientAuthStruct auth, int TaskID)
        {
            WebResponse response = new WebResponse();
            using (ClientRequestHandler handler = new ClientRequestHandler())
            {
                try
                {
                    if (handler.Authorize(auth, null))
                    {
                        if (handler.DeleteTask(TaskID))
                        {
                            response.ErrorCode = (int)ResponseCode.OK;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, "DeleteTask - OK");
                        }
                        else
                        {
                            response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Error, "Nie udało się usunąć Taska o ID " + TaskID.ToString());
                        }
                    }
                    else
                    {
                        response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                        LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Warning, "Autoryzacja nieudana - użytkownik " + auth.UserName);
                    }
                }
                catch (Exception exc)
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    response.ErrorDescription = exc.ToString();
                }
            }
            return response;
        }

        [WebMethod]
        public GroupTaskResponse GetTasks(ClientAuthStruct auth, DateTime dateFrom, DateTime dateTo, int[] states, string guid, int startIndex, int rowsCount)
        {
            return (GroupTaskResponse)CommonFunction(new GroupTaskResponse(), auth, new WebMethodInvoker(GetTasks), new object[] { auth.MachineGuid, dateFrom, dateTo, states, guid, startIndex, rowsCount });
        }

        [WebMethod]
        public DictionaryResponse GetDictionaryEntries(ClientAuthStruct auth, int type, int state)
        {
            return (DictionaryResponse)CommonFunction(new DictionaryResponse(), auth, new WebMethodInvoker(GetDictionaries), new object[] { type, state });
        }

        [WebMethod]
        public UserDataResponse GetUserData(ClientAuthStruct auth)
        {
            return (UserDataResponse)CommonFunction(new UserDataResponse(), auth, new WebMethodInvoker(GetUserData), new object[] {auth.UserName, auth.Password});
        }

        [WebMethod]
        public WebResponse UpdateDictionary(ClientAuthStruct auth, string Guid, int newState, string Config)
        {
            return CommonFunction(new WebResponse(), auth, new WebMethodInvoker(UpdateDictionary), new object[] { Guid, newState, Config });
        }

        protected WebResponse CommonFunction(WebResponse emptyStructure, ClientAuthStruct auth, WebMethodInvoker method, object [] arguments)
        {
            WebResponse response = emptyStructure;
            using (ClientRequestHandler handler = new ClientRequestHandler())
            {
                try
                {
                    if (handler.Authorize(auth, null))
                    {
                        response = method(handler, arguments);
                        if (response.ErrorCode == (int)ResponseCode.OK)
                        {
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, method.Method.Name);
                        }
                        else
                        {
                            LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Error, method.Method.Name);
                        }
                    }
                    else
                    {
                        response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                        LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Error, method.Method.Name);
                    }
                }
                catch (Exception exc)
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    LoggerHelper.Log(LogCategories.ClientRequest, LogEventID.InternalError, System.Diagnostics.TraceEventType.Error, method.Method.Name + " błąd - " + exc.ToString());
                }
            }
            return response;
        }

        protected GroupTaskResponse GetTasks(ClientRequestHandler handler, object[] arguments)
        {
            GroupTaskResponse response = new GroupTaskResponse();
            response.Tasks = handler.GetTasks((string)arguments[0], (DateTime)arguments[1], (DateTime)arguments[2], (int [])arguments[3], (string)arguments[4], (int)arguments[5], (int)arguments[6], out response.TotalCount);
            response.ErrorCode = 0;
            return response;
        }

        protected DictionaryResponse GetDictionaries(ClientRequestHandler handler, object[] arguments)
        {
            DictionaryResponse response = new DictionaryResponse();
            response.DictionaryTable = handler.GetDictionaryEntries((int)arguments[0], (int)arguments[1]);
            response.ErrorCode = 0;
            return response;
        }

        protected UserDataResponse GetUserData(ClientRequestHandler handler, object[] arguments)
        {
            UserDataResponse response = new UserDataResponse();
            MWRCommonTypes.MachineWithPrivilleges[] machines;
            MWRCommonTypes.User user;
            handler.GetUserData((string)arguments[0], (string)arguments[1], out machines, out user);
            response.MachinesList = machines;
            response.User = user;
            response.ErrorCode = 0;
            return response;
        }

        protected WebResponse UpdateDictionary(ClientRequestHandler handler, object[] arguments)
        {
            WebResponse response = new WebResponse();
            if (handler.UpdateDictionary((string)arguments[0], (int)arguments[1], (string)arguments[2]))
            {
                response.ErrorCode = 0;
            }
            else
            {
                response.ErrorCode = (int)ProxyServer.BusinessLayer.ResponseCode.ProxyServerError;
                response.ErrorDescription = "Nie udało się zapisać";
            }
            return response;
        }
        
        public delegate WebResponse WebMethodInvoker(ClientRequestHandler handler, object[] arguments);
    }
}
