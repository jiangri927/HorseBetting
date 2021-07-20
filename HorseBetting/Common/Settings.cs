using HorseBetting.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Common
{
    internal class Settings
    {
        public Settings()
        {
            
        }
        
        private string conf_filename = "setting.conf";

        // Interfaces
        public string url1 = "http://www.kle009.com";
        public string url2 = "http://www.kle009.com";
        public string captha_img = "capture.jpg";
        public string token1 = "538e4bf1150bcc06e1ae888ba4f2cee7";
        public string token2 = "AC45aa138944e118e87d3ab50f979ba9be";
        public string token3 = "bf932e03308599de38db47f89b9b622e";
        public string app_phone = "+18435805154";
        public string app_mail = "lasvegasking11812@gmail.com";
        public string app_pass = "QWEasd18";
        public bool is_Test;
        public AccountItem agentAccount = new AccountItem();
        public AccountItem userAccount = new AccountItem();

        public List<BetSettingItem> userList = new List<BetSettingItem>();

        private bool m_isSG;
        public bool isSG
        {
            get { return m_isSG; }
            set { m_isSG = value; }
        }

        private bool m_isMY;
        public bool isMY
        {
            get { return m_isMY; }
            set { m_isMY = value; }
        }

        private bool m_isHK;
        public bool isHK
        {
            get { return m_isHK; }
            set { m_isHK = value; }
        }

        private bool m_isMC;
        public bool isMC
        {
            get { return m_isMC; }
            set { m_isMC = value; }
        }

        private bool m_isAU;
        public bool isAU
        {
            get { return m_isAU; }
            set { m_isAU = value; }
        }

        private bool m_isJP;
        public bool isJP
        {
            get { return m_isJP; }
            set { m_isJP = value; }
        }

        private bool m_isUS;
        public bool isUS
        {
            get { return m_isUS; }
            set { m_isUS = value; }
        }

        private bool m_isSW;
        public bool isSW
        {
            get { return m_isSW; }
            set { m_isSW = value; }
        }

        private bool m_isFR;
        public bool isFR
        {
            get { return m_isFR; }
            set { m_isFR = value; }
        }


        private bool m_isNZ;
        public bool isNZ
        {
            get { return m_isNZ; }
            set { m_isNZ = value; }
        }

        private bool m_isUK;
        public bool isUK
        {
            get { return m_isUK; }
            set { m_isUK = value; }
        }

        private string m_strMail;
        public string mail
        {
            get { return m_strMail; }
            set { m_strMail = value; }
        }

        private string m_strPhone;
        public string phone
        {
            get { return m_strPhone; }
            set { m_strPhone = value; }
        }
        private static Settings g_settings;
        public static Settings GetSettings
        {
            get
            {
                if (Settings.g_settings == null)
                {
                    Settings.g_settings = new Settings();
                }
                return Settings.g_settings;
            }
        }
        
        public void LoadSettings()
        {
            if (File.Exists(conf_filename))
            {
                string value = File.ReadAllText(conf_filename);
                Settings.g_settings = JsonConvert.DeserializeObject<Settings>(value);

            }
        }
        public void WriteSettings()
        {
            string contents = JsonConvert.SerializeObject(Settings.g_settings);
            File.WriteAllText(conf_filename, contents);
        }

        public BetSettingItem FindBetSettingItem(string strAccountName)
        {
            BetSettingItem result;
            try
            {
                BetSettingItem betSettingItem = this.userList.Find((BetSettingItem item)=>{ return item.AccountName == strAccountName; });
                result = betSettingItem;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = null;
            }
            return result;
        }
    }
}
