using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Production.Dto
{
    public class ProductionDetailsDto
    {
        public long Id { get; set; }
        public long? UpperLeather { get; set; }
        public long? LiningLeather { get; set; }
        public long? BackCounterLeather { get; set; }
        public long? WeltLeather { get; set; }
        public long? SoleLeather { get; set; }
        public long? HeelLeather { get; set; }
        public long? InSockLeather { get; set; }
        public long? FillingLeather { get; set; }
        public long? ReinforcementLeather { get; set; }
        public long? RemovableInSockLeather { get; set; }
    }
}
