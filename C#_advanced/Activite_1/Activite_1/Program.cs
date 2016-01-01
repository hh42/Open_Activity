using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activite_1
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            bool replay = true;
            ConsoleKey key;

            // Boucle pour rejouer
            while (replay)
            {
                AfficheMenu();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                while (consoleKeyInfo.Key != ConsoleKey.D1 && consoleKeyInfo.Key != ConsoleKey.D2 && consoleKeyInfo.Key != ConsoleKey.NumPad1 && consoleKeyInfo.Key != ConsoleKey.NumPad2)
                {
                    AfficheMenu();
                    consoleKeyInfo = Console.ReadKey(true);
                }
                if (consoleKeyInfo.Key == ConsoleKey.D1 || consoleKeyInfo.Key == ConsoleKey.NumPad1)
                    Jeu1();
                else
                    Jeu2();
                Console.WriteLine("Voulez-vous rejouer ? (O/N)");
                ConsoleKeyInfo replayKeyInfo = Console.ReadKey(true);
                key = replayKeyInfo.Key;
                replay = key == ConsoleKey.O ? true : false;
            }
        }

        private static void AfficheMenu()
        {
            Console.Clear();
            Console.WriteLine("Veuillez choisir votre mode de jeu :");
            Console.WriteLine("\t1 : Contre les monstres");
            Console.WriteLine("\t2 : Contre le boss de fin");
        }
        private static void Jeu1()
        {
            Joueur joueur = new Joueur(150);
            int cptFacile = 0;
            int cptDifficile = 0;
            while (joueur.EstVivant)
            {
                MonstreFacile monstre = FabriqueDeMonstre();
                while (monstre.EstVivant && joueur.EstVivant)
                {
                    joueur.Attaque(monstre);
                    if (monstre.EstVivant)
                        monstre.Attaque(joueur);
                }

                if (joueur.EstVivant)
                {
                    if (monstre is MonstreDifficile)
                        cptDifficile++;
                    else
                        cptFacile++;
                }
                else
                {
                    Console.WriteLine("Snif, vous êtes mort...");
                    break;
                }
            }
            Console.WriteLine("Bravo !!! Vous avez tué {0} monstres faciles et {1} monstres difficiles. Vous avez {2} points.", cptFacile, cptDifficile, cptFacile + cptDifficile * 2);
        }

        private static MonstreFacile FabriqueDeMonstre()
        {
            if (random.Next(2) == 0)
                return new MonstreFacile();
            else
                return new MonstreDifficile();
        }


        private static void Jeu2()
        {
            Joueur joueur = new Joueur(150);
            BossDeFin boss = new BossDeFin(250);
            while (joueur.EstVivant && boss.EstVivant)
            {
                joueur.Attaque(boss);
                if (boss.EstVivant)
                    boss.Attaque(joueur);
            }
            if (joueur.EstVivant)
                Console.WriteLine("Bravo, vous avez sauvé la princesse (ou le prince !)");
            else
                Console.WriteLine("Game over...");
        }
    }

    public abstract class Personnage
    {
        public abstract bool EstVivant { get; }
        public abstract void Attaque(Personnage ennemie);
        public abstract void SubitDegats(int degats);
        public int LanceLeDe()
        {
            return De.LanceLeDe();
        }
    }

    public class PersonnageAPointsDeVie : Personnage
    {
        public int PtsDeVies { get; protected set; }

        public override void Attaque(Personnage ennemie)
        {
            int nbPoints = LanceLeDe(26);
            if (this.LanceLeDe() >= ennemie.LanceLeDe())
            {
                ennemie.SubitDegats(nbPoints);
            }
        }

        public override void SubitDegats(int degat)
        {
            PtsDeVies -= degat;
        }

        public override bool EstVivant
        {
            get { return PtsDeVies > 0; }
            // pourquoi set ?
            //protected set { }
        }

        public int LanceLeDe(int de)
        {
            return De.LanceLeDe(de);
        }
    }

    public static class De
    {
        private static Random random = new Random();

        public static int LanceLeDe()
        {
            return random.Next(1, 7);
        }

        public static int LanceLeDe(int valeur)
        {
            return random.Next(1, valeur);
        }
    }

    public class MonstreFacile : Personnage
    {
        private const int atk = 10;
        private bool estVivant = true;

        public override void Attaque(Personnage joueur)
        {
            int lanceMonstre = LanceLeDe();
            int lanceJoueur = joueur.LanceLeDe();
            if (lanceMonstre > lanceJoueur)
            {
                joueur.SubitDegats(atk);
            }
        }

        public override bool EstVivant
        {
            get { return estVivant; }
        }

        public override void SubitDegats(int degats)
        {
            estVivant = false;
        }
    }

    public class MonstreDifficile : MonstreFacile
    {
        private const int degatsSort = 5;

        public override void Attaque(Personnage joueur)
        {
            base.Attaque(joueur);
            joueur.SubitDegats(SortMagique());
        }

        private int SortMagique()
        {
            int valeur = De.LanceLeDe();
            if (valeur == 6)
                return 0;
            return degatsSort * valeur;
        }
    }

    public class Joueur : PersonnageAPointsDeVie
    {

        public Joueur(int points)
        {
            PtsDeVies = points;
        }

        public void Attaque(MonstreFacile monstre)
        {
            int lanceJoueur = LanceLeDe();
            int lanceMonstre = monstre.LanceLeDe();
            if (lanceJoueur >= lanceMonstre)
                //l'héritage nous force a donner une valeur au degats subit
                monstre.SubitDegats(0);
        }

        public override void SubitDegats(int degats)
        {
            if (!BouclierFonctionne())
                base.SubitDegats(degats);
        }

        private bool BouclierFonctionne()
        {
            return De.LanceLeDe() <= 2;
        }
    }

    public class BossDeFin : PersonnageAPointsDeVie
    {
        public BossDeFin(int points)
        {
            PtsDeVies = points;
        }
    }
}
