using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MWRCommonTypes.Tasks
{
    public class MakeScreenshootTask : Task
    {
		public static string UniqueID
		{
			get { return "7897D173-796A-4c2a-91FD-9CB8ACF54062"; }
		}

        public string FileName { get; set; }
		  public FtpSettings FTPSettings { get; set; }

        public MakeScreenshootTask()
            : base(MakeScreenshootTask.UniqueID)
        {
        }

        public override ProcessResult Process()
        {
            ProcessResult res = new ProcessResult();
            res.ErrorCode = 0;
            Bitmap bmp = null;
            Graphics graph = null;
            FtpClient ftpClient = null;
            try
            {
                bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
                graph = Graphics.FromImage(bmp);
                graph.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                FileName = string.Format("zrzut_{0}-{1}-{2}_{3}.jpg", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Ticks.ToString());
                bmp.Save(FileName, ImageFormat.Jpeg);
					 ftpClient = new FtpClient(FTPSettings.ServerAddress, FTPSettings.User, FTPSettings.Password);
					 ftpClient.ChangeDir(FTPSettings.Directory);
                ftpClient.Upload(FileName);
                System.IO.File.Delete(FileName);
                
            }
            catch (Exception exc)
            {
                res.ErrorCode = 5;
                res.ErrorDetails = exc.ToString();
            }
            finally
            {
                if (ftpClient != null)
                ftpClient.Close();
                if (graph != null)
                graph.Dispose();
                if (bmp != null)
                bmp.Dispose();
            }
            return res;
        }

        public override void LoadConfig(string configString)
        {
            Dictionary<string, string> config = BusinessObject.GetConfigValues(configString);
				FTPSettings = new FtpSettings();
				FTPSettings.ServerAddress = config["FTPServer"];
				FTPSettings.User = config["FTPUser"];
				FTPSettings.Password = config["FTPPassword"];
				FTPSettings.Directory = config["FTPDirectory"];
        }

        protected override void InitFromXML(string xmlRequest, string xmlResponse)
        {
            if (xmlResponse == null || xmlResponse == string.Empty)
            {
                return;
            }

            Dictionary<string, string> resp = BusinessObject.GetConfigValues(xmlResponse);
            if (resp.ContainsKey("FileName"))
            {
                FileName = resp["FileName"];
            }
        }

        protected override string GetRequestXML()
        {
            return string.Empty;
        }

        protected override string GetResponseXML()
        {
            return string.Format("<Response><FileName>{0}</FileName></Response>", FileName);
        }
    }
}
