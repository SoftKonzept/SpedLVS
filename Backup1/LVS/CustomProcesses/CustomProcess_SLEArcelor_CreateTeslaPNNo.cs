using LVS.Constants;
using LVS.ViewData;

namespace LVS.CustomProcesses
{
    public class CustomProcess_SLEArcelor_CreateTeslaPNNo
    {
        public string ProcessName = constValue_CustomProcesses.const_Process_SLEArcelor_CreateTeslaPNNo;
        public string ProcessViewName = "SLEArcelor_CreateTeslaPNNo_Process";

        public ArticleViewData artVD { get; set; }
        public EingangViewData eVD { get; set; }
        private Globals._GL_USER GL_USER { get; set; }
        public CustomProcess_SLEArcelor_CreateTeslaPNNo(Globals._GL_USER myGLUser)
        {
            GL_USER = myGLUser;
        }

        /// <summary>
        ///             returns bool true, if anything is done
        ///             else false
        /// </summary>
        /// <param name="myArticleId"></param>
        /// <param name="myEingangId"></param>
        /// <param name="myAusgangId"></param>
        /// <param name="myProcessLocationString"></param>
        /// <returns></returns>
        public bool ExecuteProcess(int myArticleId, int myEingangId, int myAusgangId, string myProcessLocationString)
        {
            bool bReturn = false;
            if (myArticleId > 0)
            {
                artVD = new ArticleViewData(myArticleId, GL_USER);
                string strChange = artVD.Artikel.Produktionsnummer + "X" + int.Parse(artVD.Artikel.LVS_ID.ToString());
                artVD.Artikel.exBezeichnung = strChange;
                bReturn = artVD.Update();
            }
            return bReturn;
        }
    }
}
