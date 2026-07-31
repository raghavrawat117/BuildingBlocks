using Abstractions.IEmployeeRepo;

namespace AbstractFactoryDemo;

public interface IButton
{
    void Render();
}

public interface ICheckbox
{
    void Render();
}

public class WindowsButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering Windows Button");
    }
}

public class MacButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Rendering Mac Button");
    }
}
public class WindowsCheckbox : ICheckbox
{
    public void Render()
    {
        Console.WriteLine("Rendering Windows Checkbox");
    }
}
public class MacCheckbox : ICheckbox
{
    public void Render()
    {
        Console.WriteLine("Rendering Mac Checkbox");
    }
}

public interface IUIFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

public class WindowsFactory : IUIFactory
{
    public IButton CreateButton()
    {
        return new WindowsButton();
    }

    public ICheckbox CreateCheckbox()
    {
        return new WindowsCheckbox();
    }
}

public class MacFactory : IUIFactory
{
    public IButton CreateButton()
    {
        return new MacButton();
    }

    public ICheckbox CreateCheckbox()
    {
        return new MacCheckbox();
    }
}

public class Application
{
    private readonly IButton _button;
    private readonly ICheckbox _checkbox;

    public Application(IUIFactory factory)
    {
        _button = factory.CreateButton();
        _checkbox = factory.CreateCheckbox();
    }

    public void RenderUI()
    {
        _button.Render();
        _checkbox.Render();
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Console.WriteLine("Select UI Style");
        Console.WriteLine("1 - Windows");
        Console.WriteLine("2 - Mac");

        string choice = Console.ReadLine();

        IUIFactory factory = choice switch
        {
            "1" => new WindowsFactory(),
            "2" => new MacFactory(),
            _ => throw new Exception("Invalid choice")
        };

        Application app = new Application(factory);

        app.RenderUI();

        Console.ReadKey();
    }
}


