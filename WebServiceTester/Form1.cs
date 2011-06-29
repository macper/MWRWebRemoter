using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WebServiceTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "GetStateRequest", "GetTaskRequest", "CreateTaskRequest", "DeleteTaskRequest", "UpdateStateRequest", "UpdateTaskRequest", "GetTasks", "GetActiveStates", "GetActiveTasks", "GetTasksForClient" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            customParameters.Controls.Clear();
            switch (comboBox1.SelectedItem.ToString())
            {
                case "GetStateRequest":
                    customParameters.Controls.Add(new GetStateRequestControl());
                    break;

                case "GetTaskRequest":
                    customParameters.Controls.Add(new GetTaskControl());
                    break;

                case "CreateTaskRequest":
                    customParameters.Controls.Add(new CreateTaskControl());
                    break;

                case "DeleteTaskRequest":
                    customParameters.Controls.Add(new GetTaskControl());
                    break;

                case "UpdateStateRequest":
                    customParameters.Controls.Add(new UpdateStateControl());
                    break;

                case "UpdateTaskRequest":
                    customParameters.Controls.Add(new UpdateTaskControl());
                    break;

                case "GetTasksForClient":
                    customParameters.Controls.Add(new GetTasksControl());
                    break;

                case "GetTasks":
                case "GetActiveStates":
                case "GetActiveTasks":
                    break;
            }
        }

        private void callButton_Click(object sender, EventArgs e)
        {
            ClientInterface.ClientInterfaceSoapClient Client = new WebServiceTester.ClientInterface.ClientInterfaceSoapClient();
            ServerInterface.ServerInterfaceSoapClient Server = new WebServiceTester.ServerInterface.ServerInterfaceSoapClient();

            switch (comboBox1.SelectedItem.ToString())
            {
                case "GetStateRequest":
                    TextBox textGuid = (TextBox)customParameters.Controls[0].Controls[0];
                    ClientInterface.StateResponse response = Client.GetStateRequest(GetAuthData(), textGuid.Text, DateTime.Now);
                    lbErrorCode.Text = response.ErrorCode.ToString();
                    tbErrorDetails.Text = response.ErrorDescription;
                    break;

                case "GetTaskRequest":
                    TextBox textID = (TextBox)customParameters.Controls[0].Controls[0];
                    ClientInterface.TaskResponse TaskResponse = Client.GetTaskRequest(GetAuthData(), int.Parse(textID.Text));
                    lbErrorCode.Text = TaskResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = TaskResponse.ErrorDescription;
                    break;

                case "CreateTaskRequest":
                    textGuid = GetTextBox("tbGUID", customParameters);
                    TextBox textXml = GetTextBox("tbXMLRequest", customParameters);
                    ClientInterface.TaskStruct Task2 = new WebServiceTester.ClientInterface.TaskStruct();
                    Task2.Guid = textGuid.Text;
                    Task2.XmlRequest = textXml.Text;
                    Task2.User = 1;
                    Task2.Machine = tbMachine.Text;
                    Task2.State = 1;
                    TaskResponse = Client.CreateTaskRequest(GetAuthData(), Task2);
                    lbErrorCode.Text = TaskResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = TaskResponse.ErrorDescription;
                    break;

                case "DeleteTaskRequest":
                    textID = GetTextBox("tbTaskID", customParameters);
                    ClientInterface.WebResponse WebResponse = Client.DeleteTaskRequest(GetAuthData(), int.Parse(textID.Text));
                    lbErrorCode.Text = WebResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = WebResponse.ErrorDescription;
                    break;

                case "UpdateStateRequest":
                    textGuid = GetTextBox("tbGuid", customParameters);
                    textXml = GetTextBox("tbXML", customParameters);
                    ServerInterface.WebResponse ServerResponse = Server.UpdateStateRequest(GetServerAuthData(), textGuid.Text, textXml.Text);
                    lbErrorCode.Text = ServerResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = ServerResponse.ErrorDescription;
                    break;

                case "UpdateTaskRequest":
                    textID = GetTextBox("tbID", customParameters);
                    textXml = GetTextBox("tbXML", customParameters);
                    ServerResponse = Server.UpdateTaskRequest(GetServerAuthData(),int.Parse(textID.Text),int.Parse(GetTextBox("tbState", customParameters).Text), textXml.Text);
                    lbErrorCode.Text = ServerResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = ServerResponse.ErrorDescription;
                    break;

                case "GetTasks":
                    ServerInterface.GroupTaskResponse groupResp = Server.GetTasks(GetServerAuthData());
                    lbErrorCode.Text = groupResp.ErrorCode.ToString();
                    tbErrorDetails.Text = groupResp.ErrorDescription;
                    break;

                case "GetActiveStates":
                    ServerInterface.DictionaryResponse DictResponse = Server.GetActiveStates(GetServerAuthData());
                    lbErrorCode.Text = DictResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = DictResponse.ErrorDescription;
                    break;

                case "GetActiveTasks":
                    DictResponse = Server.GetActiveTasks(GetServerAuthData());
                    lbErrorCode.Text = DictResponse.ErrorCode.ToString();
                    tbErrorDetails.Text = DictResponse.ErrorDescription;
                    break;

                case "GetTasksForClient":
                    DateTime from = DateTime.MinValue;
                    DateTime to = DateTime.MinValue;
                    if (customParameters.Controls[0].Controls[3].Enabled)
                    {
                        from = ((DateTimePicker)customParameters.Controls[0].Controls[3]).Value;
                    }
                    if (customParameters.Controls[0].Controls[4].Enabled)
                    {
                        to = ((DateTimePicker)customParameters.Controls[0].Controls[4]).Value;
                    }

                    int [] states;
                    if (((TextBox)customParameters.Controls[0].Controls[2]).Text != string.Empty)
                    {
                        string[] st = ((TextBox)customParameters.Controls[0].Controls[2]).Text.Split(',');
                        states = new int[st.Length];
                        for (int i = 0; i < st.Length; i++)
                        {
                            states[i] = int.Parse(st[i]);
                        }
                    }
                    else
                    {
                        states = new int[0];
                    }
                    WebServiceTester.ClientInterface.ArrayOfInt proxySt = new WebServiceTester.ClientInterface.ArrayOfInt();
                    proxySt.AddRange(states);
                    ClientInterface.GroupTaskResponse resp = Client.GetTasks(GetAuthData(), from, to, proxySt, null);
                    lbErrorCode.Text = resp.ErrorCode.ToString();
                    tbErrorDetails.Text = resp.ErrorDescription;
                    break;
            }
        }

        private TextBox GetTextBox(string name, Control c)
        {
            foreach (Control ctl in c.Controls[0].Controls)
            {
                if (ctl is TextBox)
                {
                    if (ctl.Name == name)
                    {
                        return (TextBox)ctl;
                    }
                }
            }
            return null;
        }

        private ClientInterface.ClientAuthStruct GetAuthData()
        {
            ClientInterface.ClientAuthStruct auth = new WebServiceTester.ClientInterface.ClientAuthStruct();
            auth.UserName = tbUsername.Text;
            auth.Password = tbPassword.Text;
            auth.MachineGuid = tbMachine.Text;
            return auth;
        }

        private ServerInterface.ServerAuthStruct GetServerAuthData()
        {
            ServerInterface.ServerAuthStruct auth = new WebServiceTester.ServerInterface.ServerAuthStruct();
            auth.MachineGuid = tbMachine.Text;
            auth.AuthToken = tbUsername.Text;
            return auth;
        }
    }
}
