using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace ServerWinFormGUI
{
    public static class LoggerHelper
    {
        public static void Log(string category, TraceEventType type, string message)
        {
            LogEntry entry = new LogEntry();
            entry.Categories.Add(category);
            entry.Message = message;
            entry.Severity = type;
            Logger.Write(entry);
        }
    }
}
