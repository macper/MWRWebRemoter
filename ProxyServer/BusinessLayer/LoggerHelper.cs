using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace ProxyServer.BusinessLayer
{
    public static class LoggerHelper
    {
        public static void Log(LogCategories category, LogEventID id, TraceEventType type, string message)
        {
            LogEntry entry = new LogEntry();
            entry.EventId = (int)id;
            entry.Categories.Add(category.ToString());
            entry.Message = message;
            entry.Severity = type;
            Logger.Write(entry);
        }
    }
}
