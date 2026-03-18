using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class AsnValues
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
        [JsonProperty("AsnId")]
        private int _AsnId = 0;
        public int AsnId
        {
            get { return _AsnId; }
            set { _AsnId = value; }
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
        [JsonProperty("FieldName")]
        private string _FieldName = string.Empty;
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

        [DataMember]
        [JsonProperty("Value")]
        private string _Value = string.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
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
        [JsonProperty("Typ")]
        private string _Typ = string.Empty;
        public string Typ
        {
            get { return _Typ; }
            set { _Typ = value; }
        }

        [DataMember]
        [JsonProperty("Kennung")]
        private string _Kennung = string.Empty;
        public string Kennung
        {
            get { return _Kennung; }
            set { _Kennung = value; }
        }

        [DataMember]
        [JsonProperty("SatzKennung")]
        private string _SatzKennung = string.Empty;
        public string SatzKennung
        {
            get { return _SatzKennung; }
            set { _SatzKennung = value; }
        }

        //[DataMember]
        //[JsonProperty("ListEdiMessageValue")]
        //public List<string> ListEdiMessageValue 
        //{
        //    get
        //    {
        //        List<string> list = new List<string>();
        //        if ((EdiMessageValue != null) && (EdiMessageValue.Length > 0))
        //        {
        //            char cSplit = constValue_Edifact.const_Edifact_UNA6_SegmentEndzeichen[0];
        //            list = EdiMessageValue.Split(new char[] { cSplit }).ToList();
        //        }
        //        return list;
        //    } 
        //}


        public AsnValues Copy()
        {
            return (AsnValues)this.MemberwiseClone();
        }
    }
}
