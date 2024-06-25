using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace allatFinal
{
    class Allat
    {
        // mezők
        private string nev;
        private int szuletesiEv;
        private int szepsegPont, viselkedesPont;
        private int pontSzam;
        private string oltasiIgazolas;
        private int rajtszam;

        private static int aktualisEv;
        private static int korHatar;
        private static int kovetkezoRajtszam = 1;

        // konstruktor
        public Allat(string nev, int szuletesiEv, string oltasiIgazolas)
        {
            this.nev = nev;
            this.szuletesiEv = szuletesiEv;
            this.oltasiIgazolas = oltasiIgazolas;
            this.rajtszam = kovetkezoRajtszam++;
        }

        // metódusok
        public int Kor()
        {
            return aktualisEv - szuletesiEv;
        }

        public void Pontozzak(int szepsegPont, int viselkedesPont)
        {
            this.szepsegPont = szepsegPont;
            this.viselkedesPont = viselkedesPont;
            if (Kor() <= korHatar)
            {
                pontSzam = viselkedesPont * Kor() + szepsegPont * (korHatar - Kor());
            }
            else
            {
                pontSzam = 0;
            }
        }

        public override string ToString()
        {
            return $"{rajtszam}. {nev} pontszáma: {pontSzam} pont (oltási igazolás: {oltasiIgazolas})";
        }

        // tulajdonságok
        // kívülről nem változtatható értékek
        public string Nev
        {
            get { return nev; }
        }

        public int SzuletesiEv
        {
            get { return szuletesiEv; }
        }

        public int SzepsegPont
        {
            get { return szuletesiEv; }
        }

        public int ViselkedesPont
        {
            get { return viselkedesPont; }
        }

        public int PontSzam
        {
            get { return pontSzam; }
        }

        public string OltasiIgazolas
        {
            get { return oltasiIgazolas; }
        }

        public int Rajtszam
        {
            get { return rajtszam; }
        }

        // kívülről változtatható értékek
        public static int AktualisEv
        {
            get { return aktualisEv; }
            set { aktualisEv = value; }
        }

        public static int KorHatar
        {
            get { return korHatar; }
            set { korHatar = value; }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int aktEv = 2015, korhatar = 10;

            Allat.AktualisEv = aktEv;
            Allat.KorHatar = korhatar;

            AllatVerseny();

            Console.ReadKey();
        }

        private static void AllatVerseny()
        {
            // az allat nevű változó deklarálása
            List<Allat> allatok = new List<Allat>();

            string nev;
            int szulEv;
            int szepseg, viselkedes;
            int veletlenPontHatar = 10;
            string oltasiIgazolas;

            // egy Random példány létrehozása
            Random rand = new Random();

            // számoláshoz szükséges kezdőértékek beállítása
            int osszesVersenyzo = 0;
            int osszesPont = 0;
            int legtobbPont = 0;

            Console.WriteLine("Kezdődik a verseny");

            char tovabb = 'i';
            while (tovabb == 'i')
            {
                Console.Write("Az állat neve: ");
                nev = Console.ReadLine();

                Console.Write("Születési éve: ");
                while (!int.TryParse(Console.ReadLine(), out szulEv)
                    || szulEv <= 0
                    || szulEv > Allat.AktualisEv)
                {
                    Console.Write("Hibás adat, kérem újra.");
                }

                Console.Write("Oltási igazolás száma: ");
                oltasiIgazolas = Console.ReadLine();

                // véletlen pontértékek
                szepseg = rand.Next(veletlenPontHatar + 1);
                viselkedes = rand.Next(veletlenPontHatar + 1);

                // az allat példány létrehozása
                Allat allat = new Allat(nev, szulEv, oltasiIgazolas);

                // a pontozási metódus meghívása
                allat.Pontozzak(szepseg, viselkedes);

                allatok.Add(allat);

                Console.WriteLine(allat);

                // számítások
                osszesVersenyzo++;
                osszesPont += allat.PontSzam;
                if (legtobbPont < allat.PontSzam)
                {
                    legtobbPont = allat.PontSzam;
                }

                Console.Write("Van még állat? (i/n) ");

                // ellenőrzött beolvasásra átalakítás
                tovabb = char.Parse(Console.ReadLine());
            }

            // eredmény kiíratása
            Console.WriteLine("\nÖsszesen " + osszesVersenyzo + " versenyző volt," + "\nösszpontszámuk:" + osszesPont + " pont," + "\nlegnagyobb pontszám: " + legtobbPont);
        }
    }
}
