using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {

            DataContext context = new DataContext();
            WypelnianieStalymi stale = new WypelnianieStalymi();
            DataRepository data = new DataRepository(context, stale);
            data.FillStatic();
            DataService service = new DataService(data);

            Export export = new Export();
            export.JSON(data);
            export.CSV(data);

            //Import z plikow JSON do DataRepository
            Console.WriteLine("JSON IMPORT:");
            DataContext context_json = new DataContext();
            WypelnienieJSON json = new WypelnienieJSON();
            DataRepository data_json = new DataRepository(context_json, json);
            data_json.FillStatic();
            DataService service_json = new DataService(data_json);

            service.Wyswietl(data_json.GetAllWykaz());
            service.Wyswietl(data_json.GetAllKatalog());
            service.Wyswietl(data_json.GetAllOpisStanu());
            service.Wyswietl(data_json.GetAllZdarzenia());

            //Import z plikow CSV
            Console.WriteLine("\nCSV IMPORT:");
            DataContext context_csv = new DataContext();
            WypelnienieCSV csv = new WypelnienieCSV();
            DataRepository data_csv = new DataRepository(context_csv, csv);
            data_csv.FillStatic();
            DataService service_csv = new DataService(data_csv);

            service.Wyswietl(data_csv.GetAllWykaz());
            service.Wyswietl(data_csv.GetAllKatalog());
            service.Wyswietl(data_csv.GetAllOpisStanu());
            service.Wyswietl(data_csv.GetAllZdarzenia());

            Console.ReadKey();
        }
    }
}
