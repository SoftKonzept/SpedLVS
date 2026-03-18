using System.Collections.Generic;
using System.Linq;

namespace LVS.ASN.GlobalValues
{
    public class GlobalFieldVal_FilterByGlobalFieldVal
    {
        //public static List<clsASNArtFieldAssignment> GetMatchingAssignments(
        //    Dictionary<string, clsASNArtFieldAssignment> dictASNArtFieldAssignment, string myGlobalFieldVarFuncName)
        //{
        //    // Filtere die Einträge basierend auf den Bedingungen
        //    var matchingAssignments = dictASNArtFieldAssignment
        //        .Where(kvp => kvp.Value.IsGlobalFieldVar &&
        //                      kvp.Value.GlobalFieldVar == GlobalFieldVal_DeliveryNote.const_GlobalVar_DeliveryNote)
        //        .Select(kvp => kvp.Value)
        //        .ToList();

        //    return matchingAssignments;
        //}
        public static List<clsASNArtFieldAssignment> GetMatchingAssignments(
                                                                                Dictionary<string, clsASNArtFieldAssignment> dictASNArtFieldAssignment
                                                                                , string myGlobalFieldVarFuncName
                                                                           )
        {
            // Filtere die Einträge basierend auf den Bedingungen
            var matchingAssignments = dictASNArtFieldAssignment
                .Where(kvp => kvp.Value.IsGlobalFieldVar &&
                              kvp.Value.GlobalFieldVar == myGlobalFieldVarFuncName)
                .Select(kvp => kvp.Value)
                .ToList();

            return matchingAssignments;
        }
    }
}
