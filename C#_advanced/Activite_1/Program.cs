using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPC_Activite_1
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
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
            Joueur nicolas = new Joueur(150);
            int cptFacile = 0;
            int cptDifficile = 0;
            while (nicolas.EstVivant)
            {
                MonstreFacile monstre = FabriqueDeMonstre();
                while (monstre.EstVivant && nicolas.EstVivant)
                {
                    nicolas.Attaque(monstre);
                    if (monstre.EstVivant)
                        monstre.Attaque(nicolas);
                }

                if (nicolas.EstVivant)
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
            Joueur nicolas = new Joueur(150);
            BossDeFin boss = new BossDeFin(250);
            while (nicolas.EstVivant && boss.EstVivant)
            {
                nicolas.Attaque(boss);
                if (boss.EstVivant)
                    boss.Attaque(nicolas);
            }
            if (nicolas.EstVivant)
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

    public class PersonnageAPointDeVie : Personnage
    {
        private int _PtsDeVies { get; set; }

        public PersonnageAPointDeVie(int points)
        {
            _PtsDeVies = points;
        }

        public override void Attaque(Personnage ennemie)
        {
            int lancePersonnage = LanceLeDe();
            int lanceEnnemie = ennemie.LanceLeDe();
            if (lancePersonnage >= lanceEnnemie)
                ennemie.SubitDegats(this.LanceLeDe(25));
        }

        public override bool EstVivant
        {
            get { return _PtsDeVies > 0; }
        }

        public int LanceLeDe(int valeur)
        {
            return De.LanceLeDe(valeur);
        }

        public override void SubitDegats(int degats)
        {
            _PtsDeVies -= degats;
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
        private const int _atk = 10;
        private bool _estVivant = true;

        public override void Attaque(Personnage joueur)
        {
            int lanceMonstre = LanceLeDe();
            int lanceJoueur = joueur.LanceLeDe();
            if (lanceMonstre > lanceJoueur)
            {
                joueur.SubitDegats(_atk);
            }
        }

        public override bool EstVivant
        {
            get { return _estVivant; }
        }

        public override void SubitDegats(int degats)
        {
            _estVivant = false;
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


    /*
    ** 
    */


    public class Joueur : PersonnageAPointDeVie
    {
        private int _PtsDeVies { get; set; }

        public Joueur(int points)
        {
            _PtsDeVies = points;
        }

        public void Attaque(MonstreFacile monstre)
        {
            int lanceJoueur = LanceLeDe();
            int lanceMonstre = monstre.LanceLeDe();
            if (lanceJoueur >= lanceMonstre)
                monstre.SubitDegats();
        }

        public int LanceLeDe(int valeur)
        {
            return De.LanceLeDe(valeur);
        }

        public void SubitDegats(int degats)
        {
            if (!BouclierFonctionne())
                PtsDeVies -= degats;
        }

        private bool BouclierFonctionne()
        {
            return De.LanceLeDe() <= 2;
        }
    }

    public class BossDeFin : PersonnageAPointDeVie
    {
        public int PtsDeVies { get; private set; }
        /*public bool EstVivant
        {
            get { return PtsDeVies > 0; }
        }*/

        public BossDeFin(int points)
        {
            PtsDeVies = points;
        }

        public void Attaque(Joueur personnage)
        {
            int nbPoints = base.LanceLeDe(26);
            personnage.SubitDegats(nbPoints);
        }

        public void SubitDegats(int valeur)
        {
            PtsDeVies -= valeur;
        }

        /*
        private int LanceLeDe(int valeur)
        {
            return De.LanceLeDe(valeur);
        }*/
    }
}
