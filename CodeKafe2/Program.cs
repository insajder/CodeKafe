using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace CodeKafe2
{
    class Program
    {
        /*  • Napraviti C# program sa implementacijom loggera po želji EventViewer,
            log4net, FileLogger itd.
            • Program je za kasu u restoranu CodeKafe
            • Restoran na dnevnom nivou u roku od 24h opslužuje 1200 gostiju.
            • Nabavna cena obroka (glavno jelo, salata, kolač) za restoran je 350
            dinara bez PDV-a. Stopa malo prodajnog poreza je 0.0825
            • Definisati cenu za 5 jela, i 5 pića na meniju i prikazati meni sa PDV-om i
            bez
            • Upisati sve vrste grešaka info, info – information, warn – warning, fail –
            failure, success, error - Fatal, Debug itd.
            • program obrađuje n gostiju
            • vraća kusur
            • izračunati kusur na osnovu unosa
            • informaciju o trenutnom broju usluženih gostiju
            • ukupnom prometu za 24h
            • prikazati račun i poruku „dođite nam opet“
            • posle čega treba prikazati poruku „Da li ima još neko da plati“ Ako ima
            unesi 1, ako nema 0
            • Koristiti if, switch, do while, try, catch, finally block, FormatException 
            • Proširiti C# program za CodeKafe koji koristi Enumerations,
            Negeneričke kolekcije, Generičke kolekcije, Dictionary definisati
            potrebne klase, atribute i metode.
            • Program treba da sadrži sledeći mogućnosti
            – Unos podataka u obe vrste kolekcija
            – Pretragu podataka u obe vrste kolekcija
            – Podatke zapisati u .txt fajl
            – Koristiti logger biblioteku po želji
            – Štampa svih podatke iz obe vrste kolekcija */

        static void Main(string[] args)
        {
            FileLogger.Info("Program je poceo izvrsavanje.");

            Console.WriteLine("CodeKafe");

            double pdv = 0.0825;
            int opcija = 0;
            string putanja = @"C:\Users\Public\Narudzbine.txt";

            //Dictionary
            IDictionary<string, int> obrok = new Dictionary<string, int>();
            //Genericka kolekcija
            List<Narudzbina> trenutnaNarudzbina = new List<Narudzbina>();
            //Negenericka kolekcija
            ArrayList sveNarudzbine = new ArrayList();

            obrok.Add("1.Corba", 200);
            obrok.Add("2.Pasulj", 300);
            obrok.Add("3.Musaka", 350);
            obrok.Add("4.Vesalica", 400);
            obrok.Add("5.Snicla", 400);
            obrok.Add("6.Coca cola", 200);
            obrok.Add("7.Fanta", 200);
            obrok.Add("8.Pivo", 300);
            obrok.Add("9.Rakija", 250);
            obrok.Add("10.Voda", 100);

            do
            {
                Console.WriteLine("\nCene bez i sa PDV-om:");
                foreach (KeyValuePair<string, int> pair in obrok)
                {
                    Console.WriteLine("{0} - {1}din. - {2}din.", pair.Key, pair.Value, (pair.Value * pdv) + pair.Value);
                }
                Console.WriteLine("11.Zavrsi narudzbinu");
                Console.WriteLine("12.Zatvori kasu");
                Console.WriteLine("\nIzaberite opciju?");

                try
                {
                    opcija = int.Parse(Console.ReadLine());
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    FileLogger.Error("Greska! Pogresan unos.");
                    break;
                }

                switch (opcija)
                {
                    case 1:
                        Izbor("corba", 200, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 2:
                        Izbor("pasulj", 300, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 3:
                        Izbor("musaka", 350, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 4:
                        Izbor("vesalica", 400, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 5:
                        Izbor("snicla", 400, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 6:
                        Izbor("coca cola", 200, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 7:
                        Izbor("fanta", 200, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 8:
                        Izbor("pivo", 300, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 9:
                        Izbor("rakija", 250, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case 10:
                        Izbor("voda", 100, sveNarudzbine, trenutnaNarudzbina);
                        break;
                    case (int)KomandeEnum.ZAVRSI_NARIDZBINU:
                        if(trenutnaNarudzbina.Count > 0) NaplataRacuna(trenutnaNarudzbina);
                        break;
                    case (int)KomandeEnum.ZATVORI_KASU:
                        Console.WriteLine("Izlaz");
                        break;
                    default:
                        Console.WriteLine("Pogresan unos!");
                        FileLogger.Warn("Unet je znak ili slovo umesto odgovarajuceg broja");
                        break;
                }
            } while (opcija != 12);

            Console.WriteLine("\n\n-------- KRAJ DANA --------");
            Console.WriteLine("Sve narudzbine:");
            foreach (Narudzbina n in sveNarudzbine)
            {
                Console.WriteLine("{0} - {1}din", n.Artikal, n.Cena);
                using (StreamWriter sw = File.AppendText(putanja))
                {
                    sw.WriteLine(n);
                }
            }

            FileLogger.Debug("Kraj programa.");

            Console.ReadKey();
        }


        public static void Izbor(
            string obrok, 
            int cena,
            ArrayList sveNarudzbine,
            List<Narudzbina> trenutnaNarudzbina)
        {
            double cenaPDV;

            Console.WriteLine("Izabrali ste {0} - Cena bez PDV-a {1} din. Cena sa PDV-om {2} din.", 
                obrok, cena, cenaPDV = cena * 0.0825 + cena);

            Narudzbina n = new Narudzbina(obrok, cenaPDV);

            sveNarudzbine.Add(n);
            trenutnaNarudzbina.Add(n);
        }

        public static void NaplataRacuna(IList<Narudzbina> trenutnaNarudzbina)
        {
            double kusur, ukupnaCena = 0, novac;

            Console.WriteLine("\n\nVasa narudzbina je:");
            foreach (Narudzbina n in trenutnaNarudzbina)
            {
                Console.WriteLine("{0} - {1}din.", n.Artikal, n.Cena);
                ukupnaCena += n.Cena;
            }

            Console.WriteLine($"Vas racun: {ukupnaCena} din.");

            Console.WriteLine("Unesite novac?");
            novac = double.Parse(Console.ReadLine());

            while (novac < ukupnaCena)
            {
                Console.WriteLine("Nemate dovoljno novac!");
                Console.WriteLine("Unesite novac ponovo?");
                novac = double.Parse(Console.ReadLine());
            }

            kusur = novac - ukupnaCena;
            Console.WriteLine($"Vas kusur je: {kusur}");

            Console.WriteLine("Dodjite nam opet!\n\n");
            trenutnaNarudzbina = new List<Narudzbina>();
        }

        enum KomandeEnum
        {
            ZAVRSI_NARIDZBINU = 11,
            ZATVORI_KASU = 12
        }
    }
}
