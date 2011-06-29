namespace WebServiceTester
{
    partial class GetTasksControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.tbStates = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(12, 13);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(200, 20);
            this.dateFrom.TabIndex = 0;
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(12, 39);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(200, 20);
            this.dateTo.TabIndex = 1;
            // 
            // tbStates
            // 
            this.tbStates.Location = new System.Drawing.Point(12, 65);
            this.tbStates.Name = "tbStates";
            this.tbStates.Size = new System.Drawing.Size(200, 20);
            this.tbStates.TabIndex = 2;
            this.tbStates.Text = "1,2,3,4";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(218, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(79, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "nie dotyczy";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(218, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(79, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "nie dotyczy";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // GetTasksControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tbStates);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Name = "GetTasksControl";
            this.Size = new System.Drawing.Size(300, 97);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.TextBox tbStates;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}
