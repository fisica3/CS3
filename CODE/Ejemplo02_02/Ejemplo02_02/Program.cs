using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ejemplo02_02
{
    static class Program
    {
        internal static MainForm MainFormInstance;
        internal static LinkedList<Form> listaVentanas =
            new LinkedList<Form>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainFormInstance = new MainForm();
            Application.Run(MainFormInstance);
        }
    }
}
