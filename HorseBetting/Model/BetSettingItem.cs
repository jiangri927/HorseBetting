using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class BetSettingItem
    {
        public BetSettingItem()
        {

        }

        private string m_strUserName;
        public string AccountName
        {
            get { return m_strUserName; }
            set { m_strUserName = value; }
        }

        private bool m_bType;
        public bool Type
        {
            get { return m_bType; }
            set { m_bType = value; }
        }

        private string m_strType;
        public string StrType
        {
            get { return m_strType; }
            set { m_strType = value; }
        }

        public double m_times;
        public double Times
        {
            get { return m_times; }
            set { m_times = value; }
        }
    }
}
