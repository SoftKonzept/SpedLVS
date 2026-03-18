using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitNAD
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitNAD(AsnArt myAsnArt, string myNAD_3035_3035, string myNAD_C082_3039, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = NAD.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "NAME AND ADDRESS";
            ediSegmentVD.EdiSegment.Created = DateTime.Now;
            ediSegmentVD.EdiSegment.tmpId = 0;
            ediSegmentVD.EdiSegment.Storable = false;
            ediSegmentVD.EdiSegment.Code = myCode;
            ediSegmentVD.EdiSegment.SortId = 1;
            ediSegmentVD.EdiSegment.IsActive = true;
            switch (myAsnArt.Typ)
            {
                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_INVRPT_D96A:
                    switch (ediSegmentVD.EdiSegment.Code)
                    {
                        case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_NAD_DP:
                            //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_RFF_AFV:
                            //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_DTM_171:
                            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement;
                            break;
                    }
                    break;

                case constValue_AsnArt.const_ArtBeschreibung_EDIFACT_DESADV_D07A:
                    ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;
                    break;

                default:
                    ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;
                    break;
            }
            ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<Models.EdiSegmentElements>();

            EdiSegmentElements ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = MEA.Name;
            ese.Description = "Ship To";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD.Name;
            esef.Name = "Ship To";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "const";
            esef.constValue = "NAD";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD.Name;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD.Kennung_3035;
            ese.Description = "Beteiligter, Qualifier";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD.Kennung_3035;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD.Kennung_3035;
            esef.Name = "UN";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "Vorgabe";
            esef.constValue = myNAD_3035_3035;
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD.Kennung_3035;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD_C082.Kennung;
            ese.Description = "Identifikation des Beteiligten";
            ese.Position = 3;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD_C082.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C082.Kennung_3039;
            esef.Name = "Beteiligter, Identifikation";
            esef.Status = "R";
            esef.Format = "a10";
            esef.Description = "713F16";
            esef.constValue = myNAD_C082_3039;
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C082.Kennung_3039;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C082.Kennung_1131;
            esef.Name = "Codeliste, Code";
            esef.Status = "N";
            esef.Format = "a1";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C082.Kennung_1131;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C082.Kennung_3055;
            esef.Name = "Verantwortliche Stelle für die Codepflege, Code";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "92 - Assigned by buyer or buyers agent const";
            esef.constValue = "92";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C082.Kennung_3055;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD_C058.Kennung;
            ese.Description = "Name und Anschrift";
            ese.Position = 4;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD_C058.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C058.Kennung_3124;
            esef.Name = "Name and Adress";
            esef.Status = "N";
            esef.Format = "a35";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C058.Kennung_3124; ;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD_C080.Kennung;
            ese.Description = "Name des Beteiligten";
            ese.Position = 5;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD_C080.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C080.Kennung_3036;
            esef.Name = "Beteiligter";
            esef.Status = "M";
            esef.Format = "a35";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C080.Kennung_3036;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);



            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD_C059.Kennung;
            ese.Description = "Straße";
            ese.Position = 6;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD_C059.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C059.Kennung_3042;
            esef.Name = "Straße ";
            esef.Status = "M";
            esef.Format = "a35";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C059.Kennung_3042;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);




            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD.Kennung_3164;
            ese.Description = "Ort";
            ese.Position = 7;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD.Kennung_3164;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD.Kennung_3164;
            esef.Name = "City name";
            esef.Status = "O";
            esef.Format = "a35";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD.Kennung_3164;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD_C819.Kennung;
            ese.Description = "Regin/Bundeslad, Einzelheiten";
            ese.Position = 8;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD_C819.Kennung;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD_C819.Kennung_3229;
            esef.Name = "Name einer Region/eines Bundeslandes, Code";
            esef.Status = "R";
            esef.Format = "a9";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD_C819.Kennung_3229;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD.Kennung_3251;
            ese.Description = "Postleitzahl, Code";
            ese.Position = 9;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD.Kennung_3251;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD.Kennung_3251;
            esef.Name = "Postleitzahl, Code";
            esef.Status = "O";
            esef.Format = "a17";
            esef.Description = "#NOTUSED#";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD.Kennung_3251;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = NAD.Kennung_3207;
            ese.Description = "Ländername, Code";
            ese.Position = 10;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + NAD.Kennung_3207;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = NAD.Kennung_3207;
            esef.Name = "Postleitzahl, Code";
            esef.Status = "O";
            esef.Format = "a2";
            esef.Description = "Länderkennzeichen Not used";
            esef.constValue = "#NOTUSED#";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + NAD.Kennung_3207;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

        }
    }

}
