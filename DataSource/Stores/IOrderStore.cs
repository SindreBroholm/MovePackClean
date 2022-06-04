namespace DataSource.Stores;

public interface IOrderStore
{
    Task<int> PlaceNewOrder(int customerId, OrderDetail orderDetail);
}

public sealed record Order
{
    public Customer Customer { get; init; } = new();
    public OrderDetail OrderDetail { get; init; } = new();
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