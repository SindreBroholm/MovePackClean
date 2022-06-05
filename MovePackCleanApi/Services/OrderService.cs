using DataSource.Stores;
using System.Linq;

namespace MovePackCleanApi.Services;

public class OrderService
{
    private readonly IOrderStore orderStore;
    private readonly ICustomerStore customerStore;
    private readonly IOrderDetailStore orderDetailStore;

    public OrderService(IOrderStore orderStore, ICustomerStore customerStore, IOrderDetailStore orderDetailStore)
    {
        this.orderStore = orderStore;
        this.customerStore = customerStore;
        this.orderDetailStore = orderDetailStore;
    }

    public async Task<Order?> PlaceNewOrder(Order order)
    {
        var customer = await customerStore.GetCustomer(order.Customer.CustomerId);

        var customerId = customer is null ? await customerStore.NewCustomer(order.Customer) : order.Customer.CustomerId;
        var orderId = await orderStore.PlaceNewOrder(customerId, order.OrderDetail);
        return await orderStore.GetOrderById(orderId);
    }

    public async Task<OrderDetail?> UpdateOrderInformation(OrderDetail orderDetail)
    {
        await orderDetailStore.UpdateOrderDetail(orderDetail);
        return await orderDetailStore.GetOrderDetailById(orderDetail.OrderDetailId);
    }

    public async Task<Order?> GetOrderById(int orderId)
    {
        return await orderStore.GetOrderById(orderId);
    }
}
