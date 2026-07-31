using Abstractions.IEmployeeRepo;

namespace VisitorPatternDemo;

public interface IVisitor
{
    void Visit(Developer developer);
    void Visit(Manager manager);
}

public interface IEmployee
{
    void Accept(IVisitor visitor);
}

public class Developer : IEmployee
{
    public string Name { get; }

    public Developer(string name)
    {
        Name = name;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Manager : IEmployee
{
    public string Name { get; }

    public Manager(string name)
    {
        Name = name;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class SalaryReportVisitor : IVisitor
{
    public void Visit(Developer developer)
    {
        Console.WriteLine(
            $"Developer {developer.Name} salary report generated.");
    }

    public void Visit(Manager manager)
    {
        Console.WriteLine(
            $"Manager {manager.Name} salary report generated.");
    }
}

public class TaxReportVisitor : IVisitor
{
    public void Visit(Developer developer)
    {
        Console.WriteLine(
            $"Developer {developer.Name} tax report generated.");
    }

    public void Visit(Manager manager)
    {
        Console.WriteLine(
            $"Manager {manager.Name} tax report generated.");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        List<IEmployee> employees =
            new()
            {
                    new Developer("Raghav"),
                    new Manager("Amit")
            };

        IVisitor salaryVisitor =
            new SalaryReportVisitor();

        foreach (var employee in employees)
        {
            employee.Accept(salaryVisitor);
        }

        Console.WriteLine();

        IVisitor taxVisitor =
            new TaxReportVisitor();

        foreach (var employee in employees)
        {
            employee.Accept(taxVisitor);
        }

        Console.ReadKey();
    }
}