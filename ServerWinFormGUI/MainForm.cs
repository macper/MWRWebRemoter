using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MWRServerLib;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;

namespace ServerWinFormGUI
{
    public partial class MainForm : Form
    {
        protected MWRProcesser processer;
        protected int stateSuccesfull = 0;
        protected int stateFailed = 0;
        protected int taskSuccesfull = 0;
        protected int taskUpdatedOK = 0;
        protected int taskFailed = 0;
        protected int taskUpdatedFailed = 0;
        protected ServerInterface.ServerInterfaceSoapClient soapClient;

        public MainForm()
        {
            InitializeComponent();
            try
            {
                soapClient = new ServerWinFormGUI.ServerInterface.ServerInterfaceSoapClient();
                processer = new MWRProcesser(new ServerProxy(soapClient), ConfigurationManager.AppSettings["MachineGuid"], ConfigurationManager.AppSettings["AuthorizationToken"]);
                processer.StateMessage += new TextNotifier(processer_StateMessage);
                processer.StateUpdated += new TextNotifier(processer_StateUpdated);
                processer.StateProcessed += new TextNotifier(processer_StateProcessed);
                processer.TaskMessage += new TextNotifier(processer_TaskMessage);
                processer.TaskUpdated += new TextNotifier(processer_TaskUpdated);
                processer.TaskProcessed += new TextNotifier(processer_TaskProcessed);
                lbStateLoaded.Text = processer.StatesLoaded.ToString();
                lbTaskLoaded.Text = processer.TasksLoaded.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Wystąpił błąd podczas inicjalizacji - " + exc.ToString());
            }
        }
        #region TASK

        void processer_TaskProcessed(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            this.Invoke(new HelperDelegate(TaskLog), message, type, additionalInfo);
        }

        void processer_TaskUpdated(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            this.Invoke(new HelperDelegate(TaskLogUpdate), message, type, additionalInfo);
        }

        void processer_TaskMessage(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            this.Invoke(new HelperDelegate(TaskInform), message, type, additionalInfo);
        }

        protected void StartTaskProcess()
        {
            btTask.Text = "Stop";
            processer.StartTaskProcessing();
        }

        protected void StopTaskProcess()
        {
            btTask.Text = "Start";
            processer.StopStateProcessing();
        }

        protected void TaskInform(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            tbTaskProcessing.Text = message;
            if (type == System.Diagnostics.TraceEventType.Information)
            {
                tbTaskProcessing.ForeColor = Color.Black;
            }
            else
            {
                tbTaskProcessing.ForeColor = Color.Red;
            }
            LoggerHelper.Log("Task", type, message + additionalInfo);
        }

        protected void TaskLog(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            if (type == System.Diagnostics.TraceEventType.Error)
            {
                taskFailed++;
                lbTaskFailed.Text = taskFailed.ToString();
            }
            else
            {
                taskSuccesfull++;
                lbTaskSuccesfull.Text = taskSuccesfull.ToString();
            }
            TaskInform(message, type, additionalInfo);
        }

        protected void TaskLogUpdate(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            if (type == System.Diagnostics.TraceEventType.Error)
            {
                taskUpdatedFailed++;
                lbTaskUpdatedFailed.Text = taskUpdatedFailed.ToString();
            }
            else
            {
                taskUpdatedOK++;
                lbTaskUpdatedOK.Text = taskUpdatedOK.ToString();
            }
            TaskInform(message, type, additionalInfo);
        }

        #endregion

        #region STATE

        void processer_StateProcessed(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            if (type == System.Diagnostics.TraceEventType.Error)
            {
                this.Invoke(new HelperDelegate(StateLog), message, type, additionalInfo);
            }
            else
            {
                this.Invoke(new HelperDelegate(StateInform), message, type, additionalInfo);
            }
        }

        void processer_StateUpdated(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            this.Invoke(new HelperDelegate(StateLog), message, type, additionalInfo);
        }

        void processer_StateMessage(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            this.Invoke(new HelperDelegate(StateInform), message, type, additionalInfo);
        }

        protected void StateProcessStart()
        {
            btState.Text = "Stop";
            processer.StartStateProcessing();
        }

        protected void StateProcessStop()
        {
            btState.Text = "Start";
            processer.StopStateProcessing();
        }

        protected void StateInform(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            tbStateText.Text = message;
            if (type == System.Diagnostics.TraceEventType.Information)
            {
                tbStateText.ForeColor = Color.Black;
            }
            else
            {
                tbStateText.ForeColor = Color.Red;
            }
            LoggerHelper.Log("State", type, message + additionalInfo);
        }
        protected void StateLog(string message, System.Diagnostics.TraceEventType type, string additionalInfo)
        {
            tbStateText.Text = message;
            if (type == System.Diagnostics.TraceEventType.Information)
            {
                tbStateText.ForeColor = Color.Green;
                stateSuccesfull++;
                lbStateSuccess.Text = stateSuccesfull.ToString();
            }
            else
            {
                tbStateText.ForeColor = Color.Red;
                stateFailed++;
                lbStateError.Text = stateFailed.ToString();
            }
            LoggerHelper.Log("State", type, message + additionalInfo);
        }

        private void btState_Click(object sender, EventArgs e)
        {
            if (processer.StateProcessing)
            {
                btState.Text = "Start";
                processer.StopStateProcessing();
            }
            else
            {
                btState.Text = "Stop";
                processer.StartStateProcessing();
            }
        }
        #endregion

        private void btTask_Click(object sender, EventArgs e)
        {
            if (processer.TaskProcessing)
            {
                btTask.Text = "Start";
                processer.StopTaskProcessing();
            }
            else
            {
                btTask.Text = "Stop";
                processer.StartTaskProcessing();
            }
        }
    }

    public delegate void HelperDelegate(string msg, System.Diagnostics.TraceEventType type, string additionalInfo);
}
