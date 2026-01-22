using LvsScan.Portable.Proccesses;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LvsScan.Portable.ViewModels.Settings
{
    public class NetworkCheckViewModel : BaseViewModel
    {
        public NetworkCheckViewModel()
        {

        }
        private NetworkAnalyzing networkAnalyzing = new NetworkAnalyzing();

        public async Task DoAnalyzing()
        {
            networkAnalyzing = new NetworkAnalyzing();
            await networkAnalyzing.DoAnalyse();
            NetworkAnalyzingResult = new ObservableCollection<string>(networkAnalyzing.NetworkAnalyzingResult);
            string strNetworkInfo = string.Empty;
            foreach (string item in networkAnalyzing.NetworkAnalyzingResult)
            {
                strNetworkInfo += item + Environment.NewLine;
            }
            NetworkInfo = strNetworkInfo;
        }

        private ObservableCollection<string> _NetworkAnalyzingResult;
        public ObservableCollection<string> NetworkAnalyzingResult
        {
            get { return _NetworkAnalyzingResult; }
            set
            {
                SetProperty(ref _NetworkAnalyzingResult, value);
                ResultDevice = new ObservableCollection<string>(networkAnalyzing.Result_Device);
                ResultNetwork = new ObservableCollection<string>(networkAnalyzing.Result_Network);
                ResultAdapter = new ObservableCollection<string>(networkAnalyzing.Result_NetworkAdapter);
            }
        }

        private ObservableCollection<string> _ResultDevice;
        public ObservableCollection<string> ResultDevice
        {
            get { return _ResultDevice; }
            set { SetProperty(ref _ResultDevice, value); }
        }

        private ObservableCollection<string> _ResultNetwork;
        public ObservableCollection<string> ResultNetwork
        {
            get { return _ResultNetwork; }
            set { SetProperty(ref _ResultNetwork, value); }
        }


        private ObservableCollection<string> _ResultAdapter;
        public ObservableCollection<string> ResultAdapter
        {
            get { return _ResultAdapter; }
            set { SetProperty(ref _ResultAdapter, value); }
        }

        private string networkInfo;
        public string NetworkInfo
        {
            get { return networkInfo; }
            set { SetProperty(ref networkInfo, value); }
        }
    }
}
