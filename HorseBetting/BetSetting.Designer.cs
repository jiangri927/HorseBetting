namespace HorseBetting
{
    partial class BetSetting
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
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbFollow = new System.Windows.Forms.RadioButton();
            this.rbReverse = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTime = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account Name:";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(101, 13);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(100, 20);
            this.txtAccountName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bet Type:";
            // 
            // rbFollow
            // 
            this.rbFollow.AutoSize = true;
            this.rbFollow.Location = new System.Drawing.Point(101, 52);
            this.rbFollow.Name = "rbFollow";
            this.rbFollow.Size = new System.Drawing.Size(55, 17);
            this.rbFollow.TabIndex = 2;
            this.rbFollow.TabStop = true;
            this.rbFollow.Text = "Follow";
            this.rbFollow.UseVisualStyleBackColor = true;
            // 
            // rbReverse
            // 
            this.rbReverse.AutoSize = true;
            this.rbReverse.Location = new System.Drawing.Point(101, 85);
            this.rbReverse.Name = "rbReverse";
            this.rbReverse.Size = new System.Drawing.Size(65, 17);
            this.rbReverse.TabIndex = 2;
            this.rbReverse.TabStop = true;
            this.rbReverse.Text = "Reverse";
            this.rbReverse.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bet Type:";
            // 
            // cmbTime
            // 
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime.FormattingEnabled = true;
            this.cmbTime.Location = new System.Drawing.Point(101, 114);
            this.cmbTime.Name = "cmbTime";
            this.cmbTime.Size = new System.Drawing.Size(100, 21);
            this.cmbTime.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(121, 156);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 32);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // BetSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 200);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbTime);
            this.Controls.Add(this.rbReverse);
            this.Controls.Add(this.rbFollow);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BetSetting";
            this.Text = "BetSetting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbFollow;
        private System.Windows.Forms.RadioButton rbReverse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTime;
        private System.Windows.Forms.Button btnOk;
    }
}