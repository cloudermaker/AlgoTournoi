using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTournoi
{
    class Match
    {
        public Team t1 { get; set; }
        public Team t2 { get; set; }
        public int nbKillT1 { get; set; }
        public int nbKillT2 { get; set; }
        public int winner;

        public Team Play()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            this.nbKillT1 = r.Next(50);
            this.nbKillT2 = r.Next(50);

            if (this.t2.ID == 0)
            {
                this.t1.Win(t2);
                this.winner = 1;

                t1.Point += this.CalcPoint();

                return this.t1;
            }
            else if (r.Next(3) + 1 == 1)
            {
                this.t1.Win(t2);
                this.winner = 1;
                t1.Point += this.CalcPoint();

                return this.t1;
            }
            else
            {
                this.t2.Win(t1);
                this.winner = 2;
                t2.Point += this.CalcPoint();

                return this.t2;
            }
        }

        public double CalcPoint()
        {
            double winPoint = 0;

            if (t2 == null)
            {
                return 3;
            }
            
            if (winner == 1)
            {
                winPoint = 3 + t2.Point * 0.05 + (this.nbKillT1 - this.nbKillT2) * 0.01;
            }
            else
            {
                winPoint = 3 + t1.Point * 0.05 + (this.nbKillT2 - this.nbKillT1) * 0.01;
            }            

            return winPoint;
        }
    }
}
