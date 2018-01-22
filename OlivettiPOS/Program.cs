﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace OlivettiPOS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                MessageBox.Show("Kasiyer Girişi Yapılmadı!");
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new satis(args[0].ToString()));
            }
        }
    }
}