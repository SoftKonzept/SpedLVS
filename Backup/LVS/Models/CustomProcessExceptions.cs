using Common.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class CustomProcessExceptions
    {

        [DataMember]
        [JsonProperty("Id")]
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }


        [DataMember]
        [JsonProperty("CustomProcessId")]
        private int _CustomProcessId;
        public int CustomProcessId
        {
            get { return _CustomProcessId; }
            set { _CustomProcessId = value; }
        }

        [DataMember]
        [JsonProperty("CustomProcess")]
        private CustomProcesses _CustomProcess;
        public CustomProcesses CustomProcess
        {
            get { return _CustomProcess; }
            set { _CustomProcess = value; }
        }

        [DataMember]
        [JsonProperty("GoodsTypeId")]
        private int _GoodsTypeId;
        public int GoodsTypeId
        {
            get { return _GoodsTypeId; }
            set { _GoodsTypeId = value; }
        }

        [DataMember]
        [JsonProperty("GoodsType")]
        private Goodstypes _GoodsType;
        public Goodstypes GoodsType
        {
            get { return _GoodsType; }
            set { _GoodsType = value; }
        }

        [DataMember]
        [JsonProperty("GoodsTypeName")]
        private string _GoodsTypeName;
        public string GoodsTypeName
        {
            get
            {
                _GoodsTypeName = string.Empty;
                if ((GoodsType is Goodstypes) && (GoodsType.Id > 0))
                {
                    _GoodsTypeName = GoodsType.Bezeichnung;
                }
                return _GoodsTypeName;
            }

        }

        [DataMember]
        [JsonProperty("Created")]
        private DateTime _Created;
        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }




        //private List<int> _ListProcessWorkspaces = new List<int>();
        //public List<int> ListProcessWorkspaces
        //{
        //    get
        //    {
        //        _ListProcessWorkspaces = new List<int>();
        //        var list = ProcessWorkspaces.Split('#').ToList();
        //        foreach (var item in list)
        //        {
        //            int iTmp = 0;
        //            int.TryParse(item, out iTmp);
        //            if (!_ListProcessWorkspaces.Contains(iTmp))
        //            {
        //                _ListProcessWorkspaces.Add(iTmp);
        //            }
        //        }
        //        return _ListProcessWorkspaces; 
        //    }
        //}

        public CustomProcessExceptions Copy()
        {
            return (CustomProcessExceptions)this.MemberwiseClone();
        }
    }
}
