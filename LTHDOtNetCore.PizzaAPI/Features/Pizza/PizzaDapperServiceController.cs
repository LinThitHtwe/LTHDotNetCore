using LTHDOtNetCore.PizzaAPI.DTOs;
using LTHDOtNetCore.PizzaAPI.Models;
using LTHDOtNetCore.PizzaAPI.Queries;
using LTHDOtNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static LTHDOtNetCore.PizzaAPI.DTOs.PizzaOrderDTO;

namespace LTHDOtNetCore.PizzaAPI.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaDapperServiceController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperService;
        public PizzaDapperServiceController()
        {
            _appDbContext = new AppDbContext();
            _dapperService = new DapperService(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzasAsync()
        {
            var pizzas = await _appDbContext.Pizzas.ToListAsync();
            return Ok(pizzas);
        }

        [HttpGet("GetExtras")]
        public async Task<IActionResult> GetExtrasEasyn()
        {
            var pizzas = await _appDbContext.PizzaExtras.ToListAsync();
            return Ok(pizzas);
        }

        [HttpGet("Order/{invoiceNo}")]
        public  IActionResult GetOrderAsync(string invoiceNo)
        {
            var pizzaOrder = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceDTO>
                            (
                                PizzaQueries.PizzaOrderQuery,
                                new { InvoiceNo = invoiceNo }
                            );

            var pizzaOrderDetail = _dapperService.Query<PizzaOrderInvoiceDetailModel>
                                (
                                PizzaQueries.PizzaOrderDetailQuery,
                                new { InvoiceNo = invoiceNo }
                                );

            if(pizzaOrder is null || pizzaOrderDetail is null)
            {
                return NotFound();
            }

            var response = new PizzaOrderInvoiceResponse()
                            {
                            Order = pizzaOrder,
                            OrderDetail = pizzaOrderDetail
                            };

            return Ok(response);
            
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequestDTO orderRequest)
        {
            var orderedPizza = await _appDbContext.Pizzas.FirstOrDefaultAsync(pizza => pizza.Id == orderRequest.PizzaId);

            if (orderedPizza is null)
            {
                return NotFound();
            }

            var totalPrice = orderedPizza.Price;

            if (orderRequest.Extras.Length > 0)
            {
                var pizzaExtrasList = await _appDbContext.PizzaExtras.Where(pizzaExtra => orderRequest.Extras.Contains(pizzaExtra.Id)).ToListAsync();
                totalPrice += pizzaExtrasList.Sum(pizzaExtra => pizzaExtra.Price);
            }

            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");

            PizzaOrderModel pizzaOrderModel = new()
            {
                PizzaId = orderRequest.PizzaId,
                InvoiceNo = invoiceNo,
                TotalAmount = totalPrice
            };

            List<PizzaOrderDetailModel> pizzaOrderDetails = orderRequest.Extras.Select(pizzaExtraId => new PizzaOrderDetailModel()
            {
                OrderInvoiceNo = invoiceNo,
                PizzaExtraId = pizzaExtraId
            }).ToList();

            await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaOrderDetails);
            await _appDbContext.SaveChangesAsync();

            OrderResponseDTO orderResponse = new()
            {
                InvoiceNo = invoiceNo,
                Message = "Thanks for the purchase",
                TotalAmount = totalPrice,
            };

            return Ok(orderResponse);
        }
    }
}
