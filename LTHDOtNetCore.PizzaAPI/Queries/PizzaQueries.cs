namespace LTHDOtNetCore.PizzaAPI.Queries
{
    public class PizzaQueries
    {
        public static string PizzaOrderQuery { get; } =
            @"select pizzaOrder.*, 
            pizza.name, pizza.price from PizzaOrder pizzaOrder 
            inner join Pizza pizza on pizza.id = pizzaOrder.pizzaId
            where invoiceNo = @InvoiceNo";

        public static string PizzaOrderDetailQuery { get; } =
           @"select pizzaOrderDetail.*, 
            pizzaExtra.name, pizzaExtra.price from [dbo].[PizzaOrderDetail] pizzaOrderDetail
            inner join PizzaExtras pizzaExtra on pizzaOrderDetail.pizzaExtraId = pizzaExtra.id
            where orderInvoiceNo = @InvoiceNo;";
    }
}
