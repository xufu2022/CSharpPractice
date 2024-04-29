using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XJSerialize
{
    [XmlRoot("Vehicle", Namespace = "")]
    public class VehicleAuction
    {
        [XmlIgnore]
        public int MemberID { get; set; }
        [XmlIgnore]
        public int ExportID { get; set; }
        public int AuctionID { get; set; }
        public int VehicleID { get; set; }
        public string Ref { get; set; }
        public int? SiteID { get; set; }
        public string Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? RegNo { get; set; }
        public int? RegYear { get; set; }
        public string? Colour { get; set; }
        public string? Fuel { get; set; }
        public string Damage { get; set; }
        public int Doors { get; set; }
        public string? Body { get; set; }
        public short CC { get; set; }
        public string Speedo { get; set; }
        public string? TransSpeed { get; set; }
        public string? TransType { get; set; }
        public string TrimLevel { get; set; }
        public string? Engine { get; set; }
        public string Cat { get; set; }
        public decimal? ReservePrice { get; set; }
        public decimal? StartPrice { get; set; }
        public bool? HasVAT { get; set; }
        public bool? Keys { get; set; }
        public int? Starts { get; set; }
        public int? Drives { get; set; }
        public bool? Stereo { get; set; }
        public bool? VINPlate { get; set; }
        public string VINNumber { get; set; }
        public bool? LogBook { get; set; }
        public decimal? PAV { get; set; } //not included
        public decimal? CostPrice { get; set; } //not included
        public string? CanBeViewed { get; set; }
        public bool? FeaturedVehicle { get; set; }
        public string InsCoName { get; set; }
        public int BranchCode { get; set; }
        public bool? ServiceHistory { get; set; }
        public bool? NSAOwnVehicle { get; set; }
        public int CarWebID { get; set; }
        public string CarWebMake { get; set; }
        public string CarWebModel { get; set; }
        public string CarWebModelSeries { get; set; }
        public decimal? AdminFeeExVAT { get; set; }
        public string EngineCode { get; set; }
        public decimal? AuctionFeeVAT { get; set; }
        public decimal AdditionalFee { get; set; }
        public int? BodyTypeID { get; set; }
        public DateTime DateFirstReg { get; set; }
        public bool Bundle { get; set; }
        public decimal CAP_Average { get; set; }
        public decimal CostOfRepair { get; set; }
        public string InsuranceBranchName { get; set; }
        
        public string Images { get; set; }
        
        public string Videos { get; set; }
    }

}
