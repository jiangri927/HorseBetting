using HorseBetting.Common;
using HorseBetting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseBetting
{
    public partial class SettingForm : Form
    {
        private BindingSource bs = new BindingSource();
        public SettingForm()
        {
            InitializeComponent();
            tblUserList.AutoGenerateColumns = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Settings.GetSettings.WriteSettings();
            DialogResult = DialogResult.OK;
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            txtAgentName.Text = Settings.GetSettings.agentAccount.UserName;
            txtAgentPass.Text = Settings.GetSettings.agentAccount.UserPass;
            txtAgentCode.Text = Settings.GetSettings.agentAccount.UserCode;

            txtUserName.Text = Settings.GetSettings.userAccount.UserName;
            txtUserPass.Text = Settings.GetSettings.userAccount.UserPass;
            txtUserCode.Text = Settings.GetSettings.userAccount.UserCode;

            chkSG.Checked = Settings.GetSettings.isSG;
            chkMY.Checked = Settings.GetSettings.isMY;
            chkMC.Checked = Settings.GetSettings.isMC;
            chkHK.Checked = Settings.GetSettings.isHK;
            chkAU.Checked = Settings.GetSettings.isAU;
            chkSW.Checked = Settings.GetSettings.isSW;
            chkFR.Checked = Settings.GetSettings.isFR;
            chkUK.Checked = Settings.GetSettings.isUK;
            chkUS.Checked = Settings.GetSettings.isUS;
            chkNZ.Checked = Settings.GetSettings.isNZ;

            txtMail.Text = Settings.GetSettings.mail;
            txtPhone.Text = Settings.GetSettings.phone;
            LoadUserList();
        }
        private void LoadUserList()
        {
            try
            {
                Invoke(new Action(refreshList));
            }catch(Exception)
            {

            }
        }
        private void refreshList()
        {
            bs.ResetBindings(true);
            bs.DataSource = Settings.GetSettings.userList;
            tblUserList.DataSource = bs;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtAgentName.Text.Length == 0)
            {
                MessageBox.Show("Please input Agent name!", "alert");
                return;
            }
            Settings.GetSettings.agentAccount.UserName = txtAgentName.Text;

            if (txtAgentPass.Text.Length == 0)
            {
                MessageBox.Show("Please input Agent password!", "alert");
                return;
            }
            Settings.GetSettings.agentAccount.UserPass = txtAgentPass.Text;

            if (txtAgentCode.Text.Length == 0)
            {
                MessageBox.Show("Please input Agent code!", "alert");
                return;
            }
            Settings.GetSettings.agentAccount.UserCode = txtAgentCode.Text;

            if (txtUserName.Text.Length == 0)
            {
                MessageBox.Show("Please input User name!", "alert");
                return;
            }
            Settings.GetSettings.userAccount.UserName = txtUserName.Text;

            if (txtUserPass.Text.Length == 0)
            {
                MessageBox.Show("Please input User password!", "alert");
                return;
            }
            Settings.GetSettings.userAccount.UserPass = txtUserPass.Text;

            if (txtUserCode.Text.Length == 0)
            {
                MessageBox.Show("Please input User code!", "alert");
                return;
            }
            Settings.GetSettings.userAccount.UserCode = txtUserCode.Text;

            DialogResult = DialogResult.OK;
            Settings.GetSettings.isSG = chkSG.Checked;
            Settings.GetSettings.isMY = chkMY.Checked;
            Settings.GetSettings.isMC = chkMC.Checked;
            Settings.GetSettings.isHK = chkHK.Checked;
            Settings.GetSettings.isAU = chkAU.Checked;
            Settings.GetSettings.isSW = chkSW.Checked;
            Settings.GetSettings.isFR = chkFR.Checked;
            Settings.GetSettings.isUK = chkUK.Checked;
            Settings.GetSettings.isUS = chkUS.Checked;
            Settings.GetSettings.isNZ = chkNZ.Checked;
            Settings.GetSettings.mail = txtMail.Text;
            Settings.GetSettings.phone = txtPhone.Text;
            Settings.GetSettings.WriteSettings();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BetSetting frmBetSetting = new BetSetting(null);
            if(frmBetSetting.ShowDialog() == DialogResult.OK)
            {
                LoadUserList();
            }
        }

        private void BetSetting_Actions(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 3 || Settings.GetSettings.userList.Count <= e.RowIndex)
                return;

            if (e.ColumnIndex == 4)
            { 
                if (MessageBox.Show("Are you really want to remove this setting?", "Alert", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    Settings.GetSettings.userList.RemoveAt(e.RowIndex);
                    LoadUserList();
                }
            }
            else if (e.ColumnIndex == 3)
            {
                BetSetting frmBetSetting = new BetSetting(Settings.GetSettings.userList[e.RowIndex]);
                if (frmBetSetting.ShowDialog() == DialogResult.OK)
                {
                    LoadUserList();
                }
            }
            
        }
    }
}
