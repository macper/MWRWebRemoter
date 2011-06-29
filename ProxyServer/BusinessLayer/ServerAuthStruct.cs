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

namespace ProxyServer.BusinessLayer
{
    public class ServerAuthStruct
    {
        public string MachineGuid;
        public string AuthToken;
    }
}
