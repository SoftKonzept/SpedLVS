
namespace WatchDogService
{
    partial class WDProjectInstaller
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstallerWD = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstallerWD = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstallerWD
            // 
            this.serviceProcessInstallerWD.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstallerWD.Password = null;
            this.serviceProcessInstallerWD.Username = null;
            // 
            // serviceInstallerWD
            // 
            this.serviceInstallerWD.Description = "1_Sped4-WatchDog Service";
            this.serviceInstallerWD.DisplayName = "WatchDog - Sped4.WatchDog.Service";
            this.serviceInstallerWD.ServiceName = "Sped4.WatchDog.Service";
            this.serviceInstallerWD.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.serviceInstallerWD.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstallerWD_AfterInstall);
            // 
            // WDProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerWD,
            this.serviceInstallerWD});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerWD;
        private System.ServiceProcess.ServiceInstaller serviceInstallerWD;
    }
}