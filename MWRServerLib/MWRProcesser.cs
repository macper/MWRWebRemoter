using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Configuration;
using MWRCommonTypes;
using MWRCommonTypes.Configuration;
using ProxyServer;

namespace MWRServerLib
{
    public class MWRProcesser
    {
        protected StateProcesser stateProcesser;
        protected Timer stateTimer;
        protected long stateCounter = 1;
        public bool StateProcessing { get { return stateTimer.Enabled; } }
        public event TextNotifier StateMessage;
        public event TextNotifier StateUpdated;
        public event TextNotifier StateProcessed;
        public int StatesLoaded
        {
            get { return stateProcesser.LoadedObjectCount; }
        }

        protected TaskProcesser taskProcesser;
        protected Timer taskTimer;
        public bool TaskProcessing { get { return taskTimer.Enabled; } }
        public event TextNotifier TaskMessage;
        public event TextNotifier TaskUpdated;
        public event TextNotifier TaskProcessed;
        public int TasksLoaded
        {
            get { return taskProcesser.LoadedObjectCount; }
        }


        public MWRProcesser(IServerInterface webServ, string machineGuid, string authToken)
        {
            stateProcesser = new StateProcesser(webServ, machineGuid, authToken);
            stateTimer = new Timer(1000);
            stateTimer.Elapsed += new ElapsedEventHandler(stateTimer_Elapsed);
            stateProcesser.ProcessCompleted += new Notifier(stateProcesser_ProcessCompleted);
            stateProcesser.UpdateCompleted += new Notifier(stateProcesser_UpdateCompleted);
            stateProcesser.ProcessStarted += new Notifier(stateProcesser_ProcessStarted);
            stateProcesser.Message += new TextNotifier(stateProcesser_Message);

            taskProcesser = new TaskProcesser(webServ, machineGuid, authToken);
            taskTimer = new Timer(2000);
            taskTimer.Elapsed += new ElapsedEventHandler(taskTimer_Elapsed);
            taskProcesser.ProcessStarted += new Notifier(taskProcesser_ProcessStarted);
            taskProcesser.UpdateCompleted += new Notifier(taskProcesser_UpdateCompleted);
            taskProcesser.ProcessCompleted += new Notifier(taskProcesser_ProcessCompleted);
            taskProcesser.Message += new TextNotifier(taskProcesser_Message);
        }

        #region STATE
        void stateProcesser_Message(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            StateMessage(message, type, additionalInfo);
        }

        void stateProcesser_ProcessStarted(IMWRObject obj, ProcessResult result)
        {
            StateMessage(string.Format("State - {0} - rozpoczęto proces obsługi.", obj.Name), System.Diagnostics.TraceEventType.Information, null);
        }

        void stateProcesser_UpdateCompleted(IMWRObject obj, ProcessResult result)
        {
            if (result.ErrorCode == 0)
            {
                StateUpdated(string.Format("State - {0} - zapisano pomyślnie.", obj.Name), System.Diagnostics.TraceEventType.Information, null);
            }
            else
            {
                StateUpdated(string.Format("State - {0} - wystąpił błąd podczas zapisu nr. {1}.", obj.Name, result.ErrorCode), System.Diagnostics.TraceEventType.Error, result.ErrorDetails);
            }
        }

        void stateProcesser_ProcessCompleted(IMWRObject obj, ProcessResult result)
        {
            if (result.ErrorCode == 0)
            {
                StateProcessed(string.Format("State - {0} - przetworzono pomyślnie.", obj.Name), System.Diagnostics.TraceEventType.Information, null);
            }
            else
            {
                StateProcessed(string.Format("State - {0} - wystąpił błąd podczas przetwarzania nr. {1}", obj.Name, result.ErrorCode.ToString()), System.Diagnostics.TraceEventType.Error, result.ErrorDetails);
            }
        }

        void stateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            stateCounter++;
            stateProcesser.Process(stateCounter);
        }

        public void StartStateProcessing()
        {
            stateTimer.Enabled = true;
            StateMessage("Rozpoczęto proces przetwarzania statesów", System.Diagnostics.TraceEventType.Information, null);
        }

        public void StopStateProcessing()
        {
            stateTimer.Enabled = false;
            StateMessage("Proces przetwarzania statesów zatrzymany.", System.Diagnostics.TraceEventType.Information, null);
        }
        #endregion

        #region TASK
        void taskProcesser_Message(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            TaskMessage(message, type, additionalInfo);
        }

        void taskProcesser_ProcessCompleted(IMWRObject obj, ProcessResult result)
        {
            if (result.ErrorCode == 0)
            {
                TaskProcessed(string.Format("Task {0} - przetworzono prawidłowo", obj.Name), System.Diagnostics.TraceEventType.Information, null);
            }
            else
            {
                TaskProcessed(string.Format("Task {0} - błąd podczas przetwarzania. Błąd nr. {1}", obj.Name, result.ErrorCode.ToString()), System.Diagnostics.TraceEventType.Error, result.ErrorDetails);
            }
        }

        void taskProcesser_UpdateCompleted(IMWRObject obj, ProcessResult result)
        {
            if (result.ErrorCode == 0)
            {
                TaskUpdated(string.Format("Task {0} - zapisano pomyślnie", obj.Name), System.Diagnostics.TraceEventType.Information, null);
            }
            else
            {
                TaskUpdated(string.Format("Task {0} - wystąpił błąd podczas zapisu. Nr. błędu {1}", obj.Name, result.ErrorCode.ToString()), System.Diagnostics.TraceEventType.Error, result.ErrorDetails);
            }
        }

        void taskProcesser_ProcessStarted(IMWRObject obj, ProcessResult result)
        {
            TaskMessage("Rozpoczęto przetwarzanie - task " + obj.Name, System.Diagnostics.TraceEventType.Information, null);
        }

        void taskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                taskProcesser.Process();
            }
            catch (Exception exc)
            {
                TaskMessage("Błąd podczas przetwarzania tasków." + exc.ToString(), System.Diagnostics.TraceEventType.Error, exc.ToString());
            }
        }

        public void StartTaskProcessing()
        {
            taskTimer.Enabled = true;
            TaskMessage("Proces obsługi tasków rozpoczęty ...", System.Diagnostics.TraceEventType.Information, null);
        }

        public void StopTaskProcessing()
        {
            taskTimer.Enabled = false;
            TaskMessage("Proces obsługi tasków zatrzymany", System.Diagnostics.TraceEventType.Information, null);
        }
        #endregion
    }
}
