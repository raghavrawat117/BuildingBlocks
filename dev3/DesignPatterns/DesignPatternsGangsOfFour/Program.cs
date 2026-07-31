using Abstractions.IEmployeeRepo;

namespace DesignPatternsGangsOfFour;

public class DesignPatternsGangsOfFour
{
    public static void Main(string[] args) 
    {
        // Ctrl K + C comment , Ctrl K + U uncomment
        
        // // //------------ 1. Creational------------

        // // 1. Singleton------------
        // IProgramToRun program = new SingletonPatternDemo.Program();

        // // 2. Factory------------
        // IProgramToRun program = new FactoryMethodDemo.Program();

        // // 3. Abstract Factory------------
        // IProgramToRun program = new AbstractFactoryDemo.Program();

        // // 4. Builder Pattern------------
        // IProgramToRun program = new BuilderPatternDemo.Program();

        // // 5. Prototype------------
        // IProgramToRun program = new PrototypePatternDemo.Program();

        // // // ------------2. Structural------------

        // // 1. Adapter------------
        // IProgramToRun program = new AdapterPatternDemo.Program();

        // // 2. Bridge------------
        // IProgramToRun program = new BridgePatternDemo.Program();

        // // 3. Composite------------
        // IProgramToRun program = new CompositePatternDemo.Program();

        // // 4. Decorator------------
        // IProgramToRun program = new DecoratorPatternDemo.Program();

        // // 5. Facade------------
        // IProgramToRun program = new FacadePatternDemo.Program();

        // // 6 Flyweight------------
        // IProgramToRun program = new FlyweightPatternDemo.Program();

        // // 7 Proxy------------
        // IProgramToRun program = new ProxyPatternDemo.Program();

        // // // ------------3. Behavioral------------

        // // 1. Chain Of Responsibility------------
        // IProgramToRun program = new ChainOfResponsibilityDemo.Program();

        // // 2. Observer-----------
        // IProgramToRun program = new ObserverPatternDemo.Program();

        // // 3. Strategy-----------
        // IProgramToRun program = new StrategyPatternDemo.Program();

        // // 4. Command-----------
        // IProgramToRun program = new CommandPatternDemo.Program();

        // // 5. State-----------
        // IProgramToRun program = new StatePatternDemo.Program();

        // // 6. Template-----------
        // IProgramToRun program = new TemplateMethodDemo.Program();

        // // 7. Mediator-----------
        // IProgramToRun program = new MediatorPatternDemo.Program();

        // // 8. Iterator-----------
        // IProgramToRun program = new IteratorPatternDemo.Program();

        // // 9. Memento-----------
        // IProgramToRun program = new MementoPatternDemo.Program();

        // // 10. Visitor-----------
        // IProgramToRun program = new VisitorPatternDemo.Program();

        // // 11. Interpreter-----------
        IProgramToRun program = new InterpreterPatternDemo.Program();

        program.Run();
    }
}
    