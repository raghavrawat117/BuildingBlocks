using Abstractions.IEmployeeRepo;

namespace FlyweightPatternDemo;

public class TreeType
{
    public string Name { get; }
    public string Color { get; }

    public TreeType(
        string name,
        string color)
    {
        Name = name;
        Color = color;
    }

    public void Display(int x, int y)
    {
        Console.WriteLine(
            $"Tree: {Name}, Color: {Color}, Location: ({x},{y})");
    }
}

public class TreeFactory
{
    private readonly Dictionary<string, TreeType>
        _treeTypes = new();

    public TreeType GetTreeType(
        string name,
        string color)
    {
        string key = $"{name}_{color}";

        if (!_treeTypes.ContainsKey(key))
        {
            _treeTypes[key] =
                new TreeType(name, color);
        }

        return _treeTypes[key];
    }
}


public class Tree
{
    private readonly int _x;
    private readonly int _y;

    private readonly TreeType _treeType;

    public Tree(
        int x,
        int y,
        TreeType treeType)
    {
        _x = x;
        _y = y;
        _treeType = treeType;
    }

    public void Display()
    {
        _treeType.Display(_x, _y);
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        TreeFactory factory =
            new TreeFactory();

        List<Tree> forest = new();

        for (int i = 0; i < 5; i++)
        {
            TreeType oakType =
                factory.GetTreeType(
                    "Oak",
                    "Green");

            forest.Add(
                new Tree(i * 10, i * 5, oakType));
        }

        foreach (var tree in forest)
        {
            tree.Display();
        }

        Console.ReadKey();
    }
}
