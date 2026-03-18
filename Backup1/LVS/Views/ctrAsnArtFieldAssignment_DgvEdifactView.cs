using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrAsnArtFieldAssignment_DgvEdifactView
    {

        [DataMember]
        public string AsnField { get; set; } = string.Empty;

        public List<ctrAsnArtFieldAssignment_DgvEdifactViewSub> List_SubAsnField { get; set; } = new List<ctrAsnArtFieldAssignment_DgvEdifactViewSub>();

    }
}
