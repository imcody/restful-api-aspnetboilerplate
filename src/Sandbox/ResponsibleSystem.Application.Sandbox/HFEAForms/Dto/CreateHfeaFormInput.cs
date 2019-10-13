using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace ResponsibleSystem.Sandbox.HFEAForms.Dto
{
    public class CreateHfeaFormInput : ICustomValidate
    {
        public int? OrganizationId { get; set; }

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

        public HfeaClassificationDto HfeaClassification { get; set; }

        
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (OrganizationId == 0)
            {
                context.Results.Add(new ValidationResult("You are not allowed to create the form for another organization. Please check your input parameters and try again."));
            }

            var apbSession = context.IocResolver.Resolve<IAbpSession>();
            if (apbSession.TenantId.HasValue && apbSession.TenantId != OrganizationId)
            {
                context.Results.Add(new ValidationResult("You are not allowed to create the form for another organization. Please check your input parameters and try again."));
            }
        }
    }
}
