using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace kodeopgaveFiskeristyrelse
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void init()
        {
            String filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\src/Data_til_rapportering_af_fiskeri.csv");
            var lines = File.ReadAllLines(filePath);
            ParseToDB ptdb = new ParseToDB();
            ptdb.parse(lines);

        }
    }
}