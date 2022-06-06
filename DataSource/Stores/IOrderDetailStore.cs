namespace DataSource.Stores;

public interface IOrderDetailStore
{
    Task<OrderDetail?> GetOrderDetailById(int orderDetailId);
    Task<int> UpdateOrderDetail(OrderDetail orderDetail);
}
