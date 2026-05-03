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
            // Unhandled exceptions zentral behandeln
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Fonts vor der PDF-/Report-Erzeugung registrieren
            try
            {
                // kopiert (per MSBuild) und registriert Liberation Sans im Output\Fonts
                LVS.ZUGFeRD.TelerikReporting_FontRegistration.RegisterLiberationSansFonts();
            }
            catch (Exception ex)
            {
                // Loggen und ggf. Benutzer informieren, aber Anwendung starten
                System.Diagnostics.Debug.WriteLine("Font registration failed: " + ex);
                MessageBox.Show("Warnung: Schriftregistrierung fehlgeschlagen.\n" + ex.Message, "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
