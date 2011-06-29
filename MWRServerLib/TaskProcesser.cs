using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWRCommonTypes;
using ProxyServer.BusinessLayer;

namespace MWRServerLib
{
    public class TaskProcesser : BaseProcesser<MWRCommonTypes.DictionaryEntry>
    {
        public override MWRCommonTypes.ProcessResult Update(MWRCommonTypes.IMWRObject obj, ProcessResult result)
        {
            ProcessResult processResult = new ProcessResult();
            Task task = (Task)obj;
            if (result.ErrorCode != 0)
            {
                task.State = MWRCommonTypes.Enum.TaskState.Failed;
            }
            else
            {
                task.State = MWRCommonTypes.Enum.TaskState.Completed;
            }
            try
            {
                WebResponse webResp = webServer.UpdateTaskRequest(authorizationData, task.ID, (int)task.State, task.ToTaskStruct().XmlResponse, result.ErrorDetails);
                processResult.ErrorCode = webResp.ErrorCode;
                processResult.ErrorDetails = webResp.ErrorDescription;
            }
            catch (Exception exc)
            {
                processResult.ErrorCode = (int)ResponseCode.ServerError;
                processResult.ErrorDetails = exc.ToString();
            }
            return processResult;
        }

        public TaskProcesser(ProxyServer.IServerInterface webServ, string machineGuid, string authToken)
            : base(webServ, machineGuid, authToken)
        {
            DictionaryResponse resp = webServ.GetActiveTasks(authorizationData);
            if (resp.ErrorCode != 0 && resp.ErrorCode != (int)ResponseCode.RequestedObjectNotFound)
            {
                throw new Exception("Nie udało się pobrać listy aktywnych tasków. Wystąpił błąd " + resp.ErrorCode.ToString());
            }
				if (resp.ErrorCode == (int)ResponseCode.RequestedObjectNotFound)
				{
					resp.DictionaryTable = new DictionaryEntry[] { };
				}
            objectFactory = new TaskFactory(resp.DictionaryTable, machine);
        }

        public void Process()
        {
            GroupTaskResponse resp = webServer.GetTasks(authorizationData);
            MessageHit("Rozpoczęto wczytywanie nowych tasków do wykonania.", System.Diagnostics.TraceEventType.Information);
            if (resp.ErrorCode == (int)ResponseCode.RequestedObjectNotFound)
            {
                MessageHit("Brak tasków do przetworzenia", System.Diagnostics.TraceEventType.Information);
                return;
            }

            if (resp.ErrorCode != 0)
            {
                throw new Exception("Nie udało się wczytać tasków. Błąd " + resp.ErrorCode.ToString());
            }
            if (resp.Tasks.Length == 0)
            {
                return;
            }
            MessageHit(string.Format("{0} tasków do przetworzenia", resp.Tasks.Length.ToString()), System.Diagnostics.TraceEventType.Information);
            foreach (TaskStruct taskStruct in resp.Tasks)
            {
                Process(taskStruct);
            }
        }
    }
}
