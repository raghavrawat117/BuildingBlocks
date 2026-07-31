using Abstractions.IEmployeeRepo;

namespace ChainOfResponsibilityDemo;

public abstract class ApprovalHandler
{
    protected ApprovalHandler NextHandler;

    public void SetNext(ApprovalHandler nextHandler)
    {
        NextHandler = nextHandler;
    }

    public abstract void Approve(decimal amount);
}

public class TeamLead : ApprovalHandler
{
    public override void Approve(decimal amount)
    {
        if (amount <= 10000)
        {
            Console.WriteLine(
                $"Team Lead approved ₹{amount}");
        }
        else
        {
            NextHandler?.Approve(amount);
        }
    }
}

public class Manager : ApprovalHandler
{
    public override void Approve(decimal amount)
    {
        if (amount <= 50000)
        {
            Console.WriteLine(
                $"Manager approved ₹{amount}");
        }
        else
        {
            NextHandler?.Approve(amount);
        }
    }
}

public class Director : ApprovalHandler
{
    public override void Approve(decimal amount)
    {
        if (amount <= 200000)
        {
            Console.WriteLine(
                $"Director approved ₹{amount}");
        }
        else
        {
            Console.WriteLine(
                "Amount exceeds approval limits.");
        }
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        ApprovalHandler teamLead = new TeamLead();
        ApprovalHandler manager = new Manager();
        ApprovalHandler director = new Director();

        teamLead.SetNext(manager);
        manager.SetNext(director);

        teamLead.Approve(5000);
        teamLead.Approve(25000);
        teamLead.Approve(150000);

        Console.ReadKey();
    }
}
