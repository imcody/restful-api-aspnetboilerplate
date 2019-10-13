using ResponsibleSystem.Sandbox.HFEAForms.Dto;

namespace ResponsibleSystem.Sandbox.HFEAForms.Services
{
    public interface IHfeaFormValidationService
    {
        void Validate(CreateHfeaFormInput input);
        void Validate(UpdateHfeaFormInput input);
    }
}
