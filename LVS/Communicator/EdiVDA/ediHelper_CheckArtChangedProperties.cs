using System.Collections.Generic;
using System.Linq;

namespace LVS.Communicator.EdiVDA
{
    public class ediHelper_CheckArtChangedProperties
    {

        public static clsArtikel ResetLastPropertyChangesByArtikel(clsArtikel myArtikel, Globals._GL_USER myGLUser)
        {
            clsArtikel retArtikel = myArtikel.Copy();
            //ermitteln Änderungen des Arikels
            //Änderungen durchlaufen und die geänderten Properties wieder setzen
            Dictionary<string, clsObjPropertyChanges> DictChanges = clsObjPropertyChanges.GetLastPropertyChangesByArtikel(myGLUser, (int)myArtikel.ID);
            if (DictChanges.Count > 0)
            {
                //aktuell nur Artikel
                Dictionary<string, clsObjPropertyChanges> DictChangesArtikel = DictChanges.Where(x => x.Value.TableName == clsObjPropertyChanges.TableName_Artikel).ToDictionary(xs => xs.Key, xs => xs.Value);
                //Dictionary<string, clsObjPropertyChanges> DictChangesEingang = DictChanges.Where(x => x.Value.TableName == clsObjPropertyChanges.TableName_LEingang).ToDictionary(xs => xs.Key, xs => xs.Value);
                //Dictionary<string, clsObjPropertyChanges> DictChangesAusgang = DictChanges.Where(x => x.Value.TableName == clsObjPropertyChanges.TableName_LAusgang).ToDictionary(xs => xs.Key, xs => xs.Value);

                if (DictChangesArtikel.Count > 0)
                {
                    retArtikel = clsArtikel.ChangeArtikelPorpertiesToOldValue(myArtikel, DictChanges);
                }
                //if (DictChangesEingang.Count > 0)
                //{
                //    retArtikel.Eingang = clsLEingang.ChangeEingangPorpertiesToOldValue(myArtikel.Eingang, DictChangesEingang);
                //}
                //if (DictChangesAusgang.Count > 0)
                //{
                //    if ((retArtikel.Ausgang is clsLAusgang) && (retArtikel.Ausgang.LAusgangTableID > 0))
                //    {
                //        retArtikel.Ausgang = clsLAusgang.ChangeEingangPorpertiesToOldValue(myArtikel.Eingang, DictChangesEingang);
                //    }
                //}
            }
            return retArtikel;
        }
    }
}
