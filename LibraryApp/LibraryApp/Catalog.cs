using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class Katalog
    {
        private string tytul;
        private string autor;
        private string numer;

        public Katalog(string tytul, string autor, string numer)
        {
            this.tytul = tytul;
            this.autor = autor;
            this.numer = numer;
        }

        public string Tytul { get => tytul; set => tytul = value; }
        public string Numer { get => numer; set => numer = value; }
        public string Autor { get => autor; set => autor = value; }
        public string All { get => numer + " " + tytul + " " + autor; }

        public override bool Equals(object obj)
        {
            Katalog other = (Katalog)obj;
            return this.numer == other.numer;
        }
    }
}
