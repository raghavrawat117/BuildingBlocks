using Abstractions.IEmployeeRepo;

namespace CompositePatternDemo;

public interface IFileSystemItem
{
    void Display(int depth);
}

public class File : IFileSystemItem
{
    private readonly string _name;

    public File(string name)
    {
        _name = name;
    }

    public void Display(int depth)
    {
        Console.WriteLine(
            new string('-', depth) + _name);
    }
}

public class Folder : IFileSystemItem
{
    private readonly string _name;

    private readonly List<IFileSystemItem> _items =
        new();

    public Folder(string name)
    {
        _name = name;
    }

    public void Add(IFileSystemItem item)
    {
        _items.Add(item);
    }

    public void Remove(IFileSystemItem item)
    {
        _items.Remove(item);
    }

    public void Display(int depth)
    {
        // basically depth times -
        Console.WriteLine(
            new string('-', depth) + _name);

        foreach (var item in _items)
        {
            item.Display(depth + 2);
        }
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Folder root = new Folder("Root");

        Folder documents =
            new Folder("Documents");

        documents.Add(
            new File("Resume.docx"));

        documents.Add(
            new File("Notes.txt"));

        Folder pictures =
            new Folder("Pictures");

        pictures.Add(
            new File("Photo1.jpg"));

        pictures.Add(
            new File("Photo2.jpg"));

        root.Add(documents);
        root.Add(pictures);

        root.Display(1);

        Console.ReadKey();
    }
}