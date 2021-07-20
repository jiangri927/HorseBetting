using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class SubAccItem
    {
        public SubAccItem()
        {

        }
        private string m_SubAccName;
        public string SubAccName
        {
            get { return m_SubAccName; }
            set { m_SubAccName = value; }
        }

        private string m_strSubAccHistoryUrl;
        public string SubAccHistoryUrl
        {
            get { return m_strSubAccHistoryUrl; }
            set { m_strSubAccHistoryUrl = value; }
        }

        private string m_strPlayerID;
        public string PlayerID
        {
            get { return m_strPlayerID; }
            set { m_strPlayerID = value; }
        }

        private string m_strAgentName;
        public string AgentName
        {
            get { return m_strAgentName; }
            set { m_strAgentName = value; }
        }


    }
}
