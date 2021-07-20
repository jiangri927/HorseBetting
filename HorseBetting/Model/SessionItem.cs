using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class SessionItem
    {
        public SessionItem()
        {

        }
        private string m_strUserName;
        public string UserName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }

        private string m_strUserPass;
        public string UserPass
        {
            get { return m_strUserPass; }
            set { m_strUserPass = value; }
        }

        private bool m_IsLogin;
        public bool IsLogin
        {
            get { return m_IsLogin; }
            set { m_IsLogin = value; }
        }

        private string m_strUserID;
        public string UserID
        {
            get { return m_strUserID; }
            set { m_strUserID = value; }
        }

        private HttpClient m_SingHttpClient;
        public HttpClient SingHttpClient
        {
            get { return m_SingHttpClient; }
            set { m_SingHttpClient = value; }
        }

        private CookieContainer m_CookieContainer;
        public CookieContainer CookieContainer
        {
            get { return m_CookieContainer; }
            set { m_CookieContainer = value; }
        }

        private string m_strUserCode;
        public string UserCode
        {
            get { return m_strUserCode; }
            set { m_strUserCode = value; }
        }

    }
}
