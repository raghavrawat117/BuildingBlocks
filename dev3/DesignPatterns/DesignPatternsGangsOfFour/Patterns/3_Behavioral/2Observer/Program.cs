using Abstractions.IEmployeeRepo;

namespace ObserverPatternDemo;

public interface IObserver
{
    void Update(decimal stockPrice);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

public class Stock : ISubject
{
    private readonly List<IObserver> _observers =
        new();

    private decimal _price;

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            Notify();
        }
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_price);
        }
    }
}

public class MobileApp : IObserver
{
    public void Update(decimal stockPrice)
    {
        Console.WriteLine(
            $"Mobile App Updated : ₹{stockPrice}");
    }
}

public class EmailAlert : IObserver
{
    public void Update(decimal stockPrice)
    {
        Console.WriteLine(
            $"Email Alert Sent : ₹{stockPrice}");
    }
}

public class Dashboard : IObserver
{
    public void Update(decimal stockPrice)
    {
        Console.WriteLine(
            $"Dashboard Refreshed : ₹{stockPrice}");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Stock stock = new Stock();

        stock.Attach(new MobileApp());
        stock.Attach(new EmailAlert());
        stock.Attach(new Dashboard());

        stock.Price = 100;

        Console.WriteLine();

        stock.Price = 125;

        Console.ReadKey();
    }
}