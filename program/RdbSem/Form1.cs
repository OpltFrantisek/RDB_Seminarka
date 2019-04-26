using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
            {
                List<string> hashs = new List<string>();
                switch (item)
                {
                    case "Autobus":
                        CSVHelper.ExportAutobus(db.Autobus, "Autobus.csv");
                        hashs.Add(HashCreator.CreateMD5("Autobus.csv"));
                        break;
                    case "Jizda":; break;
                    case "Jizdenka":; break;
                    case "Klient":
                        CSVHelper.ExportKlient(db.Klient, "klient.csv");
                        hashs.Add(HashCreator.CreateMD5("klient.csv"));
                        break;
                    case "Kontakt":; break;
                    case "Lokalita":; break;
                    case "Ridic":; break;
                    case "Trasy":; break;
                    case "TypKontaktu":; break;
                    case "Znacka":; break;
                    default: break;
                }
                SaveHashs(hashs, db);
            }
           
        }
        private static void SaveHashs(List<string> hashs, RDB_SeminarkaEntities db)
        {
            // TODO mozna by bylo lepsi udelet to na strane sql serveru 
            foreach (var hash in hashs)
                if(db.Hash.Where(x => x.hash1 == hash) == null)
                {
                    db.Hash.Add(new Hash() { hash1 = hash });
                }
            db.SaveChanges();
        }
        private static bool ExistHash(string hash,RDB_SeminarkaEntities db)
        {
            if (db.Hash.FirstOrDefault(x => x.hash1 == hash) != null)
                return true;
            return false;
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
}
