using System;
using Abstractions.IEmployeeRepo;

namespace FacadePatternDemo;

public class InventoryService
{
    public bool IsInStock(string product)
    {
        Console.WriteLine(
            $"Checking inventory for {product}");

        return true;
    }
}

public class PaymentService
{
    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine(
            $"Payment of ₹{amount} processed");
    }
}

public class ShippingService
{
    public void ScheduleDelivery(string product)
    {
        Console.WriteLine(
            $"Delivery scheduled for {product}");
    }
}

public class OrderFacade
{
    private readonly InventoryService _inventoryService;
    private readonly PaymentService _paymentService;
    private readonly ShippingService _shippingService;

    public OrderFacade()
    {
        _inventoryService = new InventoryService();
        _paymentService = new PaymentService();
        _shippingService = new ShippingService();
    }

    public void PlaceOrder(
        string product,
        decimal amount)
    {
        if (_inventoryService.IsInStock(product))
        {
            _paymentService.ProcessPayment(amount);
            _shippingService.ScheduleDelivery(product);

            Console.WriteLine(
                "Order completed successfully");
        }
    }
}

class Program : IProgramToRun
{
    public void Run()
    {
        OrderFacade orderFacade = new OrderFacade();

        orderFacade.PlaceOrder(
            "Laptop",
            75000);

        Console.ReadKey();
    }
}