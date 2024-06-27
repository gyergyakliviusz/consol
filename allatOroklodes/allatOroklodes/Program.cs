using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace allatOroklodes
{
    class Allat
    {
        public string Nev { get; private set; }
        public int SzuletesiEv { get; private set; }
        public int RajtSzam { get; private set; }
        public int SzepsegPont { get; private set; }
        public int ViselkedesPont { get; private set; }
        public static int AktualisEv { get; set; }
        public static int KorHatar { get; set; }

        public Allat(int rajtSzam, string nev, int szuletesiEv)
        {
            this.RajtSzam = rajtSzam;
            this.Nev = nev;
            this.SzuletesiEv = szuletesiEv;
        }

        public int Kor()
        {
            return AktualisEv - SzuletesiEv;
        }

        public virtual int PontSzam()
        {
            if (Kor() < KorHatar)
            {
                return ViselkedesPont * Kor() + SzepsegPont * (KorHatar - Kor());
            }
            return 0;
        }

        public void Pontozzak(int szepsegPont, int viselkedesPont)
        {
            this.SzepsegPont = szepsegPont;
            this.ViselkedesPont = viselkedesPont;
        }

        public override string ToString()
        {
            return RajtSzam + ". " + Nev + " nevű " + this.GetType().Name.ToLower() + " pontszáma: " + PontSzam() + " pont";
        }
    }

    class Kutya : Allat
    {
        public int GazdaViszonyPont { get; private set; }
        public bool KapottViszonyPontot { get; private set; }

        public Kutya(int rajtSzam, string nev, int szulEv) : base(rajtSzam, nev, szulEv)
        {

        }

        public void ViszonyPontozas(int gazdaViszonyPont)
        {
            this.GazdaViszonyPont = gazdaViszonyPont;
            KapottViszonyPontot = true;
        }

        public override int PontSzam()
        {
            int pont = 0;
            if (KapottViszonyPontot)
            {
                pont = base.PontSzam() + GazdaViszonyPont;
            }
            return pont;
        }
    }

    class Macska : Allat
    {
        public bool VanMacskaSzallitoDoboz { get; set; }

        public Macska(int rajtSzam, string nev, int szulEv, bool vanMacskaSzallitoDoboz) : base(rajtSzam, nev, szulEv)
        {
            this.VanMacskaSzallitoDoboz = vanMacskaSzallitoDoboz;
        }

        public override int PontSzam()
        {
            if (VanMacskaSzallitoDoboz)
            {
                return base.PontSzam();
            }
            return 0;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new Vezerles().Start();

            Console.ReadKey();
        }

        class Vezerles
        {
            private List<Allat> allatok = new List<Allat>();

            public void Start()
            {
                Allat.AktualisEv = 2015;
                Allat.KorHatar = 10;

                Regisztracio();
                Kiiratas("A regisztrált versenyzők");
                Verseny();
                Kiiratas("A verseny eredménye");
            }

            private void Regisztracio()
            {
                StreamReader olvasoCsatorna = new StreamReader("allatok.txt");

                string fajta, nev;
                int rajtSzam = 1, szulEv;
                bool vanDoboz;

                while (!olvasoCsatorna.EndOfStream)
                {
                    fajta = olvasoCsatorna.ReadLine();
                    nev = olvasoCsatorna.ReadLine();
                    szulEv = int.Parse(olvasoCsatorna.ReadLine());

                    if (fajta == "kutya")
                    {
                        allatok.Add(new Kutya(rajtSzam, nev, szulEv));
                    }
                    else
                    {
                        vanDoboz = bool.Parse(olvasoCsatorna.ReadLine());
                        allatok.Add(new Macska(rajtSzam, nev, szulEv, vanDoboz));
                    }
                    rajtSzam++;
                }
                olvasoCsatorna.Close();
            }

            private void Verseny()
            {
                Random rand = new Random();
                int hatar = 11;
                foreach (Allat item in allatok)
                {
                    if (item is Kutya)
                    {
                        (item as Kutya).ViszonyPontozas(rand.Next(hatar));
                    }
                    item.Pontozzak(rand.Next(hatar), rand.Next(hatar));
                }
            }

            private void Kiiratas(string cim)
            {
                Console.WriteLine(cim);
                foreach (Allat item in allatok)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
