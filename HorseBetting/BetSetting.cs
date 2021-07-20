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
    public partial class BetSetting : Form
    {
        private BetSettingItem m_Item = null;
        private List<double> m_timesList = new List<double>
        {
            0.1,
            0.2,
            0.3,
            0.4,
            0.5,
            0.6,
            0.7,
            0.8,
            0.9,
            1.0,
            2.0,
            3.0,
            4.0,
            5.0,
            6.0,
            7.0,
            8.0,
            9.0,
            10.0,
            20.0,
            30.0,
            40.0,
            50.0,
            60.0,
            70.0,
            80.0,
            90.0,
            100.0,
            200.0,
            300.0,
            400.0,
            500.0,
            600.0,
            700.0,
            800.0,
            900.0,
            1000.0
        };
        public BetSetting(BetSettingItem item = null)
        {
            InitializeComponent();
            foreach (double num in m_timesList)
            {
                cmbTime.Items.Add(num);
            }
            if (item != null)
            {
                txtAccountName.Text = item.AccountName;
                rbFollow.Checked = item.Type;
                rbReverse.Checked = !item.Type;
                cmbTime.SelectedIndex = GetIndex(item.Times);
                m_Item = item;
            }
            else
            {
                rbFollow.Checked = true;
                rbReverse.Checked = false;
                cmbTime.SelectedIndex = 0;
            }
        }
        private int GetIndex(double dVal)
        {
            for (int i = 0; i < m_timesList.Count; i++)
            {
                if (m_timesList[i] == dVal)
                {
                    return i;
                }
            }
            return 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtAccountName.Text.Length == 0)
            {
                MessageBox.Show("Please input AccountName!", "Alert");
                return;
            }
            if (m_Item == null && Settings.GetSettings.userList.Find(new Predicate<BetSettingItem>(FindByName)) != null)
            {
                MessageBox.Show(string.Format("Setting about {0} is already exist!", txtAccountName.Text), "Alert");
                return;
            }
            if (m_Item != null)
            {
                m_Item.AccountName = txtAccountName.Text;
                m_Item.Type = rbFollow.Checked;
                m_Item.StrType = rbFollow.Checked ? "Follow" : "Reverse";
                m_Item.Times = (double)cmbTime.SelectedItem;
            }
            else
            {
                BetSettingItem item = new BetSettingItem();
                item.AccountName = txtAccountName.Text;
                item.Type = rbFollow.Checked;
                item.StrType = rbFollow.Checked ? "Follow" : "Reverse";
                item.Times = (double)cmbTime.SelectedItem;
                Settings.GetSettings.userList.Add(item);
            }
            DialogResult = DialogResult.OK;
        }

        private bool FindByName(BetSettingItem betItem)
        {
            return betItem.AccountName == txtAccountName.Text;
        }
    }
}
