using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace LVS
{
    public class clsftp : clsTP
    {
        public clsftp Copy()
        {
            return (clsftp)this.MemberwiseClone();
        }


        public clsSystem Sys;
        private string host = null;
        private string user = null;
        private string pass = null;
        private bool FtpUsePassiveMode { get; set; }
        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;
        private int bufferSize = 2048;


        public string WD_InfoText { get; set; }

        internal List<string> ListTransferedFilesToDelete2;
        public override List<string> ListTransferedFilesToDelete
        {
            get
            {
                return ListTransferedFilesToDelete2;
            }
            set
            {
                ListTransferedFilesToDelete2 = value;
            }
        }
        internal List<string> ListErrorTransferFiles2;
        public override List<string> ListErrorTransferFiles
        {
            get
            {
                return ListErrorTransferFiles2;
            }
            set
            {
                ListErrorTransferFiles2 = value;
            }
        }
        public string WDClientMC { get; set; }
        public string WD_FileNameDown
        {
            get
            {
                //return "WD_" + this.Sys.Client.MatchCode.TrimEnd('_');
                return "WD_" + WDClientMC + ".txt";
            }
        }

        public string WD_DownloadPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\Watchdog\\";
            }
        }
        public string WD_DownloadFilePath
        {
            get
            {
                return this.WD_DownloadPath + this.WD_FileNameDown;
            }
        }

        /* Construct Object */
        public clsftp()
        {

        }
        ///<summary>clsftp / InitClass</summary>
        ///<remarks></remarks>
        public override void InitClass(clsJobs myJob, clsSystem mySystem)
        {
            if (myJob is clsJobs)
            {
                if (myJob.PostBy == clsJobs.const_PostBy_FTP.ToString())
                {
                    host = myJob.FTPServer;
                    user = myJob.FTPUser;
                    pass = myJob.FTPPass;
                    FtpUsePassiveMode = myJob.FTPUsePassiveMode;
                }
            }
            else
            {
                SetSystemToClass(mySystem, true);
            }
        }
        public void InitClass(clsJobs myJob, clsSystem mySystem, bool mySetFtpSettings)
        {
            if (myJob is clsJobs)
            {
                if (myJob.PostBy == clsJobs.const_PostBy_FTP.ToString())
                {
                    host = myJob.FTPServer;
                    user = myJob.FTPUser;
                    pass = myJob.FTPPass;
                    FtpUsePassiveMode = myJob.FTPUsePassiveMode;
                }
            }
            SetSystemToClass(mySystem, mySetFtpSettings);
        }

        private void SetSystemToClass(clsSystem mySystem, bool mySetFtpSettings)
        {
            if (mySystem is clsSystem)
            {
                this.Sys = mySystem;

                if (mySetFtpSettings)
                {
                    host = this.Sys.con_FtpServer;
                    if (this.Sys.DebugModeCOM)
                    {
                        host += "DEBUG";
                    }
                    else
                    {
                        //if (this.Sys.VE_IsWatchDog)
                        //{
                        //    host += this.WDClientMC;
                        //}
                        //else
                        //{
                        //    host += Regex.Replace(this.Sys.strClient, "_", "");
                        //}
                        if (this.Sys.VE_IsWatchDog)
                        {
                            if (!host.EndsWith("/"))
                            {
                                host += "/";
                            }
                            host += this.WDClientMC;
                        }
                        else
                        {
                            host += Regex.Replace(this.Sys.strClient, "_", "");
                        }
                    }
                    user = this.Sys.con_FtpUser;
                    pass = this.Sys.con_FtpPass;
                }
            }
        }

        ///<summary>clsftp / CheckConnection</summary>
        ///<remarks>Initialisierung der FTP-Connection. Die einzelnen Parameter werden aus der 
        ///         config.ini ermittelt.</remarks>
        ///<return>bool for aktiv connection</param>
        public override bool CheckConnection()
        {

            //if (this.Sys.VE_IsWatchDog)
            //{
            //    this.WD_InfoText = string.Empty;
            //}
            bool bReConnection = true;

            try
            {
                Uri uri = new Uri("ftp://" + host);
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpRequest.Credentials = new NetworkCredential(user, pass);

                ftpRequest.UseBinary = true; //original
                //ftpRequest.UsePassive = false; // true; // original
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = false;

                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
            }
            catch (Exception ex)
            {
                bReConnection = false;
                Console.WriteLine(ex.ToString());
                if (this.Sys.VE_IsWatchDog)
                {
                    this.WD_InfoText = "[FTP-Server]: 'ftp://" + host + "' ";
                }
            }
            return bReConnection;
        }
        ///<summary>clsftp / download</summary>
        ///<remarks>Initialisierung der FTP-Connection. Die einzelnen Parameter werden aus der 
        ///         config.ini ermittelt.</remarks>
        ///<return>bool for aktiv connection</param>
        public override void DownloadFiles(ref clsJobs myJob)
        {
            Uri uriSource = new Uri("ftp://" + myJob.FTPServer + "/" + myJob.FileName);
            //auslesen der vorliegenden Dateien im FTP Ordner
            List<string> listFiles = GetFileListFromFTPDirectory(uriSource);

            //Liste für die zu löschenden Files auf dem FTP Server initialisieren
            ListTransferedFilesToDelete2 = new List<string>();

            //Liste jetzt Download der Files
            for (Int32 i = 0; i <= listFiles.Count - 1; i++)
            {
                string strDownloadFileName = listFiles[i].ToString();
                if (strDownloadFileName.IndexOf(myJob.SearchFileName) > -1)
                {
                    Uri uriSourceFile = new Uri("ftp://" + myJob.FTPServer + "/" + strDownloadFileName);

                    ftpRequest = FtpWebRequest.Create(uriSourceFile) as FtpWebRequest;
                    ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    ftpRequest.Credentials = new NetworkCredential(user, pass);
                    //ftpRequest.UsePassive = true;
                    ftpRequest.UsePassive = FtpUsePassiveMode;
                    ftpRequest.UseBinary = true;
                    ftpRequest.KeepAlive = false;
                    FtpWebResponse response = ftpRequest.GetResponse() as FtpWebResponse;
                    Stream responseStream = response.GetResponseStream();
                    FileStream fs = new FileStream(myJob.PathDirectory + "\\" + strDownloadFileName, FileMode.Create);
                    try
                    {
                        byte[] buffer = new byte[2048];
                        int ReadCount = responseStream.Read(buffer, 0, buffer.Length);
                        while (ReadCount > 0)
                        {
                            fs.Write(buffer, 0, ReadCount);
                            ReadCount = responseStream.Read(buffer, 0, buffer.Length);
                        }
                        //Wenn die Übertragung OK war, dann muss der Dateiname in der listFilesToDelete gespeichert
                        //und anschließend gelöscht werden
                        if (response.StatusCode == FtpStatusCode.ClosingData)
                        {
                            ListTransferedFilesToDelete2.Add(strDownloadFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        string strEx = ex.ToString();
                    }
                    finally
                    {
                        fs.Close();
                        responseStream.Close();
                    }
                }
            }
            //Delete Files nach dem alle Dateien übertragen wurden
            for (Int32 x = 0; x <= ListTransferedFilesToDelete2.Count - 1; x++)
            {
                Uri uriToDelete = new Uri("ftp://" + myJob.FTPServer + "/" + ListTransferedFilesToDelete2[x].ToString());
                DeleteFile(uriToDelete);
            }
        }
        ///<summary>clsftp / download</summary>
        ///<remarks>Initialisierung der FTP-Connection. Die einzelnen Parameter werden aus der 
        ///         config.ini ermittelt.</remarks>
        ///<return>bool for aktiv connection</param>
        public string Download_WDFiles()
        {
            WD_InfoText = string.Empty;
            string strDest = "ftp://" + this.host + "/" + this.WD_FileNameDown;
            Uri uriSource = new Uri(strDest);

            //auslesen der vorliegenden Dateien im FTP Ordner
            List<string> listFiles = GetFileListFromFTPDirectory(uriSource);
            if (listFiles.Count > 0)
            {
                //Liste jetzt Download der Files
                for (Int32 i = 0; i <= listFiles.Count - 1; i++)
                {
                    string strDownloadFileName = listFiles[i].ToString();
                    if (strDownloadFileName.IndexOf(this.WD_FileNameDown) > -1)
                    {
                        strDest = string.Empty;
                        strDest = "ftp://" + this.host + "/" + this.WD_FileNameDown;
                        Uri uriSourceFile = new Uri(strDest);

                        ftpRequest = FtpWebRequest.Create(uriSourceFile) as FtpWebRequest;
                        ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        ftpRequest.Credentials = new NetworkCredential(user, pass);
                        //ftpRequest.UsePassive = true;
                        ftpRequest.UsePassive = FtpUsePassiveMode;
                        ftpRequest.UseBinary = true;
                        ftpRequest.KeepAlive = false;
                        FtpWebResponse response = ftpRequest.GetResponse() as FtpWebResponse;
                        Stream responseStream = response.GetResponseStream();
                        FileStream fs = new FileStream(this.WD_DownloadFilePath, FileMode.Create);
                        try
                        {
                            byte[] buffer = new byte[2048];
                            int ReadCount = responseStream.Read(buffer, 0, buffer.Length);
                            while (ReadCount > 0)
                            {
                                fs.Write(buffer, 0, ReadCount);
                                ReadCount = responseStream.Read(buffer, 0, buffer.Length);
                            }
                            WD_InfoText += "Download-File: " + strDest + Environment.NewLine;
                        }
                        catch (Exception ex)
                        {
                            string strEx = ex.ToString();
                            WD_InfoText += "Download-File-ERROR: " + ex.ToString() + Environment.NewLine;
                        }
                        finally
                        {
                            fs.Close();
                            responseStream.Close();
                        }
                    }
                }
            }//if
            else
            {
                WD_InfoText = "Es liegen keine Dateien zur Verarbeitung vor!";
            }
            return WD_InfoText;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myJob"></param>
        /// <param name="myListToUpload"></param>
        /// <returns></returns>
        public string Upload_WDFiles(ref clsJobs myJob, List<string> myListToUpload)
        {
            WD_InfoText = string.Empty;
            Dictionary<string, Uri> dictUriDest = new Dictionary<string, Uri>();
            //Erstellen einer List mit den Uri-Verbindungs - Destination Daten
            foreach (string FilePath in myListToUpload)
            {
                string strFileName = Path.GetFileName(FilePath);
                try
                {
                    if (!myJob.FTPServer.EndsWith("/"))
                    {
                        myJob.FTPServer += "/";
                    }

                    string strDest = string.Empty;

                    string strMatchcode = InitValueCommunicator.InitValueCom_Client.Matchcode();

                    //if (this.Sys.DebugModeCOM)
                    //{
                    //    strDest = "ftp://" + myJob.FTPServer + "DEBUG/" + strFileName;
                    //}
                    //else
                    //{

                    //    //strDest = "ftp://" + myJob.FTPServer + Regex.Replace(this.Sys.strClient, "_", "") + "/" + strFileName;
                    //}
#if DEBUG
                    strDest = "ftp://" + myJob.FTPServer + "DEBUG/" + strFileName;
#else
                    strDest = "ftp://" + myJob.FTPServer + Regex.Replace(strMatchcode, "_", "") + "/" + strFileName;
#endif

                    Uri uriDest = new Uri(strDest);
                    WD_InfoText += "Upload-Path: " + strDest;
                    dictUriDest.Add(FilePath, uriDest);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    WD_InfoText += ex.ToString() + Environment.NewLine;
                }
            }
            //List UriDest durchlaufen und jede Datei übermitteln
            foreach (KeyValuePair<string, Uri> item in dictUriDest)
            {
                try
                {
                    /* Create an FTP Request */
                    FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(item.Value);

                    /* Log in to the FTP Server with the User Name and Password Provided */
                    ftpRequest.Credentials = new NetworkCredential(myJob.FTPUser, myJob.FTPPass);
                    /* When in doubt, use these options */
                    ftpRequest.UseBinary = true;
                    //ftpRequest.UsePassive = true;
                    ftpRequest.UsePassive = FtpUsePassiveMode;
                    ftpRequest.KeepAlive = false;
                    //FtpWebResponse response = ftpRequest.GetResponse() as FtpWebResponse;
                    /* Specify the Type of FTP Request */
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    try
                    {
                        /* Establish Return Communication with the FTP Server */
                        ftpStream = ftpRequest.GetRequestStream();

                        /* Open a File Stream to Read the File for Upload */
                        //FileStream localFileStream = new FileStream(item.Key, FileMode.Create);
                        FileStream localFileStream = File.OpenRead(item.Key);
                        /* Buffer for the Downloaded Data */
                        // byte[] byteBuffer = new byte[bufferSize];
                        byte[] byteBuffer = new byte[localFileStream.Length];
                        int bytesSent = localFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                        if (bytesSent > 0)
                        {
                            /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                            try
                            {
                                while (bytesSent != 0)
                                {
                                    ftpStream.Write(byteBuffer, 0, bytesSent);
                                    bytesSent = localFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                                }
                                ftpStream.Close();
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.ToString()); 
                            }
                            //Wenn die Übertragung OK war, dann muss der Dateiname in der listFilesToDelete gespeichert
                            //und anschließend gelöscht werden
                            //FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                            FtpWebResponse responseUp = (FtpWebResponse)ftpRequest.GetResponse();
                        }
                        localFileStream.Close();
                    }
                    catch (Exception ex)
                    {
                        string strError = ex.Message.ToString();

                    }
                    ftpStream.Close();
                    ftpRequest.Abort();
                    ftpRequest = null;
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            return WD_InfoText;
        }
        ///<summary>clsftp / UploadFiles</summary>
        ///<remarks></remarks>
        ///<return></param>
        public override void UploadFiles(ref clsJobs myJob, List<string> myListToUpload)
        {
            Dictionary<string, Uri> dictUriDest = new Dictionary<string, Uri>();
            //Erstellen einer List mit den Uri-Verbindungs - Destination Daten
            foreach (string FilePath in myListToUpload)
            {
                string strFileName = Path.GetFileName(FilePath);
                try
                {
                    Uri uriDest = new Uri("ftp://" + myJob.FTPServer + "/" + strFileName);
                    dictUriDest.Add(FilePath, uriDest);
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }

            //Liste für die zu löschenden Files auf dem FTP Server initialisieren
            ListTransferedFilesToDelete2 = new List<string>();
            ListErrorTransferFiles2 = new List<string>();

            //List UriDest durchlaufen und jede Datei übermitteln
            foreach (KeyValuePair<string, Uri> item in dictUriDest)
            {
                try
                {
                    /* Create an FTP Request */
                    FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(item.Value);

                    /* Log in to the FTP Server with the User Name and Password Provided */
                    ftpRequest.Credentials = new NetworkCredential(myJob.FTPUser, myJob.FTPPass);
                    /* When in doubt, use these options */
                    ftpRequest.UseBinary = true;
                    //ftpRequest.UsePassive = true;
                    ftpRequest.UsePassive = FtpUsePassiveMode;
                    ftpRequest.KeepAlive = false;
                    //FtpWebResponse response = ftpRequest.GetResponse() as FtpWebResponse;
                    /* Specify the Type of FTP Request */
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    try
                    {
                        /* Establish Return Communication with the FTP Server */
                        ftpStream = ftpRequest.GetRequestStream();

                        /* Open a File Stream to Read the File for Upload */
                        //FileStream localFileStream = new FileStream(item.Key, FileMode.Create);
                        FileStream localFileStream = File.OpenRead(item.Key);
                        /* Buffer for the Downloaded Data */
                        // byte[] byteBuffer = new byte[bufferSize];
                        byte[] byteBuffer = new byte[localFileStream.Length];
                        int bytesSent = localFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                        if (bytesSent > 0)
                        {
                            /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                            try
                            {
                                while (bytesSent != 0)
                                {
                                    ftpStream.Write(byteBuffer, 0, bytesSent);
                                    bytesSent = localFileStream.Read(byteBuffer, 0, byteBuffer.Length);
                                }
                                ftpStream.Close();
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.ToString()); 
                            }
                            //Wenn die Übertragung OK war, dann muss der Dateiname in der listFilesToDelete gespeichert
                            //und anschließend gelöscht werden
                            //FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                            FtpWebResponse responseUp = (FtpWebResponse)ftpRequest.GetResponse();
                            if (responseUp.StatusCode == FtpStatusCode.ClosingData)
                            {
                                ListTransferedFilesToDelete2.Add(item.Key);
                            }
                            else
                            {
                                ListErrorTransferFiles2.Add(item.Key);
                            }
                        }
                        else
                        {
                            ListErrorTransferFiles2.Add(item.Key);
                        }
                        localFileStream.Close();
                    }
                    catch (Exception ex)
                    {
                    }

                    ftpStream.Close();

                    //responseUp.Close();
                    ftpRequest.Abort();
                    try
                    {
                        //responseUp = (FtpWebResponse)ftpRequest.GetResponse();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    ftpRequest = null;
                    //responseUp.Close();


                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        ///<summary>clsftp / GetFileListFromFTPDirectory</summary>
        ///<remarks>Ermittel die vorliegenden Dateien in dem FTP Quellordner und schreibt diese
        ///         in eine Liste</remarks>
        ///<return>List mit den vorliegenden Dateinamen</param>
        private List<string> GetFileListFromFTPDirectory(Uri myUriSource)
        {
            List<string> listFiles = new List<string>();
            //Create FTP request
            try
            {
                ftpRequest = FtpWebRequest.Create(myUriSource) as FtpWebRequest;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.UseBinary = true;
                ftpRequest.KeepAlive = false;

                ftpResponse.Close();
                ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                Stream responseStream = ftpResponse.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                while (!reader.EndOfStream)
                {
                    listFiles.Add(reader.ReadLine());
                }
                //Clean-up
                reader.Close();
                responseStream.Close(); //redundant
                ftpResponse.Close();
            }
            catch (Exception ex)
            {
                WD_InfoText += "Error: GetFileListFromFTPDirectory()" + Environment.NewLine +
                               "Execption: " + Environment.NewLine +
                               ex.ToString();
            }
            finally
            {

            }
            return listFiles;
        }
        ///<summary>clsftp / DeleteFile</summary>
        ///<remarks>Datei wird vom FTP-Verzeichnis gelöscht</remarks>
        ///<return>List mit den vorliegenden Dateinamen</param>
        public void DeleteFile(Uri uriDelete)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(uriDelete);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                /* Establish Return Communication with the FTP Server */
                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            { }
            return;
        }





        /* Rename File */
        public void rename(string currentFileNameAndPath, string newFileName)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                /* Rename the File */
                ftpRequest.RenameTo = newFileName;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return;
        }

        /* Create a New Directory on the FTP Server */
        public void createDirectory(string newDirectory)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return;
        }

        /* Get the Date/Time a File was Created */
        public string getFileCreatedDateTime(string fileName)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string fileInfo = null;
                /* Read the Full Response Stream */
                try { fileInfo = ftpReader.ReadToEnd(); }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return File Created Date Time */
                return fileInfo;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* Get the Size of a File */
        public string getFileSize(string fileName)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string fileInfo = null;
                /* Read the Full Response Stream */
                try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return File Size */
                return fileInfo;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* List Directory Contents File/Folder Name Only */
        public string[] directoryListSimple(string directory)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return new string[] { "" };
        }

        /* List Directory Contents in Detail (Name, Size, Created, etc.) */
        public string[] directoryListDetailed(string directory)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.UsePassive = true;
                ftpRequest.UsePassive = FtpUsePassiveMode;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return new string[] { "" };
        }


    }

}
