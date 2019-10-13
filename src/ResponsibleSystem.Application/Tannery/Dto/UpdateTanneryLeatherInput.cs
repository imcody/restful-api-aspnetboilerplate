namespace ResponsibleSystem.Tannery.Dto
{
    public class UpdateTanneryLeatherInput
    {
        public long Id { get; set; }
        public bool IsCrust { get; set; }
        public string Thickness { get; set; }
        public double TotalArea { get; set; }
        public decimal PricePerFt { get; set; }
        public bool IsWaxed { get; set; }
        public string Extra { get; set; }
    }
}
