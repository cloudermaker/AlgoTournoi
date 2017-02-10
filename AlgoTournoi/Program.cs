using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoTournoi
{
    class Program
    {
        static void Main(string[] args)
        {
            //while (true)
            {
                Tournoi t = new Tournoi();
                int nb_joueurs = 10;
                int nb_tours = 100;

                t.InitTournoi(nb_joueurs);

                t.LaunchTournoi(nb_tours);

                Console.In.ReadLine();
            }
        }
    }
}
