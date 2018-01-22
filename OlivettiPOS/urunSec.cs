using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlivettiPOS
   {

    public partial class urunSec : Form
    {
        public string urunBarkod = "";
        functions fonksiyonlar = new functions();
        int sayac =0;
        int islem = 0;
        string eskiBarkod = "";
        private int fcs = 1;
        public string kasiyerID = "";


        public urunSec(string kasyer)
        {
            kasiyerID = kasyer;
            InitializeComponent();

            UrunleriYukle();

            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.form_keyPress);
            
        }


        private void form_keyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show(e.KeyChar.ToString());
        }

        private void textEdit1_Click(object sender, EventArgs e)
        {
            fcs = 1;
        }

        private void textEdit2_Click(object sender, EventArgs e)
        {
            fcs = 2;

        }
        private void UrunleriYukle()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Olivetti");
            var coll = db.GetCollection<hizliUrun>("hizliUrun");
            var sepets = coll.Find("{}").Sort("{_id:1}").Limit(56).ToListAsync().Result;
            int sira = 0;
            foreach (hizliUrun s in sepets)
            {
                sira = sira + 1;
                this.Controls["simpleButton" + sira].Text = s.urunAdi;
                this.Controls["simpleButton" + sira].Tag = s.urunBarkod;
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var client = new MongoClient();
                var db = client.GetDatabase("Olivetti");
                var coll = db.GetCollection<stk>("stk");
                var stoks = coll.Find("{'Barkod.barkod':'" + textEdit1.Text + "'}").Limit(1).ToListAsync().Result;
                int adet = 0;
                foreach (stk s in stoks)
                {
                    adet = adet + 1;
                    textEdit2.Text = s.stkAdKisa;


                }
                if (adet == 0)
                {
                    textEdit2.Text = "";
                }
            }
        }
    

        public class hizliUrun
        {
            public ObjectId id { get; set; }
            public string urunBarkod { get; set; }
            public string urunAdi { get; set; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
           UrunSec(button);
        }

        private void UrunSec(DevExpress.XtraEditors.SimpleButton btn)
        {
            try
            {
                if (islem == 0)
                {
                        //satis frm = new satis(kasiyerID);
                        //frm.sepetOku();
                        //frm.SepeteEkle(btn.Tag.ToString());
                    this.urunBarkod = btn.Tag.ToString();
                    this.DialogResult = DialogResult.OK;
                        this.Close();
                    
                    
                }
                if (islem == 1)
                {
                    groupControl1.Visible = true;
                    textEdit1.Text = btn.Tag.ToString();
                    textEdit2.Text =  btn.Text.ToString();
                    eskiBarkod = btn.Tag.ToString();

                }
                if (islem == 2)
                {
                    if (btn.Text.ToString() != "")
                    {
                        if (fonksiyonlar.OnayGoster("Ürün Silme", btn.Text.ToString() + " adlı ürün listeden silinecek. Onaylıyor musunuz?", 0, "İPTAL", "ONAYLA") == "OK")
                        {
                            var client = new MongoClient();
                            var db = client.GetDatabase("Olivetti");
                            var coll = db.GetCollection<hizliUrun>("hizliUrun");
                            var sepets = coll.Find("{'urunBarkod':'" + btn.Tag.ToString() + "'}").ToListAsync().Result;
                            foreach (hizliUrun s in sepets)
                            {

                                coll.DeleteOne(a => a.id == s.id);

                            }
                            btn.Text = "";
                            btn.Tag = "";
                            
                        }
                     
                    }
                    UrunleriYukle();
                }
                
            }
            catch
            {

            }
            
           
        }

        private void urunSec_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton21_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton27_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton28_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton29_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton30_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton31_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton32_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton33_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton34_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton35_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton36_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton37_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton38_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton39_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton40_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton41_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton42_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton43_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton44_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton45_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton46_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton47_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton48_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton49_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton50_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton51_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton52_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton53_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton54_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton55_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton56_Click(object sender, EventArgs e)
        {
            var button = (DevExpress.XtraEditors.SimpleButton)sender;
            UrunSec(button);
        }

        private void simpleButton93_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void simpleButton58_Click(object sender, EventArgs e)
        {
            islem = 2;
        }

        private void simpleButton57_Click(object sender, EventArgs e)
        {
            islem = 1;
            simpleButton57.BackColor = Color.AntiqueWhite;
        }

        private void simpleButton60_Click(object sender, EventArgs e)
        {
            textEdit1.Text = "";
            textEdit2.Text = "";
            groupControl1.Visible = false;
        }

        private void simpleButton59_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text != "")
            {
                if (eskiBarkod != "")
                {
                    var client = new MongoClient();
                    var db = client.GetDatabase("Olivetti");
                    var coll = db.GetCollection<hizliUrun>("hizliUrun");
                    var update = Builders<hizliUrun>.Update
                        .Set("urunAdi", textEdit2.Text)
                        .Set("urunBarkod", textEdit1.Text);

                    coll.UpdateOne("{'urunBarkod':'" + eskiBarkod + "'}", update);

                }
                else
                {
                    var client = new MongoClient();
                    var db = client.GetDatabase("Olivetti");
                    var coll = db.GetCollection<hizliUrun>("hizliUrun");
                    hizliUrun urun = new hizliUrun();
                    urun.urunAdi = textEdit2.Text;
                    urun.urunBarkod = textEdit1.Text;
                    coll.InsertOne(urun);

                }
                textEdit1.Text = "";
                textEdit2.Text = "";
            }
           
            UrunleriYukle();
            groupControl1.Visible = false;
           
        }

        private void simpleButton66_Click(object sender, EventArgs e)
        {
            Send("7");
        }
        private void Send(string key)
        {
            switch (this.fcs)
            {
                case 1:
                    textEdit1.Select();
                    break;
                case 2:
                    textEdit2.Select();
                    break;
            }
            SendKeys.Send(key);
        }

       


        private void simpleButton73_Click(object sender, EventArgs e)
        {
            Send("1");
        }

        private void simpleButton71_Click(object sender, EventArgs e)
        {
            Send("2");
        }

        private void simpleButton70_Click(object sender, EventArgs e)
        {
            Send("3");
        }

        private void simpleButton69_Click(object sender, EventArgs e)
        {
            Send("4");
        }

        private void simpleButton68_Click(object sender, EventArgs e)
        {
            Send("5");
        }

        private void simpleButton67_Click(object sender, EventArgs e)
        {
            Send("6");
        }

        private void simpleButton65_Click(object sender, EventArgs e)
        {
            Send("8");
        }

        private void simpleButton64_Click(object sender, EventArgs e)
        {
            Send("9");
        }

        private void simpleButton63_Click(object sender, EventArgs e)
        {
            Send(".");
        }

        private void simpleButton62_Click(object sender, EventArgs e)
        {
            Send("0");
        }

        private void simpleButton61_Click(object sender, EventArgs e)
        {
            Send("{BACKSPACE}");
        }

        private void simpleButton72_Click(object sender, EventArgs e)
        {
            Send("{ENTER}");
        }
    }
}
