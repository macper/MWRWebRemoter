namespace WebServiceTester
{
    partial class CreateTaskControl
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
            this.tbGUID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbXMLRequest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GUID:";
            // 
            // tbGUID
            // 
            this.tbGUID.Location = new System.Drawing.Point(95, 14);
            this.tbGUID.Name = "tbGUID";
            this.tbGUID.Size = new System.Drawing.Size(265, 20);
            this.tbGUID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "XMLRequest";
            // 
            // tbXMLRequest
            // 
            this.tbXMLRequest.Location = new System.Drawing.Point(95, 37);
            this.tbXMLRequest.Multiline = true;
            this.tbXMLRequest.Name = "tbXMLRequest";
            this.tbXMLRequest.Size = new System.Drawing.Size(265, 98);
            this.tbXMLRequest.TabIndex = 3;
            // 
            // CreateTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbXMLRequest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbGUID);
            this.Controls.Add(this.label1);
            this.Name = "CreateTaskControl";
            this.Size = new System.Drawing.Size(363, 145);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbGUID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbXMLRequest;
    }
}
