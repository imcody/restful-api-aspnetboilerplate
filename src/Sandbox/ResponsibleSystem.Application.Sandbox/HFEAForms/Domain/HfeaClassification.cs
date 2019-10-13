namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public class HfeaClassification
    {
        public FertilityTreatmentMultiSelect FertilityTreatmentType { get; set; }
        public PatientTypeMultiSelect PatientType { get; set; }
        public MaritalStatusMultiSelect MaritalStatus { get; set; }
        public FormCategory? FormCategory { get; set; }
    }
}
