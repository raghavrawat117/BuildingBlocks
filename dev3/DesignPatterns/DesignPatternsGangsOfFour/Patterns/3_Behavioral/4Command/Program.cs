using Abstractions.IEmployeeRepo;

namespace CommandPatternDemo;

public interface ICommand
{
    void Execute();
}

public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("Light turned ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("Light turned OFF");
    }
}

public class TurnOnCommand : ICommand
{
    private readonly Light _light;

    public TurnOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}

public class TurnOffCommand : ICommand
{
    private readonly Light _light;

    public TurnOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}

public class RemoteControl
{
    private ICommand _command;

    public void SetCommand(ICommand command)
    {
        _command = command;
    }

    public void PressButton()
    {
        _command.Execute();
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Light light = new Light();

        ICommand turnOn =
            new TurnOnCommand(light);

        ICommand turnOff =
            new TurnOffCommand(light);

        RemoteControl remote =
            new RemoteControl();

        remote.SetCommand(turnOn);
        remote.PressButton();

        remote.SetCommand(turnOff);
        remote.PressButton();

        Console.ReadKey();
    }
}