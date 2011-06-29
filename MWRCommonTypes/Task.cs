using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRCommonTypes
{
    public abstract class Task : BusinessObject, IMWRObject
    {
        protected string guid;
        public string Guid
        {
            get { return guid; }
        }

        public DateTime DateRegistered { get; set; }
        public DateTime DateSended { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateChecked { get; set; }
        public DateTime DateExecute { get; set; }
        public User User { get; set; }
        public Machine Machine { get; set; }
        public int ID { get; set; }
        public Enum.TaskState State { get; set; }
		  public string ErrorDetails { get; set; }

        public Task(string guid)
        {
            this.guid = guid;
        }

        #region IMWRObject Members

        public MWRCommonTypes.Enum.ObjectType Type
        {
            get { return MWRCommonTypes.Enum.ObjectType.Task; }
        }

        public bool Active
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public abstract ProcessResult Process();
        public abstract void LoadConfig(string configString);
        public void Init(TaskStruct taskStruct)
        {
            Active = taskStruct.Active;
            DateChecked = taskStruct.DateChecked;
            DateCompleted = taskStruct.DateCompleted;
            DateRegistered = taskStruct.DateRegistered;
            DateSended = taskStruct.DateSended;
            User = new User();
            User.ID = taskStruct.User;
            Machine = new Machine();
            Machine.Guid = taskStruct.Machine;
            ID = taskStruct.ID;
            Name = taskStruct.Name;
            State = (Enum.TaskState)taskStruct.State;
            DateExecute = taskStruct.DateToExecute;
				ErrorDetails = taskStruct.ErrorDetails;
            InitFromXML(taskStruct.XmlRequest, taskStruct.XmlResponse);
        }
        protected abstract void InitFromXML(string xmlRequest, string xmlResponse);
        protected abstract string GetRequestXML();
        protected abstract string GetResponseXML();

        public TaskStruct ToTaskStruct()
        {
            TaskStruct taskStruct = new TaskStruct();
            taskStruct.DateChecked = DateChecked;
            taskStruct.DateCompleted = DateCompleted;
            taskStruct.DateRegistered = DateRegistered;
            taskStruct.DateSended = DateSended;
            taskStruct.Guid = Guid;
            taskStruct.Machine = Machine.Guid;
            taskStruct.State = (int)State;
            taskStruct.User = User.ID;
            taskStruct.XmlRequest = GetRequestXML();
            taskStruct.XmlResponse = GetResponseXML();
            taskStruct.ID = ID;
            taskStruct.DateToExecute = DateExecute;
				taskStruct.ErrorDetails = ErrorDetails;
            return taskStruct;
        }

        #endregion

		 #region UTILITY
		  protected string GetRequestResponse(bool IsRequest, Dictionary<string, string> Settings)
		  {
			  StringBuilder strBld = new StringBuilder();
			  if (IsRequest)
			  {
				  strBld.Append("<Request>");
			  }
			  else
			  {
				  strBld.Append("<Response>");
			  }
			  foreach (string key in Settings.Keys)
			  {
				  strBld.AppendFormat("<{0}>{1}</{0}>", key, Settings[key]);
			  }
			  if (IsRequest)
			  {
				  strBld.Append("</Request>");
			  }
			  else
			  {
				  strBld.Append("</Response>");
			  }
			  return strBld.ToString();
		  }
		 #endregion
	 }
}
