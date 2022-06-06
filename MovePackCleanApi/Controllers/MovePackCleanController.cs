using DataSource.Stores;
using Microsoft.AspNetCore.Mvc;
using MovePackCleanApi.Services;

namespace MovePackCleanApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovePackCleanController : ControllerBase
{
    private readonly ServiceTypeService service;
    private readonly OrderService orderService;
    private readonly CustomerService customerService;

    public MovePackCleanController(ServiceTypeService service,
        OrderService orderService,
        CustomerService customerService)
    {
        this.service = service;
        this.orderService = orderService;
        this.customerService = customerService;
    }

    [HttpGet("services/all")]
    public async Task<ActionResult> GetAllServiceTypes()
    {
        return Ok(await service.GetAllServiceTyes());
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<Customers>> GetCustomer([FromRoute] int customerId)
    {
        var customer = await customerService.GetCustomer(customerId);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpGet("customer/search/{customerInfo}")]
    public async Task<ActionResult<Customers>> SearchForCusomter([FromRoute] string customerInfo)
    {
        var customer = await customerService.SearchForCustomer(customerInfo);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPut("customer/{customerId}/update")]
    public async Task<ActionResult<Customers>> UpdateCustomer([FromRoute] int customerId, [FromQuery] string? name, [FromQuery] string? phoneNumber, [FromQuery] string? email)
    {
        var customer = await customerService.GetCustomer(customerId);
        if (customer is null)
        {
            return NotFound("Unable to find Customer with that information.");
        }
        customer = await customerService.UpdateCustomerInformation(customer, name, phoneNumber, email);
        return customer is not null ? Ok(customer) : StatusCode(StatusCodes.Status500InternalServerError);
    }
    [HttpGet("customer/order/{orderId}")]
    public async Task<ActionResult> GetOrder([FromRoute] int orderId)
    {
        return Ok(await orderService.GetOrderById(orderId));
    }

    [HttpPost("customer/order/create")]
    public async Task<ActionResult> PlaceNewOrder([FromBody] Order order)
    {
        return Ok(await orderService.PlaceNewOrder(order));
    }

    [HttpPut("customer/order/update")]
    public async Task<ActionResult> UpdateOrder([FromBody] OrderDetail orderDetails)
    {
        var updatedOrderDetails = await orderService.UpdateOrderInformation(orderDetails);
        return updatedOrderDetails is not null ? Ok(updatedOrderDetails) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("customer/order/{orderId}/delete")]
    public async Task<ActionResult> DeleteOrder([FromRoute] int orderId)
    {
        var order = await orderService.GetOrderById(orderId);
        var isDeleted = await orderService.DeleteOrder(orderId);
        return order is null ? NotFound() : isDeleted is true ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
