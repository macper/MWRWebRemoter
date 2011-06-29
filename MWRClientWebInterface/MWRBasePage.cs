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
using System.Security.Principal;

namespace MWRClientWebInterface
{
    public class MWRBasePage : Page
    {
		protected Label lbMessage;
        protected MWRCommonTypes.MachineWithPrivilleges [] machines;
        public MWRCommonTypes.MachineWithPrivilleges [] Machines
        {
            get { return machines; }
        }
        protected MWRCommonTypes.User loggedUser;
        public MWRCommonTypes.User LoggedUser
        {
            get { return loggedUser; }
        }

        public MWRCommonTypes.MachineWithPrivilleges CurrentMachine
        {
            get { return Session["Machine"] as MWRCommonTypes.MachineWithPrivilleges; }
        }

        protected Privilleges requiredRole = Privilleges.Nothing;
        protected MWRClientLib.ClientProxy proxyServer;
        protected ProxyServer.BusinessLayer.ClientAuthStruct authorizationData;

        public MWRClientLib.ClientProxy ProxyServerInstance
        {
            get
            {
                if (proxyServer == null)
                {
                    proxyServer = new MWRClientLib.ClientProxy(new MWRClientLib.ClientInterface.ClientInterfaceSoapClient("ClientInterfaceSoap"));
                }
                return proxyServer;
            }
        }

        public ProxyServer.BusinessLayer.ClientAuthStruct AuthorizationData
        {
            get
            {
                if (authorizationData == null)
                {
                    authorizationData = new ProxyServer.BusinessLayer.ClientAuthStruct();
                    authorizationData.UserName = LoggedUser.Name;
                    authorizationData.Password = LoggedUser.Password; 
                }
                if (CurrentMachine != null)
                {
                    authorizationData.MachineGuid = CurrentMachine.Guid;
                }
                return authorizationData;
            }
        }

        protected void Authenticate()
        {
            if (Session["LoggedUser"] != null)
            {
                loggedUser = Session["LoggedUser"] as MWRCommonTypes.User;
                if (Session["Machines"] != null)
                {
                    machines = Session["Machines"] as MWRCommonTypes.MachineWithPrivilleges[];
                }
            }
        }

        protected bool Authorize()
        {
            if (requiredRole == Privilleges.Nothing)
            {
                return true;
            }
            if (CurrentMachine != null)
            {
                foreach (int priv in CurrentMachine.Privilleges)
                {
                    if ((priv & (int)requiredRole) == (int)requiredRole)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected override void  OnInit(EventArgs e)
        {
 	        base.OnInit(e);
            Authenticate();
            if (LoggedUser == null)
            {
                Server.Transfer("Logon.aspx");
            }
            if (!Authorize())
            {
                Server.Transfer("PermissionDenied.aspx");
            }
            Error += new EventHandler(MWRBasePage_Error);
        }

        void MWRBasePage_Error(object sender, EventArgs e)
        {
            Main masterPage = Master as Main;
            masterPage.MessageError(e.ToString());
        }

		protected void AddMessageError(string message)
		{
		   ((Main)Master).MessageError(message);
		}

		protected void AddMessageOK(string message)
		{
		   ((Main)Master).MessageOK(message);
		}
    }

    public enum Privilleges { Nothing = 0, State = 1, Task = 2, Admin = 4 }
}
