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

namespace MWRClientWebInterface.Controls
{
    public partial class CommonDetailsToAdd : System.Web.UI.UserControl
    {
        public DateTime DateToExecute { get { return DateTime.Parse(tbDateExecute.Text); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tbDateExecute.Text = DateTime.Now.ToString();
            }
        }

    }
}