using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class RaceData
    {
        public RaceData()
        {
            this.bData = new List<PendingData>();
            this.eData = new List<PendingData>();
        }
        private List<PendingData> m_bData;
        public List<PendingData> bData
        {
            get { return m_bData; }
            set { m_bData = value; }
        }

        private List<PendingData> m_eData;
        public List<PendingData> eData
        {
            get { return m_eData; }
            set { m_eData = value; }
        }
    }
    
}
