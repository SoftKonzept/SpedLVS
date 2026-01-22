using LVS;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Communicator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(args.ToString());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new frmMainCom());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            clsError Error = new clsError();
            //Error.Sys = new clsSystem();
            //Error._GL_User = null;
            Error.Aktion = "frmMainCom - Main(string[] args) ";
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
            //MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            // here you can log the exception ...

            clsError Error = new clsError();
            //Error.Sys = new clsSystem();
            //Error._GL_User = null;
            Error.Aktion = "frmMainCom - Main(string[] args) ";
            Error.Datum = DateTime.Now;
            Error.ErrorText = e.ExceptionObject.ToString();
            Error.exceptText = e.ToString();
            Error.WriteError();

            clsMail ErrorMail = new clsMail();
            ErrorMail.InitClass(new Globals._GL_USER(), null);
            ErrorMail.Subject = "Unhandled UI Exception - Error Mail";
            string strMes = "frmMainCom - Main(string[] args) " + Environment.NewLine;
            strMes += "ExceptionObject: " + e.ExceptionObject.ToString() + Environment.NewLine;
            strMes += "e.ToString(): " + e.ToString() + Environment.NewLine;
            ErrorMail.Message = strMes;
            ErrorMail.SendError();

        }



    }
}
