using System.ComponentModel.DataAnnotations;
using ResponsibleSystem.Sandbox.HFEAForms.Domain;

namespace ResponsibleSystem.Sandbox.HFEAForms.Dto
{
    public class HfeaClassificationDto
    {
        [Required]
        public FertilityTreatmentMultiSelect FertilityTreatmentType { get; set; }

        [Required]
        public PatientTypeMultiSelect PatientType { get; set; }

        [Required]
        public MaritalStatusMultiSelect MaritalStatus { get; set; }

        [Required]
        public string FormCategory { get; set; }
    }
}
