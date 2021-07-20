using HorseBetting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HorseBetting.SingleBet
{
    public class BetData
    {
        private static BetData m_Instance;
        public object locker = new object();
        
        public List<BetItem> m_OrderList = new List<BetItem>();
        public List<BetItem> m_ToDoBetList = new List<BetItem>();
        public List<BetItem> m_SettledUrlList = new List<BetItem>();
        public List<BetItem> m_SettledBetList = new List<BetItem>();
        public List<BetItem> m_SettledSumList = new List<BetItem>();
        public List<SettledMatchItem> m_SettledMatchList = new List<SettledMatchItem>();
        public List<BetResult> m_SettledList = new List<BetResult>();

        public int m_settledmatchIndex = 0;
        public int ThreadCount = 0;

        public int m_settledbetIndex = 0;
        public int m_unsettledbetIndex = 0;
        public int m_settledsumIndex = 0;
        public int m_settledurlIndex = 0;
        public int m_unsettledurlIndex = 0;

        public BetData()
        {

        }

        public static BetData Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new BetData();
                }
                return m_Instance;
            }
        }

        public SettledMatchItem GetSettledMatchOne()
        {
            SettledMatchItem result;
            try
            {
                Monitor.Enter(this.m_SettledMatchList);
                for (int i = this.m_settledmatchIndex; i < this.m_SettledMatchList.Count; i++)
                {
                    if ((this.m_SettledMatchList[i].RacingDate == global::System.DateTime.Today || !this.m_SettledMatchList[i].IsScrapped) && this.m_SettledMatchList[i].ScrapStatus == 0)
                    {
                        this.m_settledmatchIndex = (i + 1) % this.m_SettledMatchList.Count;
                        Monitor.Exit(this.m_SettledMatchList);
                        return m_SettledMatchList[i];
                    }
                }
                Monitor.Exit(this.m_SettledMatchList);
                result = null;
            }
            catch (global::System.Exception ex)
            {
                string message = ex.Message;
                result = null;
            }
            return result;
        }

    }
}
