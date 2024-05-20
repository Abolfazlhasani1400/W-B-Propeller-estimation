namespace HoltropResistance
{
    partial class RequestForm
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
            this.RequestedThrust_TextBox = new System.Windows.Forms.TextBox();
            this.Ok_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RequestedDraft_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RequestedAdvanceSpeed_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "required Thrust (KN)\r\n";
            // 
            // RequestedThrust_TextBox
            // 
            this.RequestedThrust_TextBox.Location = new System.Drawing.Point(205, 16);
            this.RequestedThrust_TextBox.Name = "RequestedThrust_TextBox";
            this.RequestedThrust_TextBox.Size = new System.Drawing.Size(103, 20);
            this.RequestedThrust_TextBox.TabIndex = 0;
            // 
            // Ok_Button
            // 
            this.Ok_Button.Location = new System.Drawing.Point(233, 98);
            this.Ok_Button.Name = "Ok_Button";
            this.Ok_Button.Size = new System.Drawing.Size(75, 23);
            this.Ok_Button.TabIndex = 3;
            this.Ok_Button.Text = "Ok";
            this.Ok_Button.UseVisualStyleBackColor = true;
            this.Ok_Button.Click += new System.EventHandler(this.Ok_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "required Draft (m)\r\n";
            // 
            // RequestedDraft_TextBox
            // 
            this.RequestedDraft_TextBox.Location = new System.Drawing.Point(205, 42);
            this.RequestedDraft_TextBox.Name = "RequestedDraft_TextBox";
            this.RequestedDraft_TextBox.Size = new System.Drawing.Size(103, 20);
            this.RequestedDraft_TextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Advance Speed (m/s)\r\n";
            // 
            // AdvanceSpeed_TextBox
            // 
            this.RequestedAdvanceSpeed_TextBox.Location = new System.Drawing.Point(205, 68);
            this.RequestedAdvanceSpeed_TextBox.Name = "AdvanceSpeed_TextBox";
            this.RequestedAdvanceSpeed_TextBox.Size = new System.Drawing.Size(103, 20);
            this.RequestedAdvanceSpeed_TextBox.TabIndex = 2;
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 133);
            this.ControlBox = false;
            this.Controls.Add(this.Ok_Button);
            this.Controls.Add(this.RequestedAdvanceSpeed_TextBox);
            this.Controls.Add(this.RequestedDraft_TextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RequestedThrust_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RequetThrust";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Ok_Button;
        public System.Windows.Forms.TextBox RequestedThrust_TextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox RequestedDraft_TextBox;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox RequestedAdvanceSpeed_TextBox;
    }
}