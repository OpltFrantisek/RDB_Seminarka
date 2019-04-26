using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RdbSem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            RDB_SeminarkaEntities db = new RDB_SeminarkaEntities();
           foreach(var item in checkedList_Tabulky.CheckedItems)
                switch (item)
                {
                    case "Autobus" :
                        CSVHelper.ExportAutobus(db.Autobus,"Autobus.csv");
                        HashCreator.CreateMD5("Autobus.csv");
                            break;
                    case "Jizda":; break;
                    case "Jizdenka":; break;
                    case "Klient":
                        CSVHelper.ExportKlient(db.Klient, "klient.csv");
                        HashCreator.CreateMD5("klient.csv"); ; break;
                    case "Kontakt":; break;
                    case "Lokalita":; break;
                    case "Ridic":; break;
                    case "Trasy":; break;
                    case "TypKontaktu":; break;
                    case "Znacka":; break;
                    default: break;
                }
        }
    }
    static class HashCreator
    {
        public static string CreateMD5(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    return md5.ComputeHash(stream).ToString();
                }
            }
        }
    }
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
