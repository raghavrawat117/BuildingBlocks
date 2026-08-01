using Abstractions.IEmployeeRepo;

namespace ProxyPatternDemo;

public interface IReport
{
    void Display();
}

public class RealReport : IReport
{
    public RealReport()
    {
        Console.WriteLine(
            "Loading report from database...");

        Thread.Sleep(2000);
    }

    public void Display()
    {
        Console.WriteLine(
            "Report displayed.");
    }
}

public class ReportProxy : IReport
{
    private RealReport _realReport;

    public void Display()
    {
        if (_realReport == null)
        {
            _realReport = new RealReport();
        }

        _realReport.Display();
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        IReport report =
            new ReportProxy();

        Console.WriteLine(
            "Proxy created.");

        Console.WriteLine(
            "Press any key to view report.");

        Console.ReadKey();

        report.Display();

        Console.ReadKey();
    }
}


