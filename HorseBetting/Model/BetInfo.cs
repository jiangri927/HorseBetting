using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting.Model
{
    public class BetInfo
    {
        public BetInfo(string _p, string _r, string _h, string _s, string _t)
        {
            this.player = _p;
            this.race = _r;
            this.horse = _h;
            this.stake = _s;
            this.type = _t;
        }
        private string m_player;
        public string player
        {
            get { return m_player; }
            set { m_player = value; }
        }
        private string m_race;
        public string race
        {
            get { return m_race; }
            set { m_race = value; }
        }

        private string m_horse;
        public string horse
        {
            get { return m_horse; }
            set { m_horse = value; }
        }

        private string m_stake;
        public string stake
        {
            get { return m_stake; }
            set { m_stake = value; }
        }

        private string m_type;
        public string type
        {
            get { return m_type; }
            set { m_type = value; }
        }
            
    }
}
