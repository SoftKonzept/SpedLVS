using LVS.Views;
using System.Collections.Generic;
using Telerik.WinControls.UI;

namespace Sped4.Views
{
    public class ctrAsnRead_GridView_InitGridViewRelation
    {
        public GridViewTemplate gridViewTemplate;
        public GridViewRelation gridViewRelation;

        public ctrAsnRead_GridView_InitGridViewRelation(List<ctrASNRead_AsnArticleVdaView> myChildList, MasterGridViewTemplate myMasterGridTemplate)
        {
            gridViewTemplate = new GridViewTemplate();
            gridViewRelation = new GridViewRelation(myMasterGridTemplate);
            if (myChildList != null && myChildList.Count > 0)
            {
                gridViewTemplate.DataSource = myChildList;


                gridViewRelation.ChildTemplate = gridViewTemplate;
                gridViewRelation.RelationName = "ASNID";
                gridViewRelation.ParentColumnNames.Add("ASN");
                gridViewRelation.ChildColumnNames.Add("ASN");

                //int i = 0; //loop
                //Int32 x = 0;
                //for (Int32 i = 0; i <= gridViewTemplate.Columns.Count - 1; i++)
                //{
                //    switch (gridViewTemplate.Columns[i].Name)
                //    {
                //        case "ASN":
                //            gridViewTemplate.Columns[i].Width = 60;
                //            //gridViewTemplate.Columns.Move(i, x);
                //            //gridViewTemplate.Columns.Move(i, 0);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Netto":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 1);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N0}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Brutto":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 2);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Dicke":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 3);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Breite":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 4);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Laenge":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 5);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Hoehe":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 6);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Anzahl":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 7);
                //            gridViewTemplate.Columns[i].FormatString = "{0:N2}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            x++;
                //            break;
                //        case "Einheit":
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns.Move(i, 8);
                //            x++;
                //            break;
                //        case "Position":
                //        case "Pos":
                //            gridViewTemplate.Columns[i].Width = 30;
                //            gridViewTemplate.Columns[i].TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                //            gridViewTemplate.Columns.Move(i, 9);
                //            x++;
                //            break;
                //        case "Werksnummer":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 10);
                //            x++;
                //            break;
                //        case "Produktionsnummer":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 11);
                //            x++;
                //            break;
                //        case "Charge":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 12);
                //            x++;
                //            break;
                //        case "Bestellnummer":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 13);
                //            x++;
                //            break;
                //        case "exMaterialnummer":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 14);
                //            x++;
                //            break;
                //        case "exBezeichnung":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 15);
                //            x++;
                //            break;
                //        case "exAuftrag":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 16);
                //            x++;
                //            break;
                //        case "GlowDate":
                //        case "Glühdatum":
                //            //tmpArtVda.Columns[i].HeaderText = "Glühdatum";
                //            gridViewTemplate.Columns[i].Width = 80;
                //            gridViewTemplate.Columns[i].FormatString = "{0:d}";
                //            gridViewTemplate.Columns[i].FormatInfo = CultureInfo.CreateSpecificCulture("de-DE");
                //            gridViewTemplate.Columns.Move(i, 17);
                //            x++;
                //            break;
                //        case "Gut":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 18);
                //            x++;
                //            break;
                //        case "LfsNr":
                //        case "Lieferschein":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 19);
                //            x++;
                //            break;
                //        case "TMS":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 20);
                //            x++;
                //            break;
                //        case "VehicleNo":
                //        case "KFZ":
                //            gridViewTemplate.Columns[i].Width = 120;
                //            gridViewTemplate.Columns.Move(i, 21);
                //            x++;
                //            break;
                //        default:
                //            gridViewTemplate.Columns[i].IsVisible = false;
                //            break;
                //    }
                //    i++;

                //}
                ////gridViewTemplate.BestFitColumns();



                //foreach (var item in myChildList)
                //{
                //    Console.WriteLine($"Netto: {item.Netto}, Brutto: {item.Brutto}");
                //    Console.WriteLine(gridViewTemplate.Columns["Netto"].FormatString);
                //    Console.WriteLine(gridViewTemplate.Columns["Brutto"].FormatString);
                //    Console.WriteLine(gridViewTemplate.Columns["Netto"].FormatInfo);
                //    Console.WriteLine(gridViewTemplate.Columns["Brutto"].FormatInfo);

                //}
            }
        }
    }
}
