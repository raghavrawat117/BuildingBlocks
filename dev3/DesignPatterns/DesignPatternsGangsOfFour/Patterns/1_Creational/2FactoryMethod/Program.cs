using Abstractions.IEmployeeRepo;

namespace FactoryMethodDemo;

public interface INotification
{
    void Send(string message);
}

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email Sent: {message}");
    }
}

public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS Sent: {message}");
    }
}

public class PushNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Push Notification Sent: {message}");
    }
}

public static class NotificationFactory
{
    public static INotification CreateNotification(string type)
    {
        switch (type.ToLower())
        {
            case "email":
                return new EmailNotification();

            case "sms":
                return new SmsNotification();

            case "push":
                return new PushNotification();

            default:
                throw new ArgumentException("Invalid notification type");
        }
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Console.WriteLine("Choose notification type:");
        Console.WriteLine("1 - Email");
        Console.WriteLine("2 - SMS");
        Console.WriteLine("3 - Push");

        string choice = Console.ReadLine();

        string type = choice switch
        {
            "1" => "email",
            "2" => "sms",
            "3" => "push",
            _ => throw new Exception("Invalid choice")
        };

        INotification notification =
            NotificationFactory.CreateNotification(type);

        notification.Send("Welcome to Design Patterns!");

        Console.ReadKey();
    }
}




