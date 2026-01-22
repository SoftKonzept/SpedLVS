using Common.Models;

namespace Common.Views
{
    public class ImageView
    {
        public int Id
        {
            get { return Image.Id; }
        }
        public Images Image { get; set; }
        public string TableName
        {
            get { return Image.TableName; }
        }
        public int PicNum
        {
            get { return Image.PicNum; }
        }
        public int LVSNr { get; set; } = 0;
        public int LEingangID { get; set; } = 0;
        public int LAusgangID { get; set; } = 0;
        public int TableId
        {
            get { return Image.TableId; }
        }

        public string ScanFileName
        {
            get { return Image.ScanFilename; }
        }
        public string ImageArt
        {
            get { return Image.ImageArt; }
        }
        //public byte[] ImageData { get; set; }
        //public byte[] Thumbnail { get; set; }
        public bool IsForSPLMessage
        {
            get { return Image.IsForSPLMessage; }
        }
    }
}

