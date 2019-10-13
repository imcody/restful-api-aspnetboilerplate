using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Models.PublicApi
{
    class ProductionInfoDto
    {
        public LeatherInfo[] Leathers { get; set; }

        public ProductionPartInfo UpperLeather { get; set; }
        public ProductionPartInfo LiningLeather { get; set; }
        public ProductionPartInfo BackCounterLeather { get; set; }
        public ProductionPartInfo WeltLeather { get; set; }
        public ProductionPartInfo SoleLeather { get; set; }
        public ProductionPartInfo HeelLeather { get; set; }
        public ProductionPartInfo InSockLeather { get; set; }
        public ProductionPartInfo FillingLeather { get; set; }
        public ProductionPartInfo ReinforcementLeather { get; set; }
        public ProductionPartInfo RemovableInSockLeather { get; set; }
    }
    class LeatherInfo
    {
        public long RSID { get; set; }
        public string FarmName { get; set; }
        public double FarmLatitude { get; set; }
        public double FarmLongitude { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool IsCrust { get; set; }
        public bool IsWaxed { get; set; }
        public string ExtraInfo { get; set; }
        public string Color { get; set; }
    }

    class ProductionPartInfo
    {
        public long RSID { get; set; }
    }
}
