using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeNewsDetectorApp
{
    static class Program
    {
        public static GoogleSearch searcher;
        public static brain b_brain = new brain();
        public static PointSystem ps_pointSystem = new PointSystem();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            searcher = new GoogleSearch();
            searcher.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            b_brain.LoadData();
            ps_pointSystem.LoadData();

            Application.Run(new Form1());

            b_brain.SaveData();
            ps_pointSystem.SaveData();
        }
    }
}
