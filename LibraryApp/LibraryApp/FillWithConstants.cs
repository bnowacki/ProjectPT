using LibraryApp;
using System;

namespace LibraryApp
{
    public class WypelnianieStalymi : DataFill
    {
        public WypelnianieStalymi() { }

        public void Fill(DataContext context)
        {
            //wykazy - klienci
            context.wykazy.Add(new Wykaz("Jan", "Kowalski", "1"));
            context.wykazy.Add(new Wykaz("Adam", "Maly", "2"));
            context.wykazy.Add(new Wykaz("Tomasz", "Sredni", "3"));
            context.wykazy.Add(new Wykaz("Mikolaj", "Duzy", "4"));
            context.wykazy.Add(new Wykaz("Piotr", "Nowak", "5"));
            context.wykazy.Add(new Wykaz("Anna", "Wisniewska", "6"));

            //katalogi - ksiazki
            context.katalogi.Add(0, new Katalog("Harry Potter", "J.K. Rowling", "0"));
            context.katalogi.Add(1, new Katalog("Pan Tadeusz", "Adam Mickiewicz", "1"));
            context.katalogi.Add(2, new Katalog("Lalka", "Boleslaw Prus", "2"));
            context.katalogi.Add(3, new Katalog("Dziady", "Adam Mickiewicz", "3"));
            context.katalogi.Add(4, new Katalog("Wiedzmin", "Andrzej Sapkowski", "4"));
            context.katalogi.Add(5, new Katalog("Duma i Uprzedzenie", "Jane Austin", "5"));

            //opisy stanu
            for (int i = 0; i < 6; i++)
            {
                context.opisy.Add(new OpisStanu(context.katalogi[i], DateTime.Today, i + 3, 10.99 * (i + 1)));
            }

            //zdarzenia
            for (int i = 0; i < 6; i++)
            {
                context.zdarzenia.Add(new Zdarzenie(context.wykazy[i], context.opisy[i], DateTime.Today));
            }

            //Dodanie aby Punkt 2 -> obiektów z relacjami wiele do jednego (np. wiele wypożyczeń => jedna książka).
            context.zdarzenia.Add(new Zdarzenie(context.wykazy[0], context.opisy[5], DateTime.Today));
            context.zdarzenia.Add(new Zdarzenie(context.wykazy[1], context.opisy[5], DateTime.Today));
            context.zdarzenia.Add(new Zdarzenie(context.wykazy[2], context.opisy[5], DateTime.Today));
            context.zdarzenia.Add(new Zdarzenie(context.wykazy[3], context.opisy[5], DateTime.Today));

        }

    }
}