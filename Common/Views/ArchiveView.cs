using Common.Models;

namespace Common.Views
{
    public class ArchiveView
    {
        public Archives Archive { get; set; }
        public string DocKey
        {
            get
            {
                string strTmp = string.Empty;
                if (Archive != null)
                {
                    strTmp = Archive.DocKey;
                }
                return strTmp;
            }
        }
        public string Extension
        {
            get
            {
                string strTmp = string.Empty;
                if (Archive != null)
                {
                    strTmp = Archive.Extension;
                }
                return strTmp;
            }
        }
        public int Id
        {
            get
            {
                int iTmp = 0;
                if (Archive != null)
                {
                    iTmp = Archive.Id;
                }
                return iTmp;
            }
        }
        public int LVSNr { get; set; } = 0;
        public int LEingangID { get; set; } = 0;
        public int LAusgangID { get; set; } = 0;

        public string FileName
        {
            get
            {
                string strTmp = string.Empty;
                if (Archive != null)
                {
                    strTmp = Archive.Filename;
                }
                return strTmp;
            }
        }



    }
}
