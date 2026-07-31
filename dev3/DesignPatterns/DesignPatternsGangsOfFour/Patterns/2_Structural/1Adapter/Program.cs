using Abstractions.IEmployeeRepo;

namespace AdapterPatternDemo;

public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}

public class ThirdPartyPaymentGateway
{
    public void MakePayment(decimal amount)
    {
        Console.WriteLine(
            $"Payment of ₹{amount} processed via Third-Party Gateway");
    }
}

public class PaymentAdapter : IPaymentProcessor
{
    private readonly ThirdPartyPaymentGateway _gateway;

    public PaymentAdapter(
        ThirdPartyPaymentGateway gateway)
    {
        _gateway = gateway;
    }

    public void ProcessPayment(decimal amount)
    {
        _gateway.MakePayment(amount);
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        ThirdPartyPaymentGateway gateway = new ThirdPartyPaymentGateway();

        IPaymentProcessor paymentProcessor =
            new PaymentAdapter(gateway);

        paymentProcessor.ProcessPayment(2500);

        Console.ReadKey();
    }
}