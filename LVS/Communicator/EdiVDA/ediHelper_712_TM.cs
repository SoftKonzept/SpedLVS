namespace LVS
{
    public class ediHelper_712_TM
    {
        //712F14
        public string TMS { get; set; }
        public enumVDA4913_712F14_TMS enumTMS { get; set; }
        //712F15
        public string VehicleNo { get; set; }

        public bool IsWaggon { get; set; }
        public bool IsKFZ { get; set; }
        public bool IsShip { get; set; }

        public ediHelper_712_TM(string myTMS, string myVehicleNo)
        {
            this.IsWaggon = false;
            this.IsShip = false;
            this.IsKFZ = false;
            this.VehicleNo = myVehicleNo;
            this.TMS = myTMS;
            FormatVehicleNo();
        }

        private void FormatVehicleNo()
        {
            this.enumTMS = GetEnumTMS();
            switch (this.enumTMS)
            {
                case enumVDA4913_712F14_TMS.KFZ:
                    this.IsKFZ = true;
                    break;
                case enumVDA4913_712F14_TMS.Waggonnummer:
                    if (this.VehicleNo.Contains(" "))
                    {
                        this.VehicleNo = this.VehicleNo.Replace(" ", "");
                    }
                    if (this.VehicleNo.Contains("-"))
                    {
                        this.VehicleNo = this.VehicleNo.Replace("-", "");
                    }


                    if (this.VehicleNo.Length == 12)
                    {
                        this.VehicleNo = this.VehicleNo.Substring(0, 4) + " " + this.VehicleNo.Substring(4, 4) + " " + this.VehicleNo.Substring(8, 3) + "-" + this.VehicleNo.Substring(11);
                    }
                    else
                    {
                        string tmpWaggonNo = this.VehicleNo.Replace(" ", "");
                        if (tmpWaggonNo.Length == 12)
                        {
                            this.VehicleNo = tmpWaggonNo.Substring(0, 4) + " " + tmpWaggonNo.Substring(4, 4) + " " + tmpWaggonNo.Substring(8, 3) + "-" + tmpWaggonNo.Substring(11);
                        }
                        else
                        {
                            this.VehicleNo = tmpWaggonNo;
                        }
                    }
                    this.IsWaggon = true;
                    break;
                case enumVDA4913_712F14_TMS.Schiffsname:
                    this.IsShip = true;
                    break;
            }
        }

        private enumVDA4913_712F14_TMS GetEnumTMS()
        {
            switch (this.TMS)
            {
                //KFZG
                case "01":
                    return enumVDA4913_712F14_TMS.KFZ;
                    break;
                //Bordero
                case "02":
                    return enumVDA4913_712F14_TMS.Borderonummer;
                    break;
                //Stückgut
                case "06":
                    return enumVDA4913_712F14_TMS.Stückgutnummer;
                    break;
                //Express-Gut
                case "07":
                    return enumVDA4913_712F14_TMS.Expressgutnummer;
                    break;
                //Waggon
                case "08":
                    return enumVDA4913_712F14_TMS.Waggonnummer;
                    break;
                //Postpaket
                case "09":
                    return enumVDA4913_712F14_TMS.Postpaket;
                    break;
                //Flugzeug
                case "10":
                    return enumVDA4913_712F14_TMS.Flugnummer;
                    break;
                //Schiffsname
                case "11":
                    return enumVDA4913_712F14_TMS.Schiffsname;
                    break;
                default:
                    return enumVDA4913_712F14_TMS.KFZ;
                    break;
            }
        }


    }
}
