using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            using (var db = new RDB_SeminarkaEntities())
            {
                foreach (var item in checkedList_Tabulky.CheckedItems)
                {
                    List<string> hashs = new List<string>();
                    switch (item)
                    {
                        case "Autobus":
                            CSVHelper.ExportAutobus(db.Autobus, "autobus.csv");
                            hashs.Add(HashCreator.CreateMD5("autobus.csv"));
                            break;
                        case "Jizda":
                            CSVHelper.ExportJizda(db.Jizda, "jizda.csv");
                            break;
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

        private void buttonImport_Click(object sender, EventArgs e)
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
                        case "autobus.csv":
                            var autobuses = CSVHelper.ImportAutobus(fdlg.FileName);
                            foreach(var autobus in autobuses)
                                if(db.Autobus.FirstOrDefault(x => x.spz == autobus.spz) == null)
                                    db.Autobus.Add(autobus);
                            break;

                        case "jizda.csv":
                            var jizdas = CSVHelper.ImportJizda(fdlg.FileName);
                            foreach(var jizda in jizdas)
                                if(db.Jizda.FirstOrDefault(x => x.cas == jizda.cas && x.linka == jizda.linka) == null)
                                    db.Jizda.Add(jizda);
                            break;

                        case "jizdenka.csv":
                            var jizdenkas = CSVHelper.ImportJizdenka(fdlg.FileName);
                            foreach(var jizdenka in jizdenkas)
                                if(db.Jizdenka.FirstOrDefault(x => x.cislo == jizdenka.cislo) == null)
                                    db.Jizdenka.Add(jizdenka);
                            break;

                        case "klient.csv":
                            var klients = CSVHelper.ImportKlient(fdlg.FileName);                        
                            foreach (var klient in klients)
                                if (db.Klient.FirstOrDefault(x => x.email == klient.email) == null)
                                    db.Klient.Add(klient);
                            break;

                        case "kontakt.csv":
                            var kontakts = CSVHelper.ImportKontakt(fdlg.FileName);
                            foreach(var kontakt in kontakts)
                                if(db.Kontakt.FirstOrDefault(x => x.hodnota == kontakt.hodnota) == null)
                                    db.Kontakt.Add(kontakt);
                            break;

                        case "lokalita.csv":
                            var lokalitas = CSVHelper.ImportLokalita(fdlg.FileName);
                            foreach(var lokalita in lokalitas)
                                if(db.Lokalita.FirstOrDefault(x => x.nazev == lokalita.nazev) == null)
                                    db.Lokalita.Add(lokalita);
                            break;
                        
                        case "ridic.csv":
                            var ridics = CSVHelper.ImportRidic(fdlg.FileName);
                            foreach (var ridic in ridics)
                                if(db.Ridic.FirstOrDefault(x => x.cislo_rp == ridic.cislo_rp) == null)
                                    db.Ridic.Add(ridic);
                            break;

                        case "trasy.csv":
                            var trasies = CSVHelper.ImportTrasy(fdlg.FileName);
                            foreach(var trasa in trasies)
                                if(db.Trasy.FirstOrDefault(x => x.linka == trasa.linka) == null)
                                    db.Trasy.Add(trasa);
                            break;

                        case "typkontaktu.csv":
                            var typKontaktus = CSVHelper.ImportTypKontaktu(fdlg.FileName);
                            foreach(var typKontaktu in typKontaktus)
                                if(db.TypKontaktu.FirstOrDefault(x => x.typ == typKontaktu.typ) == null)
                                    db.TypKontaktu.Add(typKontaktu);
                            break;

                        case "znacka.csv":
                            var znackas = CSVHelper.ImportZnacka(fdlg.FileName);
                            foreach(var znacka in znackas)
                                if(db.Znacka.FirstOrDefault(x => x.znacka1 == znacka.znacka1) == null)
                                    db.Znacka.Add(znacka);
                            break;
                        default: 
                            break;
                    }
                    db.SaveChanges();
                    MessageBox.Show("Done!");
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
                    string message = "";
                    switch (neco)
                    {
                        case "autobus":
                            break;
                        case "jizda":; break;
                        case "jizdenka":; break;
                        case "klient.csv":
                            var result = CSVHelper.ImportKlient(fdlg.FileName);
                            
                            if (!ExistHash(HashCreator.CreateMD5(fdlg.FileName), db))
                            {
                                if (IsfromDB(result.Select(x => x.jmeno).ToList()))
                                    message = string.Format("Soubor {0} byl vygenerovan v nasi DB ale data byli upraveny",neco);
                                else
                                    message = string.Format("Soubor {0} není z naší DB", neco);
                                
                            }
                            else
                            {
                                message = string.Format("Soubor {0} je OK", neco);                             
                            }
                            break;
                        case "kontakt":; break;
                        case "lokalita":; break;
                        case "ridic":; break;
                        case "trasy":; break;
                        case "typKontaktu":; break;
                        case "znacka":; break;
                        default: break;
                    }
                    MessageBox.Show(message);
                }

            }
        }

        private bool IsfromDB(List<string> values)
        {
            foreach(var item in values)
            {
                bool res = Encoding.UTF8.GetBytes(item).Select(x => (int)x).Sum() % 2 == 1 ? true : false;
                if (res == false && !(item.Contains(char.ConvertFromUtf32(0))|| item.Contains(' ')))
                    return false;
            }
            return true;
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
