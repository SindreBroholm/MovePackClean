using Dapper;

namespace DataSource.Stores;

public class OrderStore : IOrderStore
{
    private readonly DapperContext context;

    public OrderStore(DapperContext context)
    {
        this.context = context;
    }

    public async Task<bool> DeleteOrder(int orderId)
    {
        var query = @"
            DELETE FROM ORDERS
            WHERE [OrderId] = @OrderId
        ";

        using var connection = context.CreateConnection();
        return await connection.ExecuteAsync(query, new { OrderId = orderId }) > 0;
    }

    public async Task<Order?> GetOrderById(int orderId)
    {
        var query = @"
            SELECT
                o.[OrderId], 
                c.[CustomerId], c.[Name], c.[PhoneNumber], c.[Email],
                od.[OrderDetailId], od.[ServiceType], od.[PrimaryAddress], od.[SecondaryAddress], od.[StartTime], od.[EndTime], od.[Details]
            FROM Orders o
            INNER JOIN OrderDetails od ON od.OrderDetailId = o.OrderDetailId
            INNER JOIN Customers c ON c.CustomerId = o.CustomerId
        ";

        using var connection = context.CreateConnection();
        var order = await connection.QueryAsync<Order, Customers, OrderDetail, Order>(query, 
            (order, Customers, OrderDetail) => {
                order.Customer = Customers;
                order.OrderDetail = OrderDetail;
                return order; }, splitOn: "CustomerId, OrderDetailId");
        
        return order.FirstOrDefault(o => o.OrderId == orderId);
    }

    public async Task<int> PlaceNewOrder(int customerId, OrderDetail orderDetail)
    {
        var query = @"
            INSERT INTO [dbo].[OrderDetails]
                ([ServiceType], [PrimaryAddress], [SecondaryAddress], [StartTime], [EndTime], [Details])
            VALUES (@ServiceType, @PrimaryAddress, @SecondaryAddress, @StartTime, @EndTime, @Details)
            
            INSERT INTO [dbo].[Orders]
                    ([CustomerId], [OrderDetailId])
            OUTPUT INSERTED.OrderId
                VALUES (@CustomerId, SCOPE_IDENTITY())
        ";

        using var connection = context.CreateConnection();
        var orderId = connection.ExecuteScalarAsync<int>(query, new
        {
            CustomerId = customerId,
            ServiceType = orderDetail.ServiceTypeId,
            PrimaryAddress = orderDetail.PrimaryAddress,
            SecondaryAddress = orderDetail.SecondaryAddress,
            StartTime = orderDetail.StartTime,
            EndTime = orderDetail.EndTime,
            Details = orderDetail.Details
        });

        return await orderId;
    }
}
