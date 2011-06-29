using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MWRCommonTypes.Tasks
{
    public class KillProcessTask : Task
    {
		public static string UniqueID
		{
			get { return "96100EEE-B2A7-4e9e-8FE8-477ACCC4CC50"; }
		}

        protected int processID;
        public int ProcessID
        {
            get { return processID; }
            set { processID = value; }
        }

        public KillProcessTask()
            : base(KillProcessTask.UniqueID)
        {
        }

        public override ProcessResult Process()
        {
            ProcessResult res = new ProcessResult();

            try
            {
                Process p = System.Diagnostics.Process.GetProcessById(processID);
                if (p != null)
                {
                    p.Kill();
                    res.ErrorCode = 0;
                    return res;
                }
                else
                {
                    res.ErrorCode = 6;
                    res.ErrorDetails = "Nie ma takiego procesu";
                    return res;
                }
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
            processID = int.Parse(req["PID"]);
        }

        protected override string GetRequestXML()
        {
            return string.Format("<Request><PID>{0}</PID></Request>", processID.ToString());
        }

        protected override string GetResponseXML()
        {
            return string.Empty;
        }
    }
}
