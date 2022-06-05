namespace DataSource.Stores;

public interface ICustomerStore
{
    IAsyncEnumerable<Customers> SearchForCustomer(string customerInfo);
    Task<Customers?> GetCustomer(int customerId);
    Task<int> NewCustomer(Customers customer);
    Task<bool> UpdateCustomerInformation(Customers customer);
}

public sealed record Customers
{
    public int CustomerId { get; init; }
    public string Name { get; init; } = String.Empty;
    public string Email { get; init; } = String.Empty;
    public string PhoneNumber { get; init; } = String.Empty;
}