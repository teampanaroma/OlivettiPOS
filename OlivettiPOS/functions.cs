using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace OlivettiPOS
{
    public class functions
    {
        public string EkranYazisi(string yazi)
        {
            string str = yazi;

            return str;
        }

        public void HataMesaji(string baslik, string mesaj, int sure)
        {
            mesaj frm = new mesaj();
            frm.Controls["LabelControl1"].Text = baslik;
            frm.Controls["memoEdit1"].Text = mesaj;
            frm.Controls["label1"].Text = sure.ToString();
            frm.ShowDialog();
        }

        public void MesajGoster(string baslik, string mesaj, int sure)
        {
            mesaj2 frm = new mesaj2();
            frm.Controls["LabelControl1"].Text = baslik;
            frm.Controls["memoEdit1"].Text = mesaj;
            frm.Controls["label1"].Text = sure.ToString();
            frm.ShowDialog();
        }

        public void beklemeGoster(string baslik, string mesaj, int sure)
        {
            mesaj3 frm = new mesaj3();
            frm.Controls["LabelControl1"].Text = baslik;
            frm.Controls["richTextBox1"].Text = mesaj;
            frm.Controls["label1"].Text = sure.ToString();
            frm.ShowDialog();
        }

        public string OnayGoster(string baslik, string mesaj, int sure, string button1, string button2)
        {
            onay OnayFrm = new onay();
            OnayFrm.Controls["memoEdit1"].Text = mesaj;
            OnayFrm.Controls["LabelControl1"].Text = baslik;
            OnayFrm.Controls["simpleButton1"].Text = button1;
            OnayFrm.Controls["simpleButton2"].Text = button2;
            return OnayFrm.ShowDialog().ToString();
        }

        public int FisNo()
        {
            int cevap = 0;
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").Limit(1).ToListAsync().Result;
            foreach (AyarlarDB s in sepets)
            {
                cevap = s.fisSiraNo;
            }

            return cevap;
        }

        public void FisNoArttirSifirla(int islem)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").ToListAsync().Result;
            double kayit = 0;
            int yeniIslemNo = FisNo() + 1;
            if (islem == 1)
            {
                yeniIslemNo = 1;
            }
            var update = Builders<AyarlarDB>.Update.Set("fisSiraNo", yeniIslemNo);
            coll.UpdateOne("{}", update);
        }

        public void IadeArttir(double tutar)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").ToListAsync().Result;
            double kayit = 0;
            foreach (AyarlarDB s2 in sepets)
            {
                kayit = s2.iadeToplam + tutar;
                var update = Builders<AyarlarDB>.Update.Set("iadeToplam", kayit);
                coll.UpdateOne("{}", update);
                update = Builders<AyarlarDB>.Update.Set("iadeAdet", s2.iadeAdet + 1);
                coll.UpdateOne("{}", update);
            }
        }

        public void IadeSifirla()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").ToListAsync().Result;
            double kayit = 0;
            foreach (AyarlarDB s2 in sepets)
            {
                var update = Builders<AyarlarDB>.Update.Set("iadeToplam", kayit);
                coll.UpdateOne("{}", update);
                coll.UpdateOne("{}", update);
                update = Builders<AyarlarDB>.Update.Set("iadeAdet", 0);
                coll.UpdateOne("{}", update);
            }
        }

        public double IadeToplami()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").ToListAsync().Result;
            double kayit = 0;
            foreach (AyarlarDB s2 in sepets)
            {
                kayit = s2.iadeToplam;
            }
            return kayit;
        }

        public int IadeAdet()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").ToListAsync().Result;
            int kayit = 0;
            foreach (AyarlarDB s2 in sepets)
            {
                kayit = s2.iadeAdet;
            }
            return kayit;
        }

        public int kdvBul(int kdvDeger, string degerler)
        {
            int retDeger = 3;

            string[] kdvS = degerler.Split(',');

            foreach (string k in kdvS)
            {
                if (k.IndexOf('=') != 0)
                {
                    string[] kdvDgr = k.Split('=');

                    if (int.Parse(kdvDgr[0]) == kdvDeger)
                    {
                        retDeger = int.Parse(kdvDgr[1]);
                        break;
                    }
                }
            }
            //switch (kdvDeger)
            //{
            //    case 0:
            //        retDeger = 1;
            //        break;
            //    case 1:
            //        retDeger = 2;
            //        break;
            //    case 2:
            //        retDeger = 3;
            //        break;
            //    case 3:
            //        retDeger = 4;
            //        break;
            //}
            return retDeger;
        }

        public int kdvYuzdeBul(int kdvDeger)
        {
            int retDeger = 1;
            switch (kdvDeger)
            {
                case 1:
                    retDeger = 0;
                    break;

                case 2:
                    retDeger = 1;
                    break;

                case 3:
                    retDeger = 8;
                    break;

                case 4:
                    retDeger = 18;
                    break;
            }
            return retDeger;
        }

        public int birimDegerBul(string birim)
        {
            int retDeger = 0;
            switch (birim)
            {
                case "AD":
                    retDeger = 0;
                    break;

                case "KG":
                    retDeger = 1;
                    break;

                case "MT":
                    retDeger = 2;
                    break;

                case "LT":
                    retDeger = 3;
                    break;
            }
            return retDeger;
        }

        public string ekranaYazdir(string str)
        {
            string[] words = str.Split(' ');
            return str;
        }

        public string donustur(string str)
        {
            string once, sonra;
            once = str;
            sonra = once.Replace('ı', 'i');
            once = sonra.Replace('ö', 'o');
            sonra = once.Replace('ü', 'u');
            once = sonra.Replace('ş', 's');
            sonra = once.Replace('ğ', 'g');
            once = sonra.Replace('ç', 'c');
            sonra = once.Replace('İ', 'I');
            once = sonra.Replace('Ö', 'O');
            sonra = once.Replace('Ü', 'U');
            once = sonra.Replace('Ş', 'S');
            sonra = once.Replace('Ğ', 'G');
            once = sonra.Replace('Ç', 'C');
            if (once.Length > 20)
            {
                once = once.Substring(0, 20);
            }
            str = once;
            return str;
        }

        public AyarlarDB Ayarlar()
        {
            AyarlarDB cevap = new AyarlarDB();
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<AyarlarDB>("ayarlar");
            var sepets = coll.Find("{}").Limit(1).ToListAsync().Result;
            foreach (AyarlarDB s in sepets)
            {
                cevap.KrediKartiSecimi = s.KrediKartiSecimi;
                cevap.YemekCekiSecimi = s.YemekCekiSecimi;
                cevap.eArsivAktif = s.eArsivAktif;
                cevap.eFaturaAktif = s.eFaturaAktif;
                cevap.fisLimit = s.fisLimit;
                cevap.fisSiraNo = s.fisSiraNo;
                cevap.msAltLimit = s.msAltLimit;
                cevap.msUstLimit = s.msUstLimit;
                cevap.kasaNumarasi = s.kasaNumarasi;
                cevap.magazaNumarasi = s.magazaNumarasi;
                cevap.backOffice = s.backOffice;
            }

            return cevap;
        }

        public Kullanici kasiyerBilgi(string kID)
        {
            Kullanici cevap = new Kullanici();
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<Kullanici>("kullanici");
            var sepets = coll.Find("{'_id':ObjectId('" + kID + "')}").Limit(1).ToListAsync().Result;
            foreach (Kullanici s in sepets)
            {
                cevap.id = s.id;
                cevap.kasiyerAdi = s.kasiyerAdi;
                cevap.kasiyerkodu = s.maliKadi;
                cevap.kasiyerSifre = s.maliSifre;
            }

            return cevap;
        }

        public string yaziyaCevir(double tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ','); // Replace('.',',') ondalık ayracının . olma durumu için
            string lira = sTutar.Substring(0, sTutar.IndexOf(',')); //tutarın tam kısmı
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "BİR", "İKİ", "Üç", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" }; //KATRİLYON'un önüne ekleme yapılarak artırabilir.

            int grupSayisi = 6; //sayıdaki 3'lü grup sayısı. katrilyon içi 6. (1.234,00 daki grup sayısı 2'dir.)
            //KATRİLYON'un başına ekleyeceğiniz her değer için grup sayısını artırınız.

            lira = lira.PadLeft(grupSayisi * 3, '0'); //sayının soluna '0' eklenerek sayı 'grup sayısı x 3' basakmaklı yapılıyor.

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3) //sayı 3'erli gruplar halinde ele alınıyor.
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ"; //yüzler

                if (grupDegeri == "BİRYÜZ") //biryüz düzeltiliyor.
                    grupDegeri = "YÜZ";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler

                if (grupDegeri != "") //binler
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BİRBİN") //birbin düzeltiliyor.
                    grupDegeri = "BİN";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " TL ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0") //kuruş onlar
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0") //kuruş birler
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Kr.";
            else
                yazi += "SIFIR Kr.";

            return yazi;
        }

        public class Kullanici
        {
            public ObjectId id { get; set; }
            public string kasiyerkodu { get; set; }
            public string kasiyerAdi { get; set; }
            public string kasiyerSifre { get; set; }

            public string maliKadi { get; set; }

            public string maliSifre { get; set; }
        }

        public class AyarlarDB
        {
            public ObjectId id { get; set; }
            public int KrediKartiSecimi { get; set; }
            public int YemekCekiSecimi { get; set; }
            public int eFaturaAktif { get; set; }
            public int eArsivAktif { get; set; }
            public double fisLimit { get; set; }
            public int fisSiraNo { get; set; }
            public double iadeToplam { get; set; }
            public int iadeAdet { get; set; }
            public int msUstLimit { get; set; }
            public int msAltLimit { get; set; }
            public string magazaNumarasi { get; set; }
            public string kasaNumarasi { get; set; }

            public string backOffice { get; set; }
        }
    }
}