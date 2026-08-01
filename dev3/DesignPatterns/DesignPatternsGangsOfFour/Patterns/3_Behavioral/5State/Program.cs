using Abstractions.IEmployeeRepo;

namespace StatePatternDemo;

public interface IDocumentState
{
    void Publish(Document document);
}

public class Document
{
    private IDocumentState _state;

    public Document()
    {
        _state = new DraftState();
    }

    public void SetState(IDocumentState state)
    {
        _state = state;
    }

    public void Publish()
    {
        _state.Publish(this);
    }
}

public class DraftState : IDocumentState
{
    public void Publish(Document document)
    {
        Console.WriteLine(
            "Document published.");

        document.SetState(
            new PublishedState());
    }
}
public class PublishedState : IDocumentState
{
    public void Publish(Document document)
    {
        Console.WriteLine(
            "Document already published.");
    }
}

public class Program : IProgramToRun
{
    public void Run()
    {
        Document document = new Document();

        document.Publish();

        document.Publish();

        Console.ReadKey();
    }
}
