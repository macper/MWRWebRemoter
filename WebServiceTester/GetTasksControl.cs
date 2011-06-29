using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebServiceTester
{
    public partial class GetTasksControl : UserControl
    {
        public GetTasksControl()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateFrom.Enabled = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dateTo.Enabled = !checkBox2.Checked;
        }
    }
}
