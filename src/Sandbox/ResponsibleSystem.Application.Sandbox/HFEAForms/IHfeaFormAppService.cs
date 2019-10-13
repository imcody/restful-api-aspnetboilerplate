using ResponsibleSystem.Sandbox.HFEAForms.Domain;
using ResponsibleSystem.Sandbox.HFEAForms.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResponsibleSystem.Sandbox.HFEAForms
{
    public interface IHfeaFormAppService
    {
        Task<HfeaFormDto> Get(string id);
        Task<HfeaFormReadOnlyDto> GetReadOnly(string id);
        Task<IList<HfeaFormReadOnlyDto>> GetAll();
        Task<HfeaForm> Create(CreateHfeaFormInput input);
        Task<HfeaForm> Edit(UpdateHfeaFormInput input);
        Task Delete(string id);
    }
}
