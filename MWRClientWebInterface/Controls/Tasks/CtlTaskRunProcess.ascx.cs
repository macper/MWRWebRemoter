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

namespace MWRClientWebInterface.Controls.Tasks
{
    public partial class CtlTaskRunProcess : BaseTaskControl<MWRCommonTypes.Tasks.StartProcessTask>
    {
		public override string TaskID
		{
			get { return MWRCommonTypes.Tasks.StartProcessTask.UniqueID; }
		}
        protected override MWRCommonTypes.Tasks.StartProcessTask GetNewTask()
        {
            return new MWRCommonTypes.Tasks.StartProcessTask();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void UpdateProperties(MWRCommonTypes.Tasks.StartProcessTask task)
        {
            task.FilePath = tbProcessName.Text;
        }

        public override void SetControlsDisabled()
        {
            tbProcessName.Enabled = false;
        }

        public override void Fill(MWRCommonTypes.Task tsk)
        {
            MWRCommonTypes.Tasks.StartProcessTask runTask = tsk as MWRCommonTypes.Tasks.StartProcessTask;
            tbProcessName.Text = runTask.FilePath;
        }
    }
}