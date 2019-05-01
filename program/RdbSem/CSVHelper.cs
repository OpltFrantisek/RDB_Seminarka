using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Text;
using System.Linq;
using System;

namespace RdbSem
{
    static class CSVHelper
    {
        #region Import Methods

        internal static List<Autobus> ImportAutobus(string path)
        {
            List<Autobus> autobuses = new List<Autobus>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    autobuses.Add(new Autobus() { spz = tmp[0], znacka = tmp[1] });
                }
            }
            return autobuses;
        }

        internal static List<Jizda> ImportJizda(string path)
        {
            List<Jizda> jizdas = new List<Jizda>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');

                    jizdas.Add(new Jizda() { linka = tmp[0], spz = tmp[1], cislo_rp = tmp[2], cas = timestampStringToDatetime(tmp[3]) });
                }
            }
            return jizdas;
        }

        internal static List<Jizdenka> ImportJizdenka(string path)
        {
            List<Jizdenka> jizdenkas = new List<Jizdenka>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    string email = tmp[1] == "\\N" ? null : tmp[1];
                    long cislo = long.Parse(tmp[3]);
                    jizdenkas.Add(new Jizdenka() { linka = tmp[0], email = email, cas = timestampStringToDatetime(tmp[2]), cislo = cislo });
                }
            }
            return jizdenkas;
        }

        internal static List<Klient> ImportKlient(string path)
        {
            List<Klient> klients = new List<Klient>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    klients.Add(new Klient() { jmeno = tmp[0], prijmeni = tmp[1], email = tmp[2] });
                }
            }
            return klients;
        }

        internal static List<Kontakt> ImportKontakt(string path)
        {
            List<Kontakt> kontakts = new List<Kontakt>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    kontakts.Add(new Kontakt() { hodnota = tmp[0], typ = tmp[1], cislo_rp = tmp[2] });
                }
            }
            return kontakts;
        }

        internal static List<Lokalita> ImportLokalita(string path)
        {
            List<Lokalita> lokalitas = new List<Lokalita>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    lokalitas.Add(new Lokalita() { nazev = tmp[0] });
                }
            }
            return lokalitas;
        }

        internal static List<Ridic> ImportRidic(string path)
        {
            List<Ridic> ridics = new List<Ridic>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    ridics.Add(new Ridic() { cislo_rp = tmp[0], jmeno = tmp[1], prijmeni = tmp[2] });
                }
            }
            return ridics;
        }

        internal static List<Trasy> ImportTrasy(string path)
        {
            List<Trasy> trasies = new List<Trasy>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    trasies.Add(new Trasy() { linka = tmp[0], odkud = tmp[1], kam = tmp[2] });
                }
            }
            return trasies;
        }

        internal static List<TypKontaktu> ImportTypKontaktu(string path)
        {
            List<TypKontaktu> typKontaktus = new List<TypKontaktu>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    typKontaktus.Add(new TypKontaktu() { typ = tmp[0] });
                }
            }
            return typKontaktus;
        }

        internal static List<Znacka> ImportZnacka(string path)
        {
            List<Znacka> znackas = new List<Znacka>();
            using (var fr = File.OpenText(path))
            {
                string line;
                while ((line = fr.ReadLine()) != null)
                {
                    var tmp = line.Split(',');
                    znackas.Add(new Znacka() { znacka1 = tmp[0] });
                }
            }
            return znackas;
        }

        private static System.DateTime timestampStringToDatetime(string timestamp)
        {
            // Because timestamp doesnt exist, it's converted to datetime
            double seconds = double.Parse(timestamp, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture);
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0);
            dateTime = dateTime.AddSeconds(seconds);

            return dateTime;
        }

        #endregion

        #region Export methods

        internal static void ExportAutobus(DbSet<Autobus> autobus, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in autobus)
            {
                sb.Append(string.Format("{0},{1}\n", item.spz, item.Znacka1.znacka1));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportJizda(DbSet<Jizda> jizda, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in jizda)
            {
                sb.Append(string.Format("{0},{1},{2},{3}\n", item.linka, item.spz, item.cislo_rp, dateTimeToTimestampString(item.cas)));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }

        }

        internal static void ExportJizdenka(DbSet<Jizdenka> jizdenka, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in jizdenka)
            {
                sb.Append(string.Format("{0},{1},{2},{3}\n", item.linka, item.email, dateTimeToTimestampString(item.cas), item.cislo.ToString()));
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
                bool res = Encoding.UTF8.GetBytes(item.jmeno).Select(x => (int)x).Sum() % 2 == 1 ? true : false;
                string jmeno;
                if (res)
                    jmeno = item.jmeno;
                else
                    jmeno = item.jmeno + char.ConvertFromUtf32(0);
                sb.Append(string.Format("{0},{1},{2}\n", jmeno, item.prijmeni, item.email));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportKontakt(DbSet<Kontakt> kontakt, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in kontakt)
            {
                sb.Append(string.Format("{0}\n", item.typ));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportLokalita(DbSet<Lokalita> lokalita, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lokalita)
            {
                sb.Append(string.Format("{0}\n", item.nazev));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportRidic(DbSet<Ridic> ridic, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in ridic)
            {
                sb.Append(string.Format("{0},{1},{2}\n", item.cislo_rp, item.jmeno, item.prijmeni));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportTrasy(DbSet<Trasy> trasy, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in trasy)
            {
                sb.Append(string.Format("{0},{1},{2}\n", item.linka, item.odkud, item.kam));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportTypKontaktu(DbSet<TypKontaktu> typKontaktu, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in typKontaktu)
            {
                sb.Append(string.Format("{0}\n", item.typ));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        internal static void ExportZnacka(DbSet<Znacka> znacka, string path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in znacka)
            {
                sb.Append(string.Format("{0}\n", item.znacka1));
            }
            using (var file = File.CreateText(path))
            {
                file.Write(sb.ToString());
            }
        }

        private static string dateTimeToTimestampString(DateTime dateTime)
        {
            double seconds = dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string timestamp = seconds.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);
            return timestamp;
        }

        #endregion
    }
}
