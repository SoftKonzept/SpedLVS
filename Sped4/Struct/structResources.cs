using System;

namespace Sped4.Struct
{
    ///<summary>Globals / struct _Recources</summary>
    ///<remarks>Recources für Fahrzeuge (z.B. Fahrer, Auflieger etc. kommt aus Tabelle VehicleRecources</remarks>
    public struct structRecources
    {
        public decimal RecourceID;
        public String RecourceTyp;
        public DateTime TimeFrom;
        public DateTime TimeTo;
        public decimal VehicleID;
        public decimal PersonalID;
        public Int32 Status;
        public string Name;
        public string KFZ;
        public System.Drawing.Point oldPosition;
        public decimal oldVehicleIDZM;
        public DateTime oldTimeFrom;
        public DateTime oldTimeTo;
        public DateTime nRecStartTime;  //next Resscource Starttime
        public DateTime fRecEndTime; // vorhergehende Resscource Endzeit
    }
}
