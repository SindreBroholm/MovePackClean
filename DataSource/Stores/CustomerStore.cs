using Dapper;

namespace DataSource.Stores;

public class CustomerStore : ICustomerStore
{
    private readonly DapperContext context;

    public CustomerStore(DapperContext context)
    {
        this.context = context;
    }

    public async Task<bool> UpdateCustomerInformation(Customers udatedInfo)
    {
        var query = @"
            UPDATE [dbo].[Customers]
            SET [Name] = @Name,
                [PhoneNumber] = @PhoneNumber,
                [Email] = @Email
            WHERE [CustomerId] = @CustomerId
        ";
        using var connection = context.CreateConnection();
        var result = await connection.ExecuteAsync(query, new
        {
            Name = udatedInfo.Name,
            PhoneNumber = udatedInfo.PhoneNumber, 
            Email = udatedInfo.Email,
            CustomerId = udatedInfo.CustomerId
        });

        return result > 0;
    }

    public async IAsyncEnumerable<Customers> SearchForCustomer(string customerInfo)
    {
        var query = @"
            SELECT DISTINCT 
                [CustomerId], [Name], [PhoneNumber], [Email]
            FROM [dbo].[Customers]
                WHERE [Name] = @customerInfo 
                    OR [PhoneNumber] = @customerInfo 
                    OR [Email] = @customerInfo
        ";

        using var connection = context.CreateConnection();
        foreach (var customer in await connection.QueryAsync<Customers>(query, new { customerInfo = customerInfo }))
        {
                yield return customer;
        }
    }

    public async Task<int> NewCustomer(Customers customer)
    {
        var query = @"
            INSERT INTO [dbo].[Customers]
                ([Name], [PhoneNumber], [Email])
            OUTPUT INSERTED.CustomerId
            VALUES 
                (@Name, @PhoneNumber, @Email)
            
        ";

        using var connection = context.CreateConnection();
        var customerId = connection.ExecuteScalarAsync<int>(query, new
        {
            Name = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
        });

        return await customerId;
    }

    public async Task<Customers?> GetCustomer(int customerId)
    {
        var query = @"
            SELECT DISTINCT 
                [CustomerId], [Name], [PhoneNumber], [Email]
            FROM [dbo].[Customers]
                WHERE [CustomerId] = @CustomerId
        ";

        using var connection = context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Customers>(query, new { CustomerId = customerId });
    }
}
