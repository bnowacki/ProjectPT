using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    class WypelnienieLosowymi : DataFill
    {
        public WypelnienieLosowymi() { }

        public static int SIZE = 100;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void Fill(DataContext context)
        {
            for (int i = 0; i < SIZE; i++)
            {
                //context.wykazy.Add(new Wykaz(RandomString(), "Kowalski", "1"));
                Console.Write(random);
                Console.Write("elo");
            }
        }
    }
}
