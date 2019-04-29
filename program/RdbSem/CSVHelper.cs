using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Text;
using System.Linq;
namespace RdbSem
{
    static class CSVHelper
    {
       
        internal static List<Klient> ImportKlient(string path)
        {
            List<Klient> klients = new List<Klient>(); 
            using (var fr = File.OpenText(path))
            {             
                string line;
                while((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    klients.Add(new Klient() { jmeno = tmp[0], prijmeni = tmp[1], email = tmp[2] });
                }
            }
            return klients;
        }

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
                bool res =  Encoding.UTF8.GetBytes(item.jmeno).Select(x => (int)x).Sum() % 2 == 1 ? true : false ;
                string jmeno;
                if (res)
                    jmeno = item.jmeno;
                else
                    jmeno = item.jmeno + char.ConvertFromUtf32(0);
                sb.Append(string.Format("{0},{1},{2}\n", jmeno, item.prijmeni,item.email));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }
    }
}
