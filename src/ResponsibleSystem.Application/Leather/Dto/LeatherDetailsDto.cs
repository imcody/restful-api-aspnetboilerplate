using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Leather.Dto
{
    public class LeatherDetailsDto
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime EstimatedSlaughterDate { get; set; }

        public double Weight { get; set; }

        public bool IsCrust { get; set; }

        public string Thickness { get; set; }
        public double TotalArea { get; set; }
        public decimal PricePerFt { get; set; }
        public bool IsWaxed { get; set; }
        public string Extra { get; set; }
        public string Color { get; set; }
        public bool EarsOn { get; set; }
        public string Status { get; set; }

        public string Farm { get; set; }
        public string Slaughterhouse { get; set; }
        public string Tannery { get; set; }
    }
}
