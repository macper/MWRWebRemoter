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
using System.Web.Configuration;

namespace MWRClientWebInterface.Controls
{
    public partial class CtlTaskMakeScreenshoot : BaseTaskControl<MWRCommonTypes.Tasks.MakeScreenshootTask>
    {
        protected override MWRCommonTypes.Tasks.MakeScreenshootTask GetNewTask()
        {
            return new MWRCommonTypes.Tasks.MakeScreenshootTask(); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void UpdateProperties(MWRCommonTypes.Tasks.MakeScreenshootTask task)
        {
        }

        public override void SetControlsDisabled()
        {
            phImage.Visible = true;
        }

        public override void Fill(MWRCommonTypes.Task tsk)
        {
            if (tsk.State == MWRCommonTypes.Enum.TaskState.Completed)
            {
                MWRCommonTypes.Tasks.MakeScreenshootTask scrTsk = tsk as MWRCommonTypes.Tasks.MakeScreenshootTask;
                try
                {
                    if (System.IO.File.Exists(WebConfigurationManager.AppSettings["TempDirPhysicalPath"] + scrTsk.FileName))
                    {
                        imgScreen.ImageUrl = WebConfigurationManager.AppSettings["TempDir"] + "\\" + scrTsk.FileName;
                        return;
                    }
                    MWRCommonTypes.FtpClient ftpClient = new MWRCommonTypes.FtpClient(scrTsk.FTPServerAddress, scrTsk.FTPUser, scrTsk.FTPPassword);
                    ftpClient.ChangeDir(scrTsk.FTPDirectory);
                    ftpClient.Download(scrTsk.FileName, WebConfigurationManager.AppSettings["TempDirPhysicalPath"] + scrTsk.FileName);
                    ftpClient.Close();
                    imgScreen.ImageUrl = WebConfigurationManager.AppSettings["TempDir"] + "\\" + scrTsk.FileName;
                }
                catch (Exception exc)
                {
                    AddMessageError("Nie udało się pobrać obrazka z FTP-a : " + exc.ToString());
                }
            }
            else
            {
                phImage.Visible = false;
            }
        }
    }
}