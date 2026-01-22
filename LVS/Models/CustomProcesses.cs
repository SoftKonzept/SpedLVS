using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class CustomProcesses
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
        [JsonProperty("AdrId")]
        private int _AdrId;
        public int AdrId
        {
            get { return _AdrId; }
            set { _AdrId = value; }
        }

        [DataMember]
        [JsonProperty("ProcessName")]
        private string _ProcessName;
        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }

        [DataMember]
        [JsonProperty("ProcessWorkspaces")]
        private string _ProcessWorkspaces;
        public string ProcessWorkspaces
        {
            get { return _ProcessWorkspaces; }
            set { _ProcessWorkspaces = value; }
        }
        [DataMember]
        [JsonProperty("IsActive")]
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [DataMember]
        [JsonProperty("Created")]
        private DateTime _Created;
        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }


        [DataMember]
        [JsonProperty("Adress")]
        private Addresses _Adress;
        public Addresses Adress
        {
            get { return _Adress; }
            set { _Adress = value; }
        }

        private List<int> _ListProcessWorkspaces = new List<int>();
        public List<int> ListProcessWorkspaces
        {
            get
            {
                _ListProcessWorkspaces = new List<int>();
                var list = ProcessWorkspaces.Split('#').ToList();
                foreach (var item in list)
                {
                    int iTmp = 0;
                    int.TryParse(item, out iTmp);
                    if (!_ListProcessWorkspaces.Contains(iTmp))
                    {
                        _ListProcessWorkspaces.Add(iTmp);
                    }
                }
                return _ListProcessWorkspaces;
            }
        }

        private List<CustomProcessExceptions> _ListCustomProcessExceptions = new List<CustomProcessExceptions>();
        public List<CustomProcessExceptions> ListCustomProcessExceptions
        {
            get { return _ListCustomProcessExceptions; }
            set { _ListCustomProcessExceptions = value; }
        }

        public CustomProcesses Copy()
        {
            return (CustomProcesses)this.MemberwiseClone();
        }
    }
}
