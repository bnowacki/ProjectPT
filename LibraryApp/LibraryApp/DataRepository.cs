using LibraryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class DataRepository
    {
        private DataContext context;
        private DataFill fill;

        public DataRepository(DataContext context, DataFill fill)
        {
            this.context = context;
            this.fill = fill;
        }

        public void FillStatic() => fill.Fill(context);

        //C.R.U.D. (Create, Read, Update, Delete)
        /* (Add) Dodawanie nowych danych do kolekcji
        (Get) Odczyt pojedynczych obiektów, np. na podstawie identyfikatora lub pozycji w kolekcji
        (GetAll) Odczyt wszystkich obiektów z kolekcji
        (Update) Aktualizacja danych w kolekcji - opcjonalnie, podając obiekt lub pozycję w kolekcji
        (Delete) Usuwanie wskazanych danych z kolekcji - podając obiekt lub pozycję w kolekcji
        */
        #region Wykaz
        public void AddWykaz(Wykaz W)
        {
            context.wykazy.Add(W);
        }

        public Wykaz GetWykaz(string id)
        {
            foreach (var W in context.wykazy)
            {
                if (W.Id == id)
                {
                    return W;
                }
            }
            throw new Exception("Brak takiego wykazu");
        }

        public IEnumerable<Wykaz> GetAllWykaz()
        {
            return context.wykazy;
        }

        public void DeleteWykaz(Wykaz w)//jezeli jest 
        {
            foreach (var z in context.zdarzenia)
            {
                if (z.w == w) throw new Exception("Czytelnik posiada wypozyczenie wiec nie mozna go usunac");
            }
            context.wykazy.Remove(w);
        }

        public void DeleteWykaz(string _id)
        {
            Wykaz tmp = GetWykaz(_id);

            foreach (var z in context.zdarzenia)
            {
                if (z.w == tmp) throw new Exception("Czytelnik posiada wypozyczenie wiec nie mozna go usunac");
            }
            context.wykazy.Remove(tmp);
        }

        #endregion

        #region Katalog
        private int KatalogCounter = 0;

        public int KatalogCounter1 { get => KatalogCounter; set => KatalogCounter = value; }

        public void AddKatalog(Katalog K)
        {
            context.katalogi.Add(KatalogCounter, K);
            KatalogCounter++;
        }

        public Katalog GetKatalog(int id)
        {
            return context.katalogi[id];
        }

        public IEnumerable<Katalog> GetAllKatalog()
        {
            return context.katalogi.Values;
        }

        public void DeleteKatalog(int id)
        {
            foreach (var O in context.opisy)
            {
                if (O.K1.Equals(context.katalogi[id])) throw new Exception("Nie można usunąć obiektu. Jest on w użyciu przez Opis stanu");
            }
            context.katalogi.Remove(id);
        }

        public void DeleteKatalog(Katalog K)
        {
            foreach (var O in context.opisy)
            {
                if (O.K1 == K) throw new Exception("Nie można usunąć obiektu. Jest on w użyciu przez Opis stanu");
            }

            for (int id = 0; id < context.katalogi.Count; id++)
            {
                if (context.katalogi[id].Equals(K))
                {
                    context.katalogi.Remove(id);
                    return;
                }
            }
        }
        #endregion

        #region Zdarzenie

        public void AddZdarzenie(Zdarzenie Z)
        {
            context.zdarzenia.Add(Z);
        }

        public Zdarzenie GetZdarzenie(int id)
        {
            return context.zdarzenia[id];
        }

        public IEnumerable<Zdarzenie> GetAllZdarzenia()
        {
            return context.zdarzenia;
        }

        public void DeleteZdarzenie(Zdarzenie Z)
        {
            foreach (var zz in context.zdarzenia)
            {
                if (Z.Equals(zz))
                {
                    context.zdarzenia.Remove(Z);
                    return;//zeby wyszedl po znalezieniu
                }
            }
            throw new Exception("Nie ma takiego zdarzenia");
        }

        public void DeleteZdarzenie(int _id)
        {
            if (_id >= context.zdarzenia.Count()) throw new Exception("Nie ma takiego zdarzenia");
            context.zdarzenia.Remove(context.zdarzenia[_id]);
        }
        #endregion

        #region OpisStanu

        public void AddOpisStanu(OpisStanu O)
        {
            context.opisy.Add(O);
        }

        public OpisStanu GetOpisStanu(int id)
        {
            return context.opisy[id];
        }

        public IEnumerable<OpisStanu> GetAllOpisStanu()
        {
            return context.opisy;
        }

        public void DeleteOpisStanu(OpisStanu O)
        {
            foreach (var z in context.zdarzenia)
            {
                if (z.o.Equals(O))
                {
                    throw new Exception("Opis jest w uzyciu");
                }
            }
            context.opisy.Remove(O);
        }

        public void DeleteOpisStanu(int id)
        {
            OpisStanu O = GetOpisStanu(id);
            foreach (var z in context.zdarzenia)
            {
                if (z.o.Equals(O))
                {
                    throw new Exception("Opis jest w uzyciu");
                }
            }
            context.opisy.Remove(O);
        }

        #endregion
    }
}

