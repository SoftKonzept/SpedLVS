using System;
using System.Collections.Generic;

namespace LVS.Helper
{
    public class helper_sqlServers
    {
        //public static List<string> GetListForSqlServers()
        //{
        //    List<string> retList = new List<string>();
        //    string strReturn = string.Empty;
        //    //DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();

        //    SqlDataSourceEnumerator instance =  SqlDataSourceEnumerator.Instance;
        //    if (instance != null)
        //    {
        //        DataTable servers = instance.GetDataSources();
        //        foreach (DataRow server in servers.Rows)
        //        {
        //            string s = server["ServerName"].ToString();
        //            if (!string.IsNullOrEmpty(s))
        //            {
        //                retList.Add(s);
        //            }
        //        }
        //    }
        //    return retList;
        //}


        public static List<string> GetListForSqlServers()
        {
            List<string> retList = new List<string>();
            string ServerName = Environment.MachineName;
            Microsoft.Win32.RegistryView registryView = Environment.Is64BitOperatingSystem ? Microsoft.Win32.RegistryView.Registry64 : Microsoft.Win32.RegistryView.Registry32;
            using (Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, registryView))
            {
                Microsoft.Win32.RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        if (instanceName == "MSSQLSERVER")
                        {
                            retList.Add(ServerName);

                        }
                        else
                        {
                            retList.Add(ServerName + "\\" + instanceName);
                        }
                    }
                }
            }
            return retList;
        }
    }
}
