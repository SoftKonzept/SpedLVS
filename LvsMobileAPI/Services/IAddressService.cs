using Common.ApiModels;
using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IAddressService
    {
        public Addresses GET_Adress(int AddressId);
        public ResponseAddress GET_AddressList(ResponseAddress resAdr);
        public ResponseAddress POST_AddressSupplierNo(ResponseAddress resAdr);
    }


    public class AddressService : IAddressService
    {
        private SvrSettings srv;

        public AddressService()
        {
            srv = new SvrSettings();
        }

        public Addresses GET_Adress(int AddressId)
        {
            Addresses retAdr = new Addresses();
            if (AddressId > 0)
            {
                AddressViewData adrVD = new AddressViewData(AddressId, 0);
                if (
                        (adrVD.Address is Common.Models.Addresses) &&
                        (adrVD.Address.Id > 0)
                   )
                {
                    retAdr = adrVD.Address.Copy();
                }
            }
            return retAdr;
        }

        public ResponseAddress GET_AddressList(ResponseAddress resAdr)
        {
            Addresses retAdr = new Addresses();
            AddressViewData adrVD = new AddressViewData();
            adrVD.GetAddresslist(resAdr.AppProcess, resAdr.WorkspaceId);
            resAdr.Address = adrVD.Address.Copy();
            resAdr.ListAddresses = new List<Addresses>(adrVD.ListAddresses);
            return resAdr;
        }

        public ResponseAddress POST_AddressSupplierNo(ResponseAddress resAdr)
        {
            resAdr.AdrReferenz.SupplierNo = AddressViewData.GetSupplierNo(resAdr.AdrReferenz.SenderAdrId, resAdr.AdrReferenz.VerweisAdrId, resAdr.WorkspaceId);
            return resAdr;
        }
    }


}
