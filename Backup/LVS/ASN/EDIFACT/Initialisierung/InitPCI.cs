using LVS.Models;
using LVS.ViewData;
using System;
using System.Collections.Generic;

namespace LVS.ASN.EDIFACT.Initialisierung
{
    public class InitPCI
    {
        public EdiSegmentViewData ediSegmentVD;
        internal AsnArt asnArt = new AsnArt();
        public InitPCI(AsnArt myAsnArt, string myCode)
        {
            asnArt = myAsnArt.Copy();

            ediSegmentVD = new EdiSegmentViewData();
            ediSegmentVD.EdiSegment.AsnArtId = (int)asnArt.Id;
            ediSegmentVD.EdiSegment.Name = PCI.Name.ToUpper();
            ediSegmentVD.EdiSegment.Status = "M";
            ediSegmentVD.EdiSegment.RepeatCount = 1;
            ediSegmentVD.EdiSegment.Ebene = 0;
            ediSegmentVD.EdiSegment.Description = "PACKAGE IDENTIFICATION";
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
            ese.Name = PCI.Name;
            ese.Description = "PACKAGE IDENTIFICATION";
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
            esef.Shortcut = PCI.Name;
            esef.Name = "PACKAGE IDENTIFICATION";
            esef.Status = "M";
            esef.Format = "a3";
            esef.Description = "const";
            esef.constValue = PCI.Name;
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
            ese.Name = PCI.Kennung_4233;
            ese.Description = "Hierarchical id. number";
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
            esef.Shortcut = PCI.Kennung_4233;
            esef.Name = "Marking instructions, coded";
            esef.Status = "R";
            esef.Format = "a3";
            esef.Description = "ZZZ Mutually defined";
            esef.constValue = "ZZZ";
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
            ese.Name = PCI_C210.Kennung;
            ese.Description = "MARKS & LABELS";
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
            esef.Shortcut = PCI_C210.Kennung_7102;
            esef.Name = "Shipping marks ";
            esef.Status = "M";
            esef.Format = "c35";
            esef.Description = "Not Used";
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

            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = PCI.Kennung_8169;
            ese.Description = "Container/package status";
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
            esef.Shortcut = PCI.Kennung_8169;
            esef.Name = "Container/package status";
            esef.Status = "C";
            esef.Format = "c3";
            esef.Description = "Container/package status";
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


            ese = new EdiSegmentElements();
            ese.Id = 0;
            ese.EdiSegmentId = 0;
            ese.Name = PCI_C827.Kennung;
            ese.Description = "TYPE OF MARKING ";
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
            esef.Shortcut = PCI_C827.Kennung_7511;
            esef.Name = "Type of marking, coded";
            esef.Status = "C";
            esef.Format = "c3";
            esef.Description = "not used";
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

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = PCI_C827.Kennung_1131;
            esef.Name = "Code list qualifier ";
            esef.Status = "C";
            esef.Format = "c3";
            esef.Description = "not used";
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

            esef = new EdiSegmentElementFields();
            esef.Id = 0;
            esef.EdiSemgentElementId = 0;
            esef.Shortcut = PCI_C827.Kennung_3055;
            esef.Name = "Code list responsible agency";
            esef.Status = "C";
            esef.Format = "c3";
            esef.Description = "not used";
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
