using DataSource.Stores;

namespace MovePackCleanApi.Services;

public class ServiceTypeService
{
    private readonly IServiceTypesStore serviceTypeStore;

    public ServiceTypeService(IServiceTypesStore serviceTypeStore)
    {
        this.serviceTypeStore = serviceTypeStore;
    }

    public async Task<ServiceTypes[]> GetAllServiceTyes()
    {
        var serviceTypes = new List<ServiceTypes>();
        await foreach(var type in serviceTypeStore.GetAllServiceTypes())
        {
            serviceTypes.Add(type);
        }
        return serviceTypes.ToArray();
    }
}
