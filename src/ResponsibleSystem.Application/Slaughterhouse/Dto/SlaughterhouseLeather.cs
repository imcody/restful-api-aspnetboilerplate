using Abp.Application.Services.Dto;

namespace ResponsibleSystem.Slaughterhouse.Dto
{
    public class SlaughterhouseLeatherDto: EntityDto<long>
    {
        public string PPNo { get; set; }
        public string IdNo { get; set; }
        public long FarmId { get; set; }
        public string  Farm { get; set; }
        public double Weight { get; set; }
        public bool EarsOn { get; set; }
    }
}
