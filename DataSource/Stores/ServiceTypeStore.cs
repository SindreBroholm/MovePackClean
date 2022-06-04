using Dapper;

namespace DataSource.Stores;

public class ServiceTypeStore : IServiceTypesStore
{
    private readonly DapperContext context;

    public ServiceTypeStore(DapperContext context)
    {
        this.context = context;
    }

    public async IAsyncEnumerable<ServiceTypes> GetAllServiceTypes()
    {
        var query = @"
            SELECT * FROM [TheMovers].[dbo].[ServiceTypes]
        ";

        using var connection = context.CreateConnection();
        var serviceTypes = await connection.QueryAsync<ServiceTypes>(query);

        foreach(var type in serviceTypes)
        {
            if (type.Active)
            {
                yield return type;
            }
        }
    }
}
