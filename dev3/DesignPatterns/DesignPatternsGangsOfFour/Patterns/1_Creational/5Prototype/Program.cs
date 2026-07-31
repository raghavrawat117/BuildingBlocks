using Abstractions.IEmployeeRepo;

namespace PrototypePatternDemo;

public interface IPrototype<T>
{
    T Clone();
}

public class Employee : IPrototype<Employee>
{
    public string Name { get; set; }
    public string Department { get; set; }

    public Employee Clone()
    {
        return (Employee)this.MemberwiseClone();
    }

    public void Display()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Department: {Department}");
        Console.WriteLine();
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Employee original = new Employee
        {
            Name = "Raghav",
            Department = "Engineering"
        };

        Employee clone = original.Clone();

        clone.Name = "Amit";

        Console.WriteLine("Original Employee");
        original.Display();

        Console.WriteLine("Cloned Employee");
        clone.Display();

        Console.ReadKey();
    }
}

