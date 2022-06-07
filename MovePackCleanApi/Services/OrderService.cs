using DataSource.Stores;

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

    public async Task<bool> DeleteOrder(int orderDetailId)
    {
       return await orderStore.DeleteOrderDetail(orderDetailId);    
    }

    public async Task<Order?> PlaceNewOrder(Order order)
    {
        var customer = await customerStore.GetCustomer(order.Customer.CustomerId);

        var customerId = customer is null ? await customerStore.NewCustomer(order.Customer) : order.Customer.CustomerId;
        var orderId = await orderStore.PlaceNewOrder(customerId, order.OrderDetail);
        return await orderStore.GetOrderByOrderDetailId(orderId);
    }

    public async Task<OrderDetail?> UpdateOrderInformation(OrderDetail orderDetail)
    {
        await orderDetailStore.UpdateOrderDetail(orderDetail);
        return await orderDetailStore.GetOrderDetailById(orderDetail.OrderDetailId);
    }

    public async Task<Order?> GetOrderByOrderDetailId(int orderId)
    {
        return await orderStore.GetOrderByOrderDetailId(orderId);
    }
}
