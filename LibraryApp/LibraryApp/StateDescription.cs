using LibraryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class OpisStanu
    {
        private Katalog K;
        private DateTime data_zakupu;
        private int ilosc;
        private double cena;

        public OpisStanu(Katalog k, DateTime data_zakupu, int ilosc, double cena)
        {
            K = k;
            this.data_zakupu = data_zakupu;
            this.ilosc = ilosc;
            this.cena = cena;
        }

        public Katalog K1 { get { return K; } set => K = value; }
        public DateTime Data_zakupu { get => data_zakupu; set => data_zakupu = value; }
        public int Ilosc { get => ilosc; set => ilosc = value; }
        public double Cena { get => cena; set => cena = value; }
        public string All { get => this.K.All + " " + Data_zakupu + " " + Ilosc + " " + Cena; }
        public override bool Equals(object obj)
        {
            if (obj is OpisStanu)
            {
                OpisStanu other = (OpisStanu)obj;
                return this.K.Equals(other.K) && this.data_zakupu == other.data_zakupu;
            }
            return base.Equals(obj);
        }
    }
}
