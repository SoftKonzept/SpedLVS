using LVS;
using System;
using System.Windows.Forms;

namespace Sped4
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
            Application.DoEvents();
            Application.Run(new frmMAIN());
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString());

            clsError Error = new clsError();
            Error.exceptText = e.ExceptionObject.ToString();
            Error.WriteError();
        }
    }
}
