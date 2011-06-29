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

namespace MWRClientWebInterface.Controls
{
    public class BaseControl : System.Web.UI.UserControl
    {
        protected MWRBasePage BasePage { get { return Page as MWRBasePage; } }
        protected void AddMessageOK(string message)
        {
            ((Main)BasePage.Master).MessageOK(message);
        }

        protected void AddMessageError(string message)
        {
            ((Main)BasePage.Master).MessageError(message);
        }
    }
}
