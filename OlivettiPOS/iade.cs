using System;
using System.Windows.Forms;

namespace OlivettiPOS
{
    public partial class iade : Form
    {
        private int sayac = 0;
        public string stkAdi = "";
        public double stkFiyati = 0;

        public iade(string stkAd, double stkFiyat)
        {
            InitializeComponent();
            Opacity = 1;
            stkAdi = stkAd;
            stkFiyati = stkFiyat;
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onay_KeyDown);
        }

        //private void onay_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (e.KeyData == Keys.Enter)
        //    {
        //        simpleButton2.PerformClick();
        //    }
        //    if (e.KeyData == Keys.Escape)
        //    {
        //        simpleButton1.PerformClick();
        //    }
        //}

        private void onay_Load(object sender, EventArgs e)
        {
            memoEdit1.Text = stkAdi;
            textEdit11.Text = stkFiyati.ToString();
            textEdit11.Select();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            stkFiyati = double.Parse(textEdit11.Text.Replace('.', ','));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            Send("1");
        }

        private void Send(string key)
        {
            textEdit11.Select();
            SendKeys.Send(key);
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            Send("0");
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            Send(".");
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

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Send("{BACKSPACE}");
        }

        private void textEdit11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton2.PerformClick();
            }
        }
    }
}