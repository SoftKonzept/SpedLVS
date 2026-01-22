using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.EDIFACT
{
    public class QTY_C186
    {
        internal clsEdiSegmentElement SegElement;
        internal QTY QTY;
        public const string Kennung = "C186";

        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        ///             Liefermenge, ist
        /// </summary>
        /// <param name="mySegElement"></param>
        public QTY_C186(QTY myEdiElement)
        {
            this.QTY = myEdiElement;
            this.SegElement = myEdiElement.SelectedSegmentElement;
            CreateValue();
        }

        public const string f_6063_selVal_11 = "11";
        public const string f_6063_selVal_12 = "12";
        public const string f_6063_selVal_52 = "52";
        public const string f_6063_selVal_171 = "171";
        public const string f_6063_selVal_189 = "189";

        internal const string Kennung_6063 = "6063";
        /// <summary>       
        ///                 Menge, Qualifier -> Versendete Menge
        ///                 
        ///                 11 Split quantity
        ///                 12 Despatch quantity
        ///                 52 Quantity per pack
        ///                 171 Maximum stackability
        ///                 189 Number of packages in handling unit
        /// </summary>
        private string _f_6063;
        public string f_6063
        {
            get
            {
                _f_6063 = string.Empty;
                if ((this.sef6063 is clsEdiSegmentElementField) && (this.sef6063.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.QTY.EDICreate, this.sef6063, string.Empty, string.Empty);
                    _f_6063 = v.ReturnValue;
                }
                return _f_6063;
            }
        }
        public clsEdiSegmentElementField sef6063
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == QTY_C186.Kennung_6063);
                }
                return tmpSef;
            }
        }
        /// <summary>       
        ///                 Menge, Qualifier 
        ///                 
        ///                 11 Split quantity
        ///                 12 Despatch quantity
        ///                 52 Quantity per pack
        ///                 171 Maximum stackability
        ///                 189 Number of packages in handling unit
        /// </summary>
        private void GetQuantity()
        {
            switch (this.f_6063)
            {

                case QTY_C186.f_6063_selVal_189:
                    this.f_6060 = this.QTY.EDICreate.Lager.Artikel.Anzahl.ToString();
                    if (this.QTY.EDICreate.Lager.Artikel.Anzahl > 1)
                    {
                        f_6411 = "PCE";
                    }
                    else
                    {
                        f_6411 = "C62";
                    }
                    break;

                case QTY_C186.f_6063_selVal_11:
                case QTY_C186.f_6063_selVal_12:
                case QTY_C186.f_6063_selVal_52:
                    this.f_6060 = this.QTY.EDICreate.Lager.Artikel.Brutto.ToString("#.000").Replace(",", ".");
                    f_6411 = "KGM";
                    break;

                case QTY_C186.f_6063_selVal_171:
                    this.f_6060 = "1";
                    if (this.QTY.EDICreate.Lager.Artikel.Anzahl > 1)
                    {
                        f_6411 = "PCE";
                    }
                    else
                    {
                        _f_6411 = "C62";
                    }
                    break;

                default:
                    this.f_6060 = string.Empty;
                    break;

            }
        }

        internal const string Kennung_6060 = "6060";
        /// <summary>
        ///             Referenz, Identifikation 
        ///             Versendete Menge          
        /// </summary>
        private string _f_6060;
        public string f_6060
        {
            get
            {
                _f_6060 = string.Empty;
                if ((this.sef6060 is clsEdiSegmentElementField) && (this.sef6060.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.QTY.EDICreate, this.sef6060, string.Empty, string.Empty);
                    _f_6060 = v.ReturnValue;
                }
                return _f_6060;
            }
            set
            {
                _f_6060 = value;
            }
        }
        public clsEdiSegmentElementField sef6060
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == QTY_C186.Kennung_6060);
                }
                return tmpSef;
            }
        }

        internal const string Kennung_6411 = "6411";
        /// <summary>
        ///             Maßeinheit, Code 
        ///             
        ///             C62 one 
        ///             PCE piece
        ///             SET set 
        ///             MTR metre 
        ///             CMT centimetre 
        ///             MMT millimetre 
        ///             MTK square metre 
        ///             LEF leaf 
        ///             MTQ cubic metre 
        ///             LTR litre 
        ///             PR pair 
        ///             RO roll 
        ///             TNE tonne (metric ton) 
        ///             KGM kilogram 
        ///             GRM gram 
        ///             KMT kilometre
        ///
        /// </summary>
        private string _f_6411;
        public string f_6411
        {
            get
            {
                _f_6411 = string.Empty;
                if ((this.sef6411 is clsEdiSegmentElementField) && (this.sef6411.ID > 0))
                {
                    clsEdiVDAAssignValueAlias v = new clsEdiVDAAssignValueAlias(string.Empty, this.QTY.EDICreate, this.sef6411, string.Empty, string.Empty);
                    _f_6411 = v.ReturnValue;
                }
                return _f_6411;
            }
            set
            {
                _f_6411 = value;
            }
        }
        public clsEdiSegmentElementField sef6411
        {
            get
            {
                clsEdiSegmentElementField tmpSef = new clsEdiSegmentElementField();
                if (this.SegElement.ListEdiSegmentElementFields.Count > 0)
                {
                    tmpSef = this.SegElement.ListEdiSegmentElementFields.FirstOrDefault(x => x.Shortcut == QTY_C186.Kennung_6411);
                }
                return tmpSef;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CreateValue()
        {
            this.Value += this.f_6063;
            if (!f_6060.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_6060;
            }
            if (!f_6411.Equals(clsEdiVDAValueAlias.const_VDA_Value_NotUsed))
            {
                this.Value += UNA.const_Trennzeichen_Gruppe_Doppelpunkt + this.f_6411;
            }

        }

        //===================================================================================

        /// <summary>
        ///             QUANTITY QUALIFIER 
        /// </summary>
        public int f_6063_QuantityQualifier { get; set; } = 0;
        /// <summary>
        ///             QUANTITY QUALIFIER 
        /// </summary>
        public int f_6060_Quantity { get; set; } = 0;
        /// <summary>
        ///             QUANTITY QUALIFIER 
        /// </summary>
        public string f_6411_UnitQualifier { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="myEdiValueString"></param>
        public QTY_C186(string myEdiValueString)
        {
            if ((!myEdiValueString.Equals(string.Empty)) && (myEdiValueString.Length > 0))
            {
                int iTmp = 0;
                List<string> strValue = myEdiValueString.Split(new char[] { ':' }).ToList();
                for (int i = 0; i < strValue.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            iTmp = 1;
                            int.TryParse(strValue[i].ToString(), out iTmp);
                            this.f_6063_QuantityQualifier = iTmp;
                            break;
                        case 1:
                            iTmp = 1;
                            int.TryParse(strValue[i].ToString(), out iTmp);
                            this.f_6060_Quantity = iTmp;
                            break;
                        case 2:
                            this.f_6411_UnitQualifier = strValue[i].ToString();
                            break;
                        case 3:
                        case 4:
                            break;
                    }
                }
            }
        }

    }
}
