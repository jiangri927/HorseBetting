using HorseBetting.Common;
using HorseBetting.Controller;
using HorseBetting.Model;
using HorseBetting.SingleBet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseBetting
{

    public partial class CitiBetBot : Form
    {
        private onWriteLogEvent m_onWriteLogEvent;
        private onUpdateLoginStatus m_onUpdateLoginStatus;
        private onOneBetEvent m_onOneBetEvent;

        private BindingSource bs = new BindingSource();

        public delegate void onWriteLogEvent(string strLog);
        public delegate void onUpdateLoginStatus();
        public delegate void onOneBetEvent(BetItem newItem);

        public CitiBetCtr m_CitiBetCtr;
        private Thread m_MainThread;

        private Thread m_BettingThread;
        private Thread m_UnsettleThread;
        private Thread m_ScrapyThred;
        private Thread thread_4;
        private int m_nRunMode = 0;

        public CitiBetBot()
        {
            InitializeComponent();
            this.onWriteLog += WriteLog;
            this.onUpdateLogin += UpdateLogin;
            this.onOneBet += OneBet;
            tblBet.AutoGenerateColumns = false;

            string strUrl = "<script>document.domain = 'kle009.com';</script><script>top.reply('bets','Your EAT order has been partially transacted.<br><br>Rc: &nbsp;&nbsp;<span style=\"color:#FF0000\"><strong>9</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;Hs:&nbsp;&nbsp;<span style=\"color:#FF0000\"><strong>6</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;Win:&nbsp;&nbsp;<span style=\"color:#FF0000\"><strong>3</strong></span>&nbsp;&nbsp;&nbsp;Plc:&nbsp;&nbsp;<span style=\"color:#FF0000\"><strong>3</strong></span>');</script>\"";
            strUrl = strUrl.Replace("&nbsp;", "");
            strUrl = strUrl.Replace("<span style=\"color:#FF0000\"><strong>", "");
            Match match = Regex.Match(strUrl, "Win:([^<])");
            string strWin = match.Groups[1].Value;
            string strPlc = Regex.Match(strUrl, "Plc:([^<])").Groups[1].Value;
            strUrl = "";
        }

        
        private void CitiBetBot_Load(object sender, EventArgs e)
        {
            Settings.GetSettings.LoadSettings();
            Settings.GetSettings.is_Test = false;

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingForm frmSetting = new SettingForm();
            frmSetting.ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.WriteLog(string.Format("Trying to login", new object[0]));
            if (!this.TestConnection())
            {

                this.WriteLog(string.Format("Can not login agent or user", new object[0]));
                
            }
            else if (this.CheckAgentInfo())
            {
                this.EnableButtons(false);
                this.Login();
            }
        }

        private bool TestConnection()
        {
            bool result3;
            try
            {
                bool flag = false;
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage result = httpClient.GetAsync("http://superlink.co.kr/test/King1").Result;
                result.EnsureSuccessStatusCode();
                string result2 = result.Content.ReadAsStringAsync().Result;
                if (result2 == "success")
                {
                    result3 = true;
                }
                else
                {
                    result3 = flag;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result3 = false;
            }
            return result3;
        }

        private void WriteLog(string strMsg)
        {
            try
            {
                if (rtLog.InvokeRequired)
                {
                    rtLog.Invoke(m_onWriteLogEvent, new object[]{
                        strMsg
                    });
                }
                else
                {
                    rtLog.AppendText((rtLog.Text.Length == 0 ? "" : "\r\n") + string.Format("{1} -> {0}", strMsg, GetTimeStamp()));
                    rtLog.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        private void UpdateLogin()
        {
            if (this.rtLog.InvokeRequired)
            {
                this.rtLog.Invoke(this.m_onUpdateLoginStatus);
            }
            else
            {
                try
                {
                    base.Invoke(new Action(Settings.GetSettings.WriteSettings));
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                this.rtLog.ScrollToCaret();
            }
        }
        private void OneBet(BetItem betItem_0)
        {
            base.Invoke(new Action(this.UpdateBetList));
        }

        private void UpdateBetList()
        {
            bs.ResetBindings(true);
            bs.DataSource = BetData.Instance.m_SettledList;
            tblBet.DataSource = bs;

        }

        private string GetTimeStamp()
        {
            return string.Format("[{0}] ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private bool CheckAgentInfo()
        {
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(Settings.GetSettings.agentAccount.UserName) || string.IsNullOrEmpty(Settings.GetSettings.agentAccount.UserPass) || string.IsNullOrEmpty(Settings.GetSettings.agentAccount.UserCode) ||
                    string.IsNullOrEmpty(Settings.GetSettings.userAccount.UserName) || string.IsNullOrEmpty(Settings.GetSettings.userAccount.UserPass) || string.IsNullOrEmpty(Settings.GetSettings.userAccount.UserCode))
                {
                    MessageBox.Show("Please input account information", "Alert");
                    return false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }
        private void EnableButtons(bool bEnable)
        {
            btnStart.Enabled = bEnable;
            btnStop.Enabled = !bEnable;
            btnSettings.Enabled = bEnable;
        }

        public event onWriteLogEvent onWriteLog
        {
            add
            {
                onWriteLogEvent onWriteLogEvent = this.m_onWriteLogEvent;
                onWriteLogEvent onWriteLogEvent2;
                do
                {
                    onWriteLogEvent2 = onWriteLogEvent;
                    onWriteLogEvent value2 = (onWriteLogEvent)Delegate.Combine(onWriteLogEvent2, value);
                    onWriteLogEvent = Interlocked.CompareExchange<onWriteLogEvent>(ref m_onWriteLogEvent, value2, onWriteLogEvent2);
                }
                while (onWriteLogEvent != onWriteLogEvent2);
            }
            remove
            {
                onWriteLogEvent onWriteLogEvent = this.m_onWriteLogEvent;
                onWriteLogEvent onWriteLogEvent2;
                do
                {
                    onWriteLogEvent2 = onWriteLogEvent;
                    onWriteLogEvent value2 = (onWriteLogEvent)Delegate.Remove(onWriteLogEvent2, value);
                    onWriteLogEvent = Interlocked.CompareExchange<onWriteLogEvent>(ref m_onWriteLogEvent, value2, onWriteLogEvent2);
                }
                while (onWriteLogEvent != onWriteLogEvent2);
            }
        }

        public event onUpdateLoginStatus onUpdateLogin
        {
            add
            {
                onUpdateLoginStatus onUpdateLoginStatus = m_onUpdateLoginStatus;
                onUpdateLoginStatus onUpdateLoginStatus2;
                do
                {
                    onUpdateLoginStatus2 = onUpdateLoginStatus;
                    onUpdateLoginStatus value2 = (onUpdateLoginStatus)Delegate.Combine(onUpdateLoginStatus2, value);
                    onUpdateLoginStatus = Interlocked.CompareExchange<onUpdateLoginStatus>(ref m_onUpdateLoginStatus, value2, onUpdateLoginStatus2);
                }
                while (onUpdateLoginStatus != onUpdateLoginStatus2);
            }
            remove
            {
                onUpdateLoginStatus onUpdateLoginStatus = m_onUpdateLoginStatus;
                onUpdateLoginStatus onUpdateLoginStatus2;
                do
                {
                    onUpdateLoginStatus2 = onUpdateLoginStatus;
                    onUpdateLoginStatus value2 = (onUpdateLoginStatus)Delegate.Remove(onUpdateLoginStatus2, value);
                    onUpdateLoginStatus = Interlocked.CompareExchange<onUpdateLoginStatus>(ref m_onUpdateLoginStatus, value2, onUpdateLoginStatus2);
                }
                while (onUpdateLoginStatus != onUpdateLoginStatus2);
            }
        }

        public event onOneBetEvent onOneBet
        {
            add
            {
                onOneBetEvent onOneBetEvent = m_onOneBetEvent;
                onOneBetEvent onOneBetEvent2;
                do
                {
                    onOneBetEvent2 = onOneBetEvent;
                    onOneBetEvent value2 = (onOneBetEvent)Delegate.Combine(onOneBetEvent2, value);
                    onOneBetEvent = Interlocked.CompareExchange<onOneBetEvent>(ref m_onOneBetEvent, value2, onOneBetEvent2);
                }
                while (onOneBetEvent != onOneBetEvent2);
            }
            remove
            {
                onOneBetEvent onOneBetEvent = m_onOneBetEvent;
                onOneBetEvent onOneBetEvent2;
                do
                {
                    onOneBetEvent2 = onOneBetEvent;
                    onOneBetEvent value2 = (onOneBetEvent)Delegate.Remove(onOneBetEvent2, value);
                    onOneBetEvent = Interlocked.CompareExchange<onOneBetEvent>(ref m_onOneBetEvent, value2, onOneBetEvent2);
                }
                while (onOneBetEvent != onOneBetEvent2);
            }
        }
        private void Login()
        {
            if (this.m_CitiBetCtr == null)
            {
                this.m_CitiBetCtr = new CitiBetCtr(this.m_onWriteLogEvent, this.m_onUpdateLoginStatus, this.m_onOneBetEvent);
            }
            this.m_CitiBetCtr.initHttpClient();
            this.m_onWriteLogEvent("Login has been started!");
            
            Settings.GetSettings.WriteSettings();
            this.m_onWriteLogEvent("The Scraper has been started!");
            m_MainThread = new Thread(new ThreadStart(MainThreadLoop));
            string format = "[{0}] Thread Count - {1}";
            object arg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            BetData instance = BetData.Instance;
            int num = instance.ThreadCount + 1;
            instance.ThreadCount = num;
            Trace.WriteLine(string.Format(format, arg, num));
            m_MainThread.Start();
        }

        private void MainThreadLoop()
        {
            try
            {
                if (!Settings.GetSettings.is_Test)
                {
                    this.m_CitiBetCtr.doLogin(this.m_nRunMode);
                    if (this.m_ScrapyThred == null && this.m_nRunMode < 2)
                    {
                        // m_ScrapyThread:
                        //  Creates each thread for registered user that keeps on newly created bet orders
                        this.m_ScrapyThred = new Thread(new ThreadStart(this.m_CitiBetCtr.startMainScrap));
                        this.m_ScrapyThred.Start();
                        Trace.WriteLine(string.Format("[{0}] Thread Count - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BetData.Instance.ThreadCount++));
                    }
                    if (this.m_UnsettleThread == null && this.m_nRunMode < 2)
                    {
                        this.m_UnsettleThread = new Thread(new ThreadStart(this.m_CitiBetCtr.MakeToDoBetList));
                        this.m_UnsettleThread.Start();
                        Trace.WriteLine(string.Format("[{0}] Thread Count - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BetData.Instance.ThreadCount++));
                    }
                    if (this.m_BettingThread == null && this.m_nRunMode != 1)
                    {
                        this.m_BettingThread = new Thread(new ThreadStart(this.m_CitiBetCtr.DoBettingWork));
                        this.m_BettingThread.Start();
                        Trace.WriteLine(string.Format("[{0}] Thread Count - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BetData.Instance.ThreadCount++));
                    }
                }
                else
                {
                    this.m_CitiBetCtr.doTest();
                }
            }
            catch (Exception ex)
            {
                this.m_onWriteLogEvent(string.Format("MainThreadError : {0}", ex.Message));
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopBot();
        }

        private void CitiBetBot_FormClosing(object sender, FormClosingEventArgs e)
        {
            Directory.CreateDirectory("Logs");

            string path = string.Format("Logs/{0}_{1}_{2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            using (StreamWriter sw = File.AppendText(path))
            {
                foreach(string sentence in  rtLog.Text.Split('\n'))
                    sw.WriteLine(sentence);
                
            }

            Environment.Exit(0);
            this.StopBot();           
        }

        private void StopBot()
        {
            if (this.m_MainThread != null)
            {
                this.m_MainThread.Abort();
                this.m_MainThread = null;
            }
            if (this.m_BettingThread != null)
            {
                this.m_BettingThread.Abort();
                this.m_BettingThread = null;
            }
            if (this.m_ScrapyThred != null)
            {
                this.m_ScrapyThred.Abort();
                this.m_ScrapyThred = null;
            }
            if (this.m_UnsettleThread != null)
            {
                this.m_UnsettleThread.Abort();
                this.m_UnsettleThread = null;
            }
            if (this.thread_4 != null)
            {
                this.thread_4.Abort();
                this.thread_4 = null;
            }
            if(m_CitiBetCtr != null)
                m_CitiBetCtr.Abort();
            this.m_onWriteLogEvent("The Scraper has been stopped!");
            
            this.EnableButtons(true);
        }


        
    }

}
