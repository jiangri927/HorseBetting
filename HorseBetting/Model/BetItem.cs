using HorseBetting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HorseBetting.Model
{
    public class BetItem
    {
        public BetItem()
        {

        }
        public BetItem(HorseItem horse, string player, string matchType, string matchID, string stadium, string country, string divid, string racingnum, string Agent, DateTime date)
        {
            PlayerID = player;
            MatchType = matchType;
            MatchID = matchID;
            StadiumName = stadium;
            string empty = string.Empty;
            string empty2 = string.Empty;
            Utils.getCountry(country, ref empty, ref empty2);
            this.Country = empty;
            this.Animal = empty2;
            this.Dividend = divid;
            this.RacingNumber = racingnum;
            this.HorseNumber = horse.HorseNumber;
            this.Win = Utils.ParseToDouble(horse.Win);
            this.Place = Utils.ParseToDouble(horse.Place);
            this.Tickets = Utils.ParseToDouble(horse.Tickets);
            this.Percent = Utils.ParseToDouble(horse.Percent);
            this.Limit = horse.Limit;
            this.Tax = Utils.ParseToDouble(horse.Tax);
            this.Total = horse.Total;
            this.Type = horse.Type;
            this.FType = horse.FType;
            this.Url = horse.Url;
            this.AgentName = Agent;
            this.LogTime = global::System.DateTime.Now;
            this.BetDate = date;
            string raceDate = "";
            string raceType = "";
            this.ParseURL(horse.Url, ref raceDate, ref raceType);
            this.RaceDate = raceDate;
            this.RaceType = raceType;
        }


        private string m_strPlayerID;
        public string PlayerID
        {
            get { return m_strPlayerID; }
            set { m_strPlayerID = value; }
        }

        private string m_strMatchID;
        public string MatchID
        {
            get { return m_strMatchID; }
            set { m_strMatchID = value; }
        }

        private string m_strMatchType;
        public string MatchType
        {
            get { return m_strMatchType; }
            set { m_strMatchType = value; }
        }

        private string m_strStadiumName;
        public string StadiumName
        {
            get { return m_strStadiumName; }
            set { m_strStadiumName = value; }
        }

        private string m_strCountry;
        public string Country
        {
            get { return m_strCountry; }
            set { m_strCountry = value; }
        }

        private string m_strAnimal;
        public string Animal
        {
            get { return m_strAnimal; }
            set { m_strAnimal = value; }
        }

        private string m_strDividend;
        public string Dividend
        {
            get { return m_strDividend; }
            set { m_strDividend = value; }
        }

        private string m_strRacingNumber;
        public string RacingNumber
        {
            get { return m_strRacingNumber; }
            set { m_strRacingNumber = value; }
        }
        private DateTime m_BetDate;
        public DateTime BetDate
        {
            get { return m_BetDate; }
            set { m_BetDate = value; }
        }

        private DateTime m_LogTime;
        public DateTime LogTime
        {
            get { return m_LogTime; }
            set { m_LogTime = value; }
        }

        private string m_strHorseNumber;
        public string HorseNumber
        {
            get { return m_strHorseNumber; }
            set { m_strHorseNumber = value; }
        }

        private double m_Win;
        public double Win
        {
            get { return m_Win; }
            set { m_Win = value; }
        }

        private double m_Win2;
        public double Win2
        {
            get { return m_Win2; }
            set { m_Win2 = value; }
        }

        private double m_Win3;
        public double Win3
        {
            get { return m_Win3; }
            set { m_Win3 = value; }
        }

        
        private double m_Place2;
        public double Place2
        {
            get { return m_Place2; }
            set { m_Place2 = value; }
        }

        private double m_Place;
        public double Place
        {
            get { return m_Place; }
            set { m_Place = value; }
        }

        private double m_Place3;
        public double Place3
        {
            get { return m_Place3; }
            set { m_Place3 = value; }
        }

        private double m_Tickets;
        public double Tickets
        {
            get { return m_Tickets; }
            set { m_Tickets = value; }
        }

        private double m_Percent;
        public double Percent
        {
            get { return m_Percent; }
            set { m_Percent = value; }
        }

        private string m_strLimit;
        public string Limit
        {
            get { return m_strLimit; }
            set { m_strLimit = value; }
        }

        private double m_Tax;
        public double Tax
        {
            get { return m_Tax; }
            set { m_Tax = value; }
        }

        private string m_strTotal;
        public string Total
        {
            get { return m_strTotal; }
            set { m_strTotal = value; }
        }

        private string m_strFType;
        public string FType
        {
            get { return m_strFType; }
            set { m_strFType = value; }
        }

        private string m_strType;
        public string Type
        {
            get { return m_strType; }
            set { m_strType = value; }
        }

        private string m_strTransactionID;
        public string TransactionID
        {
            get { return m_strTransactionID; }
            set { m_strTransactionID = value; }
        }

        private string m_strUrl;
        public string Url
        {
            get { return m_strUrl; }
            set { m_strUrl = value; }
        }

        private string m_strAgentName;
        public string AgentName
        {
            get { return m_strAgentName; }
            set { m_strAgentName = value; }
        }

        private bool m_isScrapped;
        public bool IsScrapped
        {
            get { return m_isScrapped; }
            set { m_isScrapped = value; }
        }

        private string m_strRaceDate;
        public string RaceDate
        {
            get { return m_strRaceDate; }
            set { m_strRaceDate = value; }
        }

        private string m_strRaceType;
        public string RaceType
        {
            get { return m_strRaceType; }
            set { m_strRaceType = value; }
        }

        private bool m_isPlaced;
        public bool IsPlaced
        {
            get { return m_isPlaced; }
            set { m_isPlaced = value; }
        }

        private int m_FailedCount;
        public int FailedCount
        {
            get { return m_FailedCount; }
            set { m_FailedCount = value; }
        }

        private void ParseURL(string strURL, ref string strDate, ref string strType)
        {
            try
            {
                global::System.Uri uri = new global::System.Uri("http://www.kle009.com/" + strURL);
                strDate = HttpUtility.ParseQueryString(uri.Query).Get("d");
                strType = HttpUtility.ParseQueryString(uri.Query).Get("type");
            }
            catch (global::System.Exception ex)
            {
                string message = ex.Message;
            }
        }
    }
}
