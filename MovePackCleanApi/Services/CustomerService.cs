using DataSource.Stores;

namespace MovePackCleanApi.Services;

public class CustomerService
{
    private readonly ICustomerStore customerStore;

    public CustomerService(ICustomerStore customerStore)
    {
        this.customerStore = customerStore;
    }

    public async Task<Customer?> GetCustomer(int customerId)
    {
        return await customerStore.GetCustomer(customerId).FirstOrDefaultAsync();
    }
    public async Task<Customer?> SearchForCustomer(string customerInfo)
    {
        return await customerStore.SearchForCustomer(customerInfo).FirstOrDefaultAsync();
    }

    public async Task<Customer?> UpdateCustomerInformation(Customer customer, string? name, string? phoneNumber, string? email)
    {
        var udatedInfo = new Customer()
        {
            CustomerId = customer.CustomerId,
            Name = name ?? customer.Name,
            Email = email ?? customer.Email,
            PhoneNumber = phoneNumber ?? customer.PhoneNumber,
        };

        await customerStore.UpdateCustomerInformation(udatedInfo);

        return await customerStore.GetCustomer(udatedInfo.CustomerId).FirstOrDefaultAsync();
    }
}
