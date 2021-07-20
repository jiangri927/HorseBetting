using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting
{
    public class BetResult
    {
        private string strPlayer;
        public string player{
            get { return strPlayer; }
            set { strPlayer = value; }
        }

        private string m_strstrDate;
        public string strDate
        {
            get { return m_strstrDate; }
            set { m_strstrDate = value; }
        }

        private string strCountry;
        public string country
        {
            get { return strCountry; }
            set { strCountry = value; }
        }

        private string strLocation;
        public string location
        {
            get { return strLocation; }
            set { strLocation = value; }
        }
        private string stranimal;
        public string animal
        {
            get { return stranimal; }
            set { stranimal = value; }
        }
        private string strHorse;
        public string horse
        {
            get { return strHorse; }
            set { strHorse = value; }
        }

        private string strRace;
        public string race
        {
            get { return strRace; }
            set { strRace = value; }
        }

        private string strStake;
        public string stake
        {
            get { return strStake; }
            set { strStake = value; }
        }

        private string strWin;
        public string win
        {
            get { return strWin; }
            set { strWin = value; }
        }

        private string strPlace;
        public string place
        {
            get { return strPlace; }
            set { strPlace = value; }
        }

        private string strLimit;
        public string limit
        {
            get { return strLimit; }
            set { strLimit = value; }
        }

        private string strType;
        public string type
        {
            get { return strType; }
            set { strType = value; }
        }

        private string m_strTime;
        public string strTime
        {
            get { return m_strTime; }
            set { m_strTime = value; }
        }

        private DateTime m_betTime;
        public DateTime betTime
        {
            get { return m_betTime; }
            set { m_betTime = value; }
        }

    }
}
