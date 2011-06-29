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
    public partial class ProcessInfoStateInfoControl : StateInfoControl
    {
        MWRCommonTypes.States.RefreshProcessListState.ProcessInfo[] Processes { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public override void InitControl(MWRCommonTypes.State state)
        {
            MWRCommonTypes.States.RefreshProcessListState refreshState = state as MWRCommonTypes.States.RefreshProcessListState;
            Processes = refreshState.Processes;
            dgProcessList.DataSource = Processes;
            dgProcessList.DataBind();
        }
    }
}