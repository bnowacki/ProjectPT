using LibraryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class WypelnieniePlik : DataFill
    {
        public WypelnieniePlik() { }

        public void Fill(DataContext context)
        {
            string[] lines = System.IO.File.ReadAllLines(@"Wykaz.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split(';');
                context.wykazy.Add(new Wykaz(words[0], words[1], words[2]));

            }
            lines = System.IO.File.ReadAllLines(@"Katalog.txt");
            int i = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(';');
                context.katalogi.Add(i, new Katalog(words[0], words[1], words[2]));
                i++;
            }

            lines = System.IO.File.ReadAllLines(@"OpisStanu.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split(';');

                foreach (Katalog k in context.katalogi.Values)
                {
                    if (k.Numer == words[0]) context.opisy.Add(new OpisStanu(k, DateTime.Parse(words[1]), Int32.Parse(words[2]), Double.Parse(words[3])));
                }
            }


            lines = System.IO.File.ReadAllLines(@"Zdarzenie.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split(';');
                Katalog K = new Katalog("", "", "");
                Wykaz W = new Wykaz("", "", "");
                OpisStanu O = new OpisStanu(K, DateTime.Now, 0, 0.0);
                foreach (Wykaz ww in context.wykazy)
                {
                    if (ww.Id == words[0])
                    {
                        W = ww;
                        break;
                    }
                }

                foreach (Katalog kk in context.katalogi.Values)
                {
                    if (kk.Numer == words[1])
                    {
                        K = kk;
                        break;
                    }
                }

                foreach (OpisStanu oo in context.opisy)
                {
                    if (oo.K1.Numer == K.Numer && oo.Data_zakupu == DateTime.Parse(words[2]))
                    {
                        O = oo;
                        break;
                    }
                }
                context.zdarzenia.Add(new Zdarzenie(W, O, DateTime.Parse(words[3])));
            }
        }
    }
}
