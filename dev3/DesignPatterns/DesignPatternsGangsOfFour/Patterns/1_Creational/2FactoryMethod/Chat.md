Excellent choice. The **Factory Method Pattern** is often the next pattern taught after Singleton because it introduces the concept of **encapsulating object creation**, which is a fundamental design principle.

# Factory Method Pattern

## Definition

The Factory Method Pattern defines an interface for creating objects, but lets subclasses or factory classes decide which object to instantiate.

Instead of creating objects directly using `new`, client code asks a factory to create them.

***

# Problem Statement

Suppose you have a notification system that can send:

* Email notifications
* SMS notifications
* Push notifications

Without Factory Method:

```csharp
INotification notification = new EmailNotification();
```

If the notification type changes, you must modify client code.

Using Factory Method, the client only interacts with a factory and remains unaware of the concrete implementation.

***

# Solution Structure

```text
           INotification
                ▲
                │
    ┌───────────┼───────────┐
    │           │           │
 Email      SMS         Push

                ▲
                │
      NotificationFactory
                ▲
                │
      Creates required object
```

***

# Console Application Example

## Step 1: Product Interface

### INotification.cs

```csharp
namespace FactoryMethodDemo
{
    public interface INotification
    {
        void Send(string message);
    }
}
```

***

## Step 2: Concrete Products

### EmailNotification.cs

```csharp
using System;

namespace FactoryMethodDemo
{
    public class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email Sent: {message}");
        }
    }
}
```

### SmsNotification.cs

```csharp
using System;

namespace FactoryMethodDemo
{
    public class SmsNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"SMS Sent: {message}");
        }
    }
}
```

### PushNotification.cs

```csharp
using System;

namespace FactoryMethodDemo
{
    public class PushNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Push Notification Sent: {message}");
        }
    }
}
```

***

## Step 3: Factory

### NotificationFactory.cs

```csharp
using System;

namespace FactoryMethodDemo
{
    public static class NotificationFactory
    {
        public static INotification CreateNotification(string type)
        {
            switch (type.ToLower())
            {
                case "email":
                    return new EmailNotification();

                case "sms":
                    return new SmsNotification();

                case "push":
                    return new PushNotification();

                default:
                    throw new ArgumentException("Invalid notification type");
            }
        }
    }
}
```

***

## Step 4: Client Code

### Program.cs

```csharp
using System;

namespace FactoryMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose notification type:");
            Console.WriteLine("1 - Email");
            Console.WriteLine("2 - SMS");
            Console.WriteLine("3 - Push");

            string choice = Console.ReadLine();

            string type = choice switch
            {
                "1" => "email",
                "2" => "sms",
                "3" => "push",
                _ => throw new Exception("Invalid choice")
            };

            INotification notification =
                NotificationFactory.CreateNotification(type);

            notification.Send("Welcome to Design Patterns!");

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

### Scenario 1

```text
Choose notification type:
1 - Email
2 - SMS
3 - Push

1

Email Sent: Welcome to Design Patterns!
```

### Scenario 2

```text
Choose notification type:
1 - Email
2 - SMS
3 - Push

2

SMS Sent: Welcome to Design Patterns!
```

***

# Why This Is Better Than Using `new`

Without Factory:

```csharp
if(type == "email")
{
    notification = new EmailNotification();
}
else if(type == "sms")
{
    notification = new SmsNotification();
}
```

The client becomes responsible for object creation.

With Factory:

```csharp
var notification =
    NotificationFactory.CreateNotification(type);
```

The creation logic is centralized.

***

# Real-World Example in .NET

A familiar example is:

```csharp
DbProviderFactory factory = SqlClientFactory.Instance;
DbConnection connection = factory.CreateConnection();
```

Your code asks the factory for a database connection without knowing the specific connection implementation.

***

# Advantages

✅ Encapsulates object creation

✅ Reduces coupling between client and concrete classes

✅ Makes code easier to extend

✅ Supports Open/Closed Principle

***

# Disadvantages

❌ Introduces additional classes

❌ Can increase complexity for very small applications

***

# When to Use

Use Factory Method when:

* Object creation logic is complex.
* The exact type is determined at runtime.
* You want to avoid scattered `new` statements.
* New implementations may be added in the future.

Examples:

* Payment providers (UPI, Credit Card, PayPal)
* Notification systems
* File exporters (PDF, Excel, CSV)
* Database providers

***

# Interview Question

**Q: What is the difference between Factory Method and Simple Factory?**

**Answer:**

A Simple Factory usually contains a single method that creates different objects based on input.

```csharp
NotificationFactory.CreateNotification("email");
```

A true Factory Method pattern relies on inheritance where subclasses decide which object to create.

```text
Creator
   ▲
   │
EmailFactory
SmsFactory
PushFactory
```

The example above is often called a **Simple Factory** for learning purposes, while the classic GoF Factory Method uses inheritance.

***

## GoF Factory Method Version (Recommended for Seniors)

### Product

```csharp
public interface INotification
{
    void Send(string message);
}
```

### Concrete Products

```csharp
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}
```

### Creator

```csharp
public abstract class NotificationFactory
{
    public abstract INotification CreateNotification();
}
```

### Concrete Creators

```csharp
public class EmailFactory : NotificationFactory
{
    public override INotification CreateNotification()
    {
        return new EmailNotification();
    }
}

public class SmsFactory : NotificationFactory
{
    public override INotification CreateNotification()
    {
        return new SmsNotification();
    }
}
```

### Client

```csharp
NotificationFactory factory = new EmailFactory();

INotification notification =
    factory.CreateNotification();

notification.Send("Hello World");
```

This is the textbook **Factory Method Pattern** and is typically what interviewers expect when discussing the GoF pattern.

***

✅ **Design Pattern #2 Complete: Factory Method Pattern**

**Key takeaway for juniors:**

> "Don't create objects directly with `new` when the type can vary. Let a factory decide what object should be created."
