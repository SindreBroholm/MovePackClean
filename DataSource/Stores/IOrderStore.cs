namespace DataSource.Stores;

public interface IOrderStore
{
    Task<int> PlaceNewOrder(int customerId, OrderDetail orderDetail);
    Task<Order?> GetOrderByOrderDetailId(int orderDetailId);
    Task<bool> DeleteOrderDetail(int orderId);
}

public sealed record Order
{
    public int OrderId { get; init; }
    public Customer Customer { get; set; } = new();
    public OrderDetail OrderDetail { get; set; } = new();
}

public sealed record OrderDetail
{
    public int OrderDetailId { get; init; }
    public int ServiceTypeId { get; init; } = new();
    public string PrimaryAddress { get; init; } = String.Empty;
    public string? SecondaryAddress { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public string? Details { get; init; }
}