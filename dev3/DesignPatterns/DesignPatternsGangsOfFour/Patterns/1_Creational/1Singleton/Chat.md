Great idea. A design patterns guide with simple console-app examples is very useful for juniors.

Let's start with the **Singleton Pattern**, since it's one of the most commonly taught design patterns.

# Singleton Pattern

## Definition

The Singleton Pattern ensures that:

1. Only **one instance** of a class exists.
2. A **global access point** is provided to that instance.

## Real-world Example

Think of an application logger.

You don't want multiple logger instances writing to different places. Instead, the entire application should use a single logger object.

***

# C# Console Application Example

## Logger.cs

```csharp
using System;

namespace SingletonPatternDemo
{
    public sealed class Logger
    {
        private static readonly Logger _instance = new Logger();

        // Private constructor prevents external instantiation
        private Logger()
        {
            Console.WriteLine("Logger instance created.");
        }

        // Global access point
        public static Logger Instance
        {
            get { return _instance; }
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}
```

***

## Program.cs

```csharp
using System;

namespace SingletonPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger1 = Logger.Instance;
            Logger logger2 = Logger.Instance;

            logger1.Log("Application started.");
            logger2.Log("Processing data...");

            Console.WriteLine();
            Console.WriteLine($"Reference Equals: {ReferenceEquals(logger1, logger2)}");

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Logger instance created.
[LOG] Application started.
[LOG] Processing data...

Reference Equals: True
```

Notice that:

* The constructor executes only once.
* Both variables reference the same object.
* `ReferenceEquals()` returns `True`.

***

# UML (Simple)

```text
        +----------------+
        |    Logger      |
        +----------------+
        | - _instance    |
        +----------------+
        | + Instance     |
        | + Log()        |
        +----------------+
```

***

# Advantages

✅ Controlled access to a single instance

✅ Reduced memory usage

✅ Useful for shared resources such as:

* Logging
* Configuration management
* Cache providers
* Database connection managers

***

# Disadvantages

❌ Introduces global state

❌ Can make unit testing more difficult

❌ Overuse can create tight coupling

***

# When to Use

Use Singleton when:

* Exactly one instance is needed.
* The instance should be shared across the application.
* Centralized access is required.

Examples:

* Logger
* Application configuration
* Cache manager
* Settings manager

***

# Interview Question

**Q: Why is the constructor private in a Singleton?**

**Answer:**  
A private constructor prevents other classes from creating instances using the `new` keyword, ensuring that the class controls instance creation and maintains a single object.

***

**Design Pattern #1 Complete: Singleton**

Next in a learning sequence, I would typically cover **Factory Method Pattern**, as it introduces object creation principles and is easier to understand before moving to Abstract Factory, Builder, and Prototype.
