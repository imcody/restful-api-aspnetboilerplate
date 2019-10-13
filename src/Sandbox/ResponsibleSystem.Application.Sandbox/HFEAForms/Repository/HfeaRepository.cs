using ResponsibleSystem.Sandbox.HFEAForms.Domain;
using ResponsibleSystem.Common.CosmosDb.Repositories;
using ResponsibleSystem.Common.CosmosDb.Services;

namespace ResponsibleSystem.Sandbox.HFEAForms.Repository
{
    /// <summary>
    /// Custom HfeaForm repository - we could have additional methods apart of generic methods that comes from ICosmosDbRepository<HfeaForm>
    /// </summary>
    public class HfeaRepository : CosmosDbRepository<HfeaForm>, IHfeaRepository
    {
        public HfeaRepository(ICosmosDbClient cosmosDbClient) : base(cosmosDbClient)
        {
        }

        // Custom methods

        public int GetHfeaFormRoot(string id)
        {
            // some complex implementation here
            return 1;
        }
    }
}
