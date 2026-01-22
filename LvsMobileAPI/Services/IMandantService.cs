using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IMandantService
    {
        public Mandanten GET_Mandant(int MandantId);
        public List<Mandanten> GET_MandantenList();
    }


    public class MandantService : IMandantService
    {
        private SvrSettings srv;

        public MandantService()
        {
            srv = new SvrSettings();
        }

        public Mandanten GET_Mandant(int MandantId)
        {
            Mandanten reMandant = new Mandanten();
            if (MandantId > 0)
            {
                MandantenViewData mandantenVD = new MandantenViewData(MandantId);
                if (
                        (mandantenVD.Mandant is Mandanten) &&
                        (mandantenVD.Mandant.Id > 0)
                   )
                {
                    reMandant = mandantenVD.Mandant.Copy();
                }
            }
            return reMandant;
        }

        public List<Mandanten> GET_MandantenList()
        {
            MandantenViewData mandantVD = new MandantenViewData();
            mandantVD.GetMandantenList();
            return mandantVD.ListMandanten;
        }
    }


}
