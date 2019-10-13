using ResponsibleSystem.Common.CosmosDb.Repositories;
using ResponsibleSystem.Sandbox.HFEAForms.Domain;
using ResponsibleSystem.Sandbox.HFEAForms.Dto;

namespace ResponsibleSystem.Sandbox.HFEAForms.Services
{

    public class HfeaFormValidationService : IHfeaFormValidationService
    {
        private readonly ICosmosDbRepository<HfeaForm> _hfeaRepository;

        public HfeaFormValidationService(ICosmosDbRepository<HfeaForm> hfeaRepository)
        {
            _hfeaRepository = hfeaRepository;
        }


        public void Validate(CreateHfeaFormInput input)
        {
            // Complex logic to make sure we are good - since we are not returning true false in case of issues throw on of the exceptions like:
            // throw new ResponsibleSystemUserFriendlyException($"Clinic with given HFEA centre reference number already exists.");
        }

        public void Validate(UpdateHfeaFormInput input)
        {
            // Complex logic to make sure we are good
        }
    }
}
