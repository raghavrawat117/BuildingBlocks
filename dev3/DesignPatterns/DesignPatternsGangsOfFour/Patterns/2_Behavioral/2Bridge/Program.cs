using Abstractions.IEmployeeRepo;

namespace BridgePatternDemo;

public interface IMessageSender
{
    void SendMessage(string message);
}

public class EmailSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending Email: {message}");
    }
}

public class SmsSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}
// adding 1 from myself
public class WhatsAppSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending WhatsApp: {message}");
    }
}

public abstract class Notification
{
    protected IMessageSender MessageSender;

    protected Notification(IMessageSender messageSender)
    {
        MessageSender = messageSender;
    }

    public abstract void Send(string message);
}

public class AlertNotification : Notification
{
    public AlertNotification(
        IMessageSender messageSender)
        : base(messageSender)
    {
    }

    public override void Send(string message)
    {
        MessageSender.SendMessage(
            $"[ALERT] {message}");
    }
}

public class ReminderNotification : Notification
{
    public ReminderNotification(
        IMessageSender messageSender)
        : base(messageSender)
    {
    }

    public override void Send(string message)
    {
        MessageSender.SendMessage(
            $"[REMINDER] {message}");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        IMessageSender emailSender =
            new EmailSender();

        Notification alert =
            new AlertNotification(emailSender);

        alert.Send("Server is down!");

        IMessageSender smsSender =
            new SmsSender();

        Notification reminder =
            new ReminderNotification(smsSender);

        reminder.Send("Submit timesheet.");

        // Adding from myself
        IMessageSender whatsAppSender =
            new WhatsAppSender();

        Notification statusCheck  =
            new AlertNotification(whatsAppSender);

        statusCheck.Send("Where are you??");

        Console.ReadKey();
    }
}