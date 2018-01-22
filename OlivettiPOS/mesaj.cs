using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;

namespace OlivettiPOS
{
    public partial class mesaj : Form
    {
        private const string _dllLocation = "AsrCore.dll";

        [DllImport(_dllLocation)]
        public extern static bool AsrLibBuzzer(bool active);

        [DllImport(_dllLocation)]
        public extern static bool AsrLibDllInit();

        public static bool Buzzer(bool active)
        {
            var y = AsrLibDllInit();
            bool x = false;
            try
            {
                if (active)
                {
                    x = AsrLibBuzzer(active);
                    System.Threading.Thread.Sleep(1000);
                    x = AsrLibBuzzer(!active);
                }
                else
                {
                    x = AsrLibBuzzer(active);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return x;
        }

        private int sayac = 0;

        public mesaj()
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
            if (ConfigurationManager.AppSettings["DebugMode"].ToString() != "55")
            {
                if (sayac == 0)
                {
                    Buzzer(true);
                    Buzzer(false);
                }
            }

            sayac = sayac + 1;
            if (int.Parse(label1.Text) < sayac)
            {
                timer1.Stop();
                Close();
            }
            if (sayac > 1)
            {
                timer1.Interval = 1000;
            }
        }
    }
}