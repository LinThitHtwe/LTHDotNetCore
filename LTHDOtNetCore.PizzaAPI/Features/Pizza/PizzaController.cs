using LTHDOtNetCore.PizzaAPI.DTOs;
using LTHDOtNetCore.PizzaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.PizzaAPI.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public PizzaController()
        {
            _appDbContext = new AppDbContext();
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
        public async Task<IActionResult> GetOrderAsync(string invoiceNo)
        {
            var order = await _appDbContext.PizzaOrders.FirstOrDefaultAsync(pizzaOrder => pizzaOrder.InvoiceNo == invoiceNo);
            var orderDetail = await _appDbContext.PizzaOrderDetails.Where(pizzaOrderDetail => pizzaOrderDetail.OrderInvoiceNo  == invoiceNo).ToListAsync();

            return Ok(new
            {
                Order = order,
                OrderDetail = orderDetail
            });
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
