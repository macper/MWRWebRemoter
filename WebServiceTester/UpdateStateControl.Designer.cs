namespace WebServiceTester
{
    partial class UpdateStateControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbGuid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbXML = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GUID:";
            // 
            // tbGuid
            // 
            this.tbGuid.Location = new System.Drawing.Point(71, 9);
            this.tbGuid.Name = "tbGuid";
            this.tbGuid.Size = new System.Drawing.Size(216, 20);
            this.tbGuid.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "XMLReq";
            // 
            // tbXML
            // 
            this.tbXML.Location = new System.Drawing.Point(71, 33);
            this.tbXML.Multiline = true;
            this.tbXML.Name = "tbXML";
            this.tbXML.Size = new System.Drawing.Size(216, 107);
            this.tbXML.TabIndex = 3;
            // 
            // UpdateStateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbXML);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbGuid);
            this.Controls.Add(this.label1);
            this.Name = "UpdateStateControl";
            this.Size = new System.Drawing.Size(300, 149);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbGuid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbXML;
    }
}
