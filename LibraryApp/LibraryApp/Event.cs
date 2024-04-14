using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class Zdarzenie
    {
        private Wykaz W; //kto
        private OpisStanu O; //
        private DateTime data_zakupu;

        public Zdarzenie(Wykaz w, OpisStanu o, DateTime data_zakupu)
        {
            W = w;
            O = o;
            Data_zakupu = data_zakupu;
        }

        public Wykaz w { get => W; set => W = value; }
        public OpisStanu o { get => O; set => O = value; }
        public DateTime Data_zakupu { get => data_zakupu; set => data_zakupu = value; }
        public string All { get => this.W.All + " " + this.O.All + " " + Data_zakupu; }
        //Z tym nie dziala import/ bez testy
        public override bool Equals(object obj)
        {
            if (obj is Zdarzenie)
            {
                Zdarzenie other = (Zdarzenie)obj;
                return this.W.Equals(other.W) && this.data_zakupu == other.data_zakupu;
            }
            return base.Equals(obj);
        }
    }
}
