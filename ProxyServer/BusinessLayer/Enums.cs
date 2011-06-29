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
    public enum ResponseCode { OK = 0, ConnectionFailed = 1, AuthorizationFailed = 2, ProxyServerError = 3, IncorrectDataError = 4, ServerError = 5, RequestedObjectNotFound = 6, RequestedObjectIsDisabled = 7 }
    public enum LogEventID { OK = 0, AuthorizationFailed = 1, InternalError = 2, BusinessError = 3 }
    public enum LogCategories { ClientRequest = 1, ServerRequest = 2 }
}
