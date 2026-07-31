using Abstractions.IEmployeeRepo;

namespace MediatorPatternDemo;

public interface IChatMediator
{
    void SendMessage(
        string message,
        ChatUser user);

    void RegisterUser(ChatUser user);
}

public class ChatRoom : IChatMediator
{
    private readonly List<ChatUser> _users =
        new();

    public void RegisterUser(ChatUser user)
    {
        _users.Add(user);
    }

    public void SendMessage(
        string message,
        ChatUser sender)
    {
        foreach (var user in _users)
        {
            if (user != sender)
            {
                user.Receive(message);
            }
        }
    }
}

public class ChatUser
{
    private readonly IChatMediator _mediator;

    public string Name { get; }

    public ChatUser(
        string name,
        IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void Send(string message)
    {
        Console.WriteLine(
            $"{Name} sends: {message}");

        _mediator.SendMessage(
            message,
            this);
    }

    public void Receive(string message)
    {
        Console.WriteLine(
            $"{Name} received: {message}");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        ChatRoom chatRoom = new ChatRoom();

        ChatUser raghav =
            new ChatUser("Raghav", chatRoom);

        ChatUser amit =
            new ChatUser("Amit", chatRoom);

        ChatUser priya =
            new ChatUser("Priya", chatRoom);

        chatRoom.RegisterUser(raghav);
        chatRoom.RegisterUser(amit);
        chatRoom.RegisterUser(priya);

        raghav.Send("Hello Everyone!");

        Console.ReadKey();
    }
}