using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Import;
using LVS;

namespace Import
{
    public class helper_sql_clsQueue
    {
        public static void AddEM(impArtikel myArtImport)
        {
            if (myArtImport.SysImport.CheckConnectionCom)
            {
                if (
                    (myArtImport.Artikel.ID.Equals(1)) ||
                     (myArtImport.Artikel.ID.Equals(13)) ||
                      (myArtImport.Artikel.ID.Equals(14))
                  )
                {
                    string s = string.Empty;
                }

                clsSystem sys = new clsSystem();
                sys.InitSystem(ref myArtImport.SysImport.GLSystem, 0);
                sys.con_Database_Com = myArtImport.SysImport.GLSystem.con_Database_COM;
                sys.con_PassDB_Com = myArtImport.SysImport.GLSystem.con_PassDB_COM;
                sys.con_Server_Com = myArtImport.SysImport.GLSystem.con_Server_COM;
                sys.con_UserDB_Com = myArtImport.SysImport.GLSystem.con_UserDB_COM;

                clsLager lager = new clsLager();
                lager.InitClass(myArtImport.SysImport.GLUser, myArtImport.SysImport.GLSystem, sys);
                lager.Artikel = myArtImport.Artikel;
                lager.ASNAction = new clsASNAction();
                lager.ASNAction.InitClass(ref myArtImport.SysImport.GLUser);

                clsASNTransfer AsnTransfer = new clsASNTransfer();
                AsnTransfer.GLSystem = myArtImport.SysImport.GLSystem;
                AsnTransfer.GL_User = myArtImport.SysImport.GLUser;
               
                if (AsnTransfer.DoASNTransfer(AsnTransfer.GLSystem, myArtImport.Eingang.AbBereichID, myArtImport.Eingang.MandantenID))
                {
                    lager.Artikel.listArt.Clear();
                    lager.Artikel.listArt.Add(lager.Artikel.Copy());
                    lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Eingang;
                    if (lager.ASNAction.ASNActionProcessNr > 0)
                    {
                        AsnTransfer.CreateLM(ref lager);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myArtImport"></param>
        public static void AddAM(impArtikel myArtImport)
        {
            if (myArtImport.SysImport.CheckConnectionCom)
            {
                clsSystem sys = new clsSystem();
                sys.InitSystem(ref myArtImport.SysImport.GLSystem, 0);
                sys.con_Database_Com = myArtImport.SysImport.GLSystem.con_Database_COM;
                sys.con_PassDB_Com = myArtImport.SysImport.GLSystem.con_PassDB_COM;
                sys.con_Server_Com = myArtImport.SysImport.GLSystem.con_Server_COM;
                sys.con_UserDB_Com = myArtImport.SysImport.GLSystem.con_UserDB_COM;

                clsLager lager = new clsLager();
                lager.InitClass(myArtImport.SysImport.GLUser, myArtImport.SysImport.GLSystem, sys);
                lager.Artikel = myArtImport.Artikel;
                lager.ASNAction = new clsASNAction();
                lager.ASNAction.InitClass(ref myArtImport.SysImport.GLUser);

                clsASNTransfer AsnTransfer = new clsASNTransfer();
                AsnTransfer.GLSystem = myArtImport.SysImport.GLSystem;
                AsnTransfer.GL_User = myArtImport.SysImport.GLUser;

                if (AsnTransfer.DoASNTransfer(AsnTransfer.GLSystem, myArtImport.Eingang.AbBereichID, myArtImport.Eingang.MandantenID))
                {
                    lager.Artikel.listArt.Clear();
                    lager.Artikel.listArt.Add(lager.Artikel.Copy());
                    lager.ASNAction.ASNActionProcessNr = clsASNAction.const_ASNAction_Ausgang;
                    if (lager.ASNAction.ASNActionProcessNr > 0)
                    {
                        AsnTransfer.CreateLM(ref lager);
                    }
                }
            }
        }

    }
}
