using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Slaughterhouse.Dto
{
    public class UpdateLeatherInput
    {
        public long Id { get; set; }
        public double Weight { get; set; }
        public bool EarsOn { get; set; }
    }
}
