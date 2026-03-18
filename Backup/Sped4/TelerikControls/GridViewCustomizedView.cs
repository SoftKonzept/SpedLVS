using LVS;
using System;
using Telerik.WinControls.UI;

namespace Sped4.TelerikControls
{
    public class GridViewCustomizedView
    {
        ///<summary>set_SZG / CustomizeDGVGueterArtenView</summary>
        ///<remarks></remarks>
        public static void CustomizeDGVGueterArtenView(ref RadGridView myGrd)
        {
            for (Int32 i = 0; i <= myGrd.Columns.Count - 1; i++)
            {
                string colName = myGrd.Columns[i].Name.ToString();
                //Güterarten
                switch (colName)
                {
                    case "ID":
                        myGrd.Columns[i].IsVisible = true;
                        break;

                    case "ViewID":
                        myGrd.Columns[i].HeaderText = "Matchcode";
                        if (i != 1)
                        {
                            myGrd.Columns.Move(i, 1);
                            i = 0;
                        }
                        myGrd.Columns[i].SortOrder = RadSortOrder.Ascending;
                        break;

                    case "Bezeichnung":
                        if (i != 2)
                        {
                            myGrd.Columns.Move(i, 2);
                            i = 0;
                        }
                        break;

                    case "Auftraggeber":
                        myGrd.Columns[i].HeaderText = "Auftraggeber";
                        if (i != 3)
                        {
                            myGrd.Columns.Move(i, 3);
                            i = 0;
                        }
                        //i = 0;
                        break;

                    case "ArtikelArt":
                        myGrd.Columns[i].HeaderText = "Art";
                        if (i != 4)
                        {
                            myGrd.Columns.Move(i, 4);
                            i = 0;
                        }
                        break;

                    case "Werksnummer":
                        //myGrd.Columns[i].HeaderText = "Werksnummer";
                        //myGrd.Columns.Move(i, 5);
                        if (i != 5)
                        {
                            myGrd.Columns.Move(i, 5);
                            i = 0;
                        }
                        break;

                    case "Brutto":
                        //this.dgvGArtList.Columns[i].IsVisible = false;
                        break;

                    case "NichtStapelbar":
                        myGrd.Columns[i].HeaderText = "Nicht stapelbar";
                        break;

                    case "aktiv":
                    case "activ":
                    case "Verweis":
                    case "Arbeitsbereich":
                        myGrd.Columns[i].IsVisible = false;
                        break;

                    default:
                        //this.dgvGArtList.Columns[i].IsVisible = false;
                        break;
                }
                myGrd.Columns[i].AutoSizeMode = BestFitColumnMode.DisplayedCells;

            }
        }


        ///<summary>clsClient / ctrGueterArtListe_DGVGueterArtenView</summary>
        ///<remarks></remarks>
        public static bool ctrGueterArtListe_DGVGueterArtenView(string myClientMC, ref RadGridView myDGV)
        {
            bool bReturn = false;
            switch (myClientMC)
            {
                //SZG
                case (clsClient.const_ClientMatchcode_SZG + "_"):
                    set_SZG tmpSZG = new set_SZG();
                    //bReturn = tmpSZG.CustomizeDGVGueterArtenView(ref myDGV);
                    //tmpSZG.CustomizeDGVGueterArtenView(ref myDGV);
                    GridViewCustomizedView.CustomizeDGVGueterArtenView(ref myDGV);
                    break;

                default:
                    for (Int32 i = 0; i <= myDGV.Columns.Count - 1; i++)
                    {
                        string colName = myDGV.Columns[i].Name.ToString();
                        //Warengruppen
                        switch (colName)
                        {
                            case "ViewID":
                                myDGV.Columns[i].HeaderText = "Matchcode";
                                myDGV.Columns[i].SortOrder = RadSortOrder.Ascending;
                                myDGV.Columns.Move(i, 1);
                                break;
                            case "Bezeichnung":
                                myDGV.Columns.Move(i, 2);
                                break;
                            default:
                                myDGV.Columns[i].IsVisible = false;
                                break;
                        }
                    }
                    break;
            }
            return bReturn;
        }

    }
}
