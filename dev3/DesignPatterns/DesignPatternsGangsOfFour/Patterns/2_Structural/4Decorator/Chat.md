Excellent. The **Decorator Pattern** is arguably one of the most important patterns for .NET developers because it's used extensively in:

* ASP.NET Core Middleware
* Stream classes (`FileStream`, `BufferedStream`, `GZipStream`)
* Logging pipelines
* Caching wrappers
* Dependency Injection decorators

***

# Decorator Pattern

## Definition

The Decorator Pattern allows you to add new behavior to an object dynamically without modifying its existing code.

Instead of changing the original class, you wrap it inside another object that adds extra functionality.

***

# Real-World Analogy

Think of ordering coffee.

Base coffee:

```text
Coffee
```

Add milk:

```text
Coffee + Milk
```

Add sugar:

```text
Coffee + Milk + Sugar
```

Add whipped cream:

```text
Coffee + Milk + Sugar + Whipped Cream
```

We're not modifying the original coffee class.

We're decorating it with additional features.

***

# Problem Statement

Suppose we have:

```csharp
Coffee
```

and need:

```text
Coffee
CoffeeWithMilk
CoffeeWithSugar
CoffeeWithMilkAndSugar
CoffeeWithMilkAndSugarAndCream
```

Using inheritance:

```text
Coffee
├── CoffeeWithMilk
├── CoffeeWithSugar
├── CoffeeWithMilkAndSugar
├── CoffeeWithMilkAndSugarAndCream
...
```

The number of classes grows rapidly.

This is called **class explosion**.

***

# Solution

Use composition instead.

```text
Coffee
   ↓
MilkDecorator
   ↓
SugarDecorator
```

Each decorator adds behavior.

***

# Structure

```text
          ICoffee
              ▲
      ┌───────┴────────┐
      │                │
    Coffee      CoffeeDecorator
                        ▲
             ┌──────────┴─────────┐
             │                    │
      MilkDecorator      SugarDecorator
```

***

# Console Application Example

## Step 1: Component

### ICoffee.cs

```csharp
namespace DecoratorPatternDemo
{
    public interface ICoffee
    {
        string GetDescription();
        decimal GetCost();
    }
}
```

***

## Step 2: Concrete Component

### SimpleCoffee.cs

```csharp
namespace DecoratorPatternDemo
{
    public class SimpleCoffee : ICoffee
    {
        public string GetDescription()
        {
            return "Coffee";
        }

        public decimal GetCost()
        {
            return 100;
        }
    }
}
```

***

## Step 3: Base Decorator

### CoffeeDecorator.cs

```csharp
namespace DecoratorPatternDemo
{
    public abstract class CoffeeDecorator : ICoffee
    {
        protected readonly ICoffee Coffee;

        protected CoffeeDecorator(ICoffee coffee)
        {
            Coffee = coffee;
        }

        public virtual string GetDescription()
        {
            return Coffee.GetDescription();
        }

        public virtual decimal GetCost()
        {
            return Coffee.GetCost();
        }
    }
}
```

***

## Step 4: Concrete Decorators

### MilkDecorator.cs

```csharp
namespace DecoratorPatternDemo
{
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee)
            : base(coffee)
        {
        }

        public override string GetDescription()
        {
            return Coffee.GetDescription() + ", Milk";
        }

        public override decimal GetCost()
        {
            return Coffee.GetCost() + 20;
        }
    }
}
```

***

### SugarDecorator.cs

```csharp
namespace DecoratorPatternDemo
{
    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee)
            : base(coffee)
        {
        }

        public override string GetDescription()
        {
            return Coffee.GetDescription() + ", Sugar";
        }

        public override decimal GetCost()
        {
            return Coffee.GetCost() + 10;
        }
    }
}
```

***

## Step 5: Client

### Program.cs

```csharp
using System;

namespace DecoratorPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ICoffee coffee = new SimpleCoffee();

            coffee = new MilkDecorator(coffee);

            coffee = new SugarDecorator(coffee);

            Console.WriteLine(
                $"Description: {coffee.GetDescription()}");

            Console.WriteLine(
                $"Cost: ₹{coffee.GetCost()}");

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

```text
Description: Coffee, Milk, Sugar

Cost: ₹130
```

Let's see what happened:

```csharp
Coffee
   ↓
MilkDecorator
   ↓
SugarDecorator
```

Cost calculation:

```text
Coffee = 100
Milk = 20
Sugar = 10

