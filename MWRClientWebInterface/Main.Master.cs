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
using MWRCommonTypes;

namespace MWRClientWebInterface
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Machines"] != null)
            {
                string currentGuid = null;
                if (Session["Machine"] != null)
                {
                    currentGuid = (Session["Machine"] as MachineWithPrivilleges).Guid;
                }

                MachineWithPrivilleges[] machines = Session["Machines"] as MachineWithPrivilleges[];
                foreach (MachineWithPrivilleges machine in machines)
                {
                    ddlMachine.Items.Add(new ListItem(machine.Name, machine.Guid));
                    if (machine.Guid == currentGuid)
                    {
                        ddlMachine.SelectedIndex = ddlMachine.Items.Count - 1;
                    }
                }
                if (Session["Machine"] == null)
                {
                    Session["Machine"] = machines[0];
                }
            }
        }

        protected void ddlMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Machine"] = (Session["Machines"] as MachineWithPrivilleges[])[ddlMachine.SelectedIndex];
        }

        public void MessageError(string message)
        {
			lbMessage.Visible = true;
            lbMessage.ForeColor = System.Drawing.Color.Red;
            lbMessage.Text = message;
        }

        public void MessageOK(string message)
        {
			lbMessage.Visible = true;
            lbMessage.ForeColor = System.Drawing.Color.Green;
            lbMessage.Text = message;
        }

    }
}
