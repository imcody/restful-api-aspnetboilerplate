using System.Threading.Tasks;

namespace ResponsibleSystem.Shared.DataMigrations.Services
{
    public interface IDataMigration
    {
        Task<bool?> Run();
    }
}
