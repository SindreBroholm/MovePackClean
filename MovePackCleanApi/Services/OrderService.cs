using DataSource.Stores;
using System.Linq;

namespace MovePackCleanApi.Services;

public class OrderService
{
    private readonly IOrderStore orderStore;
    private readonly ICustomerStore customerStore;

    public OrderService(IOrderStore orderStore, ICustomerStore customerStore)
    {
        this.orderStore = orderStore;
        this.customerStore = customerStore;
    }

    public async Task<bool> PlaceNewOrder(Order order)
    {
        var customer = await customerStore.GetCustomer(order.Customer.CustomerId).FirstAsync();

        var customerId = customer is null ? await customerStore.NewCustomer(order.Customer) : order.Customer.CustomerId;
        // få tak i id og returner ordre detaljer ?
        return await orderStore.PlaceNewOrder(customerId, order.OrderDetail) > 0;
    }
}
