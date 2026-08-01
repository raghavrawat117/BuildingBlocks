using Abstractions.IEmployeeRepo;

namespace StrategyPatternDemo;

public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine(
            $"Paid ₹{amount} using Credit Card");
    }
}

public class UpiPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine(
            $"Paid ₹{amount} using UPI");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine(
            $"Paid ₹{amount} using PayPal");
    }
}

public class ShoppingCart
{
    private readonly IPaymentStrategy _paymentStrategy;

    public ShoppingCart(
        IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void Checkout(decimal amount)
    {
        _paymentStrategy.Pay(amount);
    }
}


public class Program : IProgramToRun
{
    public void Run()
    {
        Console.WriteLine("Select Payment Method");
        Console.WriteLine("1 - Credit Card");
        Console.WriteLine("2 - UPI");
        Console.WriteLine("3 - PayPal");

        string choice = Console.ReadLine();

        IPaymentStrategy strategy = choice switch
        {
            "1" => new CreditCardPayment(),
            "2" => new UpiPayment(),
            "3" => new PayPalPayment(),
            _ => throw new Exception("Invalid choice")
        };

        ShoppingCart cart =
            new ShoppingCart(strategy);

        cart.Checkout(2500);

        Console.ReadKey();
    }
}
