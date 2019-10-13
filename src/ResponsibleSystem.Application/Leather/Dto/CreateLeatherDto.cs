using System;
using System.Collections.Generic;
using System.Text;
using Abp.Runtime.Validation;

namespace ResponsibleSystem.Leather.Dto
{
    public class CreateLeatherDto: IShouldNormalize
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime EstimatedSlaughterDate { get; set; }
        public long? FarmId { get; set; }
        public void Normalize()
        {
            PPNo = PPNo?.ToUpper();
            IdNo = IdNo?.ToUpper();
        }
    }
}
