namespace HorseBetting
{
    partial class CitiBetBot
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.spContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tblBet = new System.Windows.Forms.DataGridView();
            this.cbPlayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbRaceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbStadium = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbRace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbHorse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbWin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbBetTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtLog = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spContainer)).BeginInit();
            this.spContainer.Panel1.SuspendLayout();
            this.spContainer.Panel2.SuspendLayout();
            this.spContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblBet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(702, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 32);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(788, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 32);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(874, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(80, 32);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "&Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // spContainer
            // 
            this.spContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spContainer.IsSplitterFixed = true;
            this.spContainer.Location = new System.Drawing.Point(12, 53);
            this.spContainer.Name = "spContainer";
            this.spContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spContainer.Panel1
            // 
            this.spContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // spContainer.Panel2
            // 
            this.spContainer.Panel2.Controls.Add(this.groupBox2);
            this.spContainer.Size = new System.Drawing.Size(942, 596);
            this.spContainer.SplitterDistance = 298;
            this.spContainer.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tblBet);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(942, 298);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bet Result";
            // 
            // tblBet
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblBet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tblBet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblBet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbPlayer,
            this.cbRaceDate,
            this.cbCountry,
            this.cbStadium,
            this.cbRace,
            this.cbHorse,
            this.cbWin,
            this.cbPlace,
            this.cbLimit,
            this.cbType,
            this.cbBetTime});
            this.tblBet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBet.EnableHeadersVisualStyles = false;
            this.tblBet.Location = new System.Drawing.Point(3, 16);
            this.tblBet.Name = "tblBet";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tblBet.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.tblBet.RowHeadersVisible = false;
            this.tblBet.Size = new System.Drawing.Size(936, 279);
            this.tblBet.TabIndex = 3;
            // 
            // cbPlayer
            // 
            this.cbPlayer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cbPlayer.DataPropertyName = "player";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbPlayer.DefaultCellStyle = dataGridViewCellStyle2;
            this.cbPlayer.FillWeight = 80F;
            this.cbPlayer.HeaderText = "Player";
            this.cbPlayer.Name = "cbPlayer";
            this.cbPlayer.ReadOnly = true;
            this.cbPlayer.Width = 61;
            // 
            // cbRaceDate
            // 
            this.cbRaceDate.DataPropertyName = "strDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbRaceDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.cbRaceDate.FillWeight = 90F;
            this.cbRaceDate.HeaderText = "RaceDate";
            this.cbRaceDate.Name = "cbRaceDate";
            this.cbRaceDate.ReadOnly = true;
            this.cbRaceDate.Width = 90;
            // 
            // cbCountry
            // 
            this.cbCountry.DataPropertyName = "country";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbCountry.DefaultCellStyle = dataGridViewCellStyle4;
            this.cbCountry.HeaderText = "Country";
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.ReadOnly = true;
            // 
            // cbStadium
            // 
            this.cbStadium.DataPropertyName = "location";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbStadium.DefaultCellStyle = dataGridViewCellStyle5;
            this.cbStadium.HeaderText = "Stadium";
            this.cbStadium.Name = "cbStadium";
            this.cbStadium.ReadOnly = true;
            this.cbStadium.Width = 70;
            // 
            // cbRace
            // 
            this.cbRace.DataPropertyName = "race";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbRace.DefaultCellStyle = dataGridViewCellStyle6;
            this.cbRace.HeaderText = "Race";
            this.cbRace.Name = "cbRace";
            this.cbRace.ReadOnly = true;
            this.cbRace.Width = 40;
            // 
            // cbHorse
            // 
            this.cbHorse.DataPropertyName = "horse";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbHorse.DefaultCellStyle = dataGridViewCellStyle7;
            this.cbHorse.FillWeight = 80F;
            this.cbHorse.HeaderText = "Horse";
            this.cbHorse.Name = "cbHorse";
            this.cbHorse.ReadOnly = true;
            this.cbHorse.Width = 80;
            // 
            // cbWin
            // 
            this.cbWin.DataPropertyName = "win";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbWin.DefaultCellStyle = dataGridViewCellStyle8;
            this.cbWin.FillWeight = 60F;
            this.cbWin.HeaderText = "Win";
            this.cbWin.Name = "cbWin";
            this.cbWin.ReadOnly = true;
            this.cbWin.Width = 60;
            // 
            // cbPlace
            // 
            this.cbPlace.DataPropertyName = "place";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbPlace.DefaultCellStyle = dataGridViewCellStyle9;
            this.cbPlace.FillWeight = 60F;
            this.cbPlace.HeaderText = "Place";
            this.cbPlace.Name = "cbPlace";
            this.cbPlace.ReadOnly = true;
            this.cbPlace.Width = 60;
            // 
            // cbLimit
            // 
            this.cbLimit.DataPropertyName = "limit";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbLimit.DefaultCellStyle = dataGridViewCellStyle10;
            this.cbLimit.FillWeight = 80F;
            this.cbLimit.HeaderText = "Limit";
            this.cbLimit.Name = "cbLimit";
            this.cbLimit.ReadOnly = true;
            this.cbLimit.Width = 80;
            // 
            // cbType
            // 
            this.cbType.DataPropertyName = "type";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbType.DefaultCellStyle = dataGridViewCellStyle11;
            this.cbType.HeaderText = "Type";
            this.cbType.Name = "cbType";
            this.cbType.ReadOnly = true;
            this.cbType.Width = 56;
            // 
            // cbBetTime
            // 
            this.cbBetTime.DataPropertyName = "bettime";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cbBetTime.DefaultCellStyle = dataGridViewCellStyle12;
            this.cbBetTime.FillWeight = 150F;
            this.cbBetTime.HeaderText = "BetTime";
            this.cbBetTime.Name = "cbBetTime";
            this.cbBetTime.ReadOnly = true;
            this.cbBetTime.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtLog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(942, 294);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs:";
            // 
            // rtLog
            // 
            this.rtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtLog.Location = new System.Drawing.Point(3, 16);
            this.rtLog.Name = "rtLog";
            this.rtLog.ReadOnly = true;
            this.rtLog.Size = new System.Drawing.Size(936, 275);
            this.rtLog.TabIndex = 0;
            this.rtLog.Text = "";
            // 
            // CitiBetBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 661);
            this.Controls.Add(this.spContainer);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "CitiBetBot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CitiBetBot 1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CitiBetBot_FormClosing);
            this.Load += new System.EventHandler(this.CitiBetBot_Load);
            this.spContainer.Panel1.ResumeLayout(false);
            this.spContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spContainer)).EndInit();
            this.spContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblBet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.SplitContainer spContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView tblBet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbPlayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbRaceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbCountry;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbStadium;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbRace;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbHorse;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbWin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbBetTime;
    }
}

