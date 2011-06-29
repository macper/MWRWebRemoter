using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MWRClientWebInterface.Controls.Tasks
{
    public abstract class BaseTaskControl : BaseControl
    {
        public abstract void SetControlsDisabled();
        public abstract void Fill(MWRCommonTypes.Task tsk);
        public abstract void Update(DateTime dateToExecute);
		public abstract string TaskID
		{
			get;
		}

        public void InitControl(bool createMode, MWRCommonTypes.Task tsk)
        {
            if (!createMode)
            {
                SetControlsDisabled();
                Fill(tsk);
            }
        }

        protected void InitTask(MWRCommonTypes.Task task)
        {
            task.State = MWRCommonTypes.Enum.TaskState.Registered;
            task.Machine = BasePage.CurrentMachine;
            task.User = BasePage.LoggedUser;
        }
    }

    public abstract class BaseTaskControl<T> : BaseTaskControl where T:MWRCommonTypes.Task
    {
        protected abstract void UpdateProperties(T task);
        protected abstract T GetNewTask();

        public override void Update(DateTime dateToExecute)
        {
            T newTask = GetNewTask();
            InitTask(newTask);
            newTask.DateExecute = dateToExecute;
            UpdateProperties(newTask);
            ProxyServer.BusinessLayer.TaskResponse resp = BasePage.ProxyServerInstance.CreateTaskRequest(BasePage.AuthorizationData, newTask.ToTaskStruct());
            if (resp.ErrorCode != 0)
            {
                throw new ApplicationException("Wystąpił błąd podczas Update - kod błędu: " + resp.ErrorCode);
            }

        }
    }
}
