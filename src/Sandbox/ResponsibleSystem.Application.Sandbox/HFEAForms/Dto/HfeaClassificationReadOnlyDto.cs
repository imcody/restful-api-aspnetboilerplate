using ResponsibleSystem.Sandbox.HFEAForms.Domain;

namespace ResponsibleSystem.Sandbox.HFEAForms.Dto
{
    public class HfeaClassificationReadOnlyDto
    {
        public string FertilityTreatment { get; set; }
        public PatientTypeMultiSelect PatientType { get; set; }
        public string MaritalStatus { get; set; }
        public string FormCategory { get; set; }
    }
}
