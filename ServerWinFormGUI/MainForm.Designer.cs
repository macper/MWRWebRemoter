namespace ServerWinFormGUI
{
    partial class MainForm
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
			this.tbStateText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lbStateSuccess = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lbStateError = new System.Windows.Forms.Label();
			this.btState = new System.Windows.Forms.Button();
			this.btTask = new System.Windows.Forms.Button();
			this.lbTaskFailed = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbTaskSuccesfull = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tbTaskProcessing = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lbStateLoaded = new System.Windows.Forms.Label();
			this.lbTaskLoaded = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lbTaskUpdatedOK = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.lbTaskUpdatedFailed = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(137, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Stan przetwarzania stanów:";
			// 
			// tbStateText
			// 
			this.tbStateText.BackColor = System.Drawing.Color.White;
			this.tbStateText.ForeColor = System.Drawing.Color.Black;
			this.tbStateText.Location = new System.Drawing.Point(155, 6);
			this.tbStateText.Name = "tbStateText";
			this.tbStateText.ReadOnly = true;
			this.tbStateText.Size = new System.Drawing.Size(380, 20);
			this.tbStateText.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Przetworzono prawidłowo:";
			// 
			// lbStateSuccess
			// 
			this.lbStateSuccess.AutoSize = true;
			this.lbStateSuccess.ForeColor = System.Drawing.Color.Green;
			this.lbStateSuccess.Location = new System.Drawing.Point(167, 32);
			this.lbStateSuccess.Name = "lbStateSuccess";
			this.lbStateSuccess.Size = new System.Drawing.Size(13, 13);
			this.lbStateSuccess.TabIndex = 3;
			this.lbStateSuccess.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(113, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Przetworzono błędnie:";
			// 
			// lbStateError
			// 
			this.lbStateError.AutoSize = true;
			this.lbStateError.ForeColor = System.Drawing.Color.Red;
			this.lbStateError.Location = new System.Drawing.Point(167, 55);
			this.lbStateError.Name = "lbStateError";
			this.lbStateError.Size = new System.Drawing.Size(13, 13);
			this.lbStateError.TabIndex = 5;
			this.lbStateError.Text = "0";
			// 
			// btState
			// 
			this.btState.Location = new System.Drawing.Point(452, 32);
			this.btState.Name = "btState";
			this.btState.Size = new System.Drawing.Size(75, 23);
			this.btState.TabIndex = 6;
			this.btState.Text = "Start";
			this.btState.UseVisualStyleBackColor = true;
			this.btState.Click += new System.EventHandler(this.btState_Click);
			// 
			// btTask
			// 
			this.btTask.Location = new System.Drawing.Point(452, 139);
			this.btTask.Name = "btTask";
			this.btTask.Size = new System.Drawing.Size(75, 23);
			this.btTask.TabIndex = 13;
			this.btTask.Text = "Start";
			this.btTask.UseVisualStyleBackColor = true;
			this.btTask.Click += new System.EventHandler(this.btTask_Click);
			// 
			// lbTaskFailed
			// 
			this.lbTaskFailed.AutoSize = true;
			this.lbTaskFailed.ForeColor = System.Drawing.Color.Red;
			this.lbTaskFailed.Location = new System.Drawing.Point(167, 184);
			this.lbTaskFailed.Name = "lbTaskFailed";
			this.lbTaskFailed.Size = new System.Drawing.Size(13, 13);
			this.lbTaskFailed.TabIndex = 12;
			this.lbTaskFailed.Text = "0";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Przetworzono błędnie:";
			// 
			// lbTaskSuccesfull
			// 
			this.lbTaskSuccesfull.AutoSize = true;
			this.lbTaskSuccesfull.ForeColor = System.Drawing.Color.Green;
			this.lbTaskSuccesfull.Location = new System.Drawing.Point(167, 139);
			this.lbTaskSuccesfull.Name = "lbTaskSuccesfull";
			this.lbTaskSuccesfull.Size = new System.Drawing.Size(13, 13);
			this.lbTaskSuccesfull.TabIndex = 10;
			this.lbTaskSuccesfull.Text = "0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 139);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(132, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "Przetworzono prawidłowo:";
			// 
			// tbTaskProcessing
			// 
			this.tbTaskProcessing.BackColor = System.Drawing.Color.White;
			this.tbTaskProcessing.ForeColor = System.Drawing.Color.Black;
			this.tbTaskProcessing.Location = new System.Drawing.Point(155, 113);
			this.tbTaskProcessing.Name = "tbTaskProcessing";
			this.tbTaskProcessing.ReadOnly = true;
			this.tbTaskProcessing.Size = new System.Drawing.Size(380, 20);
			this.tbTaskProcessing.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(12, 116);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(132, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Stan przetwarzania zadań:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(204, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Załadowano:";
			// 
			// lbStateLoaded
			// 
			this.lbStateLoaded.AutoSize = true;
			this.lbStateLoaded.ForeColor = System.Drawing.Color.Green;
			this.lbStateLoaded.Location = new System.Drawing.Point(359, 32);
			this.lbStateLoaded.Name = "lbStateLoaded";
			this.lbStateLoaded.Size = new System.Drawing.Size(13, 13);
			this.lbStateLoaded.TabIndex = 15;
			this.lbStateLoaded.Text = "0";
			// 
			// lbTaskLoaded
			// 
			this.lbTaskLoaded.AutoSize = true;
			this.lbTaskLoaded.ForeColor = System.Drawing.Color.Green;
			this.lbTaskLoaded.Location = new System.Drawing.Point(359, 139);
			this.lbTaskLoaded.Name = "lbTaskLoaded";
			this.lbTaskLoaded.Size = new System.Drawing.Size(13, 13);
			this.lbTaskLoaded.TabIndex = 17;
			this.lbTaskLoaded.Text = "0";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(204, 139);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(71, 13);
			this.label9.TabIndex = 16;
			this.label9.Text = "Załadowano:";
			// 
			// lbTaskUpdatedOK
			// 
			this.lbTaskUpdatedOK.AutoSize = true;
			this.lbTaskUpdatedOK.ForeColor = System.Drawing.Color.Green;
			this.lbTaskUpdatedOK.Location = new System.Drawing.Point(167, 162);
			this.lbTaskUpdatedOK.Name = "lbTaskUpdatedOK";
			this.lbTaskUpdatedOK.Size = new System.Drawing.Size(13, 13);
			this.lbTaskUpdatedOK.TabIndex = 19;
			this.lbTaskUpdatedOK.Text = "0";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 162);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(112, 13);
			this.label10.TabIndex = 18;
			this.label10.Text = "Zapisano prawidłowo:";
			// 
			// lbTaskUpdatedFailed
			// 
			this.lbTaskUpdatedFailed.AutoSize = true;
			this.lbTaskUpdatedFailed.ForeColor = System.Drawing.Color.Red;
			this.lbTaskUpdatedFailed.Location = new System.Drawing.Point(167, 207);
			this.lbTaskUpdatedFailed.Name = "lbTaskUpdatedFailed";
			this.lbTaskUpdatedFailed.Size = new System.Drawing.Size(13, 13);
			this.lbTaskUpdatedFailed.TabIndex = 21;
			this.lbTaskUpdatedFailed.Text = "0";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(12, 207);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(93, 13);
			this.label12.TabIndex = 20;
			this.label12.Text = "Zapisano błędnie:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 289);
			this.Controls.Add(this.lbTaskUpdatedFailed);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.lbTaskUpdatedOK);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.lbTaskLoaded);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.lbStateLoaded);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btTask);
			this.Controls.Add(this.lbTaskFailed);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lbTaskSuccesfull);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.tbTaskProcessing);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.btState);
			this.Controls.Add(this.lbStateError);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbStateSuccess);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbStateText);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "ServerGUI";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStateText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbStateSuccess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbStateError;
        private System.Windows.Forms.Button btState;
        private System.Windows.Forms.Button btTask;
        private System.Windows.Forms.Label lbTaskFailed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbTaskSuccesfull;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTaskProcessing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbStateLoaded;
        private System.Windows.Forms.Label lbTaskLoaded;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbTaskUpdatedOK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbTaskUpdatedFailed;
        private System.Windows.Forms.Label label12;
    }
}

