using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class PendingData
    {
        public PendingData()
        {

        }
        private string m_Rc;
        public string Rc
        {
            get{ return m_Rc; }
            set { m_Rc = value; }
        }

        private string m_Hs;
        public string Hs
        {
            get { return m_Hs; }
            set { m_Hs = value; }
        }
        private string m_Win;
        public string Win
        {
            get { return m_Win; }
            set { m_Win = value; }
        }
        private string m_Plc;
        public string Plc
        {
            get { return m_Plc; }
            set { m_Plc = value; }
        }
        private string m_Per;
        public string Per
        {
            get { return m_Per; }
            set { m_Per = value; }
        }

        private string m_Limit;
        public string Limit
        {
            get { return m_Limit; }
            set { m_Limit = value; }
        }
    }
}
