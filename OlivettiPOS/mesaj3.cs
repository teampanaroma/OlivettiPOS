using System;
using System.Windows.Forms;

namespace OlivettiPOS
{
    public partial class mesaj3 : Form
    {
        private int sayac = 0;

        public mesaj3()
        {
            InitializeComponent();
            Opacity = 0.90;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}