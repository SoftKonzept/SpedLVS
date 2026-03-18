using System;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrAsnArtFieldAssignment_DgvEdifactViewSub
    {

        [DataMember]
        public string AsnSubField { get; set; } = string.Empty;
        public string Beschreibung { get; set; } = string.Empty;
    }
}
