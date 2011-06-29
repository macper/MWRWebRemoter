namespace WebServiceTester
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMachine = new System.Windows.Forms.TextBox();
            this.customParameters = new System.Windows.Forms.Panel();
            this.callButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbErrorCode = new System.Windows.Forms.Label();
            this.tbErrorDetails = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Funkcja:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(66, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(303, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password:";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(88, 34);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(162, 20);
            this.tbUsername.TabIndex = 4;
            this.tbUsername.Text = "tester";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(88, 55);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(162, 20);
            this.tbPassword.TabIndex = 5;
            this.tbPassword.Text = "test";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Machine";
            // 
            // tbMachine
            // 
            this.tbMachine.Location = new System.Drawing.Point(88, 76);
            this.tbMachine.Name = "tbMachine";
            this.tbMachine.Size = new System.Drawing.Size(162, 20);
            this.tbMachine.TabIndex = 7;
            this.tbMachine.Text = "5E5BDE80-F5F8-4fb1-B89E-993B3D999695";
            // 
            // customParameters
            // 
            this.customParameters.Location = new System.Drawing.Point(15, 102);
            this.customParameters.Name = "customParameters";
            this.customParameters.Size = new System.Drawing.Size(354, 167);
            this.customParameters.TabIndex = 8;
            // 
            // callButton
            // 
            this.callButton.Location = new System.Drawing.Point(156, 421);
            this.callButton.Name = "callButton";
            this.callButton.Size = new System.Drawing.Size(75, 23);
            this.callButton.TabIndex = 9;
            this.callButton.Text = "Wywołaj";
            this.callButton.UseVisualStyleBackColor = true;
            this.callButton.Click += new System.EventHandler(this.callButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "ErrorCode:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "ErrorDescription";
            // 
            // lbErrorCode
            // 
            this.lbErrorCode.AutoSize = true;
            this.lbErrorCode.Location = new System.Drawing.Point(153, 284);
            this.lbErrorCode.Name = "lbErrorCode";
            this.lbErrorCode.Size = new System.Drawing.Size(40, 13);
            this.lbErrorCode.TabIndex = 12;
            this.lbErrorCode.Text = "<brak>";
            // 
            // tbErrorDetails
            // 
            this.tbErrorDetails.Location = new System.Drawing.Point(15, 323);
            this.tbErrorDetails.Multiline = true;
            this.tbErrorDetails.Name = "tbErrorDetails";
            this.tbErrorDetails.Size = new System.Drawing.Size(354, 92);
            this.tbErrorDetails.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 456);
            this.Controls.Add(this.tbErrorDetails);
            this.Controls.Add(this.lbErrorCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.callButton);
            this.Controls.Add(this.customParameters);
            this.Controls.Add(this.tbMachine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "WebServiceTester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMachine;
        private System.Windows.Forms.Panel customParameters;
        private System.Windows.Forms.Button callButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbErrorCode;
        private System.Windows.Forms.TextBox tbErrorDetails;
    }
}

