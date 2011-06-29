using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Security.Principal;
using System.Collections.Generic;

namespace MWRClientWebInterface
{
    public partial class Logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginForm_Authenticate(object sender, AuthenticateEventArgs e)
        {
            MWRClientLib.ClientProxy proxy = new MWRClientLib.ClientProxy(new MWRClientLib.ClientInterface.ClientInterfaceSoapClient("ClientInterfaceSoap"));
            ProxyServer.BusinessLayer.ClientAuthStruct auth = new ProxyServer.BusinessLayer.ClientAuthStruct();
            auth.UserName = LoginForm.UserName;
            auth.Password = LoginForm.Password;
            try 
            {
                ProxyServer.BusinessLayer.UserDataResponse response = proxy.GetUserData(auth);
                if (response.ErrorCode == 0)
                {
                    e.Authenticated = true;
                    List<MWRCommonTypes.MachineWithPrivilleges> machines = new List<MWRCommonTypes.MachineWithPrivilleges>();

                    foreach (MWRCommonTypes.MachineWithPrivilleges mach in response.MachinesList)
                    {
                        machines.Add(mach);
                    }

                    MWRCommonTypes.User loggedUser = new MWRCommonTypes.User();
                    loggedUser = response.User;
                    loggedUser.Password = auth.Password;
                    Session["LoggedUser"] = loggedUser;
                    Session["Machines"] = machines.ToArray();
                    Response.Redirect("Default.aspx");
                }
                LoginForm.FailureText = "Nie udało się zalogować. Kod błędu " + response.ErrorCode.ToString();
            }
            catch (Exception exc)
            {
                LoginForm.FailureText = "Nie udało się zalogować. Wystąpił wewnętrzny błąd serwera." + exc.ToString(); 
            }
        }

        
    }
}
