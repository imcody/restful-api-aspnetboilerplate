using System.ComponentModel.DataAnnotations;

namespace ResponsibleSystem.Sandbox.HFEAForms.Dto
{
    public class UpdateHfeaFormInput
    {
        [Required]
        public string ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        [Required]
        [StringLength(5)]
        public string FormCode { get; set; }

        [Required]
        public string VersionNumber { get; set; }

        public string FormFile { get; set; }

        public string DescriptionVideo { get; set; }

        public bool UseDefaultPriority { get; set; }

        public int Priority { get; set; }

        [Required]
        public HfeaClassificationDto HfeaClassification { get; set; }
    }
}
