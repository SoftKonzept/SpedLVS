using Common.Models;
using LVS.ViewData;
using s2industries.ZUGFeRD;

namespace LVS.ZUGFeRD
{
    public class ZUGFeRD_Party
    {
        /// <summary>
        ///                 namespace s2industries.ZUGFeRD;
        ///                 public enum QuantityCodes
        /// </summary>
        /// <param name="myItem"></param>
        /// <returns></returns>
        //public static Party GetPartyItem(Addresses myAdr)
        public static Party GetPartyItem(Addresses myAdr, string myGLobalId)
        {
            string contactName = myAdr.ViewId;
            string name = myAdr.Name1;
            if ((myAdr.Name2 != null) && (myAdr.Name2 != string.Empty))
            {
                name += " " + myAdr.Name2;
            }              
            GlobalID globalId = new GlobalID(GlobalIDSchemeIdentifiers.CertifiedEmailAddress, myGLobalId);
            //GlobalID id = new GlobalID(GlobalIDSchemeIdentifiers.CompanyNumber, myAdr.Id.ToString());
            string street = myAdr.Street; // + " " + myAdrVD.Address.HouseNo;
            if((myAdr.HouseNo != null) && (myAdr.HouseNo != string.Empty))
            {
                street += " " + myAdr.HouseNo;
            }
            string addressline3 = myAdr.Name3;
            string city = myAdr.City;
            string postcode = myAdr.ZIP;
            string countysubdivision = string.Empty;
            CountryCodes cc = ZUGFeRD.ZUGFeRD_Country.ZUGFeRD_CountryCode(myAdr);

            Party partyItem = new Party
            {
                //----------------------------------------- BT-27 Seller name -> BG-4 SELLER 
                ContactName = contactName,
                //----------------------------------------- BT-28 Seller trading name -> BG-4 SELLER 
                Name = name,
                //----------------------------------------- BT-29 Seller identifier -> BG-4 SELLER 
                GlobalID = globalId,
                ID = null,
                //----------------------------------------- BT-30 Seller legal registration identifier -> BG-4 SELLER 
                //SpecifiedLegalOrganization = new LegalOrganization(), 

                //----------------------------------------- BT-35 Seller address line 1  -> BG-5 SELLER 
                //Name = "SZG Stahl Zentrum Glauchau GmbH & Co KG",
                //----------------------------------------- BT-36 Seller address line 2  -> BG-5 SELLER 
                Street = street,
                //----------------------------------------- BT-162 Seller address line 3  -> BG-5 SELLER 
                AddressLine3 = addressline3,
                //----------------------------------------- BT-37 Seller city  -> BG-5 SELLER 
                City = city,
                //----------------------------------------- BT-38 Seller post code -> BG-5 SELLER 
                Postcode = postcode,
                //----------------------------------------- BT-39 Seller country subdivision -> BG-5 SELLER 
                CountrySubdivisionName = countysubdivision,
                //----------------------------------------- BT-40 Seller country code -> BG-5 SELLER 
                Country = cc,

               
            };
            return partyItem;
        }

    }
}
