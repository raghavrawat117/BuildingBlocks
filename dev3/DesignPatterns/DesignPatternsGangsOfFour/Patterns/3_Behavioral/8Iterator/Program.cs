using Abstractions.IEmployeeRepo;

namespace IteratorPatternDemo;

public interface IIterator<T>
{
    bool HasNext();
    T Next();
}

public class EmployeeCollection
{
    private readonly List<string> _employees =
        new();

    public void Add(string employee)
    {
        _employees.Add(employee);
    }

    public IIterator<string> CreateIterator()
    {
        return new EmployeeIterator(_employees);
    }
}

public class EmployeeIterator : IIterator<string>
{
    private readonly List<string> _employees;
    private int _position = 0;

    public EmployeeIterator(
        List<string> employees)
    {
        _employees = employees;
    }

    public bool HasNext()
    {
        return _position < _employees.Count;
    }

    public string Next()
    {
        return _employees[_position++];
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        EmployeeCollection employees =
            new EmployeeCollection();

        employees.Add("Raghav");
        employees.Add("Amit");
        employees.Add("Priya");

        IIterator<string> iterator =
            employees.CreateIterator();

        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }

        Console.ReadKey();
    }
}
