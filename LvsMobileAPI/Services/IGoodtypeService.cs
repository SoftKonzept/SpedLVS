using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IGoodtypeService
    {
        public Goodstypes GET_Goodstype(int GoodstypeId);
        public List<Goodstypes> GET_GoodstypeListByWorkspace(int myWorkspaceId);
        public List<Goodstypes> GET_GoodstypeListByWorkspaceAndAddress(int myWorkspaceId, int myAdrId);
        public Goodstypes GET_GoodstypeByWorkspaceAndAddressAndWerksnummer(int myWorkspaceId, int myAdrId, string myWerksnummer);

    }


    public class GoodtypeService : IGoodtypeService
    {
        private SvrSettings srv;

        public GoodtypeService()
        {
            srv = new SvrSettings();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WorkspaceId"></param>
        /// <returns></returns>
        public Goodstypes GET_Goodstype(int GoodstypeId)
        {
            Goodstypes retGut = new Goodstypes();
            if (GoodstypeId > 0)
            {
                GoodstypeViewData gutData = new GoodstypeViewData(GoodstypeId, 1, true);
                if (
                        (gutData.Gut is Goodstypes) &&
                        (gutData.Gut.Id > 0)
                   )
                {
                    retGut = gutData.Gut.Copy();
                }
            }
            return retGut;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Goodstypes> GET_GoodstypeListByWorkspace(int myWorkspaceId)
        {
            GoodstypeViewData gutVD = new GoodstypeViewData();
            gutVD.GetGoodtypeListByWorkspace(myWorkspaceId);
            return gutVD.ListGueterarten;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Goodstypes> GET_GoodstypeListByWorkspaceAndAddress(int myWorkspaceId, int myAdrId)
        {
            GoodstypeViewData gutVD = new GoodstypeViewData();
            gutVD.GetGoodtypeListByWorkspaceAndAddress(myWorkspaceId, myAdrId);
            return gutVD.ListGueterarten;
        }
        /// <summary>
        /// 
        /// </summary>
        public Goodstypes GET_GoodstypeByWorkspaceAndAddressAndWerksnummer(int myWorkspaceId, int myAdrId, string myWerksnummer)
        {
            GoodstypeViewData gutVD = new GoodstypeViewData();
            var goodstype = gutVD.GetGoodtypeByWorkspaceAndAddressAndWerksnummer(myWorkspaceId, myAdrId, myWerksnummer);
            return goodstype;
        }
    }


}
