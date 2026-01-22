using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{

    public class INV_INV_INV_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public INV_INV_INV_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = INV.Name;

        }


    }
}
