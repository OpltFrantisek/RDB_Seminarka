using System.Data.Entity;
using System.IO;
using System.Text;

namespace RdbSem
{
    static class CSVHelper
    {



        internal static void ExportAutobus(DbSet<Autobus> autobus,string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var item in autobus)
            {
                sb.Append(string.Format("{0},{1}\n", item.spz, item.Znacka1.znacka1));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportKlient(DbSet<Klient> klient, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in klient)
            {
                sb.Append(string.Format("{0},{1},{2}\n", item.jmeno, item.prijmeni,item.email));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }
    }
}
