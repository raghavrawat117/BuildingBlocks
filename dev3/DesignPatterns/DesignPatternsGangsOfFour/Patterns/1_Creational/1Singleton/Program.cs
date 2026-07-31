using Abstractions.IEmployeeRepo;

namespace SingletonPatternDemo;

public sealed class Logger
{
    private static readonly Logger _instance = new Logger();

    // Private constructor prevents external instantiation
    private Logger()
    {
        Console.WriteLine("Logger instance created.");
    }

    // Global access point
    public static Logger Instance
    {
        get { return _instance; }
    }

    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Logger logger1 = Logger.Instance;
        Logger logger2 = Logger.Instance;

        logger1.Log("Application started.");
        logger2.Log("Processing data...");

        Console.WriteLine();
        Console.WriteLine($"Reference Equals: {ReferenceEquals(logger1, logger2)}");

        Console.ReadKey();
    }
}


