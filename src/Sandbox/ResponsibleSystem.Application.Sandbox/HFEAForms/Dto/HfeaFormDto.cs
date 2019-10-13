using System;
using ResponsibleSystem.Shared.Dto;

namespace ResponsibleSystem.Sandbox.HFEAForms.Dto
{
    public class HfeaFormDto
    {
        public string RootId { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public UserDto Publisher { get; set; }
        public DateTime CreateDate { get; set; }
        public string VersionNumber { get; set; }
        public string FormCode { get; set; }
        public int? ValidFor { get; set; }
        public int OrganizationId { get; set; }

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FormFile { get; set; }
        public string DescriptionVideo { get; set; }
        public string CreateDateString { get; set; }

        public bool UseDefaultPriority { get; set; }
        public int Priority { get; set; }

        public HfeaClassificationDto HfeaClassification { get; set; }
    }
}
