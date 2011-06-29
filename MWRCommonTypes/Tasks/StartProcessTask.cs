using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MWRCommonTypes.Tasks
{
    public class StartProcessTask : Task
    {
		public static string UniqueID
		{
			get { return "E8939F59-83F3-43c3-9CE1-B08055292603"; }
		}

        public string FilePath { get; set; }

        public StartProcessTask()
            : base(StartProcessTask.UniqueID)
        {
        }

        public override ProcessResult Process()
        {
            ProcessResult res = new ProcessResult();
            try
            {
                System.Diagnostics.Process.Start(FilePath);
                res.ErrorCode = 0;
            }
            catch (Exception exc)
            {
                res.ErrorCode = 5;
                res.ErrorDetails = exc.ToString();
            }
            return res;
        }

        public override void LoadConfig(string configString)
        {
        }

        protected override void InitFromXML(string xmlRequest, string xmlResponse)
        {
            Dictionary<string, string> req = BusinessObject.GetConfigValues(xmlRequest);
            FilePath = req["FilePath"];
        }

        protected override string GetRequestXML()
        {
            return string.Format("<Request><FilePath>{0}</FilePath></Request>", FilePath);
        }

        protected override string GetResponseXML()
        {
            return string.Empty;
        }
    }
}
