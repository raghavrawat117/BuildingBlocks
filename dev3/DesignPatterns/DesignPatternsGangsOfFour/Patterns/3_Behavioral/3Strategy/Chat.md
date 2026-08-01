Excellent. The **Strategy Pattern** is one of the most useful and interview-friendly patterns because it teaches a fundamental principle:

> **Encapsulate interchangeable algorithms and make them selectable at runtime.**

In modern .NET applications, Strategy is commonly used with:

* Dependency Injection
* Payment Processing
* Tax Calculations
* Discount Engines
* Sorting Algorithms
* Authentication Providers

***

# Strategy Pattern

## Definition

The Strategy Pattern defines a family of algorithms, encapsulates each one, and makes them interchangeable.

The client can choose which algorithm to use at runtime without changing its code.

***

# Real-World Analogy

Think of Google Maps.

You want to travel from Delhi to Noida.

Possible strategies:

```text
Car Route
Bike Route
Walking Route
Public Transport Route
```

The destination remains the same.

Only the routing algorithm changes.

***

# Problem Statement

Suppose an e-commerce application supports multiple payment methods:

* Credit Card
* UPI
* PayPal

Without Strategy:

```csharp
if(paymentMethod == "UPI")
{
    // UPI logic
}
else if(paymentMethod == "CreditCard")
{
    // Credit Card logic
}
else if(paymentMethod == "PayPal")
{
    // PayPal logic
}
```

Problems:

❌ Large if-else blocks

❌ Difficult to extend

❌ Violates Open/Closed Principle

***

# Solution

Create a common interface:

```csharp
IPaymentStrategy
```

Each payment method implements it.

The client chooses the strategy at runtime.

***

# Structure

```text
          IPaymentStrategy
                  ▲
      ┌───────────┼───────────┐
      │           │           │
 CreditCard     UPI       PayPal

                  ▲
                  |
             ShoppingCart
```

***

# Console Application Example

## Step 1: Strategy Interface

### IPaymentStrategy.cs

```csharp
namespace StrategyPatternDemo
{
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }
}
```

***

## Step 2: Concrete Strategies

### CreditCardPayment.cs

```csharp
using System;

namespace StrategyPatternDemo
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine(
                $"Paid ₹{amount} using Credit Card");
        }
    }
}
```

***

### UpiPayment.cs

```csharp
using System;

namespace StrategyPatternDemo
{
    public class UpiPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine(
                $"Paid ₹{amount} using UPI");
        }
    }
}
```

***

### PayPalPayment.cs

```csharp
using System;

namespace StrategyPatternDemo
{
    public class PayPalPayment : IPaymentStrategy
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine(
                $"Paid ₹{amount} using PayPal");
        }
    }
}
```

***

## Step 3: Context

The context uses a strategy but doesn't know implementation details.

### ShoppingCart.cs

```csharp
namespace StrategyPatternDemo
{
    public class ShoppingCart
    {
        private readonly IPaymentStrategy _paymentStrategy;

        public ShoppingCart(
            IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public void Checkout(decimal amount)
        {
            _paymentStrategy.Pay(amount);
        }
    }
}
```

***

## Step 4: Client

### Program.cs

```csharp
using System;

namespace StrategyPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Payment Method");
            Console.WriteLine("1 - Credit Card");
            Console.WriteLine("2 - UPI");
            Console.WriteLine("3 - PayPal");

            string choice = Console.ReadLine();

            IPaymentStrategy strategy = choice switch
            {
                "1" => new CreditCardPayment(),
                "2" => new UpiPayment(),
                "3" => new PayPalPayment(),
                _ => throw new Exception("Invalid choice")
            };

            ShoppingCart cart =
                new ShoppingCart(strategy);

            cart.Checkout(2500);

            Console.ReadKey();
        }
    }
}
```

***

# Sample Output

### Credit Card

```text
Select Payment Method

1 - Credit Card
2 - UPI
3 - PayPal

1

Paid ₹2500 using Credit Card
```

### UPI

```text
Paid ₹2500 using UPI
```

***

# Why This Is Better

Without Strategy:

```csharp
if(method == "UPI")
{
}
else if(method == "Card")
{
}
else if(method == "PayPal")
{
}
```

Every new payment type requires modifying existing code.

***

With Strategy:

Create a new class.

```csharp
public class CryptoPayment
    : IPaymentStrategy
{
}
```

