using System.Collections.Generic;

namespace LVS
{
    public abstract class clsTP
    {

        public abstract List<string> ListTransferedFilesToDelete { get; set; }
        public abstract List<string> ListErrorTransferFiles { get; set; }

        public abstract void InitClass(clsJobs myJob, clsSystem mySystem);
        public abstract bool CheckConnection();
        public abstract void DownloadFiles(ref clsJobs myJob);
        //public abstract string Download_WDFiles(ref clsJobs myJob);
        public abstract void UploadFiles(ref clsJobs myJob, List<string> myListToUpload);
        //public abstract string Upload_WDFiles(ref clsJobs myJob, List<string> myListToUpload);

    }
}
