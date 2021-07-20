using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class AccountItem
    {
        public AccountItem()
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

        private string m_strUserCode;
        public string UserCode
        {
            get{ return m_strUserCode; }
            set { m_strUserCode = value; }
        }
    }

    
}
