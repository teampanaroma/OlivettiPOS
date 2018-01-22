using System;
using System.Windows.Forms;

namespace OlivettiPOS
{
    public partial class onay : Form
    {
        private int sayac = 0;

        public onay()
        {
            InitializeComponent();
            Opacity = 0.90;
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void onay_Load(object sender, EventArgs e)
        {
            memoEdit1.Select();
        }
    }
}