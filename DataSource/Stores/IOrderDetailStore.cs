using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Stores;

public interface IOrderDetailStore
{
    Task<OrderDetail?> GetOrderDetailById(int orderDetailId);
    Task<int> UpdateOrderDetail(OrderDetail orderDetail);
}
