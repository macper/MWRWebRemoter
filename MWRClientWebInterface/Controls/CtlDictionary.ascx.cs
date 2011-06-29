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
using System.Collections.Generic;

namespace MWRClientWebInterface.Controls
{
    public partial class CtlDictionary : BaseControl
    {
        protected List<MWRCommonTypes.DictionaryEntry> Dictionaries
        {
            get
            {
                if (Cache["Dictionary"] == null)
                {
                    Cache["Dictionary"] = GetDictionary();
                }
                return Cache["Dictionary"] as List<MWRCommonTypes.DictionaryEntry>;
            }
        }

        protected List<MWRCommonTypes.DictionaryEntry> GetDictionary()
        {

            ProxyServer.BusinessLayer.DictionaryResponse response = BasePage.ProxyServerInstance.GetDictionaryEntries(BasePage.AuthorizationData, -1, -1);
            if (response.ErrorCode != 0)
            {
                throw new Exception("Odpowiedź serwera: " + response.ErrorCode.ToString() + " - " + response.ErrorDescription);
            }
            return new List<MWRCommonTypes.DictionaryEntry>(response.DictionaryTable);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                foreach (MWRCommonTypes.DictionaryEntry de in Dictionaries)
                {
                    ddlDictionary.Items.Add(new ListItem(de.Name, de.Guid));
                }

            }
        }

        protected void ddlDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        protected void btSubmit_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        protected void UpdateData()
        {
            ProxyServer.BusinessLayer.WebResponse response = BasePage.ProxyServerInstance.UpdateDictionary(BasePage.AuthorizationData, ddlDictionary.SelectedValue, int.Parse(ddlState.SelectedValue), tbConfig.Text);
            if (response.ErrorCode != 0)
            {
                AddMessageError("Nie udało się zapisać, serwer zwrócił kod: " + response.ErrorCode.ToString());
            }
            else
            {
                AddMessageOK("Zapisano pomyślnie");
                Cache.Remove("Dictionary");
            }
        }

        protected void FillData()
        {
            foreach (MWRCommonTypes.DictionaryEntry entry in Dictionaries)
            {
                if (ddlDictionary.SelectedValue == entry.Guid)
                {
                    lbGuid.Text = entry.Guid;
                    lbName.Text = entry.Name;
                    lbType.Text = entry.Type.ToString();
                    ddlState.SelectedValue = entry.Active ? "1" : "0";
                    tbConfig.Text = entry.Config;
                }
            }
        }

        
    }
}