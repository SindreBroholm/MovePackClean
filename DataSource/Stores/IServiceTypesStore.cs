namespace DataSource.Stores;

public interface IServiceTypesStore
{
    IAsyncEnumerable<ServiceTypes> GetAllServiceTypes();
}

public sealed record ServiceTypes
{
    public int TypeId { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool Active { get; init; }
}