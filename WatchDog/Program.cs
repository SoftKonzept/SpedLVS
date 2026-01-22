using LVS;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WatchDog
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.Run(new frmMain());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
            // here you can log the exception ...



            clsError Error = new clsError();
            //Error.Sys = new clsSystem();
            //Error._GL_User = null;
            Error.Aktion = "frmMain - Main(string[] args) ";
            Error.Datum = DateTime.Now;
            Error.ErrorText = e.Exception.Message;
            Error.exceptText = e.ToString();
            Error.WriteError();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            // here you can log the exception ...

            clsError Error = new clsError();
            //Error.Sys = new clsSystem();
            //Error._GL_User = null;
            Error.Aktion = "frmMain - Main(string[] args) ";
            Error.Datum = DateTime.Now;
            Error.ErrorText = e.ExceptionObject.ToString();
            Error.exceptText = e.ToString();
            Error.WriteError();
        }

    }
}
