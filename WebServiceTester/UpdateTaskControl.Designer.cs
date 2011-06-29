namespace WebServiceTester
{
    partial class UpdateTaskControl
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
            this.tbID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbXML = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbState = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(102, 8);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(100, 20);
            this.tbID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "XMLResponse";
            // 
            // tbXML
            // 
            this.tbXML.Location = new System.Drawing.Point(102, 34);
            this.tbXML.Multiline = true;
            this.tbXML.Name = "tbXML";
            this.tbXML.Size = new System.Drawing.Size(193, 94);
            this.tbXML.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "State:";
            // 
            // tbState
            // 
            this.tbState.Location = new System.Drawing.Point(102, 140);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(100, 20);
            this.tbState.TabIndex = 5;
            this.tbState.Text = "3";
            // 
            // UpdateTaskControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbState);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbXML);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label1);
            this.Name = "UpdateTaskControl";
            this.Size = new System.Drawing.Size(298, 177);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbXML;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbState;
    }
}
