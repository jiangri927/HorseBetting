using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class RaceInfo
    {
        public RaceInfo()
        {
            this.country = string.Empty;
            this.location = string.Empty;
            this.aniType = string.Empty;
            this.raceType = string.Empty;
            this.timeId = string.Empty;
            this.link = string.Empty;
            this.bdata = string.Empty;
            this.edata = string.Empty;
            this.strTime = string.Empty;
            this.minutes = 0;
        }
        public RaceInfo(RaceInfo _raceInfo)
        {
            this.country = _raceInfo.country;
            this.location = _raceInfo.location;
            this.aniType = _raceInfo.aniType;
            this.raceType = _raceInfo.raceType;
            this.timeId = _raceInfo.timeId;
            this.link = _raceInfo.link;
            this.bdata = _raceInfo.bdata;
            this.edata = _raceInfo.edata;
            this.strTime = _raceInfo.strTime;
            this.minutes = _raceInfo.minutes;
        }

        private string m_country;
        public string country
        {
            get { return m_country; }
            set { m_country = value; }
        }
        private string m_location;
        public string location
        {
            get { return m_location; }
            set { m_location = value; }
        }
        private string m_aniType;
        public string aniType
        {
            get { return m_aniType; }
            set { m_aniType = value; }
        }
        private string m_raceType;
        public string raceType
        {
            get { return m_raceType; }
            set { m_raceType = value; }
        }
        private string m_timeId;
        public string timeId
        {
            get { return m_timeId; }
            set { m_timeId = value; }
        }
        private string m_link;
        public string link
        {
            get { return m_link; }
            set { m_link = value; }
        }
        private string m_bdata;
        public string bdata
        {
            get { return m_bdata; }
            set { m_bdata = value; }
        }
        private string m_edata;
        public string edata
        {
            get { return m_edata; }
            set { m_edata = value; }
        }
        private string m_strTime;
        public string strTime
        {
            get { return m_strTime; }
            set { m_strTime = value; }
        }
        private int m_minutes;
        public int minutes
        {
            get { return m_minutes; }
            set { m_minutes = value; }
        }

        private string m_strmatchId;
        public string matchId
        {
            get { return m_strmatchId; }
            set { m_strmatchId = value; }
        }

    }
}
