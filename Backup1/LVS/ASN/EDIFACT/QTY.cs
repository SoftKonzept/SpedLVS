using LVS.ASN.EDIFACT;
using System.Collections.Generic;
using System.Linq;

namespace LVS
{
    public class QTY
    {
        internal clsEdiVDACreate EDICreate;
        internal clsEdiSegment Segment;
        public const string Name = "QTY";
        internal clsEdiSegmentElement SelectedSegmentElement;

        public bool IsActiv = true;

        /// <summary>
        ///             Sendungsnummer, vergeben vom Lieferanten (alt: SLB)
        /// </summary>
        /// <param name="myASNArt"></param>
        public QTY(clsEdiVDACreate myEdiCreate)
        {
            this.EDICreate = myEdiCreate;
            this.Segment = myEdiCreate.ediSegment;
            List<clsEdiSegmentElement> List_SE = this.Segment.ListEdiSegmentElement.Where(x => x.EdiSegmentId == this.Segment.ID).ToList();
            if (List_SE.Count > 0)
            {
                foreach (clsEdiSegmentElement itm in List_SE)
                {
                    SelectedSegmentElement = itm;

                    switch (itm.Name)
                    {
                        case QTY_C186.Kennung:
                            C186 = new QTY_C186(this);
                            break;
                    }
                    SelectedSegmentElement = null;
                }
                CreateValue();
            }
        }

        internal QTY_C186 C186;


        public const int const_QuantityQualifier_C186_6063_12_DispatchQuantity = 12;
        public const int const_QuantityQualifier_C186_6063_47_InvoicedQuantity = 47;
        public const int const_QuantityQualifier_C186_6063_52_QuantityPerPack = 52;

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += QTY.Name;
            if (!C186.Value.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Segement_Plus + this.C186.Value;
            }
            this.Value += UNA.const_SegementEndzeichen;
        }

        //===================================================================================================================
        /// <summary>
        ///             NUMBER OF PACKAGES
        /// </summary>
        //public int f_7224_NumberOfPackage { get; set; } = 1;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public QTY(string myEdiValueString, string myAsnArt)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                List<string> lElements = myEdiValueString.Split(new char[] { '+' }).ToList();
                for (int i = 0; i < lElements.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            break;
                        case 1:
                            C186 = new QTY_C186(lElements[i]);
                            break;
                            //case 2:
                            //    break;
                            //case 3:
                            //    break;
                            //case 4:
                            //    break;
                    }
                }
            }
        }

    }
}
