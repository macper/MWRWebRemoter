using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace MWRClientWebInterface
{
    public partial class Tasks : MWRBasePage
    {
        protected MWRCommonTypes.DictionaryEntry[] TaskDictionary
        {
            get
            {
                return Cache["Dict_Tasks"] as MWRCommonTypes.DictionaryEntry [];
            }
            set
            {
                Cache["Dict_Tasks"] = value;
            }
        }

		protected string ActiveTaskGuid
		{
		   get { return ViewState["ActiveTaskGuid"] as string; }
		   set { ViewState["ActiveTaskGuid"] = value; }
		}
	    
        protected void Page_Load(object sender, EventArgs e)
        {
			requiredRole = Privilleges.Task;
            LoadFromDictionary();
            dlSearchResults.SelectedIndexChanged += new EventHandler(dlSearchResults_SelectedIndexChanged);
        }


        void dlSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProxyServer.BusinessLayer.TaskResponse response = ProxyServerInstance.GetTaskRequest(AuthorizationData, int.Parse(dlSearchResults.Items[dlSearchResults.SelectedIndex].Cells[1].Text));
                if (response.ErrorCode == 0)
                {
                    MWRCommonTypes.Machine machine = new MWRCommonTypes.Machine();
                    MWRCommonTypes.TaskFactory taskFactory = new MWRCommonTypes.TaskFactory(TaskDictionary, CurrentMachine);
                    MWRCommonTypes.Task task = taskFactory.Load(response.Task as MWRCommonTypes.IDictObject) as MWRCommonTypes.Task;
					ActiveTaskGuid = task.Guid;
				    InitTaskControls(task, false);
					SetSaveEnabled(false);
                    return;
                }
                AddMessageError("Wystąpił błąd - serwer zwrocił kod odp:" + response.ErrorCode.ToString());
            }
            catch (Exception exc)
            {
                AddMessageError("Wystąpił błąd - " + exc.ToString());
            }
        }

        protected void LoadFromDictionary()
        {
            if (TaskDictionary == null)
            {
                ProxyServer.BusinessLayer.DictionaryResponse response = ProxyServerInstance.GetDictionaryEntries(AuthorizationData, 1, 1);
                if (response.ErrorCode != 0)
                {
                    throw new ApplicationException("Nie udało się pobrać listę zadań, serwer zwrócił błąd - " + response.ErrorCode.ToString());
                }
                TaskDictionary = response.DictionaryTable;
            }
            MWRCommonTypes.DictionaryEntry[] dict = TaskDictionary;
            ddlTaskSelected.Items.Add(new ListItem("dowolne", "0"));
            foreach (MWRCommonTypes.DictionaryEntry entry in dict)
            {
                ddlTaskSelected.Items.Add(new ListItem(entry.Name, entry.Guid));
            }
            if (HttpContext.Current.Request.Form[ddlTaskSelected.UniqueID] != null)
            {
                ddlTaskSelected.SelectedValue = HttpContext.Current.Request.Form[ddlTaskSelected.UniqueID];
            }
			if (!IsPostBack)
			{
				tbDateFrom.Text = DateTime.Now.Date.ToString();
				tbDateTo.Text = DateTime.Now.Date.AddDays(1).ToString();
			}
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ProxyServer.BusinessLayer.GroupTaskResponse response = ProxyServerInstance.GetTasks(AuthorizationData, DateTime.Parse(tbDateFrom.Text), DateTime.Parse(tbDateTo.Text), ddlTaskStates.SelectedValue == "0" ? new int[] { 1, 2, 3, 4, 5 } : new int[] { int.Parse(ddlTaskStates.SelectedValue) }, ddlTaskSelected.SelectedValue, 0, 10);
					FillDataGrid(response);
					AddMessageOK("Odnaleziono " + response.TotalCount + " rekordów");
            }
            catch (Exception exc)
            {
                AddMessageError("Wystąpił błąd podczas wysyłania żądania: " + exc.ToString());
            }
        }

		private void FillDataGrid(ProxyServer.BusinessLayer.GroupTaskResponse response)
		{
			if (response.ErrorCode != 0)
			{
				throw new ApplicationException("Nie udało się zrealizować żądania - serwer zwrócił błąd: " + response.ErrorCode.ToString());
			}

			if (response.Tasks != null && response.Tasks.Length > 0)
			{
				TaskView[] list = new TaskView[response.Tasks.Length];
				for (int i = 0; i < response.Tasks.Length; i++)
				{
					TaskView view = new TaskView();
					view.ID = response.Tasks[i].ID;
					view.Name = response.Tasks[i].Name;
					view.State = response.Tasks[i].State;
					list[i] = view;
				}
				dlSearchResults.DataSource = list;
				dlSearchResults.DataBind();
				dlSearchResults.Visible = true;
				dlSearchResults.VirtualItemCount = response.TotalCount;
			}
			else
			{
				dlSearchResults.Visible = false;
				AddMessageOK("Nie znaleziono żadnych rekordów.");
			}
			SetSaveEnabled(false);
		}

		protected void SetSaveEnabled(bool enabled)
		{
			saveTaskPH.Visible = enabled;
		}


        protected void InitTaskControls(MWRCommonTypes.Task task, bool createMode)
        {
			foreach (Control c in taskDetailsPH.Controls)
			{
				if (c != null)
				{
					c.Visible = false;
				}
			}
		   if (!createMode)
		   {
			  ctlCommonDetails.InitControl(task);
		   }
		   else
		   {
			  ctlCommonDetails.Visible = false;
		   }

		   MWRClientWebInterface.Controls.Tasks.BaseTaskControl ctl = GetActiveTaskControl(ActiveTaskGuid);
		   ctl.Visible = true;
		   ctl.InitControl(createMode, task);
        }

        protected void btCreate_Click(object sender, EventArgs e)
        {
			if (ddlTaskSelected.SelectedIndex == 0)
			{
				AddMessageError("Należy wybrać jakieś zadanie");
				return;
			}
		    ActiveTaskGuid = ddlTaskSelected.SelectedValue;
            InitTaskControls(null, true);
			tbDateToExecute.Text = DateTime.Now.ToString();
			SetSaveEnabled(true);
        }

		protected string GetVirtualPathForTask(string guid)
		{
		   Configuration.ConfSectionTask confSect = (Configuration.ConfSectionTask)ConfigurationManager.GetSection("TaskConfig");
		   return confSect.Tasks[guid.ToUpper()].VirtualPath;
		}

		protected void btSave_Click(object sender, EventArgs e)
		{
			MWRClientWebInterface.Controls.Tasks.BaseTaskControl ctl = GetActiveTaskControl(ActiveTaskGuid);
			try
			{
				ctl.Update(DateTime.Parse(tbDateToExecute.Text));
			}
			catch (Exception exc)
			{
				AddMessageError("Nie udało się dodać zadania - " + exc.ToString());
			}
			AddMessageOK("Zadanie pomyślnie dodane");
		}

		protected override void OnInit(EventArgs e)
		{
		   base.OnInit(e);
		   // Wczytanie kontrolek z taskami
		   MWRClientWebInterface.Configuration.ConfSectionTask taskSect = ConfigurationManager.GetSection("TaskConfig") as MWRClientWebInterface.Configuration.ConfSectionTask;
		   foreach (MWRClientWebInterface.Configuration.ConfigSectionElement el in taskSect.Tasks)
		   {
			   MWRClientWebInterface.Controls.Tasks.BaseTaskControl ctl = (MWRClientWebInterface.Controls.Tasks.BaseTaskControl)LoadControl(GetVirtualPathForTask(el.UniqueID));
			   ctl.Visible = false;
			   ctl.EnableViewState = true;
			   taskDetailsPH.Controls.Add(ctl);
		   }
		}

		protected MWRClientWebInterface.Controls.Tasks.BaseTaskControl GetActiveTaskControl(string guid)
		{
		   foreach (MWRClientWebInterface.Controls.Tasks.BaseTaskControl ctl in taskDetailsPH.Controls)
		   {
			  if (ctl.TaskID.ToUpper() == guid.ToUpper())
			  {
				 return ctl;
			  }
		   }
		   return null;
		}

		protected void dlSearchResults_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			try
			{
				dlSearchResults.CurrentPageIndex = e.NewPageIndex;
				ProxyServer.BusinessLayer.GroupTaskResponse response = ProxyServerInstance.GetTasks(AuthorizationData, DateTime.Parse(tbDateFrom.Text), DateTime.Parse(tbDateTo.Text), ddlTaskStates.SelectedValue == "0" ? new int[] { 1, 2, 3, 4, 5 } : new int[] { int.Parse(ddlTaskStates.SelectedValue) }, ddlTaskSelected.SelectedValue, e.NewPageIndex * 10, 10);
				FillDataGrid(response);
			}
			catch (Exception exc)
			{
				AddMessageError(exc.ToString());
			}
		}
    }

    public class TaskView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
    }
}
