using Abstractions.IEmployeeRepo;

namespace DesignPatternsGangsOfFour;

public class DesignPatternsGangsOfFour
{
    public static void Main(string[] args) 
    {
        // Ctrl K + C comment , Ctrl K + U uncomment
        // // Creational
        // // 1. Singleton
        // IProgramToRun program = new SingletonPatternDemo.Program();

        // // 2. Factory
        IProgramToRun program = new FactoryMethodDemo.Program();

        // // 3. Abstract Factory
        // IProgramToRun program = new AbstractFactoryDemo.Program();

        // // 4. Builder Pattern
        // IProgramToRun program = new BuilderPatternDemo.Program();

        // // 5. Prototype
        //IProgramToRun program = new PrototypePatternDemo.Program();

        program.Run();
    }
}
    