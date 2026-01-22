using Android.App;
using Android.Net.Wifi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LvsScan.Portable.Proccesses
{
    public class NetworkAnalyzing
    {
        public NetworkAnalyzing()
        {

        }

        public async Task DoAnalyse()
        {
            NetworkAnalyzingResult = new List<string>();
            Result_Device = new List<string>();
            Result_Network = new List<string>();
            Result_NetworkAdapter = new List<string>();

            GetStartInformation();

            //--- Informationen Gerät / Device
            GetDeviceInformation();

            //--- Informationen Netzwerk
            await GetNetworkInfosAsync();
            //--- Informationen Netzwerkadapter
            GetNetworkadapterInfos();


            //Zusammenführen der Listen
            foreach (string item in Result_Device)
            {
                NetworkAnalyzingResult.Add(item);
            }
            NetworkAnalyzingResult.Add(Environment.NewLine);

            foreach (string item in Result_Network)
            {
                NetworkAnalyzingResult.Add(item);
            }
            NetworkAnalyzingResult.Add(Environment.NewLine);

            foreach (string item in Result_NetworkAdapter)
            {
                NetworkAnalyzingResult.Add(item);
            }
            NetworkAnalyzingResult.Add(Environment.NewLine);

            string strInfos = string.Empty;
            strInfos = string.Empty;
            strInfos = "" +
                "Analyse beendet";
            NetworkAnalyzingResult.Add(strInfos);
        }

        public List<string> NetworkAnalyzingResult = new List<string>();
        public List<string> Result_Device = new List<string>();
        public List<string> Result_Network = new List<string>();
        public List<string> Result_NetworkAdapter = new List<string>();

        private void GetStartInformation()
        {
            string strInfos = string.Empty;
            strInfos = "Network Check!";
            NetworkAnalyzingResult.Add(strInfos);
            strInfos = string.Empty;
            strInfos = System.String.Format("{0}:{1,5} Uhr", "Datum", DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
            NetworkAnalyzingResult.Add(strInfos);
        }

        private void GetDeviceInformation()
        {
            string strInfos = string.Empty;
            strInfos = "Device Information:";
            Result_Device.Add(strInfos);
            strInfos = string.Empty;
            strInfos = string.Format("Devicename........: {0}", DeviceInfo.Name);
            Result_Device.Add(strInfos);
            strInfos = string.Empty;
            strInfos = string.Format("Devicetype........: {0}", DeviceInfo.DeviceType.ToString());
            Result_Device.Add(strInfos);
            strInfos = string.Empty;
            strInfos = string.Format("Version...........: {0}", DeviceInfo.VersionString);
            Result_Device.Add(strInfos);
            strInfos = string.Empty;
            strInfos = string.Format("Plattform.........: {0}", DeviceInfo.Platform);
            Result_Device.Add(strInfos);
            strInfos = string.Empty;
            strInfos = string.Format("Manufacturer......: {0}", DeviceInfo.Manufacturer);
            Result_Device.Add(strInfos);

            //NetworkAnalyzingResult.Add(Environment.NewLine);
        }
        private async Task GetNetworkInfosAsync()
        {
            string strInfos = string.Empty;
            string tmpIP = string.Empty;

            //--- Informationen Netzwerkdaten         
            strInfos = string.Empty;
            strInfos = "Netzwerk / Connection Information:";
            Result_Network.Add(strInfos);


            string host = System.Net.Dns.GetHostName();

            strInfos = string.Empty;
            strInfos = string.Format("Host...........: {0}", host);
            Result_Network.Add(strInfos);

            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Service.WifiService);
            LongToIP(wifiManager.ConnectionInfo.IpAddress);

            System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostByName(host);
            string IpAdresse = hostInfo.AddressList[0].ToString();

            strInfos = string.Empty;
            strInfos = string.Format("IP............: {0}", IpAdresse);
            Result_Network.Add(strInfos);

            tmpIP = string.Empty;
            tmpIP = LongToIP(wifiManager.DhcpInfo.Dns1);
            strInfos = string.Empty;
            strInfos = string.Format("DNS...........: {0}", tmpIP);
            Result_Network.Add(strInfos);

            tmpIP = string.Empty;
            tmpIP = LongToIP(wifiManager.DhcpInfo.Gateway);
            strInfos = string.Empty;
            strInfos = string.Format("Gateway.......: {0}", tmpIP);
            Result_Network.Add(strInfos);

            tmpIP = string.Empty;
            tmpIP = LongToIP(wifiManager.DhcpInfo.ServerAddress);
            strInfos = string.Empty;
            strInfos = string.Format("ServerAddress..: {0}", tmpIP);
            Result_Network.Add(strInfos);

            strInfos = string.Empty;
            strInfos = string.Format("SSID..........: {0}", wifiManager.ConnectionInfo.SSID.ToString());
            Result_Network.Add(strInfos);

            strInfos = string.Empty;
            strInfos = string.Format("MAC...........: {0}", wifiManager.ConnectionInfo.MacAddress.ToString());
            Result_Network.Add(strInfos);

            strInfos = string.Empty;
            strInfos = string.Format("BSSID..........: {0}", wifiManager.ConnectionInfo.BSSID.ToString());
            Result_Network.Add(strInfos);

            strInfos = string.Empty;
            strInfos = string.Format("Linkspeed Mbps.: {0}", wifiManager.ConnectionInfo.LinkSpeed.ToString());
            Result_Network.Add(strInfos);

            strInfos = string.Empty + Environment.NewLine;
            Result_Network.Add(strInfos);

            strInfos = "Internet / Speedtest:" + Environment.NewLine;
            Result_Network.Add(strInfos);
            strInfos = await CheckInternetSpeed();
            Result_Network.Add(strInfos);
        }

        private async Task<System.String> CheckInternetSpeed()
        {
            string internetSpeed = string.Empty;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                for (int i = 1; i <= 10; i++)
                {
                    DateTime d1 = DateTime.Now;
                    try
                    {
                        var client = new HttpClient();
                        byte[] response = await client.GetByteArrayAsync("http://www.google.com");
                        DateTime d2 = DateTime.Now;
                        internetSpeed += i.ToString() + ". Connection Speed: (kb/s) " + System.Math.Round((response.Length / 1024) / (d2 - d1).TotalSeconds, 2);

                    }
                    catch (System.Exception ex)
                    {
                        internetSpeed += i.ToString() + ".Connection Speed: unknown Exception :" + ex.Message;
                    }
                    internetSpeed += Environment.NewLine;
                    await Task.Delay(1000);
                }
            }
            else
            {
                internetSpeed = " KEINE Internet Verbindung !";
            }
            return internetSpeed;
        }

        public string LongToIP(long ipAddress)
        {
            System.Net.IPAddress tmpIp;
            if (System.Net.IPAddress.TryParse(ipAddress.ToString(), out tmpIp))
            {
                try
                {
                    System.Byte[] bytes = tmpIp.GetAddressBytes();
                    long addr = (long)BitConverter.ToInt32(bytes, 0);
                    return new System.Net.IPAddress(addr).ToString();
                }
                catch (System.Exception e) { return e.Message; }
            }
            else return string.Empty;
        }


        private void GetNetworkadapterInfos()
        {
            string strInfos = string.Empty;
            strInfos = string.Empty;
            strInfos = "NewortInferfaces:";
            NetworkAnalyzingResult.Add(strInfos);

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();

                strInfos = string.Empty;
                strInfos = string.Format("Adapter.........: {0}", adapter.Description);
                Result_NetworkAdapter.Add(strInfos);
                strInfos = string.Empty;
                strInfos = string.Format("Interface type..: {0}", adapter.NetworkInterfaceType);
                Result_NetworkAdapter.Add(strInfos);
                strInfos = string.Empty;
                strInfos = string.Format("Physical Address: {0}", adapter.GetPhysicalAddress().ToString());
                Result_NetworkAdapter.Add(strInfos);

                string versions = "";

                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }

                strInfos = string.Empty;
                strInfos = string.Format("IP version......: {0}", versions);
                Result_NetworkAdapter.Add(strInfos);


                //ShowIPAddresses(properties);

                // The following information is not useful for loopback adapters.
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }

                strInfos = string.Empty;
                strInfos = string.Format("DNS suffix......: {0}", properties.DnsSuffix);
                Result_NetworkAdapter.Add(strInfos);
            }
        }
    }
}
