using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MWRClientWebInterface.Controls
{
    public partial class CtlTaskKillProcess : BaseTaskControl<MWRCommonTypes.Tasks.KillProcessTask>
    {
        protected override MWRCommonTypes.Tasks.KillProcessTask GetNewTask()
        {
            return new MWRCommonTypes.Tasks.KillProcessTask();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*protected override ProxyServer.BusinessLayer.TaskResponse UpdateTask()
        {
            MWRCommonTypes.Tasks.KillProcessTask killTask = new MWRCommonTypes.Tasks.KillProcessTask();
            InitTask(killTask);
            killTask.ProcessID = int.Parse(tbProcessID.Text);
            ProxyServer.BusinessLayer.TaskResponse response = BasePage.ProxyServerInstance.CreateTaskRequest(BasePage.AuthorizationData, killTask.ToTaskStruct());
            return response;
        }*/
        protected override void UpdateProperties(MWRCommonTypes.Tasks.KillProcessTask task)
        {
            task.ProcessID = int.Parse(tbProcessID.Text);
        } 

        public override void SetControlsDisabled()
        {
            tbProcessID.Enabled = false;
        }

        public override void Fill(MWRCommonTypes.Task tsk)
        {
            tbProcessID.Text = ((MWRCommonTypes.Tasks.KillProcessTask)tsk).ProcessID.ToString();
        }
    }
}