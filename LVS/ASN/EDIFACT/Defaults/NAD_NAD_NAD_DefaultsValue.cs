using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{

    public class NAD_NAD_NAD_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public NAD_NAD_NAD_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = NAD.Name;
        }


    }
}
