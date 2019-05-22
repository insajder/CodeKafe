using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKafe2
{
    class Narudzbina
    {
        public string Artikal { get; set; }
        public double Cena { get; set; }

        public Narudzbina() { }

        public Narudzbina(string artikal, double cena)
        {
            Artikal = artikal;
            Cena = cena;
        } 

        public void Stampa()
        {
            Console.WriteLine($"{Artikal} - {Cena}");
        }

        public override string ToString()
        {
            return $"{Artikal}, {Cena}";
        }
    }
}
