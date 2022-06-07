namespace DataSource.Stores;

public interface ICustomerStore
{
    IAsyncEnumerable<Customer> SearchForCustomer(string customerInfo);
    Task<Customer?> GetCustomer(int customerId);
    Task<int> NewCustomer(Customer customer);
    Task<bool> UpdateCustomerInformation(Customer customer);
}

public sealed record Customer
{
    public int CustomerId { get; init; }
    public string Name { get; init; } = String.Empty;
    public string Email { get; init; } = String.Empty;
    public string PhoneNumber { get; init; } = String.Empty;
}