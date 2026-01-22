using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace LVS
{
    public class clsSFTP : clsTP
    {
        private string host = null;
        private string user = null;
        private string pass = null;
        private int port = 22;
        private SftpClient sftpConnection = null;
        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;
        private int bufferSize = 2048;
        private ConnectionInfo connectInfo;
        private string strSFTPPath = string.Empty;


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
            set { ListErrorTransferFiles2 = value; }
        }

        public clsSFTP()
        {
        }

        public override void InitClass(clsJobs myJob, clsSystem mySystem)
        {
            if (myJob != null)
            {
                if (myJob.PostBy == clsJobs.const_PostBy_SFTP.ToString())
                {
                    string[] splithost = myJob.FTPServer.Split('/');
                    host = splithost[0];
                    if (splithost.Length > 1)
                    {
                        strSFTPPath = splithost[1];
                    }
                    user = myJob.FTPUser;
                    pass = myJob.FTPPass;
                    port = 22;
                    KeyboardInteractiveAuthenticationMethod keyboardAuthMethod = new KeyboardInteractiveAuthenticationMethod(user);
                    keyboardAuthMethod.AuthenticationPrompt += delegate (Object senderObject, AuthenticationPromptEventArgs eventArgs)
                    {
                        foreach (var prompt in eventArgs.Prompts)
                        {

                            if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase))
                            {
                                prompt.Response = pass;

                            }

                        }

                    };
                    var passwordAuthMethod = new PasswordAuthenticationMethod(user, pass);
                    connectInfo = new ConnectionInfo(host, port, user, passwordAuthMethod, keyboardAuthMethod);
                }
            }
        }

        ///<summary>clssftp / CheckConnection</summary>
        ///<remarks>Initialisierung der SFTP-Connection. Die einzelnen Parameter werden aus der 
        ///         config.ini ermittelt.</remarks>
        ///<return>bool for aktiv connection</param>
        public override bool CheckConnection()
        {
            bool bReConnection = true;
            //sftpConnection = new SftpClient(host, user, pass);
            sftpConnection = new SftpClient(connectInfo); //host, port, user, pass);
            sftpConnection.Connect();

            if (sftpConnection.IsConnected == true)
            {

                sftpConnection.Disconnect();
            }
            else
            {
                bReConnection = false;
            }
            return bReConnection;
        }
        ///<summary>clsftp / DownloadFilesSFTP</summary>
        ///<remarks>Download mittels SFTP-Connection. Die einzelnen Parameter werden aus der 
        ///         config.ini ermittelt.</remarks>
        ///<return></param>
        public override void DownloadFiles(ref clsJobs myJob)
        {


            //Liste für die zu löschenden Files auf dem FTP Server initialisieren
            this.ListTransferedFilesToDelete2 = new List<string>();

            this.ListErrorTransferFiles2 = new List<string>();
            // Aufbau der SFTP Verbindung
            SftpClient sftp = new SftpClient(connectInfo); //host, port, user, pass);
            sftp.Connect();

            if (sftp.IsConnected == true)
            {
                sftp.ChangeDirectory(strSFTPPath);
                //("SFTP PFad gewechselt");
                var listDir = sftp.ListDirectory(sftp.WorkingDirectory);

                foreach (var fi in listDir)
                {
                    // filtere alle Verzeichnisse
                    if (fi.IsDirectory == false)
                    {
                        if (fi.Name.IndexOf(myJob.SearchFileName) > -1)
                        //if (fi.Name.IndexOf(".gz") > -1)
                        {
                            //var file = File.OpenWrite("C:\\\\develop\\test" + "\\" + fi.Name);
                            var file = File.OpenWrite(myJob.PathDirectory + "\\" + fi.Name);
                            try
                            {
                                sftp.DownloadFile(fi.Name, file);
                                //var irgendwas = sftp.GetStatus(fi.Name);
                                if (fi.Length == file.Length)
                                {
                                    ListTransferedFilesToDelete2.Add(fi.Name);
                                }
                                else
                                {
                                    ListErrorTransferFiles2.Add(fi.Name);
                                }
                                file.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.ToString());
                            }
                        }

                    }
                }
                //Delete Files nach dem alle Dateien übertragen wurden
                foreach (string fName in ListTransferedFilesToDelete)
                {
                    sftp.DeleteFile(fName);
                }
                sftp.Disconnect();
            }
        }





        ///<summary>clsftp / UploadFiles</summary>
        ///<remarks></remarks>
        ///<return></param>
        public override void UploadFiles(ref clsJobs myJob, List<string> myListToUpload = null)
        {
            Dictionary<string, string> dictUriDest = new Dictionary<string, string>();
            //Erstellen einer List mit den Uri-Verbindungs - Destination Daten
            foreach (string FilePath in myListToUpload)
            {
                string strFileName = Path.GetFileName(FilePath);
                // new Uri("ftp://" + myJob.FTPServer + "/" + strFileName);
                dictUriDest.Add(FilePath, strFileName);
            }

            ////Liste für die zu löschenden Files auf dem FTP Server initialisieren
            this.ListTransferedFilesToDelete2 = new List<string>();
            this.ListErrorTransferFiles2 = new List<string>();

            //List UriDest durchlaufen und jede Datei übermitteln
            foreach (KeyValuePair<string, string> item in dictUriDest)
            {
                try
                {
                    SftpClient sftp = new SftpClient(connectInfo); //host, port, user, pass);

                    sftp.Connect();

                    //FileStream localFileStream = File.OpenRead(item.Key);
                    FileStream localFileStream = new FileStream(item.Key, FileMode.Open);
                    sftp.BufferSize = (uint)localFileStream.Length;
                    sftp.ChangeDirectory(strSFTPPath);
                    Console.WriteLine("SFTP PFad gewechselt");
                    sftp.UploadFile(localFileStream, Path.GetFileName(item.Key));

                    Renci.SshNet.Sftp.SftpFile fi = sftp.Get(Path.GetFileName(item.Key));
                    if (fi.Length == localFileStream.Length)
                    {

                        ListTransferedFilesToDelete2.Add(item.Key);
                    }
                    else
                    {
                        ListErrorTransferFiles2.Add(item.Key);
                    }
                    localFileStream.Close();





                    sftp.Disconnect();



                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        public void DeleteFile(string strDelete)
        {
            try
            {
                SftpClient sftp = new SftpClient(connectInfo); //host, port, user, pass);
                sftp.Connect();
                sftp.DeleteFile(strDelete);
                sftp.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            { }
            return;
        }
    }
}
