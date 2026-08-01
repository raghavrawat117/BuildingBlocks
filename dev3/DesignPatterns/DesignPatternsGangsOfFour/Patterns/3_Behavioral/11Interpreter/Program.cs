using Abstractions.IEmployeeRepo;

namespace InterpreterPatternDemo;

public interface IExpression
{
    int Interpret();
}

public class NumberExpression : IExpression
{
    private readonly int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public int Interpret()
    {
        return _number;
    }
}

public class AddExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;

    public AddExpression(
        IExpression left,
        IExpression right)
    {
        _left = left;
        _right = right;
    }

    public int Interpret()
    {
        return _left.Interpret()
            + _right.Interpret();
    }
}

public class SubtractExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;

    public SubtractExpression(
        IExpression left,
        IExpression right)
    {
        _left = left;
        _right = right;
    }

    public int Interpret()
    {
        return _left.Interpret()
            - _right.Interpret();
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        IExpression five =
            new NumberExpression(5);

        IExpression three =
            new NumberExpression(3);

        IExpression two =
            new NumberExpression(2);

        IExpression expression =
            new SubtractExpression(
                new AddExpression(
                    five,
                    three),
                two);

        Console.WriteLine(
            expression.Interpret());

        Console.ReadKey();
    }
}