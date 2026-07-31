using Abstractions.IEmployeeRepo;

namespace DecoratorPatternDemo;

public interface ICoffee
{
    string GetDescription();
    decimal GetCost();
}

public class SimpleCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Coffee";
    }

    public decimal GetCost()
    {
        return 100;
    }
}

public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee Coffee;

    protected CoffeeDecorator(ICoffee coffee)
    {
        Coffee = coffee;
    }

    public virtual string GetDescription()
    {
        return Coffee.GetDescription();
    }

    public virtual decimal GetCost()
    {
        return Coffee.GetCost();
    }
}

public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee)
        : base(coffee)
    {
    }

    public override string GetDescription()
    {
        return Coffee.GetDescription() + ", Milk";
    }

    public override decimal GetCost()
    {
        return Coffee.GetCost() + 20;
    }
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee)
        : base(coffee)
    {
    }

    public override string GetDescription()
    {
        return Coffee.GetDescription() + ", Sugar";
    }

    public override decimal GetCost()
    {
        return Coffee.GetCost() + 10;
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        ICoffee coffee = new SimpleCoffee();

        coffee = new MilkDecorator(coffee);

        coffee = new SugarDecorator(coffee);

        Console.WriteLine(
            $"Description: {coffee.GetDescription()}");

        Console.WriteLine(
            $"Cost: ₹{coffee.GetCost()}");

        Console.ReadKey();
    }
}
