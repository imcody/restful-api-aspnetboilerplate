namespace ResponsibleSystem.Common.Config
{
    public interface IConfigFactory<T> where T : IConfig
    {
        T GetConfig();
    }
}
