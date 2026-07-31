using Abstractions.IEmployeeRepo;

namespace MementoPatternDemo;

public class DocumentMemento
{
    public string Content { get; }

    public DocumentMemento(string content)
    {
        Content = content;
    }
}

public class Document
{
    public string Content { get; set; }

    public DocumentMemento Save()
    {
        return new DocumentMemento(Content);
    }

    public void Restore(DocumentMemento memento)
    {
        Content = memento.Content;
    }
}

public class DocumentHistory
{
    private readonly Stack<DocumentMemento> _history =
        new();

    public void Save(DocumentMemento memento)
    {
        _history.Push(memento);
    }

    public DocumentMemento Undo()
    {
        return _history.Pop();
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Document document =
            new Document();

        DocumentHistory history =
            new DocumentHistory();

        document.Content = "Version 1";
        history.Save(document.Save());

        document.Content = "Version 2";
        history.Save(document.Save());

        document.Content = "Version 3";

        Console.WriteLine(
            $"Current: {document.Content}");

        document.Restore(history.Undo());

        Console.WriteLine(
            $"Undo 1 : {document.Content}");

        document.Restore(history.Undo());

        Console.WriteLine(
            $"Undo 2 : {document.Content}");

        Console.ReadKey();
    }
}