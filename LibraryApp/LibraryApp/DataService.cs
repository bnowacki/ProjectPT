using LibraryApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    public class DataService
    {
        private DataRepository data;

        public DataService(DataRepository data)
        {
            this.data = data;
        }

        #region Wyswietl

        public void Wyswietl(IEnumerable<Wykaz> wykazy)
        {
            List<Wykaz> nowa = wykazy.ToList<Wykaz>();

            for (int i = 0; i < wykazy.Count(); i++)
            {
                Console.WriteLine(nowa[i].All);
            }
        }

        public void Wyswietl(IEnumerable<Katalog> rzeczy)
        {
            Dictionary<int, Katalog> nowa = rzeczy.ToDictionary(x => Int32.Parse(x.Numer), x => x);

            for (int i = 0; i < rzeczy.Count(); i++)
            {
                Console.WriteLine(nowa[i].All);
            }
        }

        public void Wyswietl(IEnumerable<OpisStanu> rzeczy)
        {
            List<OpisStanu> nowa = rzeczy.ToList<OpisStanu>();

            for (int i = 0; i < rzeczy.Count(); i++)
            {
                Console.WriteLine(nowa[i].All);
            }
        }

        public void Wyswietl(IEnumerable<Zdarzenie> rzeczy)
        {
            ObservableCollection<Zdarzenie> nowa = new ObservableCollection<Zdarzenie>(rzeczy);

            for (int i = 0; i < rzeczy.Count(); i++)
            {
                Console.WriteLine(nowa[i].All);
            }
        }

        #endregion

        #region Szukaj
        public List<Wykaz> SzukajWykaz(string zapytanie)
        {
            List<Wykaz> all = this.data.GetAllWykaz().ToList<Wykaz>();
            List<Wykaz> nowa = new List<Wykaz>();
            String tmp = "";
            for (int i = 0; i < this.data.GetAllWykaz().Count(); i++)
            {
                tmp = all[i].All;
                if (tmp.Contains(zapytanie)) nowa.Add(all[i]);
            }
            return nowa;
        }

        public Dictionary<int, Katalog> SzukajKatalog(string zapytanie)
        {
            Dictionary<int, Katalog> all = this.data.GetAllKatalog().ToDictionary(x => Int32.Parse(x.Numer), x => x);
            Dictionary<int, Katalog> nowa = new Dictionary<int, Katalog>();
            String tmp = "";
            int index = 0;
            for (int i = 0; i < this.data.GetAllWykaz().Count(); i++)
            {
                tmp = all[i].All;
                if (tmp.Contains(zapytanie))
                {
                    nowa.Add(index, all[i]);
                    index++;
                }
            }
            return nowa;
        }

        public List<OpisStanu> SzukajOpisStanu(double min, double maks)
        {
            List<OpisStanu> all = this.data.GetAllOpisStanu().ToList();
            List<OpisStanu> nowa = new List<OpisStanu>();

            for (int i = 0; i < this.data.GetAllWykaz().Count(); i++)
            {
                if (all[i].Cena > min && all[i].Cena < maks) nowa.Add(all[i]);
            }
            return nowa;
        }

        public ObservableCollection<Zdarzenie> SzukajZdarzenia(DateTime start, DateTime end)
        {
            ObservableCollection<Zdarzenie> all = new ObservableCollection<Zdarzenie>(this.data.GetAllZdarzenia());
            ObservableCollection<Zdarzenie> nowa = new ObservableCollection<Zdarzenie>();

            for (int i = 0; i < this.data.GetAllWykaz().Count(); i++)
            {
                if (all[i].Data_zakupu > start && all[i].Data_zakupu < end) nowa.Add(all[i]);
            }
            return nowa;
        }
        #endregion

        #region Powiazane
        public IEnumerable<Zdarzenie> ZdarzenieDlaWykazu(Wykaz W)
        {
            ObservableCollection<Zdarzenie> all = new ObservableCollection<Zdarzenie>(this.data.GetAllZdarzenia());
            ObservableCollection<Zdarzenie> nowa = new ObservableCollection<Zdarzenie>();

            foreach (var zz in all)
            {
                if (zz.w.Equals(W)) nowa.Add(zz);
            }

            return (IEnumerable<Zdarzenie>)nowa;
        }

        public IEnumerable<Zdarzenie> ZdarzeniaDlaOpisuStanu(OpisStanu O)
        {
            ObservableCollection<Zdarzenie> all = new ObservableCollection<Zdarzenie>(this.data.GetAllZdarzenia());
            ObservableCollection<Zdarzenie> nowa = new ObservableCollection<Zdarzenie>();

            foreach (var zz in all)
            {
                if (zz.o.Equals(O)) nowa.Add(zz);
            }

            return (IEnumerable<Zdarzenie>)nowa;
        }
        public IEnumerable<OpisStanu> OpisStanuDlaKatalogu(Katalog K)
        {
            List<OpisStanu> all = this.data.GetAllOpisStanu().ToList<OpisStanu>();
            List<OpisStanu> nowa = new List<OpisStanu>();

            foreach (var oo in all)
            {
                if (oo.K1.Equals(K)) nowa.Add(oo);
            }

            return (IEnumerable<OpisStanu>)nowa;
        }
        #endregion

        #region Dodaj
        public void DodajWykaz(Wykaz W) => this.data.AddWykaz(W);
        public void DodajKatalog(Katalog K) => this.data.AddKatalog(K);
        public void DodajZdarzenie(Zdarzenie Z) => this.data.AddZdarzenie(Z);
        public void DodajOpisStanu(OpisStanu O) => this.data.AddOpisStanu(O);
        #endregion

        #region Stworz
        public void DodajWykaz(string firstName, string lastName, string id) => this.data.AddWykaz(new Wykaz(firstName, lastName, id));
        public void DodajKatalog(string tytul, string autor, string numer) => this.data.AddKatalog(new Katalog(tytul, autor, numer));
        public void DodajZdarzenie(Wykaz w, OpisStanu o, DateTime data_zakupu) => this.data.AddZdarzenie(new Zdarzenie(w, o, data_zakupu));
        public void DodajOpisStanu(Katalog k, DateTime data_zakupu, int ilosc, double cena) => this.data.AddOpisStanu(new OpisStanu(k, data_zakupu, ilosc, cena));
        #endregion

        #region Operacje na Zdarzeniach (wg UML)
        public Zdarzenie DodajZdarzenie(Wykaz W, OpisStanu O)
        {
            return new Zdarzenie(W, O, DateTime.Now);
        }

        public void UsunZdarzenie(Wykaz W, OpisStanu O)
        {
            ObservableCollection<Zdarzenie> all = new ObservableCollection<Zdarzenie>(this.data.GetAllZdarzenia());

            foreach (var zz in all)
            {
                if (zz.w.Equals(W) && zz.o.Equals(O)) this.data.DeleteZdarzenie(zz);
            }
        }
        #endregion
    }
}
