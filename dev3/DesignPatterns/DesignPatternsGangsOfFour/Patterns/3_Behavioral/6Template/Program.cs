using Abstractions.IEmployeeRepo;

namespace TemplateMethodDemo;

public abstract class Beverage
{
    // Template Method
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }

    private void BoilWater()
    {
        Console.WriteLine("Boiling water");
    }

    private void PourInCup()
    {
        Console.WriteLine("Pouring into cup");
    }

    protected abstract void Brew();

    protected abstract void AddCondiments();
}

public class Tea : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Steeping tea");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Adding lemon");
    }
}

public class Coffee : Beverage
{
    protected override void Brew()
    {
        Console.WriteLine("Brewing coffee");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Adding milk and sugar");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Beverage tea = new Tea();

        Console.WriteLine("Preparing Tea");
        tea.PrepareRecipe();

        Console.WriteLine();

        Beverage coffee = new Coffee();

        Console.WriteLine("Preparing Coffee");
        coffee.PrepareRecipe();

        Console.ReadKey();
    }
}