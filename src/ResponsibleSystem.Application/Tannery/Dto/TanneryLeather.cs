using Abp.Application.Services.Dto;

namespace ResponsibleSystem.Tannery.Dto
{
    public class TanneryLeatherDto : EntityDto<long>
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }
        public long FarmId { get; set; }
        public string  Farm { get; set; }
        public bool IsCrust { get; set; }
        public string Thickness { get; set; }
        public double TotalArea { get; set; }
        public decimal PricePerFt { get; set; }
        public bool IsWaxed { get; set; }
        public string Extra { get; set; }
        public bool InStorage { get; set; }
    }
}
