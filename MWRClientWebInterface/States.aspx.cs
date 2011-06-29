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
using System.Web.Caching;
using System.Xml.Linq;
using MWRCommonTypes;
using ProxyServer;

namespace MWRClientWebInterface
{
    public partial class States : MWRBasePage
    {
		 protected MWRCommonTypes.DictionaryEntry[] dictTable;

		 protected void Page_Load(object sender, EventArgs e)
		 {
			 requiredRole = Privilleges.State;
			 LoadStatesFromDictionary();
		 }

		 protected void LoadStatesFromDictionary()
		 {
			 if (Cache["Dict_States"] == null)
			 {
				 ProxyServer.BusinessLayer.DictionaryResponse response = ProxyServerInstance.GetDictionaryEntries(AuthorizationData, (int)MWRCommonTypes.Enum.ObjectType.State, 1);
				 if (response.ErrorCode != 0)
				 {
					 throw new ApplicationException("Nie udało się pobrać listy statesów");
				 }
				 Cache.Add("Dict_States", response.DictionaryTable, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0), System.Web.Caching.CacheItemPriority.Default, null);
			 }
			 dictTable = Cache["Dict_States"] as MWRCommonTypes.DictionaryEntry[];
			 foreach (MWRCommonTypes.DictionaryEntry entry in dictTable)
			 {
				 ddlStates.Items.Add(new ListItem(entry.Name, entry.Guid));
			 }
			 if (HttpContext.Current.Request.Form[ddlStates.UniqueID] != null)
			 {
				 ddlStates.SelectedValue = HttpContext.Current.Request.Form[ddlStates.UniqueID];
			 }
		 }

		 protected void Button1_Click(object sender, EventArgs e)
		 {
			 try
			 {
				 ProxyServer.BusinessLayer.StateResponse response = ProxyServerInstance.GetStateRequest(AuthorizationData, ddlStates.SelectedValue, DateTime.Now);
				 if (response.ErrorCode != 0)
				 {
					 throw new Exception("Serwer zwrócił kod błędu: " + response.ErrorCode.ToString());
				 }
				 UpdateData(response.State);
				 AddMessageOK("Wczytano pomyślnie");
			 }
			 catch (Exception exc)
			 {
				 AddMessageError("Wystąpił błąd - " + exc.ToString());
			 }
		 }

		 protected void UpdateData(MWRCommonTypes.StateStruct stateStruct)
		 {
			 MWRCommonTypes.StateFactory stateFactory = new MWRCommonTypes.StateFactory(dictTable, CurrentMachine);
			 MWRCommonTypes.State state = stateFactory.Load(stateStruct) as MWRCommonTypes.State;
			 state.Init(stateStruct.XmlInfo);
			 lbDateLoaded.Text = state.RegisteredDate.ToString();
			 Controls.States.BaseStateControl ctl = LoadControl(GetVirtualPathForControl(state.Guid)) as Controls.States.BaseStateControl;
			 ctl.InitControl(state);
			 stateControlPH.Controls.Add(ctl);
		 }

		 protected string GetVirtualPathForControl(string guid)
		 {
			Configuration.ConfSectionState confState = (Configuration.ConfSectionState)System.Configuration.ConfigurationManager.GetSection("StateConfig");
			 return confState.States[guid].VirtualPath;
		 }

        
    }
}
