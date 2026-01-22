using Common.Models;
using LVS.ASN.Defaults;
using LVS.Models;

namespace LVS.ASN.EDIFACT.Defaults
{

    public class QTY_QTY_QTY_DefaultsValue
    {
        public string ValueArt { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public QTY_QTY_QTY_DefaultsValue(Addresses myAdr, AsnArt myAsnArt)
        {
            ValueArt = Default_VDAClientOut.const_Const;
            Value = QTY.Name;

        }


    }
}
