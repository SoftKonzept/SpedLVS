using LVS.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class Asn
    {
        [DataMember]
        [JsonProperty("Id")]
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("ASNFileTyp")]
        private string _ASNFileTyp = string.Empty;
        public string ASNFileTyp
        {
            get { return _ASNFileTyp; }
            set { _ASNFileTyp = value; }
        }

        [DataMember]
        [JsonProperty("AsnNr")]
        private int _AsnNr = 0;
        public int AsnNr
        {
            get { return _AsnNr; }
            set { _AsnNr = value; }
        }

        [DataMember]
        [JsonProperty("AsnFieldId")]
        private int _AsnFieldId = 0;
        public int AsnFieldId
        {
            get { return _AsnFieldId; }
            set { _AsnFieldId = value; }
        }


        [DataMember]
        [JsonProperty("AsnTypId")]
        private int _AsnTypId = 0;
        public int AsnTypId
        {
            get { return _AsnTypId; }
            set { _AsnTypId = value; }
        }

        [DataMember]
        [JsonProperty("Path")]
        private string _Path = string.Empty;
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        [DataMember]
        [JsonProperty("FileName")]
        private string _FileName = string.Empty;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        [DataMember]
        [JsonProperty("Datum")]
        private DateTime _Datum = new DateTime(1900, 1, 1);
        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }

        [DataMember]
        [JsonProperty("IsRead")]
        private bool _IsRead = false;
        public bool IsRead
        {
            get { return _IsRead; }
            set { _IsRead = value; }
        }

        [DataMember]
        [JsonProperty("Direction")]
        private string _Direction = string.Empty;
        public string Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }

        [DataMember]
        [JsonProperty("MandantenId")]
        private int _MandantenId = 0;
        public int MandantenId
        {
            get { return _MandantenId; }
            set { _MandantenId = value; }
        }

        [DataMember]
        [JsonProperty("WorkspaceId")]
        private int _WorkspaceId = 0;
        public int WorkspaceId
        {
            get { return _WorkspaceId; }
            set { _WorkspaceId = value; }
        }

        [DataMember]
        [JsonProperty("EdiMessageValue")]
        private string _EdiMessageValue = string.Empty;
        public string EdiMessageValue
        {
            get { return _EdiMessageValue; }
            set { _EdiMessageValue = value; }
        }

        [DataMember]
        [JsonProperty("AsnArtId")]
        private int _AsnArtId = 0;
        public int AsnArtId
        {
            get { return _AsnArtId; }
            set { _AsnArtId = value; }
        }

        [DataMember]
        [JsonProperty("Created")]
        private DateTime _Created = DateTime.Now;
        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }

        //[IgnoreDataMember]
        //private string _EdiMessageValueToDB = string.Empty;
        //public string EdiMessageValueToDB
        //{
        //    get 
        //    {
        //        if (EdiMessageValue.Length > 0)
        //        {
        //            _EdiMessageValueToDB = EdiMessageValue.Replace("'", "#");
        //        }
        //        return _EdiMessageValueToDB; 
        //    }
        //    set { _EdiMessageValueToDB = value; }
        //}



        [DataMember]
        [JsonProperty("ListEdiMessageValue")]
        public List<string> ListEdiMessageValue
        {
            get
            {
                List<string> list = new List<string>();
                if ((EdiMessageValue != null) && (EdiMessageValue.Length > 0))
                {
                    char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0];
                    list = EdiMessageValue.Split(new char[] { cSplit }).ToList();
                }
                return list;
            }
        }

        [DataMember]
        [JsonProperty("ListEdiMessageValue")]
        public List<string> ListVdaMessageValue
        {
            get
            {
                List<string> list = new List<string>();
                if ((EdiMessageValue != null) && (EdiMessageValue.Length > 0))
                {
                    char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0];
                    list = EdiMessageValue.Split(new char[] { cSplit }).ToList();
                }
                return list;
            }
        }

        public Asn Copy()
        {
            return (Asn)this.MemberwiseClone();
        }
    }
}
