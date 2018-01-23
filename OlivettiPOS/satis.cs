using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using PBTFrontOfficeProject;
using System.Xml;
using System.Drawing.Printing;
using System.Threading;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace OlivettiPOS
{
    public partial class satis : Form
    {
        private System.DateTime sonTusZamani = default(System.DateTime);
        public static List<klavyeSablon.KlavyeTusu> kSablon = new List<klavyeSablon.KlavyeTusu>();

        public static List<klavyeSablon.ListeUrun> hUrun = new List<klavyeSablon.ListeUrun>();

        public aramisPOS.webService.ServiceSoapClient ws;
        private aramisPOS.webService.ServiceSoapClient service = new aramisPOS.webService.ServiceSoapClient();
        private functions fonksiyonlar = new functions();
        private int fcs = 0;
        private int belge = 0;
        private int banka = 0;
        private int belgeiptal = 0;
        private int yemekceki = 0;
        private int fisbasla = 0;
        private string musteriVNo = "";
        private string musteriUnvan = "";
        private string evrakNo = "";
        private string bankaText = "Kredi Kartı :";
        private string yemekCekiText = "Yemek Çeki :";
        private int baslamaSonuc = 0;
        private int kasaMesaj = 0;
        public string kasiyerID = "";
        private AppStarter app;
        private functions.AyarlarDB ayar = new functions.AyarlarDB();
        private functions.Kullanici kasiyerTanm = new functions.Kullanici();
        public string kdvTanimlari = ConfigurationManager.AppSettings["Departman"].ToString();
        private Thread t2;

        public satis(string kasyer)
        {
            kasiyerID = kasyer;
            //if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
            //{
            //    List<string> hata = FPUFuctions.Instance.CheckOpenOperations();
            //    foreach (string s in hata)
            //    {
            //        MessageBox.Show(s.ToString());
            //    }
            //}
            InitializeComponent();
            ayar = fonksiyonlar.Ayarlar();
            kasiyerTanm = fonksiyonlar.kasiyerBilgi(kasiyerID);

            if (kasiyerTanm.id.ToString() == "")
            {
                LogYaz("Kasiyer Giriş hatası");
                MessageBox.Show("Kasiyer giriş hatası!");
                Application.Exit();
            }

            if (ayar.KrediKartiSecimi == 1)
            {
                simpleButton58.Visible = true;
                bankaTipleri();
            }
            if (ayar.YemekCekiSecimi == 1)
            {
                simpleButton59.Visible = true;
                yemekCekiTipleri();
            }

            dataGridView1.Click += new EventHandler(dataGridView1_Click);
            t2 = new Thread(baglantKontrol);
        }

        private void bankaTipleri()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<OdemeTip>("odemeTipleri");
            var sepets = coll.Find("{'odemeTipi':1}").ToListAsync().Result;
            foreach (OdemeTip s in sepets)
            {
                switch (s.odemeNo)
                {
                    case 1:
                        simpleButton60.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton60.ToolTip = s.odemeNo.ToString();
                        break;

                    case 2:
                        simpleButton61.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton61.ToolTip = s.odemeNo.ToString();
                        break;

                    case 3:
                        simpleButton62.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton62.ToolTip = s.odemeNo.ToString();
                        break;

                    case 4:
                        simpleButton63.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton63.ToolTip = s.odemeNo.ToString();
                        break;

                    case 5:
                        simpleButton64.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton64.ToolTip = s.odemeNo.ToString();
                        break;

                    case 6:
                        simpleButton65.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton65.ToolTip = s.odemeNo.ToString();
                        break;

                    case 7:
                        simpleButton66.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton66.ToolTip = s.odemeNo.ToString();
                        break;

                    case 8:
                        simpleButton67.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton67.ToolTip = s.odemeNo.ToString();
                        break;

                    case 9:
                        simpleButton68.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton68.ToolTip = s.odemeNo.ToString();
                        break;

                    case 10:
                        simpleButton69.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton69.ToolTip = s.odemeNo.ToString();
                        break;

                    case 11:
                        simpleButton70.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton70.ToolTip = s.odemeNo.ToString();
                        break;

                    case 12:
                        simpleButton71.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton71.ToolTip = s.odemeNo.ToString();
                        break;
                }
            }
        }

        private void yemekCekiTipleri()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<OdemeTip>("odemeTipleri");
            var sepets = coll.Find("{'odemeTipi':2}").ToListAsync().Result;
            foreach (OdemeTip s in sepets)
            {
                switch (s.odemeNo)
                {
                    case 1:
                        simpleButton72.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton72.ToolTip = s.odemeNo.ToString();
                        break;

                    case 2:
                        simpleButton73.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton73.ToolTip = s.odemeNo.ToString();
                        break;

                    case 3:
                        simpleButton74.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton74.ToolTip = s.odemeNo.ToString();
                        break;

                    case 4:
                        simpleButton75.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton75.ToolTip = s.odemeNo.ToString();
                        break;

                    case 5:
                        simpleButton76.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton76.ToolTip = s.odemeNo.ToString();
                        break;

                    case 6:
                        simpleButton77.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton77.ToolTip = s.odemeNo.ToString();
                        break;

                    case 7:
                        simpleButton78.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton78.ToolTip = s.odemeNo.ToString();
                        break;

                    case 8:
                        simpleButton79.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton79.ToolTip = s.odemeNo.ToString();
                        break;

                    case 9:
                        simpleButton80.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton80.ToolTip = s.odemeNo.ToString();
                        break;

                    case 10:
                        simpleButton81.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton81.ToolTip = s.odemeNo.ToString();
                        break;

                    case 11:
                        simpleButton82.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton82.ToolTip = s.odemeNo.ToString();
                        break;

                    case 12:
                        simpleButton83.Text = s.odemeIsim + "\r\n(" + s.odemeNo.ToString() + ")";
                        simpleButton83.ToolTip = s.odemeNo.ToString();
                        break;
                }
            }
        }

        private void odemeTipleri()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<BelgeTipi>("belgeTipleri");
            var sepets = coll.Find("{}").ToListAsync().Result;
            foreach (BelgeTipi s in sepets)
            {
                switch (s.belgeTipId)
                {
                    case 0:
                        simpleButton23.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton23.ToolTip = s.belgeTipId.ToString();
                        labelControl12.Text = s.belgeTipAd;
                        break;

                    case 1:
                        simpleButton35.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton35.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 2:
                        simpleButton32.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton32.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 3:
                        simpleButton29.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton29.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 4:
                        simpleButton26.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton26.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 5:
                        simpleButton22.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton22.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 6:
                        simpleButton34.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton34.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 7:
                        simpleButton31.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton31.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 8:
                        simpleButton28.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton28.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 9:
                        simpleButton25.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton25.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 10:
                        simpleButton21.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton21.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 11:
                        simpleButton33.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton33.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 12:
                        simpleButton30.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton30.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 13:
                        simpleButton27.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton27.ToolTip = s.belgeTipId.ToString();
                        break;

                    case 14:
                        simpleButton24.Text = s.belgeTipAd + "\r\n(" + s.belgeTipId.ToString() + ")";
                        simpleButton24.ToolTip = s.belgeTipId.ToString();
                        break;
                }
            }
        }

        private void baglantKontrol()
        {
            var serverAdres = ConfigurationManager.AppSettings["Server"].ToString();
            if (serverAdres != "")
            {
                if (ayar.backOffice == "Genius")
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
                    {
                        PingReply DonenCevap;

                        try
                        {
                            Ping ping = new Ping();
                            DonenCevap = ping.Send(serverAdres);
                        }
                        catch (Exception ex)
                        {
                            timer2.Interval = 60000;
                            labelControl37.Text = "ERR";
                            panel1.BackColor = Color.Red;
                            return;
                        }

                        if (DonenCevap.Status == IPStatus.Success)
                        {
                            if (fisbasla == 0)
                            {
                                labelControl37.Text = "ON";
                                panel1.BackColor = Color.Green;
                                timer2.Interval = 10000;

                                var serverPatch = ConfigurationManager.AppSettings["ServerPatch"].ToString();
                                if (serverAdres != "")
                                {
                                    String last = "0000000";
                                    int sonFis = -1;
                                    var dosya = @"\\" + serverAdres + "\\" + serverPatch + "\\GNETR" + ayar.kasaNumarasi.ToString() + ".log";
                                    if (File.Exists(dosya))
                                    {
                                        try
                                        {
                                            last = File.ReadLines(dosya).Last();
                                        }
                                        catch
                                        {
                                            sonFis = 0;
                                        }
                                    }
                                    else
                                    {
                                        sonFis = 0;
                                    }

                                    if (last != "")
                                    {
                                        if (last.Length > 6)
                                        {
                                            sonFis = int.Parse(last.Substring(0, 6));
                                            int dbSonFis = fonksiyonlar.FisNo();
                                            dbSonFis = dbSonFis - 1;
                                            if (sonFis == dbSonFis)
                                            {
                                            }
                                            else
                                            {
                                                TopMost = false;

                                                var dosya2 = @"D:\\App3\\Log\\GNETR" + ayar.kasaNumarasi.ToString() + ".log";
                                                //if (!System.IO.File.Exists(dosya))
                                                //{
                                                //    System.IO.File.Create(dosya);
                                                //}
                                                System.IO.StreamWriter logNetwork = new System.IO.StreamWriter(dosya, true);

                                                if (System.IO.File.Exists(dosya2))
                                                {
                                                    String last2 = File.ReadLines(dosya2).Last();
                                                    if (last2 != "")
                                                    {
                                                        if (last2.Length > 21)
                                                        {
                                                            if (last2.Substring(19, 2).ToString() == "DF")
                                                            {
                                                                int localSonFis = int.Parse(last2.Substring(0, 6));
                                                                using (StreamReader sr = File.OpenText(dosya2))
                                                                {
                                                                    string s = String.Empty;
                                                                    int fisNo = 0;
                                                                    while ((s = sr.ReadLine()) != null)
                                                                    {
                                                                        fisNo = int.Parse(s.Substring(0, 6));
                                                                        if (fisNo > sonFis)
                                                                        {
                                                                            if (s.ToString().Trim().Length > 50)
                                                                            {
                                                                                logNetwork.WriteLine(s.ToString());
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (localSonFis < sonFis)
                                                                            {
                                                                                logNetwork.WriteLine(s.ToString());
                                                                            }
                                                                        }
                                                                        //do minimal amount of work here
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                logNetwork.Close();
                                            }
                                        }
                                    }
                                }
                            }

                            //System.IO.StreamWriter logNetwork = new System.IO.StreamWriter(dosya, true);
                            //logNetwork.WriteLine(disdosya2);
                            //logNetwork.Close();
                        }
                        else
                        {
                            labelControl37.Text = "OFF";
                            panel1.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        labelControl37.Text = "OFF";
                        panel1.BackColor = Color.Red;
                    }
                }

                if (ayar.backOffice == "Optimist")
                {
                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
                    {
                        string baglanti = "";
                        try
                        {
                            baglanti = service.BaglantiTest();
                        }
                        catch (Exception ex)
                        {
                            baglanti = "ERR";
                        }

                        if (baglanti == "OK")
                        {
                            labelControl37.Text = "ON";
                            panel1.BackColor = Color.Green;
                            timer2.Interval = 10000;

                            var client = new MongoClient();
                            var db = client.GetDatabase("Olivetti");
                            var coll = db.GetCollection<PosBelge>("PosBelge");
                            var satislar = coll.Find("{Aktarim:0}").ToListAsync().Result;
                            foreach (PosBelge s in satislar)
                            {
                                var wsSonuc = service.BelgeAktarAramis(s.ToJson(), int.Parse(ayar.magazaNumarasi));

                                if (wsSonuc.sonucId > 0)
                                {
                                    var update = Builders<PosBelge>.Update
                                    .Set("Aktarim", wsSonuc.sonucId);
                                    coll.UpdateOne("{'_id':ObjectId('" + s.id + "')}", update);
                                }
                            }
                        }
                        else
                        {
                            labelControl37.Text = "OFF";
                            panel1.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        labelControl37.Text = "OFF";
                        panel1.BackColor = Color.Red;
                    }
                }
            }

            if (labelControl37.Text == "ON")
            {
                MesajKontrol();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblTarih.Text = dt.ToLongDateString() + " " + dt.ToLongTimeString();

            if (kasaMesaj != 0)
            {
                int numRows = dataGridView1.RowCount;
                if (numRows == 0)
                {
                    timer1.Enabled = false;
                    timer1.Stop();

                    if (kasaMesaj == 1)
                    {
                        ShowMessage("Lütfen bekleyin", "Ürün listesi alınıyor");
                        ProcessStartInfo procInfo = new ProcessStartInfo();
                        procInfo.UseShellExecute = true;
                        procInfo.CreateNoWindow = true;
                        procInfo.FileName = @"D:\\App3\\import.bat";  //The file in that DIR.
                        procInfo.WorkingDirectory = @""; //The working DIR.
                        var process = new Process();
                        process.StartInfo = procInfo;
                        process.Start();
                        process.WaitForExit();
                        HideMessage();
                        fonksiyonlar.MesajGoster("Güncelleme", "Ürün Listesi Güncellendi", 2);
                        kasaMesaj = 0;
                    }

                    if (kasaMesaj == 2)
                    {
                        ShowMessage("Lütfen bekleyin", "Cari listesi alınıyor");
                        ProcessStartInfo procInfo = new ProcessStartInfo();
                        procInfo.UseShellExecute = true;
                        procInfo.CreateNoWindow = true;
                        procInfo.FileName = @"D:\\App3\\import2.bat";  //The file in that DIR.
                        procInfo.WorkingDirectory = @""; //The working DIR.
                        var process = new Process();
                        process.StartInfo = procInfo;
                        process.Start();
                        process.WaitForExit();
                        HideMessage();
                        fonksiyonlar.MesajGoster("Güncelleme", "Cari Listesi Güncellendi", 2);
                        kasaMesaj = 0;
                    }
                    if (kasaMesaj == 5)
                    {
                        ShowMessage("Lütfen bekleyin", "Promosyon bilgileri alınıyor");
                        var clientB = new MongoClient(ConfigurationManager.ConnectionStrings["mongoDBConnectionString2"].ToString());
                        var dbB = clientB.GetDatabase("backOffice");
                        var client = new MongoClient();
                        var db = client.GetDatabase("Olivetti");
                        var collB = dbB.GetCollection<Promo>("promo");
                        var collP = db.GetCollection<Promo>("promo");
                        var collM = dbB.GetCollection<MagazaMesaj>("magazamesaj");
                        var sepetsB = collB.Find("{}").ToListAsync().Result;
                        var sepetsM = collM.Find("{'magazaKodu':" + int.Parse(ayar.magazaNumarasi) + ",'mesajDurumu':0}").ToListAsync().Result;
                        db.DropCollection("promo");
                        foreach (Promo msj in sepetsB)
                        {
                            collP.InsertOne(msj);
                        }

                        var tm = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        foreach (MagazaMesaj msj2 in sepetsM)
                        {
                            var update = Builders<MagazaMesaj>.Update
                            .Set("mesajDurumu", 1)
                                .Set("cevapTarihi", tm);
                            collM.UpdateOne("{'_id':ObjectId('" + msj2._id + "')}", update);
                        }
                        HideMessage();
                        fonksiyonlar.MesajGoster("Güncelleme", "Promosyon bilgileri Güncellendi", 2);
                        kasaMesaj = 0;
                    }
                    timer1.Enabled = true;
                }
            }
        }

        private void satis_Load(object sender, EventArgs e)
        {
            LogYaz("Satış Ekranı Açıldı");
            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
            {
                this.TopMost = true;
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                this.WindowState = FormWindowState.Maximized;
            }
            DateTime dt = DateTime.Now;
            sonTusZamani = dt.ToLocalTime();
            if (kSablon.Count < 1)
            {
                System.IO.StreamReader S = new System.IO.StreamReader(Application.StartupPath + "/klavye.ini");
                int sira = 0;
                string tus = null;
                string tip = null;
                string deger = null;
                string line;
                klavyeSablon.KlavyeTusu kTus = new klavyeSablon.KlavyeTusu();
                while ((line = S.ReadLine()) != null)
                {
                    if (line.IndexOf("=") > 0)
                    {
                        if (line.IndexOf(",") > 0)
                        {
                            string[] spl = line.Split('=');
                            string[] spl2 = spl[1].Split(',');
                            sira = int.Parse(spl[0]);
                            tus = spl2[0];
                            tip = spl2[1];
                            deger = spl2[2];
                            kTus = new klavyeSablon.KlavyeTusu();
                            kTus.sira = sira;
                            kTus.kod = tus;
                            kTus.tip = tip;
                            kTus.deger = deger;
                            kSablon.Add(kTus);
                        }
                    }
                }
            }

            if (hUrun.Count < 1)
            {
                klavyeSablon.ListeUrun lUrun = new klavyeSablon.ListeUrun();
                var client = new MongoClient();
                var db = client.GetDatabase("Olivetti");
                var coll = db.GetCollection<hizliUrun>("hizliUrun");
                var sepets = coll.Find("{}").Sort("{_id:1}").Limit(30).ToListAsync().Result;
                int sira = 0;
                foreach (hizliUrun s in sepets)
                {
                    sira = sira + 1;
                    lUrun = new klavyeSablon.ListeUrun();
                    lUrun.sira = sira;
                    lUrun.urunBarkod = s.urunBarkod;
                    hUrun.Add(lUrun);
                }
            }

            labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;
            groupControl6.Visible = false;
            groupControl7.Visible = false;
            groupControl8.Visible = false;
            groupControl10.Visible = false;

            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
            {
                baslamaSonuc = FPUFuctions.Instance.CashierLogin(int.Parse(kasiyerTanm.kasiyerkodu), kasiyerTanm.kasiyerSifre).ErrorCode;
                LogYaz("CashierLogin Sonucu:" + baslamaSonuc.ToString());
                if (baslamaSonuc != 0 && baslamaSonuc != 91)
                {
                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", baslamaSonuc + errD.errorExplanation.ToString(), 2);
                    return;
                }
            }

            odemeTipleri();

            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
            {
                FPUFuctions.Instance.WriteToDisplay("HOSGELDINIZ", "PANAROMA BILISIM");
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                this.TopMost = false;
            }

            if (ConfigurationManager.AppSettings["AutoOpenFis"].ToString() == "1")
            {
                baslamaSonuc = FPUFuctions.Instance.OpenFiscalReceipt(int.Parse(kasiyerTanm.kasiyerkodu), -1, kasiyerTanm.kasiyerSifre, "", "").ErrorCode;
                LogYaz("OpenFiscalReceipt Sonucu:" + baslamaSonuc.ToString());
                if (baslamaSonuc != 0 && baslamaSonuc != 91)
                {
                    HideMessage();
                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                    return;
                }
                else
                {
                    fisbasla = 1;
                }
            }

            textEdit1.Focus();
            textEdit1.Select();
            labelControl9.Text = ayar.kasaNumarasi;
            labelControl8.Text = ayar.magazaNumarasi;
            labelControl10.Text = kasiyerTanm.kasiyerAdi;
            sepetOku();

            int numRows = dataGridView1.RowCount;
            if (numRows > 0)
            {
                onay frm = new onay();

                if (fonksiyonlar.OnayGoster("Açık Fiş Var", "Tamamlanmamış fiş var, ne yapmak istersiniz?", 0, "Fişi İptal Et", "Fişe Devam Et") != "OK")
                {
                    fisbasla = 1;
                    this.belgeiptal = 1;
                    satisTamamla();
                }
                else
                {
                    baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                    LogYaz("CancelFiscalReceipt Sonucu:" + baslamaSonuc.ToString());
                    if (baslamaSonuc != 0)
                    {
                        baslamaSonuc = FPUFuctions.Instance.SpecilCancelFiscalReceipt().ErrorCode;
                        LogYaz("SpecilCancelFiscalReceipt Sonucu:" + baslamaSonuc.ToString());
                    }
                    fisbasla = 0;
                }
            }
        }

        private void ButtonClick(string s, Object sender)
        {
            if (s.IndexOf("PLU") > 0)
            {
                string tusNo = s.Replace("<PLU", "");
                tusNo = tusNo.Replace(">", "");

                dynamic urunKarsilik2 = hUrunBul(int.Parse(tusNo));

                if (!string.IsNullOrEmpty(urunKarsilik2.urunBarkod))
                {
                    if (textEdit1.Properties.NullValuePrompt == "<IPTAL>")
                    {
                        urunIptal(urunKarsilik2.urunBarkod);

                        textEdit1.Properties.NullValuePrompt = "Barkod Okutunuz";
                    }
                    else
                    {
                        SepeteEkle(urunKarsilik2.urunBarkod);
                    }
                }
                return;
            }

            switch (s)
            {
                case "<TEKRAR>":
                    simpleButton51.PerformClick();
                    break;

                case "<FATURA>":
                    simpleButton35.PerformClick();
                    break;

                case "<EFATURA>":
                    simpleButton32.PerformClick();
                    break;

                case "<EARSIV>":
                    simpleButton29.PerformClick();
                    break;

                case "<IADE>":
                    simpleButton22.PerformClick();
                    break;

                case "<KAPAT>":
                    simpleButton40.PerformClick();
                    break;

                case "<USEC>":
                    simpleButton18.PerformClick();
                    break;

                case "<IPTAL>":
                    simpleButton3.PerformClick();
                    break;

                case "<KAC>":
                    simpleButton42.PerformClick();
                    break;

                case "<FUNC>":
                    simpleButton46.PerformClick();
                    break;

                case "<INDIRIM2>":
                    simpleButton54.PerformClick();
                    textEdit9.Select();

                    break;

                case "<INDIRIM>":
                    simpleButton54.PerformClick();
                    textEdit10.Select();
                    break;

                case "<FIPTAL>":
                    simpleButton41.PerformClick();
                    break;

                case "<KRD5>":
                    simpleButton39.PerformClick();
                    break;

                case "<XRAP>":
                    simpleButton37.PerformClick();
                    break;

                case "<ZRAP>":
                    simpleButton49.PerformClick();
                    break;

                case "<MMENU>":
                    simpleButton17.PerformClick();
                    break;

                case "<FIYATGOR>":
                    simpleButton19.PerformClick();
                    break;

                case "<ARATOPLAM>":
                    simpleButton16.PerformClick();
                    break;

                case "<NAKIT>":

                    if (groupControl8.Visible == true)
                    {
                        var button = (DevExpress.XtraEditors.TextEdit)sender;
                        if (button.Name == "textEdit5")
                        {
                            simpleButton52.PerformClick();
                        }
                        if (button.Name == "textEdit7")
                        {
                            simpleButton52.PerformClick();
                        }
                        if (button.Name == "textEdit6")
                        {
                            if (labelControl16.Text == "Kalan")
                            {
                                textEdit5.Text = labelControl17.Text;
                                textEdit5.Focus();
                            }
                            else
                            {
                                simpleButton52.PerformClick();
                            }
                        }
                    }
                    break;

                case "<KRD6>":  // YEMEK ÇEKİ TUŞ
                    if (groupControl8.Visible == true)
                    {
                        simpleButton59.PerformClick();
                        //var button = (DevExpress.XtraEditors.TextEdit)sender;
                        //if (button.Name == "textEdit5")
                        //{
                        //    textEdit7.Text = textEdit5.Text;
                        //    textEdit5.Text = "0";
                        //    SendKeys.Send("{ENTER}");
                        //    SendKeys.Send("{ENTER}");
                        //}
                        //if (button.Name == "textEdit7")
                        //{
                        //    if (labelControl16.Text == "Kalan")
                        //    {
                        //        textEdit5.Text = labelControl17.Text;
                        //        textEdit5.Focus();
                        //        fcs = 2;
                        //    }
                        //    else
                        //    {
                        //        simpleButton52.PerformClick();
                        //    }

                        //}
                    }
                    //else
                    //{
                    //    simpleButton21.PerformClick();
                    //}
                    break;

                case "<KRD1>":
                    if (groupControl8.Visible == true)
                    {
                        var button = (DevExpress.XtraEditors.TextEdit)sender;
                        if (button.Name == "textEdit5")
                        {
                            textEdit6.Text = textEdit5.Text;
                            textEdit5.Text = "0";
                            SendKeys.Send("{ENTER}");
                            simpleButton58.PerformClick();
                        }
                        if (button.Name == "textEdit6")
                        {
                            if (labelControl16.Text == "Kalan")
                            {
                                textEdit5.Text = labelControl17.Text;
                                textEdit5.Focus();
                                fcs = 2;
                            }
                            else
                            {
                                simpleButton52.PerformClick();
                            }
                        }
                    }
                    break;

                default:

                    break;
            }
        }

        private void KlavyePress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            System.DateTime tarih = default(System.DateTime);
            tarih = System.DateTime.Now;
            TimeSpan fark = default(TimeSpan);
            fark = tarih - sonTusZamani;
            double gecenSure = 0;
            gecenSure = fark.TotalMilliseconds;
            sonTusZamani = tarih;
            string sonText = null;
            if (gecenSure > ayar.msUstLimit)
            {
                TextBox2.Text = e.KeyChar.ToString();
            }
            else if (gecenSure < ayar.msAltLimit)
            {
            }
            else
            {
                sonText = TextBox2.Text;
                TextBox2.Text = TextBox2.Text + e.KeyChar.ToString();
            }

            dynamic klavyeKarsilik = KlavyeOku(TextBox2.Text);

            if (!string.IsNullOrEmpty(klavyeKarsilik.deger))
            {
                textEdit1.Text = textEdit1.Text.Replace(sonText, klavyeKarsilik.deger);
                e.Handled = true;
                if (klavyeKarsilik.tip == "C")
                {
                    ButtonClick(klavyeKarsilik.deger, sender);
                    textEdit1.Text = "";
                }
            }
        }

        private void textEdit1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            KlavyePress(sender, e);
        }

        public klavyeSablon.KlavyeTusu KlavyeOku(string tus)
        {
            klavyeSablon.KlavyeTusu sonuc = new klavyeSablon.KlavyeTusu();
            sonuc.deger = "";
            sonuc.kod = "";
            sonuc.sira = 0;
            sonuc.tip = "";
            dynamic sonuc2 = kSablon.Find(p => p.kod == tus);
            if (sonuc2 == null)
            {
                return sonuc;
            }
            else
            {
                return sonuc2;
            }
        }

        public klavyeSablon.ListeUrun hUrunBul(int tus)
        {
            klavyeSablon.ListeUrun sonuc = new klavyeSablon.ListeUrun();
            sonuc.urunBarkod = "";
            dynamic sonuc2 = hUrun.Find(p => p.sira == tus);
            if (sonuc2 == null)
            {
                return sonuc;
            }
            else
            {
                return sonuc2;
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                textEdit1.Properties.NullValuePrompt = "<IPTAL>";
                simpleButton3.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (groupControl6.Visible == true)
                {
                    switch (textEdit1.Text)
                    {
                        case "0":
                            simpleButton23.PerformClick();
                            break;

                        case "1":
                            simpleButton35.PerformClick();
                            break;

                        case "2":
                            simpleButton32.PerformClick();
                            break;

                        case "3":
                            simpleButton29.PerformClick();
                            break;

                        case "4":
                            simpleButton26.PerformClick();
                            break;

                        case "5":
                            simpleButton22.PerformClick();
                            break;

                        case "6":
                            simpleButton34.PerformClick();
                            break;

                        case "7":
                            simpleButton31.PerformClick();
                            break;

                        case "8":
                            simpleButton28.PerformClick();
                            break;

                        case "9":
                            simpleButton25.PerformClick();
                            break;

                        case "10":
                            simpleButton21.PerformClick();
                            break;

                        case "11":
                            simpleButton33.PerformClick();
                            break;

                        case "12":
                            simpleButton30.PerformClick();
                            break;

                        case "13":
                            simpleButton27.PerformClick();
                            break;

                        case "14":
                            simpleButton24.PerformClick();
                            break;
                    }
                    textEdit1.Text = "";
                    return;
                }

                if (textEdit1.Text == "05366279695")
                {
                    this.Close();
                    return;
                }

                if (textEdit1.Text == "05530892222")
                {
                    textEdit1.Text = "";
                    this.TopMost = false;
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.CreateNoWindow = true;
                    procInfo.FileName = @"D:\App1\AnyDesk\AnyDesk.exe";  //The file in that DIR.
                    procInfo.WorkingDirectory = @""; //The working DIR.
                    var process = new Process();
                    process.StartInfo = procInfo;
                    process.Start();
                    return;
                }

                if (textEdit1.Properties.NullValuePrompt == "<IPTAL>")
                {
                    urunIptal(textEdit1.Text);

                    textEdit1.Properties.NullValuePrompt = "Barkod Okutunuz";
                }
                else
                {
                    SepeteEkle(textEdit1.Text);
                }

                textEdit1.Text = "";
            }
        }

        private void textEdit21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                {
                    groupControl8.Visible = false;
                    simpleButton16.Enabled = true;
                    textEdit1.Select();
                    fcs = 0;
                    return;
                }
                else
                {
                    groupControl13.Visible = false;
                    return;
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (groupControl13.Visible == true)
                {
                    switch (textEdit21.Text)
                    {
                        case "1":
                            simpleButton72.PerformClick();
                            break;

                        case "2":
                            simpleButton73.PerformClick();
                            break;

                        case "3":
                            simpleButton74.PerformClick();
                            break;

                        case "4":
                            simpleButton75.PerformClick();
                            break;

                        case "5":
                            simpleButton76.PerformClick();
                            break;

                        case "6":
                            simpleButton77.PerformClick();
                            break;

                        case "7":
                            simpleButton78.PerformClick();
                            break;

                        case "8":
                            simpleButton79.PerformClick();
                            break;

                        case "9":
                            simpleButton80.PerformClick();
                            break;

                        case "10":
                            simpleButton81.PerformClick();
                            break;

                        case "11":
                            simpleButton82.PerformClick();
                            break;

                        case "12":
                            simpleButton83.PerformClick();
                            break;
                    }
                    textEdit21.Text = "";
                    return;
                }

                textEdit21.Text = "";
            }
        }

        private void textEdit22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                groupControl12.Visible = false;
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (groupControl12.Visible == true)
                {
                    switch (textEdit22.Text)
                    {
                        case "1":
                            simpleButton60.PerformClick();
                            break;

                        case "2":
                            simpleButton61.PerformClick();
                            break;

                        case "3":
                            simpleButton62.PerformClick();
                            break;

                        case "4":
                            simpleButton63.PerformClick();
                            break;

                        case "5":
                            simpleButton64.PerformClick();
                            break;

                        case "6":
                            simpleButton65.PerformClick();
                            break;

                        case "7":
                            simpleButton66.PerformClick();
                            break;

                        case "8":
                            simpleButton67.PerformClick();
                            break;

                        case "9":
                            simpleButton68.PerformClick();
                            break;

                        case "10":
                            simpleButton69.PerformClick();
                            break;

                        case "11":
                            simpleButton70.PerformClick();
                            break;

                        case "12":
                            simpleButton71.PerformClick();
                            break;
                    }
                    textEdit22.Text = "";
                    return;
                }

                textEdit22.Text = "";
            }
        }

        private void textEdit2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textEdit1.Select();
                fcs = 0;
            }
        }

        private void textEdit13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fcs = 9;
                textEdit14.Focus();
            }
        }

        private void textEdit15_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.belge == 2 || this.belge == 6)
                {
                    labelControl23.Visible = false;
                    simpleButton85.Visible = false;
                    var client = new MongoClient();
                    var db = client.GetDatabase("Olivetti");
                    var coll = db.GetCollection<eFatura>("eFatura");
                    var sepets = coll.Find("{'Identifier':'" + textEdit15.Text.ToString() + "'}").Limit(1).ToListAsync().Result;
                    int kayit = 0;
                    foreach (eFatura s2 in sepets)
                    {
                        kayit = 1;
                        labelControl24.Text = s2.Title;
                        this.musteriUnvan = s2.Title;
                        this.musteriVNo = s2.Identifier;
                        simpleButton88.Visible = true;
                        labelControl24.Visible = true;
                        labelControl36.Text = this.musteriUnvan;
                    }
                    if (kayit == 0)
                    {
                        fonksiyonlar.HataMesaji("Müşteri Bulunamadı", textEdit15.Text + " vergi numaralı mükellef bulunamadı", 2);
                        if (ayar.eArsivAktif == 1)
                        {
                            groupControl16.Visible = true;
                            labelControl26.Visible = true;
                            labelControl26.Text = textEdit15.Text + " vergi numaralı mükellef bulunamadı. E-Arşiv olarak devam edilsin mi?";
                            memoEdit1.Text = textEdit15.Text + " vergi numaralı mükellef bulunamadı. E-Arşiv olarak devam edilsin mi?";
                        }
                        if (ayar.eArsivAktif == 0)
                        {
                            if (ayar.eFaturaAktif == 0)
                            {
                                groupControl16.Visible = true;
                                labelControl26.Visible = true;
                                memoEdit1.Text = "E-Fatura Mükellefi değilsiniz. Normal fatura olarak devam edilsin mi?";
                            }
                        }
                        simpleButton88.Visible = false;
                        labelControl24.Visible = false;
                    }
                }
            }
        }

        private void textEdit14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.belge == 1)
                {
                    if (textEdit14.Text != "")
                    {
                        labelControl23.Visible = false;
                        simpleButton85.Visible = false;
                        var client = new MongoClient();
                        var db = client.GetDatabase("Olivetti");
                        var coll2 = db.GetCollection<musteri>("musteri");
                        var coll = db.GetCollection<musteri>("musteri");
                        var sepets = coll.Find("{'vergiNumarasi':'" + textEdit14.Text.ToString() + "'}").Limit(1).ToListAsync().Result;
                        int kayit = 0;
                        foreach (musteri s2 in sepets)
                        {
                            kayit = 1;
                            labelControl23.Text = s2.unVan;
                            simpleButton85.Visible = true;
                            labelControl23.Visible = true;
                        }
                        if (kayit == 0)
                        {
                            fonksiyonlar.HataMesaji("Müşteri Bulunamadı", textEdit14.Text + " vergi numaralı müşteri bulunamadı", 2);
                            simpleButton85.Visible = false;
                            labelControl23.Visible = false;
                        }
                    }
                    else
                    {
                        simpleButton86.PerformClick();
                    }
                }
            }
        }

        private void textEdit5_KeyPress(object sender, KeyPressEventArgs e)
        {
            KlavyePress(sender, e);
        }

        private void textEdit7_KeyPress(object sender, KeyPressEventArgs e)
        {
            KlavyePress(sender, e);
        }

        private void textEdit6_KeyPress(object sender, KeyPressEventArgs e)
        {
            KlavyePress(sender, e);
        }

        private void textEdit5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textEdit6.Focus();
                fcs = 3;
            }
            if (e.KeyCode == Keys.Escape)
            {
                simpleButton53.PerformClick();
            }
        }

        private void textEdit6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textEdit7.Focus();
                fcs = 4;
            }
            if (e.KeyCode == Keys.Escape)
            {
                simpleButton53.PerformClick();
            }
        }

        private void textEdit7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton52.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                simpleButton53.PerformClick();
            }
        }

        public void sepetOku()
        {
            int adet = 0;
            dataGridView1.Rows.Clear();
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{}").Sort("{_id:1}").ToListAsync().Result;
            double toplam = 0;
            foreach (sepet s in sepets)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                double fiyat = s.fiyatS1;
                double miktar = s.miktar;
                double satirToplam = s.toplam - s.sindirim;
                string miktarText = s.miktar.ToString();
                if (s.barkod.Length > 2)
                {
                    if (s.barkod.Substring(0, 2) == "28" || s.barkod.Substring(0, 2) == "29")
                    {
                        miktarText = s.miktar.ToString("n3");
                    }
                }

                dataGridView1.Rows.Add(s.barkod, s.stkAd, miktarText, s.fiyatS1.ToString("n2"), satirToplam.ToString("n2"), s.iptal);
                adet = adet + 1;
                labelControl41.Text = "Satır sayısı:" + adet.ToString();

                if (s.iptal == 0)
                {
                    toplam = toplam + satirToplam;
                }
                if (s.iptal == 1)
                {
                    toplam = toplam - satirToplam;
                }

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Index;
            }
            labelControl5.Text = "Tutar = " + toplam.ToString("n2");
            textEdit3.Text = toplam.ToString("n2");

            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                string col5 = Convert.ToString(rw.Cells["iptal"].Value);

                if (col5 == "1")
                {
                    //rw.Cells[0].Style.BackColor = Color.Red;
                    rw.DefaultCellStyle.BackColor = Color.Tomato;
                }
                else
                {
                }
            }

            textEdit1.Select();
            textEdit1.Focus();
        }

        public void sepetOku3()
        {
            int adet = 0;
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("promosepet");
            var sepets = coll.Find("{}").Sort("{_id:1}").ToListAsync().Result;
            double toplam = 0;
            double indirimToplamlari = 0;
            foreach (sepet s in sepets)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                double fiyat = s.fiyatS1;
                double miktar = s.miktar;
                double satirToplam = s.toplam - s.sindirim;
                indirimToplamlari = indirimToplamlari + s.sindirim;
                satirToplam = satirToplam - s.indirim;
                indirimToplamlari = indirimToplamlari + s.indirim;
                string miktarText = s.miktar.ToString();
                if (s.barkod.Length > 2)
                {
                    if (s.barkod.Substring(0, 2) == "28" || s.barkod.Substring(0, 2) == "29")
                    {
                        miktarText = s.miktar.ToString("n3");
                    }
                }

                if (s.iptal == 0)
                {
                    toplam = toplam + satirToplam;
                }
                if (s.iptal == 1)
                {
                    toplam = toplam - satirToplam;
                }
            }

            var coll2 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");
            var sepets2 = coll2.Find("{}").Sort("{_id:1}").ToListAsync().Result;
            double toplam2 = sepets2.Sum(p => p.promosyonTutar);

            if (indirimToplamlari > toplam2)
            {
                double fark = indirimToplamlari - toplam2;
                //MessageBox.Show(fark.ToString());
                var sepets3 = coll.Find("{$and: [{iptal:0},{indirim:{$gt:" + fark.ToString().Replace(',', '.') + "}}]}").Limit(1).ToListAsync().Result;

                foreach (sepet s3 in sepets3)
                {
                    var update = Builders<sepet>.Update.Set("indirim", s3.indirim - fark);
                    coll.UpdateOne("{stkID:'" + s3.stkID + "'}", update);
                }
                toplam = toplam + fark;
            }

            labelControl5.Text = "Tutar = " + toplam.ToString("n2");
            textEdit3.Text = toplam.ToString("n2");

            textEdit1.Select();
            textEdit1.Focus();
        }

        public void sepetOku2()
        {
            int adet = 0;
            fisbasla = 0;
            dataGridView1.Rows.Clear();
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{}").Sort("{_id:1}").ToListAsync().Result;
            double toplam = 0;
            foreach (sepet s in sepets)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                double fiyat = s.fiyatS1;
                double miktar = s.miktar;
                double satirToplam = s.toplam - s.sindirim;
                string miktarText = s.miktar.ToString();
                if (s.barkod.Length > 2)
                {
                    if (s.barkod.Substring(0, 2) == "28" || s.barkod.Substring(0, 2) == "29")
                    {
                        miktarText = s.miktar.ToString("n3");
                    }
                }

                dataGridView1.Rows.Add(s.barkod, s.stkAd, miktarText, s.fiyatS1.ToString("n2"), satirToplam.ToString("n2"), s.iptal);

                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
                {
                    if (fisbasla == 0)
                    {
                        baslamaSonuc = FPUFuctions.Instance.OpenFiscalReceipt(int.Parse(kasiyerTanm.kasiyerkodu), -1, kasiyerTanm.kasiyerSifre, "", "").ErrorCode;
                        if (baslamaSonuc != 0 && baslamaSonuc != 91)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                            return;
                        }
                        fisbasla = 1;
                    }

                    if (ConfigurationManager.AppSettings["BarkodPrint"].ToString() == "1")
                    {
                        baslamaSonuc = FPUFuctions.Instance.PrintFreeFiscalText("Barkod No: " + s.barkod.ToString()).ErrorCode;
                        if (baslamaSonuc != 0)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                            return;
                        }
                    }

                    decimal miktar2 = Convert.ToDecimal(s.miktar);
                    string flag = "";
                    if (s.iptal == 1)
                    {
                        flag = "-";
                    }
                    else
                    {
                        flag = "";
                    }
                    baslamaSonuc = FPUFuctions.Instance.Registeringsale(s.stkAd, flag, s.barkod, "", 1, fonksiyonlar.kdvBul(s.kdv, kdvTanimlari), 1, Convert.ToDecimal(s.fiyatS1), 0, miktar2).ErrorCode;
                    if (baslamaSonuc != 0)
                    {
                        HideMessage();
                        var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                        fonksiyonlar.HataMesaji("2-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                        return;
                    }
                }

                adet = adet + 1;
                labelControl41.Text = "Satır sayısı:" + adet.ToString();

                if (s.iptal == 0)
                {
                    toplam = toplam + satirToplam;
                }
                if (s.iptal == 1)
                {
                    toplam = toplam - satirToplam;
                }

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Index;
            }

            labelControl5.Text = "Tutar = " + toplam.ToString("n2");
            textEdit3.Text = toplam.ToString("n2");

            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                string col5 = Convert.ToString(rw.Cells["iptal"].Value);

                if (col5 == "1")
                {
                    //rw.Cells[0].Style.BackColor = Color.Red;
                    rw.DefaultCellStyle.BackColor = Color.Tomato;
                }
                else
                {
                }
            }

            textEdit1.Select();
            textEdit1.Focus();

            if (adet > 0)
            {
                simpleButton16.Enabled = true;
                simpleButton16.PerformClick();
            }
        }

        public void SepeteEkle(string barkod)
        {
            if (labelControl41.Text == "Satır sayısı:95")
            {
                fonksiyonlar.HataMesaji("Ürün Eklenemedi", "Fiş satır sayısı limiti doldu", 3);
                return;
            }

            string barkod2 = barkod;
            int numRows = dataGridView1.RowCount;

            if (barkod.Length > 2)
            {
                if (barkod.Substring(0, 2) == "26" || barkod.Substring(0, 2) == "28" || barkod.Substring(0, 2) == "29")
                {
                    if (barkod.Length > 7)
                    {
                        barkod = barkod.Substring(0, 7);
                    }
                }
                else
                {
                }
            }
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<stk>("stk");
            var stoks = coll.Find("{'Barkod.barkod':'" + barkod + "'}").Limit(1).ToListAsync().Result;
            var count = stoks.Count();
            if (count == 0)
            {
                fonksiyonlar.HataMesaji("Ürün Bulunamadı", barkod + " barkodlu ürün bulunamadı", 1);
                textEdit1.Text = "";
                textEdit2.Text = "1";
                return;
            }

            if (numRows == 0)
            {
                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
                {
                    if (ConfigurationManager.AppSettings["YemekCeki"].ToString() == "1")
                    {
                        if (belge == 0)
                        {
                            if (fonksiyonlar.OnayGoster("Yemek Çeki Seçimi", "Ödeme yemek çeki ile mi yapılacak?", 0, "Hayır", "Evet") == "OK")
                            {
                                belge = 10;
                                labelControl12.Text = "Yemek Çeki";
                                labelControl36.Text = "";
                            }
                            else
                            {
                                belge = 0;
                                labelControl12.Text = "Fiş";
                                labelControl36.Text = "";
                            }
                        }
                    }

                    //buradayuk

                    int fisTipi = 1;

                    switch (belge)
                    {
                        case 1:
                            fisTipi = 1;
                            break;

                        case 2:
                            fisTipi = 2;
                            break;

                        case 3:
                            fisTipi = 3;
                            break;

                        case 4:
                            fisTipi = 1;
                            break;

                        case 6:
                            fisTipi = 2;
                            break;

                        case 10:
                            fisTipi = 5;
                            break;

                        default:
                            fisTipi = -1;
                            break;
                    }

                    if (fisbasla == 0)
                    {
                        if (belge != 5)
                        {
                            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                            {
                                baslamaSonuc = FPUFuctions.Instance.OpenFiscalReceipt(int.Parse(kasiyerTanm.kasiyerkodu), fisTipi, kasiyerTanm.kasiyerSifre, this.musteriVNo, evrakNo, 1).ErrorCode;
                                LogYaz("OpenFiscalReceipt (" + fisTipi.ToString() + ") Sonucu:" + baslamaSonuc.ToString());
                                if (baslamaSonuc != 0 && baslamaSonuc != 91)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }

                            fisbasla = 1;
                        }
                    }
                }
            }

            foreach (stk s in stoks)
            {
                if (s.Terazi == 1)
                {
                    if (barkod2.Length > 11)
                    {
                        double adetTerazi = double.Parse(barkod2.Substring(7, 5));
                        adetTerazi = adetTerazi / 1000;
                        textEdit2.Text = adetTerazi.ToString();
                    }
                }
                if (s.Terazi == 2)
                {
                    if (barkod2.Length > 11)
                    {
                        double adetTerazi = double.Parse(barkod2.Substring(7, 5));
                        textEdit2.Text = adetTerazi.ToString();
                    }
                }

                double adet = double.Parse(textEdit2.Text);
                decimal adet2 = decimal.Parse(textEdit2.Text);
                double fiyat = s.fiyatS1;

                //iadefiyat

                if (belge == 5)
                {
                    if (ConfigurationManager.AppSettings["IadeFiyat"].ToString() == "1")
                    {
                        iade frm = new iade(s.stkAdKisa, s.fiyatS1);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            fiyat = frm.stkFiyati;
                        }
                    }
                }

                double toplam = fiyat * adet;
                toplam = double.Parse(toplam.ToString("n2"));
                double sepetToplam = double.Parse(textEdit3.Text);
                sepetToplam = sepetToplam + toplam;

                if (belge != 1 && belge != 2 && belge != 3)
                {
                    if (toplam > ayar.fisLimit)
                    {
                        fonksiyonlar.HataMesaji("Ürün Eklenemedi", "Ürün tutarı mali fiş limitinin üzerinde", 2);
                        return;
                    }

                    if (sepetToplam > ayar.fisLimit)
                    {
                        fonksiyonlar.HataMesaji("Ürün Eklenemedi", "Sepet tutarı mali fiş limitinin üzerine çıkamaz", 2);
                        return;
                    }
                }

                int kdvYuzde;
                int kdvID;
                if (belge == 4 || belge == 6)
                {
                    kdvID = fonksiyonlar.kdvBul(int.Parse(s.KdvA), kdvTanimlari);
                    kdvYuzde = fonksiyonlar.kdvYuzdeBul(kdvID);
                }
                else
                {
                    kdvID = fonksiyonlar.kdvBul(int.Parse(s.KdvS), kdvTanimlari);
                    kdvYuzde = fonksiyonlar.kdvYuzdeBul(kdvID);
                }

                double kdvDeger2 = 100 + kdvYuzde;
                double kdvDeger = kdvDeger2 / 100;
                double kdvTutari2 = toplam / kdvDeger;
                double kdvTutari3 = toplam - kdvTutari2;
                kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));
                double kdvsiztoplam = toplam - kdvTutari3;
                kdvsiztoplam = double.Parse(kdvsiztoplam.ToString("n2"));

                var entity = new sepet { stkID = s.id, stkAd = s.stkAdKisa, miktar = adet, fiyatS1 = fiyat, barkod = barkod, iptal = 0, kdv = int.Parse(s.KdvS), satiriptal = 0, kdvYuzdesi = kdvYuzde, kdvTutari = kdvTutari3, stkBrm = fonksiyonlar.birimDegerBul(s.stkBrm), toplam = toplam, kdvsizToplam = kdvsiztoplam, stkKod = s.stkKod, kod1 = s.kod1, kod2 = s.kod2, kod3 = s.kod3, kod4 = s.kod4, kod5 = s.kod5 };

                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                {
                    FPUFuctions.Instance.WriteToDisplay(fonksiyonlar.donustur(s.stkAdKisa), adet + "X" + s.fiyatS1.ToString("n2") + "=" + toplam.ToString("n2") + "TL");
                }

                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
                {
                    if (belge == 10)
                    {
                        if (kdvYuzde == 1 || kdvYuzde == 8)
                        {
                            if (ConfigurationManager.AppSettings["BarkodPrint"].ToString() == "1")
                            {
                                baslamaSonuc = FPUFuctions.Instance.PrintFreeFiscalText("Barkod No: " + barkod.ToString()).ErrorCode;
                                if (baslamaSonuc != 0)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }
                            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                            {
                                baslamaSonuc = FPUFuctions.Instance.Registeringsale(s.stkAd, "", barkod, "", 1, kdvID, 1, Convert.ToDecimal(s.fiyatS1), 0, adet2).ErrorCode;
                                LogYaz("Registeringsale (" + barkod.ToString() + "/" + Convert.ToDecimal(s.fiyatS1).ToString() + "/" + adet2.ToString() + ") Sonucu:" + baslamaSonuc.ToString());
                                if (baslamaSonuc != 0)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (belge != 5)
                        {
                            if (ConfigurationManager.AppSettings["BarkodPrint"].ToString() == "1")
                            {
                                baslamaSonuc = FPUFuctions.Instance.PrintFreeFiscalText("Barkod No: " + barkod.ToString()).ErrorCode;
                                if (baslamaSonuc != 0)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }
                            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                            {
                                baslamaSonuc = FPUFuctions.Instance.Registeringsale(s.stkAd, "", barkod, "", 1, kdvID, 1, Convert.ToDecimal(s.fiyatS1), 0, adet2).ErrorCode;
                                LogYaz("Registeringsale (" + barkod.ToString() + "/" + Convert.ToDecimal(s.fiyatS1).ToString() + "/" + adet2.ToString() + ") Sonucu:" + baslamaSonuc.ToString());
                                if (baslamaSonuc != 0)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }
                        }
                    }
                }

                sepetEkle(entity, 0);
            }

            textEdit2.EditValue = 1;
            sepetOku();
        }

        private void sepetEkle(sepet s, int iptal)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll2 = db.GetCollection<sepet>("sepet");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{stkID:'" + s.stkID + "',iptal:" + iptal + "}").ToListAsync().Result;
            double kayit = 0;
            foreach (sepet s2 in sepets)
            {
                //kayit = s.miktar + s2.miktar;
            }
            if (kayit == 0)
            {
                coll2.InsertOneAsync(s);
            }
            else
            {
                var update = Builders<sepet>.Update.Set("miktar", kayit);
                coll2.UpdateOne("{stkID:'" + s.stkID + "',iptal:" + iptal + "}", update);
            }
            if (iptal == 1)
            {
                satirİptal(s);
            }
        }

        private void satirİptal(sepet s)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll2 = db.GetCollection<sepet>("sepet");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{stkID:'" + s.stkID + "',satiriptal:0}").ToListAsync().Result;
            double kayit = 0;
            foreach (sepet s2 in sepets)
            {
                kayit = s.miktar + s2.miktar;
            }
            if (kayit == 0)
            {
                coll2.InsertOneAsync(s);
            }
            else
            {
                var update = Builders<sepet>.Update.Set("satiriptal", 1);
                coll2.UpdateOne("{stkID:'" + s.stkID + "',satiriptal:0}", update);
            }
        }

        private double sepetKontrol(string urunBarkod, int iptal, double sepetmiktar)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{'barkod':'" + urunBarkod + "','iptal':" + iptal + ",'satiriptal':0,'miktar':" + sepetmiktar.ToString().Replace(',', '.') + "}").Limit(1).ToListAsync().Result;
            double adet = 0;
            foreach (sepet s in sepets)
            {
                adet = 1;
            }
            return adet;
        }

        private void urunIptal(string urunBarkod)
        {
            double adet = double.Parse(textEdit2.Text);

            string barkod2 = urunBarkod;
            if (urunBarkod.Length > 2)
            {
                if (urunBarkod.Substring(0, 2) == "26" || urunBarkod.Substring(0, 2) == "28" || urunBarkod.Substring(0, 2) == "29")
                {
                    if (urunBarkod.Length > 7)
                    {
                        urunBarkod = urunBarkod.Substring(0, 7);
                    }
                    if (barkod2.Length > 11)
                    {
                        double adetTerazi = double.Parse(barkod2.Substring(7, 5));
                        if (urunBarkod.Substring(0, 2) != "26")
                        {
                            adetTerazi = adetTerazi / 1000;
                        }

                        adet = adetTerazi;
                    }
                }
                else
                {
                }
            }
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<stk>("stk");
            var stoks = coll.Find("{'Barkod.barkod':'" + urunBarkod + "'}").Limit(1).ToListAsync().Result;
            var count = stoks.Count();
            if (count == 0)
            {
                fonksiyonlar.HataMesaji("Ürün Bulunamadı", urunBarkod + " barkodlu ürün bulunamadı", 2);
            }
            foreach (stk s in stoks)
            {
                if (sepetKontrol(urunBarkod, 0, adet) != 0)
                {
                    decimal adet2 = decimal.Parse(adet.ToString());
                    double fiyat = s.fiyatS1;
                    double toplam = fiyat * adet;

                    int kdvYuzde;
                    if (belge == 4 || belge == 6)
                    {
                        kdvYuzde = fonksiyonlar.kdvYuzdeBul(fonksiyonlar.kdvBul(int.Parse(s.KdvA), kdvTanimlari));
                    }
                    else
                    {
                        kdvYuzde = fonksiyonlar.kdvYuzdeBul(fonksiyonlar.kdvBul(int.Parse(s.KdvS), kdvTanimlari));
                    }
                    double kdvDeger2 = 100 + kdvYuzde;
                    double kdvDeger = kdvDeger2 / 100;
                    double kdvTutari2 = toplam / kdvDeger;
                    double kdvTutari3 = toplam - kdvTutari2;
                    double kdvsiztoplam = toplam - kdvTutari3;

                    var entity = new sepet { stkID = s.id, stkAd = s.stkAdKisa, miktar = adet, fiyatS1 = fiyat, barkod = urunBarkod, iptal = 1, kdv = int.Parse(s.KdvS), satiriptal = 0, kdvYuzdesi = kdvYuzde, kdvTutari = kdvTutari3, stkBrm = fonksiyonlar.birimDegerBul(s.stkBrm), toplam = toplam, kdvsizToplam = kdvsiztoplam, stkKod = s.stkKod };

                    if (ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                    {
                        if (belge == 0)
                        {
                            FPUFuctions.Instance.WriteToDisplay(fonksiyonlar.donustur(s.stkAdKisa), adet + " X " + s.fiyatS1.ToString() + " " + toplam.ToString());

                            baslamaSonuc = FPUFuctions.Instance.Registeringsale(s.stkAd, "-", urunBarkod, "", 1, fonksiyonlar.kdvBul(int.Parse(s.KdvS), kdvTanimlari), 1, Convert.ToDecimal(s.fiyatS1), 0, adet2).ErrorCode;
                            LogYaz("Registeringsale(-) (" + urunBarkod.ToString() + "/" + Convert.ToDecimal(s.fiyatS1).ToString() + "/" + adet2.ToString() + ") Sonucu:" + baslamaSonuc.ToString());
                            if (baslamaSonuc != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                        }
                    }
                    sepetEkle(entity, 1);
                }
                else
                {
                    fonksiyonlar.HataMesaji("Ürün İptal Edilemedi", "İptal Satırı Bulunamadı", 2);
                }
            }
            textEdit1.BackColor = Color.LemonChiffon;
            textEdit2.EditValue = 1;
            sepetOku();
        }

        private void sepetSil()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{}").ToListAsync().Result;
            foreach (sepet s in sepets)
            {
                coll.DeleteOne(a => a.id == s.id);
            }

            dataGridView1.Rows.Clear();
        }

        private void sepetSil2()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("sepet");
            var sepets = coll.Find("{'kdvYuzdesi': {'$in' : [1, 8]}}").ToListAsync().Result;
            foreach (sepet s in sepets)
            {
                coll.DeleteOne(a => a.id == s.id);
            }

            dataGridView1.Rows.Clear();
        }

        private void textEdit14_Click(object sender, EventArgs e)
        {
            fcs = 9;
            textEdit14.Focus();
        }

        private void textEdit1_Click(object sender, EventArgs e)
        {
            fcs = 0;
        }

        private void textEdit2_Click(object sender, EventArgs e)
        {
            fcs = 1;
        }

        private void textEdit5_Click(object sender, EventArgs e)
        {
            fcs = 2;
        }

        private void textEdit6_Click(object sender, EventArgs e)
        {
            fcs = 3;
        }

        private void textEdit7_Click(object sender, EventArgs e)
        {
            fcs = 4;
        }

        private void textEdit9_Click(object sender, EventArgs e)
        {
            fcs = 5;
        }

        private void textEdit10_Click(object sender, EventArgs e)
        {
            fcs = 6;
        }

        private void Send(string key)
        {
            switch (this.fcs)
            {
                case 1:
                    textEdit2.Select();
                    break;

                case 0:
                    textEdit1.Select();
                    break;

                case 2:
                    textEdit5.Select();
                    break;

                case 3:
                    textEdit6.Select();
                    break;

                case 4:
                    textEdit7.Select();
                    break;

                case 5:
                    textEdit9.Select();
                    break;

                case 6:
                    textEdit10.Select();
                    break;

                case 7:

                    break;

                case 8:
                    textEdit13.Select();
                    break;

                case 9:
                    textEdit14.Select();
                    break;

                case 10:
                    textEdit15.Select();
                    break;

                case 11:
                    textEdit16.Select();
                    break;

                case 12:
                    textEdit18.Select();
                    break;

                case 13:
                    textEdit17.Select();
                    break;

                case 14:
                    textEdit19.Select();
                    break;

                case 15:
                    textEdit11.Select();
                    break;

                case 16:
                    textEdit21.Select();
                    break;

                case 17:
                    textEdit22.Select();
                    break;

                default:
                    textEdit1.Select();
                    break;
            }
            SendKeys.Send(key);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Send("1");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Send("2");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Send("3");
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            Send("4");
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            Send("5");
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Send("6");
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            Send("7");
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            Send("8");
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Send("9");
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            Send(".");
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            Send("0");
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Send("{BACKSPACE}");
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            float toplam = float.Parse(textEdit3.Text);
            if (numRows == 0)
            {
                fonksiyonlar.HataMesaji("Satış Gerçekleşmedi", "Sepette Ürün Yok", 2);
                textEdit1.Select();
            }
            else
            {
                if (toplam == 0)
                {
                    fonksiyonlar.HataMesaji("Satış Gerçekleşmedi", "0 Tutarında Belge Yazdırılamaz", 2);
                    textEdit1.Select();
                }
                else
                {
                    if (ConfigurationManager.AppSettings["Promosyon"].ToString() == "1" && this.belge != 5)
                    {
                        //Promosyon
                        double promoToplam = 0; //ürün promosyonları
                        double promoToplam2 = 0; // sepet promosyonları
                        double promoToplam3 = 0; // marka grup reyon kategori .. promosyonları
                        var client = new MongoClient();
                        var db = client.GetDatabase("Olivetti");
                        var coll1 = db.GetCollection<sepet>("sepet");
                        var sepets1 = coll1.Find("{$and: [{iptal:0},{satiriptal:0}]}").ToListAsync().Result;
                        db.DropCollection("promosepet");
                        db.DropCollection("promosyonToplamlari");
                        var collp = db.GetCollection<sepet>("promosepet");
                        var sepetsp = collp.Find("{}").ToListAsync().Result;

                        foreach (sepet s in sepets1)
                        {
                            var collp2 = db.GetCollection<sepet>("promosepet");
                            var sepetsp2 = collp2.Find("{stkID:'" + s.stkID + "'}").ToListAsync().Result;
                            double kayit = 0;
                            double pToplam = 0;
                            double pKdvTutari = 0;
                            double pKdvsiz = 0;

                            foreach (sepet pS in sepetsp2)
                            {
                                kayit = pS.miktar + s.miktar;
                                pToplam = pS.toplam + s.toplam;
                                pKdvTutari = pS.kdvTutari + s.kdvTutari;
                                pKdvsiz = pS.kdvsizToplam + s.kdvsizToplam;
                            }

                            if (kayit == 0)
                            {
                                collp.InsertOneAsync(s);
                            }
                            else
                            {
                                var update = Builders<sepet>.Update.Set("miktar", kayit).Set("toplam", pToplam).Set("kdvTutari", pKdvTutari).Set("kdvsizToplam", pKdvsiz);

                                collp.UpdateOne("{stkID:'" + s.stkID + "'}", update);
                            }
                        }

                        DateTime dt = DateTime.Now;
                        var tm = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                        var tm2 = tm.ToString("yyyy-MM-dd") + "T00:00:00.000Z";

                        string tm3 = tm.ToString("HHmm");

                        int gun = (int)dt.DayOfWeek;

                        double sepetIndirimiTutari = 0;
                        var coll = db.GetCollection<Promo>("promo");
                        var sepets = coll.Find("{$and: [ { promoAktif:1 },{'PromoMagaza.MagazaID':" + int.Parse(ayar.magazaNumarasi) + " }, {promoBaslangicZ : {$lte : '" + tm3 + "'}}, {promoBitisZ : {$gte : '" + tm3 + "'}}, {promoBaslangicT : { $lte: ISODate('" + tm2 + "') } },{promoBitisT : { $gte: ISODate('" + tm2 + "') } }, {promoGun: { $regex : '" + gun + "' }}]}").Sort("{sepetToplami:1}").ToListAsync().Result;

                        //MessageBox.Show("{$and: [ { promoAktif:1 },{'PromoMagaza.MagazaID':" + int.Parse(ayar.magazaNumarasi) + " }, {promoBaslangicZ : {$lte : '" + tm3 + "'}}, {promoBitisZ : {$gte : '" + tm3 + "'}}, {promoBaslangicT : { $lte: ISODate('" + tm2 + "') } },{promoBitisT : { $gte: ISODate('" + tm2 + "') } }, {promoGun: { $regex : '" + gun + "' }}]}");
                        foreach (Promo s in sepets)
                        {
                            //MessageBox.Show("Promosyon Var Tip : " + s.pomoAciklama);

                            if (s.promoTip == 24 || s.promoTip == 11) //x adet y alana z adet a bedava
                            {
                                string urunID1 = "";
                                string urunID2 = "";
                                int urun1Adet = 0;
                                int urun2Adet = 0;
                                double urunTutar = 0;
                                foreach (Promoayr pA in s.PromoAyr)
                                {
                                    urunID1 = pA.Urn1ID;
                                    urunID2 = pA.Urn2ID;
                                    urun1Adet = pA.Urn1Miktar;
                                    urun2Adet = pA.Urn2Miktar;
                                }

                                var coll3 = db.GetCollection<sepet>("promosepet");
                                var sepets3 = coll3.Find("{'stkID':'" + urunID1 + "'}").ToListAsync().Result;
                                var toplamAdet = sepets3.Sum(p => p.miktar);

                                //MessageBox.Show(toplamAdet.ToString());

                                int adet1 = int.Parse(urun1Adet.ToString());
                                int adet2 = int.Parse(urun2Adet.ToString());
                                int adet3 = int.Parse(toplamAdet.ToString());
                                int adet4 = (adet3 / adet1) * adet2;

                                //MessageBox.Show(adet4.ToString());

                                if (adet4 > 0)
                                {
                                    //MessageBox.Show("Promosyon Kazandınız");

                                    var coll4 = db.GetCollection<sepet>("sepet");
                                    var sepets4 = coll3.Find("{'stkID':'" + urunID2 + "'}").ToListAsync().Result;
                                    foreach (sepet s3 in sepets4)
                                    {
                                        if (s3.miktar >= adet4)
                                        {
                                            urunTutar = s3.fiyatS1 * adet4;
                                            double kdvDeger2 = 100 + s3.kdvYuzdesi;
                                            double kdvDeger = kdvDeger2 / 100;
                                            double indirimSonrasi = s3.toplam - urunTutar;
                                            double kdvTutari2 = indirimSonrasi / kdvDeger;
                                            double kdvTutari3 = indirimSonrasi - kdvTutari2;
                                            kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));
                                            var update = Builders<sepet>.Update
                                            .Set("sindirim", s3.sindirim + urunTutar)
                                            .Set("sindirimKdv", kdvTutari3);

                                            coll3.UpdateOne("{'_id':ObjectId('" + s3.id + "')}", update);
                                            promoToplam = promoToplam + urunTutar;
                                            textEdit23.Text = promoToplam.ToString("n2");

                                            break;
                                        }
                                    }

                                    var coll5 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");

                                    PromosyonToplamlari pToplam = new PromosyonToplamlari();
                                    pToplam.promosyonTex = s.pomoAciklama;
                                    pToplam.promosyonTutar = double.Parse(urunTutar.ToString("n2"));
                                    coll5.InsertOne(pToplam);
                                }
                            }

                            if (s.promoTip == 1 || s.promoTip == 2) //ürüne % veya TL indirimi
                            {
                                string urunID1 = "";
                                string urunID2 = "";
                                int urun1Adet = 0;
                                int urun2Adet = 0;
                                int urunIndTip = 0;
                                double urunTutar = 0;
                                double indirimOran = 0;
                                urunIndTip = s.indirimTipi;
                                foreach (Promoayr pA in s.PromoAyr)
                                {
                                    urunID1 = pA.Urn1ID;
                                    urunID2 = pA.Urn2ID;
                                    urun1Adet = pA.Urn1Miktar;
                                    urun2Adet = pA.Urn2Miktar;
                                }

                                var coll3 = db.GetCollection<sepet>("promosepet");
                                var sepets3 = coll3.Find("{'stkID':'" + urunID1 + "'}").ToListAsync().Result;
                                var toplamAdet = sepets3.Sum(p => p.miktar);
                                double toplamIndirim = sepets3.Sum(p => p.sindirim);

                                if (toplamAdet > 0)
                                {
                                    //MessageBox.Show("Promosyon Kazandınız");

                                    var coll4 = db.GetCollection<stk>("stk");
                                    var sepets4 = coll4.Find("{'_id':'" + urunID1 + "'}").ToListAsync().Result;
                                    foreach (stk s3 in sepets4)
                                    {
                                        if (urunIndTip == 0)
                                        {
                                            indirimOran = s3.stkInd1;
                                        }
                                        else
                                        {
                                            indirimOran = s3.stkInd2;
                                        }

                                        if (indirimOran > 0)
                                        {
                                            urunTutar = s3.fiyatS1 * toplamAdet;
                                            double urunIndirimi = 0;
                                            if (urunIndTip == 0)
                                            {
                                                urunIndirimi = urunTutar / 100;
                                                urunIndirimi = urunIndirimi * indirimOran;
                                            }
                                            else
                                            {
                                                urunIndirimi = indirimOran;
                                            }

                                            var update = Builders<sepet>.Update
                                            .Set("sindirim", toplamIndirim + urunIndirimi)
                                            .Set("sindirimKdv", 0);

                                            coll3.UpdateOne("{'stkID':'" + urunID1 + "'}", update);
                                            promoToplam = promoToplam + urunIndirimi;
                                            textEdit23.Text = promoToplam.ToString("n2");
                                            var coll5 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");

                                            PromosyonToplamlari pToplam = new PromosyonToplamlari();
                                            pToplam.promosyonTex = s.pomoAciklama;
                                            pToplam.promosyonTutar = double.Parse(urunIndirimi.ToString("n2"));
                                            coll5.InsertOne(pToplam);
                                            break;
                                        }
                                    }
                                }
                            }

                            if (s.promoTip == 17) // reyon tutar veya tl indirimi
                            {
                                string reyonAdi = "";
                                int grupTip = 0;
                                string reyonTipi = "@";
                                int urunIndTip = s.indirimTipi;
                                double indirimOran = s.indirim;
                                double urunTutar = 0;
                                foreach (Promoayr pA in s.PromoAyr)
                                {
                                    reyonAdi = pA.Urn1ID;
                                    grupTip = pA.Urn1Miktar;
                                }
                                switch (grupTip)
                                {
                                    case 0:
                                        reyonTipi = "kod1";
                                        break;

                                    case 1:
                                        reyonTipi = "kod2";
                                        break;

                                    case 2:
                                        reyonTipi = "kod3";
                                        break;

                                    case 3:
                                        reyonTipi = "kod4";
                                        break;

                                    case 4:
                                        reyonTipi = "kod5";
                                        break;
                                }

                                var coll3 = db.GetCollection<sepet>("promosepet");
                                var sepets3 = coll3.Find("{$and:[{" + reyonTipi + ":'" + reyonAdi + "'},{promoText:null}]}").ToListAsync().Result;
                                double toplamAdet = sepets3.Sum(p => p.toplam);
                                if (toplamAdet > 0)
                                {
                                    //MessageBox.Show("Promosyon Kazandınız");
                                    if (indirimOran > 0)
                                    {
                                        urunTutar = toplamAdet;
                                        double urunIndirimi = 0;
                                        if (urunIndTip == 0)
                                        {
                                            urunIndirimi = urunTutar / 100;
                                            urunIndirimi = urunIndirimi * indirimOran;
                                        }
                                        else
                                        {
                                            urunIndirimi = indirimOran;
                                        }

                                        var coll4 = db.GetCollection<sepet>("promosepet");
                                        var sepets4 = coll4.Find("{$and:[{" + reyonTipi + ":'" + reyonAdi + "'},{promoText:null}]}").ToListAsync().Result;
                                        foreach (sepet s4 in sepets4)
                                        {
                                            double indToplam = urunIndirimi;

                                            double indOran = (indToplam * 100) / toplamAdet;
                                            double indirimSatir = (s4.toplam / 100) * indOran;
                                            indirimSatir = double.Parse(indirimSatir.ToString("n2"));
                                            double indirimSonrasi = s4.toplam - indirimSatir;
                                            indirimSonrasi = double.Parse(indirimSonrasi.ToString("n2"));
                                            double kdvDeger2 = 100 + s4.kdvYuzdesi;
                                            double kdvDeger = kdvDeger2 / 100;
                                            double kdvTutari2 = indirimSonrasi / kdvDeger;
                                            double kdvTutari3 = indirimSonrasi - kdvTutari2;
                                            kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));

                                            var update = Builders<sepet>.Update
                                                 .Set("indirim", s4.indirim + indirimSatir)
                                                 .Set("indirimKdv", s4.indirimKdv + kdvTutari3)
                                                 .Set("indirimKalan", s4.indirimKalan + indirimSonrasi);
                                            coll4.UpdateOne("{'_id':ObjectId('" + s4.id + "')}", update);
                                        }

                                        var coll5 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");

                                        PromosyonToplamlari pToplam = new PromosyonToplamlari();
                                        pToplam.promosyonTex = s.pomoAciklama;
                                        pToplam.promosyonTutar = double.Parse(urunIndirimi.ToString("n2"));
                                        coll5.InsertOne(pToplam);

                                        promoToplam3 = promoToplam3 + double.Parse(urunIndirimi.ToString("n2"));
                                        textEdit23.Text = promoToplam3.ToString("n2");
                                    }
                                }
                            }

                            if (s.promoTip == 22 || s.promoTip == 23) // reyon tutarına ürün indirimi veya bedava ürün
                            {
                                string reyonAdi = "";
                                int grupTip = 0;
                                string reyonTipi = "@";
                                int urunIndTip = s.indirimTipi;
                                double indirimOran = s.indirim;
                                double sepetToplami = s.sepetToplami;
                                double urunTutar = 0;
                                string kampanyaUrun = "";
                                int kampanyaAdet = 0;
                                foreach (Promoayr pA in s.PromoAyr)
                                {
                                    reyonAdi = pA.Urn1ID;
                                    grupTip = pA.Urn1Miktar;
                                    kampanyaUrun = pA.Urn2ID;
                                    kampanyaAdet = pA.Urn2Miktar;
                                }
                                if (kampanyaAdet == 0)
                                {
                                    kampanyaAdet = 1;
                                }
                                switch (grupTip)
                                {
                                    case 0:
                                        reyonTipi = "kod1";
                                        break;

                                    case 1:
                                        reyonTipi = "kod2";
                                        break;

                                    case 2:
                                        reyonTipi = "kod3";
                                        break;

                                    case 3:
                                        reyonTipi = "kod4";
                                        break;

                                    case 4:
                                        reyonTipi = "kod5";
                                        break;
                                }

                                var coll3 = db.GetCollection<sepet>("promosepet");
                                var sepets3 = coll3.Find("{$and:[{" + reyonTipi + ":'" + reyonAdi + "'},{promoText:null}]}").ToListAsync().Result;
                                double toplamAdet = sepets3.Sum(p => p.toplam);
                                if (toplamAdet > sepetToplami)
                                {
                                    //MessageBox.Show("Promosyon Kazandınız");
                                    if (indirimOran > 0)
                                    {
                                        var coll4 = db.GetCollection<sepet>("promosepet");
                                        var sepets4 = coll4.Find("{stkID:'" + kampanyaUrun + "'}").ToListAsync().Result;
                                        double toplamAdet2 = sepets4.Sum(p => p.toplam);
                                        double toplamAdet3 = sepets4.Sum(p => p.miktar);
                                        urunTutar = toplamAdet2;
                                        if (urunTutar > 0 && toplamAdet3 >= kampanyaAdet)
                                        {
                                            if (s.promoTip == 23)
                                            {
                                                urunTutar = urunTutar / toplamAdet3;
                                                urunTutar = urunTutar * kampanyaAdet;
                                            }
                                            double urunIndirimi = 0;
                                            if (urunIndTip == 1)
                                            {
                                                urunIndirimi = urunTutar / 100;
                                                urunIndirimi = urunIndirimi * indirimOran;
                                            }
                                            else
                                            {
                                                urunIndirimi = indirimOran;
                                            }

                                            foreach (sepet s4 in sepets4)
                                            {
                                                double indToplam = urunIndirimi;

                                                double indOran = (indToplam * 100) / toplamAdet2;
                                                double indirimSatir = (s4.toplam / 100) * indOran;
                                                indirimSatir = double.Parse(indirimSatir.ToString("n2"));
                                                double indirimSonrasi = s4.toplam - indirimSatir;
                                                indirimSonrasi = double.Parse(indirimSonrasi.ToString("n2"));
                                                double kdvDeger2 = 100 + s4.kdvYuzdesi;
                                                double kdvDeger = kdvDeger2 / 100;
                                                double kdvTutari2 = indirimSonrasi / kdvDeger;
                                                double kdvTutari3 = indirimSonrasi - kdvTutari2;
                                                kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));

                                                var update = Builders<sepet>.Update
                                               .Set("sindirim", s4.sindirim + urunIndirimi)
                                               .Set("sindirimKdv", 0);
                                                coll4.UpdateOne("{'_id':ObjectId('" + s4.id + "')}", update);
                                            }
                                            var coll5 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");

                                            PromosyonToplamlari pToplam = new PromosyonToplamlari();
                                            pToplam.promosyonTex = s.pomoAciklama;
                                            pToplam.promosyonTutar = double.Parse(urunIndirimi.ToString("n2"));
                                            coll5.InsertOne(pToplam);

                                            promoToplam3 = promoToplam3 + double.Parse(urunIndirimi.ToString("n2"));
                                            textEdit23.Text = promoToplam3.ToString("n2");
                                        }
                                    }
                                }
                            }

                            if (s.promoTip == 8) // sepet toplamına hediye ürün
                            {
                                int urunIndTip = s.indirimTipi;
                                double indirimOran = s.indirim;
                                double sepetToplami = s.sepetToplami;
                                double urunTutar = 0;
                                string kampanyaUrun = "";
                                int kampanyaAdet = 0;
                                foreach (Promoayr pA in s.PromoAyr)
                                {
                                    kampanyaUrun = pA.Urn2ID;
                                    kampanyaAdet = pA.Urn2Miktar;
                                }
                                if (kampanyaAdet == 0)
                                {
                                    kampanyaAdet = 1;
                                }

                                var coll3 = db.GetCollection<sepet>("promosepet");
                                var sepets3 = coll3.Find("{}").ToListAsync().Result;
                                double toplamAdet = sepets3.Sum(p => p.toplam);
                                double toplamInd = sepets3.Sum(p => p.indirim);
                                double toplamInd2 = sepets3.Sum(p => p.sindirim);
                                toplamAdet = toplamAdet - toplamInd;
                                toplamAdet = toplamAdet - toplamInd2;
                                if (toplamAdet >= sepetToplami)
                                {
                                    //MessageBox.Show("Promosyon Kazandınız");
                                    var coll4 = db.GetCollection<sepet>("promosepet");
                                    var sepets4 = coll4.Find("{stkID:'" + kampanyaUrun + "'}").ToListAsync().Result;
                                    double toplamAdet2 = sepets4.Sum(p => p.toplam);
                                    double toplamAdet3 = sepets4.Sum(p => p.miktar);
                                    urunTutar = toplamAdet2;
                                    if (urunTutar > 0 && toplamAdet3 >= kampanyaAdet)
                                    {
                                        urunTutar = urunTutar / toplamAdet3;
                                        urunTutar = urunTutar * kampanyaAdet;
                                    }
                                    double urunIndirimi = urunTutar;
                                    foreach (sepet s4 in sepets4)
                                    {
                                        double indToplam = urunIndirimi;

                                        double indOran = (indToplam * 100) / toplamAdet2;
                                        double indirimSatir = (s4.toplam / 100) * indOran;
                                        indirimSatir = double.Parse(indirimSatir.ToString("n2"));
                                        double indirimSonrasi = s4.toplam - indirimSatir;
                                        indirimSonrasi = double.Parse(indirimSonrasi.ToString("n2"));
                                        double kdvDeger2 = 100 + s4.kdvYuzdesi;
                                        double kdvDeger = kdvDeger2 / 100;
                                        double kdvTutari2 = indirimSonrasi / kdvDeger;
                                        double kdvTutari3 = indirimSonrasi - kdvTutari2;
                                        kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));

                                        var update = Builders<sepet>.Update
                                       .Set("sindirim", s4.sindirim + urunIndirimi)
                                       .Set("sindirimKdv", 0);
                                        coll4.UpdateOne("{'_id':ObjectId('" + s4.id + "')}", update);
                                    }
                                    var coll5 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");

                                    PromosyonToplamlari pToplam = new PromosyonToplamlari();
                                    pToplam.promosyonTex = s.pomoAciklama;
                                    pToplam.promosyonTutar = double.Parse(urunIndirimi.ToString("n2"));
                                    coll5.InsertOne(pToplam);

                                    promoToplam3 = promoToplam3 + double.Parse(urunIndirimi.ToString("n2"));
                                    textEdit23.Text = promoToplam3.ToString("n2");
                                }
                            }

                            if (s.promoTip == 3) //sepet tutarına % veya TL indirimi
                            {
                                string urunID1 = "";
                                string urunID2 = "";
                                int urun1Adet = 0;
                                int urun2Adet = 0;
                                int urunIndTip = s.indirimTipi;
                                double urunTutar = 0;
                                double indirimOran = s.indirim;
                                double sepetToplami = s.sepetToplami;
                                foreach (Promoayr pA in s.PromoAyr)
                                {
                                    urunID1 = pA.Urn1ID;
                                    urunID2 = pA.Urn2ID;
                                    urun1Adet = pA.Urn1Miktar;
                                    urun2Adet = pA.Urn2Miktar;
                                }

                                var coll3 = db.GetCollection<sepet>("promosepet");
                                var sepets3 = coll3.Find("{}").ToListAsync().Result;
                                double toplamAdet = sepets3.Sum(p => p.toplam);
                                double toplamInd = sepets3.Sum(p => p.indirim);
                                double toplamInd2 = sepets3.Sum(p => p.sindirim);
                                toplamAdet = toplamAdet - toplamInd;
                                toplamAdet = toplamAdet - toplamInd2;
                                if (toplamAdet >= sepetToplami)
                                {
                                    var sepetsT = coll.Find("{$and: [ {promoTip:3},{sepetToplami: {$gte:" + sepetToplami.ToString().Replace(',', '.') + "}},{sepetToplami:{$lte:" + toplamAdet.ToString().Replace(',', '.') + "}},{ promoTip:3 },{ promoAktif:1 },{'PromoMagaza.MagazaID':" + int.Parse(ayar.magazaNumarasi) + " }, {promoBaslangicZ : {$lte : '" + tm3 + "'}}, {promoBitisZ : {$gte : '" + tm3 + "'}}, {promoBaslangicT : { $lte: ISODate('" + tm2 + "') } },{promoBitisT : { $gte: ISODate('" + tm2 + "') } }, {promoGun: { $regex : '" + gun + "' }}]}").Sort("{sepetToplami:1}").ToListAsync().Result;
                                    var sepetsTCnt = sepetsT.Count();
                                    if (sepetsTCnt == 1)
                                    {
                                        if (sepetIndirimiTutari == 0 || sepetIndirimiTutari < s.sepetToplami)
                                        {
                                            //MessageBox.Show("Promosyon Kazandınız");

                                            if (indirimOran > 0)
                                            {
                                                urunTutar = sepetToplami;
                                                double urunIndirimi = 0;
                                                if (urunIndTip == 0)
                                                {
                                                    urunIndirimi = urunTutar / 100;
                                                    urunIndirimi = urunIndirimi * indirimOran;
                                                }
                                                else
                                                {
                                                    urunIndirimi = indirimOran;
                                                }

                                                var coll4 = db.GetCollection<sepet>("promosepet");
                                                var sepets4 = coll4.Find("{}").ToListAsync().Result;
                                                foreach (sepet s4 in sepets4)
                                                {
                                                    //double indOran = indirimOran;
                                                    //double indirimSatir = (urunIndirimi / 100) * indOran;
                                                    //indirimSatir = double.Parse(indirimSatir.ToString("n2"));
                                                    //double indirimSonrasi = s4.toplam - indirimSatir;
                                                    //indirimSonrasi = double.Parse(indirimSonrasi.ToString("n2"));
                                                    //double kdvDeger2 = 100 + s4.kdvYuzdesi;
                                                    //double kdvDeger = kdvDeger2 / 100;
                                                    //double kdvTutari2 = indirimSonrasi / kdvDeger;
                                                    //double kdvTutari3 = indirimSonrasi - kdvTutari2;
                                                    //kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));

                                                    double indToplam = urunIndirimi;

                                                    double indOran = (indToplam * 100) / toplamAdet;
                                                    double indirimSatir = (s4.toplam / 100) * indOran;
                                                    indirimSatir = double.Parse(indirimSatir.ToString("n2"));
                                                    double indirimSonrasi = s4.toplam - indirimSatir;
                                                    indirimSonrasi = double.Parse(indirimSonrasi.ToString("n2"));
                                                    double kdvDeger2 = 100 + s4.kdvYuzdesi;
                                                    double kdvDeger = kdvDeger2 / 100;
                                                    double kdvTutari2 = indirimSonrasi / kdvDeger;
                                                    double kdvTutari3 = indirimSonrasi - kdvTutari2;
                                                    kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));

                                                    var update = Builders<sepet>.Update
                                                         .Set("indirim", s4.indirim + indirimSatir)
                                                         .Set("indirimKdv", s4.indirimKdv + kdvTutari3)
                                                         .Set("indirimKalan", s4.indirimKalan + indirimSonrasi);
                                                    coll4.UpdateOne("{'_id':ObjectId('" + s4.id + "')}", update);
                                                }

                                                var coll5 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");

                                                PromosyonToplamlari pToplam = new PromosyonToplamlari();
                                                pToplam.promosyonTex = s.pomoAciklama;
                                                pToplam.promosyonTutar = double.Parse(urunIndirimi.ToString("n2"));
                                                coll5.InsertOne(pToplam);

                                                promoToplam2 = double.Parse(urunIndirimi.ToString("n2"));
                                                sepetIndirimiTutari = s.sepetToplami;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        promoToplam = promoToplam + promoToplam2 + promoToplam3;
                        textEdit23.Text = promoToplam.ToString("n2");

                        sepetOku3();
                    }

                    belgeiptal = 0;
                    simpleButton16.Enabled = false;
                    groupControl8.Visible = true;
                    fcs = 2;
                    textEdit5.Text = textEdit3.Text;
                    textEdit12.Text = textEdit3.Text;
                    simpleButton58.Text = bankaText;
                    textEdit5.Focus();

                    if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
                    {
                        if (belge == 10)
                        {
                            simpleButton59.PerformClick();
                        }
                        else
                        {
                            textEdit7.Enabled = false;
                            simpleButton59.Enabled = false;
                        }
                    }

                    //aratoplam yazı
                    FPUFuctions.Instance.WriteToDisplay("TOPLAM", textEdit3.Text + " TL");
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Send("{ENTER}");
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Send("*");
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (double.Parse(textEdit2.Text) > 0)
            {
            }
            else
            {
                textEdit2.Text = "1";
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (textEdit1.Text.IndexOf("*") != -1)
            {
                string[] words = textEdit1.Text.Split('*');
                textEdit1.EditValue = "";
                textEdit1.Text = "";

                textEdit2.Select();
                textEdit2.EditValue = words[0];
                textEdit1.Select();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            textEdit1.BackColor = Color.Salmon;
            textEdit1.Properties.NullValuePrompt = "<IPTAL>";
            textEdit1.Select();
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            urunSec frm = new urunSec(kasiyerID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (textEdit1.Properties.NullValuePrompt == "<IPTAL>")
                {
                    urunIptal(frm.urunBarkod);

                    textEdit1.Properties.NullValuePrompt = "Barkod Okutunuz";
                }
                else
                {
                    SepeteEkle(frm.urunBarkod);
                }
            }
            groupControl4.Visible = true;
            groupControl7.Visible = false;
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if (groupControl7.Visible == true)
            {
                groupControl6.Visible = false;
                groupControl7.Visible = false;
                groupControl4.Visible = true;
            }
            else
            {
                groupControl6.Visible = false;
                groupControl7.Visible = true;
                groupControl4.Visible = false;
            }
            textEdit1.Select();
            fcs = 0;
        }

        private void simpleButton53_Click(object sender, EventArgs e)
        {
            simpleButton16.Enabled = true;
            textEdit4.Text = "0";
            textEdit5.Text = "0";
            textEdit6.Text = "0";
            textEdit7.Text = "0";
            textEdit8.Text = "0";
            textEdit9.Text = "0";
            textEdit10.Text = "0";
            textEdit12.Text = "0";
            textEdit23.Text = "0";
            groupControl8.Visible = false;
            textEdit1.Select();
            simpleButton58.Text = bankaText;
            fcs = 0;
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            odemeToplam();
        }

        private void odemeToplam()
        {
            float toplam = 0;
            float indirim = 0;
            float nakit = 0;
            float kredikarti = 0;
            float yemekceki = 0;
            float promosyon = 0;

            toplam = float.Parse(textEdit3.Text);
            indirim = float.Parse(textEdit4.Text);
            nakit = float.Parse(textEdit5.Text);
            kredikarti = float.Parse(textEdit6.Text);
            yemekceki = float.Parse(textEdit7.Text);

            float total = toplam + indirim;
            total = float.Parse(total.ToString("n2"));
            total = total - nakit;
            total = float.Parse(total.ToString("n2"));
            total = total - kredikarti;
            total = float.Parse(total.ToString("n2"));
            total = total - yemekceki;
            total = float.Parse(total.ToString("n2"));

            if (total < 0.00)
            {
                labelControl16.Text = "Para Üstü";
                labelControl17.Text = total.ToString("n2").Replace("-", "");
                groupControl9.BackColor = Color.Green;
            }
            else
            {
                labelControl16.Text = "Kalan";
                labelControl17.Text = total.ToString("n2");
                groupControl9.BackColor = Color.Red;
            }
            if (total == 0.00)
            {
                labelControl16.Text = "Para Üstü";
                labelControl17.Text = total.ToString("n2").Replace("-", "");
                groupControl9.BackColor = Color.Green;
            }
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {
            odemeToplam();
        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {
            odemeToplam();
        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            odemeToplam();
        }

        private void textEdit7_EditValueChanged(object sender, EventArgs e)
        {
            odemeToplam();
        }

        private void simpleButton55_Click(object sender, EventArgs e)
        {
            float indirimYuzde = float.Parse(textEdit9.Text);
            float indirimTutar = float.Parse(textEdit10.Text.Replace(".", ","));
            float toplam = float.Parse(textEdit3.Text);
            if (indirimYuzde > 99)
            {
                fonksiyonlar.HataMesaji("İndirim Yapılamadı", "İndirim yüzdesi 99 dan fazla olamaz", 2);
                return;
            }

            if (indirimTutar >= toplam)
            {
                fonksiyonlar.HataMesaji("İndirim Yapılamadı", "İndirim tutarı belge toplamından düşük olmalıdır", 2);
                return;
            }
            if (indirimTutar > 0)
            {
                textEdit4.Text = "-" + indirimTutar.ToString("n2");
            }
            else
            {
                if (indirimYuzde > 0)
                {
                    float indirilenTutar = (toplam / 100) * indirimYuzde;
                    if (indirilenTutar.ToString("n2") == toplam.ToString("n2"))
                    {
                        double fazlalik = 0.01;
                        indirilenTutar = indirilenTutar - float.Parse(fazlalik.ToString());
                    }
                    textEdit4.Text = "-" + indirilenTutar.ToString("n2");
                    textEdit8.Text = indirimYuzde.ToString();
                }
            }
            if (textEdit4.Text != "0")
            {
                float kalan = (toplam + float.Parse(textEdit4.Text));

                textEdit12.Text = kalan.ToString("n2");
                textEdit5.Text = textEdit12.Text;
            }

            if (indirimTutar > 0 || indirimYuzde > 0)
            {
                var client = new MongoClient();
                var db = client.GetDatabase("Olivetti");
                var coll = db.GetCollection<sepet>("sepet");
                var sepets = coll.Find("{}").ToListAsync().Result;
                foreach (sepet s in sepets)
                {
                    string indT = textEdit4.Text.Replace('-', ' ').Trim();

                    double indToplam = double.Parse(indT.Replace(".", ","));

                    double indOran = (indToplam * 100) / toplam;
                    double indirimSatir = (s.toplam / 100) * indOran;
                    indirimSatir = double.Parse(indirimSatir.ToString("n2"));
                    double indirimSonrasi = s.toplam - indirimSatir;
                    indirimSonrasi = double.Parse(indirimSonrasi.ToString("n2"));
                    double kdvDeger2 = 100 + s.kdvYuzdesi;
                    double kdvDeger = kdvDeger2 / 100;
                    double kdvTutari2 = indirimSonrasi / kdvDeger;
                    double kdvTutari3 = indirimSonrasi - kdvTutari2;
                    kdvTutari3 = double.Parse(kdvTutari3.ToString("n2"));

                    var update = Builders<sepet>.Update
                         .Set("indirim", indirimSatir)
                         .Set("indirimKdv", kdvTutari3)
                         .Set("indirimKalan", indirimSonrasi);
                    coll.UpdateOne("{'_id':ObjectId('" + s.id + "')}", update);
                }

                LogYaz("İndirim Yapıldı:(" + textEdit4.Text.Replace('-', ' ').Trim() + ")");
            }

            groupControl10.Visible = false;
            fcs = 2;
            textEdit5.Focus();
            simpleButton16.Enabled = true;
            simpleButton52.Enabled = true;
            simpleButton53.Enabled = true;
        }

        private void simpleButton54_Click(object sender, EventArgs e)
        {
            groupControl10.Visible = true;
            textEdit9.Select();
            fcs = 5;
            simpleButton16.Enabled = false;
            simpleButton52.Enabled = false;
            simpleButton53.Enabled = false;
        }

        private void simpleButton52_Click(object sender, EventArgs e)
        {
            odemeToplam();
            float kalan = float.Parse(labelControl17.Text);
            float toplam = 0;
            float kredikarti = 0;
            float nakit = 0;
            float yemekCeki = 0;
            toplam = float.Parse(textEdit3.Text);
            kredikarti = float.Parse(textEdit6.Text);
            nakit = float.Parse(textEdit5.Text);
            yemekCeki = float.Parse(textEdit7.Text);

            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                fonksiyonlar.HataMesaji("Satış Gerçekleşmedi", "Ürün Seçilmedi", 2);
                textEdit1.Select();
            }
            else
            {
                if (kredikarti > toplam)
                {
                    fonksiyonlar.HataMesaji("Hatalı Ödeme", "Kredi Kartı ödemesi sepet tutarından fazla olamaz", 2);
                    textEdit5.SelectAll();
                    return;
                }
                if (yemekCeki > toplam)
                {
                    fonksiyonlar.HataMesaji("Hatalı Ödeme", "Yemek çeki ödemesi sepet tutarından fazla olamaz", 2);
                    textEdit5.SelectAll();
                    fcs = 2;
                    return;
                }

                if (kredikarti > 0)
                {
                    if (nakit < toplam)
                    {
                    }
                    else
                    {
                        fonksiyonlar.HataMesaji("Hatalı Ödeme", "Nakit ve kredi kartı ödeme tutarlarını kontrol ediniz", 2);
                        return;
                    }
                }

                if (labelControl16.Text == "Kalan")
                {
                    fonksiyonlar.HataMesaji("Satış Gerçekleşmedi", "Para Eksik", 2);
                }
                else
                {
                    LogYaz("Aratoplam :Kalan:(" + kalan.ToString() + ")Toplam:(" + toplam.ToString() + ")KrediKartı:(" + kredikarti.ToString() + ")Nakit:(" + nakit.ToString() + ")YemekCeki:(" + yemekCeki.ToString() + ")");
                    satisTamamla();
                }
            }
        }

        private void satisTamamla()
        {
            if (belge == 1 || belge == 2 || belge == 3 || belge == 4)
            {
                if (this.musteriVNo.Length < 10 || this.musteriVNo.Length > 11)
                {
                    fonksiyonlar.HataMesaji("Vergi Numarası Hatalı", "Müşteri vergi numarası hatalı", 2);
                    return;
                }
            }

            ShowMessage("İşlem Yapılıyor", "Lütfen bekleyiniz...");

            double nakit = double.Parse(textEdit5.Text);
            decimal indirim = decimal.Parse(textEdit4.Text);
            double krediKarti = double.Parse(textEdit6.Text);
            double yemekCeki = double.Parse(textEdit7.Text);
            double kalan = double.Parse(labelControl17.Text);

            if (belgeiptal == 0)
            {
                double kasaNakit = 200;
                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                {
                    kasaNakit = double.Parse(FPUFuctions.Instance.GetCashInDrawer().Cash.Replace(".", ","));
                }

                if (kasaNakit < kalan)
                {
                    HideMessage();
                    fonksiyonlar.HataMesaji("Satış Gerçekleşemedi!", "Para üstü için yeterli miktar kasada mevcut değil.", 2);
                    return;
                }
            }

            if (labelControl16.Text == "Para Üstü")
            {
                if (kalan != 0)
                {
                    if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                    {
                        FPUFuctions.Instance.WriteToDisplay("PARA USTU", labelControl17.Text + " TL");
                    }
                }
            }

            if (belgeiptal == 0)
            {
                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                {
                    FPUFuctions.Instance.OpenDrawer();
                }
            }

            if (yemekCeki > 0)
            {
                belge = 10;
            }

            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var collection = db.GetCollection<PosBelge>("PosBelge");
            var collection2 = db.GetCollection<PosBelge2>("PosBelge2");
            var coll = db.GetCollection<sepet>("sepet");

            if (ConfigurationManager.AppSettings["Promosyon"].ToString() == "1")
            {
                if (belge != 5)
                {
                    coll = db.GetCollection<sepet>("promosepet");
                }
            }

            var sepets = coll.Find("{}").Sort("{_id:1}").ToListAsync().Result;
            if (yemekCeki > 0)
            {
                sepets = coll.Find("{'kdvYuzdesi': {'$in' : [1, 8]}}").ToListAsync().Result;
            }

            double satirIndirimi = sepets.Sum(p => p.sindirim);

            string disdosya = "";
            string disdosya2 = "";
            string disdosya3 = "";
            PosBelge yeniPosBelge = new PosBelge();
            PosBelge2 yeniPosBelge2 = new PosBelge2();

            yeniPosBelge.EvrakNo = this.evrakNo;
            yeniPosBelge.FisNo = fonksiyonlar.FisNo();
            yeniPosBelge.BelgeTip = this.belge;
            yeniPosBelge.Kasiyer = int.Parse(kasiyerTanm.kasiyerkodu);
            yeniPosBelge.Magaza = int.Parse(ayar.magazaNumarasi);
            yeniPosBelge.mKartNo = 0;
            yeniPosBelge.iptal = this.belgeiptal;
            yeniPosBelge.Musteri = this.musteriVNo;
            yeniPosBelge.PosNo = int.Parse(ayar.kasaNumarasi);
            yeniPosBelge.Aktarim = 0;
            DateTime dt = DateTime.Now;
            var tm = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            yeniPosBelge.Saat = tm;
            yeniPosBelge.Tarih = tm;
            if (fisbasla == 0)
            {
                int fisTipi = 1;

                switch (belge)
                {
                    case 1:
                        fisTipi = 1;
                        break;

                    case 2:
                        fisTipi = 2;
                        break;

                    case 3:
                        fisTipi = 3;
                        break;

                    case 4:
                        fisTipi = 1;
                        break;

                    case 6:
                        fisTipi = 2;
                        break;

                    case 10:
                        fisTipi = 5;
                        break;

                    default:
                        fisTipi = -1;
                        break;
                }

                if (belge != 5)
                {
                    if (fisbasla == 0)
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.OpenFiscalReceipt(int.Parse(kasiyerTanm.kasiyerkodu), fisTipi, kasiyerTanm.kasiyerSifre, this.musteriVNo, evrakNo, 1).ErrorCode;
                            LogYaz("OpenFiscalReceipt (" + fisTipi.ToString() + ") Sonuc:" + baslamaSonuc.ToString());
                        }
                        //if (baslamaSonuc != 0)
                        //{
                        //    HideMessage();
                        //    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                        //    fonksiyonlar.HataMesaji("1-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                        //    return;
                        //}
                    }
                }
            }

            if (belge != 0 && belge != 10)
            {
                yeniPosBelge2.EvrakNo = this.evrakNo;
                yeniPosBelge2.FisNo = 1;
                yeniPosBelge2.BelgeTip = this.belge.ToString();
                yeniPosBelge2.Kasiyer = int.Parse(kasiyerTanm.kasiyerkodu);
                yeniPosBelge2.Magaza = int.Parse(ayar.magazaNumarasi);
                yeniPosBelge2.mKartNo = 0;
                yeniPosBelge2.Musteri = this.musteriVNo;
                yeniPosBelge2.PosNo = 1;
                yeniPosBelge2.Tarih = tm.ToString();
            }

            List<Belgeayr> ayrintilar = new List<Belgeayr>();
            List<KdvListesi> kdvler = new List<KdvListesi>();
            List<Belgeayr2> ayrintilar2 = new List<Belgeayr2>();
            int sira = 0;
            double toplam = 0;
            double kdvIndirim = 0;
            double kdvToplam0 = 0;
            double kdvToplam1 = 0;
            double kdvToplam2 = 0;
            double kdvToplam3 = 0;
            double kdvToplami0 = 0;
            double kdvToplami1 = 0;
            double kdvToplami2 = 0;
            double kdvToplami3 = 0;
            double kdvIndirimToplam0 = 0;
            double kdvIndirimToplam1 = 0;
            double kdvIndirimToplam2 = 0;
            double kdvIndirimToplam3 = 0;
            double IndirimToplam0 = 0;
            double IndirimToplam1 = 0;
            double IndirimToplam2 = 0;
            double IndirimToplam3 = 0;
            double yemekCekiTutari = 0;
            int satirSayisi = 3;
            int sira2 = 0;
            string flag = "";

            if (indirim < 0 || satirIndirimi > 0)
            {
                sira2 = 2;
            }
            else
            {
                sira2 = 1;
            }
            double logKdvTutari = 0;
            foreach (sepet s in sepets)
            {
                if (s.iptal == 0)
                {
                    toplam = toplam + s.toplam;
                }
                else
                {
                    toplam = toplam - s.toplam;
                }
                yemekCekiTutari = yemekCekiTutari + s.toplam;
                yemekCekiTutari = yemekCekiTutari - s.indirim;
                yemekCekiTutari = yemekCekiTutari - s.sindirim;
                kdvIndirim = kdvIndirim + s.indirimKdv;
                kdvIndirim = kdvIndirim + s.sindirimKdv;
                sira = sira + 1;
                Belgeayr ayrinti = new Belgeayr();
                ayrinti.Miktar = s.miktar;
                ayrinti.SatirNo = sira;
                ayrinti.Tutar = s.toplam;
                ayrinti.Barkod = s.barkod;
                ayrinti.IndSatir = s.sindirim;
                ayrinti.IndToplam = s.indirim;
                ayrinti.Kdv = s.kdvYuzdesi;
                ayrinti.Promo = 0;
                ayrinti.Stk = s.stkID;
                ayrinti.iptal = s.iptal;
                ayrintilar.Add(ayrinti);

                switch (s.kdvYuzdesi)
                {
                    case 0:
                        if (s.sindirimKdv > 0)
                        {
                            kdvToplam0 = kdvToplam0 + s.sindirimKdv;
                        }
                        else
                        {
                            kdvToplam0 = kdvToplam0 + s.kdvTutari;
                        }

                        kdvToplami0 = kdvToplami0 + s.toplam;
                        kdvIndirimToplam0 = kdvIndirimToplam0 + s.indirimKdv;
                        IndirimToplam0 = IndirimToplam0 + s.indirimKalan;
                        kdvIndirimToplam0 = kdvIndirimToplam0 + s.sindirimKdv;
                        IndirimToplam0 = IndirimToplam0 + (s.toplam - s.sindirim);

                        break;

                    case 1:
                        if (s.sindirimKdv > 0)
                        {
                            kdvToplam1 = kdvToplam1 + s.sindirimKdv;
                        }
                        else
                        {
                            kdvToplam1 = kdvToplam1 + s.kdvTutari;
                        }

                        kdvToplami1 = kdvToplami1 + s.toplam;
                        kdvIndirimToplam1 = kdvIndirimToplam1 + s.indirimKdv;
                        IndirimToplam1 = IndirimToplam1 + s.indirimKalan;
                        kdvIndirimToplam1 = kdvIndirimToplam1 + s.sindirimKdv;
                        IndirimToplam1 = IndirimToplam1 + (s.toplam - s.sindirim);
                        break;

                    case 8:
                        if (s.sindirimKdv > 0)
                        {
                            kdvToplam2 = kdvToplam2 + s.sindirimKdv;
                        }
                        else
                        {
                            kdvToplam2 = kdvToplam2 + s.kdvTutari;
                        }
                        kdvToplami2 = kdvToplami2 + s.toplam;
                        kdvIndirimToplam2 = kdvIndirimToplam2 + s.indirimKdv;
                        IndirimToplam2 = IndirimToplam2 + s.indirimKalan;
                        kdvIndirimToplam2 = kdvIndirimToplam2 + s.sindirimKdv;
                        IndirimToplam2 = IndirimToplam2 + (s.toplam - s.sindirim);
                        break;

                    case 18:
                        if (s.sindirimKdv > 0)
                        {
                            kdvToplam3 = kdvToplam3 + s.sindirimKdv;
                        }
                        else
                        {
                            kdvToplam3 = kdvToplam3 + s.kdvTutari;
                        }
                        kdvToplami3 = kdvToplami3 + s.toplam;
                        kdvIndirimToplam3 = kdvIndirimToplam3 + s.indirimKdv;
                        IndirimToplam3 = IndirimToplam3 + s.indirimKalan;
                        kdvIndirimToplam3 = kdvIndirimToplam3 + s.sindirimKdv;
                        IndirimToplam3 = IndirimToplam3 + (s.toplam - s.sindirim);
                        break;
                }

                string miktarDis = s.miktar.ToString();

                if (s.barkod.Length > 2)
                {
                    if (s.barkod.Substring(0, 2) == "28" || s.barkod.Substring(0, 2) == "29")
                    {
                        miktarDis = (s.miktar * 1000).ToString().Replace(",", "");
                    }
                }

                logKdvTutari = s.kdvTutari;
                if (s.sindirim > 0)
                {
                    logKdvTutari = s.sindirimKdv;
                }
                sira2 = sira2 + 1;
                disdosya = disdosya + "\r\n02 " + sira.ToString().PadRight(6, ' ') + s.stkKod.ToString().PadRight(24, ' ') + s.iptal + "0     " + s.kdv.ToString().PadRight(2, ' ') + fonksiyonlar.kdvYuzdeBul(s.kdv).ToString().PadRight(3, ' ') + miktarDis.ToString().PadRight(15, ' ') + s.stkBrm + s.fiyatS1.ToString("n2").PadRight(15, ' ') + (s.miktar * s.fiyatS1).ToString("n2").PadRight(15, ' ') + s.kdvTutari.ToString("n2").PadRight(15, ' ') + "0              " + "0              " + "0              " + "0              " + "0              " + 0 + s.barkod.ToString().PadRight(24, ' ') + "0               " + "0 " + "000" + "0                                      0           0           ";
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "IS" + s.iptal + "XX" + "00000" + s.kdv + s.kdvYuzdesi.ToString().PadLeft(2, '0') + miktarDis.ToString().Replace(",", "").PadLeft(6, '0') + s.stkBrm.ToString() + s.fiyatS1.ToString("n2").Replace(',', '.').PadLeft(15, '0') + s.toplam.ToString("n2").Replace(',', '.').PadLeft(15, '0') + logKdvTutari.ToString("n2").Replace(',', '.').PadLeft(15, '0') + "0" + s.barkod.ToString().PadRight(24, ' ') + "0.00".ToString().PadLeft(15, '0') + "00000";
                if (indirim < 0 || satirIndirimi > 0)
                {
                    disdosya3 = disdosya3 + "1";
                }
                else
                {
                    disdosya3 = disdosya3 + "0";
                }
                disdosya3 = disdosya3 + "00XXXXXXXXXX";
                satirSayisi = satirSayisi + 1;
                if (indirim < 0)
                {
                    sira2 = sira2 + 1;
                    disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "ID" + belgeiptal + "XX" + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + s.indirim.ToString("n2").Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0".ToString().PadRight(13, 'X');
                    satirSayisi = satirSayisi + 1;
                }
                if (s.sindirim > 0)
                {
                    sira2 = sira2 + 1;
                    disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "ID" + belgeiptal + "XX" + s.sindirim.ToString("n2").Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + s.sindirim.ToString("n2").Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0".ToString().PadRight(13, 'X');
                    satirSayisi = satirSayisi + 1;
                }

                if (belge != 0 && belge != 10)
                {
                    Belgeayr2 ayrinti2 = new Belgeayr2();
                    ayrinti2.stkAd = s.stkAd;
                    ayrinti2.stkFiyat = s.fiyatS1.ToString("n2");
                    ayrinti2.Miktar = s.miktar.ToString();
                    ayrinti2.SatirNo = sira;
                    ayrinti2.Tutar = (s.miktar * s.fiyatS1).ToString("n2");
                    ayrinti2.Barkod = s.barkod;
                    ayrinti2.IndSatir = "0";
                    ayrinti2.IndToplam = "0";
                    ayrinti2.Kdv = s.kdvYuzdesi.ToString();
                    ayrinti2.Promo = 0;
                    ayrinti2.iptal = s.iptal;
                    ayrintilar2.Add(ayrinti2);
                }

                if (belge != 5)
                {
                    if (fisbasla == 0)
                    {
                        if (ConfigurationManager.AppSettings["BarkodPrint"].ToString() == "1")
                        {
                            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                            {
                                baslamaSonuc = FPUFuctions.Instance.PrintFreeFiscalText("Barkod No: " + s.barkod.ToString()).ErrorCode;
                                if (baslamaSonuc != 0 && belgeiptal != 0)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    hataLogYaz("Barkod Fişe Yazılamadı:{" + s.barkod.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }
                        }

                        decimal miktar = Convert.ToDecimal(s.miktar);

                        if (s.iptal == 1)
                        {
                            flag = "-";
                        }
                        else
                        {
                            flag = "";
                        }
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.Registeringsale(s.stkAd, flag, s.barkod, "", 1, fonksiyonlar.kdvBul(s.kdv, kdvTanimlari), 1, Convert.ToDecimal(s.fiyatS1), 0, miktar).ErrorCode;
                            LogYaz("Registeringsale(" + flag + ") (" + s.barkod.ToString() + "/" + Convert.ToDecimal(s.fiyatS1).ToString() + "/" + miktar.ToString() + ") Sonucu:" + baslamaSonuc.ToString());
                            if (baslamaSonuc != 0 && belgeiptal != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                hataLogYaz("Ürün Fişe Yazılamadı:{" + s.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                                fonksiyonlar.HataMesaji("2-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                        }
                    }
                }
            }

            if (kdvToplami0 != 0)
            {
                KdvListesi kdvList = new KdvListesi();
                kdvList.kdvYuzde = "00";
                kdvList.kdvTutari = kdvToplam0.ToString("n2");
                kdvList.kdvToplami = kdvToplami0.ToString("n2");
                kdvler.Add(kdvList);
            }
            if (kdvToplami1 != 0)
            {
                KdvListesi kdvList = new KdvListesi();
                kdvList.kdvYuzde = "01";
                kdvList.kdvTutari = kdvToplam1.ToString("n2");
                kdvList.kdvToplami = kdvToplami1.ToString("n2");
                kdvler.Add(kdvList);
            }
            if (kdvToplami2 != 0)
            {
                KdvListesi kdvList = new KdvListesi();
                kdvList.kdvYuzde = "08";
                kdvList.kdvTutari = kdvToplam2.ToString("n2");
                kdvList.kdvToplami = kdvToplami2.ToString("n2");
                kdvler.Add(kdvList);
            }
            if (kdvToplami3 != 0)
            {
                KdvListesi kdvList = new KdvListesi();
                kdvList.kdvYuzde = "18";
                kdvList.kdvTutari = kdvToplam3.ToString("n2");
                kdvList.kdvToplami = kdvToplami3.ToString("n2");
                kdvler.Add(kdvList);
            }

            yeniPosBelge2.Kdvler = kdvler;
            List<Odeme> odemeler = new List<Odeme>();
            List<Odeme2> odemeler2 = new List<Odeme2>();

            //foreach (sepet s in sepets)
            //{
            //    if (s.indirim > 0)
            //    {
            //        decimal ind = Convert.ToDecimal("-" + s.indirim);
            //        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
            //        {
            //            if (belge != 5)
            //            {
            //                FPUFuctions.Instance.Subtotal();

            //                baslamaSonuc = FPUFuctions.Instance.PreAndDiscount("D", ind).ErrorCode;
            //                if (baslamaSonuc != 0)
            //                {
            //                    HideMessage();
            //                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
            //                    fonksiyonlar.HataMesaji("3-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
            //                    return;

            //                }

            //            }

            //        }
            //    }
            //}
            double maliAraToplam = 0;

            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
            {
                maliAraToplam = FPUFuctions.Instance.Subtotal();
                LogYaz("Subtotal :" + maliAraToplam.ToString());
            }

            if (indirim < 0)
            {
                if (belge != 5)
                {
                    if (ConfigurationManager.AppSettings["Promosyon"].ToString() != "1")
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.PreAndDiscount("D", indirim).ErrorCode;
                            LogYaz("PreAndDiscount (" + indirim.ToString() + ")Sonuc:" + baslamaSonuc.ToString());
                            if (baslamaSonuc != 0 && belgeiptal != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                hataLogYaz("İndirim Yapılamadı:{indirim:" + indirim.ToString() + "}{maliaratoplam:" + maliAraToplam.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                                fonksiyonlar.HataMesaji("3-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                        }
                    }
                }
            }

            if (ConfigurationManager.AppSettings["Promosyon"].ToString() == "1" && this.belge != 5)
            {
                //var collP = db.GetCollection<sepet>("promosepet");
                //var sepetsP = collP.Find("{sindirim : {$gte : 0.01}}").Sort("{_id:1}").ToListAsync().Result;
                int aratoplam = 0;
                double promoToplam = 0;
                //foreach (sepet p in sepetsP)
                //{
                //    if (p.promoText != null)
                //    {
                //        if (aratoplam == 0)
                //        {
                //            FPUFuctions.Instance.Subtotal();
                //            FPUFuctions.Instance.PrintFreeFiscalText("- - - - - - - - - - - - - - - - - - - - - ");
                //            FPUFuctions.Instance.PrintFreeFiscalText("********** KAZANILAN İNDİRİMLER **********");
                //            aratoplam = 1;
                //        }
                //        promoToplam = promoToplam + p.sindirim;
                //        FPUFuctions.Instance.PrintFreeFiscalText("*" + p.promoText);
                //        FPUFuctions.Instance.PrintFreeFiscalText("*****Kazanç :        (" + p.sindirim.ToString("n2").PadLeft(10, ' ') + " TL)");
                //        //"-" + p.sindirim + " TL  = " +
                //    }

                //}

                //double promoToplam2 = 0;
                //string promoText2 = "";
                //sepetsP = collP.Find("{indirim : {$gte : 0.01}}").Sort("{_id:1}").ToListAsync().Result;
                //foreach (sepet p in sepetsP)
                //{
                //    if (p.promoText2 != null)
                //    {
                //        if (aratoplam == 0)
                //        {
                //            FPUFuctions.Instance.Subtotal();
                //            FPUFuctions.Instance.PrintFreeFiscalText("- - - - - - - - - - - - - - - - - - - - - ");
                //            FPUFuctions.Instance.PrintFreeFiscalText("********** KAZANILAN İNDİRİMLER **********");
                //            aratoplam = 1;
                //        }
                //        promoToplam2 = promoToplam2 + p.indirim;
                //        promoToplam = promoToplam + p.indirim;
                //        promoText2 = p.promoText2;
                //    }

                //}

                //if (promoToplam2 > 0)
                //{
                //    FPUFuctions.Instance.PrintFreeFiscalText("*" + promoText2);
                //    FPUFuctions.Instance.PrintFreeFiscalText("*****Kazanç :        (" + promoToplam2.ToString("n2").PadLeft(10, ' ') + " TL)");

                //}

                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                {
                    var collP3 = db.GetCollection<PromosyonToplamlari>("promosyonToplamlari");
                    var sepetsP3 = collP3.Find("{}").Sort("{_id:1}").ToListAsync().Result;
                    foreach (PromosyonToplamlari p3 in sepetsP3)
                    {
                        if (aratoplam == 0)
                        {
                            FPUFuctions.Instance.PrintFreeFiscalText("- - - - - - - - - - - - - - - - - - - - - ");
                            FPUFuctions.Instance.PrintFreeFiscalText("********** KAZANILAN İNDİRİMLER **********");
                            aratoplam = 1;
                        }
                        FPUFuctions.Instance.PrintFreeFiscalText("*" + p3.promosyonTex);
                        FPUFuctions.Instance.PrintFreeFiscalText("*****Kazanç :        (" + p3.promosyonTutar.ToString("n2").PadLeft(10, ' ') + " TL)");
                        promoToplam = promoToplam + p3.promosyonTutar;
                    }

                    if (indirim < 0)
                    {
                        promoToplam = promoToplam - Convert.ToDouble(indirim);
                        if (aratoplam == 1)
                        {
                            FPUFuctions.Instance.PrintFreeFiscalText("*****Belge İndirimi: (" + indirim.ToString("n2").PadLeft(10, ' ') + " TL)");
                        }
                    }
                    if (promoToplam != 0)
                    {
                        FPUFuctions.Instance.PrintFreeFiscalText("******************************************");
                        baslamaSonuc = FPUFuctions.Instance.PreAndDiscount("D", Convert.ToDecimal("-" + promoToplam)).ErrorCode;
                        LogYaz("PreAndDiscount (" + promoToplam.ToString() + ")Sonuc:" + baslamaSonuc.ToString());
                    }

                    if (aratoplam > 0)
                    {
                        maliAraToplam = FPUFuctions.Instance.Subtotal();
                        LogYaz("Subtotal (" + maliAraToplam.ToString() + ")");
                    }
                }
            }

            if (krediKarti != 0)
            {
                Odeme odeme = new Odeme();
                odeme.OdemeTip = 1;
                odeme.OdemeTutar = krediKarti;
                odeme.OdemeDetay = this.banka;
                odemeler.Add(odeme);
                if (belge != 0)
                {
                    Odeme2 odeme2 = new Odeme2();
                    odeme2.OdemeTip = 1;
                    odeme2.OdemeTutar = krediKarti.ToString("n2");
                    odeme2.OdemeDetay = "Kredi Kartı";
                    odemeler2.Add(odeme2);
                }
                disdosya = disdosya + "\r\n03 " + "1     " + "1 " + "01" + nakit.ToString("n2").PadRight(15, ' ') + "0              " + "                        " + "0";
                satirSayisi = satirSayisi + 1;

                sira2 = sira2 + 1;
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "PY" + "0" + "XX" + "01" + krediKarti.ToString("n2").Replace(',', '.').PadLeft(15, '0') + "0" + "0.00".ToString().PadLeft(15, '0') + " ".ToString().PadLeft(24, ' ') + "0000" + " ".ToString().PadLeft(40, ' ') + "99" + "X".ToString().PadLeft(15, 'X');

                if (belge != 5)
                {
                    if (belgeiptal == 0)
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.TotalCalculating(1, Convert.ToDecimal((krediKarti))).ErrorCode;
                            LogYaz("TotalCalculating (krediKartı:" + krediKarti.ToString() + ") Sonuc:" + baslamaSonuc.ToString());
                            if (baslamaSonuc != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                hataLogYaz("Ödeme Girilemedi:{krediKarti:" + krediKarti.ToString() + "}{maliaratoplam:" + maliAraToplam.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                                fonksiyonlar.HataMesaji("4-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                        }
                    }
                }
            }

            if (yemekCeki != 0)
            {
                Odeme odeme = new Odeme();
                odeme.OdemeTip = 2;
                odeme.OdemeTutar = yemekCekiTutari;
                odeme.OdemeDetay = this.yemekceki;
                odemeler.Add(odeme);

                if (belge != 0)
                {
                    Odeme2 odeme2 = new Odeme2();
                    odeme2.OdemeTip = 2;
                    odeme2.OdemeTutar = yemekCekiTutari.ToString("n2");
                    odeme2.OdemeDetay = "Diğer";
                    odemeler2.Add(odeme2);
                    disdosya = disdosya + "\r\n03 " + "1     " + "2 " + "01" + yemekCekiTutari.ToString("n2").PadRight(15, ' ') + "00                                             ";
                    satirSayisi = satirSayisi + 1;
                }

                if (belge != 5)
                {
                    if (belgeiptal == 0)
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.TotalCalculating(3, Convert.ToDecimal((yemekCekiTutari))).ErrorCode;
                            LogYaz("TotalCalculating (krediKartı:" + yemekCekiTutari.ToString() + ") Sonuc:" + baslamaSonuc.ToString());
                            if (baslamaSonuc != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                hataLogYaz("Ödeme Girilemedi:{yemekCekiTutari:" + yemekCekiTutari.ToString() + "}{maliaratoplam:" + maliAraToplam.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                                fonksiyonlar.HataMesaji("5-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                        }
                    }
                }
            }

            if (nakit != 0)
            {
                Odeme odeme = new Odeme();
                odeme.OdemeTip = 0;
                if (kalan != 0)
                {
                    odeme.OdemeTutar = nakit - kalan;
                }
                else
                {
                    odeme.OdemeTutar = nakit;
                }

                odeme.OdemeDetay = 0;
                odemeler.Add(odeme);

                Odeme2 odeme2 = new Odeme2();
                odeme2.OdemeTip = 0;
                if (kalan != 0)
                {
                    odeme2.OdemeTutar = (nakit - kalan).ToString("n2");
                }
                else
                {
                    odeme2.OdemeTutar = nakit.ToString("n2");
                }
                odeme2.OdemeDetay = "Nakit";
                odemeler2.Add(odeme2);

                disdosya = disdosya + "\r\n03 " + "1     " + "0 " + "01" + nakit.ToString("n2").PadRight(15, ' ') + "0              " + "                        " + "0";
                satirSayisi = satirSayisi + 1;

                sira2 = sira2 + 1;
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "PY" + "0" + "XX" + "00" + nakit.ToString("n2").Replace(',', '.').PadLeft(15, '0') + "0" + "0.00".ToString().PadLeft(15, '0') + " ".ToString().PadLeft(24, ' ') + "0000" + " ".ToString().PadLeft(40, ' ') + "99" + "X".ToString().PadLeft(15, 'X');

                if (belge != 5)
                {
                    if (belgeiptal == 0)
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.TotalCalculating(0, Convert.ToDecimal((nakit))).ErrorCode;
                            LogYaz("TotalCalculating (nakit:" + nakit.ToString() + ") Sonuc:" + baslamaSonuc.ToString());
                            if (baslamaSonuc != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                hataLogYaz("Ödeme Girilemedi:{nakit:" + nakit.ToString() + "}{maliaratoplam:" + maliAraToplam.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                                fonksiyonlar.HataMesaji("6-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                        }
                    }
                }
            }

            if (yemekCekiTutari > 0)
            {
                if (simpleButton59.Text.IndexOf("(") > 0)
                {
                    string[] tips = simpleButton59.Text.Split('(');
                    if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                    {
                        FPUFuctions.Instance.PrintFreeFiscalText(tips[0].Trim());
                    }
                }
            }

            if (belge != 5)
            {
                if (belgeiptal == 0)
                {
                    if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                    {
                        baslamaSonuc = FPUFuctions.Instance.CloseFiscalReceipt().ErrorCode;
                        LogYaz("CloseFiscalReceipt Sonuc:" + baslamaSonuc.ToString());
                        if (baslamaSonuc != 0)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            hataLogYaz("Belge Kapatılamadı:{maliaratoplam:" + maliAraToplam.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());

                            baslamaSonuc = FPUFuctions.Instance.TotalCalculating(0, Convert.ToDecimal((0.05))).ErrorCode;
                            LogYaz("TotalCalculating (nakit:0.05) Sonuc:" + baslamaSonuc.ToString());

                            baslamaSonuc = FPUFuctions.Instance.CloseFiscalReceipt().ErrorCode;
                            errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            hataLogYaz("Belge Kapatılamadı:{maliaratoplam:" + maliAraToplam.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());

                            if (baslamaSonuc != 0)
                            {
                                fonksiyonlar.HataMesaji("Belge Kapatılamadı!", errD.errorMessage.ToString(), 2);

                                baslamaSonuc = FPUFuctions.Instance.SpecilCancelFiscalReceipt().ErrorCode;
                                LogYaz("SpecilCancelFiscalReceipt Sonucu:" + baslamaSonuc.ToString());

                                fisbasla = 0;
                                return;
                            }
                        }
                    }
                }
            }
            if (belge == 5)
            {
                if (belgeiptal == 0)
                {
                    if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                    {
                        baslamaSonuc = FPUFuctions.Instance.ExpenseslipInfo(1, 1, toplam, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00).ErrorCode;
                        LogYaz("ExpenseslipInfo Sonuc:" + baslamaSonuc.ToString());
                        if (baslamaSonuc != 0)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            hataLogYaz("Gider Pusulası Yazılamadı:{maliaratoplam:" + maliAraToplam.ToString() + "}{nakit:" + nakit.ToString() + "}{kredikarti:" + krediKarti.ToString() + "}{yemekCeki:" + yemekCeki.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                            fonksiyonlar.HataMesaji("8-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                            return;
                        }
                    }
                }
            }
            if (belgeiptal == 1)
            {
                if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                {
                    baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                    LogYaz("CancelFiscalReceipt Sonuc:" + baslamaSonuc.ToString());
                    if (baslamaSonuc != 0)
                    {
                        baslamaSonuc = FPUFuctions.Instance.SpecilCancelFiscalReceipt().ErrorCode;
                        LogYaz("SpecilCancelFiscalReceipt Sonuc:" + baslamaSonuc.ToString());
                        if (baslamaSonuc != 0)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            hataLogYaz("Belge İptal Edilemedi:{maliaratoplam:" + maliAraToplam.ToString() + "}{nakit:" + nakit.ToString() + "}{kredikarti:" + krediKarti.ToString() + "}{yemekCeki:" + yemekCeki.ToString() + "}" + errD.errorCode + ":" + errD.errorMessage.ToString());
                            fonksiyonlar.HataMesaji("9-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                            return;
                        }
                    }
                }
            }

            yeniPosBelge.Toplam = Convert.ToDouble((toplam + Convert.ToDouble(indirim)).ToString("n2"));
            yeniPosBelge.ToplamInd = Convert.ToDouble(Math.Abs(indirim));
            yeniPosBelge.BelgeAyr = ayrintilar.ToList<Belgeayr>();
            yeniPosBelge.Odeme = odemeler.ToList<Odeme>();

            if (belge != 0 && belge != 10)
            {
                yeniPosBelge2.Toplam = toplam.ToString("n2");
                yeniPosBelge2.ToplamYazi = fonksiyonlar.yaziyaCevir(toplam);
                yeniPosBelge2.ToplamInd = Convert.ToDouble(Math.Abs(indirim)).ToString("n2");
                yeniPosBelge2.BelgeAyr = ayrintilar2.ToList<Belgeayr2>();
                yeniPosBelge2.Odeme = odemeler2.ToList<Odeme2>();
            }

            if (belge != 0 && belge != 10)
            {
                musteri2 musteriM = new musteri2();
                if (belge == 1)
                {
                    var clientM = new MongoClient();
                    var dbM = clientM.GetDatabase("Olivetti");
                    var collectionM = dbM.GetCollection<musteri>("musteri");
                    var musteriDetay = collectionM.Find("{'vergiNumarasi':'" + this.musteriVNo + "'}").Limit(1).ToListAsync().Result;
                    foreach (musteri m in musteriDetay)
                    {
                        musteriM.adres1 = m.adres1;
                        musteriM.adres2 = m.adres2;
                        musteriM.adres3 = m.adres3;
                        musteriM.il = m.il;
                        musteriM.postaKodu = m.postaKodu;
                        musteriM.telefon = m.telefon;
                        musteriM.unVan = m.unVan;
                        musteriM.vergiDairesi = m.vergiDairesi;
                        musteriM.vergiNumarasi = m.vergiNumarasi;
                    }
                }
                else
                {
                    musteriM.adres1 = "";
                    musteriM.adres2 = "";
                    musteriM.adres3 = "";
                    musteriM.il = "";
                    musteriM.postaKodu = "";
                    musteriM.telefon = "";
                    musteriM.unVan = this.musteriUnvan;
                    musteriM.vergiDairesi = "";
                    musteriM.vergiNumarasi = this.musteriVNo;
                }

                yeniPosBelge2.musteriDetay = musteriM;
            }

            collection.InsertOne(yeniPosBelge);

            if (kalan != 0)
            {
                nakit = nakit - kalan;
            }

            disdosya = disdosya + "\r\n05 110    " + indirim.ToString("n2").PadRight(15, ' ') + "                        " + "                        " + "0   0";
            satirSayisi = satirSayisi + 1;
            disdosya = disdosya + "\r\n05 112    " + "               " + "                        " + "                        " + "0   0";
            satirSayisi = satirSayisi + 1;
            disdosya = disdosya + "\r\n06 " + kdvToplami0.ToString("n2").PadRight(15, ' ') + kdvToplami1.ToString("n2").PadRight(15, ' ') + kdvToplami2.ToString("n2").PadRight(15, ' ') + kdvToplami3.ToString("n2").PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + kdvToplam0.ToString("n2").PadRight(15, ' ') + kdvToplam1.ToString("n2").PadRight(15, ' ') + kdvToplam2.ToString("n2").PadRight(15, ' ') + kdvToplam3.ToString("n2").PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ') + "0".ToString().PadRight(15, ' ');
            satirSayisi = satirSayisi + 1;

            if (indirim < 0)
            {
                satirSayisi = satirSayisi + 2;
                sira2 = sira2 + 1;
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "RN" + "0" + "XX" + "1000002" + indirim.ToString("n2").Replace(',', '.').Replace('-', '0').PadLeft(15, '0') + " ".ToString().PadLeft(24, ' ') + " ".ToString().PadLeft(24, ' ') + "0000" + "XXXX".ToString().PadLeft(44, ' ');
            }

            if (satirIndirimi > 0)
            {
                satirSayisi = satirSayisi + 2;
                sira2 = sira2 + 1;
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "RN" + "0" + "XX" + "1000002" + satirIndirimi.ToString("n2").Replace(',', '.').Replace('-', '0').PadLeft(15, '0') + " ".ToString().PadLeft(24, ' ') + " ".ToString().PadLeft(24, ' ') + "0000" + "XXXX".ToString().PadLeft(44, ' ');
            }

            sira2 = sira2 + 1;
            disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "RN" + "0" + "XX" + "1000010" + "0".ToString().PadLeft(15, '0') + " ".ToString().PadLeft(24, ' ') + " ".ToString().PadLeft(24, ' ') + "0000" + "XXXX".ToString().PadLeft(44, ' ');

            sira2 = sira2 + 1;
            disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "RN" + "0" + "XX" + "1000012" + "0".ToString().PadLeft(15, '0') + " ".ToString().PadLeft(24, ' ') + " ".ToString().PadLeft(24, ' ') + "0000" + "3D15460085" + "XXXX".ToString().PadLeft(34, ' ');

            sira2 = sira2 + 1;
            if (indirim < 0)
            {
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "VF" + "0" + "XX" + "0" + kdvIndirimToplam0.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvIndirimToplam1.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvIndirimToplam2.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvIndirimToplam3.ToString("n2").Replace(",", ".").PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "X".ToString().PadRight(42, 'X');
            }
            else
            {
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "VF" + "0" + "XX" + "0" + kdvToplam0.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvToplam1.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvToplam2.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvToplam3.ToString("n2").Replace(",", ".").PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "X".ToString().PadRight(42, 'X');
            }

            sira2 = sira2 + 1;
            if (indirim < 0 || satirIndirimi > 0)
            {
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "DF" + "0" + "XX" + "0" + IndirimToplam0.ToString("n2").Replace(",", ".").PadLeft(15, '0') + IndirimToplam1.ToString("n2").Replace(",", ".").PadLeft(15, '0') + IndirimToplam2.ToString("n2").Replace(",", ".").PadLeft(15, '0') + IndirimToplam3.ToString("n2").Replace(",", ".").PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "X".ToString().PadRight(42, 'X');
            }
            else
            {
                disdosya3 = disdosya3 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + sira2.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "DF" + "0" + "XX" + "0" + kdvToplami0.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvToplami1.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvToplami2.ToString("n2").Replace(",", ".").PadLeft(15, '0') + kdvToplami3.ToString("n2").Replace(",", ".").PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "X".ToString().PadRight(42, 'X');
            }

            double belgeKdv = kdvToplam0 + kdvToplam1 + kdvToplam2 + kdvToplam3;

            yeniPosBelge2.ToplamKdv = belgeKdv.ToString("n2");

            disdosya = "01 1     " + String.Format("{0:d}", dt).Replace(".", "/").PadRight(14, ' ') + fonksiyonlar.FisNo().ToString().PadRight(12, ' ') + String.Format("{0:HH:mm:ss}", dt).Replace(":", "") + "      1     1       " + this.belge + this.belgeiptal + evrakNo.ToString().PadRight(10, ' ') + "0     " + "0     " + satirSayisi.ToString().PadRight(6, ' ') + toplam.ToString("n2").PadRight(15, ' ') + belgeKdv.ToString("n2").PadRight(15, ' ') + indirim.ToString("n2").PadRight(15, ' ') + "0              " + "0              " + "0              " + "0              " + "0              " + "                        " + "0" + "                        " + disdosya;
            int siraBaslangic = 0;
            siraBaslangic = siraBaslangic + 1;
            disdosya2 = yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + "0".ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "RH" + belgeiptal + "XX" + "1".ToString().PadRight(8, ' ') + dt.ToString("yyyyMMddHHmmss") + dt.ToString("yyyyMMddHHmmss");
            if (evrakNo != "")
            {
                disdosya2 = disdosya2 + evrakNo.ToString().PadLeft(10, ' ');
            }
            else
            {
                disdosya2 = disdosya2 + yeniPosBelge.FisNo.ToString().PadLeft(10, ' ');
            }
            int belgeTipiLog = 0;
            switch (belge)
            {
                case 5:
                    belgeTipiLog = 2;
                    break;

                case 2:
                    belgeTipiLog = 4;
                    break;

                case 3:
                    belgeTipiLog = 4;
                    break;

                case 4:
                    belgeTipiLog = 1;
                    break;

                case 6:
                    belgeTipiLog = 4;
                    break;

                default:
                    belgeTipiLog = belge;
                    break;
            }

            double logToplam = toplam + double.Parse(indirim.ToString());
            logToplam = logToplam - satirIndirimi;
            double kdvToplami = 0;

            kdvToplami = belgeKdv;

            disdosya2 = disdosya2 + belgeTipiLog + satirSayisi.ToString().PadLeft(4, '0') + logToplam.ToString("n2").Replace(',', '.').PadLeft(15, '0') + kdvToplami.ToString("n2").Replace(',', '.').PadLeft(15, '0') + " ".ToString().PadRight(24, ' ');

            if (indirim < 0 || satirIndirimi > 0)
            {
                disdosya2 = disdosya2 + "1";
            }
            else
            {
                disdosya2 = disdosya2 + "0";
            }
            disdosya2 = disdosya2 + "0" + "1" + "0001" + "XXXXXX";
            if (indirim < 0)
            {
                disdosya2 = disdosya2 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + siraBaslangic.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "TD" + belgeiptal + "XX" + indirim.ToString("n2").Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + indirim.ToString("n2").Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0".ToString().PadRight(28, 'X');
                siraBaslangic = siraBaslangic + 1;
                satirSayisi = satirSayisi + 1;
            }

            if (satirIndirimi > 0)
            {
                disdosya2 = disdosya2 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + siraBaslangic.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "TD" + belgeiptal + "XX" + "0.00".ToString().Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0.00".ToString().Replace('-', '0').Replace(',', '.').PadLeft(15, '0') + satirIndirimi.ToString("n2").Replace(',', '.').PadLeft(15, '0') + "0.00".ToString().PadLeft(15, '0') + "0".ToString().PadRight(28, 'X');
                siraBaslangic = siraBaslangic + 1;
                satirSayisi = satirSayisi + 1;
            }

            disdosya2 = disdosya2 + "\r\n" + yeniPosBelge.FisNo.ToString().PadLeft(6, '0') + siraBaslangic.ToString().PadLeft(4, '0') + ayar.kasaNumarasi.ToString() + ayar.magazaNumarasi.ToString() + "RE" + belgeiptal + "XX" + "".ToString().PadRight(40, ' ') + "1".ToString().PadLeft(10, ' ') + yeniPosBelge.FisNo.ToString().PadRight(44, ' ') + "X".ToString().PadRight(24, 'X');

            disdosya2 = disdosya2 + disdosya3;
            //System.IO.StreamWriter dosyaDis = new System.IO.StreamWriter("disDosya.txt", true);
            //dosyaDis.WriteLine(disdosya);
            //dosyaDis.Close();

            //var serverAdres = ConfigurationManager.AppSettings["Server"].ToString();

            //var serverPatch = ConfigurationManager.AppSettings["ServerPatch"].ToString();

            //System.IO.StreamWriter dosyaDis2 = new System.IO.StreamWriter(@"\\" + serverAdres + "\\" + serverPatch + "\\GNETR" + ayar.kasaNumarasi.ToString() + ".log", true);
            //dosyaDis2.WriteLine(disdosya2);
            //dosyaDis2.Close();

            try
            {
                System.IO.StreamWriter dosyaDis2 = new System.IO.StreamWriter(@"D:\\App3\\Log\\GNETR" + ayar.kasaNumarasi.ToString() + ".log", true);
                dosyaDis2.WriteLine(disdosya2);
                dosyaDis2.Close();
            }
            catch (Exception ex)
            {
                fonksiyonlar.HataMesaji("Log Yazılamadı", ex.Message, 1);
                hataLogYaz("Log Yazılamadı:{" + disdosya2 + "}" + maliAraToplam.ToString() + "}{nakit:" + nakit.ToString() + "}{kredikarti:" + krediKarti.ToString() + "}{yemekCeki:" + yemekCeki.ToString() + "}" + ex.Message.ToString());
            }

            if (belgeiptal == 0)
            {
                if (belge != 0 && belge != 10)
                {
                    string raporMrt = "fatura.mrt";
                    switch (belge)
                    {
                        case 1:
                            raporMrt = "fatura.mrt";
                            break;

                        case 2:
                            raporMrt = "efatura.mrt";
                            break;

                        case 3:
                            raporMrt = "earsiv.mrt";
                            break;

                        case 4:
                            raporMrt = "toptanfatura.mrt";
                            break;

                        case 5:
                            raporMrt = "iade.mrt";
                            break;
                    }
                    XmlDocument doc = (XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(yeniPosBelge2.ToJson(), "PosBelge");

                    System.IO.StreamWriter dosya = new System.IO.StreamWriter(@"D:\App3\Log\fatura.xml");
                    dosya.Write(doc.OuterXml);
                    dosya.Close();

                    var report = new Stimulsoft.Report.StiReport();
                    report.Load(raporMrt);
                    var database = new Stimulsoft.Report.Dictionary.StiXmlDatabase("Fatura", @"D:\App3\Log\fatura.xml", @"D:\App3\Log\fatura.xml");
                    report.Dictionary.Databases.Add(database);
                    report.PrinterSettings.ShowDialog = false;
                    report.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["InvoicePrinter"].ToString();
                    report.Print();
                    yaziciDuzelt();
                }
            }

            //collection2.InsertOne(yeniPosBelge2);
            fonksiyonlar.FisNoArttirSifirla(0);

            if (belge == 5)
            {
                //burdayız
                fonksiyonlar.IadeArttir(yeniPosBelge.Toplam);
            }

            if (yemekCeki == 0)
            {
                sepetSil();
            }
            else
            {
                sepetSil2();
            }

            if (ConfigurationManager.AppSettings["Promosyon"].ToString() == "1")
            {
                db.DropCollection("promosepet");
                db.DropCollection("promosyonToplamlari");
            }

            labelControl41.Text = "Satır sayısı:0";
            labelControl5.Text = labelControl5.Text = "Tutar = 0,00";
            textEdit3.Text = "0";
            textEdit4.Text = "0";
            textEdit5.Text = "0";
            textEdit6.Text = "0";
            textEdit7.Text = "0";
            textEdit8.Text = "0";
            textEdit9.Text = "0";
            textEdit10.Text = "0";
            textEdit12.Text = "0";
            textEdit23.Text = "0";
            this.belge = 0;
            this.evrakNo = "";
            this.musteriVNo = "11111111111";
            labelControl12.Text = simpleButton23.Text;
            textEdit13.Text = "";
            textEdit14.Text = "";
            labelControl23.Text = "";
            simpleButton85.Visible = false;
            labelControl36.Text = "";
            this.musteriUnvan = "";
            fcs = 0;
            simpleButton58.Text = bankaText;
            simpleButton59.Text = yemekCekiText;
            textEdit7.Enabled = true;
            simpleButton59.Enabled = true;
            fisbasla = 0;
            if (belgeiptal == 0)
            {
                //if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
                //{
                //    if (ConfigurationManager.AppSettings["AutoOpenFis"].ToString() == "1")
                //    {
                //        baslamaSonuc =  FPUFuctions.Instance.OpenFiscalReceipt(int.Parse(kasiyerTanm.kasiyerkodu), -1, kasiyerTanm.kasiyerSifre, "", "").ErrorCode;
                //        if (baslamaSonuc != 0)
                //        {
                //            HideMessage();
                //            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                //            fonksiyonlar.HataMesaji("10-İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                //            return;
                //        }
                //        fisbasla = 1;
                //    }
                //}
            }

            this.belgeiptal = 0;

            simpleButton16.Enabled = true;
            groupControl8.Visible = false;
            simpleButton58.Text = bankaText;
            textEdit1.Select();
            fcs = 0;

            if (yemekCeki > 0)
            {
                sepetOku2();
            }

            HideMessage();

            // MesajKontrol();

            if (kasaMesaj != 0)
            {
                if (kasaMesaj == 1)
                {
                    ShowMessage("Lütfen bekleyin", "Ürün listesi alınıyor");
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.CreateNoWindow = true;
                    procInfo.FileName = @"D:\\App3\\import.bat";  //The file in that DIR.
                    procInfo.WorkingDirectory = @""; //The working DIR.
                    var process = new Process();
                    process.StartInfo = procInfo;
                    process.Start();
                    process.WaitForExit();
                    HideMessage();
                    fonksiyonlar.MesajGoster("Güncelleme", "Ürün Listesi Güncellendi", 2);
                    kasaMesaj = 0;
                }

                if (kasaMesaj == 2)
                {
                    ShowMessage("Lütfen bekleyin", "Cari listesi alınıyor");
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.CreateNoWindow = true;
                    procInfo.FileName = @"D:\\App3\\import2.bat";  //The file in that DIR.
                    procInfo.WorkingDirectory = @""; //The working DIR.
                    var process = new Process();
                    process.StartInfo = procInfo;
                    process.Start();
                    process.WaitForExit();
                    HideMessage();
                    fonksiyonlar.MesajGoster("Güncelleme", "Cari Listesi Güncellendi", 2);
                    kasaMesaj = 0;
                }

                if (kasaMesaj == 5)
                {
                    ShowMessage("Lütfen bekleyin", "Promosyon bilgileri alınıyor");
                    var clientB = new MongoClient(ConfigurationManager.ConnectionStrings["mongoDBConnectionString2"].ToString());
                    var dbB = clientB.GetDatabase("backOffice");
                    var collB = dbB.GetCollection<Promo>("promo");
                    var collP = db.GetCollection<Promo>("promo");
                    var collM = dbB.GetCollection<MagazaMesaj>("magazamesaj");
                    var sepetsB = collB.Find("{}").ToListAsync().Result;
                    var sepetsM = collM.Find("{'magazaKodu':" + int.Parse(ayar.magazaNumarasi) + ",'mesajDurumu':0}").ToListAsync().Result;
                    db.DropCollection("promo");
                    foreach (Promo msj in sepetsB)
                    {
                        collP.InsertOne(msj);
                    }

                    tm = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    foreach (MagazaMesaj msj2 in sepetsM)
                    {
                        var update = Builders<MagazaMesaj>.Update
                        .Set("mesajDurumu", 1)
                            .Set("cevapTarihi", tm);
                        collM.UpdateOne("{'_id':ObjectId('" + msj2._id + "')}", update);
                    }
                    HideMessage();
                    fonksiyonlar.MesajGoster("Güncelleme", "Promosyon bilgileri Güncellendi", 2);
                    kasaMesaj = 0;
                }
            }

            fonksiyonlar.MesajGoster("İşlem Tamam", "", 1);

            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
            {
                FPUFuctions.Instance.WriteToDisplay("TESEKKUR EDERIZ", "YINE BEKLERIZ");
            }

            belge = 0;
            groupControl13.Visible = false;
        }

        private void MesajKontrol()
        {
            label1.Text = "Mesaj kontrol";
            if (labelControl37.Text == "ON")
            {
                if (ayar.backOffice == "Genius")
                {
                    var serverAdres = ConfigurationManager.AppSettings["Server"].ToString();
                    var serverPatch = ConfigurationManager.AppSettings["ServerPatch"].ToString();
                    var serverPatch2 = ConfigurationManager.AppSettings["ServerPatch2"].ToString();
                    var serverPatch3 = ConfigurationManager.AppSettings["ServerPatch3"].ToString();

                    if (serverAdres != "" && serverPatch != "" && serverPatch2 != "")
                    {
                        if (System.IO.File.Exists("\\\\" + serverAdres + "\\" + serverPatch2 + "\\" + ayar.kasaNumarasi + "\\Msg.SBS") == true)
                        {
                            string text = System.IO.File.ReadAllText("\\\\" + serverAdres + "\\" + serverPatch2 + "\\" + ayar.kasaNumarasi + "\\Msg.SBS");
                            if (text != "")
                            {
                                if (text.Trim().ToString() == "1,Stk")
                                {
                                    System.IO.File.Copy("\\\\" + serverAdres + "\\" + serverPatch3 + "\\URUN.SBS", Application.StartupPath + "\\URUN.SBS", true);
                                    using (StreamWriter writer = new StreamWriter("\\\\" + serverAdres + "\\" + serverPatch2 + "\\" + ayar.kasaNumarasi + "\\Msg.SBS"))
                                    {
                                        writer.Write("3,Stk");
                                    }
                                    kasaMesaj = 1;
                                }

                                if (text.Trim().ToString() == "1,Musteri")
                                {
                                    System.IO.File.Copy("\\\\" + serverAdres + "\\" + serverPatch3 + "\\MUSTERI.SBS", Application.StartupPath + "\\MUSTERI.SBS", true);
                                    using (StreamWriter writer = new StreamWriter("\\\\" + serverAdres + "\\" + serverPatch2 + "\\" + ayar.kasaNumarasi + "\\Msg.SBS"))
                                    {
                                        writer.Write("3,Musteri");
                                    }
                                    kasaMesaj = 2;
                                }
                            }
                        }
                    }

                    if (kasaMesaj == 0)
                    {
                        //promosyon mesaj kontrol
                        if (ConfigurationManager.AppSettings["Promosyon"].ToString() == "1")
                        {
                            try
                            {
                                var client = new MongoClient(ConfigurationManager.ConnectionStrings["mongoDBConnectionString2"].ToString());
                                var db = client.GetDatabase("backOffice");
                                var coll = db.GetCollection<MagazaMesaj>("magazamesaj");
                                var sepets = coll.Find("{'magazaKodu':" + int.Parse(ayar.magazaNumarasi) + ",'mesajDurumu':0}").ToListAsync().Result;
                                foreach (MagazaMesaj msj in sepets)
                                {
                                    if (msj.mesajTuru == 5)
                                    {
                                        kasaMesaj = 5;
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                if (ayar.backOffice == "Optimist")
                {
                    try
                    {
                        var mesajSonuc = service.MesajKontrol(int.Parse(ayar.kasaNumarasi));
                        if (mesajSonuc.mesajId == 0)
                        {
                            label1.Text = "Mesaj yok";
                        }
                        else
                        {
                            label1.Text = "Mesaj var : " + mesajSonuc.mesajAciklama;

                            if (mesajSonuc.mesajTur == 1)
                            {
                                string dosyaAdi = int.Parse(ayar.kasaNumarasi) + ".SBS.zip";
                                string Url = service.Endpoint.Address.ToString();
                                string[] UrlSpl = Url.Split('/');
                                var dosyaLink = Url.Replace(UrlSpl[3], "UrunListe/" + dosyaAdi);
                                label1.Text = "Dosya alınıyor : " + dosyaAdi;
                                WebClient webClient = new WebClient();
                                webClient.DownloadFile(dosyaLink, @"D:\App3\URUN.SBS.zip");
                                FileInfo info = new FileInfo(@"D:\App3\URUN.SBS.zip");
                                Decompress(info);
                                kasaMesaj = 1;
                                label1.Text = "Dosya alındı : " + dosyaAdi;
                                service.MesajGuncelle(int.Parse(ayar.kasaNumarasi), mesajSonuc.mesajId, 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        label1.Text = ex.Message;
                    }
                }
            }
        }

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        private void yaziciDuzelt()
        {
            string s = "";
            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
            {
                e1.Graphics.DrawString(s, new Font("Times New Roman", 12), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
            };
            try
            {
                p.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["SlipPrinter"].ToString();
                p.Print();
            }
            catch (Exception ex)
            {
            }
        }

        private void BelgeTipiSec(string tip, int tipid)
        {
            this.musteriUnvan = "Perakende Müşteri";
            this.musteriVNo = "11111111111";
            labelControl36.Text = this.musteriUnvan;

            if (tip.IndexOf("(") > 0)
            {
                string[] tips = tip.Split('(');
                tip = tips[0].Trim();

                simpleButton16.Enabled = true;
                simpleButton17.Enabled = true;
                simpleButton18.Enabled = true;
                simpleButton19.Enabled = true;

                if (fisbasla == 1)
                {
                    if (this.belge != tipid)
                    {
                        if (fonksiyonlar.OnayGoster("Belge Değişimi", "Belge tipi değişmesi için varolan fiş iptal edilecek. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                        {
                            this.belgeiptal = 1;
                            satisTamamla();
                        }
                        else
                        {
                            textEdit1.Select();
                            return;
                        }
                    }
                }

                if (tip == "Fatura" || tip == "Toptan Fatura")
                {
                    if (ayar.eArsivAktif == 1)
                    {
                        this.belge = 0;
                        labelControl12.Text = "Fiş";
                        labelControl36.Text = "";
                        this.evrakNo = "";
                        this.musteriUnvan = "";
                        fonksiyonlar.HataMesaji("Fatura Ekranı Açılamadı", "E-Arşiv mükellefisiniz. Normal Fatura Düzenleyemezsiniz.", 2);
                    }
                    else
                    {
                        groupControl14.Visible = true;
                        fcs = 8;
                        textEdit13.Select();
                    }
                }
                if (tip == "E-Fatura" || tip == "Toptan E-Fatura")
                {
                    fcs = 10;
                    textEdit15.Select();
                    groupControl15.Visible = true;
                    simpleButton88.Visible = false;
                }
                if (tip == "E-Arşiv")
                {
                    if (ayar.eArsivAktif == 0)
                    {
                        this.belge = 0;
                        labelControl12.Text = "Fiş";
                        fonksiyonlar.HataMesaji("Fatura Ekranı Açılamadı", "E-Arşiv mükellefi değilsiniz. E-Arşiv Fatura Düzenleyemezsiniz.", 2);
                    }
                    else
                    {
                        fcs = 11;
                        textEdit16.Select();
                        groupControl17.Visible = true;
                    }
                }

                this.belge = tipid;
                labelControl12.Text = tip;
                groupControl4.Visible = true;
                groupControl6.Visible = false;
                textEdit1.Select();
            }
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton35_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton32_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton29_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton28_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton31_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton34_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton21_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton33_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton30_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton27_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip == "")
            {
            }
            else
            {
                BelgeTipiSec(button.Text, int.Parse(button.ToolTip));
            }
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            if (groupControl18.Visible == true)
            {
                simpleButton16.Enabled = true;
                groupControl18.Visible = false;
                textEdit1.Select();
                fcs = 0;
            }
            else
            {
                simpleButton16.Enabled = false;
                groupControl18.Visible = true;
                textEdit17.Select();
                fcs = 13;
            }
            simpleButton16.Enabled = false;
            simpleButton17.Enabled = false;
            simpleButton18.Enabled = false;
            simpleButton19.Enabled = false;
        }

        private void textEdit9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton55.PerformClick();
            }

            if (e.KeyCode == Keys.Escape)
            {
                textEdit9.Text = "0";
                simpleButton55.PerformClick();
            }
        }

        private void textEdit10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton55.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                textEdit10.Text = "0";
                simpleButton55.PerformClick();
            }
        }

        private void textEdit17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                simpleButton95.PerformClick();
            }
            if (e.KeyCode == Keys.Enter)
            {
                string sonUrun = "";
                if (simpleButton96.Visible == true)
                {
                    sonUrun = memoEdit2.Text;
                }
                int sonuc = 0;
                var client = new MongoClient();
                var db = client.GetDatabase("Olivetti");
                var coll = db.GetCollection<stk>("stk");
                string sorgu = "{}";
                if (textEdit17.Text != "")
                {
                    string barkod = textEdit17.Text;

                    if (barkod.Length > 2)
                    {
                        if (barkod.Substring(0, 2) == "26" || barkod.Substring(0, 2) == "28" || barkod.Substring(0, 2) == "29")
                        {
                            if (barkod.Length > 7)
                            {
                                barkod = barkod.Substring(0, 7);
                            }
                        }
                    }

                    sorgu = "{'Barkod.barkod':'" + barkod + "'}";

                    var sepets = coll.Find(sorgu).Limit(1).ToListAsync().Result;

                    foreach (stk s in sepets)
                    {
                        memoEdit2.Text = s.stkAd;
                        labelControl32.Text = s.fiyatS1.ToString("n2");
                        labelControl33.Text = s.fiyatS2.ToString("n2");
                        labelControl34.Text = s.fiyatS3.ToString("n2");
                        sonuc = 1;
                        simpleButton96.Visible = true;
                        textEdit17.SelectAll();
                    }

                    if (sonuc == 0)
                    {
                        memoEdit2.Text = "Ürün Bulunamadı";
                        labelControl32.Text = "0,00";
                        labelControl33.Text = "0,00";
                        labelControl34.Text = "0,00";
                        simpleButton96.Visible = false;
                    }
                    else
                    {
                        if (memoEdit2.Text == sonUrun)
                        {
                            simpleButton96.PerformClick();
                            return;
                        }
                    }
                }
            }
        }

        private void simpleButton40_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fonksiyonlar.OnayGoster("Kasiyer Çıkış Onayı", "Kasiyer Çıkışı Yapılacak. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                {
                    if (fisbasla == 1)
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                            if (baslamaSonuc != 0)
                            {
                                HideMessage();
                                var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                return;
                            }
                            fisbasla = 0;
                        }
                    }
                    this.Close();
                    FPUFuctions.Instance.WriteToDisplay("        KASA","       KAPALI");
                }
            }
            else
            {
                fonksiyonlar.HataMesaji("Çıkış Yapılamadı", "Açık belge varken çıkış yapamazsınız. Önce belgeyi bitirmelisiniz.", 2);
            }
        }

        private void simpleButton58_Click(object sender, EventArgs e)
        {
            groupControl12.Visible = true;
            fcs = 17;
            textEdit22.Select();
        }

        private void simpleButton59_Click(object sender, EventArgs e)
        {
            //yemek çeki buton
            groupControl13.Visible = true;

            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<sepet>("sepet");
            if (ConfigurationManager.AppSettings["Promosyon"].ToString() == "1")
            {
                coll = db.GetCollection<sepet>("promosepet");
            }
            var sepets = coll.Find("{}").ToListAsync().Result;
            double toplam = 0;
            foreach (sepet s in sepets)
            {
                if (s.kdvYuzdesi == 1 || s.kdvYuzdesi == 8)
                {
                    double satirToplam = s.toplam - s.sindirim;
                    satirToplam = satirToplam - s.indirim;
                    if (s.iptal == 0)
                    {
                        toplam = toplam + satirToplam;
                    }
                    if (s.iptal == 1)
                    {
                        toplam = toplam - satirToplam;
                    }
                }
            }

            if (toplam > 0)
            {
                textEdit20.Text = toplam.ToString("n2");
                fcs = 16;
                textEdit21.Select();
            }
            else
            {
                groupControl13.Visible = false;
                fonksiyonlar.HataMesaji("Satış Yapılamadı", "Sepette yemek çeki ile satılabilecek ürün yok", 3);
            }

            // MessageBox.Show("Yemek çeki toplamı" + toplam.ToString("n2"));
            // var aggregate = coll.Aggregate()
            // .Match(new BsonDocument { { "kdvYuzdesi", "1" } })
            // .Group(new BsonDocument { { "_id", "1" }, { "sepetToplam", new BsonDocument("$sum", "$toplam") } });

            //var sepets = aggregate.ToListAsync().Result;
        }

        private void KartSec()
        {
            groupControl12.Visible = false;
            if (textEdit12.Text == textEdit5.Text)
            {
                textEdit6.Text = textEdit12.Text;
                textEdit5.Text = "0";
            }
            else
            {
                if (labelControl16.Text == "Kalan")
                {
                    textEdit6.Text = labelControl17.Text;
                }
            }
            textEdit6.Focus();
            fcs = 3;
        }

        private void YemekCekiSec()
        {
            if (textEdit12.Text == textEdit5.Text)
            {
                textEdit7.Text = textEdit12.Text;
                textEdit5.Text = "0";
            }
            else
            {
                if (labelControl16.Text == "Kalan")
                {
                    textEdit7.Text = labelControl17.Text;
                }
            }
            textEdit7.Focus();
            fcs = 4;
            groupControl13.Visible = false;
            simpleButton52.PerformClick();
        }

        private void simpleButton60_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton61_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton62_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton63_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton64_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton65_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton66_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton67_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton68_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton69_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton70_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton71_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.banka = int.Parse(button.ToolTip);
                simpleButton58.Text = button.Text + " :";
            }
            KartSec();
        }

        private void simpleButton72_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton73_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton74_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton75_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton76_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton77_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton78_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton79_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton80_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton81_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton82_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton83_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            if (button.ToolTip != "")
            {
                this.yemekceki = int.Parse(button.ToolTip);
                simpleButton59.Text = button.Text + " :";
            }
            YemekCekiSec();
        }

        private void simpleButton84_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton85_Click(object sender, EventArgs e)
        {
            this.musteriVNo = textEdit14.Text;
            this.musteriUnvan = labelControl23.Text;
            labelControl36.Text = labelControl23.Text;
            groupControl14.Visible = false;
            this.evrakNo = textEdit13.Text;
            fcs = 0;
            textEdit1.Focus();
            textEdit13.Text = "";
            textEdit14.Text = "";
            labelControl23.Text = "";
        }

        private void simpleButton86_Click(object sender, EventArgs e)
        {
            this.musteriVNo = "11111111111";
            this.musteriUnvan = "Perakende Müşteri";
            labelControl36.Text = this.musteriUnvan;
            groupControl14.Visible = false;
            this.evrakNo = textEdit13.Text;
            fcs = 0;
            textEdit1.Focus();
            textEdit13.Text = "";
            textEdit14.Text = "";
            labelControl23.Text = "";
        }

        private void simpleButton88_Click(object sender, EventArgs e)
        {
            this.musteriVNo = textEdit15.Text;
            this.musteriUnvan = labelControl24.Text;
            groupControl15.Visible = false;
            this.evrakNo = "";
            fcs = 0;
            textEdit1.Focus();
            textEdit15.Text = "";
            labelControl24.Text = "";
        }

        private void simpleButton87_Click(object sender, EventArgs e)
        {
            groupControl15.Visible = false;
            this.belge = 0;
            labelControl12.Text = "Fiş";
            fcs = 0;
            textEdit1.Focus();
            textEdit15.Text = "";
            labelControl24.Text = "";
        }

        private void simpleButton89_Click(object sender, EventArgs e)
        {
            groupControl16.Visible = false;
            labelControl26.Visible = false;

            fcs = 0;
            textEdit1.Focus();
        }

        private void simpleButton90_Click(object sender, EventArgs e)
        {
            this.musteriVNo = "";
            groupControl16.Visible = false;
            groupControl15.Visible = false;
            labelControl26.Visible = false;
            this.evrakNo = "";
            if (ayar.eFaturaAktif == 1)
            {
                if (ayar.eArsivAktif == 1)
                {
                    this.belge = 3;
                    labelControl12.Text = "E-Arşiv";
                    labelControl36.Text = this.musteriUnvan;
                }
            }
            else
            {
                this.belge = 1;
                labelControl12.Text = "Fatura";
                this.evrakNo = textEdit13.Text;
                labelControl36.Text = this.musteriUnvan;
            }
            fcs = 0;
            textEdit1.Focus();
            textEdit13.Text = "";
        }

        private void simpleButton92_Click(object sender, EventArgs e)
        {
            int uzunluk = textEdit16.Text.Length;
            if (uzunluk > 11 || uzunluk < 10)
            {
                fonksiyonlar.HataMesaji("Vergi Numarası Hatalı", "Müşteri Vergi Numarası Hatalı", 2);
                return;
            }

            if (textEdit16.Text != "")
            {
                this.musteriVNo = textEdit16.Text;
            }
            else
            {
                this.musteriVNo = "11111111111";
            }
            groupControl17.Visible = false;
            fcs = 0;
            textEdit1.Focus();
            textEdit16.Text = "";
        }

        private void simpleButton91_Click(object sender, EventArgs e)
        {
            groupControl17.Visible = false;
            this.belge = 0;
            labelControl12.Text = "Fiş";
            fcs = 0;
            textEdit1.Focus();
            textEdit16.Text = "";
        }

        private void simpleButton41_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fonksiyonlar.OnayGoster("Belge İptal Onayı", "Belge İptali Yapılacak. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                {
                    if (fisbasla == 1)
                    {
                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
                        {
                            baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                            if (baslamaSonuc != 0)
                            {
                                FPUFuctions.Instance.OpenFiscalReceipt(int.Parse(kasiyerTanm.kasiyerkodu), -1, kasiyerTanm.kasiyerSifre, "", "");

                                baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                                if (baslamaSonuc != 0)
                                {
                                    HideMessage();
                                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                                    return;
                                }
                            }
                        }

                        fisbasla = 0;
                    }
                }
            }
            else
            {
                if (fonksiyonlar.OnayGoster("Belge İptal Onayı", "Belge İptali Yapılacak. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                {
                    //baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                    //if (baslamaSonuc != 0)
                    //{
                    //    if (baslamaSonuc != 0)
                    //    {
                    this.belgeiptal = 1;
                    satisTamamla();
                    //    }
                    //}
                }
            }

            groupControl6.Visible = false;
            groupControl7.Visible = false;
            groupControl4.Visible = true;

            textEdit1.Select();
            textEdit1.Focus();
        }

        private void simpleButton42_Click(object sender, EventArgs e)
        {
            FPUFuctions.Instance.OpenDrawer();

            groupControl6.Visible = false;
            groupControl7.Visible = false;
            groupControl4.Visible = true;

            textEdit1.Select();
            textEdit1.Focus();
        }

        private void simpleButton45_Click(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["DebugMode"].ToString() == "55")
            {
                int numRows = dataGridView1.RowCount;
                if (numRows > 0)
                {
                    if (groupControl11.Visible == false)
                    {
                        groupControl11.Visible = true;
                        textEdit19.Select();
                        fcs = 14;
                        groupControl7.Visible = false;
                        groupControl6.Visible = false;
                        groupControl4.Visible = true;
                    }
                    else
                    {
                        groupControl11.Visible = false;
                        groupControl7.Visible = false;
                        groupControl6.Visible = false;
                        groupControl4.Visible = true;
                    }
                }
                else
                {
                    fonksiyonlar.HataMesaji("Sepette Ürün Yok", "İndirim yapılacak ürünü öncelikle sepete ekleyiniz", 1);
                }
            }
        }

        private void textEdit19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                fcs = 15;
                textEdit11.Select();
            }

            if (e.KeyCode == Keys.Escape)
            {
                textEdit19.Text = "0";
                textEdit11.Text = "0";
                groupControl11.Visible = false;
                fcs = 0;
                textEdit1.Select();
            }
        }

        private void textEdit11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton56.PerformClick();
            }

            if (e.KeyCode == Keys.Escape)
            {
                textEdit19.Text = "0";
                textEdit11.Text = "0";
                groupControl11.Visible = false;
                fcs = 0;
                textEdit1.Select();
            }
        }

        private void simpleButton48_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton96_Click(object sender, EventArgs e)
        {
            groupControl18.Visible = false;
            memoEdit2.Text = "";
            labelControl32.Text = "0,00";
            labelControl33.Text = "0,00";
            labelControl34.Text = "0,00";
            simpleButton96.Visible = false;
            SepeteEkle(textEdit17.Text);
            textEdit17.Text = "";
            simpleButton16.Enabled = true;
            simpleButton17.Enabled = true;
            simpleButton18.Enabled = true;
            simpleButton19.Enabled = true;
            textEdit1.Select();
            fcs = 0;
        }

        private void simpleButton95_Click(object sender, EventArgs e)
        {
            textEdit17.Text = "";
            groupControl18.Visible = false;
            memoEdit2.Text = "";
            labelControl32.Text = "0,00";
            labelControl33.Text = "0,00";
            labelControl34.Text = "0,00";
            simpleButton96.Visible = false;
            simpleButton16.Enabled = true;
            simpleButton17.Enabled = true;
            simpleButton18.Enabled = true;
            simpleButton19.Enabled = true;
            textEdit1.Select();
            fcs = 0;
        }

        private void simpleButton37_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fonksiyonlar.OnayGoster("X Raporu", "X Raporu Alınacak. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                {
                    if (fisbasla == 1)
                    {
                        FPUFuctions.Instance.CancelFiscalReceipt();
                        fisbasla = 0;
                    }
                    ShowMessage("İşlem Yapılıyor", "Lütfen bekleyin...");

                    DateTime dt = DateTime.Now;
                    int satiriptaladet = 0;
                    double satiriptaltutar = 0;
                    var client = new MongoClient();
                    var db = client.GetDatabase("Olivetti");
                    var coll = db.GetCollection<PosBelge>("PosBelge");
                    var sepets = coll.Find("{$and: [ {'Tarih' : { $gte: ISODate('" + dt.ToString("yyyy-MM-dd") + "T00:00:00.000Z') }},{'BelgeAyr.iptal':1}]}").ToListAsync().Result;
                    foreach (PosBelge s in sepets)
                    {
                        foreach (Belgeayr a in s.BelgeAyr)
                        {
                            if (a.iptal == 1)
                            {
                                satiriptaladet = satiriptaladet + 1;
                                satiriptaltutar = satiriptaltutar + a.Tutar;
                            }
                        }
                    }

                    if (satiriptaladet > 0)
                    {
                        FPUFuctions.Instance.OpenNonFiscalReceipt();
                        FPUFuctions.Instance.PrintNonFiscalFreeText("SATIR İPTAL ADETİ          :" + satiriptaladet.ToString().PadLeft(10, ' ') + " AD");
                        FPUFuctions.Instance.PrintNonFiscalFreeText("SATIR İPTAL TUTARI         :" + satiriptaltutar.ToString("n2").PadLeft(10, ' ') + " TL");
                        FPUFuctions.Instance.CloseNonFiscalReceipt();
                    }

                    int yemekcekiiptaladet = 0;
                    double yemekcekiiptaltutar = 0;
                    coll = db.GetCollection<PosBelge>("PosBelge");
                    sepets = coll.Find("{$and: [ {'Tarih' : { $gte: ISODate('" + dt.ToString("yyyy-MM-dd") + "T00:00:00.000Z') }},{'BelgeTip':10},{'iptal':1}]}").ToListAsync().Result;
                    foreach (PosBelge s in sepets)
                    {
                        yemekcekiiptaladet = yemekcekiiptaladet + 1;
                        yemekcekiiptaltutar = yemekcekiiptaltutar + s.Toplam;
                    }

                    if (yemekcekiiptaladet > 0)
                    {
                        FPUFuctions.Instance.OpenNonFiscalReceipt();
                        FPUFuctions.Instance.PrintNonFiscalFreeText("MALİ OLM. FİŞ İPTAL ADETİ  :" + yemekcekiiptaladet.ToString().PadLeft(10, ' ') + " AD");
                        FPUFuctions.Instance.PrintNonFiscalFreeText("MALİ OLM. FİŞ İPTAL TUTARI :" + yemekcekiiptaltutar.ToString("n2").PadLeft(10, ' ') + " TL");
                        FPUFuctions.Instance.CloseNonFiscalReceipt();
                    }

                    FPUFuctions.Instance.ExpenseslipInfo(fonksiyonlar.IadeAdet(), 0, fonksiyonlar.IadeToplami(), 0, fonksiyonlar.IadeToplami(), 0, 0, 0, 0, 0);
                    baslamaSonuc = FPUFuctions.Instance.XReport().ErrorCode;
                    if (baslamaSonuc != 0)
                    {
                        HideMessage();
                        var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                        fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                        return;
                    }

                    HideMessage();
                }
            }
            else
            {
                fonksiyonlar.HataMesaji("X Raporu Yazdırılamadı", "Tamamlanmamış fiş varken X raporu alınamaz. Önce fişi bitirmelisiniz.", 2);
            }

            groupControl6.Visible = false;
            groupControl7.Visible = false;
            groupControl4.Visible = true;

            textEdit1.Select();
            textEdit1.Focus();
        }

        private void simpleButton49_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fonksiyonlar.OnayGoster("Z Raporu", "Z Raporu Alınacak. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                {
                    if (fisbasla == 1)
                    {
                        FPUFuctions.Instance.CancelFiscalReceipt();
                        fisbasla = 0;
                    }
                    ShowMessage("İşlem Yapılıyor", "Lütfen bekleyin...");
                    DateTime dt = DateTime.Now;
                    int satiriptaladet = 0;
                    double satiriptaltutar = 0;
                    var client = new MongoClient();
                    var db = client.GetDatabase("Olivetti");
                    var coll = db.GetCollection<PosBelge>("PosBelge");
                    var sepets = coll.Find("{$and: [ {'Tarih' : { $gte: ISODate('" + dt.ToString("yyyy-MM-dd") + "T00:00:00.000Z') }},{'BelgeAyr.iptal':1}]}").ToListAsync().Result;
                    foreach (PosBelge s in sepets)
                    {
                        foreach (Belgeayr a in s.BelgeAyr)
                        {
                            if (a.iptal == 1)
                            {
                                satiriptaladet = satiriptaladet + 1;
                                satiriptaltutar = satiriptaltutar + a.Tutar;
                            }
                        }
                    }

                    if (satiriptaladet > 0)
                    {
                        FPUFuctions.Instance.OpenNonFiscalReceipt();
                        FPUFuctions.Instance.PrintNonFiscalFreeText("SATIR İPTAL ADETİ          :" + satiriptaladet.ToString().PadLeft(10, ' ') + " AD");
                        FPUFuctions.Instance.PrintNonFiscalFreeText("SATIR İPTAL TUTARI         :" + satiriptaltutar.ToString("n2").PadLeft(10, ' ') + " TL");
                        FPUFuctions.Instance.CloseNonFiscalReceipt();
                    }

                    int yemekcekiiptaladet = 0;
                    double yemekcekiiptaltutar = 0;
                    coll = db.GetCollection<PosBelge>("PosBelge");
                    sepets = coll.Find("{$and: [ {'Tarih' : { $gte: ISODate('" + dt.ToString("yyyy-MM-dd") + "T00:00:00.000Z') }},{'BelgeTip':10},{'iptal':1}]}").ToListAsync().Result;
                    foreach (PosBelge s in sepets)
                    {
                        yemekcekiiptaladet = yemekcekiiptaladet + 1;
                        yemekcekiiptaltutar = yemekcekiiptaltutar + s.Toplam;
                    }

                    if (yemekcekiiptaladet > 0)
                    {
                        FPUFuctions.Instance.OpenNonFiscalReceipt();
                        FPUFuctions.Instance.PrintNonFiscalFreeText("MALİ OLM. FİŞ İPTAL ADETİ  :" + yemekcekiiptaladet.ToString().PadLeft(10, ' ') + " AD");
                        FPUFuctions.Instance.PrintNonFiscalFreeText("MALİ OLM. FİŞ İPTAL TUTARI :" + yemekcekiiptaltutar.ToString("n2").PadLeft(10, ' ') + " TL");
                        FPUFuctions.Instance.CloseNonFiscalReceipt();
                    }
                    FPUFuctions.Instance.ExpenseslipInfo(fonksiyonlar.IadeAdet(), 0, fonksiyonlar.IadeToplami(), 0, fonksiyonlar.IadeToplami(), 0, 0, 0, 0, 0);
                    var sonuc = FPUFuctions.Instance.ZReport();
                    HideMessage();
                    if (sonuc.ErrorCode.ToString() == "1")
                    {
                        ShowMessage("İşlem Yapılıyor", "Lütfen bekleyin...");
                        FPUFuctions.Instance.SetModeLogin("0000");
                        baslamaSonuc = FPUFuctions.Instance.ZReport().ErrorCode;
                        if (baslamaSonuc != 0)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                            return;
                        }
                        else
                        {
                            logSifirla();
                            fonksiyonlar.IadeSifirla();
                        }
                        FPUFuctions.Instance.ChangeECRMode(1);
                        HideMessage();
                    }
                    fonksiyonlar.FisNoArttirSifirla(1);
                    this.Close();
                }
            }
            else
            {
                fonksiyonlar.HataMesaji("Z Raporu Yazdırılamadı", "Tamamlanmamış fiş varken Z raporu alınamaz. Önce fişi bitirmelisiniz.", 2);
            }
        }

        private void simpleButton44_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fisbasla == 1)
                {
                    FPUFuctions.Instance.CancelFiscalReceipt();
                    fisbasla = 0;
                }
                fcs = 12;
                groupControl4.Visible = true;
                groupControl7.Visible = false;
                groupControl19.Visible = true;
                labelControl35.Text = "Avans Miktarı";
                textEdit18.Properties.UseSystemPasswordChar = false;
                textEdit18.Text = "";
                textEdit18.Focus();
                simpleButton16.Enabled = false;
                simpleButton17.Enabled = false;
                simpleButton18.Enabled = false;
                simpleButton19.Enabled = false;
            }
            else
            {
                fonksiyonlar.HataMesaji("Açık Fiş Var", "Açık belge varken bu işlemi yapamazsınız. Önce belgeyi bitirmelisiniz.", 2);
            }
        }

        private void simpleButton94_Click(object sender, EventArgs e)
        {
            groupControl19.Visible = false;
            simpleButton16.Enabled = true;
            simpleButton17.Enabled = true;
            simpleButton18.Enabled = true;
            simpleButton19.Enabled = true;

            if (textEdit18.Text != "")
            {
                if (labelControl35.Text == "Avans Miktarı")
                {
                    double tutar = double.Parse(textEdit18.Text);
                    baslamaSonuc = FPUFuctions.Instance.CashInCashOut(tutar).ErrorCode;
                    if (baslamaSonuc != 0)
                    {
                        HideMessage();
                        var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                        fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                        return;
                    }
                }
                if (labelControl35.Text == "Çekilen Tutar")
                {
                    double tutar = double.Parse("-" + textEdit18.Text.Replace(".", ","));
                    double kasaNakit = double.Parse(FPUFuctions.Instance.GetCashInDrawer().Cash.Replace(".", ","));
                    double sonuc = tutar + kasaNakit;
                    if (sonuc < 0)
                    {
                        fonksiyonlar.HataMesaji("Çekme İşlemi Yapılamadı!", "Çekme işlemi için yeterli tutar kasada mevcut değil.", 2);
                        return;
                    }
                    baslamaSonuc = FPUFuctions.Instance.CashInCashOut(tutar).ErrorCode;
                    if (baslamaSonuc != 0)
                    {
                        HideMessage();
                        var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                        fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                        return;
                    }
                }
                if (labelControl35.Text == "Şifre Giriniz")
                {
                    DateTime myDT = DateTime.Now;

                    string sifre = myDT.Year.ToString().Substring(2, 2) + myDT.Month.ToString() + myDT.Day.ToString();
                    if (textEdit18.Text == sifre)
                    {
                        if (app == null)
                        {
                            app = new AppStarter();
                        }
                        this.TopMost = false;
                        app.StartMainWindow();
                    }
                    else
                    {
                        fonksiyonlar.HataMesaji("Şifre Hatalı", "Şifre Hatalı", 2);
                    }
                }
            }
            textEdit1.Select();
        }

        private void simpleButton93_Click(object sender, EventArgs e)
        {
            groupControl19.Visible = false;
            simpleButton16.Enabled = true;
            simpleButton17.Enabled = true;
            simpleButton18.Enabled = true;
            simpleButton19.Enabled = true;
        }

        private void simpleButton43_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fisbasla == 1)
                {
                    FPUFuctions.Instance.CancelFiscalReceipt();
                    fisbasla = 0;
                }
                fcs = 12;
                groupControl4.Visible = true;
                groupControl7.Visible = false;
                groupControl19.Visible = true;
                labelControl35.Text = "Çekilen Tutar";
                textEdit18.Properties.UseSystemPasswordChar = false;
                textEdit18.Text = "";
                textEdit18.Focus();
                simpleButton16.Enabled = false;
                simpleButton17.Enabled = false;
                simpleButton18.Enabled = false;
                simpleButton19.Enabled = false;
            }
            else
            {
                fonksiyonlar.HataMesaji("Açık Fiş Var", "Açık belge varken bu işlemi yapamazsınız. Önce belgeyi bitirmelisiniz.", 2);
            }
        }

        private void simpleButton39_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows == 0)
            {
                if (fisbasla == 1)
                {
                    FPUFuctions.Instance.CancelFiscalReceipt();
                    fisbasla = 0;
                }
                fcs = 12;
                groupControl4.Visible = true;
                groupControl7.Visible = false;
                groupControl19.Visible = true;
                labelControl35.Text = "Şifre Giriniz";
                textEdit18.Properties.UseSystemPasswordChar = true;
                textEdit18.Text = "";
                textEdit18.Focus();
            }
            else
            {
                fonksiyonlar.HataMesaji("Açık Fiş Var", "Açık belge varken bu işlemi yapamazsınız. Önce belgeyi bitirmelisiniz.", 2);
            }
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            if (groupControl6.Visible == true)
            {
                groupControl6.Visible = false;
                groupControl7.Visible = true;
            }
            else
            {
                groupControl6.Visible = true;
                groupControl7.Visible = false;
            }
            textEdit1.Select();
            fcs = 0;

            simpleButton16.Enabled = true;
            simpleButton17.Enabled = true;
            simpleButton18.Enabled = true;
            simpleButton19.Enabled = true;
        }

        private void simpleButton46_Click(object sender, EventArgs e)
        {
            if (groupControl6.Visible == true)
            {
                groupControl7.Visible = false;
                groupControl6.Visible = false;
                groupControl4.Visible = true;
            }
            else
            {
                groupControl7.Visible = false;
                groupControl6.Visible = true;
                groupControl4.Visible = false;
            }
            simpleButton16.Enabled = false;
            simpleButton17.Enabled = false;
            simpleButton18.Enabled = false;
            simpleButton19.Enabled = false;
            textEdit1.Select();
            fcs = 0;
        }

        private void ShowMessage(string baslik, string mesaj)
        {
            groupControl20.Dock = DockStyle.Fill;
            memoEdit3.Text = "\r\n" + baslik;
            memoEdit3.Text = memoEdit3.Text + "\r\n\r\n";
            memoEdit3.Text = memoEdit3.Text + mesaj;
            groupControl20.Visible = true;
            System.Threading.Thread.Sleep(100);
        }

        private void HideMessage()
        {
            System.Threading.Thread.Sleep(100);
            groupControl20.Visible = false;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            fcs = 0;
            textEdit1.Focus();
        }

        private void groupControl19_Paint(object sender, PaintEventArgs e)
        {
        }

        private void groupControl7_Paint(object sender, PaintEventArgs e)
        {
        }

        private void simpleButton38_Click(object sender, EventArgs e)
        {
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            if (t2.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                t2.Start();
            }
            if (t2.ThreadState == System.Threading.ThreadState.Stopped)
            {
                t2.Abort();
                t2 = new Thread(baglantKontrol);
                t2.Start();
            }
        }

        private void simpleButton56_Click(object sender, EventArgs e)
        {
            int numRows = dataGridView1.RowCount;
            if (numRows > 0)
            {
                string sonUrun = dataGridView1.Rows[numRows - 1].Cells[0].Value.ToString();
                double sonUrunToplam = double.Parse(dataGridView1.Rows[numRows - 1].Cells[4].Value.ToString());
                double inidimTl = double.Parse(textEdit11.Text.Replace(".", ","));
                int indirimYuzde = int.Parse(textEdit19.Text);
                if (indirimYuzde > 99)
                {
                    fonksiyonlar.HataMesaji("İndirim yapılamadı", "%99 dan fazla indirim yapıamaz!", 2);
                    return;
                }

                if (inidimTl >= sonUrunToplam)
                {
                    fonksiyonlar.HataMesaji("İndirim yapılamadı", "İndirim tutarı ürün toplamından düşük olmalıdır!", 2);
                    return;
                }

                if (indirimYuzde > 0)
                {
                }
                else if (inidimTl > 0)
                {
                    var client = new MongoClient();
                    var db = client.GetDatabase("Olivetti");
                    var coll = db.GetCollection<sepet>("sepet");
                    var sepets = coll.Find("{'barkod':'" + sonUrun + "'}").ToListAsync().Result;
                    foreach (sepet s in sepets)
                    {
                        double indOran = (inidimTl * 100) / sonUrunToplam;
                        double indirimSatir = (double.Parse(s.toplam.ToString()) / 100) * indOran;
                        double indirimSonrasi = s.toplam - indirimSatir;

                        double kdvDeger2 = 100 + s.kdvYuzdesi;
                        double kdvDeger = kdvDeger2 / 100;
                        double kdvTutari2 = indirimSonrasi / kdvDeger;
                        double kdvTutari3 = indirimSonrasi - kdvTutari2;
                        var update = Builders<sepet>.Update
                         .Set("sindirim", indirimSatir)
                             .Set("sindirimKdv", kdvTutari3);
                        coll.UpdateOne("{'_id':ObjectId('" + s.id + "')}", update);

                        if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "-1")
                        {
                            if (fisbasla == 1)
                            {
                                FPUFuctions.Instance.PreAndDiscount("D", Convert.ToDecimal("-" + indirimSatir));
                            }
                        }
                        sepetOku();
                        break;
                    }
                }
            }

            groupControl11.Visible = false;
            fcs = 0;
            textEdit1.Select();
            textEdit11.Text = "0";
            textEdit19.Text = "0";
        }

        private void simpleButton36_Click(object sender, EventArgs e)
        {
            //if (kasaMesaj != 0)
            //{
            //    if (kasaMesaj == 1)
            //    {
            ShowMessage("Lütfen bekleyin", "Ürün listesi alınıyor");
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.UseShellExecute = true;
            procInfo.CreateNoWindow = true;
            procInfo.FileName = @"D:\\App3\\import.bat";  //The file in that DIR.
            procInfo.WorkingDirectory = @""; //The working DIR.
            var process = new Process();
            process.StartInfo = procInfo;
            process.Start();
            process.WaitForExit();
            HideMessage();
            fonksiyonlar.MesajGoster("Güncelleme", "Ürün Listesi Güncellendi", 2);
            //        kasaMesaj = 0;
            //    }
            //}
            //else
            //{
            //    fonksiyonlar.HataMesaji("Güncelleme Yok", "Bilgi Güncellemesi Bulunamadı", 1);
            //}

            groupControl6.Visible = false;
            groupControl7.Visible = false;
            groupControl4.Visible = true;

            textEdit1.Select();
            textEdit1.Focus();
        }

        private void simpleButton51_Click_1(object sender, EventArgs e)
        {
            //fiştekrar
            simpleButton17.PerformClick();

            int numRows = dataGridView1.RowCount;
            if (numRows > 0)
            {
                fonksiyonlar.HataMesaji("Açık Belge Var", "Var olan belgeyi bitirmeden bu işleme devam edemezsiniz!", 1);
            }
            else
            {
                if (ConfigurationManager.AppSettings["AutoOpenFis"].ToString() == "1")
                {
                    if (fisbasla == 1)
                    {
                        baslamaSonuc = FPUFuctions.Instance.CancelFiscalReceipt().ErrorCode;
                        if (baslamaSonuc != 0)
                        {
                            HideMessage();
                            var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                            fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                            return;
                        }
                        fisbasla = 0;
                    }
                }

                baslamaSonuc = FPUFuctions.Instance.SetModeLogin("0000").ErrorCode;
                if (baslamaSonuc != 0)
                {
                    HideMessage();
                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                    return;
                }
                var fis = FPUFuctions.Instance.ReadLastReceiptDetail("L");
                baslamaSonuc = FPUFuctions.Instance.ReadEJSpecificReceiptByNumber(int.Parse(fis.ZNo), int.Parse(fis.ReceiptNumber)).ErrorCode;
                FPUFuctions.Instance.ChangeECRMode(1);
                if (baslamaSonuc != 0)
                {
                    HideMessage();
                    var errD = ErrorCode.GetErrorExplanation(baslamaSonuc.ToString());
                    fonksiyonlar.HataMesaji("İşlem Tamamlanamadı!", errD.errorExplanation.ToString(), 2);
                    return;
                }
            }
        }

        private void simpleButton57_Click(object sender, EventArgs e)
        {
            simpleButton17.PerformClick();
            var report = new Stimulsoft.Report.StiReport();
            report.Load("fatura.mrt");
            var database = new Stimulsoft.Report.Dictionary.StiXmlDatabase("Fatura", @"D:\App3\Log\fatura.xml", @"D:\App3\Log\fatura.xml");
            report.Dictionary.Databases.Add(database);
            report.PrinterSettings.ShowDialog = false;
            report.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["InvoicePrinter"].ToString();
            report.Print();
            yaziciDuzelt();
            fonksiyonlar.MesajGoster("Fatura Tekrarı", "Son Fatura Tekrar Yazıcıya Gönderildi", 1);
        }

        private void logSifirla()
        {
            DateTime dt = DateTime.Now;
            var dosya = @"D:\App3\Log\GNETR" + ayar.kasaNumarasi.ToString() + ".log";
            var dizin = @"D:\App3\Log\Backup";
            if (System.IO.File.Exists(dosya))
            {
                if (System.IO.Directory.Exists(dizin) == false)
                {
                    System.IO.Directory.CreateDirectory(dizin);
                }
                var dosya2 = @"D:\App3\Log\Backup\GNETR" + ayar.kasaNumarasi.ToString() + "-" + dt.ToString("yyyyMMddHHmmss") + ".log";

                if (System.IO.File.Exists(dosya2) == false)
                {
                    System.IO.File.Copy(dosya, dosya2, true);
                    System.IO.File.Delete(dosya);
                }
                else
                {
                }
            }
        }

        private void hataLogYaz(string hataMesaji)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var dosya = @"D:\App3\hataLoglari.log";
                if (!System.IO.File.Exists(dosya))
                {
                    System.IO.File.Create(dosya);
                }
                System.IO.StreamWriter hataLogu = new System.IO.StreamWriter(dosya, true);
                hataLogu.WriteLine(dt.ToString() + ":" + hataMesaji);
                hataLogu.Close();
            }
            catch
            {
            }
        }

        private void LogYaz(string mesaj)
        {
            try
            {
                DateTime dt = DateTime.Now;

                var dizin = @"D:\App3\Log2\";
                if (!System.IO.Directory.Exists(dizin))
                {
                    System.IO.Directory.CreateDirectory(dizin);
                }

                var dosya = dizin + @"\" + dt.ToString("yyyyMMdd") + ".log";
                if (!System.IO.File.Exists(dosya))
                {
                    System.IO.File.Create(dosya);
                }
                System.IO.StreamWriter hataLogu = new System.IO.StreamWriter(dosya, true);
                hataLogu.WriteLine(dt.ToString() + ":" + mesaj);
                hataLogu.Close();
            }
            catch
            {
            }
        }
    }

    public class sepetToplami
    {
        public string id { get; set; }
        public double sepetToplam { get; set; }
    }

    public class sepet
    {
        public ObjectId id { get; set; }
        public string stkID { get; set; }
        public string stkKod { get; set; }
        public double miktar { get; set; }
        public string stkAd { get; set; }
        public double fiyatS1 { get; set; }
        public string barkod { get; set; }
        public int iptal { get; set; }
        public int satiriptal { get; set; }
        public int kdv { get; set; }
        public int kdvYuzdesi { get; set; }
        public double kdvTutari { get; set; }
        public double kdvsizToplam { get; set; }
        public double toplam { get; set; }
        public double indirim { get; set; }
        public double indirimKdv { get; set; }

        public double indirimKalan { get; set; }

        public double sindirim { get; set; }
        public double sindirimKdv { get; set; }
        public int stkBrm { get; set; }

        public string kod1 { get; set; }
        public string kod2 { get; set; }
        public string kod3 { get; set; }
        public string kod4 { get; set; }
        public string kod5 { get; set; }
    }

    public class stk
    {
        public string id { get; set; }
        public int stkID { get; set; }
        public string stkKod { get; set; }
        public string stkAd { get; set; }
        public string stkAdKisa { get; set; }
        public string KdvS { get; set; }
        public string KdvA { get; set; }
        public string stkFirma { get; set; }
        public double fiyatS1 { get; set; }
        public double fiyatS2 { get; set; }
        public double fiyatS3 { get; set; }
        public double fiyatS4 { get; set; }
        public double fiyatS5 { get; set; }
        public float fiyatA { get; set; }
        public string stkGrp { get; set; }
        public int stkMarka { get; set; }
        public int stkKoliAdet { get; set; }
        public int Terazi { get; set; }
        public int stkDegisti { get; set; }
        public double stkInd1 { get; set; }
        public double stkInd2 { get; set; }
        public double stkInd3 { get; set; }
        public int stkKategori { get; set; }
        public string stkKayitTarihi { get; set; }
        public string stkDegTarihi { get; set; }
        public string kod1 { get; set; }
        public string kod2 { get; set; }
        public string kod3 { get; set; }
        public string kod4 { get; set; }
        public string kod5 { get; set; }
        public string hedefKar { get; set; }
        public string stkTip { get; set; }
        public string paketOlcu { get; set; }
        public string stkBrm { get; set; }
        public string stkPaketBrm { get; set; }
        public string stkKoliBrm { get; set; }
        public string kKisi { get; set; }
        public int gKisi { get; set; }
        public string pbA { get; set; }
        public string pbS { get; set; }
        public string Web { get; set; }
        public int rafGun { get; set; }
        public string posNoInd { get; set; }
        public string spot { get; set; }
        public int stkGunSayi { get; set; }
        public Barkods[] Barkod { get; set; }
    }

    public class Barkods
    {
        public int sira { get; set; }
        public string barkod { get; set; }
        public int barkodTipi { get; set; }
        public int barkodFiyat { get; set; }
        public int barkodMiktari { get; set; }
    }

    public class PosBelge
    {
        public ObjectId id { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime Saat { get; set; }
        public int PosNo { get; set; }
        public int Magaza { get; set; }
        public string EvrakNo { get; set; }
        public int FisNo { get; set; }
        public string Musteri { get; set; }
        public int mKartNo { get; set; }
        public int iptal { get; set; }
        public int Kasiyer { get; set; }
        public double Toplam { get; set; }
        public double ToplamInd { get; set; }
        public int BelgeTip { get; set; }

        public int Aktarim { get; set; }
        public List<Belgeayr> BelgeAyr { get; set; }
        public List<Odeme> Odeme { get; set; }
    }

    public class Belgeayr
    {
        public int SatirNo { get; set; }
        public string Stk { get; set; }
        public string Barkod { get; set; }
        public double Miktar { get; set; }
        public double Kdv { get; set; }
        public double Tutar { get; set; }
        public double IndSatir { get; set; }
        public double IndToplam { get; set; }
        public int Promo { get; set; }
        public int iptal { get; set; }
    }

    public class Odeme
    {
        public int OdemeTip { get; set; }
        public double OdemeTutar { get; set; }
        public int OdemeDetay { get; set; }
    }

    public class BelgeTipi
    {
        public ObjectId id { get; set; }
        public int belgeTipId { get; set; }
        public string belgeTipAd { get; set; }
    }

    public class OdemeTip
    {
        public ObjectId id { get; set; }
        public int odemeTipi { get; set; }
        public int odemeNo { get; set; }
        public string odemeIsim { get; set; }
    }

    public class musteri
    {
        public ObjectId id { get; set; }
        public int musteriId { get; set; }
        public string unVan { get; set; }
        public string adres1 { get; set; }
        public string adres2 { get; set; }
        public string adres3 { get; set; }
        public string postaKodu { get; set; }
        public string il { get; set; }
        public string telefon { get; set; }
        public string vergiDairesi { get; set; }
        public string vergiNumarasi { get; set; }
        public string indirim { get; set; }
        public int fiyat { get; set; }
    }

    public class musteri2
    {
        public int musteriId { get; set; }
        public string unVan { get; set; }
        public string adres1 { get; set; }
        public string adres2 { get; set; }
        public string adres3 { get; set; }
        public string postaKodu { get; set; }
        public string il { get; set; }
        public string telefon { get; set; }
        public string vergiDairesi { get; set; }
        public string vergiNumarasi { get; set; }
        public string indirim { get; set; }
        public int fiyat { get; set; }
    }

    public class PosBelge2
    {
        public string Tarih { get; set; }
        public int PosNo { get; set; }
        public int Magaza { get; set; }
        public string EvrakNo { get; set; }
        public int FisNo { get; set; }
        public string Musteri { get; set; }
        public int mKartNo { get; set; }
        public int Kasiyer { get; set; }
        public string Toplam { get; set; }
        public string ToplamYazi { get; set; }
        public string ToplamKdv { get; set; }
        public string ToplamInd { get; set; }
        public string BelgeTip { get; set; }
        public musteri2 musteriDetay { get; set; }
        public List<Belgeayr2> BelgeAyr { get; set; }
        public List<Odeme2> Odeme { get; set; }
        public List<KdvListesi> Kdvler { get; set; }
    }

    public class Belgeayr2
    {
        public string stkAd { get; set; }
        public string stkFiyat { get; set; }
        public int SatirNo { get; set; }
        public string Barkod { get; set; }
        public string Miktar { get; set; }
        public string Kdv { get; set; }
        public string Tutar { get; set; }
        public string IndSatir { get; set; }
        public string IndToplam { get; set; }
        public int Promo { get; set; }
        public int iptal { get; set; }
        public string Toplam { get; set; }

        public stk stk { get; set; }
    }

    public class Odeme2
    {
        public int OdemeTip { get; set; }
        public string OdemeTutar { get; set; }
        public string OdemeDetay { get; set; }
    }

    public class KdvListesi
    {
        public string kdvYuzde { get; set; }
        public string kdvTutari { get; set; }
        public string kdvToplami { get; set; }
    }

    public class eFatura
    {
        public ObjectId id { get; set; }
        public string Identifier { get; set; }
        public string Alias { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime FirstCreationTime { get; set; }
        public DateTime AliasCreationTime { get; set; }
        public string AccountType { get; set; }
    }

    public class Ayarlar
    {
        public ObjectId id { get; set; }
        public int KrediKartiSecimi { get; set; }
        public int YemekCekiSecimi { get; set; }
        public int eFaturaAktif { get; set; }
        public int eArsivAktif { get; set; }
        public int fisLimit { get; set; }
        public int fisSiraNo { get; set; }
        public int iadeToplam { get; set; }
    }

    public class Promo
    {
        public ObjectId _id { get; set; }
        public int promoId { get; set; }
        public int promoKodu { get; set; }
        public int promoTip { get; set; }
        public int promoAktif { get; set; }
        public string pomoAciklama { get; set; }

        public DateTime promoBaslangicT { get; set; }
        public string promoBaslangicZ { get; set; }
        public DateTime promoBitisT { get; set; }
        public string promoBitisZ { get; set; }
        public string promoGun { get; set; }
        public int musteriTipi { get; set; }
        public float sepetToplami { get; set; }
        public int indirimTipi { get; set; }
        public float indirim { get; set; }
        public int odemeTipi { get; set; }
        public int fiyatTip { get; set; }
        public int onay { get; set; }
        public List<Promoayr> PromoAyr { get; set; }
        public List<Promomagaza> PromoMagaza { get; set; }
    }

    public class Promoayr
    {
        public string Urn1ID { get; set; }
        public int Urn1Miktar { get; set; }
        public string Urn2ID { get; set; }
        public int Urn2Miktar { get; set; }
        public int Urn2IndirimTip { get; set; }
        public int Urn2Indirim { get; set; }
    }

    public class Promomagaza
    {
        public int MagazaID { get; set; }
    }

    public class hizliUrun
    {
        public ObjectId id { get; set; }
        public string urunBarkod { get; set; }
        public string urunAdi { get; set; }
    }

    public class promoCount
    {
        public int _id { get; set; }
        public double adet { get; set; }
    }

    public class MagazaMesaj
    {
        public ObjectId _id { get; set; }
        public int magazaKodu { get; set; }
        public string magazaAdi { get; set; }
        public int mesajTuru { get; set; }

        public int mesajDurumu { get; set; }

        public DateTime mesajTarihi { get; set; }

        public DateTime cevapTarihi { get; set; }
    }

    public class PromosyonToplamlari
    {
        public ObjectId _id { get; set; }
        public double promosyonTutar { get; set; }
        public string promosyonTex { get; set; }
    }
}