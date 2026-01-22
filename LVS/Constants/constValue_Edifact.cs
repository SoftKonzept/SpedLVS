using LVS.Views;
using System.Collections.Generic;
using System.Linq;

namespace LVS.Constants
{
    public class constValue_Edifact
    {
        public const string const_Edifact_UNA1_GruppendatenelementTrennzeichen = ":";
        public const string const_Edifact_UNA2_SegmentDatenelementTrennzeichen = "+";
        public const string const_Edifact_UNA3_Dezimalzeichen = ".";
        public const string const_Edifact_UNA4_Freigabezeichen = "?";
        public const string const_Edifact_UNA5_Reserviert = " ";
        public const string const_Edifact_UNA6_SegmentEndzeichen = "'";


        /// ---- QALITY:D:96A

        public const string const_Edifact_QalityD96A_UNHSegment = "UNH+1+QALITY:D:96A:UN";



        ///  ------ EDIFACT_ASN_D96A  
        ///  ------ EDIFACT_DESADV_D07A
        /// 

        public const string const_Edifact_CPS = "CPS";           // 

        public const string const_Edifact_DTM_11 = "DTM+11";     // Eingangsdatum
        public const string const_Edifact_DTM_94 = "DTM+94";           // Artikel.Glühdatum
        public const string const_Edifact_DTM_132 = "DTM+132";   // Anlieferdatum

        public const string const_Edifact_GIR_3 = "GIR+3";             // Artikel.Produktionsnummer

        public const string const_Edifact_LIN = "LIN";                 // Artikel.Werksnummer
        public const string const_Edifact_LOC_11 = "LOC+11";     // Empfangsort

        public const string const_Edifact_MEA_AAY_AAG = "MEA+AAY+AAG"; // Artikel.Brutto 
        public const string const_Edifact_MEA_AAY_AAL = "MEA+AAY+AAL"; // Artikel.Netto 
        public const string const_Edifact_MEA_AAY_LN = "MEA+AAY+LN"; // Artikel.Laenge 
        public const string const_Edifact_MEA_AAY_WD = "MEA+AAY+WD"; // Artikel.Breite
        public const string const_Edifact_MEA_AAY_TH = "MEA+AAY+TH"; // Artikel.Dicke   

        public const string const_Edifact_NAD_BY = "NAD+BY";     // Versender
        public const string const_Edifact_NAD_CN = "NAD+CN";     // Empfänger
        public const string const_Edifact_NAD_CZ = "NAD+CZ";     // Auftraggeber
        public const string const_Edifact_NAD_FW = "NAD+FW";     // Lager
        public const string const_Edifact_NAD_SE = "NAD+SE";     // Versender
        public const string const_Edifact_NAD_SF = "NAD+SF";     // Versender
        public const string const_Edifact_NAD_ST = "NAD+ST";     // Empfänger

        public const string const_Edifact_PAC = "PAC";                 // Artikel.Anzahl 
        public const string const_Edifact_PIA = "PIA";                 // Artikel.Anzahl 
        // 
        public const string const_Edifact_PCI_17 = "PCI+17";           // Artikel.Brutto 

        public const string const_Edifact_RFF_AAS = "RFF+AAS";         // Lieferschein
        public const string const_Edifact_RFF_AAT = "RFF+AAT";         // Artikel.ex Bezeichnung
        public const string const_Edifact_RFF_AAU = "RFF+AAU";         // Lieferschein
        public const string const_Edifact_RFF_ON = "RFF+ON";           // Artikel.Bestellnummer

        public const string const_Edifact_TDT_12 = "TDT+12";        // KFZ oder Waggon

        public const string const_Edifact_QTY_12 = "QTY+12";        // Artikel.Anzahl
        public const string const_Edifact_QTY_52 = "QTY+52";         // Artikel.Einheit



