using ResponsibleSystem.Sandbox.HFEAForms.Domain;
using ResponsibleSystem.Common.CosmosDb.Repositories;

namespace ResponsibleSystem.Sandbox.HFEAForms.Repository
{
    public interface IHfeaRepository : ICosmosDbRepository<HfeaForm>
    {
        int GetHfeaFormRoot(string id);
    }
}