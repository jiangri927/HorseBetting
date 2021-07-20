using HorseBetting.Common;
using HorseBetting.Model;
using HorseBetting.SingleBet;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParseTool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static HorseBetting.CitiBetBot;

namespace HorseBetting.Controller
{
    public class CitiBetCtr
    {
        private onWriteLogEvent m_onWriteLogEvent = null;
        private onOneBetEvent m_onOneBetEvent = null;
        private onUpdateLoginStatus m_onUpdateLoginStatus = null;

        public SessionItem m_AgentSession = new SessionItem();
        public SessionItem UserSession = new SessionItem();
        private List<SubAccItem> m_MemberList = new List<SubAccItem>();
        
        private List<Thread> m_UserList = new List<Thread>();
                
        // Token: 0x040000D0 RID: 208
        private List<RaceInfo> m_RaceInfoList = new List<RaceInfo>();
        private List<BetItem> m_RunningBets = new List<BetItem>();

        // Token: 0x040000D2 RID: 210
        private string validationCode = "";

        // Token: 0x040000D3 RID: 211
        private int int_2 = 100;

        private bool m_bWinPlace = true;
        private bool m_bForecast = false;
        private bool m_bQuinella = false;
        private bool m_bRaceEach = false;

        // Token: 0x040000D8 RID: 216
        private string string_1 = string.Format("{0}\\alert.wav", global::System.Windows.Forms.Application.StartupPath);

        public CitiBetCtr(onWriteLogEvent _onWriteEvent, onUpdateLoginStatus _onUpdateLoginEvent, onOneBetEvent _onOneBetEvent)
        {
            this.m_onWriteLogEvent = _onWriteEvent;
            this.m_onUpdateLoginStatus = _onUpdateLoginEvent;
            this.m_onOneBetEvent = _onOneBetEvent;
            global::System.Net.ServicePointManager.SecurityProtocol = (global::System.Net.SecurityProtocolType.Ssl3 | global::System.Net.SecurityProtocolType.Tls | global::System.Net.SecurityProtocolType.Tls11 | global::System.Net.SecurityProtocolType.Tls12);


        }

        public void initHttpClient()
        {
            this.m_AgentSession.UserName = Settings.GetSettings.agentAccount.UserName;
            this.m_AgentSession.UserPass = Settings.GetSettings.agentAccount.UserPass;
            this.m_AgentSession.IsLogin = false;
            this.m_AgentSession.UserCode = Settings.GetSettings.agentAccount.UserCode;
            this.m_AgentSession.SingHttpClient = GetHttpClient("Agent");
            this.UserSession.UserName = Settings.GetSettings.userAccount.UserName;
            this.UserSession.UserPass = Settings.GetSettings.userAccount.UserPass;
            this.UserSession.IsLogin = false;
            this.UserSession.UserCode = Settings.GetSettings.userAccount.UserCode;
            this.UserSession.SingHttpClient = GetHttpClient("User");
        }

        private HttpClient GetHttpClient(string strMode)
        {
            HttpClient httpClient = null;
            try
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler
                {
                    AutomaticDecompression = (global::System.Net.DecompressionMethods.GZip | global::System.Net.DecompressionMethods.Deflate)
                };
                CookieContainer cookieContainer = new CookieContainer();
                cookieContainer.Add(new Uri(Settings.GetSettings.url2), new Cookie("lang", "EN"));
                httpClientHandler.CookieContainer = cookieContainer;
                if (strMode == "Agent")
                {
                    this.m_AgentSession.CookieContainer = cookieContainer;
                }
                else if (strMode == "User")
                {
                    this.UserSession.CookieContainer = cookieContainer;
                }
                httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                httpClient.DefaultRequestHeaders.ExpectContinue = new bool?(false);
                httpClient.Timeout = TimeSpan.FromDays(1.0);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return httpClient;
        }

