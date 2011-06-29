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
	public partial class CtlTaskFileManage : BaseTaskControl<MWRCommonTypes.Tasks.FileManageTask>
	{
		public override void SetControlsDisabled()
		{
			dlOperationType.Enabled = false;
			tbDestination.Enabled = false;
			tbPath.Enabled = false;
			tbContent.Enabled = false;
		}

		public override void Fill(MWRCommonTypes.Task tsk)
		{
			MWRCommonTypes.Tasks.FileManageTask fTask = tsk as MWRCommonTypes.Tasks.FileManageTask;
			dlOperationType.SelectedIndex = (int)fTask.CurrentOperation;
			tbPath.Text = fTask.RequestedObjectPath;
			tbDestination.Text = fTask.DestinationObjectPath;
			if (fTask.State == MWRCommonTypes.Enum.TaskState.Completed)
			{
				if (fTask.CurrentOperation == MWRCommonTypes.Tasks.OperationType.GetFile || fTask.CurrentOperation == MWRCommonTypes.Tasks.OperationType.PutFile)
				{
					tbContent.Text = fTask.FileContents;
				}
				if (fTask.CurrentOperation == MWRCommonTypes.Tasks.OperationType.GetDirectory)
				{
					TreeNode root = new TreeNode();
					root.Text = fTask.ProcessedObject.Name;
					FillTree(root, fTask.ProcessedObject as MWRCommonTypes.Tasks.MWRFileDirectory);
					treeView.Nodes.Add(root);
				}
			}
			
		}

		private void FillTree(TreeNode rootNode, MWRCommonTypes.Tasks.MWRFileDirectory dir)
		{
			foreach (MWRCommonTypes.Tasks.MWRFileBase mwr in dir.SubFiles)
			{
				if (!mwr.IsDirectory)
				{
					rootNode.ChildNodes.Add(new TreeNode(string.Format("[P] {0} ({1})", mwr.Name, ((MWRCommonTypes.Tasks.MWRFileInfo)mwr).FileSize.ToString())));
				}
				else
				{
					TreeNode newNode = new TreeNode();
					newNode.Text = string.Format("[K] {0} ", mwr.Name);
					FillTree(newNode, mwr as MWRCommonTypes.Tasks.MWRFileDirectory);
					rootNode.ChildNodes.Add(newNode);
				}
			}
		}

		public override string TaskID
		{
			get { return MWRCommonTypes.Tasks.FileManageTask.UniqueID; }
		}

		protected override void UpdateProperties(MWRCommonTypes.Tasks.FileManageTask task)
		{
			task.CurrentOperation = (MWRCommonTypes.Tasks.OperationType)dlOperationType.SelectedIndex;
			task.RequestedObjectPath = tbPath.Text;
			task.DestinationObjectPath = tbDestination.Text;
			task.FileContents = tbContent.Text;
		}

		protected override MWRCommonTypes.Tasks.FileManageTask GetNewTask()
		{
			return new MWRCommonTypes.Tasks.FileManageTask();
		}
	}
}