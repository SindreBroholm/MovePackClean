using Dapper;

namespace DataSource.Stores;

public class OrderStore : IOrderStore
{
    private readonly DapperContext context;

    public OrderStore(DapperContext context)
    {
        this.context = context;
    }


    public async Task<int> PlaceNewOrder(int customerId, OrderDetail orderDetail)
    {
        var query = @"
            INSERT INTO [dbo].[OrderDetails]
                ([ServiceType], [PrimaryAddress], [SecondaryAddress], [StartTime], [EndTime], [Details])
            VALUES (@ServiceType, @PrimaryAddress, @SecondaryAddress, @StartTime, @EndTime, @Details)
            
            INSERT INTO [dbo].[Orders]
                    ([CustomerId], [OrderDetailId])
                VALUES (@CustomerId, SCOPE_IDENTITY())
        ";

        using var connection = context.CreateConnection();
        var result = connection.ExecuteAsync(query, new
        {
            CustomerId = customerId,
            ServiceType = orderDetail.ServiceTypeId,
            PrimaryAddress = orderDetail.PrimaryAddress,
            SecondaryAddress = orderDetail.SecondaryAddress,
            StartTime = orderDetail.StartTime,
            EndTime = orderDetail.EndTime,
            Details = orderDetail.Details
        });

        return await result;
    }
}
