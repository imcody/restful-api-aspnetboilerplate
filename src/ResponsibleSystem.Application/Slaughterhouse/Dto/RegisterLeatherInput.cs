using System;
using System.Collections.Generic;
using System.Text;
using Abp.Runtime.Validation;

namespace ResponsibleSystem.Slaughterhouse.Dto
{
    public class RegisterLeatherInput: IShouldNormalize
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }
        public double Weight { get; set; }
        public bool EarsOn { get; set; }
        public void Normalize()
        {
            PPNo = PPNo?.ToUpper();
            IdNo = IdNo?.ToUpper();
        }
    }
}
