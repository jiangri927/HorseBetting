using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class HorseItem
    {
        public HorseItem()
        {

        }
        private string m_strHorseNumber;
        public string HorseNumber
        {
            get { return m_strHorseNumber; }
            set { m_strHorseNumber = value; }
        }

        private string m_strWin;
        public string Win
        {
            get { return m_strWin; }
            set { m_strWin = value; }
        }

        private string m_strPlace;
        public string Place
        {
            get { return m_strPlace; }
            set { m_strPlace = value; }
        }

        private string m_strTickets;
        public string Tickets
        {
            get { return m_strTickets; }
            set { m_strTickets = value; }
        }

        private string m_strPercent;
        public string Percent
        {
            get { return m_strPercent; }
            set { m_strPercent = value; }
        }

        private string m_strLimit;
        public string Limit
        {
            get { return m_strLimit; }
            set { m_strLimit = value; }
        }

        private string m_strTax;
        public string Tax
        {
            get { return m_strTax; }
            set { m_strTax = value; }
        }

        private string m_strTotal;
        public string Total
        {
            get { return m_strTotal; }
            set { m_strTotal = value; }
        }

        private string m_strType;
        public string Type
        {
            get { return m_strType; }
            set { m_strType = value; }
        }

        private string m_strFType;
        public string FType
        {
            get { return m_strType; }
            set { m_strType = value; }
        }

        private string m_strUrl;
        public string Url
        {
            get { return m_strUrl; }
            set { m_strUrl = value; }
        }
    }
}
