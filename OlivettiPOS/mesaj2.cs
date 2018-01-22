using System;
using System.Windows.Forms;

namespace OlivettiPOS
{
    public partial class mesaj2 : Form
    {
        private int sayac = 0;

        public mesaj2()
        {
            InitializeComponent();
            Opacity = 0.90;
            timer1.Enabled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac = sayac + 1;
            if (int.Parse(label1.Text) < sayac)
            {
                timer1.Stop();
                Close();
            }
        }
    }
}