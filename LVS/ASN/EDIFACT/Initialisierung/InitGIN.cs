using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitGIN
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitGIN(AsnArt myAsnArt, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = GIN.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "Goods identity number";
            ediSegmentVD.EdiSegment.Created = DateTime.Now;
            ediSegmentVD.EdiSegment.tmpId = 0;
            ediSegmentVD.EdiSegment.Storable = false;
            ediSegmentVD.EdiSegment.Code = myCode;
            ediSegmentVD.EdiSegment.SortId = 1;
            ediSegmentVD.EdiSegment.IsActive = true;
            ediSegmentVD.EdiSegment.EdiSegmentCheckFunction = string.Empty;

            ediSegmentVD.EdiSegment.ListEdiSegmentElements = new List<Models.EdiSegmentElements>();

            EdiSegmentElements ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = GIN.Name;
            ese.Description = "Goods identity number";
            ese.Position = 1;
            ese.Created = DateTime.Now;
            ese.tmpId = 0;
            ese.Code = ediSegmentVD.EdiSegment.Code;
            ese.SortId = 1;
            ese.Kennung = ediSegmentVD.EdiSegment.Name + " | " + ese.Name;
            ese.IsActive = true;
            ese.ListEdiSegmentElementFields = new List<EdiSegmentElementFields>();

            EdiSegmentElementFields esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = GIN.Name;
            esef.Name = "Goods identity number";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "const";
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


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = GIN.Kennung_7405;
            ese.Description = "Identity number qualifier";
            ese.Position = 1;
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
            esef.Shortcut = GIN.Kennung_7405;
            esef.Name = "Identity number qualifier";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "ML = Marking/label number";
            esef.constValue = "ML";
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
            ese.Name = GIN_C208.Kennung;
            ese.Description = "Identity number range";
            ese.Position = 1;
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
            esef.Shortcut = GIN_C208.Kennung_7402;
            esef.Name = "Identity number ";
            esef.Status = "M";
            esef.Format = "a35";
            esef.Description = "Package label number – Handling Unit Identification";
            esef.constValue = string.Empty;
            esef.Position = 2;
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
