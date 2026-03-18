using LVS.Communicator.EdiVDA;
using LVS.Constants;
using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitMEA
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitMEA(AsnArt myAsnArt, string myMEA_6311_6311, string myMEA_C502_6313, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = MEA.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "Consignment gross weight";
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
                    switch (ediSegmentVD.EdiSegment.Code)
                    {
                        case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_NAD_DP:
                            //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_RFF_AFV:
                            //case EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_SegToDeactivate_Code_DTM_171:
                            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement;
                            break;
                    }
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
            ese.Description = "Consignment gross weight";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + MEA.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = MEA.Name;
            esef.Name = "Consignment gross weight";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "const";
            esef.constValue = "MEA";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + MEA.Name;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = MEA_C502.Kennung_C502;
            ese.Description = "Measurement details";
            ese.Position = 2;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = MEA_C502.Kennung_6313;
            esef.Name = "Measured attribute code";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "Total gross weight";
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
                    esef.constValue = string.Empty;
                    break;
                default:
                    esef.constValue = string.Empty;
                    break;
            }

            //esef.constValue = string.Empty;  // AAD
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = MEA_C174.Kennung_C174;
            ese.Description = "Value or range";
            ese.Position = 3;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = MEA_C174.Kennung_6411;
            esef.Name = "Measurement unit code";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "KGM = Kilogram";
            esef.constValue = "KGM";
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = MEA_C174.Kennung_6314;
            esef.Name = "Measure";
            esef.Status = "R";
            esef.Format = "a12";
            esef.Description = "Gross weight. Maximum 3 decimals";
            esef.constValue = string.Empty;
            esef.Position = 1;
            esef.Created = DateTime.Now;
            esef.FormatString = string.Empty;
            esef.Code = ediSegmentVD.EdiSegment.Code;
            esef.SortId = 1;
            esef.Kennung = ese.Kennung + " | " + esef.Shortcut;
            esef.EdiSegmentId = 0;
            esef.AsnArtId = (int)asnArt.Id;
            ese.ListEdiSegmentElementFields.Add(esef);
            ediSegmentVD.EdiSegment.ListEdiSegmentElements.Add(ese);

        }
    }

}
