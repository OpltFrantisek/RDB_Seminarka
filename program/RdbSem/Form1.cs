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
            using (var db = new RDB_SeminarkaEntities())
            {
                foreach (var item in checkedList_Tabulky.CheckedItems)
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
           
        }
        private static void SaveHashs(List<string> hashs, RDB_SeminarkaEntities db)
        {
            // TODO mozna by bylo lepsi udelet to na strane sql serveru 
            foreach (var hash in hashs)
                if(db.Hash.FirstOrDefault(x => x.hash1 == hash) == null)
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

        private void buttonKontrola_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "import souboru";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                using (var db = new RDB_SeminarkaEntities()) {
                    var neco = (fdlg.FileName.Split('\\').Last());
                    switch (neco)
                    {
                        case "Autobus":
                            break;
                        case "Jizda":; break;
                        case "Jizdenka":; break;
                        case "klient.csv":
                            var resutlt = CSVHelper.ImportKlient(fdlg.FileName);
                            foreach (var klient in resutlt)
                                if (db.Klient.FirstOrDefault(x => x.email == klient.email) == null)
                                    db.Klient.Add(klient);
                            break;
                        case "Kontakt":; break;
                        case "Lokalita":; break;
                        case "Ridic":; break;
                        case "Trasy":; break;
                        case "TypKontaktu":; break;
                        case "Znacka":; break;
                        default: break;
                    }
                    db.SaveChanges();
                }
                 
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "import souboru";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                using (var db = new RDB_SeminarkaEntities())
                {
                    var neco = (fdlg.FileName.Split('\\').Last());
                    switch (neco)
                    {
                        case "Autobus":
                            break;
                        case "Jizda":; break;
                        case "Jizdenka":; break;
                        case "klient.csv":
                            var resutlt = CSVHelper.ImportKlient(fdlg.FileName);
                            if (!ExistHash(fdlg.FileName, db))
                            {
                                string message = "Soubor nebyl vygenerován touto DB";
                                MessageBox.Show(message);
                            }                          
                            break;
                        case "Kontakt":; break;
                        case "Lokalita":; break;
                        case "Ridic":; break;
                        case "Trasy":; break;
                        case "TypKontaktu":; break;
                        case "Znacka":; break;
                        default: break;
                    }
                    db.SaveChanges();
                }

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
                    var buffer = md5.ComputeHash(stream);
                    return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
