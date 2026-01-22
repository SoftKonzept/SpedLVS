using System.ComponentModel;
using System.Configuration.Install;

namespace WatchDogService
{
    [RunInstaller(true)]
    public partial class WDProjectInstaller : System.Configuration.Install.Installer
    {
        public WDProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstallerWD_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
