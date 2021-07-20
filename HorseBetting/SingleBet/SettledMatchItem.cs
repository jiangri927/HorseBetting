using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.SingleBet
{
    public class SettledMatchItem
    {
        public SettledMatchItem()
        {

        }
        private string m_strPlayerID;
        public string PlayerID
        {
            get { return m_strPlayerID; }
            set { m_strPlayerID = value; }
        }

        private string m_strLocation;
        public string Location
        {
            get { return m_strLocation; }
            set { m_strLocation = value; }
        }

        private string m_strTote;
        public string Tote
        {
            get { return m_strTote; }
            set { m_strTote = value; }
        }

        private string m_strTotalTiockets;
        public string TotalTickets
        {
            get { return m_strTotalTiockets; }
            set { m_strTotalTiockets = value; }
        }

        private string m_strTotalTax;
        public string TotalTax
        {
            get { return m_strTotalTax; }
            set { m_strTotalTax = value; }
        }

        private string m_strProfit;
        public string Profit
        {
            get { return m_strProfit; }
            set { m_strProfit = value; }
        }

        private string m_strType;
        public string Type
        {
            get { return m_strType; }
            set { m_strType = value; }
        }
        private string m_String_0;
        public string URL
        {
            get { return m_String_0; }
            set { m_String_0 = value; }
        }

        private DateTime m_RacingDate;
        public DateTime RacingDate
        {
            get { return m_RacingDate; }
            set { m_RacingDate = value; }
        }

        private bool m_IsScrapped;
        public bool IsScrapped
        {
            get { return m_IsScrapped; }
            set { m_IsScrapped = value; }
        }

        private string m_AgentName;
        public string AgentName
        {
            get { return m_AgentName; }
            set { m_AgentName = value; }
        }

        private int m_ScrapStatus;
        public int ScrapStatus
        {
            get { return m_ScrapStatus; }
            set { m_ScrapStatus = value; }
        }


    }
}
