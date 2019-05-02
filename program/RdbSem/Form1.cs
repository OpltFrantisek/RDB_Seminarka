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
        const char WHITE_CHAR = '@';

        static string[] order = { "klient.csv", "lokalita.csv", "trasy.csv", "typkontaktu.csv", "ridic.csv", "kontakt.csv", "znacka.csv", "autobus.csv", "jizda.csv", "jizdenka.csv" };

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
                            hashs.Add(HashCreator.CreateMD5("jizda.csv"));
                            break;
                        case "Jizdenka":
                            CSVHelper.ExportJizdenka(db.Jizdenka, "jizdenka.csv");
                            hashs.Add(HashCreator.CreateMD5("jizdenka.csv"));
                            ; break;
                        case "Klient":
                            CSVHelper.ExportKlient(db.Klient, "klient.csv");
                            hashs.Add(HashCreator.CreateMD5("klient.csv"));
                            break;
                        case "Kontakt":
                            CSVHelper.ExportKontakt(db.Kontakt, "kontakt.csv");
                            hashs.Add(HashCreator.CreateMD5("kontakt.csv"));
                            ; break;
                        case "Lokalita":
                            CSVHelper.ExportLokalita(db.Lokalita, "lokalita.csv");
                            hashs.Add(HashCreator.CreateMD5("lokalita.csv"));
                            ; break;
                        case "Ridic":
                            CSVHelper.ExportRidic(db.Ridic, "ridic.csv");
                            hashs.Add(HashCreator.CreateMD5("ridic.csv"));
                            ; break;
                        case "Trasy":
                            CSVHelper.ExportTrasy(db.Trasy, "trasy.csv");
                            hashs.Add(HashCreator.CreateMD5("trasy.csv"));
                            ; break;
                        case "TypKontaktu":
                            CSVHelper.ExportTypKontaktu(db.TypKontaktu, "typkontaktu.csv");
                            hashs.Add(HashCreator.CreateMD5("typkontaktu.csv"));
                            ; break;
                        case "Znacka":
                            CSVHelper.ExportZnacka(db.Znacka, "znacka.csv");
                            hashs.Add(HashCreator.CreateMD5("znacka.csv"));
                            ; break;
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
                if (db.Hash.FirstOrDefault(x => x.hash1 == hash) == null)
                {
                    db.Hash.Add(new Hash() { hash1 = hash });
                }
            db.SaveChanges();
        }
        private static bool ExistHash(string hash, RDB_SeminarkaEntities db)
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
            fdlg.Multiselect = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                using (var db = new RDB_SeminarkaEntities())
                {
                    // var neco = (fdlg.FileName.Split('\\').Last());
                    var names = fdlg.FileNames.Select(x =>
                    {
                        var tmp = x.Split('\\').Last();
                        return new Tuple<string, string>(x, tmp);
                    });
                    foreach (var file in order)
                    {
                        var tmp = names.FirstOrDefault(x => x.Item2 == file);
                        if (tmp == null)
                            continue;
                        var path = tmp.Item1;
                        switch (file)
                        {
                            case "autobus.csv":
                                var autobuses = CSVHelper.ImportAutobus(path);
                                foreach (var autobus in autobuses)
                                    if (db.Autobus.FirstOrDefault(x => x.spz == autobus.spz) == null)
                                        db.Autobus.Add(autobus);
                                break;

                            case "jizda.csv":
                                var jizdas = CSVHelper.ImportJizda(path);
                                foreach (var jizda in jizdas)
                                    if (db.Jizda.FirstOrDefault(x => x.cas == jizda.cas && x.linka == jizda.linka) == null)
                                        db.Jizda.Add(jizda);
                                break;

                            case "jizdenka.csv":
                                var jizdenkas = CSVHelper.ImportJizdenka(path);
                                foreach (var jizdenka in jizdenkas)
                                    if (db.Jizdenka.FirstOrDefault(x => x.cislo == jizdenka.cislo) == null)
                                        db.Jizdenka.Add(jizdenka);
                                break;

                            case "klient.csv":
                                var klients = CSVHelper.ImportKlient(path);
                                foreach (var klient in klients)
                                    if (db.Klient.FirstOrDefault(x => x.email == klient.email) == null)
                                    {
                                        // Storing watermarked values
                                        bool evenLength = klient.email.Length % 2 == 0 ? true : false;
                                        bool evenSum = Encoding.UTF8.GetBytes(klient.email).Select(x => (int)x).Sum() % 2 == 0 ? true : false;

                                        // If both are not even or both are not odd then try to watermark
                                        if (!((evenLength && evenSum) || (!evenLength && !evenSum)))
                                        {
                                            bool evenPrijmeni = klient.prijmeni.Length % 2 == 0 ? true : false;
                                            if (evenPrijmeni)
                                                klient.prijmeni = klient.prijmeni + WHITE_CHAR;
                                        }
                                        db.Klient.Add(klient);
                                    }
                                break;

                            case "kontakt.csv":
                                var kontakts = CSVHelper.ImportKontakt(path);
                                foreach (var kontakt in kontakts)
                                    if (db.Kontakt.FirstOrDefault(x => x.hodnota == kontakt.hodnota) == null)
                                        db.Kontakt.Add(kontakt);
                                break;

                            case "lokalita.csv":
                                var lokalitas = CSVHelper.ImportLokalita(path);
                                foreach (var lokalita in lokalitas)
                                    if (db.Lokalita.FirstOrDefault(x => x.nazev == lokalita.nazev) == null)
                                        db.Lokalita.Add(lokalita);
                                break;

                            case "ridic.csv":
                                var ridics = CSVHelper.ImportRidic(path);
                                foreach (var ridic in ridics)
                                    if (db.Ridic.FirstOrDefault(x => x.cislo_rp == ridic.cislo_rp) == null)
                                        db.Ridic.Add(ridic);
                                break;

                            case "trasy.csv":
                                var trasies = CSVHelper.ImportTrasy(path);
                                foreach (var trasa in trasies)
                                    if (db.Trasy.FirstOrDefault(x => x.linka == trasa.linka) == null)
                                        db.Trasy.Add(trasa);
                                break;

                            case "typkontaktu.csv":
                                var typKontaktus = CSVHelper.ImportTypKontaktu(path);
                                foreach (var typKontaktu in typKontaktus)
                                    if (db.TypKontaktu.FirstOrDefault(x => x.typ == typKontaktu.typ) == null)
                                        db.TypKontaktu.Add(typKontaktu);
                                break;

                            case "znacka.csv":
                                var znackas = CSVHelper.ImportZnacka(path);
                                foreach (var znacka in znackas)
                                    if (db.Znacka.FirstOrDefault(x => x.znacka1 == znacka.znacka1) == null)
                                        db.Znacka.Add(znacka);
                                break;
                            default:
                                break;
                        }
                        db.Database.Log = Console.Write;
                        db.SaveChanges();
                    }

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
                            var klients = CSVHelper.ImportKlient(fdlg.FileName);

                            if (!ExistHash(HashCreator.CreateMD5(fdlg.FileName), db))
                            {
                                int expected = 0;
                                int actual = 0;
                                foreach (var klient in klients)
                                {
                                    bool evenLength = klient.email.Length % 2 == 0 ? true : false;
                                    bool evenSum = Encoding.UTF8.GetBytes(klient.email).Select(x => (int)x).Sum() % 2 == 0 ? true : false;

                                    if (!((evenLength && evenSum) || (!evenLength && !evenSum)))
                                    {
                                        bool evenPrijmeni = klient.prijmeni.Length % 2 == 0 ? true : false;
                                        char last = klient.prijmeni[klient.prijmeni.Length - 1];

                                        if (evenPrijmeni)
                                        {
                                            // should be odd, because even entries would be watermarked
                                            expected++;
                                        }
                                        else
                                        {
                                            if (last == WHITE_CHAR)
                                            {
                                                expected++;
                                                actual++;
                                            }
                                        }
                                    }
                                }

                                if ((float)actual / expected > 0.5) // If there are more than 50% watermarks, then its ok
                                    message = string.Format("Soubor {0} byl vygenerovan v nasi DB.", neco);
                                else
                                    message = string.Format("Soubor {0} byl vygenerovan v nasi DB ale data byli upraveny.", neco);
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
            foreach (var item in values)
            {
                bool res = Encoding.UTF8.GetBytes(item).Select(x => (int)x).Sum() % 2 == 1 ? true : false;
                if (res == false && !(item.Contains(char.ConvertFromUtf32(0)) || item.Contains(' ')))
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