Total = 130
```

***

# Visual Flow

```text
SimpleCoffee
      |
      v
MilkDecorator
      |
      v
SugarDecorator
```

Calls:

```text
GetCost()

SugarDecorator
      ↓
MilkDecorator
      ↓
SimpleCoffee

100 + 20 + 10
```

***

# Real-World Example: Notification System

Base notification:

```csharp
INotification
```

Concrete:

```csharp
EmailNotification
```

Decorators:

```csharp
LoggingNotificationDecorator
CachingNotificationDecorator
RetryNotificationDecorator
```

Usage:

```csharp
var notification =
    new RetryNotificationDecorator(
        new LoggingNotificationDecorator(
            new EmailNotification()
        ));
```

Behavior gets added dynamically.

***

# Real-World .NET Example: Streams

One of the best examples in .NET.

```csharp
Stream stream =
    new FileStream("data.txt", FileMode.Open);
```

Decorate with buffering:

```csharp
stream =
    new BufferedStream(stream);
```

Decorate with compression:

```csharp
stream =
    new GZipStream(stream, CompressionMode.Decompress);
```

Result:

```text
FileStream
     ↓
BufferedStream
     ↓
GZipStream
```

Each layer adds behavior without changing the original class.

***

# ASP.NET Core Middleware

Middleware follows a very similar idea.

```text
Request
   ↓
Authentication Middleware
   ↓
Logging Middleware
   ↓
Exception Middleware
   ↓
Controller
```

Each middleware decorates request processing with extra functionality.

***

# UML (Simplified)

```text
             ICoffee
                ▲
                |
          SimpleCoffee

                ▲
                |
        CoffeeDecorator
                ▲
       ┌────────┴────────┐
       │                 │
MilkDecorator   SugarDecorator
```

***

# Decorator vs Inheritance

### Using Inheritance

```text
CoffeeWithMilk
CoffeeWithSugar
CoffeeWithMilkAndSugar
CoffeeWithMilkAndSugarAndCream
```

Problems:

❌ Many classes

❌ Hard to maintain

***

### Using Decorator

```csharp
new SugarDecorator(
    new MilkDecorator(
        new SimpleCoffee()));
```

Benefits:

✅ Flexible

✅ Runtime composition

✅ No class explosion

***

# Decorator vs Composite

A favorite interview question.

## Composite

Represents a tree.

```text
Folder
 ├── File
 └── File
```

Focus:

✅ Hierarchies

***

## Decorator

Wraps an object.

```text
Coffee
  ↓
Milk
  ↓
Sugar
```

Focus:

✅ Additional behavior

***

# Decorator vs Adapter

Another common question.

### Adapter

Changes an interface.

```text
Old Interface
      ↓
   Adapter
      ↓
New Interface
```

Focus:

✅ Compatibility

***

### Decorator

Keeps the same interface but adds behavior.

```text
Component
      ↓
Decorator
```

Focus:

✅ Enhancement

***

# Advantages

✅ Adds behavior dynamically

✅ Avoids subclass explosion

✅ Follows Open/Closed Principle

✅ Flexible runtime composition

✅ Small focused classes

***

# Disadvantages

❌ Many small classes

❌ Can be harder to debug

❌ Deep decorator chains can be confusing

***

# When to Use

Use Decorator when:

* You need optional features.
* Multiple combinations of behavior exist.
* Inheritance would create too many subclasses.
* You want runtime flexibility.

Examples:

* Logging
* Caching
* Retry policies
* Compression
* Encryption
* UI enhancements
* Middleware pipelines

***

# Interview Question

### Q: What is the key difference between Decorator and Inheritance?

**Answer:**

Inheritance adds behavior at compile time:

```csharp
class CoffeeWithMilk : Coffee
{
}
```

Decorator adds behavior at runtime:

```csharp
ICoffee coffee =
    new MilkDecorator(
        new SimpleCoffee());
```

Decorator is more flexible because features can be combined dynamically.

***

# Decorator Pattern in One Line

> **Attach additional responsibilities to an object dynamically without modifying its original class.**

✅ **Structural Pattern #4 Complete: Decorator Pattern**

### Structural Patterns Progress

1. ✅ Adapter
2. ✅ Bridge
3. ✅ Composite
4. ✅ Decorator
5. ⏭️ Facade
6. ⏭️ Flyweight
7. ⏭️ Proxy

**Next Pattern: Facade Pattern**, which provides a simple interface over a complex subsystem and is one of the most commonly used patterns in enterprise applications.
