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
    public partial class CtlTaskSpecific : BaseControl
    {
        
        protected BaseTaskControl activeControl;

        protected bool createMode
        {
            get { return hdCreateMode.Value == "1"; }
            set { hdCreateMode.Value = value ? "1" : "0"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (hdTaskGuid.Value != string.Empty)
            {
                InitControl(createMode, hdTaskGuid.Value);
            }
        }

        public void InitControl(MWRCommonTypes.Task tsk)
        {
            InitControl(false, tsk.Guid);
            ctlCommonTaskDetails.InitControl(tsk);
            activeControl.InitControl(false, tsk);
        }

        public void InitControl(bool createMode, string taskGuid)
        {
            DisableControls();
            this.createMode = createMode;
            ctlCommonTaskDetails.Visible = !createMode;
            activeControl = FindProperControl(taskGuid);
            activeControl.Visible = true;
            if (createMode)
            {
                ctlCommonDetailsToAdd.Visible = true;
                btRefresh.Visible = false;
                btSave.Visible = true;
            }
            else
            {
                ctlCommonDetailsToAdd.Visible = false;
                btRefresh.Visible = true;
                btSave.Visible = false;
            }
            hdTaskGuid.Value = taskGuid;
        }

        public void Update()
        {
            if (!createMode)
            {
                throw new Exception("Tryb do przeglądania - update niedozwolone!");
            }
            try
            {
                activeControl = FindActiveControl();
                activeControl.Update(ctlCommonDetailsToAdd.DateToExecute);
                AddMessageOK("Dane zostały zapisane");
            }
            catch (Exception exc)
            {
                AddMessageError("Wystąpił błąd podczas próby Update - " + exc.ToString());
            }

        }

        protected void Refresh()
        {
            
        }

        protected void DisableControls()
        {
            ctlTskKillProcess.Visible = false;
            ctlTskRunProcess.Visible = false;
            ctlTskMakeScreenShoot.Visible = false;
        }

        protected BaseTaskControl FindProperControl(string Guid)
        {
            switch (Guid.ToUpper())
            {
                case "96100EEE-B2A7-4E9E-8FE8-477ACCC4CC50":
                    return ctlTskKillProcess;

                case "E8939F59-83F3-43C3-9CE1-B08055292603":
                    return ctlTskRunProcess;

                case "7897D173-796A-4C2A-91FD-9CB8ACF54062":
                    return ctlTskMakeScreenShoot;

                default:
                    throw new NotImplementedException("Nie odnaleziono odpowiedniej kontrolki dla tego taska - " + Guid.ToUpper());
            }
        }

        protected BaseTaskControl FindActiveControl()
        {
            foreach (Control c in this.Controls)
            {
                BaseTaskControl bTc = c as BaseTaskControl;
                if (bTc != null &&  bTc.Visible)
                {
                    return bTc;
                }
            }
            return null;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            Update();
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}