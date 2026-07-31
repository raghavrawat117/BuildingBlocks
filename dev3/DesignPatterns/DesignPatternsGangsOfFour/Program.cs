using Abstractions.IEmployeeRepo;

namespace DesignPatternsGangsOfFour;

public class DesignPatternsGangsOfFour
{
    public static void Main(string[] args) 
    {
        // Ctrl K + C comment , Ctrl K + U uncomment
        
        // // //------------ 1. Creational------------
        // // 1. Singleton
        // IProgramToRun program = new SingletonPatternDemo.Program();

        // // 2. Factory
        // IProgramToRun program = new FactoryMethodDemo.Program();

        // // 3. Abstract Factory
        // IProgramToRun program = new AbstractFactoryDemo.Program();

        // // 4. Builder Pattern
        // IProgramToRun program = new BuilderPatternDemo.Program();

        // // 5. Prototype
        // IProgramToRun program = new PrototypePatternDemo.Program();

        // // // ------------2. Structural------------
        // // 1. Adapter 
        // IProgramToRun program = new AdapterPatternDemo.Program();

        // // 2. Bridge
        // IProgramToRun program = new BridgePatternDemo.Program();

        // // 3. Composite
        // IProgramToRun program = new CompositePatternDemo.Program();

        // // 4. Decorator
        // IProgramToRun program = new DecoratorPatternDemo.Program();

        // // 5. Facade
        // IProgramToRun program = new FacadePatternDemo.Program();

        // // 6 Flyweight
        // IProgramToRun program = new FlyweightPatternDemo.Program();

        // // 7 Proxy
        IProgramToRun program = new ProxyPatternDemo.Program();

        program.Run();
    }
}
    