using Abp.Runtime.Validation;

namespace ResponsibleSystem.Tannery.Dto
{
    public class RegisterTanneryLeatherInput : IShouldNormalize
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }
        public bool IsCrust { get; set; }
        public string Thickness { get; set; }
        public double TotalArea { get; set; }
        public decimal PricePerFt { get; set; }
        public bool IsWaxed { get; set; }
        public string Extra { get; set; }

        public void Normalize()
        {
            PPNo = PPNo?.ToUpper();
            IdNo = IdNo?.ToUpper();
        }
    }
}