        public bool doLogin(int mode)
        {
            bool result = false;
            try
            {
                bool flag = mode > 1 || this.SiteLogin(m_AgentSession);
                bool flag2 = mode == 1 || this.SiteLogin(UserSession);
                result = (flag && flag2);
            }
            catch (Exception ex)
            {
                this.m_onWriteLogEvent(string.Format("{0} -> Error Occured! {1}", "doLogin", ex.Message));
            }
            this.m_onUpdateLoginStatus();
            return result;
        }
        public void doTest()
        {
            try
            {
                string path = string.Format("{0}\\test.html", global::System.Windows.Forms.Application.StartupPath);
                if (File.Exists(path))
                {
                    File.ReadAllText(path);
                    SettledMatchItem settledMatchItem = new SettledMatchItem();
                    settledMatchItem.PlayerID = "ka47t1";
                    settledMatchItem.Location = "AU (Dog)-Geelong";
                    settledMatchItem.Tote = "NW";
                    settledMatchItem.TotalTickets = "20";
                    settledMatchItem.TotalTax = "0.02";
                    settledMatchItem.Profit = "19.52";
                    settledMatchItem.Type = "66N";
                    settledMatchItem.URL = "new_viewtransactions.jsp?uid=ka47t1&rd=22-05-2020&typ=66N','66N";
                    settledMatchItem.RacingDate = global::System.DateTime.Parse("2020-05-22 00:00:00");
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public bool SiteLogin(SessionItem item)
        {
            bool result;
            try
            {
                if (item != null && !item.UserName.Equals(item.UserName))
                {
                    result = false;
                }
                else
                {
                    this.m_onWriteLogEvent(string.Format("{0} -> Login has been started!", item.UserName));
                    HttpResponseMessage result2 = item.SingHttpClient.GetAsync(string.Format("{0}/", Settings.GetSettings.url1)).Result;
                    result2.EnsureSuccessStatusCode();
                    if (result2.StatusCode != HttpStatusCode.OK)
                    {
                        this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                        result = false;
                    }
                    else
                    {
                        string result3 = result2.Content.ReadAsStringAsync().Result;
                        if (string.IsNullOrEmpty(result3))
                        {
                            this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                            result = false;
                        }
                        else
                        {
                            Match match = Regex.Match(result3, "\\('(?<VAR>[^\\\\].*)'");
                            string value = match.Groups["VAR"].Value;
                            item.SingHttpClient.DefaultRequestHeaders.Referrer = new Uri(Settings.GetSettings.url1);
                            result2 = item.SingHttpClient.GetAsync(string.Format("{0}/{1}", Settings.GetSettings.url1, value)).Result;
                            result2.EnsureSuccessStatusCode();
                            if (result2.StatusCode != HttpStatusCode.OK)
                            {
                                this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                result = false;
                            }
                            else
                            {
                                result3 = result2.Content.ReadAsStringAsync().Result;
                                if (string.IsNullOrEmpty(result3))
                                {
                                    this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                    result = false;
                                }
                                else
                                {
                                    item.SingHttpClient.DefaultRequestHeaders.Referrer = new Uri(string.Format("{0}/{1}", Settings.GetSettings.url1, value));
                                    result2 = item.SingHttpClient.GetAsync(string.Format("{0}/_index_ctb.jsp", Settings.GetSettings.url1)).Result;
                                    result2.EnsureSuccessStatusCode();
                                    if (result2.StatusCode != HttpStatusCode.OK)
                                    {
                                        this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                        result = false;
                                    }
                                    else
                                    {
                                        result3 = result2.Content.ReadAsStringAsync().Result;
                                        if (string.IsNullOrEmpty(result3))
                                        {
                                            this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                            result = false;
                                        }
                                        else
                                        {
                                            Match match2 = Regex.Match(result3, "id=\"valid[^=]*=\"(?<VAR>[^\"]*)");
                                            this.validationCode = match2.Groups["VAR"].Value;
                                            if (!string.IsNullOrEmpty(this.validationCode))
                                            {
                                                Random random = new Random();
                                                if (!Utils.downloadFile(string.Format("{0}/img.jpg?0.{1}", Settings.GetSettings.url1, random.Next(0, 100000)), item.CookieContainer))
                                                {
                                                    this.m_onWriteLogEvent(string.Format("{0} -> Capture download fail!", item.UserName));
                                                    result = false;
                                                }
                                                else
                                                {
                                                    string txtCapthaCode = "";
                                                    if (Utils.trySolveCaptchaUpdate("capture.jpg", ref txtCapthaCode) < 0)
                                                    {
                                                        this.m_onWriteLogEvent(string.Format("{0} -> Can't get capture key!", item.UserName));
                                                        result = false;
                                                    }
                                                    else
                                                    {
                                                        string text2 = this.mask(this.validationCode + txtCapthaCode + this.mask("voodoo_people_" + item.UserName + this.mask(item.UserPass)));
                                                        string text3 = string.Format("{0}/login?uid={1}&pass={2}&code={3}&lang=EN&ssl=http", new object[]
                                                        {
                                                            Settings.GetSettings.url1,
                                                            item.UserName,
                                                            text2,
                                                            txtCapthaCode
                                                        });
                                                        item.SingHttpClient.DefaultRequestHeaders.Referrer = new global::System.Uri(string.Format("{0}/_index_ctb.jsp", Settings.GetSettings.url1));
                                                        result2 = item.SingHttpClient.GetAsync(text3).Result;
                                                        result2.EnsureSuccessStatusCode();
                                                        if (result2.StatusCode != global::System.Net.HttpStatusCode.OK)
                                                        {
                                                            this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                                            result = false;
                                                        }
                                                        else
                                                        {
                                                            result3 = result2.Content.ReadAsStringAsync().Result;
                                                            if (string.IsNullOrEmpty(result3))
                                                            {
                                                                this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                                                result = false;
                                                            }
                                                            else if (result3.Contains("validate_pin.jsp"))
                                                            {
                                                                if (this.VerifyPin(item, text3))
                                                                {
                                                                    this.m_onWriteLogEvent(string.Format("{0} -> Login Success!", item.UserName));
                                                                    item.IsLogin = true;
                                                                }
                                                                result = false;
                                                            }
                                                            else
                                                            {
                                                                this.m_onWriteLogEvent(string.Format("{0} -> Login Failure!", item.UserName));
                                                                result = false;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                this.m_onWriteLogEvent(string.Format("{0} -> ValidCode is null!", item.UserName));
                                                result = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (global::System.Exception ex)
            {
                this.m_onWriteLogEvent(string.Format("{0} -> Error Occured! {1}", "SiteLogin", ex.Message));
                result = false;
            }
            return result;
        }

        public bool VerifyPin(SessionItem item, string referer)
        {
            string value = "528170610";
            string value2 = "1893216991";
            string value3 = "528170610";
            string value4 = "309371099";
            string value5 = "d5d1c52699301d95eb0a805e4dab0707";
            item.SingHttpClient.DefaultRequestHeaders.Referrer = new Uri(referer);
            HttpResponseMessage result = item.SingHttpClient.GetAsync(string.Format("{0}/validate_pin.jsp", Settings.GetSettings.url2)).Result;
            result.EnsureSuccessStatusCode();
            bool result2;
            if (result.StatusCode != HttpStatusCode.OK)
            {
                this.m_onWriteLogEvent(string.Format("{0} -> Verify Pin Failure!", item.UserName));
                result2 = false;
            }
            else
            {
                string result3 = result.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(result3))
                {
                    this.m_onWriteLogEvent(string.Format("{0} -> Verify Pine Failure!", item.UserName));
                    result2 = false;
                }
                else
                {
                    Match match = Regex.Match(result3, "r1='(?<VAR>[^']*)'");
                    Match match2 = Regex.Match(result3, "r2='(?<VAR>[^']*)'");
                    string value6 = match.Groups["VAR"].Value;
                    string value7 = match2.Groups["VAR"].Value;
                    string value8 = this.mask(value6 + value7 + this.mask("pin_" + item.UserName + item.UserCode));
                    result = item.SingHttpClient.PostAsync(string.Format("{0}/verifypin", Settings.GetSettings.url2), new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>("code", value8),
                        new KeyValuePair<string, string>("trafficStatistics", value),
                        new KeyValuePair<string, string>("trafficStatisticsCanvas", value2),
                        new KeyValuePair<string, string>("trafficStatisticsActivex", value3),
                        new KeyValuePair<string, string>("trafficStatisticsResolution", value4),
                        new KeyValuePair<string, string>("trafficStatistics2", value5)
                    })).Result;
                    result.EnsureSuccessStatusCode();
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        this.m_onWriteLogEvent(string.Format("{0} -> Verify Pin Failure!", item.UserName));
                        result2 = false;
                    }
                    else
                    {
                        result3 = result.Content.ReadAsStringAsync().Result;
                        if (string.IsNullOrEmpty(result3))
                        {
                            this.m_onWriteLogEvent(string.Format("{0} -> Verify Pine Failure!", item.UserName));
                            result2 = false;
                        }
                        else if (result3.Contains("terms.jsp"))
                        {
                            result2 = true;
                        }
                        else
                        {
                            this.m_onWriteLogEvent(string.Format("{0} -> Verify Pin Failure!", item.UserName));
                            result2 = false;
                        }
                    }
                }
            }
            return result2;
        }

        private Int32 rotate_left(UInt32 n, int s)
        {
            var t4 = (n << s) | (n >> (32 - s));
            return (Int32)t4;
        }


        private string cvt_hex(UInt32 val)
        {
            var str = "";

            UInt32 v;

            for (int i = 7; i >= 0; i--)
            {
                v = (val >> (i * 4)) & 0x0f;
                str += v.ToString("X1");
            }
            return str;
        }


        private string Utf8Encode(string str)
        {
            str = str.Replace("\r\n", "\n");
            var utftext = "";

            for (var n = 0; n < str.Length; n++)
            {

                int c = str[n];

                if (c < 128)
                {
                    utftext += Convert.ToChar(c);
                }
                else if ((c > 127) && (c < 2048))
                {
                    utftext += Convert.ToChar((c >> 6) | 192);
                    utftext += Convert.ToChar((c & 63) | 128);
                }
                else
                {
                    utftext += Convert.ToChar((c >> 12) | 224);
                    utftext += Convert.ToChar(((c >> 6) & 63) | 128);
                    utftext += Convert.ToChar((c & 63) | 128);
                }

            }

            return utftext;
        }

        public string mask(string msg)
        {
            int i, j;
            Int32[] W = new Int32[80];
            UInt32 H0 = 0x67452301;
            UInt32 H1 = 0xEFCDAB89;
            UInt32 H2 = 0x98BADCFE;
            UInt32 H3 = 0x10325476;
            UInt32 H4 = 0xC3D2E1F0;
            UInt32 A, B, C, D, E;
            Int32 temp;

            msg = Utf8Encode(msg);

            int msg_len = msg.Length;

            List<UInt32> word_array = new List<UInt32>();
            for (i = 0; i < msg_len - 3; i += 4)
            {
                j = msg[i] << 24 | msg[i + 1] << 16 |
                msg[i + 2] << 8 | msg[i + 3];
                word_array.Add((UInt32)(j));
            }
            UInt32 ii = 0;
            switch (msg_len % 4)
            {
                case 0:
                    ii = 0x080000000;
                    break;
                case 1:
                    ii = (UInt32)((msg[msg_len - 1]) << 24 | (UInt32)0x0800000);
                    break;

                case 2:
                    ii = (UInt32)((msg[msg_len - 2] << 24) | (msg[msg_len - 1]) << 16 | (UInt32)0x08000);
                    break;

                case 3:
                    ii = (UInt32)((msg[msg_len - 3] << 24) | (msg[msg_len - 2] << 16) | (msg[msg_len - 1] << 8) | (UInt32)0x80);
                    break;
            }

            word_array.Add(ii);

            while ((word_array.Count % 16) != 14) word_array.Add(0);

            word_array.Add((UInt32)(msg_len >> 29));
            var t = (msg_len << 3) & 0xffffffff;
            word_array.Add((UInt32)t);


            for (int blockstart = 0; blockstart < word_array.Count; blockstart += 16)
            {

                for (i = 0; i < 16; i++) W[i] = (Int32)word_array[blockstart + i];
                for (i = 16; i <= 79; i++) W[i] = rotate_left((UInt32)(W[i - 3] ^ W[i - 8] ^ W[i - 14] ^ W[i - 16]), 1);

                A = H0;
                B = H1;
                C = H2;
                D = H3;
                E = H4;

                for (i = 0; i <= 19; i++)
                {

                    temp = (Int32)((rotate_left(A, 5) + ((B & C) | (~B & D)) + E + W[i] + (UInt32)0x5A827999) & (UInt32)0x0ffffffff);
                    E = D;
                    D = C;
                    C = (UInt32)rotate_left(B, 30);
                    B = A;
                    A = (UInt32)temp;
                }

                for (i = 20; i <= 39; i++)
                {
                    temp = (Int32)((rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + (UInt32)0x6ED9EBA1) & (UInt32)0x0ffffffff);
                    E = D;
                    D = C;
                    C = (UInt32)(rotate_left(B, 30));
                    B = A;
                    A = (UInt32)temp;
                }

                for (i = 40; i <= 59; i++)
                {
                    temp = (Int32)((rotate_left(A, 5) + ((B & C) | (B & D) | (C & D)) + E + W[i] + 0x8F1BBCDC) & 0x0ffffffff);
                    E = D;
                    D = C;
                    C = (UInt32)rotate_left(B, 30);
                    B = A;
                    A = (UInt32)temp;
                }

                for (i = 60; i <= 79; i++)
                {
                    temp = (Int32)((rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0xCA62C1D6) & 0x0ffffffff);
                    E = D;
                    D = C;
                    C = (UInt32)rotate_left(B, 30);
                    B = A;
                    A = (UInt32)temp;
                }

                H0 = (H0 + A) & 0x0ffffffff;
                H1 = (H1 + B) & 0x0ffffffff;
                H2 = (H2 + C) & 0x0ffffffff;
                H3 = (H3 + D) & 0x0ffffffff;
                H4 = (H4 + E) & 0x0ffffffff;

            }

            string res_temp = cvt_hex(H0) + cvt_hex(H1) + cvt_hex(H2) + cvt_hex(H3) + cvt_hex(H4);

            return res_temp.ToLower();
        }

        public void startMainScrap()
        {
            try
            {
                HttpResponseMessage result = this.m_AgentSession.SingHttpClient.GetAsync(string.Format("{0}/member/member_tree.jsp", Settings.GetSettings.url2)).Result;
                result.EnsureSuccessStatusCode();
                
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    this.m_onWriteLogEvent(string.Format("{0} -> can't get subacc!", this.m_AgentSession.UserName));
                }
                else
                {
                    string result2 = result.Content.ReadAsStringAsync().Result;
                    
                    if (string.IsNullOrEmpty(result2))
                    {
                        this.m_onWriteLogEvent(string.Format("{0} -> can't get subacc!", this.m_AgentSession.UserName));
                    }
                    else
                    {
                        // Read the member_tree.jsp and get the registered users.
                        MatchCollection matchCollection = Regex.Matches(result2, "citibet[^\\[]*\\[\"(?<Name>[^\"]*)\"[^\"]*\"(?<User>[^\"]*)");
                        if (matchCollection.Count > 0)
                        {
                            foreach (object obj in matchCollection)
                            {
                                Match match = (Match)obj;
                                string value = match.Groups["Name"].Value;
                                string user = match.Groups["User"].Value;
                                string text = string.Format("history.jsp?uid={0}", value);
                                if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(user) && this.m_MemberList.Find((SubAccItem p) => { return p.PlayerID == user; }) == null)
                                {
                                    SubAccItem subAccItem = new SubAccItem();
                                    subAccItem.SubAccName = value;
                                    subAccItem.SubAccHistoryUrl = text;
                                    subAccItem.PlayerID = value;
                                    subAccItem.AgentName = this.m_AgentSession.UserName;
                                    this.m_MemberList.Add(subAccItem);
                                }
                            }
                        }
                        // Pick up users to follow and creates a thread per each user.
                        
                        using (List<SubAccItem>.Enumerator enumerator2 = this.m_MemberList.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                if (Settings.GetSettings.userList.Find((BetSettingItem p) => { return p.AccountName == enumerator2.Current.SubAccName; }) != null)
                                {
                                    Thread thread = new Thread(new ParameterizedThreadStart(this.DetectUserAction));
                                    Trace.WriteLine(string.Format("[{0}] Thread Count - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), BetData.Instance.ThreadCount++));
                                    thread.Start(enumerator2.Current);
                                    this.m_UserList.Add(thread);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void DetectUserAction(object obj)
        {
            try
            {
                SubAccItem subAccItem = obj as SubAccItem;
                this.m_AgentSession.SingHttpClient.DefaultRequestHeaders.Referrer = new Uri(string.Format("{0}/member_tree.jsp", Settings.GetSettings.url2));
                int num = this.int_2 - 1;
                for (; ; )
                {
                    try
                    {
                        HttpResponseMessage result = this.m_AgentSession.SingHttpClient.GetAsync(string.Format("{0}/{1}", Settings.GetSettings.url2, subAccItem.SubAccHistoryUrl)).Result;
                        string result2 = result.Content.ReadAsStringAsync().Result;
                        // ForTEST
                        // result2 = File.ReadAllText("Transaction History.html");
                        if (string.IsNullOrEmpty(result2))
                        {
                            this.m_onWriteLogEvent(string.Format("{0} -> can't get history!", this.m_AgentSession.UserName));
                        }
                        else
                        {
                            int int_ = 0;
                            if (++num == this.int_2)
                            {
                                int_ = 1;
                                num = 0;
                            }
                            this.ParseMemberHistory(result2, subAccItem.PlayerID, int_, this.m_AgentSession.UserName);
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
            }
            catch (Exception ex2)
            {
                string message2 = ex2.Message;
            }
        }

        private void ParseMemberHistory(string strResult, string strPlayerID, int int_3, string strUserName)
        {
            try
            {
                lock(m_RunningBets)
                {
                    m_RunningBets.RemoveAll((BetItem item) => { return item.PlayerID == strPlayerID; });
                }

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(strResult);
                HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(".//div[@class='txn_wrapper' and preceding-sibling::div[@id='Unsettled'] and following-sibling::div[@id='Forecast']]");
                HtmlNodeCollection htmlNodeCollection2 = htmlDocument.DocumentNode.SelectNodes(".//div[@class='txn_wrapper' and preceding-sibling::div[@id='Forecast'] and following-sibling::div[@id='Quinella']]");
                HtmlNodeCollection htmlNodeCollection3 = htmlDocument.DocumentNode.SelectNodes(".//div[@class='txn_wrapper' and preceding-sibling::div[@id='Quinella']]");
                if (this.m_bWinPlace && htmlNodeCollection != null)
                {
                    foreach (HtmlNode htmlNode_ in htmlNodeCollection)
                    {
                        this.ParseBet(htmlNode_, strPlayerID, "WIN/PLC", strUserName);
                    }
                }
                if (this.m_bForecast && htmlNodeCollection2 != null)
                {
                    foreach (HtmlNode htmlNode_2 in htmlNodeCollection2)
                    {
                        this.ParseBet(htmlNode_2, strPlayerID, "Forecast", strUserName);
                    }
                }
                if (this.m_bQuinella && htmlNodeCollection3 != null)
                {
                    foreach (HtmlNode htmlNode_3 in htmlNodeCollection3)
                    {
                        this.ParseBet(htmlNode_3, strPlayerID, "Quinella", strUserName);
                    }
                }
                if (this.m_bRaceEach && int_3 == 1)
                {
                    HtmlNodeCollection htmlNodeCollection4 = htmlDocument.DocumentNode.SelectNodes(".//div[@class='txn_wrapper_settled']/div[@class='Race_each']");
                    if (htmlNodeCollection4 != null && htmlNodeCollection4.Count > 0)
                    {
                        foreach (HtmlNode htmlNode_4 in htmlNodeCollection4)
                        {
                            this.ParseSettledList(htmlNode_4, strPlayerID, strUserName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_onWriteLogEvent(ex.Message);
            }
        }
        
        private void ParseBet(HtmlNode htmlNode_0, string strPlayer, string strMatchType, string strAgent)
        {
            try
            {
                string stadium = string.Empty;
                string racingnum = string.Empty;
                string country = string.Empty;
                string divid = string.Empty;
                DateTime date = default(DateTime);
                string matchID = string.Empty;

                

                HtmlNode htmlNode = htmlNode_0.SelectSingleNode(".//div[@class='lca_tote']/h3");
                if (htmlNode != null)
                {
                    stadium = htmlNode.InnerText;
                }
                HtmlNodeCollection htmlNodeCollection = htmlNode_0.SelectNodes(".//div[contains(@class, 'Race_')]");
                if (htmlNodeCollection != null && htmlNodeCollection.Count > 0)
                {
                    foreach (HtmlNode htmlNode2 in htmlNodeCollection)
                    {
                        HtmlNode htmlNode3 = htmlNode2.SelectSingleNode(".//div[@class='race_numbs']/dl/dd");
                        if (htmlNode3 != null)
                        {
                            racingnum = htmlNode3.InnerText.Trim();
                        }
                        HtmlNode htmlNode4 = htmlNode2.SelectSingleNode(".//div[@class='race_infodetails']");
                        if (htmlNode4 != null)
                        {
                            HtmlNode htmlNode5 = htmlNode4.SelectSingleNode(".//p");
                            HtmlNodeCollection htmlNodeCollection2 = htmlNode4.SelectNodes(".//dl/dt/span");
                            if (htmlNode5 != null)
                            {
                                country = htmlNode5.InnerText.Trim();
                                if (htmlNodeCollection2.Count > 1)
                                {
                                    divid = htmlNodeCollection2[1].InnerText.Trim();
                                    string s = htmlNodeCollection2[0].InnerText.Trim();
                                    date = DateTime.Parse(s);
                                }
                            }
                            else if (htmlNodeCollection2.Count > 2)
                            {
                                country = htmlNodeCollection2[0].InnerText.Trim();
                                divid = htmlNodeCollection2[2].InnerText.Trim();
                                string s2 = htmlNodeCollection2[1].InnerText.Trim();
                                date = global::System.DateTime.Parse(s2);
                            }
                        }
                        HtmlNode htmlNode6 = htmlNode2.SelectSingleNode(".//div[@class='overview_table']");
                        if (htmlNode6 != null)
                        {
                            matchID = htmlNode6.Id.Replace("quick_link_", "");
                        }
                        List<HorseItem> list = new List<HorseItem>();
                        HtmlNode htmlNode7 = htmlNode2.SelectSingleNode(".//table[contains(@name, 'tbl_detail')]");
                        if (htmlNode7 != null)
                        {
                            list = this.ParseHorseItemList(htmlNode7, 0, strMatchType);
                        }
                        foreach (HorseItem horse in list)
                        {
                            BetItem betItem = new BetItem(horse, strPlayer, strMatchType, matchID, stadium, country, divid, racingnum, strAgent, date);
                            lock (m_RunningBets)
                            {
                                m_RunningBets.Add(betItem);
                            }
                            int num = this.FindBetInUnSettledList(betItem);
                            bool b_country = this.CheckCountry(betItem);
                            if (!b_country)
                            {
                                // this.m_onWriteLogEvent(string.Format("blocked country: {0}", betItem.Country));
                            }
                            if (num == 1 && b_country)
                            {
                                BetData.Instance.m_OrderList.Add(betItem);

                                this.m_onWriteLogEvent(string.Format("Url Added: ID:{0}, Country:{1}, Stadium:{2}, Race:{3}, MatchID:{4}, Horse:{5}, Percent:{6}, Limit:{7}, {8}/{9}",
                                    betItem.PlayerID, betItem.Country, betItem.StadiumName, betItem.RacingNumber, betItem.MatchID, betItem.HorseNumber, betItem.Percent, betItem.Limit, betItem.Win, betItem.Place));
                            }
                            else if (num == 2)
                            {
                                string strQuery = string.Format("update unsettleditems set Win='{0}',Place='{1}',Tickets='{2}',Tax='{3}',Total='{4}' where MatchDate='{5}' and MatchID='{6}' and RacingNumber='{7}' and HorseNumber='{8}' and [Percent]='{9}' and Limited='{10}';", new object[]
                                {
                                    betItem.Win,
                                    betItem.Place,
                                    betItem.Tickets,
                                    betItem.Tax,
                                    betItem.Total,
                                    betItem.BetDate.ToString("yyyy-MM-dd"),
                                    betItem.MatchID,
                                    betItem.RacingNumber,
                                    betItem.HorseNumber,
                                    betItem.Percent,
                                    betItem.Limit
                                });
                                // global::DBUtility.MSSqlMng.Instance.UpdateQuery(strQuery);
                                this.m_onWriteLogEvent(string.Format("Url Updated: {0} {1}-{2}", betItem.Country, betItem.StadiumName, betItem.RacingNumber));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        private List<HorseItem> ParseHorseItemList(HtmlNode htmlNode_0, int int_3, string string_2)
        {
            List<HorseItem> list = new List<HorseItem>();
            try
            {
                HtmlNodeCollection htmlNodeCollection = htmlNode_0.SelectNodes(".//tr[@class='row_bet' or @class='row_eat']");
                if (htmlNodeCollection != null && htmlNodeCollection.Count > 0)
                {
                    foreach (HtmlNode htmlNode_ in htmlNodeCollection)
                    {
                        HorseItem horseItem = this.ParseHorseItem(htmlNode_, int_3, string_2);
                        if (horseItem != null)
                        {
                            list.Add(horseItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return list;
        }

        private HorseItem ParseHorseItem(HtmlNode htmlNode_0, int int_3, string string_2)
        {
            HorseItem result;
            try
            {
                HorseItem horseItem = new HorseItem();
                HtmlNodeCollection htmlNodeCollection = htmlNode_0.SelectNodes(".//td[not(contains(@class, 'title'))]");
                HtmlNode htmlNode = htmlNode_0.SelectSingleNode(".//td[@class='type']/span");
                if (htmlNode != null)
                {
                    string attributeValue = htmlNode.GetAttributeValue("onclick", "");
                    Match match = Regex.Match(attributeValue, "'(?<Url>[^']*)'");
                    horseItem.Url = match.Groups["Url"].Value;
                }
                if (htmlNodeCollection == null)
                {
                    result = null;
                }
                else
                {
                    if (int_3 == 0)
                    {
                        horseItem.Type = htmlNode.InnerText.Trim();
                        if (string_2 != null)
                        {
                            if (!(string_2 == "WIN/PLC"))
                            {
                                if (!(string_2 == "Forecast"))
                                {
                                    if (string_2 == "Quinella" && htmlNodeCollection.Count > 7)
                                    {
                                        horseItem.HorseNumber = htmlNodeCollection[0].InnerText;
                                        horseItem.Tickets = htmlNodeCollection[1].InnerText;
                                        horseItem.FType = htmlNodeCollection[2].InnerText;
                                        horseItem.Percent = htmlNodeCollection[3].InnerText;
                                        horseItem.Limit = htmlNodeCollection[4].InnerText;
                                        horseItem.Tax = htmlNodeCollection[5].InnerText;
                                        horseItem.Total = htmlNodeCollection[6].InnerText.Replace("(", "-").Replace(")", "").Replace("$", "").Replace(",", "");
                                    }
                                }
                                else if (htmlNodeCollection.Count > 7)
                                {
                                    horseItem.HorseNumber = htmlNodeCollection[0].InnerText;
                                    horseItem.Tickets = htmlNodeCollection[1].InnerText;
                                    horseItem.FType = htmlNodeCollection[2].InnerText;
                                    horseItem.Percent = htmlNodeCollection[3].InnerText;
                                    horseItem.Limit = htmlNodeCollection[4].InnerText;
                                    horseItem.Tax = htmlNodeCollection[5].InnerText;
                                    horseItem.Total = htmlNodeCollection[6].InnerText.Replace("(", "-").Replace(")", "").Replace("$", "").Replace(",", "");
                                }
                            }
                            else if (htmlNodeCollection.Count > 7)
                            {
                                horseItem.HorseNumber = htmlNodeCollection[0].InnerText;
                                horseItem.Win = htmlNodeCollection[1].InnerText;
                                horseItem.Place = htmlNodeCollection[2].InnerText;
                                horseItem.Percent = htmlNodeCollection[3].InnerText;
                                horseItem.Limit = htmlNodeCollection[4].InnerText;
                                horseItem.Tax = htmlNodeCollection[5].InnerText;
                                horseItem.Total = htmlNodeCollection[6].InnerText.Replace("(", "-").Replace(")", "").Replace("$", "").Replace(",", "");
                            }
                        }
                    }
                    else if (string_2 != null)
                    {
                        if (!(string_2 == "WIN/PLC"))
                        {
                            if (!(string_2 == "Forecast"))
                            {
                                if (string_2 == "Quinella" && htmlNodeCollection.Count > 10)
                                {
                                    horseItem.FType = htmlNodeCollection[0].InnerText;
                                    horseItem.HorseNumber = htmlNodeCollection[1].InnerText;
                                    horseItem.Tickets = htmlNodeCollection[2].InnerText;
                                    horseItem.Percent = htmlNodeCollection[3].InnerText;
                                    horseItem.Limit = htmlNodeCollection[4].InnerText;
                                    horseItem.Tax = htmlNodeCollection[5].InnerText;
                                    horseItem.Total = htmlNodeCollection[6].InnerText.Replace("(", "-").Replace(")", "").Replace("$", "").Replace(",", "");
                                    horseItem.Type = htmlNodeCollection[10].InnerText;
                                }
                            }
                            else if (htmlNodeCollection.Count > 10)
                            {
                                horseItem.FType = htmlNodeCollection[0].InnerText;
                                horseItem.HorseNumber = htmlNodeCollection[1].InnerText;
                                horseItem.Tickets = htmlNodeCollection[2].InnerText;
                                horseItem.Percent = htmlNodeCollection[3].InnerText;
                                horseItem.Limit = htmlNodeCollection[4].InnerText;
                                horseItem.Tax = htmlNodeCollection[5].InnerText;
                                horseItem.Total = htmlNodeCollection[6].InnerText.Replace("(", "-").Replace(")", "").Replace("$", "").Replace(",", "");
                                horseItem.Type = htmlNodeCollection[10].InnerText;
                            }
                        }
                        else if (htmlNodeCollection.Count > 10)
                        {
                            horseItem.HorseNumber = htmlNodeCollection[0].InnerText;
                            horseItem.Win = htmlNodeCollection[1].InnerText;
                            horseItem.Place = htmlNodeCollection[2].InnerText;
                            horseItem.Percent = htmlNodeCollection[3].InnerText;
                            horseItem.Limit = htmlNodeCollection[4].InnerText;
                            horseItem.Tax = htmlNodeCollection[5].InnerText;
                            horseItem.Total = htmlNodeCollection[6].InnerText.Replace("(", "-").Replace(")", "").Replace("$", "").Replace(",", "");
                            horseItem.Type = htmlNodeCollection[11].InnerText;
                        }
                    }
                    result = horseItem;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = null;
            }
            return result;
        }

        private int FindBetInUnSettledList(BetItem betItem_0)
        {
            int result;
            try
            {
                BetItem betItem = BetData.Instance.m_OrderList.Find((BetItem item) => { return item.Country == betItem_0.Country && item.PlayerID == betItem_0.PlayerID && item.StadiumName == betItem_0.StadiumName && item.MatchID == betItem_0.MatchID && item.RacingNumber == betItem_0.RacingNumber && item.HorseNumber == betItem_0.HorseNumber && item.Percent == betItem_0.Percent && item.Limit == betItem_0.Limit && item.Win == betItem_0.Win && item.Place == betItem_0.Place; });
                if (betItem == null)
                {
                    result = 1;
                }
                else if (betItem.Win != betItem_0.Win || betItem.Place != betItem_0.Place || betItem.Tickets != betItem_0.Tickets || betItem.Tax != betItem_0.Tax)
                {
                    /*betItem.Win = betItem_0.Win;
                    betItem.Place = betItem_0.Place;
                    betItem.Tickets = betItem_0.Tickets;
                    betItem.Tax = betItem_0.Tax;
                    string.Format("", new object[0]);
                    betItem.IsScrapped = false;*/
                    result = 2;
               
                }
                else
                {
                    result = 0;
                 
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = 0;
            }
            return result;
        }

        private bool CheckCountry(BetItem betItem_0)
        {
            bool flag = false;
            bool result;
            if (betItem_0 == null)
            {
                result = flag;
            }
            else
            {
                try
                {
                    if (Settings.GetSettings.isSG && betItem_0.StadiumName == "Singapore")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isMY && betItem_0.StadiumName == "Malaysia")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isHK && betItem_0.StadiumName == "Hong Kong")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isMC && betItem_0.StadiumName == "Macau")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isAU && betItem_0.Country == "AU")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isJP && betItem_0.Country == "Japan")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isUK && betItem_0.Country == "UK")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isUS && betItem_0.Country == "USA")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isNZ && betItem_0.Country == "NZ")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isFR && betItem_0.Country == "FR")
                    {
                        return true;
                    }
                    if (Settings.GetSettings.isSW && betItem_0.Country == "SW")
                    {
                        return true;
                    }
                    
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
                result = flag;
            }
            return result;
        }

        private void ParseSettledList(HtmlNode htmlNode_0, string string_2, string string_3)
        {
            try
            {
                HtmlNode htmlNode = htmlNode_0.SelectSingleNode(".//div[@class='lca_date']/h3");
                DateTime racingDate = DateTime.Now;
                if (htmlNode != null)
                {
                    racingDate = DateTime.Parse(htmlNode.InnerText);
                }
                HtmlNodeCollection htmlNodeCollection = htmlNode_0.SelectNodes(".//table[@class='max_report']/tr");
                if (htmlNodeCollection != null)
                {
                    foreach (HtmlNode htmlNode2 in htmlNodeCollection)
                    {
                        try
                        {
                            SettledMatchItem settledMatchItem_0 = new SettledMatchItem();
                            settledMatchItem_0 = new SettledMatchItem();
                            settledMatchItem_0.RacingDate = racingDate;
                            settledMatchItem_0.IsScrapped = false;
                            settledMatchItem_0.PlayerID = string_2;
                            settledMatchItem_0.AgentName = string_3;

                            HtmlNodeCollection htmlNodeCollection2 = htmlNode2.SelectNodes(".//td");
                            if (htmlNodeCollection2 != null && htmlNodeCollection2.Count > 5)
                            {
                                settledMatchItem_0.Location = htmlNodeCollection2[0].InnerText.Trim();
                                settledMatchItem_0.Tote = htmlNodeCollection2[1].InnerText.Trim();
                                settledMatchItem_0.TotalTickets = htmlNodeCollection2[2].InnerText.Trim();
                                settledMatchItem_0.TotalTax = htmlNodeCollection2[3].InnerText.Trim();
                                settledMatchItem_0.Profit = htmlNodeCollection2[4].InnerText.Trim();
                                HtmlNode htmlNode3 = htmlNodeCollection2[5].SelectSingleNode(".//span[@class='txn_view']");
                                if (htmlNode3 != null)
                                {
                                    string value = htmlNode3.Attributes["onclick"].Value;
                                    Match match = Regex.Match(value, "'(?<Url>[^']*)'[^']*'(?<Type>[^']*)");
                                    settledMatchItem_0.URL = match.Groups["Url"].Value;
                                    settledMatchItem_0.Type = match.Groups["Type"].Value;
                                }
                            }
                            if (!string.IsNullOrEmpty(settledMatchItem_0.Type) && BetData.Instance.m_SettledMatchList.Find((SettledMatchItem p)=> { return p.URL == settledMatchItem_0.URL; }) == null)
                            {
                                BetData.Instance.m_SettledMatchList.Add(settledMatchItem_0);
                                this.m_onWriteLogEvent(string.Format("Settled Event: {0} {1}-{2} is added", settledMatchItem_0.Location, settledMatchItem_0.Tote, settledMatchItem_0.Type));
                            }
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex2)
            {
                string message2 = ex2.Message;
            }
        }

        public void MakeToDoBetList()
        {
            try
            {
                for (; ; )
                {
                    if (BetData.Instance.m_OrderList == null || BetData.Instance.m_OrderList.Count == 0)
                    {
                        Thread.Sleep(300);
                        continue;
                    }
                    
                    for (int i = 0; i < BetData.Instance.m_OrderList.Count; i++)
                    {
                        try
                        {
                            BetItem betItem = BetData.Instance.m_OrderList[i];

                            if (betItem.IsScrapped)
                            {
                                continue;
                            }
                            this.m_AgentSession.SingHttpClient.DefaultRequestHeaders.Referrer = new Uri(string.Format("{0}/history.jsp?uid=", Settings.GetSettings.url2, betItem.PlayerID));
                            string url = string.Format("{0}/{1}", Settings.GetSettings.url2, betItem.Url);
                            HttpResponseMessage result = this.m_AgentSession.SingHttpClient.GetAsync(url).Result;
                            
                            string result2 = result.Content.ReadAsStringAsync().Result;
                            // ForTEST
                            /*if(i == 0)
                                result2 = File.ReadAllText("a.html");
                            else
                                result2 = File.ReadAllText("b.html");*/
                            if (string.IsNullOrEmpty(result2))
                            {
                                continue;
                            }
                            List<BetItem> list = this.ParseBetItemList(betItem, result2);
                            
                            using (List<BetItem>.Enumerator enumerator = list.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    BetItem betItem_0 = enumerator.Current;
                                    
                                    BetItem exitItem = BetData.Instance.m_ToDoBetList.Find((BetItem item) => { return item.Country == betItem_0.Country && item.PlayerID == betItem_0.PlayerID && item.StadiumName == betItem_0.StadiumName && item.MatchID == betItem_0.MatchID && item.RacingNumber == betItem_0.RacingNumber && item.HorseNumber == betItem_0.HorseNumber && item.Percent == betItem_0.Percent && item.Limit == betItem_0.Limit && item.Win3 == betItem_0.Win && item.Place3 == betItem_0.Place; });
                                    // ForTEST
                                    if (exitItem == null && betItem_0.BetDate.AddMinutes(5.0) > DateTime.Now)
                                    {
                                        BetSettingItem betSettingItem = Settings.GetSettings.FindBetSettingItem(betItem_0.PlayerID);

                                        betItem_0.Win3 = betItem_0.Win;
                                        betItem_0.Place3 = betItem_0.Place;

                                        betItem_0.Win = betItem_0.Win * betSettingItem.Times;
                                        betItem_0.Place = betItem_0.Place * betSettingItem.Times;

                                        if (betItem_0.Win > 0)
                                            betItem_0.Win = Math.Max(1, (int)betItem_0.Win);
                                        if (betItem_0.Place > 0)
                                            betItem_0.Place = Math.Max(1, (int)betItem_0.Place);

                                        betItem_0.Win2 = betItem_0.Win;
                                        betItem_0.Place2 = betItem_0.Place;

                                        BetData.Instance.m_ToDoBetList.Add(betItem_0);
                                        string txtWinPlace = (betItem_0.MatchType == "WIN/PLC") ? string.Format("{0}/{1}", betItem_0.Win, betItem_0.Place) : betItem_0.Tickets.ToString();
                                        this.m_onWriteLogEvent(string.Format("UnSettled Bet:({0}) Country:{1}, Stadium:{2}, Type:{3}, Race:{4}, Horse:{5}, {6}, {7}) is added", new object[]
                                        {
                                            betItem_0.PlayerID,
                                            betItem_0.Country,
                                            betItem_0.StadiumName,
                                            betItem_0.MatchType,
                                            betItem_0.RacingNumber,
                                            betItem_0.HorseNumber,
                                            betItem_0.Type,
                                            txtWinPlace
                                        }));

                                    }
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }

                    }
                    
                    Thread.Sleep(300);
                }
            }
            catch (Exception ex2)
            {
                string message2 = ex2.Message;
            }
        }

        private List<BetItem> ParseBetItemList(BetItem betItem_0, string strResponse)
        {
            List<BetItem> list = new List<BetItem>();
            try
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(strResponse);
                HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes(".//table[@class='max_report']/tr");
                if (htmlNodeCollection == null)
                {
                    return list;
                }
                foreach (HtmlNode htmlNode in htmlNodeCollection)
                {
                    
                    HtmlNodeCollection htmlNodeCollection2 = htmlNode.SelectNodes(".//td");
                    if (htmlNodeCollection2 != null)
                    {
                        DateTime tt = Utils.ParseToDateTime(htmlNodeCollection2[0].InnerText.Trim(), "dd-MM-yyyy HH:mm:ss");
                        BetItem betItem = list.Find((BetItem item)=> { return item.BetDate == tt; });
                        if (betItem == null)
                        {
                            betItem = new BetItem();
                            betItem.PlayerID = betItem_0.PlayerID;
                            betItem.MatchID = betItem_0.MatchID;
                            betItem.MatchType = betItem_0.MatchType;
                            betItem.StadiumName = betItem_0.StadiumName;
                            betItem.Country = betItem_0.Country;
                            betItem.Animal = betItem_0.Animal;
                            betItem.Dividend = betItem_0.Dividend;
                            betItem.RacingNumber = betItem_0.RacingNumber;
                            betItem.BetDate = tt;
                            betItem.LogTime = TimeClass.GetLogTime(tt);
                            betItem.HorseNumber = betItem_0.HorseNumber;
                            betItem.Percent = betItem_0.Percent;
                            betItem.Limit = betItem_0.Limit;
                            betItem.Tax = betItem_0.Tax;
                            betItem.Total = betItem_0.Total;
                            betItem.FType = betItem_0.FType;
                            betItem.Type = betItem_0.Type;
                            betItem.TransactionID = betItem_0.TransactionID;
                            betItem.Url = betItem_0.Url;
                            betItem.RaceDate = betItem_0.RaceDate;
                            betItem.RaceType = betItem_0.RaceType;
                            betItem.IsPlaced = false;
                            string matchType = betItem_0.MatchType;
                            string text = matchType;
                            if (text != null)
                            {
                                if (!(text == "WIN/PLC"))
                                {
                                    if (!(text == "Forecast"))
                                    {
                                        if (text == "Quinella" && htmlNodeCollection2.Count > 3)
                                        {
                                            betItem.Tickets = Utils.ParseToDouble(htmlNodeCollection2[3].InnerText.Trim());
                                        }
                                    }
                                    else if (htmlNodeCollection2.Count > 3)
                                    {
                                        betItem.Tickets = Utils.ParseToDouble(htmlNodeCollection2[3].InnerText.Trim());
                                    }
                                }
                                else if (htmlNodeCollection2.Count > 4)
                                {
                                    betItem.Win = Utils.ParseToDouble(htmlNodeCollection2[3].InnerText.Trim());
                                    betItem.Place = Utils.ParseToDouble(htmlNodeCollection2[4].InnerText.Trim());
                                }
                            }
                            list.Add(betItem);
                        }
                        else
                        {
                            string matchType2 = betItem_0.MatchType;
                            string text2 = matchType2;
                            if (text2 != null)
                            {
                                if (!(text2 == "WIN/PLC"))
                                {
                                    if (!(text2 == "Forecast"))
                                    {
                                        if (text2 == "Quinella")
                                        {
                                            betItem.Tickets += Utils.ParseToDouble(htmlNodeCollection2[3].InnerText);
                                        }
                                    }
                                    else
                                    {
                                        betItem.Tickets += Utils.ParseToDouble(htmlNodeCollection2[3].InnerText);
                                    }
                                }
                                else
                                {
                                    betItem.Win += Utils.ParseToDouble(htmlNodeCollection2[3].InnerText);
                                    betItem.Place += Utils.ParseToDouble(htmlNodeCollection2[4].InnerText);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return list;
        }

        public void DoBettingWork()
        {
            int counter = 0;
            for (; ; )
            {
                if(counter == 0)
                {
                    this.m_RaceInfoList = this.ParseRaceInfo();
                }
                counter = (counter + 1) % 50;
                if (BetData.Instance.m_ToDoBetList.Count == 0)
                {
                    Thread.Sleep(5000);
                    continue;
                }
                                
                List<BetItem> list = BetData.Instance.m_ToDoBetList.FindAll((BetItem betItem_0) => { return !betItem_0.IsPlaced; }).ToList();
                if (list == null || list.Count == 0)
                {
                    Thread.Sleep(5000);
                    continue;
                }
                
                for (int i = 0; i < list.Count; i++)
                {
                    RaceInfo raceInfo = this.GetRaceInfo(list[i]);
                    if (raceInfo != null)
                    {
                        /*this.m_onWriteLogEvent(string.Format("Found Race Item({0}){1} - {2} ({3})", new object[]
                        {
                            list[i].PlayerID,
                            list[i].Country,
                            list[i].StadiumName,
                            list[i].Dividend
                        }));*/
                        BetSettingItem betSettingItem = Settings.GetSettings.FindBetSettingItem(list[i].PlayerID);
                        string t = (!betSettingItem.Type) ? ((list[i].Type == "Bet") ? "bet" : "book") : ((list[i].Type == "Bet") ? "book" : "bet");
                        if (betSettingItem != null)
                        {
                            string strStake = String.Format("{0}/{1}", list[i].Win, list[i].Place);
                            BetInfo betInfo = new BetInfo(list[i].PlayerID, list[i].RacingNumber.Replace("Race", "").Trim(), list[i].HorseNumber, strStake, t);
                            string win = "";
                            string place = "";
                            string limit = "";
                            string type = "";
                            int betRst = this.DoBetOrEat(raceInfo, betInfo, list[i], ref win, ref place, ref limit, ref type);
                            if (betRst == 1)
                            {
                                list[i].Win -= double.Parse(win);
                                list[i].Place -= double.Parse(place);
                                if(list[i].Win == 0 && list[i].Place == 0)
                                {
                                    list[i].IsPlaced = true;
                                    this.m_onWriteLogEvent(string.Format("UnsettledBet completed:  -->  {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}/{8}",
                                        betInfo.player,
                                        raceInfo.country,
                                        raceInfo.aniType,
                                        raceInfo.location,
                                        betInfo.race,
                                        betInfo.horse,
                                        type,
                                        list[i].Win2,
                                        list[i].Place2
                                        ));

                                    string parameter = string.Format("Betting Successed  -->  {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}/{8}", new object[]
                                    {
                                        list[i].PlayerID,
                                        raceInfo.country,
                                        raceInfo.aniType,
                                        raceInfo.location,
                                        betInfo.race,
                                        betInfo.horse,
                                        type,
                                        list[i].Win2,
                                        list[i].Place2
                                    });
                                    Thread thread = new Thread(new ParameterizedThreadStart(this.SendEmail));
                                    thread.Start(parameter);
                                }
                                /*BetResult rst = BetData.Instance.m_SettledBetList.Find((BetResult item) => { return item.player == list[i].PlayerID && 
                                    item.strDate == list[i].RaceDate &&
                                    item.country == list[i].Country && 
                                    item.location == list[i].StadiumName && 
                                    item.animal == raceInfo.aniType && 
                                    item.race == betInfo.race && 
                                    item.horse == betInfo.horse && 
                                    item.limit == list[i].Limit &&
                                    item.type == list[i].Type &&
                                    ; });*/
                                BetResult betResult = new BetResult();
                                betResult.player = list[i].PlayerID;
                                betResult.strDate = list[i].RaceDate;
                                betResult.country = list[i].Country;
                                betResult.location = list[i].StadiumName;
                                betResult.animal = raceInfo.aniType;
                                betResult.race = betInfo.race;
                                betResult.horse = betInfo.horse;
                                betResult.win = win;
                                betResult.place = place;
                                betResult.limit = limit;
                                betResult.type = list[i].Type;
                                betResult.betTime = DateTime.Now;
                                betResult.strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                BetData.Instance.m_SettledList.Add(betResult);
                                this.m_onOneBetEvent(list[i]);

                                
                            }
                            else
                            {
                                BetItem betItem = list[i];
                                int failedCount = betItem.FailedCount;
                                betItem.FailedCount = failedCount + 1;
                            }
                            BetItem originItem = m_RunningBets.Find((BetItem item) => { return item.Country == list[i].Country && item.PlayerID == list[i].PlayerID && item.StadiumName == list[i].StadiumName && item.MatchID == list[i].MatchID && item.RacingNumber == list[i].RacingNumber && item.HorseNumber == list[i].HorseNumber && item.Percent == list[i].Percent && item.Limit == list[i].Limit; });
                            if (originItem == null)
                            {
                                list[i].IsPlaced = true;
                                this.m_onWriteLogEvent(string.Format("UnsettledBet timeout:  -->  {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}: {7}/{8} of {9}/{10}",
                                        betInfo.player,
                                        raceInfo.country,
                                        raceInfo.aniType,
                                        raceInfo.location,
                                        betInfo.race,
                                        betInfo.horse,
                                        type,
                                        list[i].Win,
                                        list[i].Place,
                                        list[i].Win2,
                                        list[i].Place2
                                        ));
                            }
                        }
                    }
                    else
                    {
                        Trace.WriteLine(string.Format("Not found match id-{0}, strTime-{1}, RaceList-{2}", list[i].RaceType, list[i].RaceDate, JsonConvert.SerializeObject(this.m_RaceInfoList)));
                        this.m_onWriteLogEvent(string.Format("Not found Race Item {0} - {1} ({2})", list[i].Country, list[i].StadiumName, list[i].Dividend));
                        this.m_RaceInfoList = this.ParseRaceInfo();
                        counter = 1;
                    }
                }
                
                
                Thread.Sleep(5000);
            }
        }
        private void SendEmail(object object_0)
        {
            try
            {
                string content = object_0 as string;
                SendSMS(content);
                SendEmail("Citibet Bot", "New bet has been placed!", content);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }
        public void SendSMS(string content)
        {
            try
            {
                global::Twilio.TwilioClient.Init(Settings.GetSettings.token2, Settings.GetSettings.token3);
                global::Twilio.Types.PhoneNumber phoneNumber = new global::Twilio.Types.PhoneNumber(Settings.GetSettings.app_phone);
                global::Twilio.Rest.Api.V2010.Account.MessageResource messageResource = global::Twilio.Rest.Api.V2010.Account.MessageResource.Create(new global::Twilio.Types.PhoneNumber(Settings.GetSettings.phone), null, phoneNumber, null, content, null, null, null, null, null, null, null, null, null, null, null, null, null);
                global::System.Console.WriteLine(messageResource.Sid);
            }
            catch (global::System.Exception ex)
            {
                string message = ex.Message;
            }
        }

        // Token: 0x06000152 RID: 338 RVA: 0x0000E814 File Offset: 0x0000CA14
        public void SendEmail(string sender, string subject, string content)
        {
            try
            {
                global::System.Net.Mail.SmtpClient smtpClient = new global::System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = global::System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new global::System.Net.NetworkCredential(Settings.GetSettings.app_mail, Settings.GetSettings.app_pass);
                global::System.Net.Mail.MailAddress from = new global::System.Net.Mail.MailAddress(Settings.GetSettings.app_mail, sender, global::System.Text.Encoding.UTF8);
                global::System.Net.Mail.MailAddress to = new global::System.Net.Mail.MailAddress(Settings.GetSettings.mail);
                global::System.Net.Mail.MailMessage mailMessage = new global::System.Net.Mail.MailMessage(from, to);
                mailMessage.Body = content;
                mailMessage.BodyEncoding = global::System.Text.Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.SubjectEncoding = global::System.Text.Encoding.UTF8;
                try
                {
                    smtpClient.Send(mailMessage);
                    mailMessage.Dispose();
                }
                catch (global::System.Exception ex)
                {
                    string message = ex.Message;
                }
            }
            catch (global::System.Exception ex2)
            {
                string message2 = ex2.Message;
            }
        }

        private double GetTickets(BetItem betItem_0)
        {
            double result = 0.0;
            try
            {
                result = ((betItem_0.MatchType == "WIN/PLC") ? ((betItem_0.Win > betItem_0.Place) ? betItem_0.Win : betItem_0.Place) : betItem_0.Tickets);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return result;
        }
        private int DoBetOrEat(RaceInfo raceInfo, BetInfo betInfo, BetItem betItem, ref string strWin, ref string strPlace, ref string strLimit, ref string outputType)
        {
            int result;
            try
            {
                int num = 0;
                RaceData raceData = this.ParseOrders(raceInfo);
                Uri uri = new Uri(raceInfo.link);
                string txtRaceDate = HttpUtility.ParseQueryString(uri.Query).Get("race_date");
                string txtRaceType = HttpUtility.ParseQueryString(uri.Query).Get("race_type");
                if (raceData == null)
                {
                    result = 0;
                }
                else
                {
 
                    string string_5 = "";
                    
                    string inputType = betInfo.type;
                    if (inputType != null)
                    {
                        if (!(inputType == "bet"))
                        {
                            if (inputType == "book")
                            {
                                outputType = "Bet";
                                List<PendingData> list = this.FindPendingData(raceData.eData, betInfo, 1);
                                /*this.m_onWriteLogEvent(string.Format("Try betting for {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}", 
                                    betInfo.player,
                                    raceInfo.country,
                                    raceInfo.aniType,
                                    raceInfo.location,
                                    betInfo.race,
                                    betInfo.horse,
                                    outputType,
                                    betInfo.stake
                                ));*/
                                if (list == null || list.Count <= 0)
                                {
                                    /*this.m_onWriteLogEvent(string.Format("Cannot find appropriate market for {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}",                                    
                                        betInfo.player, raceInfo.country, raceInfo.aniType, raceInfo.location, betInfo.race, betInfo.horse, outputType, betInfo.stake
                                    ));*/
                                    return 2;
                                }

                                int index = -1;
                                int max_steak = 0;
                                for(int i = 0; i <list.Count; i++)
                                {
                                    if(list[i].Limit.Split('/')[0] == "0" || list[i].Limit.Split('/')[1] == "0")
                                    {
                                        Console.WriteLine("Error");
                                    }
                                    int steak = Math.Max(int.Parse(list[i].Win), int.Parse(list[i].Plc));
                                    if (max_steak < steak)
                                    {
                                        max_steak = steak;
                                        index = i;
                                    }
                                }
                                index = new Random().Next(0, list.Count - 1);
                                PendingData pendingData = list[index];

                                int p_Win = Math.Min(int.Parse(pendingData.Win), (int)betItem.Win),
                                    p_Place = Math.Min(int.Parse(pendingData.Plc), (int)betItem.Place);
                              
                                strWin = p_Win.ToString();
                                strPlace = p_Place.ToString();
                                strLimit = pendingData.Limit;
                             
                                this.m_onWriteLogEvent(string.Format("Sending request for {0} {1}({2}) {3}, Race {4}, Horse {5}, Limit:{6}, {7}-{8}", new object[]
                                {
                                    betInfo.player,
                                    raceInfo.country,
                                    raceInfo.aniType,
                                    raceInfo.location,
                                    betInfo.race,
                                    betInfo.horse,
                                    pendingData.Limit,
                                    outputType,
                                    betInfo.stake
                                }));

                                string requestUri = string.Format("{0}/erequest?t=frm&race={1}&horse={2}&win={3}&place={4}&amount={5}&limit={6}&type=book&race_type={7}&race_date={8}&show={9}&post=1&rd={10}", new object[]
                                {
                                    Settings.GetSettings.url2,
                                    betInfo.race,
                                    betInfo.horse,
                                    strWin,
                                    strPlace,
                                    pendingData.Per,
                                    pendingData.Limit,
                                    txtRaceType,
                                    txtRaceDate,
                                    betInfo.race,
                                    this.GenerateRandomString()
                                });
                                // ForTEST
                                HttpResponseMessage result2 = this.UserSession.SingHttpClient.GetAsync(requestUri).Result;
                                string_5 = result2.Content.ReadAsStringAsync().Result;
                                // string_5 = "Your EAT order has been fully transacted";
                            }
                        }
                        else
                        {
                            outputType = "Eat";
                            List<PendingData> list2 = this.FindPendingData(raceData.bData, betInfo, 1);
                            /*this.m_onWriteLogEvent(string.Format("Try betting for {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}", new object[]
                            {
                                betInfo.player,
                                raceInfo.aniType,
                                raceInfo.country,
                                raceInfo.location,
                                betInfo.race,
                                betInfo.horse,
                                outputType,
                                betInfo.stake
                            }));*/
                            
                            if (list2 == null || list2.Count <= 0)
                            {
                                /*this.m_onWriteLogEvent(string.Format("Cannot find appropriate market for {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}", new object[]
                                {
                                    betInfo.player, raceInfo.country, raceInfo.aniType, raceInfo.location, betInfo.race, betInfo.horse, outputType, betInfo.stake
                                }));*/
                                return 2;
                            }
                            int index = -1;
                            int max_steak = 0;
                            for (int i = 0; i < list2.Count; i++)
                            {
                                if (list2[i].Limit.Split('/')[0] == "0" || list2[i].Limit.Split('/')[1] == "0")
                                {
                                    Console.WriteLine("Error");
                                }
                                int steak = Math.Max(int.Parse(list2[i].Win), int.Parse(list2[i].Plc));
                                if (max_steak < steak)
                                {
                                    max_steak = steak;
                                    index = i;
                                }
                            }
                            PendingData pendingData2 = list2[index];
                            
                            int p_Win = Math.Min(int.Parse(pendingData2.Win), (int)betItem.Win),
                                p_Place = Math.Min(int.Parse(pendingData2.Plc), (int)betItem.Place);

                            strWin = p_Win.ToString();
                            strPlace = p_Place.ToString();
                            strLimit = pendingData2.Limit;
     
                            this.m_onWriteLogEvent(string.Format("Sending request for {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}", new object[]
                            {
                                betInfo.player,
                                raceInfo.country,
                                raceInfo.aniType,
                                raceInfo.location,
                                betInfo.race,
                                betInfo.horse,
                                outputType,
                                betInfo.stake
                            }));
                            string requestUri2 = string.Format("{0}/brequest?t=frm&race={1}&horse={2}&win={3}&place={4}&amount={5}&limit={6}&type=bet&race_type={7}&race_date={8}&show={9}&post=1&rd={10}", new object[]
                            {
                                Settings.GetSettings.url2,
                                betInfo.race,
                                betInfo.horse,
                                strWin,
                                strPlace,
                                pendingData2.Per,
                                pendingData2.Limit,
                                txtRaceType,
                                txtRaceDate,
                                betInfo.race,
                                this.GenerateRandomString()
                            });
                            // ForTEST
                            HttpResponseMessage result3 = this.UserSession.SingHttpClient.GetAsync(requestUri2).Result;
                            string_5 = result3.Content.ReadAsStringAsync().Result;
                            // string_5 = "Your EAT order has been fully transacted";

                        }
                    }
                    switch (this.ParseRequestREsult(string_5))
                    {
                        case 1:
                            this.m_onWriteLogEvent(string.Format("Betting Successed  -->  {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}/{8}", new object[]
                            {
                                betInfo.player,
                                raceInfo.country,
                                raceInfo.aniType,
                                raceInfo.location,
                                betInfo.race,
                                betInfo.horse,
                                outputType,
                                strWin,
                                strPlace
                            }));
                            this.Mp3Play();
                            return 1;
                        case 3:
                            this.m_onWriteLogEvent(string.Format("Insufficient credit  -->  {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}", new object[]
                            {
                                betInfo.player,
                                raceInfo.country,
                                raceInfo.aniType,
                                raceInfo.location,
                                betInfo.race,
                                betInfo.horse,
                                outputType,
                                betInfo.stake
                                }));
                            return 3;
                        case 4:
                            string_5 = string_5.Replace("&nbsp;", "");
                            string_5 = string_5.Replace("<span style=\"color:#FF0000\"><strong>", "");
                            strWin = Regex.Match(string_5, "Win:([^<])").Groups[1].Value;
                            strPlace = Regex.Match(string_5, "Plc:([^<])").Groups[1].Value;

                            this.m_onWriteLogEvent(string.Format("Betting Successed(Partially)  -->  {0} {1}({2}) {3}, Race {4}, Horse{5}, {6}-{7}/{8}", new object[]
                            {
                                betInfo.player,
                                raceInfo.country,
                                raceInfo.aniType,
                                raceInfo.location,
                                betInfo.race,
                                betInfo.horse,
                                outputType,
                                strWin,
                                strPlace
                            }));
                            this.m_onWriteLogEvent(string_5);
                            this.Mp3Play();
                            return 1;
                    }
                    result = num;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = -1;
            }
            return result;
        }

        public void Mp3Play()
        {
            try
            {
                global::System.Media.SoundPlayer soundPlayer = new global::System.Media.SoundPlayer(this.string_1);
                soundPlayer.Play();
            }
            catch (global::System.Exception ex)
            {
                string message = ex.Message;
            }
        }

        private int ParseRequestREsult(string string_2)
        {
            int result;
            try
            {
                global::System.Diagnostics.Trace.WriteLine(string_2);
                if (string.IsNullOrEmpty(string_2))
                {
                    result = 2;
                }
                else if (string_2.Contains("transacted"))
                {
                    if(string_2.Contains("has been fully transacted"))
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 4;
                    }
                }
                else if (string_2.Contains("Insufficient credit"))
                {
                    result = 3;
                }
                else
                {
                    this.m_onWriteLogEvent(string_2);
                    result = 0;
                }
            }
            catch (global::System.Exception ex)
            {
                string message = ex.Message;
                result = -1;
            }
            return result;
        }

        private string GenerateRandomString()
        {
            string result;
            try
            {
                string text = "0.";
                Random random = new Random();
                for (int i = 0; i < 16; i++)
                {
                    text += random.Next(10).ToString();
                }
                result = text;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = "";
            }
            return result;
        }
        private List<PendingData> FindPendingData(List<PendingData> list_5, BetInfo betInfo, int int_3 = 0)
        {
            List<PendingData> result;
            try
            {
                List<PendingData> list = new List<PendingData>();
                List<PendingData> list2 = list_5.FindAll((PendingData d)=> { return d.Hs == betInfo.horse && d.Rc == betInfo.race; });
                if (list2 == null || list2.Count == 0 || int_3 == 0)
                {
                    result = list2;
                }
                else
                {
                    foreach (PendingData pendingData in list2)
                    {
                        string[] str_tmp = betInfo.stake.Split('/');
                        string[] dst_arr = pendingData.Limit.Split('/');
                        if(dst_arr[0] != "0" && dst_arr[1] != "0")
                        {
                            if(str_tmp[0] != "0" && str_tmp[1] != "0")
                            {
                                list.Add(pendingData);
                            }
                        }
                        if(dst_arr[0] == "0" && dst_arr[1] != "0")
                        {
                            if (str_tmp[0] == "0" && str_tmp[1] != "0")
                            {
                                list.Add(pendingData);
                            }
                        }
                        if (dst_arr[0] != "0" && dst_arr[1] == "0")
                        {
                            if (str_tmp[0] != "0" && str_tmp[1] == "0")
                            {
                                list.Add(pendingData);
                            }
                        }
                    }
                    result = list;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = null;
            }
            return result;
        }

        private bool method_23(string string_2)
        {
            bool result;
            try
            {
                string[] array = string_2.Split(new char[]
                {
                    '/'
                });
                if (array.Length == 2 && (array[0] == "0" || array[1] == "0"))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = false;
            }
            return result;
        }

        private RaceData ParseOrders(RaceInfo raceInfo_0)
        {
            RaceData result3;
            try
            {
                HttpResponseMessage result = this.UserSession.SingHttpClient.GetAsync(this.GetRaceURL(raceInfo_0.link, "bdata")).Result;
                result.EnsureSuccessStatusCode();
                string result2 = result.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(result2))
                {
                    result3 = null;
                }
                else
                {
                    List<PendingData> bData = this.ParsePendingData(result2);
                    HttpResponseMessage result4 = this.UserSession.SingHttpClient.GetAsync(this.GetRaceURL(raceInfo_0.link, "edata")).Result;
                    result4.EnsureSuccessStatusCode();
                    string result5 = result4.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrEmpty(result5))
                    {
                        result3 = null;
                    }
                    else
                    {
                        List<PendingData> eData = this.ParsePendingData(result5);
                        result3 = new RaceData
                        {
                            bData = bData,
                            eData = eData
                        };
                    }
                }
            }
            catch (Exception)
            {
                result3 = null;
            }
            return result3;
        }
        private List<PendingData> ParsePendingData(string string_2)
        {
            List<PendingData> result;
            try
            {
                List<PendingData> list = new List<PendingData>();
                JObject jobject = JObject.Parse(string_2.Replace("c(", "").Replace(");", ""));
                if (jobject == null || jobject["pendingData"] == null)
                {
                    result = null;
                }
                else
                {
                    string text = jobject["pendingData"].ToString();
                    if (string.IsNullOrEmpty(text))
                    {
                        result = list;
                    }
                    else
                    {
                        List<string> list2 = text.Split(new string[]
                        {
                            "\n"
                        }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (list2 == null || list2.Count < 1)
                        {
                            result = list;
                        }
                        else
                        {
                            foreach (string text2 in list2)
                            {
                                List<string> list3 = text2.Split(new string[]
                                {
                                    "\t"
                                }, StringSplitOptions.None).ToList();
                                if (list3 != null && list3.Count >= 6)
                                {
                                    list.Add(new PendingData
                                    {
                                        Rc = list3[0],
                                        Hs = list3[1],
                                        Win = list3[2],
                                        Plc = list3[3],
                                        Per = list3[4],
                                        Limit = list3[5].Trim(new char[]
                                        {
                                            '!'
                                        })
                                    });
                                }
                            }
                            result = list;
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
        private string GetRaceURL(string string_2, string string_3)
        {
            Uri uri = new Uri(string_2);
            string text = HttpUtility.ParseQueryString(uri.Query).Get("race_date");
            string text2 = HttpUtility.ParseQueryString(uri.Query).Get("race_type");
            return string.Format("{0}/{1}?race_date={2}&race_type={3}&rc=0&m=HK&c=2&lu=0", new object[]
            {
                Settings.GetSettings.url2,
                string_3,
                text,
                text2
            });
        }

        private RaceInfo GetRaceInfo(BetItem betItem_0)
        {

            RaceInfo result;
            try
            {
                if (this.m_RaceInfoList == null || this.m_RaceInfoList.Count == 0)
                {
                    result = null;
                }
                else
                {
                    RaceInfo raceInfo = this.m_RaceInfoList.Find((RaceInfo inf)=> { return inf.matchId == betItem_0.RaceType && inf.strTime == betItem_0.RaceDate; });
                    result = raceInfo;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result = null;
            }
            return result;
        }

        private List<RaceInfo> ParseRaceInfo()
        {
            List<RaceInfo> result3;
            try
            {
                Trace.WriteLine("Getting Race List Started");
                HtmlDocument htmlDocument = new HtmlDocument();
                HttpResponseMessage result = this.UserSession.SingHttpClient.GetAsync(string.Format("{0}/player.jsp", Settings.GetSettings.url2)).Result;
                result.EnsureSuccessStatusCode();
                string result2 = result.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(result2))
                {
                    this.m_onWriteLogEvent(string.Format("{0} -> can't get match list!", this.UserSession.UserName));
                    result3 = null;
                }
                else
                {
                    htmlDocument.LoadHtml(result2);
                    HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//div[@id='oldcarddata']/div[contains(@icardd, 'row')]");
                    if (htmlNodeCollection == null || htmlNodeCollection.Count < 1)
                    {
                        result3 = null;
                    }
                    else
                    {
                        List<RaceInfo> list = new List<RaceInfo>();
                        foreach (HtmlNode htmlNode in htmlNodeCollection)
                        {
                            HtmlNode htmlNode2 = htmlNode.SelectSingleNode(".//div[@class='summaryline']");
                            if (htmlNode2 == null)
                                continue;
                            
                            HtmlNode htmlNode3 = htmlNode2.SelectSingleNode(".//dd[@class='country_name']");
                            if (htmlNode3 == null)
                                continue;
                            
                            string strCountry = htmlNode3.InnerText.Trim();
                            if (string.IsNullOrEmpty(strCountry))
                                continue;
                            
                            HtmlNode htmlNode4 = htmlNode2.SelectSingleNode(".//dd[@class='rc_type']/span");
                            if (htmlNode4 == null)
                                continue;
                            
                            string aniType = htmlNode4.InnerText.Trim();
                            HtmlNode htmlNode5 = htmlNode2.SelectSingleNode(".//dd[@class='rc_time']");
                            if (htmlNode5 == null)
                                continue;
                            
                            string attributeValue = htmlNode5.GetAttributeValue("icardd", "");
                            if (string.IsNullOrEmpty(attributeValue))
                                continue;
                           
                            GroupCollection groups = Regex.Match(attributeValue, "cati(?<sTime>\\d+_\\d+_\\d+_\\d+)").Groups;
                            if (groups == null || groups["sTime"] == null)
                                continue;
                            
                            HtmlNodeCollection htmlNodeCollection2 = htmlNode.SelectNodes(".//div[@class='expendline']");
                            if (htmlNodeCollection2 == null || htmlNodeCollection2.Count < 1)
                                continue;
                            
                            foreach (HtmlNode htmlNode6 in htmlNodeCollection2)
                            {
                                HtmlNode htmlNode7 = htmlNode6.SelectSingleNode(".//dd[@class='location_name']");
                                if (htmlNode7 == null)
                                    continue;
                                
                                string strLocation = htmlNode7.InnerText.Trim();
                                if (string.IsNullOrEmpty(strLocation))
                                    continue;
                                
                                HtmlNodeCollection htmlNodeCollection3 = htmlNode6.SelectNodes(".//ul/li/a");
                                if (htmlNodeCollection3 == null || htmlNodeCollection3.Count < 1)
                                    continue;
                                
                                foreach (HtmlNode htmlNode8 in htmlNodeCollection3)
                                {
                                    string attributeValue2 = htmlNode8.GetAttributeValue("onclick", "");
                                    string text3 = htmlNode8.InnerText.Trim();
                                    if (string.IsNullOrEmpty(attributeValue2) || string.IsNullOrEmpty(text3))
                                        continue;
                                    
                                    RaceInfo raceInfo = new RaceInfo();
                                    raceInfo.country = strCountry;
                                    raceInfo.location = strLocation.Replace("&nbsp;", " ");
                                    raceInfo.aniType = aniType;
                                    raceInfo.raceType = text3;
                                    raceInfo.link = Settings.GetSettings.url2 + attributeValue2.Split(new char[]
                                    {
                                        ','
                                    })[1].Trim(new char[]
                                    {
                                        '\''
                                    });
                                    raceInfo.timeId = groups["sTime"].Value;
                                    string empty = string.Empty;
                                    string empty2 = string.Empty;
                                    this.GetRaceInfo(raceInfo.link, ref empty, ref empty2);
                                    raceInfo.matchId = empty;
                                    raceInfo.strTime = empty2;
                                    list.Add(raceInfo);
                                    
                                }
                            }
                        }
                        Trace.WriteLine(string.Format("Getting Race List Finished Items - {0}", list.Count));
                        result3 = list;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                result3 = null;
            }
            return result3;
        }

        private void GetRaceInfo(string strLink, ref string raceDate, ref string raceType)
        {
            try
            {
                Uri uri = new Uri(strLink);
                raceType = HttpUtility.ParseQueryString(uri.Query).Get("race_date");
                raceDate = HttpUtility.ParseQueryString(uri.Query).Get("race_type");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public void Abort()
        {
            foreach (Thread thr in m_UserList)
            {
                thr.Abort();
            }
        }
    }
}
