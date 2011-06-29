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
using MWRCommonTypes;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections.Generic;

namespace ProxyServer
{
    /// <summary>
    /// Summary description for ServerInterface
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServerInterface : System.Web.Services.WebService, IServerInterface
    {

        [WebMethod]
        public WebResponse UpdateStateRequest(ServerAuthStruct auth, string stateGuid, string informationXml)
        {
            WebResponse response = new WebResponse();
            ServerRequestHandler handler = new ServerRequestHandler();

            try
            {
                if (!handler.Authorize(auth))
                {
                    response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Error, string.Format("Błąd autoryzacji - maszyna {0}", auth.MachineGuid));
                    return response;
                }

                bool active = false;
                if (!handler.MWRStateExists(stateGuid, ref active))
                {
                    response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Error, string.Format("Stan {0} nie istnieje ! ", stateGuid));
                    return response;
                }

                if (!active)
                {
                    response.ErrorCode = (int)ResponseCode.RequestedObjectIsDisabled;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Warning, string.Format("Stan {0} jest wyłączony ", stateGuid));
                    return response;
                }

                if (!handler.UpdateState(stateGuid, informationXml, auth.MachineGuid))
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.InternalError, System.Diagnostics.TraceEventType.Error, string.Format("Nie udało się dodać stanu - {0}", stateGuid));
                }
                else
                {
                    response.ErrorCode = (int)ResponseCode.OK;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, string.Format("Stan {0} - OK", stateGuid));
                }
                return response;
            }
            catch (Exception exc)
            {
                response.ErrorCode = (int)ResponseCode.ProxyServerError;
                response.ErrorDescription = exc.ToString();
                return response;
            }
            finally
            {
                handler.Dispose();
            }
        }

        [WebMethod]
        public WebResponse UpdateTaskRequest(ServerAuthStruct auth, int taskID, int newState, string xmlResponse, string errorInfo)
        {
            WebResponse response = new WebResponse();
            ServerRequestHandler handler = new ServerRequestHandler();

            try
            {

                if (!handler.Authorize(auth))
                {
                    response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Error, string.Format("Błąd autoryzacji - maszyna {0}", auth.MachineGuid));
                    return response;
                }

                TaskStruct task = handler.GetTask(taskID);

                if (task == null)
                {
                    response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Error, string.Format("UpdateTask - task o ID {0} nie istnieje", taskID.ToString()));
                    return response;
                }

                if (!task.Active)
                {
                    response.ErrorCode = (int)ResponseCode.RequestedObjectIsDisabled;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Error, string.Format("UpdateTask - task o guid {0} jest wyłączony", task.Guid));
                    return response;
                }

                task.State = newState;
                task.XmlResponse = xmlResponse;
                task.ID = taskID;
                task.DateCompleted = DateTime.Now;
					 task.ErrorDetails = errorInfo; 

                if (!handler.UpdateTask(task))
                {
                    response.ErrorCode = (int)ResponseCode.ProxyServerError;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.InternalError, System.Diagnostics.TraceEventType.Error, string.Format("UpdateTask - nie udało się dodać, Task Guid : {0}", task.Guid));
                }
                else
                {
                    response.ErrorCode = (int)ResponseCode.OK;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, string.Format("UpdateTask - {0} - OK", task.Guid));
                }
                return response;

            }
            catch (Exception exc)
            {
                response.ErrorCode = (int)ResponseCode.ProxyServerError;
                response.ErrorDescription = exc.ToString();
                return response;
            }
            finally
            {
                handler.Dispose();
            }
        }

        [WebMethod]
        public GroupTaskResponse GetTasks(ServerAuthStruct auth)
        {
            GroupTaskResponse response = new GroupTaskResponse();
            ServerRequestHandler handler = new ServerRequestHandler();

            try
            {
                if (!handler.Authorize(auth))
                {
                    response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Error, string.Format("Błąd autoryzacji - maszyna {0}", auth.MachineGuid));
                    return response;
                }

                TaskStruct[] taski = handler.GetTasks(auth.MachineGuid);
                if (taski == null)
                {
                    response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, "GetTasks - brak tasków do wykonania");
                    return response;
                }

                response.Tasks = taski;
                response.ErrorCode = (int)ResponseCode.OK;
                LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, string.Format("GetTasks - pobrano {0} tasków do wykonania", taski.Length.ToString()));
                return response;
            }
            catch (Exception exc)
            {
                response.ErrorCode = (int)ResponseCode.ProxyServerError;
                response.ErrorDescription = exc.ToString();
                return response;
            }
            finally
            {
                handler.Dispose();
            }
        }

        [WebMethod]
        public DictionaryResponse GetActiveStates(ServerAuthStruct auth)
        {
            ServerRequestHandler handler = new ServerRequestHandler();
            return GetDictionaryList(new GetDictionaryListDelegate(handler.GetActiveStates), auth);
        }

        [WebMethod]
        public DictionaryResponse GetActiveTasks(ServerAuthStruct auth)
        {
            ServerRequestHandler handler = new ServerRequestHandler();
            return GetDictionaryList(new GetDictionaryListDelegate(handler.GetActiveTasks), auth);
        }

        protected DictionaryResponse GetDictionaryList(GetDictionaryListDelegate method, ServerAuthStruct auth)
        {
            DictionaryResponse response = new DictionaryResponse();
            ServerRequestHandler handler = new ServerRequestHandler();

            try
            {
                if (!handler.Authorize(auth))
                {
                    response.ErrorCode = (int)ResponseCode.AuthorizationFailed;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.AuthorizationFailed, System.Diagnostics.TraceEventType.Error, string.Format("Błąd autoryzacji - maszyna {0}", auth.MachineGuid));
                    return response;
                }

                List<MWRCommonTypes.DictionaryEntry> list = method.Invoke();
                if (list.Count == 0)
                {
                    response.ErrorCode = (int)ResponseCode.RequestedObjectNotFound;
                    LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.BusinessError, System.Diagnostics.TraceEventType.Warning, "Nie odnaleziono żadnych obiektów słownikowych.");
                    return response;
                }
                response.DictionaryTable = list.ToArray();
                response.ErrorCode = (int)ResponseCode.OK;
                LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.OK, System.Diagnostics.TraceEventType.Information, "Lista słownikowa pobrana pomyślnie.");
                return response;
            }
            catch (Exception exc)
            {
                response.ErrorCode = (int)ResponseCode.ProxyServerError;
                response.ErrorDescription = exc.ToString();
                LoggerHelper.Log(LogCategories.ServerRequest, LogEventID.InternalError, System.Diagnostics.TraceEventType.Error, "Wystąpił błąd podczas pobierania listy słownikowej - " + exc.ToString());
            }
            finally
            {
                handler.Dispose();
            }
            return response;
        }
    }

    public delegate List<MWRCommonTypes.DictionaryEntry> GetDictionaryListDelegate();
}