No changes to `ShoppingCart`.

***

# Real-World Example: Discount Calculation

## Strategy Interface

```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal amount);
}
```

***

## Regular Customer

```csharp
public class RegularDiscount
    : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * 0.95m;
    }
}
```

***

## Premium Customer

```csharp
public class PremiumDiscount
    : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * 0.80m;
    }
}
```

***

## Context

```csharp
public class BillingService
{
    private readonly IDiscountStrategy _strategy;

    public BillingService(IDiscountStrategy strategy)
    {
        _strategy = strategy;
    }

    public decimal Calculate(decimal amount)
    {
        return _strategy.ApplyDiscount(amount);
    }
}
```

***

# Modern .NET + Dependency Injection Example

This is how you'll often use Strategy in ASP.NET Core.

## Interface

```csharp
public interface INotificationStrategy
{
    void Send(string message);
}
```

***

## Implementations

```csharp
public class EmailNotificationStrategy
    : INotificationStrategy
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}
```

```csharp
public class SmsNotificationStrategy
    : INotificationStrategy
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}
```

***

## Service Registration

```csharp
builder.Services.AddTransient<
    EmailNotificationStrategy>();

builder.Services.AddTransient<
    SmsNotificationStrategy>();
```

The application selects the appropriate strategy at runtime.

***

# UML (Simplified)

```text
         IPaymentStrategy
                 ▲
      ----------------------
      |          |         |
   Card        UPI      PayPal

                 ▲
                 |
            ShoppingCart
```

***

# Strategy vs State

This is a classic interview question.

## Strategy

The client chooses the algorithm.

```csharp
new ShoppingCart(
    new UpiPayment());
```

Focus:

✅ Different ways to perform a task.

***

## State

The object changes behavior automatically based on its internal state.

```text
Draft
 ↓
Published
 ↓
Archived
```

Focus:

✅ Behavior changes due to state transitions.

***

# Strategy vs Template Method

## Strategy

Uses composition.

```csharp
Context HAS-A Strategy
```

Example:

```csharp
ShoppingCart -> PaymentStrategy
```

***

## Template Method

Uses inheritance.

```csharp
AbstractClass
        ▲
        |
ConcreteClass
```

Algorithm structure is fixed.

***

# Strategy vs Command

## Strategy

Represents **how** something is done.

```csharp
Pay using UPI
Pay using Card
```

Focus:

✅ Algorithm selection

***

## Command

Represents **what** action should be executed.

```csharp
CreateOrderCommand
DeleteOrderCommand
```

Focus:

✅ Encapsulating requests

***

# Advantages

✅ Eliminates large if-else blocks

✅ Easy to extend

✅ Promotes Open/Closed Principle

✅ Runtime algorithm selection

✅ Better testing

✅ Cleaner code

***

# Disadvantages

❌ More classes

❌ Client must know which strategy to choose

❌ Can be overkill for very small logic

***

# When to Use

Use Strategy when:

* Multiple algorithms solve the same problem.
* Algorithm selection happens at runtime.
* You want to remove complex conditionals.
* New algorithms may be introduced later.

Examples:

* Payment methods
* Discount rules
* Tax calculations
* Shipping cost calculations
* Sorting algorithms
* Authentication providers

***

# Interview Question

### Q: What problem does the Strategy Pattern solve?

**Answer:**

The Strategy Pattern eliminates large conditional statements by encapsulating algorithms into separate classes and making them interchangeable at runtime.

Instead of:

```csharp
if(method == "UPI")
{
}
else if(method == "CreditCard")
{
}
```

you simply use:

```csharp
IPaymentStrategy strategy =
    new UpiPayment();

strategy.Pay(1000);
```

***

# Strategy Pattern in One Line

> **Define a family of algorithms, encapsulate each one, and make them interchangeable at runtime.**

✅ **Behavioral Pattern #3 Complete: Strategy Pattern**

### Behavioral Patterns Progress

1. ✅ Chain of Responsibility
2. ✅ Observer
3. ✅ Strategy
4. ⏭️ Command
5. ⏭️ Interpreter
6. ⏭️ Iterator
7. ⏭️ Mediator
8. ⏭️ Memento
9. ⏭️ State
10. ⏭️ Template Method
11. ⏭️ Visitor

**Next Pattern: Command Pattern**, another highly practical pattern used in CQRS, menu actions, undo/redo systems, job queues, and background task processing.
