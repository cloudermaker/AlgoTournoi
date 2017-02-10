using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTournoi
{
    public class Team
    {
        public Team(int ID)
        {
            this.Name = string.Concat("Team_", ID);
            this.ID = ID;
            this.Point = 0;
            this.Histo = new List<KeyValuePair<int,char>>();
            this.HasBye = false;
            this.IsPlaying = false;
        }

        public bool HasPlay(Team t)
        {
            foreach (KeyValuePair<int, char> pair in this.Histo)
            {
                if (pair.Key == t.ID)
                    return true;
            }

            return false;
        }

        public void Win(Team advers)
        {
            this.Histo.Add(new KeyValuePair<int,char>(advers.ID, 'V'));
            advers.Histo.Add(new KeyValuePair<int,char>(this.ID, 'L'));

            if (advers.ID == 0)
                this.HasBye = true;
        }

        public bool HasBye { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public double Point { get; set; }
        public List<KeyValuePair<int, char>> Histo { get; set; }
        public bool IsPlaying { get; set; }
    }
}