        public static List<ctrAsnArtFieldAssignment_DgvEdifactView> EdifactSegmentList()
        {
            List<ctrAsnArtFieldAssignment_DgvEdifactView> list = new List<ctrAsnArtFieldAssignment_DgvEdifactView>();

            ctrAsnArtFieldAssignment_DgvEdifactView v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_CPS;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_DTM_11;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_DTM_94;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_DTM_132;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_GIR_3;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_LIN;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_LOC_11;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_MEA_AAY_AAG;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_MEA_AAY_AAL;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_MEA_AAY_LN;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_MEA_AAY_WD;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_MEA_AAY_TH;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_BY;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_CN;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_CZ;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_FW;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_SE;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_SF;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_NAD_ST;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_PAC;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_PIA;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_PCI_17;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_RFF_AAS;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_RFF_AAT;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_RFF_AAU;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_RFF_ON;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_TDT_12;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_QTY_12;
            list.Add(v);

            v = new ctrAsnArtFieldAssignment_DgvEdifactView();
            v.AsnField = constValue_Edifact.const_Edifact_QTY_52;
            list.Add(v);

            return list.OrderBy(item => item.AsnField).ToList();
        }


        public const string const_Edifact_Sub_QTY_C186_f6063 = "QTY_C186_f6063";
        public const string const_Edifact_Sub_QTY_C186_f6060 = "QTY_C186_f6060";
        public const string const_Edifact_Sub_QTY_C186_f6411 = "QTY_C186_f6411";

        public static Dictionary<string, ctrAsnArtFieldAssignment_DgvEdifactView> DictEdifactSubSegmentList()
        {
            Dictionary<string, ctrAsnArtFieldAssignment_DgvEdifactView> dict = new Dictionary<string, ctrAsnArtFieldAssignment_DgvEdifactView>();

            ctrAsnArtFieldAssignment_DgvEdifactViewSub sub = new ctrAsnArtFieldAssignment_DgvEdifactViewSub();
            List<ctrAsnArtFieldAssignment_DgvEdifactView> list = constValue_Edifact.EdifactSegmentList();
            foreach (ctrAsnArtFieldAssignment_DgvEdifactView item in list)
            {
                switch (item.AsnField)
                {
                    case constValue_Edifact.const_Edifact_QTY_12:
                    case constValue_Edifact.const_Edifact_QTY_52:
                        item.List_SubAsnField = new List<ctrAsnArtFieldAssignment_DgvEdifactViewSub>();
                        sub = new ctrAsnArtFieldAssignment_DgvEdifactViewSub()
                        {
                            AsnSubField = constValue_Edifact.const_Edifact_Sub_QTY_C186_f6063,
                            Beschreibung = "Quantity Code",
                        };
                        item.List_SubAsnField.Add(sub);
                        sub = new ctrAsnArtFieldAssignment_DgvEdifactViewSub()
                        {
                            AsnSubField = constValue_Edifact.const_Edifact_Sub_QTY_C186_f6060,
                            Beschreibung = "Quantity - Menge",
                        };
                        item.List_SubAsnField.Add(sub);
                        sub = new ctrAsnArtFieldAssignment_DgvEdifactViewSub()
                        {
                            AsnSubField = constValue_Edifact.const_Edifact_Sub_QTY_C186_f6411,
                            Beschreibung = "Measure unit qualifier - Einheit",
                        };
                        item.List_SubAsnField.Add(sub);
                        break;
                    case constValue_Edifact.const_Edifact_RFF_ON:
                        item.List_SubAsnField = new List<ctrAsnArtFieldAssignment_DgvEdifactViewSub>();
                        sub = new ctrAsnArtFieldAssignment_DgvEdifactViewSub()
                        {
                            AsnSubField = "Test1",
                            Beschreibung = "Beschreibung 1",
                        };
                        item.List_SubAsnField.Add(sub);
                        break;
                    default:
                        item.List_SubAsnField = new List<ctrAsnArtFieldAssignment_DgvEdifactViewSub>();
                        break;

                }
                dict.Add(item.AsnField, item);
            }
            return dict;
        }
    }
}
