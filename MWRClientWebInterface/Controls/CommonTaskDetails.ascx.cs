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
using MWRCommonTypes;

namespace MWRClientWebInterface.Controls
{
    public partial class CommonTaskDetails : BaseControl
    {
        protected Task task;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitControl(Task task)
        {
            this.task = task;
            lbTaskName.Text = task.Name;
            lbTaskGuid.Text = task.Guid;
            lbTaskID.Text = task.ID.ToString();
            lbTaskState.Text = task.State.ToString();
            lbTaskDateRegistered.Text = task.DateRegistered.ToString();
            lbTaskDateSended.Text = task.DateSended.ToString();
            lbTaskDateCompleted.Text = task.DateCompleted.ToString();
            lbTaskDateExecute.Text = task.DateExecute.ToString();
        }
    }
}