namespace LVS.Communicator.EdiVDA
{
    public class EdiSegmentCheck
    {
        public const string const_EdiSegmentCheck_BMW = EdiSegmentCheck_BMW.const_EdiSegmentCheck_BMW;
        public const string const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement = EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement;
        public static bool Check(clsEdiVDACreate myEdiCreate)
        {
            bool result = true;
            if (
                    (myEdiCreate.ediSegment.EdiSegmentCheckFunction != null) &&
                    (myEdiCreate.ediSegment.EdiSegmentCheckFunction.Length > 0)
               )
            {
                switch (myEdiCreate.ediSegment.EdiSegmentCheckFunction)
                {
                    case EdiSegmentCheck.const_EdiSegmentCheck_BMW:
                        result = EdiSegmentCheck_BMW.Check(myEdiCreate);
                        break;
                    case EdiSegmentCheck.const_EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement:
                        result = EdiSegmentCheck_INVRPTD96A_IsStoreOutMovement.Check(myEdiCreate);
                        break;
                }
            }
            return result;
        }
    }
}
