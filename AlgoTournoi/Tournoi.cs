using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTournoi
{
    public class Tournoi
    {
        private List<Team> AllTeam = new List<Team>();
        private List<Match> AllMatch = new List<Match>();
        private int nbTryMax;

        public Tournoi()
        {
            this.nbTryMax = 100000;
        }

        public Tournoi(int nbTour)
        {
            this.nbTryMax = nbTour;
        }

        public void InitTournoi(int nbTeam)
        {
            for (int i = 1; i <= nbTeam; i++)
            {
                Team t = new Team(i);

                this.AllTeam.Add(t);
            }

            if (nbTeam % 2 == 1)
                this.AllTeam.Add(new Team(0));
        }

        private Team FindAdvers(Team t1)
        {
            // On cherche les teams restantes
            List<Team> freeTeam = new List<Team>();
            foreach (Team t in this.AllTeam)
                if (!t.IsPlaying)
                    freeTeam.Add(t);

            // On random sur les restants
            Team t2 = null;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int id = r.Next(freeTeam.Count());
            t2 = freeTeam[id];

            if (t1.ID != t2.ID && !t1.HasPlay(t2) && !t2.IsPlaying)
            {
                t1.IsPlaying = true;
                t2.IsPlaying = true;
                return t2;
            }

            return null;
        }

        private bool FindAllMatch()
        {            
            int count = 0;
            bool matchFound = false;

            while (count < nbTryMax && !matchFound)
            {
                // Init la recherche de match
                matchFound = true;
                this.AllMatch = new List<Match>();
                foreach (Team t in this.AllTeam)
                    t.IsPlaying = false;

                foreach (Team t in this.AllTeam)
                {
                    if (t.IsPlaying)
                        continue;

                    Match m = new Match();
                    m.t1 = t;
                    m.t2 = this.FindAdvers(t);

                    if (m.t2 == null)
                    {
                        matchFound = false;
                        break;
                    }

                    this.AllMatch.Add(m);
                }

                count++;
            }

            if (matchFound)
                return true;
            else
                return false;
        }

        private bool AllTeamHasBye()
        {
            bool res = true;

            foreach (Team t in this.AllTeam)
                res &= t.HasBye;

            return res;
        }

        private Team PrintScore()
        {
            double scoreMax = 0;
            Team winner = null;

            this.AllTeam.Sort(delegate(Team t1, Team t2)
            { return t1.Point.CompareTo(t2.Point); });

            foreach (Team t in this.AllTeam)
            {
                if (scoreMax < t.Point)
                {
                    scoreMax = t.Point;
                    winner = t;
                }

                Console.Out.Write(string.Format("Team {0}: {1} => ", t.Name, t.Point));
                foreach (KeyValuePair<int, char> pair in t.Histo)
                    Console.Out.Write(string.Concat(pair.Value, ' '));
                Console.Out.WriteLine();
            }

            return winner;
        }

        private void EndTour()
        {
            foreach (Team t in this.AllTeam)
                t.IsPlaying = false;
        }

        public void LaunchTournoi(int nbTourMax)
        {
            bool existOtherMatch = true;
            int nbTour = 1;

            Console.Out.WriteLine("--- Starting Tournoi ---");

            while (existOtherMatch && nbTour <= nbTourMax)
            {
                Console.Out.WriteLine(string.Format("# Tour: {0}.", nbTour));
                this.AllMatch = new List<Match>();

                if (!this.FindAllMatch())
                    break;
                foreach (Match m in this.AllMatch)
                {
                    Console.Out.Write(string.Format("  Match: {0} VS {1}... ", m.t1.Name, m.t2.Name));
                    Team t = m.Play();

                    if (m.t1.ID == 0 || m.t2.ID == 0)
                        Console.Out.WriteLine(string.Format("{0} BYE.", t.Name));
                    else
                        Console.Out.WriteLine(string.Format("{0} Win! [{1}-{2}]", t.Name, m.nbKillT1, m.nbKillT2));
                }
                this.EndTour();

                nbTour++;
                existOtherMatch = !this.AllTeamHasBye();
            }
            Console.Out.WriteLine();

            Team winner = this.PrintScore();
            if (winner == null)
                winner = new Team(0);
            Console.Out.WriteLine(string.Format("--- Tournoi is Over. Winner is: {0} ({1} points) ---", winner.Name, winner.Point));
        }
    }
}
