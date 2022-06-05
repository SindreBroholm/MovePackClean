using Dapper;

namespace DataSource.Stores;

public class OrderDetailStore : IOrderDetailStore
{
    private readonly DapperContext context;

    public OrderDetailStore(DapperContext context)
    {
        this.context = context;
    }

    public async Task<OrderDetail?> GetOrderDetailById(int orderDetailId)
    {
        var query = @"
            SELECT 
                [OrderDetailId], [ServiceType], [PrimaryAddress], [SecondaryAddress], 
                [StartTime], [EndTime], [Details]
            FROM [dbo].[OrderDetails]
                WHERE [OrderDetailId] = @OrderDetailId
        ";

        using var connection = context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<OrderDetail>(query, new
        {
            OrderDetailId = orderDetailId
        });
    }

    public async Task<int> UpdateOrderDetail(OrderDetail orderDetail)
    {
        var query = @"
            UPDATE [dbo].[OrderDetails]
            SET
                [ServiceType] = @ServiceType,
                [PrimaryAddress] = @PrimaryAddress, 
                [SecondaryAddress] = @SecondaryAddress
                [StartTime] = @StartTime
                [EndTime] =  @EndTime
                [Details] = @Details
            WHERE [OrderDetailId] = @OrderDetailId
        ";

        using var connection = context.CreateConnection();
        return await connection.ExecuteAsync(query, new
        {
            ServiceType = orderDetail.ServiceTypeId,
            PrimaryAddress = orderDetail.PrimaryAddress,
            SecondaryAddress = orderDetail.SecondaryAddress,
            StartTime = orderDetail.StartTime,
            EndTime = orderDetail.EndTime,
            Details = orderDetail.Details
        });
    }
}
