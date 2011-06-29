using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;

namespace MWRCommonTypes.States
{
    public class RefreshProcessListState : State
    {
        Process[] activeProcesses;
        public ProcessInfo[] Processes { get; set; }

        protected override string GetInformationXML()
        {
            StringBuilder strBld = new StringBuilder();
            strBld.Append("<ProcessStateInfo>");
            if (activeProcesses != null)
            {
                if (activeProcesses.Length > 0)
                {
                    
                    foreach (Process p in activeProcesses)
                    {
                        ProcessInfo pInfo = new ProcessInfo();
                        pInfo.Name = p.ProcessName;
                        pInfo.MemoryUsed = p.PagedMemorySize64;
                        pInfo.PID = p.Id;
                        strBld.Append(pInfo.ToXML());
                    }
                }
            }
            strBld.Append("</ProcessStateInfo>");
            return strBld.ToString();
        }

        public override void Init(string xmlInformation)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<ProcessInfo> list = new List<ProcessInfo>();
            xmlDoc.LoadXml(xmlInformation);
            foreach (XmlNode node in xmlDoc.ChildNodes[0].ChildNodes)
            {
                ProcessInfo pInfo = new ProcessInfo();
                pInfo.PID = int.Parse(node.Attributes["ID"].Value);
                pInfo.Name = node.Attributes["Name"].Value;
                pInfo.MemoryUsed = long.Parse(node.Attributes["Memory"].Value);
                list.Add(pInfo);
            }
            Processes = list.ToArray();
        }

        public override ProcessResult Process()
        {
            ProcessResult result = new ProcessResult();
            try
            {
                activeProcesses = System.Diagnostics.Process.GetProcesses();
            }
            catch (Exception exc)
            {
                result.ErrorCode = 5;
                result.ErrorDetails = exc.ToString();
            }
            return result;
        }

        public override void LoadConfig(string configString)
        {
        }

        public RefreshProcessListState()
            : base("19C690FD-F51C-4831-88DD-F4BF798D3B68")
        { }

        public class ProcessInfo
        {
            public int PID { get; set;}
            public string Name { get; set; }
            public long MemoryUsed { get; set; }

            public string ToXML()
            {
                return string.Format("<Process ID=\"{0}\" Name=\"{1}\" Memory=\"{2}\" />", PID.ToString(), Name, MemoryUsed.ToString());
            }
        }

    }
}
