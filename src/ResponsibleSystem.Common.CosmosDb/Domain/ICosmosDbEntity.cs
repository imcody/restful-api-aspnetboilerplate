namespace ResponsibleSystem.Common.CosmosDb.Domain
{
    public interface ICosmosDbEntity
    {
        string Id { get; }
        string Entity { get; }
    }
}